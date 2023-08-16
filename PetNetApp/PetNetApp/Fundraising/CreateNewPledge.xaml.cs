using System;
using System.Collections.Generic;
using System.Globalization;
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
using DataObjects;
using LogicLayer;
using WpfPresentation.Fundraising;

namespace WpfPresentation.Fundraising
{
    /// <summary>
    /// Interaction logic for CreateNewPledge.xaml
    /// </summary>
    public partial class CreateNewPledge : Page
    {
        private int _fundEventId;
        private PledgeVM _pledgeVM = new PledgeVM();
        private UsersVM _usersVM = new UsersVM();
        MasterManager _masterManager = null;
        private static Regex _isNameValid = new Regex(@"^[A-Za-z]+$");
        private static Regex _isAmountValid = new Regex(@"^[0-9]+(\.[0-9]{0,2})?$");
        private static Regex _isValidPhone = new Regex(@"^[0-9]+$");
        public CreateNewPledge(int fundEventId, MasterManager masterManager)
        {
            InitializeComponent();
            _fundEventId = fundEventId;
            _masterManager = masterManager;
        }

        /// <summary>
        /// William Rients
        /// Created: 2023/04/07
        /// 
        /// When a key is pressed for the txtFirstName,
        /// input is check if it's a valid character in 
        /// a first name and max length
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        private void txtFirstName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            string firstName = txtFirstName.Text.Insert(txtFirstName.CaretIndex, e.Text);
            if (!_isNameValid.IsMatch(firstName))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// William Rients
        /// Created: 2023/04/07
        /// 
        /// When a key is pressed for the txtLastName,
        /// input is check if it's a valid character in 
        /// a last name and max length
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        private void txtLastName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            string lastName = txtLastName.Text.Insert(txtLastName.CaretIndex, e.Text); 
            if (!_isNameValid.IsMatch(lastName))
            {
                e.Handled = true; 
            }
        }

