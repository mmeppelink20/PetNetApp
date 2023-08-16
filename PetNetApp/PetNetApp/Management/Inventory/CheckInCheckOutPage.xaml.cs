/// <summary>
/// Nathan Zumsande
/// Created: 2023/04/12
/// Presentation layer methods for the CheckInCheckOut UI
/// </summary>
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

namespace WpfPresentation.Management.Inventory
{
    /// <summary>
    /// Interaction logic for CheckInCheckOutPage.xaml
    /// </summary>
    public partial class CheckInCheckOutPage : Page
    {
        MasterManager _manager = null;
        List<ShelterInventoryItemVM> _inventory = null;

        /// <summary>
        /// Nathan Zumsande
        /// Created: 2023/04/12
        /// Initalizes the CheckInCheckOutPage
        /// </summary>
        /// <param name="manager"></param>
        public CheckInCheckOutPage(MasterManager manager)
        {
            _manager = manager;
            InitializeComponent();
        }

        /// <summary>
        /// Nathan Zumsande
        /// Created: 2023/04/12
        /// Method to run when the page loads
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckInCheckOut_Loaded(object sender, RoutedEventArgs e)
        {
            UpdatePage();
        }

        /// <summary>
        /// Nathan Zumsande
        /// Created: 2023/04/12
        /// Public helper method used to reload the pages data grid
        /// </summary>
        public void UpdatePage()
        {
            try
            {
                _inventory = _manager.ShelterInventoryItemManager.RetrieveInventoryByShelterId((int)_manager.User.ShelterId);
                datShelterInventory.ItemsSource = _inventory;
            }
            catch (Exception)
            {
                PromptWindow.ShowPrompt("No Data Found", "Failed to retrieve shelter inventory");
                return;
            }
        }

        /// <summary>
        /// Nathan Zumsande
        /// Created: 2023/04/12
        /// Navigates to the EditItemQuantityPage to add to the quantity
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCheckIn_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = (ShelterInventoryItemVM)datShelterInventory.SelectedItem;
            if (selectedItem == null)
            {
                PromptWindow.ShowPrompt("Error", "Please select the desired item to increase the quantity of from the list.", ButtonMode.Ok);
            }
            else
            {
                frmCheckInCheckOut.Navigate(new EditInventoryQuantityPage(_manager, selectedItem, true, this));
            }
        }

        /// <summary>
        /// Nathan Zumsande
        /// Created: 2023/04/12
        /// Navigates to the EditItemQuantityPage to decrease the quantity
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCheckOut_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = (ShelterInventoryItemVM)datShelterInventory.SelectedItem;
            if (selectedItem == null)
            {
                PromptWindow.ShowPrompt("Error", "Please select the desired item to decrease the quantity of from the list.", ButtonMode.Ok);
            }
            else
            {
                frmCheckInCheckOut.Navigate(new EditInventoryQuantityPage(_manager, selectedItem, false, this));
            }
        }
    }
}
