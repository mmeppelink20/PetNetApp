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
using WpfPresentation.Community.UsersControl;
using LogicLayer;
using DataObjects;

namespace WpfPresentation.Community
{
    /// <summary>
    /// Interaction logic for UserManagementPage.xaml
    /// </summary>

    public partial class UserManagementPage : Page
    {
        private MasterManager _masterManager = MasterManager.GetMasterManager();

        List<UsersVM> _employeeList = null;

        public UserManagementPage()
        {
            InitializeComponent();
            
        }

        /// <summary>
        /// Hoang Chu
        /// Created: 2023/02/01
        /// 
        /// </summary>
        ///
        /// <remarks>
        /// Updater: Barry Mikulas
        /// Updated: 2023/02/26
        /// Updated the sub menu on user to change between suspend and unsuspend user depending on their current Suspend Status
        /// Updated menu for Update to say Update Roles
        /// 
        /// Zaid Rachman
        /// Updated: 2023/04/27
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="user"></param>
        /// <param name="index"></param>

        public void DisplayUsers(UsersVM user, int index)
        {
            UCPreviewUser ucPreviewUser = new UCPreviewUser();
            if (user.Active)
            {
                var bc = new BrushConverter();
                ucPreviewUser.elsIsActive.Fill = (Brush)bc.ConvertFrom("#3D8361");
                ucPreviewUser.elsIsActive.ToolTip = "Active User";
            }
            else
            {
                var bc = new BrushConverter();
                ucPreviewUser.elsIsActive.Fill = (Brush)bc.ConvertFrom("#F54242");
                ucPreviewUser.elsIsActive.ToolTip = "Inactive User";
            }
            ucPreviewUser.lblUserAccountName.Content = user.GivenName + " " + user.FamilyName;
            ucPreviewUser.lblUserEmailName.Content = user.Email;
            ucPreviewUser.btnUsersProfile.Click += (obj, arg) => usersProfile_MouseClick(user);
            ucPreviewUser.btnUsersMoreDetails.Click += (obj, arg) =>
                    {
                        ucPreviewUser.btnUsersMoreDetails.ContextMenu = new ContextMenu();
                        MenuItem menuItemUpdate = new MenuItem()
                        { Header = "Update Roles"};
                        menuItemUpdate.Click += (object1, args) => menuItem_Update_Click(user);
                        ucPreviewUser.btnUsersMoreDetails.ContextMenu.Items.Add(menuItemUpdate);


                        //Barry Mikulas 2023/02/26
                        //changed to check SuspendEmployee status and show corresponding menu
                        MenuItem menuItemSuspend = new MenuItem()
                        { Header = "Suspend" };
                        MenuItem menuItemUnsuspend = new MenuItem()
                        { Header = "Unsuspend" };
                        if (!user.Suspend) //show suspend menu item if user not suspended
                        {
                            menuItemSuspend.Click += (object1, args) => menuItem_Suspend_Click(user);
                            ucPreviewUser.btnUsersMoreDetails.ContextMenu.Items.Add(menuItemSuspend);
                        }
                        else
                        {
                            menuItemUnsuspend.Click += (object1, args) => menuItem_Unsuspend_Click(user);
                            ucPreviewUser.btnUsersMoreDetails.ContextMenu.Items.Add(menuItemUnsuspend);
                        }

                        MenuItem menuItemDeactivate = new MenuItem()
                        { Header = "Deactivate" };
                        MenuItem menuItemActivate = new MenuItem()
                        { Header = "Activate" };
                        if (user.Active)
                        {
                            menuItemDeactivate.Click += (object1, args) => menuItem_Deactivate_Click();
                            ucPreviewUser.btnUsersMoreDetails.ContextMenu.Items.Add(menuItemDeactivate);
                        }
                        else
                        {
                            menuItemActivate.Click += (object1, args) => menuItem_Activate_Click();
                            ucPreviewUser.btnUsersMoreDetails.ContextMenu.Items.Add(menuItemActivate);
                        }
                        ucPreviewUser.btnUsersMoreDetails.ContextMenu.IsOpen = true;

                        // Customer records option.

                        MenuItem menuItemCustomerRecords = new MenuItem()
                        { Header = "Customer Records" };
                        menuItemCustomerRecords.Click += (object1, args) => menuItem_CustomerRecords_Click(user);
                        ucPreviewUser.btnUsersMoreDetails.ContextMenu.Items.Add(menuItemCustomerRecords);

                        // Return statement.

                        return;
                    };
            if(index % 2 == 0)
            {
                var bc = new BrushConverter();
                ucPreviewUser.Background = (Brush)bc.ConvertFrom("#D6CDA4");
            }
            else
            {
                var bc = new BrushConverter();
                ucPreviewUser.Background = (Brush)bc.ConvertFrom("#EEF2E6");
            }
            stpUsersList.Children.Add(ucPreviewUser);
        }

