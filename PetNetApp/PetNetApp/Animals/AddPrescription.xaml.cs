/// <summary>
/// Tyler hand 
/// 2023/03/25
/// </summary>
/// <remarks>
/// Oleksiy Fedchuk
/// Updated : 2023/04/28
///  Final QA
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
    /// Interaction logic for AddPresciption.xaml
    /// </summary>
    public partial class AddPrescription : Window
    {
        private MasterManager _masterManager = MasterManager.GetMasterManager();
        private int animalId;
        private Prescription prescription = new Prescription();

        /// <summary>
        /// Tyler hand 
        /// 2023/03/25
        /// 
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated : 2023/04/28
        ///  Final QA
        /// </remarks>
        /// <param name="animalId"></param>
        public AddPrescription(int animalId)
        {
            prescription.UserId = _masterManager.User.UsersId;
            try
            {
                prescription.MedicalRecordId = _masterManager.MedicalRecordManager.RetrieveLastMedicalRecordIdByAnimalId(animalId);
            }
            catch (Exception ex)
            {
                PromptWindow.ShowPrompt("Error ", "Medical Record not found.", ButtonMode.Ok);
                this.Close();
               
            }
           
            this.animalId = animalId;
            InitializeComponent();
        }
        /// <summary>
        /// Tyler hand 
        /// 2023/03/25
        /// 
        /// this is to close the window 
        /// 
        /// </summary>
        /// /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated : 2023/04/28
        ///  Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// /// Tyler hand 
        /// 2023/03/25
        ///  
        /// this is the button to save the new prescription
        /// 
        /// 
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated : 2023/04/28
        ///  Final QA
        /// </remarks>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            bool suscessful = false;
            try
            {


                if (txtAddPresciptionName.Text == null || txtAddPresciptionName.Text == "")
                {
                    PromptWindow.ShowPrompt("Missing Presciption Name", "You need to enter a Presciption name.", ButtonMode.Ok);
                    txtAddPresciptionName.Focus();
                    return;
                }
                if (txtAddDosage.Text == null || txtAddDosage.Text == "")
                {
                    PromptWindow.ShowPrompt("Missing Dosage ", "You need to enter a Dosage.", ButtonMode.Ok);
                    txtAddDosage.Focus();
                    return;
                }
                if (txtAddFrequency.Text == null || txtAddFrequency.Text == "")
                {
                    PromptWindow.ShowPrompt("Missing Frequency ", "You need to enter a Frequency.", ButtonMode.Ok);
                    txtAddFrequency.Focus();
                    return;
                }
                if (txtAddNumDays.Text == null || txtAddNumDays.Text == "")
                {
                    PromptWindow.ShowPrompt("Missing Number of days ", "You need to enter a Number of days.", ButtonMode.Ok);
                    txtAddNumDays.Focus();
                    return;
                }
                if (txtPrescriptionTypeId.Text == null || txtPrescriptionTypeId.Text == "")
                {
                    PromptWindow.ShowPrompt("Missing Prescription Type ", "You need to enter a  Prescription Type.", ButtonMode.Ok);
                    txtPrescriptionTypeId.Focus();
                    return;
                }
                if (txtNotesUpdate.Text == null || txtNotesUpdate.Text == "")
                {
                    PromptWindow.ShowPrompt("Missing Notes ", "You need to enter a  Notes.", ButtonMode.Ok);
                    txtNotesUpdate.Focus();
                    return;
                }
                if (dpickerDatePrescribed.Text == null || dpickerDatePrescribed.Text == "")
                {
                    PromptWindow.ShowPrompt("Missing Date Prescribed ", "You need to enter a Date Prescribed.", ButtonMode.Ok);
                    dpickerDatePrescribed.Focus();
                    return;
                }
                if (dpickerEndDate.Text == null || dpickerEndDate.Text == "")
                {
                    PromptWindow.ShowPrompt("Missing End Date ", "You need to enter a End Date.", ButtonMode.Ok);
                    dpickerEndDate.Focus();
                    return;
                }



                prescription.PrescriptionName = txtAddPresciptionName.Text;
                prescription.PrescriptionDosage = txtAddDosage.Text;
                prescription.PrescriptionFrequency = txtAddFrequency.Text;
                prescription.PrescriptionDuration = Int32.Parse(txtAddNumDays.Text);
                prescription.PrescriptionTypeId = txtPrescriptionTypeId.Text;
                prescription.PrescriptionNotes = txtNotesUpdate.Text;
                prescription.DatePrescribed = (DateTime)dpickerDatePrescribed.SelectedDate;
                prescription.EndDate = (DateTime)dpickerEndDate.SelectedDate;



                suscessful = _masterManager.PrescriptionManager.AddPresciptionByMedicalRecordId(prescription, prescription.MedicalRecordId);


                this.DialogResult = true;



            }
            catch (Exception ex)
            {
                PromptWindow.ShowPrompt("Can not save file", "Could not save the file.  " + ex.Message, ButtonMode.Ok);
                this.DialogResult = false;
            }
           
        }
    }
}
