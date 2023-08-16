/// <summary>
/// Mads Rhea
/// Created: 2023/02/05
/// 
/// </summary>
/// <remarks>
/// Oleksiy Fedchuk
/// Updated: 2023/04/28
/// 
/// Final QA
/// </remarks>
using DataObjects;
using LogicLayer;
using PetNetApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
using WpfPresentation.Misc;

namespace WpfPresentation.Misc
{
    /// <summary>
    /// Created By: Alex Oetken [2023/02/15]
    /// 
    /// Last Updated By: Mads Rhea [2023/02/19]
    /// 
    /// Notes: -
    /// </summary>
    /// <remarks>
    /// Oleksiy Fedchuk
    /// Updated: 2023/04/28
    /// 
    /// Final QA
    /// </remarks>
    public partial class SignUp : Page
    {
        private static SignUp _existingSignUp = null;
        private MasterManager _manager = MasterManager.GetMasterManager();
        private Users _user = null;
        private const string COMBOBOX_PLACEHOLDER_TEXT = "---";

        /// <summary>
        /// Mads Rhea
        /// Created: 2023/02/05
        /// 
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/28
        /// 
        /// Final QA
        /// </remarks>
        public SignUp()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Mads Rhea
        /// Created: 2023/02/05
        /// 
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/28
        /// 
        /// Final QA
        /// </remarks>
        /// <returns></returns>
        public static SignUp GetSignUpPage()
        {
            if (_existingSignUp == null)
            {
                _existingSignUp = new SignUp();
            }

            return _existingSignUp;
        }
        /// <summary>
        /// Mads Rhea
        /// Created: 2023/02/05
        /// 
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/28
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSignUp_Click(object sender, RoutedEventArgs e)
        {
            string email = txtEmail.Text;
            string givenName = txtGivenName.Text;
            string familyName = txtFamilyName.Text;
            string phoneNumber = txtPhone.Text;
            string zipCode = txtZipCode.Text;
            string password = txtPassword.Password;
            string confirmPassword = txtPasswordConfirm.Password;
            string genderId = genderSelection.Text;
            string pronounId = pronounSelection.Text;

            if (!email.IsValidEmail())
            {
                PromptWindow.ShowPrompt("Invalid Email","Please enter your email.");
                txtEmail.Focus();
                txtEmail.SelectAll();
                return;
            }

            if (!givenName.IsValidFirstName())
                {
                PromptWindow.ShowPrompt("Invalid Given Name","Please enter a valid first Name.");
                txtGivenName.Focus();
                txtGivenName.SelectAll();
                return; 
            }

            if (!familyName.IsValidLastName())
            {
                PromptWindow.ShowPrompt("Invalid Family Name","Please enter a valid last name.");
                txtFamilyName.Focus();
                txtFamilyName.SelectAll();
                return;
            }

            if (!phoneNumber.IsValidPhone())
            {
                PromptWindow.ShowPrompt("Invalid Phone Number","Please enter a valid phone number with area code.");
                txtPhone.Focus();
                txtPhone.SelectAll();
                return; 
            }

            if (!zipCode.IsValidZipcode())
            {
                PromptWindow.ShowPrompt("Invalid Zipcode","Please enter a valid zip code.");
                txtZipCode.Focus();
                txtZipCode.SelectAll();
                return; 
            }

            if (password == "" || password.Length < 8)
            {
                PromptWindow.ShowPrompt("Invalid Password","Please enter a password with at least 8 characters.");
                txtPassword.Focus();
                txtPassword.SelectAll();
                return; 
            }

            if (password != confirmPassword)
            {
                PromptWindow.ShowPrompt("Invalid Password","Please match passwords.");
                txtPassword.Clear();
                txtPasswordConfirm.Clear();
                txtPassword.Focus(); 
                return; 
            }

            if (genderId == COMBOBOX_PLACEHOLDER_TEXT)
            {
                PromptWindow.ShowPrompt("Invalid Gender","Please enter your gender");
                genderSelection.Focus();
                return; 
            }

            if (pronounId == COMBOBOX_PLACEHOLDER_TEXT)
            {
                PromptWindow.ShowPrompt("Invalid Pronouns","Please enter your preferred pronouns.");
                pronounSelection.Focus();
                return; 
            }


            _user = new Users()
            {
                Email = email,
                GivenName = givenName,
                FamilyName = familyName,
                Phone = phoneNumber,
                Zipcode = zipCode,
                GenderId = genderId,
                PronounId = pronounId
            };

            try
            {
                if(_manager.UsersManager.AddUser(_user, password))
                {
                    PromptWindow.ShowPrompt("Success","Account has been created! Please log in using your new credentials.");
                    NavigationService.Navigate(LogInPage.GetLogInPage());
                   
                }
            } 
            catch (Exception)
            {
                PromptWindow.ShowPrompt("Erro","Uh oh! Something bad happened. Let's try that again.");
                txtPassword.Clear();
                txtPasswordConfirm.Clear();
                txtEmail.Clear();
                txtFamilyName.Clear();
                txtGivenName.Clear();
                txtZipCode.Clear();
                txtPhone.Clear(); 

            }


        }
        /// <summary>
        /// Mads Rhea
        /// Created: 2023/02/05
        /// 
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/28
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSignUpCancel_Click(object sender, RoutedEventArgs e)
        {
            var result = PromptWindow.ShowPrompt("Really Cancel?", "Are you sure? You will lose your progress.",
                ButtonMode.YesNo);

            if (result == PromptSelection.Yes)
            {
                txtPassword.Clear();
                txtPasswordConfirm.Clear();
                txtEmail.Clear();
                txtFamilyName.Clear();
                txtGivenName.Clear();
                txtZipCode.Clear();
                txtPhone.Clear();
                NavigationService.Navigate(LogInPage.GetLogInPage());

            }
            else
            {
                
            }
        }
        /// <summary>
        /// Mads Rhea
        /// Created: 2023/02/05
        /// 
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/28
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // 4 lines added by Stephen Jaurigue
            genderSelection.Items.Clear();
            pronounSelection.Items.Clear();
            genderSelection.Items.Add(new ComboBoxItem() { Content = COMBOBOX_PLACEHOLDER_TEXT, IsEnabled = false });
            pronounSelection.Items.Add(new ComboBoxItem() { Content = COMBOBOX_PLACEHOLDER_TEXT, IsEnabled = false });

            List<string> genders = new List<string>();
            List<string> pronouns = new List<string>();
            try
            {
                genders = _manager.UsersManager.RetrieveGenders();
                pronouns = _manager.UsersManager.RetrievePronouns();
            }
            catch (Exception ex)
            {

                throw ex;
            }

            genderSelection.SelectedIndex = 0;
            pronounSelection.SelectedIndex = 0;

            foreach (var gender in genders)
            {
                genderSelection.Items.Add(gender);
            }

            foreach(var pronoun in pronouns)
            {
                pronounSelection.Items.Add(pronoun);
            }
        }
    }
}
