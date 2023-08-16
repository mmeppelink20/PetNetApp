/// <summary>
/// Nathan Zumsande
/// Created: 2023/01/31
/// 
/// Presentation layer methods for the Edit Quarantine Page
/// </summary>
///
///  <remarks>
/// Zaid Rachman
/// Updated: 2023/04/21
/// Final QA
/// </remarks>
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
using DataObjects;
using LogicLayer;
using WpfPresentation.Animals;
using WpfPresentation.Animals.Medical;

namespace WpfPresentation.Animals
{
    /// <summary>
    /// Interaction logic for QuarantinePage.xaml
    /// 
    /// 
    /// </summary>
    /// <remarks>
    /// Zaid Rachman
    /// Updated: 2023/04/21
    /// Final QA
    /// </remarks>
    public partial class QuarantinePage : Page
    {


        private int _medicalRecordID;
        private bool _quarantineStatus;
        private bool _oldQuarantineStatus;
        private MedicalRecordManager _medicalRecordManager = null;
        private MedicalTreatmentPage _medicalTreatmentPage = null;


        /// <summary>
        /// Nathan Zumsande
        /// Created: 2023/01/31
        /// 
        /// Initalization of the Edit Quarantine Page
        /// 
        /// </summary>
        ///
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/21
        /// Final QA
        /// </remarks>
        /// <param name="medicalRecord"></param>
        /// <param name="medicalRecordManager"></param>
        /// <param name="medicalTreatmentPage"></param>
        public QuarantinePage(MedicalRecord medicalRecord, MedicalRecordManager medicalRecordManager, MedicalTreatmentPage medicalTreatmentPage)
        {
            _medicalRecordID = medicalRecord.MedicalRecordId;
            _oldQuarantineStatus = medicalRecord.QuarantineStatus;
            _medicalRecordManager = medicalRecordManager;
            _medicalTreatmentPage = medicalTreatmentPage;

            InitializeComponent();
        }
        /// <summary>
        /// Nathan Zumsande
        /// Created: 2023/01/31
        /// 
        /// Empty constructor
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/21
        /// Final QA
        /// </remarks>
        public QuarantinePage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Nathan Zumsande
        /// Created: 2023/01/31
        /// 
        /// When page is loaded sets the check box to checked
        /// based on whether or not the QuarantineStatus is true
        /// in the passed in medical record
        /// </summary>
        ///
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/21
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QuarantinePage_Loaded(object sender, RoutedEventArgs e)
        {
            // sets check box 
            if (_oldQuarantineStatus == true)
            {
                chkQuarantineStatus.IsChecked = true;
            }
            else
            {
                chkQuarantineStatus.IsChecked = false;
            }
        }

        /// <summary>
        /// Nathan Zumsande
        /// Created: 2023/01/31
        /// 
        /// Method that Edits the Quarantine Status
        /// </summary>
        ///
        ///  <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/21
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            _quarantineStatus = (bool)chkQuarantineStatus.IsChecked;
            try
            {
                if (_medicalRecordManager.EditQuarantineStatusByMedicalRecordId(_medicalRecordID, _quarantineStatus, _oldQuarantineStatus))
                {
                    NavigationService.Navigate(null);
                    _medicalTreatmentPage.RefreshPage();
                }

                // prompt window
            }
            catch (Exception ex)
            {
                PromptWindow.ShowPrompt("Update Failed", "Update Failed" + "\n" + ex.Message, ButtonMode.Ok);
                NavigationService.Navigate(null);
                _medicalTreatmentPage.RefreshPage();
            }
        }

        /// <summary>
        /// Nathan Zumsande
        /// Created: 2023/01/31
        /// 
        /// Method that cancels the Edit Quarantine Status
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

            PromptSelection selection = PromptWindow.ShowPrompt("Cancel Update?", "This will cancel the Quarantine Update. Changes will not be saved.", ButtonMode.YesNo);
            if (selection == PromptSelection.Yes)
            {
                NavigationService.Navigate(null);
            }
        }
    }
}
