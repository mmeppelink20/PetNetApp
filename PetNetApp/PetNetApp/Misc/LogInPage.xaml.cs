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
using LogicLayer;
using DataObjects;
using PetNetApp;
using WpfPresentation;

namespace WpfPresentation.Misc
{
    /// <summary>
    /// Mads Rhea
    /// Created: 2023/01/27
    /// 
    /// WPF for the Log-In page.
    /// </summary>
    ///
    /// <remarks>
    /// Oleksiy Fedchuk
    /// Updated: 2023/04/28
    /// 
    /// Final QA
    /// </remarks>
    public partial class LogInPage : Page
    {
        private static LogInPage _existingLogIn = null;
        private MasterManager _manager = MasterManager.GetMasterManager();
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
        public LogInPage()
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
        public static LogInPage GetLogInPage()
        {
            if (_existingLogIn == null)
            {
                _existingLogIn = new LogInPage();
            }

            return _existingLogIn;
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
        private void btnLogIn_Click(object sender, RoutedEventArgs e)
        {
            string email = txtEmail.Text;
            string password = txtPassword.Password;

            if (!email.IsValidEmail())
            {
                ErrorLoading(true);
                ChangeErrorText("Email is not valid.", "Please enter a valid email.");
                lblUserError.Visibility = Visibility.Visible;
                txtEmail.SelectAll();
                txtEmail.Focus();
                if (password == "" || password == null)
                {
                    ChangeErrorText("Email / Password is not valid.", "Please enter a valid email / password.");
                    lblPasswordError.Visibility = Visibility.Visible;
                }
                return;
            }
            else
            {
                ErrorLoading(false);
                lblUserError.Visibility = Visibility.Hidden;
            }

            if (password == "" || password == null)
            {
                ErrorLoading(true);
                ChangeErrorText("Password cannot be empty.", "Please enter a password");
                lblPasswordError.Visibility = Visibility.Visible;
                txtPassword.SelectAll();
                txtPassword.Focus();
                return;
            }
            else
            {
                ErrorLoading(false);
                lblPasswordError.Visibility = Visibility.Hidden;
            }

            try
            {
                _manager.User = _manager.UsersManager.LoginUser(email, password);
                PromptWindow.ShowPrompt("Hello","Welcome back, " + _manager.User.GivenName + "\n\nYou're signed in as " + RoleBuilder(_manager.User));
                
            }
            catch (Exception up)
            {
                ChangeErrorText(up.Message, up.InnerException.Message);
                ErrorLoading(true);
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
            txtEmail.Text = "";
            txtPassword.Password = "";
            rowError.Height = new GridLength(1);

            txtEmail.Focus();
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
        /// <param name="onOff"></param>
        private void ErrorLoading(bool onOff)
        {
            if (onOff)
            {
                // show elements
                lblErrorErr.Visibility = Visibility.Visible;
                lblErrorHelp.Visibility = Visibility.Visible;
                rowError.Height = new GridLength(50);
            }
            else
            {
                // hide elements
                lblErrorErr.Visibility = Visibility.Hidden;
                lblErrorHelp.Visibility = Visibility.Hidden;
                rowError.Height = new GridLength(15);
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
        /// <param name="error"></param>
        /// <param name="help"></param>
        private void ChangeErrorText(string error, string help)
        {
            lblErrorErr.Content = error;
            lblErrorHelp.Content = help;
        }
        /// <summary>
        /// Mads Rhea
        /// Created: 2023/02/05
        /// 
        /// testing method to ensure the program is correctly detecting roles
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/28
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="user"></param>
        /// <returns></returns>
        private string RoleBuilder(UsersVM user)
        {
            string roles = "";
            if (user.Roles.Count == 1)
            {
                roles = user.Roles[0] + ".";
            }
            else if (user.Roles.Count == 2)
            {
                roles = user.Roles[0] + " and " + user.Roles[1] + ".";
            }
            else
            {
                for (int i = 0; i < user.Roles.Count; i++)
                {
                    if (i == user.Roles.Count - 1)
                    {
                        roles += "and " + user.Roles[i] + ".";
                        break;
                    }
                    roles += user.Roles[i] + ", ";
                }
            }
            return roles;
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
            NavigationService.Navigate(SignUp.GetSignUpPage());
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
        private void txtEmail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtPassword.Focus();
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
        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnLogIn_Click(sender, e);
            }
        }
    }

}
