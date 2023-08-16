///<summary>
/// Andrew Cromwell
/// Created: 2023/03/27
/// 
/// </summary>
/// <remarks>
/// Zaid Rachman
/// Updated: 2023/04/21
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

namespace WpfPresentation.Management.Inventory
{
    /// <summary>
    /// Andrew Cromwell
    /// Created: 2023/03/27
    /// 
    /// Interaction logic for InventoryNavigationPage.xaml
    /// </summary>
    /// <remarks>
    /// Nathan Zumsande
    /// Updated: 2023/04/20
    /// Added role access for the inventory navigation
    /// </remarks>
    public partial class InventoryNavigationPage : Page
    {
        public static InventoryNavigationPage _existingInventoryNavigationPage = null;

        private MasterManager _manager = null;
        private Button[] _inventoryTabButtons;
        /// <summary>
        /// Andrew Cromwell
        /// Created: 2023/03/27
        /// 
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/21
        /// Final QA
        /// </remarks>
        /// <param name="manager"></param>
        public InventoryNavigationPage(MasterManager manager)
        {
            InitializeComponent();
            _manager = manager;
            _inventoryTabButtons = new Button[] { btnShelterInventory, btnItemLibrary, btnViewRequests, btnViewResourceAddRequest, btnCheckIn, btnInventoryChanges, btnAnimalSpecialNeeds, btnRequestFromShelter };
            btnShelterInventory_Click(this, new RoutedEventArgs());
        }
        /// <summary>
        /// Andrew Cromwell
        /// Created: 2023/03/27
        /// 
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/21
        /// Final QA
        /// </remarks>
        /// 
        /// <param name="manager"></param>
        /// <returns></returns>
        public static InventoryNavigationPage GetInventoryNavigationPage(MasterManager manager)
        {
            if(_existingInventoryNavigationPage == null)
            {
                _existingInventoryNavigationPage = new InventoryNavigationPage(manager);
            }
            _existingInventoryNavigationPage.ShowButtonsByRole();
            return _existingInventoryNavigationPage;
        }
        /// <summary>
        /// Andrew Cromwell
        /// Created: 2023/03/27
        /// 
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/21
        /// Final QA
        /// </remarks>
        /// <param name="selectedButton"></param>
        private void ChangeSelectedButton(Button selectedButton)
        {
            UnselectAllButtons();
            selectedButton.Style = (Style)Application.Current.Resources["rsrcSelectedButton"];
        }
        /// <summary>
        /// Andrew Cromwell
        /// Created: 2023/03/27
        /// 
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/21
        /// Final QA
        /// </remarks>
        private void UnselectAllButtons()
        {
            foreach (Button button in _inventoryTabButtons)
            {
                button.Style = (Style)Application.Current.Resources["rsrcUnselectedButton"];
            }
        }
        /// <summary>
        /// Andrew Cromwell
        /// Created: 2023/03/27
        /// 
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/21
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnShelterInventory_Click(object sender, RoutedEventArgs e)
        {
            ChangeSelectedButton(btnShelterInventory);
            frameInventory.Navigate(new ViewShelterInventoryPage());
        }
        /// <summary>
        /// Andrew Cromwell
        /// Created: 2023/03/27
        /// 
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/21
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItemLibrary_Click(object sender, RoutedEventArgs e)
        {
            ChangeSelectedButton(btnItemLibrary);
            frameInventory.Navigate(new Library.LibraryUI(_manager));
        }
        /// <summary>
        /// Andrew Cromwell
        /// Created: 2023/03/27
        /// 
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/21
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnViewRequests_Click(object sender, RoutedEventArgs e)
        {
            ChangeSelectedButton(btnViewRequests);
            frameInventory.Navigate(ViewRequestListPage.GetViewRequestListPage(_manager));
        }
        /// <summary>
        /// Andrew Cromwell
        /// Created: 2023/03/27
        /// 
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/21
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnViewResourceAddRequest_Click(object sender, RoutedEventArgs e)
        {
            ChangeSelectedButton(btnViewResourceAddRequest);
            // replace with page name and then delete comment
            frameInventory.Navigate(ViewNewItemRequestsPage.GetViewNewItemRequestsPage());
        }
        /// <summary>
        /// Andrew Cromwell
        /// Created: 2023/03/27
        /// 
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/21
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCheckIn_Click(object sender, RoutedEventArgs e)
        {
            ChangeSelectedButton(btnCheckIn);
            frameInventory.Navigate(new CheckInCheckOutPage(_manager));
        }
        /// <summary>
        /// Andrew Cromwell
        /// Created: 2023/03/27
        /// 
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/21
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInventoryChanges_Click(object sender, RoutedEventArgs e)
        {
            ChangeSelectedButton(btnInventoryChanges);
            frameInventory.Navigate(ViewInventoryChangesPage.GetViewInventoryChangesPage(_manager));
        }
        /// <summary>
        /// Andrew Cromwell
        /// Created: 2023/03/27
        /// 
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/21
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAnimalSpecialNeeds_Click(object sender, RoutedEventArgs e)
        {
            ChangeSelectedButton(btnAnimalSpecialNeeds);
            // replace with page name and then delete comment
            frameInventory.Navigate(null);
        }
        /// <summary>
        /// 
        /// Andrew Cromwell
        /// Created: 2023/03/27
        /// 
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/21
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRequestFromShelter_Click(object sender, RoutedEventArgs e)
        {
            ChangeSelectedButton(btnRequestFromShelter);
            if(CreateShelterRequestPage.GetCreateShelterRequestPage(_manager) == null)
            {
                frameInventory.Navigate(SelectShelterForRequestPage.GetSelectShelterForRequestPage(_manager));
            }
            else
            {
                frameInventory.Navigate(CreateShelterRequestPage.GetCreateShelterRequestPage(_manager));
            }
            
        }

