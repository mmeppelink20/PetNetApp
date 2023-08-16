using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
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
    /// Interaction logic for MedicalRecordListPage.xaml
    /// </summary>
    public partial class MedicalRecordListPage : Page
    {
        private Animal _animal = null;
        private MasterManager _manager = MasterManager.GetMasterManager();
        private List<MedicalRecordVM> _medicalRecords = null;
        private List<Images> _images = null;
        private MedicalRecordVM _medicalRecordVM;

        public MedicalRecordListPage(Animal animal, MasterManager masterManager)
        {
            _animal = animal;
            _manager = masterManager;
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            PopulateDataGrid();
        }

        /// <summary>
        /// Molly Meister
        /// Created: 2023/03/09
        /// 
        /// Populates the data grid
        /// </summary>
        public void PopulateDataGrid()
        {
            grdMedRecordDetail.Visibility = Visibility.Hidden;
            if (_medicalRecords == null)
            {
                try
                {
                    _medicalRecords = _manager.MedicalRecordManager.RetrieveAllMedicalRecordsByAnimalId(_animal.AnimalId);
                    if(_medicalRecords.Count > 0)
                    {
                        datMedRecords.ItemsSource = _medicalRecords;
                        lblNoMedRecords.Visibility = Visibility.Hidden;
                    } 
                    else
                    {
                        foreach (var col in datMedRecords.Columns)
                        {
                            col.Visibility = Visibility.Hidden;
                        }
                        datMedRecords.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
                        lblNoMedRecords.Visibility = Visibility.Visible;
                    }

                }
                catch (Exception up)
                {
                    PromptWindow.ShowPrompt("Error", up.Message + "\n\n" + up.InnerException.Message);
                }
            }

        }

        /// <summary>
        /// Molly Meister
        /// Created: 2023/03/09
        /// 
        /// Populates medical record detail grid for selected medical record
        /// </summary>
        /// <param name="medicalRecordId"></param>
        public void DisplayMedicalRecordDetail()
        {
            // series of checks to attempt population for only the fields contained in the MedicalRecordVM to avoid nulls
            if (_medicalRecordVM.IsProcedure)
            {
                try
                {
                    _medicalRecordVM.Procedure = _manager.ProcedureManager.RetrieveProcedureByMedicalRecordId(_medicalRecordVM.MedicalRecordId);
                    txtProc.Text = _medicalRecordVM.Procedure.ProcedureName;
                }
                catch (Exception up)
                {
                    txtProc.Text = "Error Loading Procedure";
                }
            }
            if(_medicalRecordVM.IsTest)
            {
                try
                {
                    _medicalRecordVM.Test = _manager.TestManager.RetrieveTestByMedicalRecordId(_medicalRecordVM.MedicalRecordId);
                    txtTest.Text = _medicalRecordVM.Test.TestName;
                }
                catch (Exception up)
                {
                    // not sure if should show prompt window here, could end up being multiple if more
                    // than one exception in this method, but really it shouldn't get to here before failing
                    txtTest.Text = "Error Loading Test";
                }
            }
            if(_medicalRecordVM.IsVaccination)
            {
                try
                {
                    _medicalRecordVM.Vaccination= _manager.VaccinationManager.RetrieveVaccinationByMedicalRecordId(_medicalRecordVM.MedicalRecordId);
                    txtVaccination.Text = _medicalRecordVM.Vaccination.VaccineName;
                }
                catch (Exception up)
                {
                    txtVaccination.Text = "Error Loading Vaccination";
                }
            }
            if (_medicalRecordVM.IsPrescription)
            {
                // prescriptions not yet implemented, in progress by Tyler
            }
            if(!_medicalRecordVM.MedicalNotes.Equals(""))
            {
                txtNotes.Text = _medicalRecordVM.MedicalNotes;
            }
            if(_medicalRecordVM.Images)
            {
                btnImages.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// Molly Meister
        /// Created: 2023/03/10
        /// 
        /// Resets the detail grid fields and MedicalRecordVM
        /// </summary>
        public void ResetDetailGrid()
        {
            txtTest.Text = "";
            txtVaccination.Text = "";
            txtProc.Text = "";
            txtNotes.Text = "";
            btnImages.Visibility = Visibility.Hidden;
        }



        /// <summary>
        /// Molly Meister
        /// Created: 2023/03/09
        /// 
        /// Gets selected column in the data grid as a MedicalRecordVM and passes it to the DispolayMedicalRecordDetail method
        /// </summary>
        private void datMedRecords_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(grdMedRecordDetail.Visibility == Visibility.Hidden)
            {
                grdMedRecordDetail.Visibility = Visibility.Visible;
            }
            ResetDetailGrid();
            _medicalRecordVM = (MedicalRecordVM)datMedRecords.SelectedItem as MedicalRecordVM;
            DisplayMedicalRecordDetail();
        }

        private void btnImages_Click(object sender, RoutedEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            nav.Navigate(new WpfPresentation.Animals.MedicalFilesPage(_animal, _manager));
        }
    }
}
