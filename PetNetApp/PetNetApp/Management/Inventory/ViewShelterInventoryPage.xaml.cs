/// <summary>
/// Zaid Rachman
/// Created: 2023/03/19
/// 
/// Page containing a list of all items in a shelter
/// </summary>
///
/// <remarks>
/// Brian Collum
/// Updated: 2023/04/07
/// 
/// Added refreshShelterInventoryList and btnEdit_Click
/// 
/// Nathan Zumsande
/// Updated: 2023/04/20
/// Added the ShowRolesByButton and HideAllButton methods and modifyed
/// the onload to provide role access
/// </remarks>
/// <remarks>
/// Oleksiy Fedchuk
/// Updated: 2023/04/19
/// 
/// Final QA
/// 
/// Brian Collum
/// Updated: 2023/04/21
/// Added Inventory UI filtering
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
    /// Interaction logic for ViewShelterInventoryPage.xaml
    /// </summary>
    public partial class ViewShelterInventoryPage : Page
    {
        MasterManager _masterManager = MasterManager.GetMasterManager();
        List<ShelterInventoryItemVM> _shelterInventoryItemVMList = null; //used to populate the datagrid
        List<ShelterInventoryItemVM> _shelterInventoryItemVMCart = new List<ShelterInventoryItemVM>(); //used to collect items to buy

        // Item Filtering
        string _itemNameFilter = null;
        List<ShelterInventoryItemVM> _shelterFilteredInventoryItemVMList = null;


        /// <summary>
        /// Zaid Rachman
        /// Created: 2023/03/19
        /// </summary>
        /// 
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/19
        /// 
        /// Final QA
        /// </remarks>
        public ViewShelterInventoryPage()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Zaid Rachman
        /// Created: 2023/03/29
        /// This constructor is for when the user presses the back button on the "ViewItemCart" page. This way the user doesn't lose their list of items
        /// </summary>
        /// 
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/19
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="shelterInventoryItemVMs"></param>
        public ViewShelterInventoryPage(List<ShelterInventoryItemVM> shelterInventoryItemVMs)
        {
            _shelterInventoryItemVMCart = shelterInventoryItemVMs;
            InitializeComponent();
        }
        /// <summary>
        /// Zaid Rachman
        /// Created: 2023/03/19
        /// Sets up datagrid
        /// 
        /// Zaid Rachman
        /// Updated: 2023/03/31
        /// Code regarding the cboShelter is currently commented out. This feature is being moved to another page. 
        /// </summary>
        /// <remarks>
        /// Nathan Zumsande
        /// Updated: 2023/04/20
        /// Added the Show buttons by role to onload
        /// so roles can only see assigned features.
        /// 
        /// 
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/19
        /// 
        /// Final QA and removed commented out code regarding cboShelter
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Users user;
            try
            {
                user = _masterManager.UsersManager.RetrieveUserByUsersId(MasterManager.GetMasterManager().User.UsersId);
            }
            catch (Exception)
            {
                PromptWindow.ShowPrompt("Missing Data", "Failed to retrieve user's shelter ID");
                return;

            }

            int? shelterId = user.ShelterId;

            if (shelterId != null)
            {
                try
                {
                    _shelterInventoryItemVMList = _masterManager.ShelterInventoryItemManager.RetrieveInventoryByShelterId((int)shelterId);
                }
                catch (Exception)
                {

                    PromptWindow.ShowPrompt("Missing Data", "Failed to retrieve shelter inventory");
                    return;
                }
                try
                {
                    UpdateFlags();
                    datShelterInventory.ItemsSource = _shelterInventoryItemVMList;

                }
                catch (Exception ex)
                {
                    throw ex;
                }


            }

            lblItemsInCart.Content = "Items In Cart: " + _shelterInventoryItemVMCart.Count.ToString();

            ShowButtonsByRole();
        }

        /// <summary>
        /// Zaid Rachman
        /// Created: 2023/03/19
        /// Populates the flags column
        /// Zaid Rachman
        /// Updated: 2023/04/04
        /// 
        /// Added Low Stock and Over Stock indicators 
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/19
        /// 
        /// Final QA
        /// </remarks>
        private void UpdateFlags()
        {

            List<ShelterInventoryItemVM> shelterItems = _shelterInventoryItemVMList;

            //Creates a list of flags in string form
            foreach (ShelterInventoryItemVM shelter in shelterItems)
            {
                List<string> Flags = new List<string>(); //used to collect all flags for a shelter item
                string flagsList = ""; //used later for formating the flags
                if (shelter.InTransit)
                {
                    Flags.Add("In Transit");
                }
                if (shelter.Urgent)
                {
                    Flags.Add("Urgent");
                }
                if (shelter.Processing)
                {
                    Flags.Add("Processing");
                }
                if (shelter.DoNotOrder)
                {
                    Flags.Add("Do Not Order");
                }
                if (shelter.CustomFlag != "")
                {
                    Flags.Add(shelter.CustomFlag);
                }
                if (shelter.Quantity < shelter.LowInventoryThreshold) //Checks to see if quantity is lower than the threshold set
                {
                    Flags.Add("Low Quantity");
                }
                if (shelter.Quantity > shelter.HighInventoryThreshold) //Checks to see if quantity is higher than the threshold set
                {
                    Flags.Add("Overstocked");
                }
                Console.WriteLine(Flags.Count);

                //Formating
                for (int i = 0; i < Flags.Count; i++)
                {
                    Console.WriteLine(Flags[i]);
                    flagsList += " " + Flags[i];

                    if (i == Flags.Count - 2)
                    {
                        if (Flags.Count > 2)
                        {
                            flagsList += ",";
                        }
                        flagsList += " and";
                    }
                    else if (i < Flags.Count - 2)
                    {
                        flagsList += ",";
                    }
                }


                shelter.DisplayFlags = flagsList; //Using the CustomFlag property as a way to show all flags



            }

        }

        /// <summary>
        /// Zaid Rachman
        /// 2023/03/19
        /// 
        /// Directs user to the viewedit page for the inventory item.
        /// </summary>
        /// <remarks>
        /// Nathan Zumsande
        /// Updated: 2023/04/20
        /// Double click wont direct to the edit page if the edit and delete buttons are not visible
        /// 
        ///
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/19
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void datShelterInventory_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (datShelterInventory.SelectedItem != null && btnDelete.Visibility == Visibility.Visible && btnEdit.Visibility == Visibility.Visible)
            {
                var selectedShelterItem = (ShelterInventoryItemVM)datShelterInventory.SelectedItem;
                NavigationService.Navigate(new ViewEditShelterInventoryItem(selectedShelterItem));
            }

        }
        /// <summary>
        /// Zaid Rachman
        /// Created: 2023/03/29
        /// </summary>
        /// 
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/19
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnViewCart_Click(object sender, RoutedEventArgs e)
        {
            if (_shelterInventoryItemVMCart != null)
            {
                NavigationService.Navigate(new ViewEditCartPage(_shelterInventoryItemVMCart));
            }

        }
        /// <summary>
        /// Zaid Rachman
        /// Created: 2023/02/31
        /// </summary>
        /// 
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/19
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddToCart_Click(object sender, RoutedEventArgs e)
        {
            ShelterInventoryItemVM selectedItem = (ShelterInventoryItemVM)datShelterInventory.SelectedItem;
            if (datShelterInventory.SelectedItem != null)
            {
                if (_shelterInventoryItemVMCart.Count > 0)
                {
                    bool alreadyIn = false;
                    foreach (ShelterInventoryItemVM shelter in _shelterInventoryItemVMCart)
                    {
                        if (shelter.ItemId.Equals(selectedItem.ItemId))
                        {
                            alreadyIn = true;
                            break;
                        }
                    }
                    if (!alreadyIn)
                    {
                        _shelterInventoryItemVMCart.Add(selectedItem);
                        lblItemsInCart.Content = "Items In Cart: " + _shelterInventoryItemVMCart.Count.ToString();
                    }
                }
                else
                {
                    _shelterInventoryItemVMCart.Add(selectedItem);
                    lblItemsInCart.Content = "Items In Cart: " + _shelterInventoryItemVMCart.Count.ToString();
                }

            }

        }
        /// <summary>
        /// Zaid Rachman
        /// Created 2023/02/31
        /// </summary>
        /// 
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/19
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (datShelterInventory.SelectedItem != null)
            {
                var selectedShelterItem = (ShelterInventoryItemVM)datShelterInventory.SelectedItem;
                NavigationService.Navigate(new ViewEditShelterInventoryItem(selectedShelterItem));
            }
        }
        /// <summary>
        /// Brian Collum
        /// Created: 2023/04/07
        /// Refresh the list of Inventory items by loading them from the database
        /// </summary>
        /// 
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/19
        /// 
        /// Final QA and fixed method name to follow standard practices
        /// 
        /// Brian Collum
        /// Updated: 2023/04/21
        /// Added search filter support
        /// </remarks>
        public void RefreshShelterInventoryList()
        {
            Users user;
            try
            {
                user = _masterManager.UsersManager.RetrieveUserByUsersId(MasterManager.GetMasterManager().User.UsersId);
            }
            catch (Exception)
            {
                PromptWindow.ShowPrompt("Missing Data", "Failed to retrieve user's shelter ID");
                return;
            }

            int? shelterId = user.ShelterId;

            if (shelterId != null)
            {
                try
                {
                    _shelterInventoryItemVMList = _masterManager.ShelterInventoryItemManager.RetrieveInventoryByShelterId((int)shelterId);
                }
                catch (Exception)
                {

                    PromptWindow.ShowPrompt("Missing Data", "Failed to retrieve shelter inventory");
                    return;
                }
                try
                {
                    UpdateFlags();
                    ApplyFilters(); // Apply filtering
                    datShelterInventory.ItemsSource = _shelterFilteredInventoryItemVMList;  // Load filtered Inventory
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            lblItemsInCart.Content = "Items In Cart: " + _shelterInventoryItemVMCart.Count.ToString();
        }
        /// <summary>
        /// Brian Collum
        /// Created 2023/04/07
        /// </summary>
        /// 
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/19
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (datShelterInventory.SelectedItem != null)
            {
                var selectedShelterItem = (ShelterInventoryItemVM)datShelterInventory.SelectedItem;
                try
                {
                    PromptSelection result = PromptWindow.ShowPrompt("Remove Item?", "Do you want to remove " + selectedShelterItem.ItemId + " from your shelter?", ButtonMode.YesNo);
                    if (result == PromptSelection.Yes)
                    {
                        _masterManager.ShelterInventoryItemManager.DisableShelterInventoryItem(selectedShelterItem.ShelterId, selectedShelterItem.ItemId);
                    }
                }
                catch (Exception ex)
                {
                    PromptWindow.ShowPrompt("Error", "Failed to remove that item from your shelter.", ButtonMode.Ok);
                }
            }
            else
            {
                PromptWindow.ShowPrompt("Error", "Please select an item from the list to remove from your shelter.", ButtonMode.Ok);
            }
            RefreshShelterInventoryList();
        }

        /// <summary>
        /// Brian Collum
        /// Created: 2023/04/21
        /// 
        /// Apply the user's selected search filter to the Inventory UI
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// 
        /// </remarks>
        private void ApplyFilters()
        {
            // Update item name filter
            if (_itemNameFilter == null || _itemNameFilter.Equals("Filter by Name"))
            {
                _itemNameFilter = "";
            }
            // Reset filter list
            _shelterFilteredInventoryItemVMList = new List<ShelterInventoryItemVM>();
            try
            {
                foreach (ShelterInventoryItemVM item in _shelterInventoryItemVMList)
                {
                    // Match name search string
                    if (item.ItemId.IndexOf(_itemNameFilter, 0, StringComparison.OrdinalIgnoreCase) != -1)
                    {
                        _shelterFilteredInventoryItemVMList.Add(item);  // Populate the filtered list
                    }
                }
            }
            catch (Exception ex)
            {
                PromptWindow.ShowPrompt("Error", "Failed to apply item name filter, " + ex.InnerException.Message, ButtonMode.Ok);
            }
        }

        /// <summary>
        /// Brian Collum
        /// Created: 2023/04/21
        /// 
        /// Clear the placeholder text from the search by name textbox when the user selects it
        /// </summary>
        /// <remarks>
        /// </remarks>
        private void txtSearchFilter_GotFocus(object sender, RoutedEventArgs e)
        {
            // Clear search box on select
            if (txtSearchFilter.Text == "" || txtSearchFilter.Text == "Filter by Name")
            {
                txtSearchFilter.Text = "";
            }
        }

        /// <summary>
        /// Brian Collum
        /// Created: 2023/04/21
        /// 
        /// Restore the placeholder text to the search by name textbox when the user deselects it
        /// </summary>
        /// <remarks>
        /// </remarks>
        private void txtSearchFilter_LostFocus(object sender, RoutedEventArgs e)
        {
            // Reset placeholder text when text box is empty and user deselects
            if (txtSearchFilter.Text == "")
            {
                txtSearchFilter.Text = "Filter by Name";
            }
        }

        /// <summary>
        /// Brian Collum
        /// Created: 2023/04/21
        /// 
        /// Apply search by name filtering when user presses enter after entering their search query
        /// </summary>
        /// <remarks>
        /// </remarks>
        private void txtSearchFilter_KeyDown(object sender, KeyEventArgs e)
        {
            // When user hits Return after entering text
            if (e.Key == Key.Return)
            {
                try
                {
                    _itemNameFilter = txtSearchFilter.Text;
                    RefreshShelterInventoryList();
                }
                catch (Exception ex)
                {
                    PromptWindow.ShowPrompt("Error", "Failed to apply item name filter, " + ex.InnerException.Message, ButtonMode.Ok);
                }
            }
        }

        /// <summary>
        /// Nathan Zumsande
        /// Created: 2023/04/20
        ///  Shows the edit and delete buttons if the user is a admin or manager,
        ///  and hides them if they are an employee
        /// </summary>
        private void ShowButtonsByRole()
        {

            HideAllButtons();
            string[] allowedRoles = { "Admin", "Manager" };
            if (_masterManager.User.Roles.Exists(role => allowedRoles.Contains(role)))
            {
                btnEdit.Visibility = Visibility.Visible;
                btnDelete.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// Nathan Zumsande
        /// Created: 2023/04/20
        ///  Hides the edit and delete button
        /// </summary>
        private void HideAllButtons()
        {
            btnEdit.Visibility = Visibility.Hidden;
            btnDelete.Visibility = Visibility.Hidden;
        }
    }
}
