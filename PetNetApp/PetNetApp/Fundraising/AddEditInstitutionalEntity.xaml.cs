using DataAccessLayerFakes;
using DataObjects;
using LogicLayer;
using System;
using System.Collections.Generic;
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
using WpfPresentation.UserControls;

namespace WpfPresentation.Fundraising
{
    /// <summary>
    /// Interaction logic for AlterContact.xaml
    /// </summary>
    public partial class AddEditInstitutionalEntity : Window
    {
        private MasterManager _masterManager = MasterManager.GetMasterManager();
        private WindowMode2 _windowMode;
        private string _contactType;
        private SponsorEvent _sponsorEvent = new SponsorEvent();
        private InstitutionalEntity _institutionalEntity;


        /// <summary>
        /// Default constructor for AddEditInstitutionalEnity
        /// </summary>
        /// <param name="entityType">sets the type of institutional being added</param>
        public AddEditInstitutionalEntity(string entityType)
        {
            _windowMode = WindowMode2.Add;
            _contactType = entityType;
            InitializeComponent();
            SetupAddInstitutionalEntity();
        }

        /// <summary>
        /// used to for edit and view entity
        /// </summary>
        /// <param name="institutionalEntity"></param>
        /// <param name="windowMode">sets the window mode to edit or view</param>
        /// <param name="entityType">brings in the type </param>
        public AddEditInstitutionalEntity(InstitutionalEntity institutionalEntity, string windowMode, string entityType)
        {
            _windowMode = windowMode.ToLower() == "edit" ? WindowMode2.Edit : WindowMode2.View;
            _contactType = entityType;
            _institutionalEntity = institutionalEntity;
            InitializeComponent();
            if (_windowMode == WindowMode2.Edit)
            {
                SetupEditInstitutionalEntity();
            }
            else
            {
                SetupViewInstitutionalEntity();
            }
        }
        /// <summary>
        /// Barry Mikulas
        /// Created: 2023/03/09
        /// 
        /// Sets up window objects for editing.
        /// 
        /// </summary>
        private void SetupEditInstitutionalEntity()
        {
            lblWindowTitle.Content = _windowMode + " " + _contactType.Substring(0, 1).ToUpper() + _contactType.Substring(1);
            AddEditMode();
            stackEditClose.IsEnabled = false;
            stackEditClose.Visibility = Visibility.Collapsed;
            stackSaveCancel.IsEnabled = true;
            stackSaveCancel.Visibility = Visibility.Visible;
        }
        /// <summary>
        /// Barry Mikulas
        /// Created: 2023/03/09
        /// 
        /// Sets up window objects for viewing.
        /// 
        /// </summary>
        private void SetupViewInstitutionalEntity()
        {
            lblWindowTitle.Content = _windowMode + " " + _contactType.Substring(0, 1).ToUpper() + _contactType.Substring(1);
            ViewMode();
            stackEditClose.IsEnabled = true;
            stackEditClose.Visibility = Visibility.Visible;
            stackSaveCancel.IsEnabled = false;
            stackSaveCancel.Visibility = Visibility.Collapsed;
        }
        /// <summary>
        /// Barry Mikulas
        /// Created: 2023/03/09
        /// 
        /// Sets up window objects for adding.
        /// 
        /// </summary>
        private void SetupAddInstitutionalEntity()
        {
            lblWindowTitle.Content = _windowMode + " " + _contactType.Substring(0, 1).ToUpper() + _contactType.Substring(1);
            AddEditMode();
            stackEditClose.IsEnabled = false;
            stackEditClose.Visibility = Visibility.Collapsed;
            stackSaveCancel.IsEnabled = true;
            stackSaveCancel.Visibility = Visibility.Visible;
        }
        /// <summary>
        /// Barry Mikulas
        /// Created: 2023/03/09
        /// 
        /// Sets up window objects for viewing.
        /// 
        /// </summary>
        private void ViewMode()
        {
            tbCompanyName.IsReadOnly = true;
            tbGivenName.IsReadOnly = true;
            tbFamilyName.IsReadOnly = true;
            tbEmail.IsReadOnly = true;
            tbPhone.IsReadOnly = true;
            tbAddress.IsReadOnly = true;
            tbAddress2.IsReadOnly = true;
            tbZipcode.IsReadOnly = true;
            tbCity.IsReadOnly = true;
            tbState.IsReadOnly = true;
            btnCancel.IsCancel = false;
            btnClose.IsCancel = true;
            btnSave.IsDefault = false;
            btnEdit.IsDefault = true;
            if (_contactType != "Sponsor")
            {
                btnVeiwEvents.Visibility = Visibility.Collapsed;
                btnVeiwEvents.IsEnabled = false;
            }
            else
            {
                btnVeiwEvents.Visibility = Visibility.Visible;
                btnVeiwEvents.IsEnabled = true;
            }
        }
        /// <summary>
        /// Barry Mikulas
        /// Created: 2023/03/09
        /// 
        /// Sets up window objects for adding and editing.
        /// 
        /// </summary>
        private void AddEditMode()
        {
            tbCompanyName.IsReadOnly = false;
            tbGivenName.IsReadOnly = false;
            tbFamilyName.IsReadOnly = false;
            tbEmail.IsReadOnly = false;
            tbPhone.IsReadOnly = false;
            tbAddress.IsReadOnly = false;
            tbAddress2.IsReadOnly = false;
            tbZipcode.IsReadOnly = false;
            tbCity.IsReadOnly = true;
            tbState.IsReadOnly = true;
            btnSave.IsDefault = true;
            btnEdit.IsDefault = false;
        }


