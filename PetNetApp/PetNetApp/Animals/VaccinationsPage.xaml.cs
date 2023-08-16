/// <summary>
/// Zaid Rachman
/// Created: 2023/02/11
/// 
/// Interaction logic for VaccinationsPage.xaml
/// Retrieves/Populates a list of vaccinations for the selected animal
/// </summary>
///
/// <remarks>
/// Oleksiy Fedchuk
/// Updated: 2023/04/17
/// 
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
    /// Zaid Rachman
    /// Created: 2023/02/11
    /// Interaction logic for VaccinationsPage.xaml
    /// </summary>
    public partial class VaccinationsPage : Page
    {
        private Animal _animal = new Animal();
        private List<Vaccination> _animalVaccines = null; //Contains all of the vaccines for the current animal selected
        private VaccinationManager _vaccinationManager = new VaccinationManager();


        /// <summary>
        /// Zaid Rachman
        /// 2023/02/11
        /// 
        /// Constructor for VaccinationsPage, takes in an animal object.
        /// </summary>
        /// 
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/17
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="animal"></param>
        public VaccinationsPage(Animal animal)
        {
            _animal = animal;
            InitializeComponent();

        }
        /// <summary>
        /// Zaid Rachman
        /// 2023/02/11
        /// 
        /// Page loaded event. Updates the animalId label and populates the datagrid with the selected animal's vaccinations
        /// </summary>
        /// 
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/17
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

            lblAnimalID.Content = "Animal ID #: " + _animal.AnimalId;
            try
            {
                if (_animalVaccines == null)
                {
                    _animalVaccines = _vaccinationManager.RetrieveVaccinationsByAnimalId(_animal.AnimalId);
                    datVaccinations.ItemsSource = _animalVaccines;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        /// <summary>
        /// Zaid Rachman
        /// Created: 2023/11/02
        /// 
        /// Button click event that navigates to the AddEditVaccinationsPage. Sets the page for add mode.
        /// </summary>
        /// 
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/17
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddVaccine_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEditVaccinationsPage(_animal));
        }
        /// <summary>
        /// Zaid Rachman
        /// Created: 2023/11/02
        /// 
        /// Double click event that navigates to the AddEditVaccination.
        /// Sets the page for Edit mode, unless value selected is null.
        /// If the selected item is null, set page for add mode.
        ///
        /// </summary>
        /// 
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/17
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void datVaccinations_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (datVaccinations.SelectedItem == null)
            {
                NavigationService.Navigate(new AddEditVaccinationsPage(_animal)); //If there are no records selected, create a new one
            }
            else
            {
                var vaccine = (Vaccination)datVaccinations.SelectedItem;

                NavigationService.Navigate(new AddEditVaccinationsPage(vaccine, _animal));
            }

        }
    }
}