/// <summary>
/// Andrew Schneider
/// Created: 2023/03/30
/// 
/// Interaction logic for ViewNewItemRequestsPage.xaml
/// </summary>
///
/// <remarks>
/// Updater Name
/// Updated: yyyy/mm/dd
/// </remarks>

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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfPresentation.UserControls;


namespace WpfPresentation.Management.Inventory
{

    public partial class ViewNewItemRequestsPage : Page
    {
        private static ViewNewItemRequestsPage _existingViewNewItemRequestsPage = null;
        private MasterManager _masterManager = MasterManager.GetMasterManager();
        private List<ResourceAddRequest> _resourceAddRequests = null;

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/03/30
        /// 
        /// Private contstructor for ViewNewItemRequestsPage
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// </remarks>
        private ViewNewItemRequestsPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/03/30
        /// 
        /// Gets the existing NewItemRequest page or a new one if it doesn't exist.
        /// Refreshes data but maintains page
        /// </summary>
        /// 
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <returns>An instance of ViewNewItemRequestsPage</returns>
        public static ViewNewItemRequestsPage GetViewNewItemRequestsPage()
        {
            if (_existingViewNewItemRequestsPage == null)
            {
                _existingViewNewItemRequestsPage = new ViewNewItemRequestsPage();
            }
            _existingViewNewItemRequestsPage.LoadResourceAddRequestsData();

            return _existingViewNewItemRequestsPage;
        }

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/03/31
        /// 
        /// Tries to retrieve the active resource add requests and calls PopulateResourceAddRequestList().
        /// Based on Stephen's ViewCampaign methods.
        /// </summary>
        /// 
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data 
        /// </remarks>
        private void LoadResourceAddRequestsData()
        {
            try
            {
                _resourceAddRequests = _masterManager.ResourceAddRequestManager.
                    RetrieveActiveResourceAddRequestsByShelterId((int)_masterManager.User.ShelterId);
                PopulateResourceAddRequestList();
            }
            catch (ApplicationException ex)
            {
                _resourceAddRequests = new List<ResourceAddRequest>();
                PromptWindow.ShowPrompt("Error", ex.Message);
            }
        }

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/03/31
        /// 
        /// If there are no resource add requests displays message to that affect, otherwise
        /// uses the requests to build NewItemRequestUserControls and assigns them to the 
        /// resource stackpanel. Based on Stephen's method from ViewCampaign.
        /// </summary>
        /// 
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data 
        /// </remarks>
        private void PopulateResourceAddRequestList()
        {
            stackRequests.Children.Clear();
            if (_resourceAddRequests.Count() == 0)
            {
                nothingToShowMessage.Visibility = Visibility.Visible;
            }
            else
            {
                nothingToShowMessage.Visibility = Visibility.Collapsed;
                bool alternate = false;

                foreach (ResourceAddRequest resourceAddRequest in _resourceAddRequests)
                {
                    NewItemRequestUserControl item = new NewItemRequestUserControl(resourceAddRequest, alternate, svItemRequestList);
                    stackRequests.Children.Add(item);
                    alternate = !alternate;
                }
            }
        }
    }
}
