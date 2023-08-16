/// <summary>
/// Ethan Kline
/// Created: 2023/03/3
/// 
/// 
/// Interaction logic for Medical_Notes.xaml
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
    /// Interaction logic for Medical_Notes.xaml
    /// </summary>
    public partial class Medical_Notes : Page
    {
        private List<MedicalRecordVM> _MedicalRecords;
        private Animal _medicalnoteAnimal;
        private MasterManager _manager;

        /// <summary>
        /// Ethan Kline
        /// Created: 2023/03/3
        /// 
        /// Constructor that is used when a new medicalnote is being added
        /// 
        /// 
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/17
        /// Final QA
        /// </remarks>
        public Medical_Notes(Animal animal, MasterManager manager)
        {
            InitializeComponent();
            _medicalnoteAnimal = animal;
            _manager = manager;
        }
        /// <summary>
        /// Ethan Kline
        /// Created: 2023/03/3
        /// 
        /// Puts the columns of the medical notes datagrid  in the correct order by animalid
        /// for the animal, displays "No Notes Available" if there are no medicalnotes for 
        /// the animal.
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
            frmMedicalRecords.Content = new MedicalFilesPage(_medicalnoteAnimal, _manager);
            datMedicalRecordGrid.ItemsSource = null;

            try
            {
                _MedicalRecords = _manager.MedicalRecordManager.SelectMedicalRecordByAnimal(_medicalnoteAnimal.AnimalId);
                if (_MedicalRecords.Count != 0)
                {
                    datMedicalRecordGrid.ItemsSource = _MedicalRecords;

                    datMedicalRecordGrid.Columns.RemoveAt(0);
                    datMedicalRecordGrid.Columns.RemoveAt(0);
                    datMedicalRecordGrid.Columns.RemoveAt(0);
                    datMedicalRecordGrid.Columns.RemoveAt(0);
                    datMedicalRecordGrid.Columns.RemoveAt(0);
                    datMedicalRecordGrid.Columns.RemoveAt(0);
                    datMedicalRecordGrid.Columns.RemoveAt(0);
                    datMedicalRecordGrid.Columns.RemoveAt(2);
                    datMedicalRecordGrid.Columns.RemoveAt(2);
                    datMedicalRecordGrid.Columns.RemoveAt(2);
                    datMedicalRecordGrid.Columns.RemoveAt(2);
                    datMedicalRecordGrid.Columns.RemoveAt(2);
                    datMedicalRecordGrid.Columns.RemoveAt(2);
                    datMedicalRecordGrid.Columns.RemoveAt(2);

                }
                else
                {
                    List<string> noRecordMessage = new List<string>();
                    datMedicalRecordGrid.ItemsSource = noRecordMessage;
                    datMedicalRecordGrid.Columns[0].Header = "No Notes Available";
                }
            }
            catch (Exception ex)
            {
                PromptWindow.ShowPrompt("An Error occurred", ex.Message + "\n" + ex.InnerException, ButtonMode.Ok);
            }   
        }

        /// <summary>
        /// Ethan Kline
        /// 2023/03/10
        /// if a row is selected edit the medical note
        /// 
        /// 
        /// </summary>
        /// 
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/17
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void datMedicalRecordGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var MedicalRecordVM = (MedicalRecordVM)datMedicalRecordGrid.SelectedItem;
            NavigationService.Navigate(new Edit_Medical_Notes(MedicalRecordVM, _manager));
        }

        /// <summary>
        /// Molly Meister
        /// 2023/03/27
        ///
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
        private void btn_upload_file_Click(object sender, RoutedEventArgs e)
        {
            var uploadAdditionalFileWindow = new UploadAdditionalFileWindow(_medicalnoteAnimal, _manager);
            uploadAdditionalFileWindow.Owner = Window.GetWindow(this);
            uploadAdditionalFileWindow.ShowDialog();
            NavigationService.Navigate(new Medical_Notes(_medicalnoteAnimal, _manager));
        }

        /// <summary>
        /// Ethan Kline
        /// 2023/03/10
        /// if a row is selected edit the medical note
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
        private void btn_edit_Click(object sender, RoutedEventArgs e)
        {
            if (datMedicalRecordGrid.SelectedItems.Count == 0)
            {
                return;
            }
            var MedicalRecordVM = (MedicalRecordVM)datMedicalRecordGrid.SelectedItem;
            NavigationService.Navigate(new Edit_Medical_Notes(MedicalRecordVM, _manager));
        }

        /// <summary>
        /// Ethan Kline
        /// 2023/03/10
        /// on click go to add a medical note
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
        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Edit_Medical_Notes(_medicalnoteAnimal, _manager));
        }
    }
}
