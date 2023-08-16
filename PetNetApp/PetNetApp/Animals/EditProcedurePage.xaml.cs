/// <summary>
/// Andrew Cromwell
/// Created: 2023/02/08
/// 
/// Interaction logic for EditProcedurePage.xaml
/// </summary>

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

namespace WpfPresentation.Animals
{
    /// <summary>
    /// Andrew Cromwell
    /// Created: 2023/02/08
    /// 
    /// Interaction logic for EditProcedurePage.xaml
    /// </summary>
    /// 
    /// <remarks>
    /// Zaid Rachman
    /// Updated: 2023/04/27
    /// 
    /// Final QA
    /// </remarks>
    public partial class EditProcedurePage : Page
    {
        private Animal _medProcedureAnimal;
        private MasterManager _manager;
        private bool _forAdd;
        private ProcedureVM _oldProcedure;

        /// <summary>
        /// Andrew Cromwell
        /// Created: 2023/02/08
        /// 
        /// Constructor that is used when a new procedure is being added
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/27
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="animal">the animal that recieved the procedure</param>
        /// <param name="manager">the MasterManager being used through out the program</param>
        public EditProcedurePage(Animal animal, MasterManager manager)
        {
            InitializeComponent();
            _forAdd = true;
            _medProcedureAnimal = animal;
            _manager = manager;            
            lblEditProcedure.Content = "Add Procedure";
            dateProcedurePerformed.DisplayDateEnd = DateTime.Today;
        }

        /// <summary>
        /// Andrew Cromwell
        /// Created: 2023/02/16
        /// 
        /// Constructor that is used when a procedure is being edited
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/27
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="oldProcedure">the procedure that will be overwriten</param>
        /// <param name="manager">the MasterManager being used through out the program</param>
        public EditProcedurePage(ProcedureVM oldProcedure, MasterManager manager)
        {
            InitializeComponent();
            _forAdd = false;
            _oldProcedure = oldProcedure;
            _manager = manager;
            dateProcedurePerformed.DisplayDateEnd = DateTime.Today;
            txtProcedureName.Text = _oldProcedure.ProcedureName;
            dateProcedurePerformed.SelectedDate = _oldProcedure.ProcedureDate;
            txtProcedureMedsAdministered.Text = _oldProcedure.MedicationsAdministered;
            txtProcedureNotes.Text = _oldProcedure.ProcedureNotes;
        }

        /// <summary>
        /// Andrew Cromwell
        /// Created: 2023/02/08
        /// 
        /// When the save buttton is clicked, the input on the edit procedure page is cheked,
        /// and if it is acceptable it is saved. If it is a new procedure being added,
        /// the medical record id of the animal's last medical record is used. If the animal
        /// does not have a MedicalRecord one will be created for it
        /// </summary>
        /// 
        /// <remarks>
        /// Andrew Cromwell
        /// Updated: 2023/02/16 
        /// Added logic to handle editing an existing procedure record
        /// 
        /// Zaid Rachman
        /// Updated: 2023/04/27
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMedProcedureEditSave_Click(object sender, RoutedEventArgs e)
        {
            if(txtProcedureName.Text == null || txtProcedureName.Text == "")
            {
                PromptWindow.ShowPrompt("Missing Procedure Name", "You need to enter a procedure name.", ButtonMode.Ok);
                txtProcedureName.Focus();
                return;
            }

            if(dateProcedurePerformed.SelectedDate == null)
            {
                PromptWindow.ShowPrompt("Missing Procedure Date", "You need to select the date the procedure was performed.", ButtonMode.Ok);
                dateProcedurePerformed.Focus();
                return;
            }

            string prompt = "Verify that the information you entered is correct.";
            if (!_forAdd)
            {
                prompt += "\nThis will overwrite the existing procedure information";
            }
            PromptSelection selection = PromptWindow.ShowPrompt("Verify Input", prompt, ButtonMode.SaveCancel);
            if(selection == PromptSelection.Cancel)
            {
                return;
            }

            Procedure procedure = new Procedure();
            if (_forAdd)
            {
                try
                {
                    procedure.MedicalRecordId = _manager.MedicalRecordManager.RetrieveLastMedicalRecordIdByAnimalId(_medProcedureAnimal.AnimalId);
                } 
                catch (Exception ex)
                {
                    try
                    {
                        MedicalRecordVM medicalRecord = new MedicalRecordVM() { AnimalId = _medProcedureAnimal.AnimalId };
                        procedure.MedicalRecordId = _manager.MedicalRecordManager.AddMedicalRecord(medicalRecord);
                    }catch(Exception ex2)
                    {
                        PromptWindow.ShowPrompt("An Error occurred", ex2.Message + "\n" + ex.InnerException, ButtonMode.Ok);
                    }
                }
            }
            else
            {
                procedure.ProcedureId = _oldProcedure.ProcedureId;
                procedure.MedicalRecordId = _oldProcedure.MedicalRecordId;
            }
            procedure.UserId = _manager.User.UsersId;
            procedure.ProcedureName = txtProcedureName.Text;
            procedure.ProcedureDate = (DateTime)dateProcedurePerformed.SelectedDate;
            procedure.MedicationsAdministered = txtProcedureMedsAdministered.Text;
            procedure.ProcedureNotes = txtProcedureNotes.Text;
            if (_forAdd)
            {
                try
                {
                    bool success = _manager.ProcedureManager.AddProcedureByMedicalRecordId(procedure, procedure.MedicalRecordId);
                    if (success)
                    {
                        PromptWindow.ShowPrompt("Success", "The procedure was saved.", ButtonMode.Ok);
                        NavigationService.GoBack();
                    }
                    else
                    {
                        PromptWindow.ShowPrompt("Failure", "The procedure was not saved.", ButtonMode.Ok);
                        return;
                    }
                } 
                catch(Exception ex)
                {
                    PromptWindow.ShowPrompt("An Error occurred", ex.Message + "\n" + ex.InnerException, ButtonMode.Ok);
                    return;
                }                
            }
            else
            {
                try
                {
                    bool success = _manager.ProcedureManager.EditProcedureByProcedureId(procedure, _oldProcedure);
                    if (success)
                    {
                        PromptWindow.ShowPrompt("Success", "The procedure was saved.", ButtonMode.Ok);
                        NavigationService.GoBack();
                    }
                    else
                    {
                        PromptWindow.ShowPrompt("Failure", "The procedure was not saved.", ButtonMode.Ok);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    PromptWindow.ShowPrompt("An Error occurred", ex.Message + "\n" + ex.InnerException, ButtonMode.Ok);
                    return;
                }
            }
            
        }

        /// <summary>
        /// Andrew Cromwell
        /// Created: 2023/02/08
        /// 
        /// closes the page to edit a procedure
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/27
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMedProcedureEditCancel_Click(object sender, RoutedEventArgs e)
        {
            PromptSelection selection = PromptWindow.ShowPrompt("Really Cancel?", "Do you really want to cancel? The data you entered will not be saved.", ButtonMode.YesNo);
            if(selection == PromptSelection.Yes)
            {
                NavigationService.GoBack();
            }
            
        }
    }
}