        /// <summary>
        /// Barry Mikulas
        /// Created: 2023/03/09
        /// 
        /// When the page loads and the user arrived from a view or add option 
        /// populate the text boxes
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_windowMode == WindowMode2.View || _windowMode == WindowMode2.Edit)
                {
                    tbCompanyName.Text = _institutionalEntity.CompanyName;
                    tbGivenName.Text = _institutionalEntity.GivenName;
                    tbFamilyName.Text = _institutionalEntity.FamilyName;
                    tbEmail.Text = _institutionalEntity.Email;
                    tbPhone.Text = new string(_institutionalEntity.Phone.Where(c => char.IsDigit(c)).ToArray());
                    tbAddress.Text = _institutionalEntity.Address;
                    tbAddress2.Text = _institutionalEntity.Address2;
                    // check to see if zipcode is 5 digits
                    if (_institutionalEntity.Zipcode.IsValidZipcode())
                    {
                        tbZipcode.Text = _institutionalEntity.Zipcode.Substring(0, 5);
                        LoadCityStateByZipCode();
                    }
                }
            }
            catch (Exception ex)
            {
                PromptWindow.ShowPrompt("Error", ex.Message + "\n\n" + ex.InnerException.Message, ButtonMode.Ok);
            }

        }

        /// <summary>
        /// Barry Mikulas
        /// Created: 2023/03/09
        /// 
        /// When a user types in a textbox this will prevent them from entering anything but a digit
        /// this is based on https://stackoverflow.com/a/12721673
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        /// <summary>
        /// Barry Mikulas
        /// Created: 2023/03/09
        /// 
        /// Event handler for Cancel button click
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            switch (_windowMode)
            {
                case WindowMode2.Add:
                    //TODO: confirm the user wants to cancel input
                    if (PromptWindow.ShowPrompt("Cancel", "Are you sure you want to cancel adding?", ButtonMode.YesNo) == PromptSelection.Yes)
                    {
                        this.Close();
                    }
                    break;
                case WindowMode2.View:
                   // this.Close();
                    break;
                case WindowMode2.Edit:
                    //TODO: confirm user wants to cancel editing
                    if (PromptWindow.ShowPrompt("Cancel", "Are you sure you want to cancel editing?", ButtonMode.YesNo) == PromptSelection.Yes)
                    {
                        this.Close();
                    }
                    else
                    {
                        return;
                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/03/10
        /// 
        /// Click event method for the "Edit" button that perfomrs validation
        /// and if it passes updates an institutional entity record in the
        /// database.
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            _windowMode = WindowMode2.Edit;
            SetupEditInstitutionalEntity();
        }

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/03/10
        /// 
        /// Click event method for the "Save" button that performs validation
        /// and if it passes inserts an institutional entity record into the
        /// database.
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
            ValidateEntityOnSave();
        }

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/03/10
        /// 
        /// Helper method to validate data when "Save" button is clicked.
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        private void ValidateEntityOnSave()
        {
            if (tbGivenName.Text == "" || tbFamilyName.Text == "" || tbEmail.Text == "" || tbPhone.Text == ""
                || tbAddress.Text == "" || tbZipcode.Text == "")
            {
                PromptWindow.ShowPrompt("Error", "Please enter all fields.", ButtonMode.Ok);
            }

            else
            {
                InstitutionalEntity newInstitutionalEntity = new InstitutionalEntity();
                newInstitutionalEntity.ShelterId = _masterManager.User.ShelterId.Value;
                newInstitutionalEntity.ContactType = _contactType;

                bool goodData = true;

                if (tbCompanyName == null || tbCompanyName.Text == "")
                {
                    newInstitutionalEntity.CompanyName = "";
                }
                else
                {
                    if (!(tbGivenName.Text.Length > 100))
                    {
                        goodData = true;
                        newInstitutionalEntity.CompanyName = tbCompanyName.Text;
                    }
                    else
                    {
                        goodData = false;
                        PromptWindow.ShowPrompt("Format Error", "Company name cannot be longer than 100 characters.",
                                                ButtonMode.Ok);
                        tbCompanyName.Focus();
                        tbCompanyName.SelectAll();
                    }
                }

                // Validate given name
                if (goodData)
                {
                    if (tbGivenName.Text.IsValidFirstName())
                    {
                        goodData = true;
                        newInstitutionalEntity.GivenName = tbGivenName.Text;
                    }
                    else
                    {
                        goodData = false;
                        PromptWindow.ShowPrompt("Format Error", "Given name must be in proper format.",
                                                ButtonMode.Ok);
                        tbGivenName.Focus();
                        tbGivenName.SelectAll();
                    }

                    // Validate family name
                    if (goodData)
                    {
                        if (tbFamilyName.Text.IsValidLastName())
                        {
                            goodData = true;
                            newInstitutionalEntity.FamilyName = tbFamilyName.Text;
                        }
                        else
                        {
                            goodData = false;
                            PromptWindow.ShowPrompt("Format Error", "Family name must be in proper format.",
                                                    ButtonMode.Ok);
                            tbFamilyName.Focus();
                            tbFamilyName.SelectAll();
                        }
                    }

                    // Validate email
                    if (goodData)
                    {
                        if (tbEmail.Text.IsValidEmail())
                        {
                            goodData = true;
                            newInstitutionalEntity.Email = tbEmail.Text;
                        }
                        else
                        {
                            goodData = false;
                            PromptWindow.ShowPrompt("Format Error", "Email must be in proper format.",
                                                    ButtonMode.Ok);
                            tbEmail.Focus();
                            tbEmail.SelectAll();
                        }
                    }

                    // Validate phone
                    if (goodData)
                    {
                        if (tbPhone.Text.IsValidPhone())
                        {
                            goodData = true;
                            newInstitutionalEntity.Phone = tbPhone.Text;
                        }
                        else
                        {
                            goodData = false;
                            PromptWindow.ShowPrompt("Format Error", "Phone number must be in proper format.",
                                                    ButtonMode.Ok);
                            tbPhone.Focus();
                            tbPhone.SelectAll();
                        }
                    }

                    // Validate address 1
                    if (goodData)
                    {
                        if (tbAddress.Text.IsValidAddress())
                        {
                            goodData = true;
                            newInstitutionalEntity.Address = tbAddress.Text;
                        }
                        else
                        {
                            goodData = false;
                            PromptWindow.ShowPrompt("Format Error", "Address Line 1 must be in proper format.",
                                                    ButtonMode.Ok);
                            tbAddress.Focus();
                            tbAddress.SelectAll();
                        }
                    }

                    // Validate address 2
                    if (goodData)
                    {
                        if (tbAddress2 == null || tbAddress2.Text == "")
                        {
                            newInstitutionalEntity.Address2 = "";
                        }
                        else
                        {
                            if (tbAddress2.Text.IsValidAddress2())
                            {
                                goodData = true;
                                newInstitutionalEntity.Address2 = tbAddress2.Text;
                            }
                            else
                            {
                                goodData = false;
                                PromptWindow.ShowPrompt("Format Error", "Address Line 2 must be in proper format.",
                                                        ButtonMode.Ok);
                                tbAddress2.Focus();
                                tbAddress2.SelectAll();
                            }
                        }
                    }

                    // Validate ZIP code
                    if (goodData)
                    {
                        if (tbZipcode.Text.IsValidZipcode())
                        {
                            goodData = true;
                            newInstitutionalEntity.Zipcode = tbZipcode.Text;
                        }
                        else
                        {
                            goodData = false;
                            PromptWindow.ShowPrompt("Format Error", "Zipcode must be in proper format.",
                                                    ButtonMode.Ok);
                            tbZipcode.Focus();
                            tbZipcode.SelectAll();
                        }
                    }

                    // If validation has passed (goodData is still true) try to create the new institutional entity record
                    if (goodData)
                    {
                        switch (_windowMode)
                        {
                            case WindowMode2.Add:
                                SaveNewEntity(newInstitutionalEntity);
                                break;
                            case WindowMode2.Edit:
                                UpdateEntity(newInstitutionalEntity);
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/03/10
        /// 
        /// Method with try/catch to save a new institutional entity record. Prompt window inserts
        /// contact type into success prompt window.
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="newInstitutionalEntity">The Institutional Entity to be saved</param>
        private void SaveNewEntity(InstitutionalEntity newInstitutionalEntity)
        {
            try
            {
                _masterManager.InstitutionalEntityManager.AddInstitutionalEntity(newInstitutionalEntity);


                _institutionalEntity = newInstitutionalEntity;
                PromptWindow.ShowPrompt("Success", "New " + _institutionalEntity.ContactType + " record has been added", ButtonMode.Ok);
                _windowMode = WindowMode2.View;
                SetupViewInstitutionalEntity();
            }
            catch (Exception ex)
            {
                PromptWindow.ShowPrompt("Error", "Add record failed.\n" + ex, ButtonMode.Ok);
            }
        }

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/03/10
        /// 
        /// Method with try/catch to update an institutional entity record
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="newInstitutionalEntity">The updated Institutional Entity</param>
        private void UpdateEntity(InstitutionalEntity newInstitutionalEntity)
        {
            try
            {
                _masterManager.InstitutionalEntityManager.EditInstitutionalEntity(_institutionalEntity, newInstitutionalEntity);

                _institutionalEntity = newInstitutionalEntity;
                PromptWindow.ShowPrompt("Success", _institutionalEntity.ContactType + " record has been updated", ButtonMode.Ok);
                _windowMode = WindowMode2.View;
                SetupViewInstitutionalEntity();
            }
            catch (Exception ex)
            {
                PromptWindow.ShowPrompt("Error", "Update failed.\n" + ex, ButtonMode.Ok);
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            return;
        }

        /// <summary>
        /// Barry Mikulas
        /// Created: 2023/03/09
        /// 
        /// After the user moves from zipcode field tries to load city and state if is a valid zipcode
        /// </summary>
        private void tbZipcode_LostFocus(object sender, RoutedEventArgs e)
        {
            // if WindowMode.Add or WindowMode.Edit and IsValidZipcode - load city and state
            if ((_windowMode == WindowMode2.Add || _windowMode == WindowMode2.Edit) && tbZipcode.Text.IsValidZipcode())
            {
                LoadCityStateByZipCode();
            }
            else if ((_windowMode == WindowMode2.Add || _windowMode == WindowMode2.Edit) && tbZipcode.Text.Length < 5)
            {
                tbCity.Text = "";
                tbState.Text = "";
            }
        }
        /// <summary>
        /// Barry Mikulas
        /// Created: 2023/03/09
        /// 
        /// Loads the city and state for a zip code - if it fails displays a message.
        /// 
        /// </summary>
        private void LoadCityStateByZipCode()
        {
            Zipcode zipcodeData = new Zipcode();
            try
            {
                zipcodeData = _masterManager.ZipcodeManager.RetrieveCityStateLatLongByZipcode(tbZipcode.Text);
                tbCity.Text = zipcodeData.City;
                tbState.Text = zipcodeData.State;
            }
            catch (Exception)
            {
                PromptWindow.ShowPrompt("Zipcode Error", "Please enter a valid zipcode");
                tbCity.Text = "";
                tbState.Text = "";
                tbZipcode.Text = "";
                tbZipcode.Focus();
                tbZipcode.SelectAll();
            }
        }
        /// <summary>
        /// Barry Mikulas
        /// Created: 2023/03/09
        /// 
        /// Directs the view to the view page based on the contactype they are working with.
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closed(object sender, EventArgs e)
        {
            switch (_contactType)
            {
                case "Host":
                    ViewFundraisingEventHosts.GetViewFundraisingEventHosts();
                    break;
                case "Contact":
                    ViewFundraisingEventContacts.GetViewEventContacts();
                    break;
                case "Sponsor":
                    ViewFundraisingEventSponsors.GetViewEventSponsors();
                    break;
                default:
                    break;
            }
        }


        /// <summary>
        /// Ethan Kline
        /// Created: 2023/04/3
        /// 
        /// on click open the view event window and close this window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnVeiwEvents_Click(object sender, RoutedEventArgs e)
        {
            _sponsorEvent.CompanyName = _institutionalEntity.CompanyName;
            ViewEventsWin page = new ViewEventsWin(_sponsorEvent, _masterManager);
            page.Show();
            this.Close();
        }
    }


    enum WindowMode2
    {
        Add,
        Edit,
        View
    }
}
