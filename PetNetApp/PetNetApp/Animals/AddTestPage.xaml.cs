/// <summary>
/// Nathan Zumsande
/// Created: 2023/02/07
/// 
/// Presentation layer methods for the Add Test Page
/// </summary>
///
///
/// <remarks>
/// Zaid Rachman
/// Updated: 2023/04/21
/// Final QA
/// </remarks>
/// 
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
using DataObjects;
using LogicLayer;
using WpfPresentation.Animals;

namespace WpfPresentation.Animals
{
    /// <summary>
    /// Interaction logic for AddTestPage.xaml
    /// 
    /// <remarks>
    /// Zaid Rachman
    /// Updated: 2023/04/21
    /// Final QA
    /// </remarks>
    /// </summary>
    public partial class AddTestPage : Page
    {
        private int _animalId;
        private MedicalRecord _medicalRecord;
        private int _userId;
        private MedicalRecordManager _medicalRecordManager;
        private TestManager _testManager;

        /// <summary>
        /// Nathan Zumsande
        /// Created: 2023/02/07
        /// 
        /// Empty constructor
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/21
        /// Final QA
        /// </remarks>
        public AddTestPage()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Nathan Zumsande
        /// Created: 2023/02/07
        /// 
        /// Initalization of the Add Test Page with an Animal ID
        /// parameter so it can create a medical record to add the test to
        /// </summary>
        ///
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/21
        /// Final QA
        /// </remarks>
        /// <param name="animalId"></param>
        /// <param name="userId"></param>
        /// <param name="medicalRecordManager"></param>
        /// <param name="testManager"></param>
        public AddTestPage(int animalId, int userId, MedicalRecordManager medicalRecordManager, TestManager testManager)
        {
            _animalId = animalId;
            _userId = userId;
            _medicalRecord = null;
            _medicalRecordManager = medicalRecordManager;
            _testManager = testManager;
            InitializeComponent();
        }


        /// <summary>
        /// Nathan Zumsande
        /// Created: 2023/02/07
        /// 
        /// Initalization of the Add Test Page with a Medical Record
        /// Parameter to add a test to the passed Medical Record instead
        /// of creating a new one
        /// 
        /// NOT CURRENTLY IN USE, MIGHT BE HELPFUL LATER TO ADD MULTIPLE
        /// TESTS TO ONE MEDICALRECORD THAT IS ALREAD MADE
        /// </summary>
        ///
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/21
        /// Final QA
        /// </remarks>
        /// <param name="medicalRecord"></param>
        /// <param name="userId"></param>
        /// <param name="medicalRecordManager"></param>
        /// <param name="testManager"></param>
        public AddTestPage(MedicalRecord medicalRecord, int userId, MedicalRecordManager medicalRecordManager, TestManager testManager)
        {
            _userId = userId;
            _medicalRecord = medicalRecord;
            _medicalRecordManager = medicalRecordManager;
            _testManager = testManager;
            InitializeComponent();
        }


        /// <summary>
        /// Nathan Zumsande
        /// Created: 2023/02/07
        /// 
        /// When page is loaded sets date of the DateTimePicker
        /// to today
        /// </summary>
        ///
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/21
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddTest_Loaded(object sender, RoutedEventArgs e)
        {
            txtDate.Value = DateTime.Now;

        }

        /// <summary>
        /// Nathan Zumsande
        /// Created: 2023/02/07
        /// 
        /// Method for Adding a test
        /// </summary>
        ///
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/21
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddTest_Click(object sender, RoutedEventArgs e)
        {
            Test test = new Test();
            test.TestName = txtTestName.Text;
            test.UserId = _userId;
            test.TestDate = (DateTime)txtDate.Value;
            test.TestAcceptableRange = txtRange.Text;
            test.TestResult = txtResults.Text;
            test.TestNotes = txtNotes.Text;


            if (test.TestName == "")
            {
                PromptWindow.ShowPrompt("Bad Input", "You must enter a Test Name.", ButtonMode.Ok);
                txtTestName.Focus();
                return;
            }
            if (test.TestName.Length > 50)
            {
                PromptWindow.ShowPrompt("Bad Input", "Test Name cannot be more than 50 characters", ButtonMode.Ok);
                txtTestName.Focus();
                return;
            }
            else if (test.TestAcceptableRange.Length > 50)
            {
                PromptWindow.ShowPrompt("Bad Input", "Acceptable Range cannot be more than 50 characters", ButtonMode.Ok);
                txtRange.Focus();
                return;
            }
            if (test.TestResult == "")
            {
                PromptWindow.ShowPrompt("Bad Input", "You must enter a Test Result.", ButtonMode.Ok);
                txtResults.Focus();
                return;
            }
            if (test.TestResult.Length > 50)
            {
                PromptWindow.ShowPrompt("Bad Input", "Test Results cannot be more than 50 characters", ButtonMode.Ok);
                txtResults.Focus();
                return;
            }
            if (test.TestNotes.Length > 500)
            {
                PromptWindow.ShowPrompt("Bad Input", "Test Notes cannot be more than 500 characters", ButtonMode.Ok);
                txtNotes.Focus();
                return;
            }

            try
            {
                int medicalRecordId = 0;
                if (_medicalRecord == null)
                {
                    medicalRecordId = _medicalRecordManager.AddTestMedicalRecordByAnimalId(_animalId, test.TestName, true, test.TestResult);
                }
                else
                {
                    medicalRecordId = _medicalRecord.MedicalRecordId;
                }
                if (_testManager.AddTestByMedicalRecordId(test, medicalRecordId))
                {
                    NavigationService.Navigate(null);
                    AnimalMedicalTestsPage.GetLastViewedAnimalMedicalTestsPage();
                }
            }
            catch (Exception ex)
            {
                PromptWindow.ShowPrompt("Insert Failed", "Insert Failed" + "\n" + ex.Message, ButtonMode.Ok);
                NavigationService.Navigate(null);
                AnimalMedicalTestsPage.GetLastViewedAnimalMedicalTestsPage();
            }

        }

        /// <summary>
        /// Nathan Zumsande
        /// Created: 2023/02/07
        /// 
        /// Method to cancel the Add Test
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
            PromptSelection selection = PromptWindow.ShowPrompt("Cancel Create Test?", "Are you sure you wish to cancel? Changes will not be saved.", ButtonMode.YesNo);
            if (selection == PromptSelection.Yes)
            {
                NavigationService.Navigate(null);
            }
        }
    }
}
