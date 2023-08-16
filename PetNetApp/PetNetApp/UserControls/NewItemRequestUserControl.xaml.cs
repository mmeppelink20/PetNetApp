/// <summary>
/// Andrew Schneider
/// Created: 2023/03/31
/// 
/// Interaction logic for NewItemRequestUserControl.xaml
/// </summary>
///
/// <remarks>
/// Updater Name
/// Updated: yyyy/mm/dd
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

namespace WpfPresentation.UserControls
{
    /// <summary>
    /// Interaction logic for NewItemRequestUserControl.xaml
    /// </summary>
    public partial class NewItemRequestUserControl : UserControl
    {
        public ResourceAddRequest ResourceAddRequest { get; set; }
        public bool UseAlternateColors { get; set; }
        private ResourceAddRequest newResourceAddRequest = new ResourceAddRequest();
        private MasterManager _masterManager = MasterManager.GetMasterManager();
        private Users _user = new Users();
        private ScrollViewer _svItemRequestList = new ScrollViewer();

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/03/31
        /// 
        /// Constructor method that assigns parameters and calls necessary methods
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="resourceAddRequest">ResourceAddRequest that needs a user control</param>
        /// <param name="useAlternateColors">Bool for control color</param>
        /// <param name="svItemRequestList">Parent ScrollViewer the popup will be centered on</param>
        public NewItemRequestUserControl(ResourceAddRequest resourceAddRequest, bool useAlternateColors, ScrollViewer svItemRequestList)
        {
            ResourceAddRequest = resourceAddRequest;
            UseAlternateColors = useAlternateColors;
            DataContext = this;
            _svItemRequestList = svItemRequestList;
            CreateNewResourceAddRequestObject();
            InitializeComponent();
        }

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/03/31
        /// 
        /// Creates a new ResourceAddRequest object which can have "Active" set
        /// to false and then be passed to EditResourceAddRequestActiveField.
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        private void CreateNewResourceAddRequestObject()
        {
            newResourceAddRequest.ResourceAddRequestId = ResourceAddRequest.ResourceAddRequestId;
            newResourceAddRequest.ShelterId = ResourceAddRequest.ShelterId;
            newResourceAddRequest.Active = false;
        }

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/03/31
        /// 
        /// Event handler for the "Deactivate" button. Calls the Edit method to update the
        /// ResourceAddRequest's "Active" field in the database. Shows confirmaition, success,
        /// and error dialogs as needed.
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeactivate_Click(object sender, RoutedEventArgs e)
        {
            bool success = false;
            var result = PromptWindow.ShowPrompt("Confirm", "Are you sure you want to deactivate '"
                                                       + ResourceAddRequest.Title + "'?", ButtonMode.YesNo);
            if (result == PromptSelection.Yes)
            {
                try
                {
                    success = _masterManager.ResourceAddRequestManager
                        .EditResourceAddRequestActiveField(ResourceAddRequest, new ResourceAddRequest() { Active = false });
                }
                catch (Exception ex)
                {
                    PromptWindow.ShowPrompt("Error", "Deactivating new item request failed.\n"
                                                + ex, ButtonMode.Ok);
                }

                if (success)
                {
                    PromptWindow.ShowPrompt("Success", "'" + ResourceAddRequest.Title
                                                + "' has been deactivated.", ButtonMode.Ok);
                    WpfPresentation.Management.Inventory.ViewNewItemRequestsPage
                        .GetViewNewItemRequestsPage();
                }
            }
        }

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/04/01
        /// 
        /// Helper method that obtains the user Id for the user that created the ResourceAddRequest,
        /// calls the accessor method to obtain the user name of the user, and then assigns the
        /// name to lblUsersName.
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        private void AssignUsersName()
        {
            int id = ResourceAddRequest.UsersId;
            try
            {
                _user = _masterManager.UsersManager.RetrieveUserByUsersId(id);
                if (_user.GivenName == null)
                {
                    _user.GivenName = "";
                    lblUsersName.Content = _user.FamilyName + " said:";
                }
                else
                {
                    lblUsersName.Content = _user.GivenName + " " + _user.FamilyName + " said:";
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/03/31
        /// 
        /// Event handler that calls AssinUsersName() and then displays the Notes popup.
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _this_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AssignUsersName();
            popResourceAddRequestNote.PlacementTarget = _svItemRequestList;
            popResourceAddRequestNote.IsOpen = true;
        }

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/03/31
        /// 
        /// Event handler that keeps the Notes popup open when _this_MouseDown event ends
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _this_MouseUp(object sender, MouseButtonEventArgs e)
        {
            popResourceAddRequestNote.IsOpen = true;
        }

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/04/01
        /// 
        /// Event handler that closes the Notes popup when the "X" button is clicked.
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClosePopupX_Click(object sender, RoutedEventArgs e)
        {
            popResourceAddRequestNote.IsOpen = false;
        }

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/04/01
        /// 
        /// Event handler that closes the Notes popup when the "Close" button is clicked.
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClosePopup_Click(object sender, RoutedEventArgs e)
        {
            popResourceAddRequestNote.IsOpen = false;
        }
    }
}
