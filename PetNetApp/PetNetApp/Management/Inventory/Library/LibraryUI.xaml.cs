/// <summary>
/// Brian Collum
/// Created: 2023/03/06
/// 
/// This is the base UI for the Library, which represents the PetNet-wide library of inventory items that exist, and their tags
/// 
/// </summary>
///
/// <remarks>
/// Brian Collum
/// Updated: 2023/04/07
/// 
/// Nathan Zumsande
/// Updated: 2023/04/20
/// Added User role access
/// 
/// Zaid Rachman
/// Updated: 2023/04/21
/// Final QA
/// 
/// Brian Collum
/// Updated: 2023/04/21
/// Added library UI filtering
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
using LogicLayerInterfaces;
using DataObjects;

namespace WpfPresentation.Management.Inventory.Library
{
    public partial class LibraryUI : Page
    {
        private static LibraryUI _existingLibraryUI = null;
        private MasterManager _masterManager = null;
        private LibraryManager _libraryManager = null;

        private List<Item> _libraryItemList = null;
        // Filter controls
        private List<string> _categoryList = null;
        private List<string> _filteredCategoryList = null;
        private List<Item> _filteredLibraryItemList = null;
        private string _itemNameFilter = null;

        /// <summary>
        /// Brian Collum
        /// Created: 2023/02/23
        /// Constructor for the ShelterVM List UI
        /// </summary>
        /// 
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/21
        /// Final QA
        /// </remarks>
        /// <param name="manager">The MasterManager from the parent UI</param>
        public LibraryUI(MasterManager manager)
        {
            InitializeComponent();
            _masterManager = manager;
            _libraryManager = new LibraryManager();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/21
        /// Final QA
        /// </remarks>
        /// <param name="manager"></param>
        /// <returns></returns>
        public static LibraryUI GetLibraryUI(MasterManager manager)
        {
            if (_existingLibraryUI == null)
            {
                _existingLibraryUI = new LibraryUI(manager);
            }
            return _existingLibraryUI;
        }

        /// <summary>
        /// 
        /// </summary>
        ///  <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/21
        /// Final QA
        /// </remarks>
        public LibraryUI()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Brian Collum
        /// Created: 2023/02/24
        /// Refresh the list of library items by loading them from the database
        /// </summary>
        /// 
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/21
        /// Final QA
        /// 
        /// Brian Collum
        /// Updated: 2023/04/21
        /// Added support for filtering
        /// </remarks>
        public void RefreshLibraryList()
        {
            try
            {
                // Both category and item lists need to be updated in case of additions
                _categoryList = _masterManager.ItemManager.RetrieveAllCategories();
                _libraryItemList = _libraryManager.GetLibraryItemList();
                ApplyFilters();
                // Set the datagrid to display the filtered list
                datLibraryInventory.ItemsSource = _filteredLibraryItemList;
            }
            catch (Exception ex)
            {
                PromptWindow.ShowPrompt("Error", "Failed to retrieve list of library items " + ex.InnerException.Message, ButtonMode.Ok);
            }
        }

        /// <summary>
        /// Brian Collum
        /// Created: 2023/02/24
        /// Displays an explanation of what this UI is for
        /// </summary>
        /// 
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/21
        /// Final QA
        /// </remarks>
        private void btnLibraryHelp_Click(object sender, RoutedEventArgs e)
        {
            PromptWindow.ShowPrompt("What is this page for?", "PetNet Inventory Library" +
                "\n\nThis is a library of all inventory items in the PetNet Database." +
                "\n\nA PetNet Administrator can add new items to the database." +
                "\n\nA Shelter worker with inventory management privileges may populate their shelter inventory by adding items off of this list." +
                "\n\nIf you manage a shelter's inventory and you need an item added to this list, please use the 'Request a New Item' button, and a PetNet Administrator will review your request.");
        }

        /// <summary>
        /// Brian Collum
        /// Created: 2023/02/24
        /// Refresh the list of library items on page load
        /// </summary>
        /// <remarks>
        /// Nathan Zumsande
        /// Updated 2023/04/20
        /// Added the call to the ShowButtonsByRoles method
        /// 
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/21
        /// Final QA
        /// 
        /// Brian Collum
        /// Updated 2023/04/21
        /// Now loads categories for filtering, and initializes filtering lists
        /// </remarks>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (_libraryItemList == null)
            {
                // Initialize filter lists
                try
                {
                    _categoryList = _masterManager.ItemManager.RetrieveAllCategories();
                    comboFilterLibraryByTag.ItemsSource = _categoryList;
                }
                catch (Exception ex)
                {
                    PromptWindow.ShowPrompt("Error", "Failed to retrieve list of item categories " + ex.InnerException.Message, ButtonMode.Ok);
                }
                _filteredCategoryList = new List<string>();
                _filteredLibraryItemList = new List<Item>();
                RefreshLibraryList();
            }
            ShowButtonsByRoles();
        }

