/// <summary>
/// Ethan Kline 
/// Created: 2023/02/18
/// 
/// Interaction logic for Edit_Medical_Notes.xaml
/// 
/// </summary>
/// <remarks>
/// Zaid Rachman
/// Updated: 2023/04/17
/// Final QA
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

namespace WpfPresentation.Animals
{
    /// <summary>
    /// Interaction logic for Edit_Medical_Notes.xaml
    /// </summary>
    /// <remarks>
    /// Zaid Rachman
    /// Updated: 2023/04/17
    /// Final QA
    /// </remarks>
    public partial class Edit_Medical_Notes : Page
    {
        private MasterManager _manager;
        private MedicalRecordVM _MedicalRecordVM = null;
        private bool add;
        private Animal _animal;

        public Edit_Medical_Notes()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Ethan Kline
        /// Created: 2023/03/2
        /// 
        /// Constructor that is used when a Medicalnote is being edited
        /// 
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/17
        /// Final QA
        /// </remarks>
        /// <param name="medicalRecord">the medicalRecord that will be overwriten</param>
        /// <param name="manager">the MasterManager being used through out the program</param>
        public Edit_Medical_Notes(MedicalRecordVM medicalRecord, MasterManager manager)
        {
            InitializeComponent();
            _MedicalRecordVM = medicalRecord;
            _manager = manager;
            add = false;
        }
        /// <summary>
        /// Ethan Kline
        /// 2023/03/10
        /// Constructor that is used when we add a Medicalnote 
        /// 
        /// 
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/17
        /// Final QA
        /// </remarks>
        /// <param name="animal"></param>
        /// <param name="manager"></param>
        public Edit_Medical_Notes(Animal animal, MasterManager manager)
        {
            InitializeComponent();
            add = true;
            _animal = animal;
            _manager = manager;
        }

        /// <summary>
        /// Ethan Kline
        /// 2023/03/10
        /// on load if we are editing call the populate
        /// 
        /// 
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/17
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (add == false)
            {
                Populate();
            }
        }
        /// <summary>
        /// Ethan Kline
        /// 2023/03/10
        /// if we are editing fill in the note
        /// 
        /// 
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/17
        /// Final QA
        /// </remarks>
        private void Populate()
        {
            if (_MedicalRecordVM != null)
            {
                txtMedicalRecords.Text = _MedicalRecordVM.MedicalNotes.ToString();
            }
        }
        /// <summary>
        /// Ethan Kline
        /// Created: 2023/02/18
        /// 
        /// When the save buttton is clicked, the input on the edit medical notes page is cheked. 
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/17
        /// Final QA
        /// </remarks>
        /// 
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditSave_Click(object sender, RoutedEventArgs e)
        {
            if (add == false)
            {
                MedicalRecord medicalRecord = new MedicalRecord()
                {
                    AnimalId = _MedicalRecordVM.AnimalId,
                    MedicalNotes = txtMedicalRecords.Text,
                    IsProcedure = _MedicalRecordVM.IsProcedure,
                    IsTest = _MedicalRecordVM.IsTest,
                    IsVaccination = _MedicalRecordVM.IsVaccination,
                    IsPrescription = _MedicalRecordVM.IsPrescription,
                    Images = _MedicalRecordVM.Images,
                    QuarantineStatus = _MedicalRecordVM.QuarantineStatus,
                    Diagnosis = _MedicalRecordVM.Diagnosis,
                    MedicalRecordId = _MedicalRecordVM.MedicalRecordId
                };

                try
                {
                    if (_manager.MedicalRecordManager.EditMedicalRecord(_MedicalRecordVM, medicalRecord))
                    {
                        NavigationService.GoBack();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Note failed to save.", ex.Message);
                }
            }
            else
            {
                MedicalRecord medicalRecord = new MedicalRecord()
                {
                    AnimalId = _animal.AnimalId,
                    MedicalNotes = txtMedicalRecords.Text,
                    Diagnosis = "we need a Diagnosis!"
                };

                try
                {
                    if (_manager.MedicalRecordManager.AddMedicalNote(medicalRecord))
                    {
                        NavigationService.GoBack();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Note failed to save.", ex.Message);
                }
            }
        }
        /// <summary>
        /// Ethan Kline
        /// 2023/03/10
        /// if this is clicked go back to the medical note page
        /// 
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/17
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
