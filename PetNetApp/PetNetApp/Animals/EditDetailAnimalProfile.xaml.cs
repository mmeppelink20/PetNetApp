/// <summary>
/// Andrew Schneider
/// Created: 2023/02/01
/// 
/// Interaction logic for EditDetailAnimalProfile.xaml
/// </summary>
///
/// <remarks>
/// Updater Name
/// Updated: yyyy/mm/dd
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
    public partial class EditDetailAnimalProfile : Page
    {
        private MasterManager _manager = null;
        private AnimalVM _animalVM = null;
        private ToolTip _toolTip = new ToolTip();
        Dictionary<string, List<string>> _breeds = null;
        List<string> _genders = null;
        List<string> _types = null;
        List<string> _statuses = null;
        List<string> _yesNo = new List<string> { "Yes", "No" };
        DateTime _maxBroughtInDate = DateTime.Now;
        DateTime _minBroughtInDate = DateTime.Today - TimeSpan.FromDays(3);
        private List<Images> _imagesList = null;

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/02/01
        /// 
        /// Constructor for building the EditDetailAnimalProfile page
        /// where the user can view and edit an animal profile record
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="manager">An instance of the master manager</param>
        /// <param name="animal">Animal clicked on AnimalListUserControl.xaml page</param>
        public EditDetailAnimalProfile(MasterManager manager, AnimalVM animal)
        {
            InitializeComponent();
            _manager = manager;
            _animalVM = animal;
        }

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/02/01
        /// </summary>
        /// <remarks>
        /// Added a line of code to change the selected UI buttons when this page loads in case something returns to it(MedicalNavigationPage)
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            PopulateComboBoxes();
            SetDetailMode();
            PopulateImage();
        }

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/02/02
        /// 
        /// Helper method for setting the page for viewing (detail mode). Changes visibility,
        /// enabled, and read only settings for text boxes and combo boxes as needed. Changes
        /// buttons' content property
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        private void SetDetailMode()
        {
            btnEditSave.Content = "Edit";
            btnBack.Content = "Back";
            PopulateControls();

            txtAnimalTypeId.Visibility = Visibility.Visible;
            txtAnimalBreedId.Visibility = Visibility.Visible;
            txtAnimalGender.Visibility = Visibility.Visible;
            txtAnimalStatusId.Visibility = Visibility.Visible;
            txtAggressive.Visibility = Visibility.Visible;
            txtChildFriendly.Visibility = Visibility.Visible;
            txtNeuterStatus.Visibility = Visibility.Visible;

            cmbAnimalTypeId.Visibility = Visibility.Hidden;
            cmbAnimalBreedId.Visibility = Visibility.Hidden;
            cmbAnimalGender.Visibility = Visibility.Hidden;
            cmbAnimalStatusId.Visibility = Visibility.Hidden;
            cmbAggressive.Visibility = Visibility.Hidden;
            cmbChildFriendly.Visibility = Visibility.Hidden;
            cmbNeuterStatus.Visibility = Visibility.Hidden;

            txtAnimalName.IsReadOnly = true;
            txtAnimalId.IsReadOnly = true;
            txtAnimalId.IsEnabled = true;
            txtAnimalTypeId.IsReadOnly = true;
            txtAnimalBreedId.IsReadOnly = true;
            txtAnimalGender.IsReadOnly = true;
            txtKennelName.IsReadOnly = true;
            txtKennelName.IsEnabled = true;
            txtBroughtIn.IsReadOnly = true;
            txtBroughtIn.IsEnabled = true;
            txtAnimalStatusId.IsReadOnly = true;
            txtDescription.IsReadOnly = true;
            txtPersonality.IsReadOnly = true;
            txtMicrochipNumber.IsReadOnly = true;
            txtAggressive.IsReadOnly = true;
            txtAggressiveDescription.IsReadOnly = true;
            txtChildFriendly.IsReadOnly = true;
            txtNeuterStatus.IsReadOnly = true;
            txtNotes.IsReadOnly = true;

            string[] allowedKennelRoles = { "Admin", "Manager", "Maintenance" };
            if (_manager.User.Roles.Exists(role => allowedKennelRoles.Contains(role)))
            {
                btnKennelPage.Visibility = Visibility.Visible;
            }
            else
            {
                btnKennelPage.Visibility = Visibility.Collapsed;
            }

            if (_animalVM.AnimalStatusId == "Healthy")
            {
                btnAdoptionProfile.Visibility = Visibility.Visible;
            }
            else
            {
                btnAdoptionProfile.Visibility = Visibility.Collapsed;
            }

            string[] allowedMedicalRoles = { "Admin", "Manager", "Vet" };
            if (_manager.User.Roles.Exists(role => allowedMedicalRoles.Contains(role)))
            {
                btnMedicalProfile.Visibility = Visibility.Visible;
            }
            else
            {
                btnMedicalProfile.Visibility = Visibility.Hidden;
            }
        }

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/02/02
        /// 
        /// Helper method for setting the page to editing mode. Changes visibility, enabled, and read
        /// only settings for text boxes and combo boxes as needed. Changes buttons' content property
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        private void SetEditMode()
        {
            btnEditSave.Content = "Save";
            btnBack.Content = "Cancel";

            txtAnimalTypeId.Visibility = Visibility.Hidden;
            txtAnimalBreedId.Visibility = Visibility.Hidden;
            txtAnimalGender.Visibility = Visibility.Hidden;
            txtAnimalStatusId.Visibility = Visibility.Hidden;
            txtAggressive.Visibility = Visibility.Hidden;
            txtChildFriendly.Visibility = Visibility.Hidden;
            txtNeuterStatus.Visibility = Visibility.Hidden;

            cmbAnimalTypeId.Visibility = Visibility.Visible;
            cmbAnimalBreedId.Visibility = Visibility.Visible;
            cmbAnimalGender.Visibility = Visibility.Visible;
            cmbAnimalStatusId.Visibility = Visibility.Visible;
            cmbAggressive.Visibility = Visibility.Visible;
            cmbChildFriendly.Visibility = Visibility.Visible;
            cmbNeuterStatus.Visibility = Visibility.Visible;

            txtAnimalName.IsReadOnly = false;
            txtAnimalId.IsReadOnly = true;
            txtAnimalId.IsEnabled = false;
            txtAnimalTypeId.IsReadOnly = false;
            txtAnimalBreedId.IsReadOnly = true;
            txtAnimalGender.IsReadOnly = false;
            txtKennelName.IsReadOnly = true;
            txtKennelName.IsEnabled = false;
            txtBroughtIn.IsReadOnly = true;
            txtBroughtIn.IsEnabled = false;
            txtAnimalStatusId.IsReadOnly = false;
            txtDescription.IsReadOnly = false;
            txtPersonality.IsReadOnly = false;
            txtMicrochipNumber.IsReadOnly = false;
            txtAggressive.IsReadOnly = false;
            if (cmbAggressive.SelectedItem.ToString() == "Yes")
            {
                txtAggressiveDescription.IsEnabled = true;
                txtAggressiveDescription.IsReadOnly = false;
            }
            else
            {
                txtAggressiveDescription.IsEnabled = false;
                txtAggressiveDescription.IsReadOnly = true;
            }

            txtChildFriendly.IsReadOnly = false;
            txtNeuterStatus.IsReadOnly = false;
            txtNotes.IsReadOnly = false;
        }

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/02/02
        /// 
        /// Helper method for populating values onto the text boxes
        /// and selecting the combo boxes' selected items using the 
        /// values stored on the animal object
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        private void PopulateControls()
        {
            _toolTip.Content = _animalVM.AnimalStatusDescription;
            ToolTipService.SetToolTip(txtAnimalStatusId, _toolTip);

            lblTitle.Content = _animalVM.AnimalName + "'s Profile";
            txtAnimalName.Text = _animalVM.AnimalName;
            txtAnimalId.Text = _animalVM.AnimalId.ToString();
            txtAnimalTypeId.Text = _animalVM.AnimalTypeId;
            cmbAnimalTypeId.SelectedItem = _animalVM.AnimalTypeId;
            txtAnimalBreedId.Text = _animalVM.AnimalBreedId;
            cmbAnimalBreedId.SelectedItem = _animalVM.AnimalBreedId;
            txtAnimalGender.Text = _animalVM.AnimalGender;
            cmbAnimalGender.SelectedItem = _animalVM.AnimalGender;
            txtKennelName.Text = _animalVM.KennelName;
            DateTime broughtIn = new DateTime();
            broughtIn = _animalVM.BroughtIn;
            txtBroughtIn.Text = broughtIn.ToShortDateString();
            txtAnimalStatusId.Text = _animalVM.AnimalStatusId;
            cmbAnimalStatusId.SelectedItem = _animalVM.AnimalStatusId;
            txtDescription.Text = _animalVM.Description;
            txtPersonality.Text = _animalVM.Personality;
            txtMicrochipNumber.Text = _animalVM.MicrochipNumber;
            txtAggressive.Text = (_animalVM.Aggressive) ? "Yes" : "No";
            cmbAggressive.SelectedItem = (_animalVM.Aggressive) ? "Yes" : "No";
            txtAggressiveDescription.Text = _animalVM.AggressiveDescription;
            txtChildFriendly.Text = (_animalVM.ChildFriendly) ? "Yes" : "No";
            cmbChildFriendly.SelectedItem = (_animalVM.ChildFriendly) ? "Yes" : "No";
            txtNeuterStatus.Text = (_animalVM.NeuterStatus) ? "Yes" : "No";
            cmbNeuterStatus.SelectedItem = (_animalVM.NeuterStatus) ? "Yes" : "No";
            txtNotes.Text = _animalVM.Notes;
        }

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/02/02
        /// 
        /// Helper method for populating the combo boxes with all the available data that can be
        /// selected when editing the animal profile record. The _yesNo list is used to present
        /// the user with human readable Yes/No options that can be coverted to booleans in the
        /// background.
        /// </summary>
        ///
        /// <remarks>
        /// Stephen Jaurigue
        /// Updated: 2023/02/28 
        /// Fixed a problem where the breeds list was getting set to the dictionary items
        /// instead of being left blank until a type was selected
        /// </remarks>
        /// <exception cref="Exception"></exception>
        private void PopulateComboBoxes()
        {
            try
            {
                _breeds = _manager.AnimalManager.RetrieveAllAnimalBreeds();
                _types = _manager.AnimalManager.RetrieveAllAnimalTypes();
                cmbAnimalTypeId.ItemsSource = _types;

                _genders = _manager.AnimalManager.RetrieveAllAnimalGenders();
                cmbAnimalGender.ItemsSource = _genders;
                _statuses = _manager.AnimalManager.RetrieveAllAnimalStatuses();
                cmbAnimalStatusId.ItemsSource = _statuses;
                cmbAggressive.ItemsSource = _yesNo;
                cmbChildFriendly.ItemsSource = _yesNo;
                cmbNeuterStatus.ItemsSource = _yesNo;
            }
            catch (Exception ex)
            {
                PromptWindow.ShowPrompt("Error", "An error occured populating combo boxes\n" + ex, ButtonMode.Ok);
                NavigationService.Navigate(WpfPresentation.Animals.AnimalListPage.GetAnimalListPage(_manager));
            }
        }

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/03/06
        /// 
        /// Helper method for populating the animal image. If no image is
        /// available a label becomes visible alerting the user to this fact.
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        private void PopulateImage()
        {
            if (_imagesList == null || _imagesList.Count == 0)
            {
                try
                {
                    _imagesList = _manager.ImagesManager.RetrieveAnimalImagesByAnimalId(_animalVM.AnimalId);
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
                    imgAnimal.Source = _manager.ImagesManager.RetrieveImageByImages(_imagesList[0]);
                    lblNoImage.Visibility = Visibility.Hidden;
                }
                catch (Exception)
                {
                    BitmapImage brokenImage = new BitmapImage();
                    brokenImage.BeginInit();
                    brokenImage.UriSource = new Uri(@"/Images/BrokenImageGreen.png", UriKind.Relative);
                    brokenImage.EndInit();
                    imgAnimal.Source = brokenImage;
                    imgAnimal.Height = 250;
                    imgAnimal.Width = 250;
                    lblNoImage.Visibility = Visibility.Hidden;
                }
            }
        }

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/02/02
        /// 
        /// Click event method that performs two different operations based on the content
        /// property of the button. If "Edit" the method setEditMode() is called. If "Save"
        /// validation checking is performed on the editable fields and an attempt is made
        /// to update the animal record in the database.
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditSave_Click(object sender, RoutedEventArgs e)
        {
            if (btnEditSave.Content.ToString() == "Edit")
            {
                SetEditMode();
            }
            // If button text is not "Edit" then it is "Save" and we need to validate and save the entered information.
            else
            {
                if (txtAnimalName.Text == "" || cmbAnimalTypeId.SelectedItem == null || cmbAnimalBreedId.SelectedItem == null ||
                    cmbAnimalGender.SelectedItem == null || cmbAnimalStatusId.SelectedItem == null || txtBroughtIn.Text == "" ||
                    cmbAggressive.SelectedItem == null || cmbChildFriendly.SelectedItem == null || cmbNeuterStatus.SelectedItem == null)
                {
                    PromptWindow.ShowPrompt("Error", "Please enter all fields.", ButtonMode.Ok);
                }
                else
                {
                    AnimalVM newAnimal = new AnimalVM();

                    bool goodData = true;

                    // Validate animal name
                    if (goodData)
                    {
                        if (txtAnimalName.Text.IsValidFirstName())
                        {
                            goodData = true;
                            newAnimal.AnimalName = txtAnimalName.Text;
                        }
                        else
                        {
                            goodData = false;
                            PromptWindow.ShowPrompt("Format Error", "Animal name must be in proper format.",
                                                    ButtonMode.Ok);
                            txtAnimalName.Focus();
                            txtAnimalName.SelectAll();
                        }
                    }

                    // Validate microchip number length
                    if (goodData)
                    {
                        // Check if anything has been entered (it's nullable so it could be left blank)
                        if (txtMicrochipNumber.Text.Length > 0)
                        {
                            // Check if input will work with the database datatype
                            if (txtMicrochipNumber.Text.Length > 15)
                            {
                                goodData = false;
                                PromptWindow.ShowPrompt("Data Error", "Microchip number can only be\n15 characters long.",
                                                        ButtonMode.Ok);
                                txtMicrochipNumber.Focus();
                                txtMicrochipNumber.SelectAll();
                            }
                            else
                            {
                                goodData = true;
                            }
                        }
                    }

                    // If validation has passed (goodData is still true) try to update the animal profile record
                    if (goodData)
                    {
                        newAnimal.AnimalId = _animalVM.AnimalId;
                        newAnimal.AnimalShelterId = _animalVM.AnimalShelterId;
                        newAnimal.AnimalTypeId = cmbAnimalTypeId.SelectedItem.ToString();
                        newAnimal.AnimalBreedId = cmbAnimalBreedId.SelectedItem.ToString();
                        newAnimal.AnimalGender = cmbAnimalGender.SelectedItem.ToString();
                        newAnimal.KennelName = txtKennelName.Text;
                        newAnimal.BroughtIn = _animalVM.BroughtIn;
                        newAnimal.AnimalStatusId = cmbAnimalStatusId.SelectedItem.ToString();
                        newAnimal.Description = txtDescription.Text;
                        newAnimal.Personality = txtPersonality.Text;
                        newAnimal.MicrochipNumber = txtMicrochipNumber.Text;
                        if (cmbAggressive.SelectedItem.ToString() == "Yes")
                        {
                            newAnimal.Aggressive = true;
                            newAnimal.AggressiveDescription = txtAggressiveDescription.Text;
                        }
                        else
                        {
                            newAnimal.Aggressive = false;
                            newAnimal.AggressiveDescription = "";
                        }

                        if (cmbChildFriendly.SelectedItem.ToString() == "Yes")
                        {
                            newAnimal.ChildFriendly = true;
                        }
                        else
                        {
                            newAnimal.ChildFriendly = false;
                        }
                        if (cmbNeuterStatus.SelectedItem.ToString() == "Yes")
                        {
                            newAnimal.NeuterStatus = true;
                        }
                        else
                        {
                            newAnimal.NeuterStatus = false;
                        }
                        newAnimal.Notes = txtNotes.Text;

                        try
                        {
                            if (_manager.AnimalManager.EditAnimal(_animalVM, newAnimal))
                            {
                                // success
                                PromptWindow.ShowPrompt("Success", "Animal record has been updated", ButtonMode.Ok);
                                _animalVM = newAnimal;
                                SetDetailMode();
                            }
                            else
                            {
                                PromptWindow.ShowPrompt("Error", "An error occured.\nPlease try again.", ButtonMode.Ok);
                            }
                        }
                        catch (Exception ex)
                        {
                            PromptWindow.ShowPrompt("Error", "Update failed.\n" + ex, ButtonMode.Ok);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/02/03
        /// 
        /// Click event method that performs two different operations based on the content property
        /// of the button. If "Back" the user is navigated back to the page from which they came
        /// (usually AnimalListUserControl.xaml). If "Cancel" a popup is shown to confirm if the
        /// user actually intends to stop editing without saving changes. 
        /// </summary>
        ///
        /// <remarks>
        /// Stephen Jaurigue
        /// Updated: 2023/02/28 
        /// Fixed a problem where the user returns to this page after navigating somewhere else that
        /// the back button doesn't go to the AnimalListPage
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Back_Click(object sender, RoutedEventArgs e)
        {
            if (btnBack.Content.ToString() == "Back")
            {
                NavigationService.Navigate(AnimalListPage.GetAnimalListPage(_manager));
            }
            // If button text is not "Back" then it is "Cancel" and we need to prompt the user for confirmation
            else
            {
                var result = PromptWindow.ShowPrompt("Discard Changes", "Are you sure you want to cancel?\n" +
                                                    "Changes will not be saved.", ButtonMode.YesNo);
                if (result == PromptSelection.Yes)
                {
                    SetDetailMode();
                }
            }
        }

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/02/03
        /// 
        /// Click event method that takes the user to the adoption profile of the animal they are
        /// currently viewing. If the user is currently editing (btnEditSave content = "Save") a
        /// popup is shown to confirm if the user actually intends to stop editing and leave the
        /// page without saving changes. 
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdoptionProfile_Click(object sender, RoutedEventArgs e)
        {
            var result = PromptWindow.ShowPrompt("Discard Changes", "Are you sure you want to leave?\n" +
                                                    "Changes will not be saved.", ButtonMode.YesNo);
            if (result == PromptSelection.Yes)
            {
                SetDetailMode();
                var page = AnimalsPage.GetAnimalsPage();
                page.ChangeSelectedButton(page.btnAdopt);
                NavigationService.Navigate(new WpfPresentation.Animals.
                    ViewAdoptableAnimalProfile(_animalVM.AnimalId));
            }
            else
            {
                var page = AnimalsPage.GetAnimalsPage();
                page.ChangeSelectedButton(page.btnAdopt);
                NavigationService.Navigate(new WpfPresentation.Animals.
                        ViewAdoptableAnimalProfile(_animalVM.AnimalId));
            }
        }

    /// <summary>
    /// Andrew Schneider
    /// Created: 2023/02/03
    /// 
    /// Click event method that takes the user to the kennel page. If the user is currently editing
    /// (btnEditSave content = "Save") a popup is shown to confirm if the user actually intends to
    /// stop editing and leave the page without saving changes. 
    /// </summary>
    ///
    /// <remarks>
    /// Updater Name
    /// Updated: yyyy/mm/dd 
    /// example: Fixed a problem when user inputs bad data
    /// </remarks>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void btnKennelPage_Click(object sender, RoutedEventArgs e)
    {
        if (btnEditSave.Content.ToString() == "Save")
        {
            var result = PromptWindow.ShowPrompt("Discard Changes", "Are you sure you want to leave?\n" +
                                                "Changes will not be saved.", ButtonMode.YesNo);
            if (result == PromptSelection.Yes)
            {
                SetDetailMode();
                NavigationService.Navigate(new WpfPresentation.Management.ViewKennelPage());
            }
        }
        else
        {
            NavigationService.Navigate(new WpfPresentation.Management.ViewKennelPage());
        }
    }

    /// <summary>
    /// Andrew Schneider
    /// Created: 2023/02/03
    /// 
    /// Click event method that takes the user to the medical profile of the animal they are
    /// currently viewing. If the user is currently editing (btnEditSave content = "Save") a
    /// popup is shown to confirm if the user actually intends to stop editing and leave the
    /// page without saving changes. 
    /// </summary>
    ///
    /// <remarks>
    /// Stephen Jaurigue
    /// Updated: 2023/02/28 
    /// Fixed a problem where medical page back feature wouldn't return to the animal profile page
    /// </remarks>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void btnMedicalProfile_Click(object sender, RoutedEventArgs e)
    {
        if (btnEditSave.Content.ToString() == "Save")
        {
            var result = PromptWindow.ShowPrompt("Discard Changes", "Are you sure you want to leave?\n" +
                                                "Changes will not be saved.", ButtonMode.YesNo);
            if (result == PromptSelection.Yes)
            {
                SetDetailMode();
                NavigationService nav = NavigationService.GetNavigationService(this);
                nav.Navigate(new WpfPresentation.Animals.MedicalNavigationPage(_manager, _animalVM));
            }
        }
        else
        {
            NavigationService.Navigate(new MedicalNavigationPage(_manager, _animalVM, this));
            var animalsPage = AnimalsPage.GetAnimalsPage();
            animalsPage.ChangeSelectedButton(animalsPage.btnMedical);
            // nav.Navigate(new WpfPresentation.Animals.AnimalMedicalProfile(_animalVM.AnimalId));
        }
    }

    /// <summary>
    /// Andrew Schneider
    /// Created: 2023/02/22
    /// 
    /// Helper method that links the breeds and types combo boxes so that when an animal type
    /// is selected only breeds of that type are available in the breeds box.
    /// </summary>
    ///
    /// <remarks>
    /// Updater Name
    /// Updated: yyyy/mm/dd 
    /// example: Fixed a problem when user inputs bad data
    /// </remarks>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void cmbAnimalTypeId_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        cmbAnimalBreedId.ItemsSource = _breeds[cmbAnimalTypeId.SelectedItem.ToString()];
        cmbAnimalBreedId.IsEnabled = true;
    }

    /// <summary>
    /// Andrew Schneider
    /// Created: 2023/02/22
    /// 
    /// Helper method that links the Aggressive combo box with the Aggressive Description textbox,
    /// so that a description can only be entered if "Yes" has been selected in the combo box.
    /// </summary>
    ///
    /// <remarks>
    /// Updater Name
    /// Updated: yyyy/mm/dd 
    /// example: Fixed a problem when user inputs bad data
    /// </remarks>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void cmbAggressive_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (cmbAggressive.SelectedItem.ToString() == "Yes")
        {
            txtAggressiveDescription.IsEnabled = true;
            txtAggressiveDescription.IsReadOnly = false;
        }
        else
        {
            txtAggressiveDescription.IsEnabled = false;
            txtAggressiveDescription.IsReadOnly = true;
        }
    }
}
}
