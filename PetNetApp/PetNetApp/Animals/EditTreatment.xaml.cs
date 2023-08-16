/// <summary>
/// Matthew Meppelink
/// Created: 2023/02/16
/// 
/// 
/// 
/// </summary>
///
/// <remarks>
/// Zaid Rachman
/// Updated: 2023/04/21
/// Final QA
/// </remarks>
/// 
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
    /// Interaction logic for EditTreatment.xaml
    /// </summary>
    public partial class EditTreatment : Page
    {
        private MedicalTreatmentPage _medicalTreatmentPage = null;
        private MedicalRecordManager _medicalRecordManager = null;
        private MedicalRecord _medicalRecord = null;
        private string _oldDiagnosisName = null;
        private string _oldNotes = null;

        /// <summary>
        /// Matthew Meppelink
        /// Created: 2023/02/16
        /// 
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/21
        /// Final QA
        /// </remarks>
        /// <param name="medicalRecord"></param>
        /// <param name="medicalTreatmentPage"></param>
        public EditTreatment(MedicalRecord medicalRecord, MedicalTreatmentPage medicalTreatmentPage)
        {
            InitializeComponent();
            _medicalRecord = medicalRecord;
            _medicalTreatmentPage = medicalTreatmentPage;
            _medicalRecordManager = new MedicalRecordManager();



            lblUpdateTreatmentName.Content = "Update Diagnosis: " + medicalRecord.Diagnosis.ToString();
            txtDiagnosisUpdate.Text = medicalRecord.Diagnosis.ToString();
            txtNotesUpdate.Text = medicalRecord.MedicalNotes.ToString();

            _oldDiagnosisName = medicalRecord.Diagnosis.ToString();
            _oldNotes = medicalRecord.MedicalNotes.ToString();
        }

        /// <summary>
        /// Matthew Meppelink
        /// Created: 2023/02/16
        /// 
        /// saves the user inputed data and updates the data in the database
        /// 
        /// </summary>
        ///
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/21
        /// Final QA
        /// </remarks>
        /// 
        private void btnSave_click(object sender, RoutedEventArgs e)
        {
            PromptSelection selection = (PromptWindow.ShowPrompt("Update Diagnosis", "Do you want to update this treatment record?", ButtonMode.YesNo));
            if (selection == PromptSelection.Yes)
            {
                try
                {
                    _medicalRecordManager.EditTreatmentByMedicalRecordId(_medicalRecord.MedicalRecordId, txtDiagnosisUpdate.Text.ToString(), txtNotesUpdate.Text.ToString(), _oldDiagnosisName, _oldNotes);
                    _medicalTreatmentPage.RefreshPage();
                }
                catch (Exception ex)
                {
                    PromptWindow.ShowPrompt("Error", ex.Message + "\n" + ex.InnerException.Message, ButtonMode.Ok);
                    _medicalTreatmentPage.RefreshPage();
                }
            }
            else
            {
                return;
            }

            NavigationService.Navigate(null);
        }

        /// <summary>
        /// Matthew Meppelink
        /// Created: 2023/02/16
        /// 
        /// cancels the edit, prompts user to confirm their actino if fields have 
        /// been changed
        /// 
        /// </summary>
        ///
        ///<remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/21
        /// Final QA
        /// </remarks>
        /// 
        private void btnCancel_click(object sender, RoutedEventArgs e)
        {
            if (!_oldDiagnosisName.Equals(txtDiagnosisUpdate.Text) || !_oldNotes.Equals(txtNotesUpdate.Text))
            {
                PromptSelection selection = (PromptWindow.ShowPrompt("Cancel?", "Changes have been made, are you sure you want to cancel?", ButtonMode.YesNo));
                if (selection == PromptSelection.Yes)
                {
                    NavigationService.Navigate(null);
                }
                else
                {
                    return;
                }
            }
            NavigationService.Navigate(null);
        }

    }
}