        /// <summary>
        /// William Rients
        /// Created: 2023/04/07
        /// 
        /// When a key is pressed for the txtAmountPledge,
        /// it checks if input is only numeric an has two
        /// decimal places and max length
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        private void txtAmountPledge_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            string amount = txtAmountPledge.Text.Insert(txtAmountPledge.CaretIndex, e.Text); 
            if (!_isAmountValid.IsMatch(amount))
            {
                e.Handled = true; 
            }
        }

        /// <summary>
        /// William Rients
        /// Created: 2023/04/07
        /// 
        /// When a key is pressed for the txtPhoneNumber,
        /// it checks that it is numeric and max length
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        private void txtPhoneNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            string phoneNum = txtPhoneNumber.Text.Insert(txtPhoneNumber.CaretIndex, e.Text);
            if (!_isValidPhone.IsMatch(phoneNum) || phoneNum.Length > 13)
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// William Rients
        /// Created: 2023/04/07
        /// 
        /// Checks that all text boxes and at least one
        /// radio button has content
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        private bool validateInput()
        {
            bool result = true;
            if (txtAmountPledge.Text == "" || txtAmountPledge.Text == null)
            {
                PromptWindow.ShowPrompt("Error", "Pledge Amount is required.");
                result = false;
            }
            else if (txtTargetPledge.Text == "" || txtTargetPledge.Text == null)
            {
                PromptWindow.ShowPrompt("Error", "Pledge Target is required.");
                result = false;
            }
            else if (txtRequirementPledge.Text == "" || txtRequirementPledge.Text == null)
            {
                PromptWindow.ShowPrompt("Error", "Pledge Requirement is required.");
                result = false;
            }
            else if (txtMessagePledge.Text == "" || txtMessagePledge.Text == null)
            {
                PromptWindow.ShowPrompt("Error", "Pledge Message is required.");
                result = false;
            }
            else if (txtEmail.Text == "" || txtEmail.Text == null)
            {
                PromptWindow.ShowPrompt("Error", "Pledge Email is required.");
                result = false;
            }
            else if (txtPhoneNumber.Text == "" || txtPhoneNumber.Text == null)
            {
                PromptWindow.ShowPrompt("Error", "Pledge Phone Number is required.");
                result = false;
            }
            else if (txtFirstName.Text == "" || txtFirstName.Text == null)
            {
                PromptWindow.ShowPrompt("Error", "Pledge First Name is required.");
                result = false;
            }
            else if (txtLastName.Text == "" || txtLastName.Text == null)
            {
                PromptWindow.ShowPrompt("Error", "Pledge Last Name is required.");
                result = false;
            }                      
            else if (rdbPhone.IsChecked == false && rdbEmail.IsChecked == false)
            {
                PromptWindow.ShowPrompt("Error", "Pledge Contact Preference is required.");
                result = false;
            }
            return result;
        }

        /// <summary>
        /// William Rients
        /// Created: 2023/04/07
        /// 
        /// When clicked, the validate input method is called and 
        /// all data is checked, if validate input is true, the pledgeVM
        /// properties are set to all the textboxes content and the user
        /// is added to the database
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (validateInput())
            {
                _pledgeVM.FundraisingEventId = _fundEventId;
                _pledgeVM.Amount = decimal.Parse(txtAmountPledge.Text);
                _pledgeVM.Target = txtTargetPledge.Text;
                _pledgeVM.Requirement = txtRequirementPledge.Text;
                _pledgeVM.Message = txtMessagePledge.Text;
                _pledgeVM.GivenName = txtFirstName.Text;
                _pledgeVM.FamilyName = txtLastName.Text;
                _pledgeVM.Phone = txtPhoneNumber.Text;
                _pledgeVM.Email = txtEmail.Text;
                if (rdbEmail.IsChecked == true)
                {
                    _pledgeVM.IsContactPreferencePhone = false;
                }
                else if (rdbPhone.IsChecked == true)
                {

                    _pledgeVM.IsContactPreferencePhone = true;
                }
                try
                {
                    if (_masterManager.PledgeManager.CreatePledge(_pledgeVM))
                    {
                        PromptWindow.ShowPrompt("Success", "Pledge Created!");
                        NavigationService.Navigate(new ViewFundraisingEventsPage());
                    }
                    
                }
                catch (Exception ex)
                {
                    PromptWindow.ShowPrompt("Error", ex.Message);
                }
            }
            
        }

        /// <summary>
        /// William Rients
        /// Created: 2023/04/07
        /// 
        /// When clicked, the user is navigated back
        /// to the previous page of fundraising events
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            if (PromptWindow.ShowPrompt("Warning", "You will lose all changes.", ButtonMode.YesNo) == PromptSelection.Yes)
            {
                NavigationService.GoBack();
            }                               
        }

        /// <summary>
        /// William Rients
        /// Created: 2023/04/07
        /// 
        /// When a the email textbox loses focus, 
        /// the email is check to see if the email is in the 
        /// database, if so the textboxes are filled with users
        /// information and disabled, if no email in the database,
        /// the user is free to enter their information since a userId
        /// can be null in the database pledge table
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        private void txtEmail_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                _usersVM = _masterManager.UsersManager.RetrieveUserByUserEmail(txtEmail.Text);
                if (_usersVM != null)
                {
                    txtFirstName.Text = _usersVM.GivenName;
                    txtLastName.Text = _usersVM.FamilyName;
                    txtPhoneNumber.Text = _usersVM.Phone;
                    txtFirstName.IsEnabled = false;
                    txtLastName.IsEnabled = false;
                    txtPhoneNumber.IsEnabled = false;
                    _pledgeVM.UserId = _usersVM.UsersId;
                }
            }
            catch (Exception ex)
            {
                PromptWindow.ShowPrompt("Error", ex.Message);
            }
        }
    }
}