        /// <summary>
        /// Nathan Zumsande
        /// Created: 2023/04/20
        ///  Method to allow access to features of the inventory navigation
        ///  based on the users roles
        /// </summary>
        private void ShowButtonsByRole()
        {
            HideAllButtons();
            ShowShelterInventoryButtonByRole();
            ShowItemLibraryButtonByRole();
            ShowViewRequestsButtonByRole();
            ShowViewResourceAddRequestsButtonByRole();
            ShowCheckInCheckOutButtonByRole();
            ShowInventoryChangesButtonByRole();
            ShowRequestFromShelterButtonByRole();
        }

        /// <summary>
        /// Nathan Zumsande
        /// Created: 2023/04/20
        ///  Initalizes buttons to be hidden
        /// </summary>
        private void HideAllButtons()
        {
            foreach(var btn in _inventoryTabButtons)
            {
                btn.Visibility = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// Nathan Zumsande
        /// Created: 2023/04/20
        ///  Access for shelter inventory page
        /// </summary>
        private void ShowShelterInventoryButtonByRole()
        {
            string[] allowedRoles = { "Admin", "Manager", "Employee" };
            if (_manager.User.Roles.Exists(role => allowedRoles.Contains(role)))
            {
                btnShelterInventory.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// Nathan Zumsande
        /// Created: 2023/04/20
        ///  Access for item library page
        /// </summary>
        private void ShowItemLibraryButtonByRole()
        {
            string[] allowedRoles = { "Admin", "Manager" };
            if (_manager.User.Roles.Exists(role => allowedRoles.Contains(role)))
            {
                btnItemLibrary.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// Nathan Zumsande
        /// Created: 2023/04/20
        ///  Access for requests page
        /// </summary>
        private void ShowViewRequestsButtonByRole()
        {
            string[] allowedRoles = { "Admin", "Manager" };
            if (_manager.User.Roles.Exists(role => allowedRoles.Contains(role)))
            {
                btnViewRequests.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// Nathan Zumsande
        /// Created: 2023/04/20
        ///  Access for additional recource requests page
        /// </summary>
        private void ShowViewResourceAddRequestsButtonByRole()
        {
            string[] allowedRoles = { "Admin" };
            if (_manager.User.Roles.Exists(role => allowedRoles.Contains(role)))
            {
               btnViewResourceAddRequest.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// Nathan Zumsande
        /// Created: 2023/04/20
        ///  Access for check in and check out page
        /// </summary>
        private void ShowCheckInCheckOutButtonByRole()
        {
            string[] allowedRoles = { "Admin", "Manager", "Employee" };
            if (_manager.User.Roles.Exists(role => allowedRoles.Contains(role)))
            {
                btnCheckIn.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// Nathan Zumsande
        /// Created: 2023/04/20
        ///  Access for inventory changes page
        /// </summary>
        private void ShowInventoryChangesButtonByRole()
        {
            string[] allowedRoles = { "Admin", "Manager", "Employee" };
            if (_manager.User.Roles.Exists(role => allowedRoles.Contains(role)))
            {
                btnInventoryChanges.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// Nathan Zumsande
        /// Created: 2023/04/20
        ///  Access for requests from shelter page
        /// </summary>
        private void ShowRequestFromShelterButtonByRole()
        {
            string[] allowedRoles = { "Admin", "Manager"};
            if (_manager.User.Roles.Exists(role => allowedRoles.Contains(role)))
            {
                btnRequestFromShelter.Visibility = Visibility.Visible;
            }
        }
    }
}
