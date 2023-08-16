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

namespace WpfPresentation.Shelters
{
    /// <summary>
    /// Brian Collum
    /// Created: 2023/02/23
    /// This is the Datagrid UI for CRUD functionality on Shelters
    /// User control version can be added in the future
    /// </summary>
    public partial class ShelterVMListUI : Page
    {
        private static ShelterVMListUI _existingShelterVMListUI = null;
        private MasterManager _masterManager = null;
        private ShelterManager _shelterManager = null;

        private List<Shelter> _shelterList = null;

        /// <summary>
        /// Brian Collum
        /// Created: 2023/02/23
        /// Constructor for the ShelterVM List UI
        /// </summary>
        /// <param name="manager">The MasterManager from the parent UI</param>
        public ShelterVMListUI(MasterManager manager)
        {
            InitializeComponent();
            _masterManager = manager;
            _shelterManager = new ShelterManager();
        }

        /// <summary>
        /// Brian Collum
        /// Created: 2023/02/23
        /// Initialize the shelter List UI
        /// </summary>
        /// <param name="manager">The MasterManager from the parent UI</param>
        public static ShelterVMListUI GetShelterVMListUI(MasterManager manager)
        {
            if (_existingShelterVMListUI == null)
            {
                _existingShelterVMListUI = new ShelterVMListUI(manager);
            }
            return _existingShelterVMListUI;
        }

        /// <summary>
        /// Brian Collum
        /// Created: 2023/02/23
        /// Load the list of shelters on page load
        /// </summary>
        /// <param name="manager">The MasterManager from the parent UI</param>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (_shelterList == null)
            {
                refreshShelterList();
            }
        }

        /// <summary>
        /// Brian Collum
        /// Created: 2023/02/23
        /// Loads a fresh copy of the list of shelters from the database
        /// </summary>
        /// <remarks>
        /// Brian Collum
        /// Updated: 2023/03/24
        /// Moved refreshShelterList() into a try/catch for safety
        /// </remarks>
        private void refreshShelterList()
        {
            try
            {
                _shelterList = _shelterManager.GetShelterList();
                datShelterVMListView.ItemsSource = _shelterList;
            }
            catch (Exception ex)
            {
                PromptWindow.ShowPrompt("Error", ex.Message);
            }
        }
        /// <summary>
        /// Brian Collum
        /// Created: 2023/02/23
        /// Opens an AlterShelter window in View Shelter mode
        /// </summary>
        /// <param name="selectedShelter">Shelter to view</param>
        private void openViewShelterWindow(Shelter selectedShelter)
        {
            if (selectedShelter != null)
            {
                try
                {
                    string windowMode = "viewshelter";
                    ShelterVM selectedShelterVM = _shelterManager.RetrieveShelterVMByShelterID(selectedShelter.ShelterId);
                    var viewShelterWindow = new AlterShelter(_masterManager, selectedShelterVM, windowMode);
                    viewShelterWindow.Owner = Window.GetWindow(this);
                    viewShelterWindow.ShowDialog();
                }
                catch (Exception ex)
                {
                    PromptWindow.ShowPrompt("Error", ex.Message);
                }
            }
        }

        /// <summary>
        /// Brian Collum
        /// Created: 2023/02/23
        /// Opens an AlterShelter window in Add Shelter mode
        /// </summary>
        private void openAddShelterWindow()
        {
            try
            {
                var addShelterWindow = new AlterShelter(_masterManager);
                addShelterWindow.Owner = Window.GetWindow(this);
                addShelterWindow.ShowDialog();
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
                PromptWindow.ShowPrompt("Error", ex.Message);
            }
        }

        /// <summary>
        /// Brian Collum
        /// Created: 2023/02/23
        /// Opens an AlterShelter window in Edit Shelter mode
        /// </summary>
        /// <param name="selectedShelter">Shelter to Edit</param>
        private void openEditShelterWindow(Shelter selectedShelter)
        {
            try
            {
                string windowMode = "editshelter";
                ShelterVM selectedShelterVM = _shelterManager.RetrieveShelterVMByShelterID(selectedShelter.ShelterId);
                var viewShelterWindow = new AlterShelter(_masterManager, selectedShelterVM, windowMode);
                viewShelterWindow.Owner = Window.GetWindow(this);
                viewShelterWindow.ShowDialog();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                PromptWindow.ShowPrompt("Error", ex.Message);
            }
        }

        /// <summary>
        /// Brian Collum
        /// Created: 2023/02/23
        /// Double click event for double clicking the shelter list UI
        /// Opens an AlterShelter window in View Shelter mode
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void datShelterVMListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedShelter = (Shelter)(datShelterVMListView.SelectedItem);
            openViewShelterWindow(selectedShelter);
        }

        /// <summary>
        /// Brian Collum
        /// Created: 2023/02/23
        /// Click event for the Add Shelter button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddShelter_Click(object sender, RoutedEventArgs e)
        {
            openAddShelterWindow();
            refreshShelterList();
        }

        /// <summary>
        /// Brian Collum
        /// Created: 2023/02/23
        /// Context Menu for right-clicking the shelter UI
        /// View selected shelter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextViewShelter_Click(object sender, RoutedEventArgs e)
        {
            if (datShelterVMListView.SelectedItem != null)
            {
                var selectedShelter = (Shelter)(datShelterVMListView.SelectedItem);
                ShelterVM selectedShelterVM = _shelterManager.RetrieveShelterVMByShelterID(selectedShelter.ShelterId);
                // Open shelter detail page
                if (selectedShelterVM != null)
                {
                    openViewShelterWindow(selectedShelterVM);
                }
            }
        }

        /// <summary>
        /// Brian Collum
        /// Created: 2023/02/23
        /// Context Menu for right-clicking the shelter UI
        /// Deactivate selected shelter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextDeactivateShelter_Click(object sender, RoutedEventArgs e)
        {
            if (datShelterVMListView.SelectedItem != null)
            {
                var selectedShelter = (Shelter)(datShelterVMListView.SelectedItem);
                bool deactivateSuccessful = false;
                if (selectedShelter != null)
                {
                    try
                    {
                        deactivateSuccessful = _shelterManager.DeactivateShelter(selectedShelter);
                    }
                    catch (Exception ex)
                    {
                        // MessageBox.Show(ex.Message);
                        PromptWindow.ShowPrompt("Error", ex.Message);
                    }
                }
                if (deactivateSuccessful)
                {
                    // MessageBox.Show(selectedShelter.ShelterName + " deactivated");
                    PromptWindow.ShowPrompt("Shelter deactivated", selectedShelter.ShelterName + " deactivated");
                    refreshShelterList();
                }
            }
        }

        /// <summary>
        /// Brian Collum
        /// Created: 2023/02/23
        /// Context Menu for right-clicking the shelter UI
        /// Edit selected shelter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextEditShelter_Click(object sender, RoutedEventArgs e)
        {
            if (datShelterVMListView.SelectedItem != null)
            {
                var selectedShelter = (Shelter)(datShelterVMListView.SelectedItem);
                ShelterVM selectedShelterVM = _shelterManager.RetrieveShelterVMByShelterID(selectedShelter.ShelterId);
                // Open shelter detail page
                if (selectedShelterVM != null)
                {
                    openEditShelterWindow(selectedShelterVM);
                    refreshShelterList();
                }
            }
        }
    }
}