        /// <summary>
        /// Created by Teft Francisco
        /// Created: 2023/03/03
        /// Button will navigate to the user's customer records.
        /// </summary>
        private void menuItem_CustomerRecords_Click(Users user)
        {
            NavigationService.Navigate(new CustomerRecordsPage(user));
        }

        private void usersProfile_MouseClick(UsersVM user)
        {
            NavigationService.Navigate(Misc.UserProfilePage.GetUserProfilePage(user));
        }

        // MenuItem Click
        /// <summary>
        /// Barry Mikulas
        /// 2023/04/27
        /// 
        /// 
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/27
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="user"></param>
        private void menuItem_Update_Click(Users user)
        {
            RoleManagementPopup roleManagementPopupWindow = new RoleManagementPopup(_masterManager, user);
            roleManagementPopupWindow.Owner = Window.GetWindow(this);
            roleManagementPopupWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            roleManagementPopupWindow.ShowDialog();
        }

        /// <summary>
        /// Created by Barry Mikulas
        /// Created: 2023/02/26
        /// Button will launch a window to confirm the user is being suspended.
        /// </summary>
        private void menuItem_Suspend_Click(Users user)
        {
            SuspendUserPopup suspendUserPopup = new SuspendUserPopup(_masterManager, user);
            suspendUserPopup.Owner = Window.GetWindow(this);
            suspendUserPopup.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            suspendUserPopup.ShowDialog();
            NavigationService.Navigate(new UserManagementPage());
        }

        /// <summary>
        /// Created by Barry Mikulas
        /// Created: 2023/02/26
        /// Button will launch a window to confirm the user is being unsuspended.
        /// </summary>
        private void menuItem_Unsuspend_Click(Users user)
        {
            
            SuspendUserPopup suspendUserPopup = new SuspendUserPopup(_masterManager, user);
            suspendUserPopup.Owner = Window.GetWindow(this);
            suspendUserPopup.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            suspendUserPopup.ShowDialog();
            NavigationService.Navigate(new  UserManagementPage());
        }

        private void menuItem_Deactivate_Click()
        {
            MessageBox.Show("Deativate");
        }

        private void menuItem_Activate_Click()
        {
            MessageBox.Show("Activate");
        }
        // End menu item click


        /// <summary>
        /// Hoang Chu
        /// Created: 2023/02/12
        /// 
        /// </summary>
        /// Show up the users list when the page is loaded
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (_employeeList == null)
            {
                try
                {
                    _employeeList = _masterManager.UsersManager.RetriveAllEmployees();
                    int index = 0;
                    foreach (UsersVM user in _employeeList)
                    {
                        DisplayUsers(user, index);
                        index++;
                    }
                }
                catch (Exception ex)
                {
                    PromptWindow.ShowPrompt("Error", "Can not get the data. \n\n" + ex.Message);
                }
            }
        }
    }

}
