using DataObjects;
using LogicLayer;
using PetNetApp;
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

namespace WpfPresentation.Misc
{
    /// <summary>
    /// Mads Rhea
    /// Created: 2023/02/05
    /// 
    /// WPF for Account Info Page tab within "Account Settings"
    /// </summary>
    ///
    /// <remarks>
    /// Oleksiy Fedchuk
    /// Updated: 2023/04/28
    /// 
    /// Final QA
    /// </remarks>
    public partial class AccountInfoPage : Page
    {
        private static AccountInfoPage _existingAccountInfo = null;
        private MasterManager _manager = MasterManager.GetMasterManager();
        /// <summary>
        /// Mads Rhea
        /// Created: 2023/02/05
        /// 
        /// </summary>
        ///
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/28
        /// 
        /// Final QA
        /// </remarks>
        public AccountInfoPage()
        {
            InitializeComponent();
            RefreshOnAccountUpdate();

            lblCurrentEmail.Content = "Current Email: " + _manager.User.Email;
        }
        /// <summary>
        /// Mads Rhea
        /// Created: 2023/02/05
        /// 
        /// </summary>
        ///
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/28
        /// 
        /// Final QA
        /// </remarks>
        /// <returns></returns>
        public static AccountInfoPage GetAccountInfoPage()
        {
            if (_existingAccountInfo == null)
            {
                _existingAccountInfo = new AccountInfoPage();
            }

            return _existingAccountInfo;
        }
        /// <summary>
        /// Mads Rhea
        /// Created: 2023/02/05
        /// 
        /// </summary>
        ///
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/28
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPasswordSave_Click(object sender, RoutedEventArgs e)
        {
            string oldPassword = txtOldPassword.Password;
            string newPassword = txtNewPassword.Password;
            string confirmPassword = txtConfirmPassword.Password;

            if (oldPassword == "")
            {
                PromptWindow.ShowPrompt("Error", "You must confirm your old password.");
                txtOldPassword.Focus();
                return;
            }

            if (newPassword == "")
            {
                PromptWindow.ShowPrompt("Error", "You must enter a new password.");
                txtNewPassword.Focus();

                return;
            }

            if (newPassword == oldPassword)
            {
                PromptWindow.ShowPrompt("Password Error", "This matches a password previously used.\n\nPlease choose a different password.");
                txtNewPassword.Clear();
                txtConfirmPassword.Clear();
                txtNewPassword.Focus();

                return;
            }

            if (confirmPassword == "")
            {
                PromptWindow.ShowPrompt("Password Error", "You did not confirm your new password.\n\nPlease confirm before continuing.");
                txtConfirmPassword.Focus();

                return;
            }

            if (newPassword != confirmPassword)
            {
                PromptWindow.ShowPrompt("Password Error", "Passwords do not match.\n\nPlease try again.");
                txtNewPassword.Clear();
                txtConfirmPassword.Clear();
                txtNewPassword.Focus();

                return;
            }

            try
            {
                PromptWindow.ShowPrompt("Success", "Password was reset!");
                RefreshOnAccountUpdate();
            }
            catch (Exception up)
            {
                PromptWindow.ShowPrompt("Error", up.Message);
            }

            Keyboard.ClearFocus();

        }
        /// <summary>
        /// Mads Rhea
        /// Created: 2023/02/05
        /// 
        /// </summary>
        ///
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/28
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEmailSave_Click(object sender, RoutedEventArgs e)
        {
            string email = txtNewEmail.Text;
            string password = passConfirmEmail.Password;

            if (!email.IsValidEmail())
            {
                PromptWindow.ShowPrompt("EmailError", "Please enter a valid Email.");
                txtNewEmail.SelectAll();
                txtNewEmail.Focus();
                return;
            }

            if (email == _manager.User.Email)
            {
                PromptWindow.ShowPrompt("Email Error", "Email is the same as your current email.\n\nPlease enter a different email if you wish to change it.");
                txtNewEmail.SelectAll();
                txtNewEmail.Focus();
                return;
            }

            if (password == "" || password == null)
            {
                PromptWindow.ShowPrompt("Password Error", "Password cannot be left empty.\n\nPlease enter your password.");
                passConfirmEmail.Focus();
                return;
            }

            try
            {
                if (_manager.UsersManager.UpdateEmail(_manager.User.Email, email, password))
                {
                    PromptWindow.ShowPrompt("Success", "Email was updated!");
                    lblCurrentEmail.Content = "Current Email: " + email;
                    RefreshOnAccountUpdate();
                }
                else
                {
                    PromptWindow.ShowPrompt("Password Error", "Password was incorrect.\n\nPlease retype your password and try again.");
                    passConfirmEmail.Clear();
                    passConfirmEmail.Focus();
                }
            }
            catch (Exception ex)
            {
                PromptWindow.ShowPrompt("Error", ex.Message);
            }

            Keyboard.ClearFocus();

        }
        /// <summary>
        /// Mads Rhea
        /// Created: 2023/02/05
        /// 
        /// </summary>
        ///
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/28
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtOldPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                txtNewPassword.Focus();
            }
        }
        /// <summary>
        /// Mads Rhea
        /// Created: 2023/02/05
        /// 
        /// </summary>
        ///
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/28
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtNewPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                txtConfirmPassword.Focus();
            }
        }
        /// <summary>
        /// Mads Rhea
        /// Created: 2023/02/05
        /// 
        /// </summary>
        ///
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/28
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtConfirmPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                btnPasswordSave_Click(sender, e);
            }
        }
        /// <summary>
        /// Mads Rhea
        /// Created: 2023/02/05
        /// 
        /// </summary>
        ///
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/28
        /// 
        /// Final QA
        /// </remarks>
        private void RefreshOnAccountUpdate()
        {
            txtNewEmail.Clear();
            passConfirmEmail.Clear();

            txtOldPassword.Clear();
            txtNewPassword.Clear();
            txtConfirmPassword.Clear();
        }
        /// <summary>
        /// Mads Rhea
        /// Created: 2023/02/05
        /// 
        /// </summary>
        ///
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/28
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtNewEmail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                passConfirmEmail.Focus();
            }
        }
        /// <summary>
        /// Mads Rhea
        /// Created: 2023/02/05
        /// 
        /// </summary>
        ///
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/28
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void passConfirmEmail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                btnEmailSave_Click(sender, e);
            }
        }
    }
}
