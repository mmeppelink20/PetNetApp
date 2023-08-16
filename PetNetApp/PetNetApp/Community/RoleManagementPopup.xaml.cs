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
using System.Windows.Shapes;

namespace WpfPresentation.Community
{
    /// <summary>
    /// Interaction logic for RoleManagementPopup.xaml
    /// </summary>
    /// 
    /// <summary>
    /// Barry Mikulas
    /// Created: 2023/02/11
    /// 
    /// </summary>
    /// Window for managing a single user's roles.
    /// 
    /// <remarks>
    /// Updater Barry Mikulas
    /// Updated: 2023/02/26
    /// 
    /// Zaid Rachman
    /// Updated: 2023/04/27
    /// 
    /// Final QA
    /// 
    /// </remarks>
    public partial class RoleManagementPopup : Window
    {
        private MasterManager _masterManager = MasterManager.GetMasterManager();
        private List<Role> _roles = new List<Role>(); //for the role list combo box
        private List<Role> _rolesByUser = new List<Role>(); //user's role list
        private Users _users;

        public RoleManagementPopup(MasterManager manager, Users user)
        {
            InitializeComponent();
            this._masterManager = manager;
            this._users = user;
        }

        /// <summary>
        /// Barry Mikulas
        /// Created: 2023/02/15
        /// 
        /// Event handler for the click of the add role button.
        ///
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/27
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_AddRole_Click(object sender, RoutedEventArgs e)
        {
            Role newUserRole = new Role();
            bool success = false;
            newUserRole.RoleId = cboChooseRole.Text;
            //check to see if role selected from combo box
            if (cboChooseRole.SelectedItem == null || ((Role)cboChooseRole.SelectedItem).RoleId == "Choose Role")
            {
                //if no role selected tell user
                PromptWindow.ShowPrompt("Error", "Please select a role to add and try again", ButtonMode.Ok);
                return;
            }
            else
            {
                //check to see if role list already has role
                for (int i = 0; i < _rolesByUser.Count(); i++)
                {
                    if (_rolesByUser[i].RoleId == newUserRole.RoleId)
                    {
                        PromptWindow.ShowPrompt("Error", "User already has the role: " + newUserRole.RoleId + ". Please choose another.", ButtonMode.Ok);
                        return;
                    }
                }
                if (PromptWindow.ShowPrompt("Role to Add", "Click Save to add the role: " + newUserRole.RoleId + " for the user.", ButtonMode.SaveCancel) == PromptSelection.Cancel)
                {
                    return;
                }
                //add role to list
                try
                {
                    success = _masterManager.RoleManager.AddRoleByUsersId(newUserRole, _users.UsersId);
                }
                catch (Exception ex)
                {
                    PromptWindow.ShowPrompt("Error", ex.Message, ButtonMode.Ok);
                    return;
                }

                //reload role list 
                PopulateUserRoleGrid();
            }

        }

        /// <summary>
        /// Barry Mikulas
        /// Created: 2023/02/11
        /// 
        /// Event handler for clicking the previous button. Was never implemented.
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/27
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Previous_Click(object sender, RoutedEventArgs e)
        {
            if (PromptWindow.ShowPrompt("Previous", "Go to edit user info screen?", ButtonMode.YesNo) == PromptSelection.Yes)
            {
                //navigate back to edit user details
            }
        }

        /// <summary>
        /// Barry Mikulas
        /// Created: 2023/02/11
        /// Event handler for clicking the finish button - Button was renamed "done".
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/27
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Finish_Click(object sender, RoutedEventArgs e)
        {
            if (PromptWindow.ShowPrompt("Save", "Are you finished editing roles for: " + _users.GivenName + " " + _users.FamilyName + "?", ButtonMode.YesNo) == PromptSelection.Yes)
            {
                this.Close();
            }
         }

        /// <summary>
        /// Barry Mikulas
        /// Created: 2023/02/11
        /// Prompts user for confirmation of cancelation, closes window if confirmed
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/27
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            // verify person wants to close the window
            if (PromptWindow.ShowPrompt("Confirm Cancel", "Are you sure you want to cancel?", ButtonMode.YesNo) == PromptSelection.No)
            {
                //return to the window
            }
            else
            {
                //close the page
                this.Close();
            }
        }




