/// <summary>
/// Nathan Zumsande
/// Created: 2023/04/06
/// 
/// Presentation layer methods for the Add Category Tag Page
/// </summary>
///
/// <remarks>
/// Oleksiy Fedchuk
/// Updated: 2023/04/13
/// 
/// FinalQA
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
    /// <summary>
    /// Interaction logic for AddCategoryTagPage.xaml
    /// </summary>
    public partial class AddCategoryTagPage : Page
    {

        MasterManager _masterManager = null;
        LibraryUI _libraryUI = null;

        /// <summary>
        /// Nathan Zumsande
        /// Created: 2023/04/06
        /// 
        /// Initalizes the Add Category Tag Page
        /// </summary>
        ///
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/13
        /// 
        /// FinalQA
        /// </remarks>
        /// <param name="masterManager"></param>
        /// <param name="libraryUI"></param>
        public AddCategoryTagPage(MasterManager masterManager, LibraryUI libraryUI)
        {
            _masterManager = masterManager;
            _libraryUI = libraryUI;
            InitializeComponent();
        }


        /// <summary>
        /// Nathan Zumsande
        /// Created: 2023/04/06
        /// 
        /// Gets all categories when the page is loaded in
        /// order to set the categories in the DataGrid
        /// </summary>
        ///
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/13
        /// 
        /// FinalQA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddCategorytag_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> categories = new List<string>();
            try
            {
                categories = _masterManager.ItemManager.RetrieveAllCategories();
                if(categories == null)
                {
                    lblcategory.Content = "No categories found";
                    datCategory.Visibility = Visibility.Hidden;
                }
                else
                {
                    datCategory.ItemsSource = categories;
                }
                
            }
            catch (Exception ex)
            {
                PromptWindow.ShowPrompt("Error", "Failed to get Categories" + "\n" + ex.Message, ButtonMode.Ok);
                _libraryUI.RefreshLibraryList();
                NavigationService.Navigate(null);
            }
        }

        /// <summary>
        /// Nathan Zumsande
        /// Created: 2023/04/06
        /// 
        /// Method to add a Category 
        /// </summary>
        ///
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/13
        /// 
        /// FinalQA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddCategory_Click(object sender, RoutedEventArgs e)
        {
            string categoryId = txtname.Text;
            bool result = false;
            if (categoryId == "" || categoryId == null)
            {
                PromptWindow.ShowPrompt("Bad Input", "You must enter a Category Name.", ButtonMode.Ok);
                txtname.Focus();
                return;
            }
            if (categoryId.Length > 50)
            {
                PromptWindow.ShowPrompt("Bad Input", "Category Name cannot be more than 50 characters", ButtonMode.Ok);
                txtname.Focus();
                return;
            }
            if (datCategory.Items.Contains(categoryId))
            {
                PromptWindow.ShowPrompt("Bad Input", "Category is already in the category list", ButtonMode.Ok);
                txtname.Focus();
                datCategory.SelectedItem = categoryId;
                return;
            }
            try
            {
                result = _masterManager.ItemManager.AddCategory(categoryId);
                if (result)
                {
                    _libraryUI.RefreshLibraryList();
                    NavigationService.Navigate(null);
                }
                else
                {
                    throw new ApplicationException("Category addition failed");
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
        /// Created: 2023/04/06
        /// 
        /// Method to cancel the Add Category Tag
        /// </summary>
        ///
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/13
        /// 
        /// FinalQA
        /// </remarks>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            PromptSelection selection = PromptWindow.ShowPrompt("Cancel?", "Are you sure you wish to cancel? Changes will not be saved.", ButtonMode.YesNo);
            if (selection == PromptSelection.Yes)
            {
                _libraryUI.RefreshLibraryList();
                NavigationService.Navigate(null);
            }
        }

    }
}
