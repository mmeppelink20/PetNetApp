///<summary>
///Nathan Zumsande
///Created: 2020/03/11
///
/// Animal Medical Tests Page
///
/// </summary>
/// <remarks>
/// Zaid Rachman
/// Updated: 2023/04/21
/// Final QA
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

namespace WpfPresentation.Animals
{
    /// <summary>
    /// Interaction logic for AnimalMedicalTestsPage.xaml
    /// 
    /// </summary>
    /// <remarks>
    /// Zaid Rachman
    /// Updated: 2023/04/21
    /// Final QA
    /// </remarks>
    public partial class AnimalMedicalTestsPage : Page
    {
        private static AnimalMedicalTestsPage _existingAnimalMedicalTestsPage = null;

        private bool _needsReloaded = true;
        private MasterManager _manager = MasterManager.GetMasterManager();
        private Animal _animal = null;
        /// <summary>
        /// Stephen Jaurigue
        /// Created: 2023/02/12
        /// 
        /// Animal Medical Tests Page Constructor
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/21
        /// Final QA
        /// </remarks>
        private AnimalMedicalTestsPage()
        {
            InitializeComponent();
            this.DataContext = this;
        }
        /// <summary>
        /// Stephen Jaurigue
        /// Created: 2023/02/12
        /// 
        /// 
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/21
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgTests_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SelectTest((Test)dgTests.SelectedItem);
        }

        /// <summary>
        /// Stephen Jaurigue
        /// Created: 2023/02/25
        /// 
        /// </summary>
        /// <remarks>
        /// Stephen Jaurigue: Setup for view test
        /// </remarks>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/21
        /// Final QA
        /// </remarks>
        /// 
        /// <param name="selectedTest">The test currently selected in the datagrid</param>
        private void SelectTest(Test selectedTest)
        {
            if (selectedTest != null)
            {
                // replace with code to navigate to the test here
                PromptWindow.ShowPrompt(selectedTest.TestName, selectedTest.TestNotes);
            }
        }

        /// <summary>
        /// Stephen Jaurigue
        /// Created: 2023/02/23
        /// 
        /// Returns the existing AnimalMedicalTestPage with newly loaded data for the new animal
        /// or a new page if one hasn't been created
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/21
        /// Final QA
        /// </remarks>
        /// <param name="animal">The animal to load data for</param>
        /// <returns></returns>
        public static AnimalMedicalTestsPage GetAnimalMedicalTestsPage(Animal animal)
        {
            if (_existingAnimalMedicalTestsPage == null)
            {
                _existingAnimalMedicalTestsPage = new AnimalMedicalTestsPage();
            }
            _existingAnimalMedicalTestsPage._animal = animal;
            _existingAnimalMedicalTestsPage.LoadAnimalTestData();
            _existingAnimalMedicalTestsPage._needsReloaded = false;
            return _existingAnimalMedicalTestsPage;
        }

        /// <summary>
        /// Stephen Jaurigue
        /// Created: 2023/02/23
        /// 
        /// Gets the previously viewed AnimalMedicalTestsPage
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/21
        /// Final QA
        /// </remarks>
        /// <returns>Previously existing AnimalMedicalTestsPage</returns>
        public static AnimalMedicalTestsPage GetLastViewedAnimalMedicalTestsPage()
        {
            if (_existingAnimalMedicalTestsPage == null)
            {
                _existingAnimalMedicalTestsPage = new AnimalMedicalTestsPage();
            }
            else
            {
                _existingAnimalMedicalTestsPage.LoadAnimalTestData();
                _existingAnimalMedicalTestsPage._needsReloaded = false;
            }
            return _existingAnimalMedicalTestsPage;
        }
        /// <summary>
        /// Stephen Jaurigue
        /// Created: 2023/02/12
        ///
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/21
        /// Final QA
        /// </remarks>
        private void LoadAnimalTestData()
        {
            lblAnimalId.Content = _animal.AnimalId;
            try
            {
                dgTests.ItemsSource = _manager.TestManager.RetrieveTestsByAnimalId(_animal.AnimalId);
            }
            catch (Exception ex)
            {
                PromptWindow.ShowPrompt("Error", ex.InnerException.Message);
            }
        }
        /// <summary>
        /// Nathan Zumsande
        /// Created: 2023/03/11
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/21
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (_animal != null && _needsReloaded)
            {
                LoadAnimalTestData();
                _needsReloaded = false;
            }
            if (!_manager.User.Roles.Contains("Vet") && !_manager.User.Roles.Contains("Admin"))
            {
                btnAddTest.IsEnabled = false;
            }
            if (_manager.User.Roles.Contains("Vet") || _manager.User.Roles.Contains("Admin"))
            {
                btnAddTest.IsEnabled = true;
            }

        }
        /// <summary>
        /// Stephen Jaurigue
        /// Created: 2023/02/12
        ///
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/21
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            _needsReloaded = true;
        }

        /// <summary>
        /// Nathan Zumsande
        /// Created: 2023/03/11
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/21
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddTest_Click(object sender, RoutedEventArgs e)
        {
            frmTests.Navigate(new AddTestPage(_animal.AnimalId, _manager.User.UsersId, (MedicalRecordManager)_manager.MedicalRecordManager, (TestManager)_manager.TestManager));
        }
    }
}
