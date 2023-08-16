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
    /// Created: 2023/02/15
    /// 
    /// WPF for User Details within "Account Settings"
    /// </summary>
    ///
    /// <remarks>
    /// Oleksiy Fedchuk
    /// Updated: 2023/04/28
    /// 
    /// Final QA
    /// </remarks>
    public partial class UserDetailsPage : Page
    {
        private static UserDetailsPage _existingUserDetails = null;
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
        public UserDetailsPage()
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
        public static UserDetailsPage GetUserDetailsPage()
        {
            if (_existingUserDetails == null)
            {
                _existingUserDetails = new UserDetailsPage();
            }
            return _existingUserDetails;
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
        private void cmboGender_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> genders = new List<string>();
            try
            {
                genders = _manager.UsersManager.RetrieveGenders();
            }
            catch (Exception ex)
            {

                throw ex;
            }

            foreach (var gender in genders)
            {
                cmboGender.Items.Add(gender);
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
        private void cmboPronoun_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> pronouns = new List<string>();

            try
            {
                pronouns = _manager.UsersManager.RetrievePronouns();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            foreach (var pronoun in pronouns)
            {
                cmboPronoun.Items.Add(pronoun);
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
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (txtGivenName.Text == _manager.User.GivenName &&
               txtFamilyName.Text == _manager.User.FamilyName &&
               cmboGender.SelectedItem.ToString() == _manager.User.GenderId &&
               cmboPronoun.SelectedItem.ToString() == _manager.User.PronounId &&
               txtAddress.Text == _manager.User.Address &&
               txtAddress2.Text == _manager.User.Address2 &&
               txtPhone.Text == _manager.User.Phone &&
               txtZipcode.Text == _manager.User.Zipcode)
            {
                PromptWindow.ShowPrompt("Hey", "You haven't changed anything!");
            }
            else
            {
                Users user = new Users()
                {
                    UsersId = _manager.User.UsersId,
                    GivenName = txtGivenName.Text,
                    FamilyName = txtFamilyName.Text,
                    GenderId = (string)cmboGender.SelectedItem,
                    PronounId = (string)cmboPronoun.SelectedItem,
                    Address = txtAddress.Text,
                    Address2 = txtAddress2.Text,
                    Phone = txtPhone.Text,
                    Zipcode = txtZipcode.Text

                };

                try
                {
                    if (_manager.UsersManager.EditUserDetails(_manager.User, user))
                    {
                        PromptWindow.ShowPrompt("Success", "Profile successfully updated!");
                        LoadUserDetails();
                    }

                }
                catch (Exception up)
                {
                    PromptWindow.ShowPrompt("Error", up.Message);
                }
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
        private void LoadUserDetails()
        {
            txtGivenName.Text = _manager.User.GivenName;
            txtFamilyName.Text = _manager.User.FamilyName;
            cmboGender.SelectedItem = _manager.User.GenderId;
            cmboPronoun.SelectedItem = _manager.User.PronounId;
            txtAddress.Text = _manager.User.Address;
            txtAddress2.Text = _manager.User.Address2;
            txtPhone.Text = _manager.User.Phone;
            txtZipcode.Text = _manager.User.Zipcode;
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
            // 2 lines added by Stephen Jaurigue
            cmboGender.Items.Clear();
            cmboPronoun.Items.Clear();

            LoadUserDetails();
        }
    }
}
