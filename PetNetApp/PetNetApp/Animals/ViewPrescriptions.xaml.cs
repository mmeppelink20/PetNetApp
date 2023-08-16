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
    /// Interaction logic for ViewPrescriptions.xaml
    ///  /// <summary>
    /// Tyler hand 
    /// Created: 2023/03/16
    /// 
    /// </summary>
    ///  <remarks>
    /// Oleksiy Fedchuk
    /// Updated : 2023/04/28
    ///  Final QA
    /// </remarks>
    public partial class ViewPrescriptions : Page
    {
        private MasterManager _masterManager = MasterManager.GetMasterManager();
        private int animalId;


        /// <summary>
        /// /// Tyler hand 
        /// 2023/03/25
        /// 
        /// this is the logic for the viewing of prescriptions
        /// </summary>
        ///  <remarks>
        /// Oleksiy Fedchuk
        /// Updated : 2023/04/28
        ///  Final QA
        /// </remarks>
        /// <param name="animalId"></param>
        public ViewPrescriptions(int animalId)
        {
            
            this.animalId = animalId;
            this.DataContext = this;
            InitializeComponent();
        }
        /// <summary>
        /// /// Tyler hand 
        /// 2023/03/25
        /// 
        /// this is the logic for the Loading of prescriptions
        /// </summary>
        ///  remarks>
        /// Oleksiy Fedchuk
        /// Updated : 2023/04/28
        ///  Final QA
        /// </remarks>
        /// <param name="animalId"></param>
        private void LoadPrescriptions()
        {
            
            try
            {
                dataPrescriptions.ItemsSource = _masterManager.PrescriptionManager.RetrievePrescriptions(animalId);
            }
            catch (Exception ex)
            {

                PromptWindow.ShowPrompt("Error", "Cannot get the data. \n\n" + ex.Message);
            }
        }
        /// <summary>
        /// /// Tyler hand 
        /// 2023/03/25
        /// 
        /// this is the logic for the Loaded prescriptions 
        /// </summary>
        ///   <remarks>
        /// Oleksiy Fedchuk
        /// Updated : 2023/04/28
        ///  Final QA
        /// </remarks>
        /// <param name="animalId"></param>
        private void Prescriptions_loaded(object sender, RoutedEventArgs e)
        {
            LoadPrescriptions();
           
        }
        /// <summary>
        /// /// Tyler hand 
        /// 2023/03/25
        /// 
        /// this is the logic for the click to add a new  prescription
        /// </summary>
        ///  <remarks>
        /// Oleksiy Fedchuk
        /// Updated : 2023/04/28
        ///  Final QA
        /// </remarks>
        /// <param name="animalId"></param>
        private void btnAddPresciption_Click(object sender, RoutedEventArgs e)
        {
           


            AddPrescription addPrescription = new AddPrescription(animalId);
            addPrescription.Owner = Window.GetWindow(this);
            addPrescription.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            
           bool results  =  (bool)addPrescription.ShowDialog();
            if (results  )
            {
                NavigationService.Navigate(new ViewPrescriptions(animalId));
            }


        

        }
    }
}
