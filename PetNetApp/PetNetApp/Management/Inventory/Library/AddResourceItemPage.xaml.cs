/// <summary>
/// Nathan Zumsande
/// Created: 2023/03/22
/// 
/// Presentation layer methods for the Add Resource Item Page
/// </summary>
///
/// <remarks>
/// Zaid Rachman
/// Updated: 2023/04/21
/// Final QA
/// </remarks>
using DataObjects;
using LogicLayer;
using LogicLayerInterfaces;
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
using WpfPresentation.Management.Inventory.Library;

namespace WpfPresentation.Management.Inventory
{
    /// <summary>
    /// Interaction logic for AddResourceItemPage.xaml
    /// </summary>
    public partial class AddResourceItemPage : Page
    {

        private IItemManager _itemManager = null;
        private LibraryUI _libraryUI = null;
        private Item _item = null;
        private bool _edit = false;

        public AddResourceItemPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Nathan Zumsande
        /// Created: 2023/03/22
        /// 
        /// Initalizes the Add Resource Item Page
        /// </summary>
        ///
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/21
        /// Final QA
        /// </remarks>
        /// <param name="itemManager"></param>
        /// <param name="libraryUI"></param>
        public AddResourceItemPage(IItemManager itemManager, LibraryUI libraryUI)
        {
            _itemManager = itemManager;
            _libraryUI = libraryUI;
            InitializeComponent();
        }

        /// <summary>
        /// Nathan Zumsande
        /// Created: 2023/03/22
        /// 
        /// Initalizes the Add Resource Item Page for the edit mode
        /// </summary>
        ///
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/21
        /// Final QA
        /// </remarks>
        /// <param name="itemManager"></param>
        /// <param name="item"></param>
        /// <param name="libraryUI"></param>
        public AddResourceItemPage(IItemManager itemManager, Item item, LibraryUI libraryUI)
        {
            _itemManager = itemManager;
            _item = item;
            _libraryUI = libraryUI;
            _edit = true;
            InitializeComponent();
        }


        /// <summary>
        /// Nathan Zumsande
        /// Created: 2023/03/22
        /// 
        /// Method to add a Resource Item of Edit Resource Item
        /// </summary>
        ///
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/21
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddResource_Click(object sender, RoutedEventArgs e)
        {
            List<string> categories = new List<string>();
            string itemId = txtname.Text;
            bool result = false;

            foreach (var c in cmbcategory.SelectedItems)
            {
                categories.Add(c.ToString());
            }

            if (itemId == "" || itemId == null)
            {
                PromptWindow.ShowPrompt("Bad Input", "You must enter a Resource Name.", ButtonMode.Ok);
                txtname.Focus();
                return;
            }
            if (itemId.Length > 50)
            {
                PromptWindow.ShowPrompt("Bad Input", "Resource Name cannot be more than 50 characters", ButtonMode.Ok);
                txtname.Focus();
                return;
            }
            try
            {
                if (!_edit) { 
                    result = _itemManager.AddItem(itemId);
                    if (result)
                    {
                        foreach (string c in categories)
                        {
                            result = _itemManager.AddItemCategory(itemId, c);
                        }
                    }
                }
                else
                {
                    foreach (string c in categories)
                    {
                        if (!_item.CategoryId.Contains(c))
                        {
                            //add
                            result = _itemManager.AddItemCategory(itemId, c);
                        }
                    }
                    foreach (string c in _item.CategoryId)
                    {
                        if (!categories.Contains(c))
                        {
                            //remove
                            result = _itemManager.RemoveItemCategory(itemId, c);
                        }
                    }
                }
                if (result)
                {
                    _libraryUI.RefreshLibraryList();
                    NavigationService.Navigate(null);
                }
            }
            catch (Exception ex)
            {
                PromptWindow.ShowPrompt("Error", ex.Message, ButtonMode.Ok);
                _libraryUI.RefreshLibraryList();
                NavigationService.Navigate(null);
            }

        }

        /// <summary>
        /// Nathan Zumsande
        /// Created: 2023/03/22
        /// 
        /// Method to cancel the Add Resource Item
        /// </summary>
        ///
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/21
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            PromptSelection selection = PromptWindow.ShowPrompt("Cancel?", "Are you sure you wish to cancel? Changes will not be saved.", ButtonMode.YesNo);
            if (selection == PromptSelection.Yes)
            {
                _libraryUI.RefreshLibraryList();
                NavigationService.Navigate(null);
            }
        }

        /// <summary>
        /// Nathan Zumsande
        /// Created: 2023/03/22
        /// 
        /// Gets all categories when the page is loaded in
        /// order to set the categories in the CheckComboBox.
        /// If in edit mode then it also sets the labels, textbox, 
        /// and preselects the categories of the passed item.
        /// </summary>
        ///
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/21
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddResourceItem_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> categories = new List<string>();
            List<string> temp = new List<string>();
            try
            {
                categories = _itemManager.RetrieveAllCategories();
                cmbcategory.ItemsSource = categories;
                if (_edit)
                {
                    lbltitle.Content = "Edit Resource";
                    txtname.Text = _item.ItemId;
                    txtname.IsEnabled = false;
                    foreach (string c in _item.CategoryId)
                    {
                        temp.Add(c);
                    }
                    cmbcategory.SelectedItemsOverride = temp;
                    btnAddResource.Content = "Save Edit";
                }
            }
            catch (Exception ex)
            {
                PromptWindow.ShowPrompt("Error", "Failed to get Categories" + "\n" + ex.Message, ButtonMode.Ok);
                _libraryUI.RefreshLibraryList();
                NavigationService.Navigate(null);
            }
        }
    }
}
