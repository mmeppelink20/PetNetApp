/// <summary>
/// Andrew Cromwell
/// Created: 2023/02/08
/// 
/// Interaction logic for MedProcedurePage.xaml
/// </summary>
/// <remarks>
/// Zaid Rachman
/// Updated: 2023/04/27
/// 
/// Final QA
/// </remarks>

using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for MedProcedurePage.xaml
    /// </summary>
    public partial class MedProcedurePage : Page
    {
        private Animal _procedureAnimal;
        private MasterManager _manager;
        private List<ProcedureVM> _procedures;
        
        public MedProcedurePage(Animal animal, MasterManager manager)
        {
            InitializeComponent();
            _procedureAnimal = animal;
            _manager = manager;
            DisplayProcedureAnimalId();
            
            
        }

        /// <summary>
        /// Andrew Cromwell
        /// Created: 2023/02/16
        /// 
        /// Puts the columns of the procedure datagrid in the correct order if there are procedures
        /// for the animal, displays "no procedure records available" if there are no procedures for 
        /// the animal.
        /// </summary>
        /// <remarks>
        /// Andrew Cromwell
        /// Updated: 2023/04/21
        /// 
        /// Reformated datagrid to look better at the request of final QA Team
        /// 
        /// Zaid Rachman
        /// Updated: 2023/04/27
        /// 
        /// Final QA
        /// </remarks>
        /// 
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            datMedProcedure.ItemsSource = null;
            try
            {
                _procedures = _manager.ProcedureManager.RetrieveProceduresByAnimalId(_procedureAnimal.AnimalId);
            } catch(Exception ex)
            {
                PromptWindow.ShowPrompt("An Error occurred", ex.Message + "\n" + ex.InnerException, ButtonMode.Ok);
                _procedures = new List<ProcedureVM>();
            }

            datMedProcedure.ItemsSource = _procedures;
                    
        }
        /// <summary>
        /// Andrew Cromwell
        /// Created: 2023/02/16
        /// 
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/27
        /// 
        /// Final QA
        /// </remarks>
        private void DisplayProcedureAnimalId()
        {
            lblProcedureAnimalId.Content = "Animal ID #" + _procedureAnimal.AnimalId;
        }

        /// <summary>
        /// Andrew Cromwell
        /// Created: 2023/02/16
        /// 
        /// Navigates to the page for adding a medical procedure.
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/27
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddMedProcedure_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EditProcedurePage(_procedureAnimal, _manager));
        }

        /// <summary>
        /// Andrew Cromwell
        /// Created: 2023/02/16
        /// 
        /// Returns to the previous page.
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/27
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMedProcedureCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        /// <summary>
        /// Andrew Cromwell
        /// Created: 2023/02/16
        /// 
        /// If a procedure is selected, the page to edit it is brought up.
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/27
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void datMedProcedure_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ProcedureVM selecteProcedure = (ProcedureVM)datMedProcedure.SelectedItem;
            if(selecteProcedure == null)
            {
                NavigationService.Navigate(new EditProcedurePage(_procedureAnimal, _manager));
            }
            else
            {
                NavigationService.Navigate(new EditProcedurePage(selecteProcedure, _manager));
            }
            
        }
    }
}
