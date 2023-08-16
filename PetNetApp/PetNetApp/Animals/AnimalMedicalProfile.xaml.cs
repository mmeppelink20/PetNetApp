///<summary>
///William Rients
/// Created: 2023/03/10
///
/// </summary>
/// <remarks>
/// Zaid Rachman
/// Updated: 2023/04/21
/// Final QA
/// </remarks>
using DataObjects;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfPresentation.Animals
{
    /// <summary>
    /// Interaction logic for AnimalMedicalProfile.xaml
    /// </summary>
    public partial class AnimalMedicalProfile : Page
    {
        private int _animalId;
        private List<Images> _imagesList;
        Kennel _kennel = new Kennel();
        AnimalVM _animalVM = new AnimalVM();
        MasterManager _masterManager = MasterManager.GetMasterManager();
        /// <summary>
        /// William Rients
        /// Created: 2023/03/11
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/21
        /// Final QA
        /// </remarks>
        /// <param name="animalId"></param>
        private ViewPrescriptions _viewPrescriptions = null;

        public AnimalMedicalProfile(int animalId)
        {
            InitializeComponent();
            _animalId = animalId;
        }

        /// <summary>
        /// William Rients
        /// Created: 2023/02/17
        /// 
        /// Sets all of the controls to be read only
        /// </summary>
        ///
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/21
        /// Final QA
        /// </remarks>
        public void DisableControls()
        {
            txtAnimalBreed.IsEnabled = false;
            txtAnimalId.IsEnabled = false;
            txtAnimalKennelNum.IsEnabled = false;
            txtAnimalMicrochipNum.IsEnabled = false;
            txtAnimalName.IsEnabled = false;
            txtAnimalNotes.IsEnabled = false;
            rdbAnimalAlteredNo.IsEnabled = false;
            rdbAnimalAlteredYes.IsEnabled = false;
            rdbAnimalGenderFemale.IsEnabled = false;
            rdbAnimalGenderMale.IsEnabled = false;
            btnSave.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// William Rients
        /// Created: 2023/02/17
        /// 
        /// When loaded, disableControls() is called and
        /// all animals information populates the text boxes
        /// </summary>
        ///
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/21
        /// Final QA
        /// </remarks>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DisableControls();
            try
            {
                _animalVM = _masterManager.AnimalManager.RetrieveAnimalMedicalProfileByAnimalId(_animalId);
                _kennel = _masterManager.KennelManager.RetrieveKennelIdByAnimalId(_animalId);
                lblProfileName.Content = _animalVM.AnimalName + "'s Medical Profile";
                txtAnimalBreed.Text = _animalVM.AnimalBreedId;
                txtAnimalId.Text = _animalVM.AnimalId.ToString();
                txtAnimalMicrochipNum.Text = _animalVM.MicrochipNumber;
                txtAnimalName.Text = _animalVM.AnimalName;
                txtAnimalNotes.Text = _animalVM.Notes;
                if (_kennel.KennelId == 0)
                {
                    txtAnimalKennelNum.Text = "Unassigned";
                }
                else
                {
                    txtAnimalKennelNum.Text = _kennel.KennelId.ToString();
                }
                if (_animalVM.AnimalGender == "Male")
                {
                    rdbAnimalGenderMale.IsChecked = true;
                }
                else if (_animalVM.AnimalGender == "Female")
                {
                    rdbAnimalGenderFemale.IsChecked = true;
                }
                if (_animalVM.NeuterStatus == true)
                {
                    rdbAnimalAlteredYes.IsChecked = true;
                }
                else if (_animalVM.NeuterStatus == false)
                {
                    rdbAnimalAlteredYes.IsChecked = true;
                }
                PopulateImage();
            }
            catch (Exception ex)
            {
                PromptWindow.ShowPrompt("Error", ex.Message + "\n\n" + ex.InnerException.Message, ButtonMode.Ok);
            }            
        }

        /// <summary>
        /// William Rients
        /// Created: 2023/03/24
        /// 
        /// Populates the animals medical image
        /// </summary>
        ///
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/21
        /// Final QA
        /// </remarks>
        private void PopulateImage()
        {
            if (_imagesList == null || _imagesList.Count == 0)
            {
                try
                {
                    _imagesList = _masterManager.ImagesManager.RetrieveMedicalImagesByAnimalId(_animalId);
                }
                catch (Exception ex)
                {
                    PromptWindow.ShowPrompt("Error", ex.Message + "\n\n" + ex.InnerException.Message);
                }
            }

            if (_imagesList.Count == 0)
            {
                lblNoImage.Visibility = Visibility.Visible;
            }
            else
            {
                try
                {
                    imgMedAnimal.Source = _masterManager.ImagesManager.RetrieveImageByImages(_imagesList[0]);
                    lblNoImage.Visibility = Visibility.Hidden;
                }
                catch (Exception)
                {
                    BitmapImage brokenImage = new BitmapImage();
                    brokenImage.BeginInit();
                    brokenImage.UriSource = new Uri(@"/Images/BrokenImageGreen.png", UriKind.Relative);
                    brokenImage.EndInit();
                    imgMedAnimal.Source = brokenImage;
                    imgMedAnimal.Height = 250;
                    imgMedAnimal.Width = 250;
                    lblNoImage.Visibility = Visibility.Hidden;
                }
            }
        }

        /// <summary>
        /// Author: Asa Armstrong
        /// Date: 2023/04/26
        /// Description: Navigates to AddEditAnimalDeath page
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/27
        /// 
        /// FinalQA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeath_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GetNavigationService(this).Navigate(new AddAnimalDOD513(_animalVM, _kennel));
            
         
        }
        /// <summary>
        /// Tyler hand
        /// Created: 2023/04/17
        /// 
        /// Click 
        /// all animals information populates the text boxes
        /// </summary>
        ///
        private void btnAddPrescriptions_Click(object sender, RoutedEventArgs e)
        {

            NavigationService.Navigate(new ViewPrescriptions(_animalId));
        }
    }
}