        /// <summary>
        /// Barry Mikulas
        /// Created: 2023/02/11
        ///
        /// </summary>
        ///  <remark>
        /// 
        /// Modified: 2023/03/01
        /// by: Asa Armstrong
        /// 
        /// Zaid Rachman
        /// Updated: 2023/04/27
        /// 
        /// Final QA
        /// 
        /// </remark>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_RemoveRole(object sender, RoutedEventArgs e)
        {
            //can get the value of the RoleId from using ((Button)sender).Tag
            string roleId = "";
          
            if (sender.GetType().ToString() == "System.Windows.Controls.MenuItem")
            {
                var selectedRole = (Role)(datUserRoles.SelectedItem);
                roleId = selectedRole.RoleId;
            }
            else
            {
                roleId = ((Button)sender).Tag.ToString();
            }
            
            if (PromptWindow.ShowPrompt("Remove Role?", "Confirm, are you sure you want to remove the role " + roleId + "?", ButtonMode.YesNo) == PromptSelection.Yes)
            {
                // Created By: Asa Armstrong
                try
                {
                    if (_masterManager.RoleManager.RemoveRoleByUsersIdAndRoleId(_users.UsersId, roleId))
                    {
                        PromptWindow.ShowPrompt("Congrats!", "Role Removed.");
                        PopulateUserRoleGrid();
                    }
                    else
                    {
                        throw new ApplicationException("Role not removed.");
                    }
                }
                catch (Exception ex)
                {
                    PromptWindow.ShowPrompt("Error", "" + ex.Message);
                }
                // End of Asa Armstrong's UC
            }
            else
            {
                //do nothing
            }
        }

        /// <summary>
        /// Barry Mikulas
        /// Created: 2023/02/11
        /// Populates the window with user roles and drop down list.
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/27
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // retrieve role list for combo box
            try
            {
                _roles = _masterManager.RoleManager.RetrieveAllRoles();
                //this.cboChooseRole.ItemsSource = from r in _roles
                //                                 orderby r.RoleId
                //                                 select r.RoleId; 
                var newItem = new Role { RoleId = "Choose Role", Description = "Click a role"};
                this.cboChooseRole.Items.Add(newItem);
            
                foreach (var item in _roles)
                {
                    this.cboChooseRole.Items.Add(item);
                }
                //this.cboChooseRole.ItemsSource = _roles;
                cboChooseRole.DisplayMemberPath = "RoleId";
                cboChooseRole.SelectedItem = newItem;
                
            }
            catch (Exception ex)
            {
                PromptWindow.ShowPrompt("Error", ex.Message);
            }

            PopulateUserRoleGrid();
        }

        /// <summary>
        /// Barry Mikulas
        /// Created: 2023/02/11
        /// Retrieves user's role list and populates the grid.
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/27
        /// 
        /// Final QA
        /// </remarks>
        private void PopulateUserRoleGrid()
        {
            //retrieve user's roles
            //populate data grid
            try
            {
                _rolesByUser = _masterManager.RoleManager.RetrieveRoleListByUserId(_users.UsersId);
                datUserRoles.ItemsSource = _rolesByUser;
            }
            catch (Exception ex)
            {
                PromptWindow.ShowPrompt("Error", ex.Message);
            }
        }

        /// <summary>
        /// Barry Mikulas
        /// Created: 2023/02/11
        /// Event Handler for clicking the X in the upper right of the window/
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/27
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCloseWindowX_Click(object sender, RoutedEventArgs e)
        {
            btn_Cancel_Click(sender, e);
        }

        /// <summary>
        /// Barry Mikulas
        /// Created: 2023/02/11
        /// Event handler for clicking the remove button on the roles grid.
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/27
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextRemoveRole_Click(object sender, RoutedEventArgs e)
        {
            btn_RemoveRole(sender, e);
        }
    }
}
