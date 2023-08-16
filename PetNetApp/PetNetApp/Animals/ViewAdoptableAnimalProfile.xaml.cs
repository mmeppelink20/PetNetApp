using DataObjects;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for ViewAdoptableAnimalProfile.xaml
    /// </summary>
    public partial class ViewAdoptableAnimalProfile : Page
    {
        private int _animalId = 0;
        public AnimalVM animalVM = new AnimalVM();
        private MasterManager _masterManager = MasterManager.GetMasterManager();
        private int curImageIdx = 0;
        private List<Images> _animalImages = null;
        private List<AnimalUpdates> _animalUpdates = null;

        public ViewAdoptableAnimalProfile(int animalId)
        {
            InitializeComponent();
            _animalId = animalId;
        }

        /// <summary>
        /// Hoang Chu
        /// Created: 2023/02/17
        /// 
        /// </summary>
        /// Method that display animal profile
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        private void DisplayAnimalProfile()
        {
            lblAnimalProfileName.Content = animalVM.AnimalName;
            lblAnimalBreed.Content = animalVM.AnimalBreedId;
            lblAnimalShelter.Content = "ShelterID : " + animalVM.AnimalShelterId;
            txtAnimalDescription.Text = animalVM.Description;
        }

        /// <summary>
        /// Hoang Chu
        /// Created: 2023/02/17
        /// 
        /// </summary>
        /// Method get the animal images
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        private void GetImageFile()
        {
            try
            {
                _animalImages = _masterManager.ImagesManager.RetriveImageByAnimalId(_animalId);
                
            }
            catch (Exception ex)
            {
                PromptWindow.ShowPrompt("Error", "Can not get the images. \n\n" + ex.Message);
            }
        }

        /// <summary>
        /// Hoang Chu
        /// Created: 2023/02/17
        /// 
        /// </summary>
        /// Method load the image to the image frame
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        private void LoadImage()
        {
            //picAnimalImageList.Source = new BitmapImage(new Uri(imageFiles[curImageIdx], UriKind.Relative));
            try
            {
                if (_animalImages.Count == 0)
                {
                    picAnimalImageList.Source = new BitmapImage(new Uri(@"../../Images/AnimalImage.png", UriKind.Relative));
                }
                else
                {
                    picAnimalImageList.Source = _masterManager.ImagesManager.RetrieveImageByImages(_animalImages[curImageIdx]);
                }
                
            }
            catch (Exception ex)
            {
                picAnimalImageList.Source = new BitmapImage(new Uri(@"../../Images/NullImage.png", UriKind.Relative));
            }
            LoadAnimalNote();
        }

        /// <summary>
        /// Hoang Chu
        /// Created: 2023/02/17
        /// 
        /// </summary>
        /// Method change the animal images to the previous one in the list
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPreviousImage_Click(object sender, RoutedEventArgs e)
        {
            if(curImageIdx > 0)
            {
                curImageIdx--;
                LoadImage();
            }
        }

        /// <summary>
        /// Hoang Chu
        /// Created: 2023/02/17
        /// 
        /// </summary>
        /// Method change the animal images to the next one in the list
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNextImage_Click(object sender, RoutedEventArgs e)
        {
            if (curImageIdx < _animalImages.Count - 1)
            {
                curImageIdx++;
                LoadImage();
            }
        }

        /// <summary>
        /// Hoang Chu
        /// Created: 2023/02/17
        /// 
        /// </summary>
        /// Method load everything in the page when the page is loaded
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

            try
            {
                animalVM = _masterManager.AnimalManager.RetrieveAnimalAdoptableProfile(_animalId);
                
            }
            catch (Exception ex)
            {
                PromptWindow.ShowPrompt("Error", "Can not get the data. \n\n" + ex.Message);
            }
            
            DisplayAnimalProfile();
            GetImageFile();
            LoadImage();
        }

        /// <summary>
        /// Hoang Chu
        /// Created: 2023/02/12
        /// 
        /// </summary>
        /// Button that save animal note to database
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPostComment_Click(object sender, RoutedEventArgs e)
        {
            SaveAnimalUpdateToDataBase();
        }

        /// <summary>
        /// Hoang Chu
        /// Created: 2023/02/12
        /// 
        /// </summary>
        /// Save the animal note to database
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        private void SaveAnimalUpdateToDataBase()
        {
            string animalRecordNotes = "";
            if (tbxAnimalPostUpdate.Text == "" || tbxAnimalPostUpdate.Text == null)
            {
                PromptWindow.ShowPrompt("Error", "Please enter your note first.");
            }
            else
            {
                animalRecordNotes = tbxAnimalPostUpdate.Text;
                PromptSelection dialogResult = PromptWindow.ShowPrompt("Animal Update Note", "Do you want to record this note?", ButtonMode.YesNo);
                if (dialogResult == PromptSelection.Yes)
                {
                    try
                    {
                        if (_masterManager.AnimalUpdatesManager.AddAnimalUpdatesByAnimalId(_animalId, animalRecordNotes))
                        {
                            PromptWindow.ShowPrompt("", "Update Note Success.");
                            tbxAnimalPostUpdate.Text = "Enter your note here.";
                            LoadAnimalNote();
                        }
                    }
                    catch (Exception ex)
                    {
                        PromptWindow.ShowPrompt("Error", ex.Message);
                    }
                }
                else
                {
                    PromptWindow.ShowPrompt("Cancel", "Cancel update animal post");
                    tbxAnimalPostUpdate.Text = "Enter your note here.";
                }
            }
        }

        /// <summary>
        /// Hoang Chu
        /// Created: 2023/02/12
        /// 
        /// </summary>
        /// Show the animal note
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        private string LoadAnimalNote()
        {
            string result = "";

            try
            {
                result = _masterManager.AnimalUpdatesManager.RetrieveAnimalUpdatesByAnimal(_animalId);
                if (result != "")
                {
                    tbkAnimalNote.Text = result;
                }
                _animalUpdates = _masterManager.AnimalUpdatesManager.RetrieveAllAnimalUpdatesByAnimalId(_animalId);
                if (_animalUpdates.Count == 0)
                {
                    _animalUpdates = new List<AnimalUpdates>();
                }
            }
            catch (Exception ex)
            {
                PromptWindow.ShowPrompt("Error", ex.Message);
            }

            return result;
        }

        /// <summary>
        /// Author: Hoang Chu
        /// 04/21/2023
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbxAnimalPostUpdate_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                SaveAnimalUpdateToDataBase();
            }
        }

        /// <summary>
        /// Author: Hoang Chu
        /// 04/21/2023
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnViewAllComment_Click(object sender, RoutedEventArgs e)
        {
            var animalNoteWindow = new AnimalUpdatesWindow(_animalUpdates);
            animalNoteWindow.ShowDialog();
        }

        /// <summary>
        /// Author: Hoang Chu
        /// 04/21/2023
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnViewApplications_Click(object sender, RoutedEventArgs e)
        {
            AdoptionApplicantsWindow adoptionApplicantsWindow = new AdoptionApplicantsWindow(animalVM, _masterManager);
            adoptionApplicantsWindow.Show();
        }
    }
}
