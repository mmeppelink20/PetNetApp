/// <summary>
/// John
/// Created: 2023/02/03
/// 
/// Interaction logic for AddAnimalPage.xaml
/// </summary>
///
/// <remarks>
/// Andrew Schneider
/// Updated: 2023/02/22
/// </remarks>

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
using LogicLayer;
using DataObjects;

namespace WpfPresentation.Animals
{
    public partial class AddAnimalPage : Page
    {
        private MasterManager _manager = null;
        private AnimalVM _newAnimal = null;
        private Dictionary<string, List<string>> _breeds = null;
        private List<string> _genders = null;
        private List<string> _types = null;
        private List<string> _statuses = null;
        private List<Kennel> _kennels = null;
        private List<string> _yesNo = new List<string> { "Yes", "No" };
        private List<Images> _imagesList;

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/02/23
        /// 
        /// Constructor for building the AddAnimalProfile page
        /// where the user can add a new animal profile record
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="manager">An instance of the master manager</param>
        public AddAnimalPage(MasterManager manager)
        {
            _manager = manager;
            InitializeComponent();
        }

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/02/01
        /// 
        /// Page Loaded event method. Sets certain values and calls populateComboBoxes().
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            dpReceivedDate.SelectedDate = DateTime.Today;
            dpReceivedDate.IsEnabled = false;
            PopulateComboBoxes();
            wpAnimalImages.Children.Clear();
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
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <exception cref="Exception">Fails to retrieve data to fill combo boxes</exception>
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
                _kennels = _manager.KennelManager.RetrieveAllEmptyKennels(_manager.User.ShelterId.Value);
                cmbAggressive.ItemsSource = _yesNo;
                cmbChildFriendly.ItemsSource = _yesNo;
                cmbNeuterStatus.ItemsSource = _yesNo;
            }
            catch (Exception ex)
            {
                PromptWindow.ShowPrompt("Error", "An error occured populating combo boxes\n" + ex, ButtonMode.Ok);
                NavigationService.Navigate(WpfPresentation.Animals.AnimalListPage.GetAnimalListPage(MasterManager.GetMasterManager()));
            }
        }

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/02/27
        /// 
        /// Click event method for the "Save" button that performs validation
        /// and if it passes inserts an animal record into the database.
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (btnSave.Content.ToString() == "Edit")
            {
                NavigationService nav = NavigationService.GetNavigationService(this);
                nav.Navigate(new WpfPresentation.Animals.EditDetailAnimalProfile(_manager, _newAnimal));
            }
            else
            {

                _newAnimal = new AnimalVM();

                if (txtAnimalName.Text == "" || cmbAnimalTypeId.SelectedItem == null || cmbAnimalBreedId.SelectedItem == null ||
                    cmbAnimalGender.SelectedItem == null || cmbAnimalStatusId.SelectedItem == null || cmbAggressive.SelectedItem == null ||
                    cmbChildFriendly.SelectedItem == null || cmbNeuterStatus.SelectedItem == null)
                {
                    PromptWindow.ShowPrompt("Error", "Please enter all fields.", ButtonMode.Ok);
                }
                else
                {

                    bool goodData = true;

                    // Validate animal name
                    if (goodData)
                    {
                        if (txtAnimalName.Text.IsValidFirstName())
                        {
                            goodData = true;
                            _newAnimal.AnimalName = txtAnimalName.Text;
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
                        _newAnimal.AnimalShelterId = _manager.User.ShelterId.Value;
                        _newAnimal.AnimalTypeId = cmbAnimalTypeId.SelectedItem.ToString();
                        _newAnimal.AnimalBreedId = cmbAnimalBreedId.SelectedItem.ToString();
                        _newAnimal.AnimalGender = cmbAnimalGender.SelectedItem.ToString();
                        _newAnimal.AnimalStatusId = cmbAnimalStatusId.SelectedItem.ToString();
                        _newAnimal.Description = txtDescription.Text;
                        _newAnimal.Personality = txtPersonality.Text;
                        _newAnimal.MicrochipNumber = txtMicrochipNumber.Text;
                        _newAnimal.BroughtIn = DateTime.Today;

                        if (!(cmbKennelName.SelectedItem == null || cmbKennelName.SelectedItem.ToString() == ""))
                        {
                            _newAnimal.KennelName = ((Kennel)cmbKennelName.SelectedItem).KennelName;
                        }

                        if (cmbAggressive.SelectedItem.ToString() == "Yes")
                        {
                            _newAnimal.Aggressive = true;
                            _newAnimal.AggressiveDescription = txtAggressiveDescription.Text;
                        }
                        else
                        {
                            _newAnimal.Aggressive = false;
                            _newAnimal.AggressiveDescription = "";
                        }

                        if (cmbChildFriendly.SelectedItem.ToString() == "Yes")
                        {
                            _newAnimal.ChildFriendly = true;
                        }
                        else
                        {
                            _newAnimal.ChildFriendly = false;
                        }
                        if (cmbNeuterStatus.SelectedItem.ToString() == "Yes")
                        {
                            _newAnimal.NeuterStatus = true;
                        }
                        else
                        {
                            _newAnimal.NeuterStatus = false;
                        }
                        _newAnimal.Notes = txtNotes.Text;

                        try
                        {
                            _manager.AnimalManager.AddAnimal(_newAnimal);
                            AddAnimalToKennel();
                            SetEditMode();
                        }
                        catch (Exception ex)
                        {
                            PromptWindow.ShowPrompt("Error", "Saving new record failed.\n" + ex, ButtonMode.Ok);

                        }
                    }
                }
            }
        }

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/03/07
        /// 
        /// Helper method for checking if a kennel name has been selected
        /// and then trying to assign the animal to that kennel.
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <exception cref="Exception">Insert animal into kennel fails</exception>
        private void AddAnimalToKennel()
        {
            if (!(cmbKennelName.SelectedItem == null || cmbKennelName.SelectedItem.ToString() == ""))
            {
                var kennelId = ((Kennel)cmbKennelName.SelectedItem).KennelId;
                try
                {
                    _manager.KennelManager.AddAnimalIntoKennelByAnimalId(kennelId, _newAnimal.AnimalId);
                    PromptWindow.ShowPrompt("Success", "Animal record has been created\n" +
                                            "Animal added to " + _newAnimal.KennelName, ButtonMode.Ok);
                }
                catch (Exception ex)
                {
                    PromptWindow.ShowPrompt("Error", "An error occured assigning animal to kennel\n" + ex, ButtonMode.Ok);
                }
            }
            else
            {
                PromptWindow.ShowPrompt("Success", "Animal record has been created", ButtonMode.Ok);
            }
        }

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/02/24
        /// 
        /// Click event method for the "Cancel" button. A popup is shown to confirm if
        /// the user actually intends to stop creating an animal record without saving. 
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            var result = PromptWindow.ShowPrompt("Discard Changes", "Are you sure you want to cancel?\n" +
                                                    "Animal record will not be saved.", ButtonMode.YesNo);
            if (result == PromptSelection.Yes)
            {
                NavigationService.Navigate(new AnimalListPage(_manager));
            }
        }

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/02/22
        /// 
        /// Helper method that links the breeds and types combo
        /// boxes so that when an animal type is selected only
        /// breeds of that type are available in the breeds box
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbAnimalType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cmbAnimalBreedId.ItemsSource = _breeds[cmbAnimalTypeId.SelectedItem.ToString()];
            cmbKennelName.ItemsSource = from kennel in _kennels
                                        where kennel.AnimalTypeId == cmbAnimalTypeId.SelectedItem.ToString()
                                        select kennel;
            cmbKennelName.DisplayMemberPath = "KennelName";
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
                txtAggressiveDescription.Text = "";
            }
        }

        // Methods for adding and displaying animal images
        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/02/28
        /// 
        /// Helper method for setting the page to "Edit" mode - this is the mode after the "Save"
        /// button has been clicked to save the initial record and return an animal Id and the user
        /// is ready to add images. We have to get an Id for the new animal before we can save images
        /// to the record.
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        private void SetEditMode()
        {
            btnSave.Content = "Edit";
            txtAnimalName.IsEnabled = false;
            cmbAnimalTypeId.IsEnabled = false;
            cmbAnimalBreedId.IsEnabled = false;
            cmbAnimalGender.IsEnabled = false;
            cmbNeuterStatus.IsEnabled = false;
            cmbAnimalStatusId.IsEnabled = false;
            cmbKennelName.IsEnabled = false;
            txtMicrochipNumber.IsEnabled = false;
            txtDescription.IsEnabled = false;
            txtPersonality.IsEnabled = false;
            cmbAggressive.IsEnabled = false;
            txtAggressiveDescription.IsEnabled = false;
            cmbChildFriendly.IsEnabled = false;
            txtNotes.IsEnabled = false;
        }

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/02/28
        /// 
        /// Click event method for the "Add Images" button. Checks to see if page has been saved
        /// (to return an animal Id) and then creates an instance of UploadAdditionalImageWindow
        /// where the user can add images.
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddImages_Click(object sender, RoutedEventArgs e)
        {
            if (btnSave.Content.ToString() == "Save")
            {
                PromptWindow.ShowPrompt("Error", "Please save animal record\nbefore adding photos.", ButtonMode.Ok);
            }
            else
            {
                var uploadAdditionalFileWindow = new WpfPresentation.Animals.UploadAdditionalImageWindow(_newAnimal, _manager);
                uploadAdditionalFileWindow.Owner = Window.GetWindow(this);
                uploadAdditionalFileWindow.ShowDialog();
                PopulateImages();
            }
        }

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/02/27
        /// 
        /// Helper method to display images that have been added to the animal
        /// profile record using UploadAdditionalImageWindow.
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <exception cref="Exception">Fails to retrieve animal images</exception>
        private void PopulateImages()
        {
            if (_imagesList == null || _imagesList.Count == 0)
            {
                try
                {
                    _imagesList = _manager.ImagesManager.RetrieveAnimalImagesByAnimalId(_newAnimal.AnimalId);

                    foreach (var image in _imagesList)
                    {
                        Image viewableImage = new Image();
                        viewableImage.Margin = new Thickness(10, 0, 10, 0);
                        viewableImage.Stretch = Stretch.Uniform;
                        viewableImage.StretchDirection = StretchDirection.Both;
                        viewableImage.Source = _manager.ImagesManager.RetrieveImageByImages(image);
                        wpAnimalImages.Children.Add(viewableImage);
                    }
                }
                catch (Exception ex)
                {
                    PromptWindow.ShowPrompt("Error", ex.Message + "\n\n" + ex.Message);
                }
            }
        }
    }
}