        /// <summary>
        /// Nathan Zumsande
        /// Created: 2023/04/06
        /// Navigates to the AddResourceItemPage
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/21
        /// Final QA
        /// </remarks>
        private void btnAddLibraryItem_Click(object sender, RoutedEventArgs e)
        {
            frmLibrary.Navigate(new AddResourceItemPage(_masterManager.ItemManager, this));
        }

        /// <summary>
        /// Nathan Zumsande
        /// Created: 2023/04/06
        /// Navigates to the EditResourceItemPage
        /// </summary>
        /// 
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/21
        /// Final QA
        /// </remarks>
        private void btnEditLibraryItem_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = (Item)datLibraryInventory.SelectedItem;
            if (selectedItem == null)
            {
                PromptWindow.ShowPrompt("Error", "Please select an item to edit from the list to edit.", ButtonMode.Ok);
            }
            else
            {
                frmLibrary.Navigate(new AddResourceItemPage(_masterManager.ItemManager, selectedItem, this));
            }
        }

        /// <summary>
        /// Nathan Zumsande
        /// Created: 2023/04/06
        /// Navigates to the AddCategoryTagPage
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/21
        /// Final QA
        /// </remarks>
        private void btnAddCategory_Click(object sender, RoutedEventArgs e)
        {
            frmLibrary.Navigate(new AddCategoryTagPage(_masterManager, this));
        }
        /// <summary>
        /// Brian Collum
        /// Created: 2023/04/07
        /// Adds the selected Library item to the currently selected shelter's inventory
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/21
        /// Final QA
        /// </remarks>
        private void btnAddToShelterInventory_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = (Item)datLibraryInventory.SelectedItem;
            var currentShelter = _masterManager.User.ShelterId;
            if (currentShelter != null)
            {
                if (selectedItem == null)
                {
                    PromptWindow.ShowPrompt("Error", "Please select an item from the list to add to your shelter.", ButtonMode.Ok);
                }
                else
                {
                    try
                    {
                        PromptSelection result = PromptWindow.ShowPrompt("Add Item?", "Do you want to add this item to Shelter " + currentShelter, ButtonMode.YesNo);
                        if (result == PromptSelection.Yes)
                        {
                            if (_masterManager.ShelterInventoryItemManager.AddToShelterInventory((int)currentShelter, selectedItem.ItemId))
                            {
                                PromptWindow.ShowPrompt("Item Added", "Added " + selectedItem.ItemId + " to " + currentShelter, ButtonMode.Ok);
                            }
                            else
                            {
                                PromptWindow.ShowPrompt("Item visible", selectedItem.ItemId + " is active in " + currentShelter, ButtonMode.Ok);
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        PromptWindow.ShowPrompt("Error", "Failed to add that item to your shelter.", ButtonMode.Ok);
                    }
                }
            }
            else
            {
                PromptWindow.ShowPrompt("Error", "You must have a shelter associated with your account in order to use this feature.", ButtonMode.Ok);
            }
        }

        /// <summary>
        /// Nathan Zumsande
        /// Created: 2023/04/20
        /// Shows and hides admin functions based on if the logged in user
        /// has the admin role assigned to them
        /// </summary>
        private void ShowButtonsByRoles()
        {
            if (!_masterManager.User.Roles.Contains("Admin"))
            {
                lblPetNetAdminButtons.Visibility = Visibility.Collapsed;
                btnAddCategory.Visibility = Visibility.Collapsed;
                btnAddLibraryItem.Visibility = Visibility.Collapsed;
                btnEditLibraryItem.Visibility = Visibility.Collapsed;
            }
            else
            {
                lblPetNetAdminButtons.Visibility = Visibility.Visible;
                btnAddCategory.Visibility = Visibility.Visible;
                btnAddLibraryItem.Visibility = Visibility.Visible;
                btnEditLibraryItem.Visibility = Visibility.Visible;
            }
        }

		/// <summary>
        /// Brian Collum
        /// Created: 2023/04/21
        /// 
        /// Apply the user's selected search filters to the Library UI
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// 
        /// </remarks>
        private void ApplyFilters()
        {
            // Update category filters
            try
            {
                _filteredCategoryList = new List<string>();
                foreach (var tag in comboFilterLibraryByTag.SelectedItems)
                {
                    _filteredCategoryList.Add(tag.ToString());
                }
            }
            catch (Exception ex)
            {
                PromptWindow.ShowPrompt("Error", "Failed to update category filters " + ex.InnerException.Message, ButtonMode.Ok);
            }
            // Update name filter
            if (_itemNameFilter == null)
            {
                _itemNameFilter = "";
            }
            // Reset the list
            _filteredLibraryItemList = new List<Item>();
            // If no items are selected, apply no category filters
            if (_filteredCategoryList.Count > 0)
            {
                // Find tags common to selected filters and add those items to the new Library display
                IEnumerable<string> commonItems;
                foreach (Item item in _libraryItemList)
                {
                    commonItems = item.CategoryId.Intersect(_filteredCategoryList); // OR style tag matching => Match any selected tags
                    if (commonItems.Count() > 0 && item.ItemId.IndexOf(_itemNameFilter, 0, StringComparison.OrdinalIgnoreCase) != -1)
                    {
                        _filteredLibraryItemList.Add(item);
                    }
                }
            }
            // No category filters set, filtre by name only
            else
            {
                foreach (Item item in _libraryItemList)
                {
                    // Match name search string
                    if (item.ItemId.IndexOf(_itemNameFilter, 0, StringComparison.OrdinalIgnoreCase) != -1)
                    {
                        _filteredLibraryItemList.Add(item);
                    }
                }
            }
        }

        /// <summary>
        /// Brian Collum
        /// Created: 2023/04/21
        /// 
        /// Register changes to the combobox when the user clicks a tag to filter by
        /// </summary>
        /// <remarks>
        /// </remarks>
        private void comboFilterLibraryByTag_ItemSelectionChanged(object sender, Xceed.Wpf.Toolkit.Primitives.ItemSelectionChangedEventArgs e)
        {
            RefreshLibraryList();
        }

        /// <summary>
        /// Brian Collum
        /// Created: 2023/04/21
        /// 
        /// Clear the placeholder text from the search by name textbox when the user selects it
        /// </summary>
        /// <remarks>
        /// </remarks>
        private void txtFilterLibraryByName_GotFocus(object sender, RoutedEventArgs e)
        {
            // Clear search box on select
            if (txtFilterLibraryByName.Text == "" || txtFilterLibraryByName.Text == "Filter by Name")
            {
                txtFilterLibraryByName.Text = "";
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
        private void txtFilterLibraryByName_LostFocus(object sender, RoutedEventArgs e)
        {
            // Reset placeholder text when text box is empty and user deselects
            if (txtFilterLibraryByName.Text == "")
            {
                txtFilterLibraryByName.Text = "Filter by Name";
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
        private void txtFilterLibraryByName_KeyDown(object sender, KeyEventArgs e)
        {
            // When user hits Return after entering text
            if (e.Key == Key.Return)
            {
                try
                {
                    _itemNameFilter = txtFilterLibraryByName.Text;
                    RefreshLibraryList();
                }
                catch (Exception ex)
                {
                    PromptWindow.ShowPrompt("Error", "Failed to apply item name filter, " + ex.InnerException.Message, ButtonMode.Ok);
                }
            }
        }

        /// Andrew Cromwell
        /// Created: 2023/04/20
        /// Opens the page where the user can request for a new item to be added to the library
        /// </summary>
        private void btnRequestLibraryAddition_Click(object sender, RoutedEventArgs e)
        {
            frmLibrary.Navigate(new RequestNewLibraryItem(_masterManager));
        }
    }
}
