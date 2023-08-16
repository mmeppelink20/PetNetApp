/// <summary>
/// Andrew Cromwell
/// Created: 2023/02/01
/// 
/// Interaction logic for MedicalNavigationPage.xaml
/// </summary>
/// <remarks>
/// Zaid Rachman
/// Updated: 2023/04/21
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
using LogicLayer;
using DataObjects;
using WpfPresentation.Animals.Medical;

namespace WpfPresentation.Animals
{
    /// <summary>
    /// Interaction logic for MedicalNavigationPage.xaml
    /// </summary>
    public partial class MedicalNavigationPage : Page
    {
        private Animal _medicalProfileAnimal = null;

        private MasterManager _manager = null;
        private Button[] _medicalTabButtons;

        private Page _returnPage = null;
        /// <summary>
        /// Andrew Cromwell
        /// Created: 2023/02/01
        /// </summary>
        /// 
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/21
        /// Final QA
        /// </remarks>
        /// <param name="manager"></param>
        /// <param name="animal"></param>
        public MedicalNavigationPage(MasterManager manager, Animal animal)
        {
            InitializeComponent();
            _manager = manager;

            _medicalTabButtons = new Button[] { btnMedProfile, btnVaccinations, btnTreatment, btnTests, btnMedNotes, btnMedProcedures, btnMedRecordList };
            _medicalProfileAnimal = animal;
            _returnPage = MedicalPage.GetMedicalPage(_manager);
            displayMedProfileAnimalName();

            // modified by Stephen: Modified the MedicalNavigationPage to show Profile by default
            btnMedProfile_Click(this, new RoutedEventArgs());
        }

        /// <summary>
        /// Stephen Jaurigue
        /// Created: 2023/02/28
        /// 
        /// Overloaded constructor for when this page needs to navigate to a different page than the usual
        /// </summary>
        /// 
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/21
        /// Final QA
        /// </remarks>
        /// <param name="manager">existing instance of the master manager</param>
        /// <param name="animal"> the animal to view medical details about</param>
        /// <param name="returnPage">the page to return to when the back button is pressed</param>
        public MedicalNavigationPage(MasterManager manager, Animal animal, Page returnPage)
        {
            InitializeComponent();
            _manager = manager;
            _medicalTabButtons = new Button[] { btnMedProfile, btnVaccinations, btnTreatment, btnTests, btnMedNotes, btnMedProcedures };
            _medicalProfileAnimal = animal;
            _returnPage = returnPage;
            displayMedProfileAnimalName();

            btnMedProfile_Click(this, new RoutedEventArgs());
        }
        /// <summary>
        /// Stephen Jaurigue
        /// Created: 2023/02/12
        /// 
        /// </summary>
        /// 
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/21
        /// Final QA
        /// </remarks>
        private void displayMedProfileAnimalName()
        {
            this.lblMedProfileAnimal.Content = _medicalProfileAnimal.AnimalName;
        }
        /// <summary>
        /// Stephen Jaurigue
        /// Created: 2023/02/12
        /// </summary>
        /// 
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/21
        /// Final QA
        /// </remarks>
        /// <param name="selectedButton"></param>
        private void ChangeSelectedButton(Button selectedButton)
        {
            UnselectAllButtons();
            selectedButton.Style = (Style)Application.Current.Resources["rsrcSelectedButton"];
        }
        /// <summary>
        /// Stephen Jaurigue
        /// Created: 2023/02/12
        /// 
        /// </summary>
        /// 
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/21
        /// Final QA
        /// </remarks>
        private void UnselectAllButtons()
        {
            foreach (Button button in _medicalTabButtons)
            {
                button.Style = (Style)Application.Current.Resources["rsrcUnselectedButton"];
            }
        }
        /// <summary>
        /// Stephen Jaurigue
        /// Created: 2023/02/24
        /// </summary>
        /// 
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/21
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMedProfile_Click(object sender, RoutedEventArgs e)
        {
            ChangeSelectedButton(btnMedProfile);
            // replace with page name and then delete comment
            frameMedical.Navigate(new AnimalMedicalProfile(_medicalProfileAnimal.AnimalId));
        }

        /// <summary>
        /// Stephen Jaurigue
        /// Created: 2023/02/24
        /// </summary>
        /// 
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/21
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnVaccinations_Click(object sender, RoutedEventArgs e)
        {
            ChangeSelectedButton((Button)sender);
            // replace with page name and then delete comment
            frameMedical.Navigate(new VaccinationsPage(_medicalProfileAnimal));
        }

        /// <summary>
        /// Stephen Jaurigue
        /// Created: 2023/02/12
        /// 
        /// </summary>
        /// 
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/21
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTreatment_Click(object sender, RoutedEventArgs e)
        {
            ChangeSelectedButton((Button)sender);
            frameMedical.Navigate(new MedicalTreatmentPage(_medicalProfileAnimal));
        }

        /// <summary>
        /// Stephen Jaurigue
        /// Created: 2023/02/12
        /// 
        /// </summary>
        /// 
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/21
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTests_Click(object sender, RoutedEventArgs e)
        {
            ChangeSelectedButton((Button)sender);
          
            frameMedical.Navigate(AnimalMedicalTestsPage.GetAnimalMedicalTestsPage(_medicalProfileAnimal));
        }

        /// <summary>
        /// Stephen Jaurigue
        /// Created: 2023/02/12
        /// </summary>
        /// 
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/21
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMedNotes_Click(object sender, RoutedEventArgs e) 
        {
            ChangeSelectedButton((Button)sender);
            
            frameMedical.Navigate(new Medical_Notes(_medicalProfileAnimal, _manager));
        }

        /// <summary>
        /// Stephen Jaurigue
        /// Created: 2023/02/12
        /// 
        /// </summary>
        /// 
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/21
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMedProcedures_Click(object sender, RoutedEventArgs e)
        {
            ChangeSelectedButton(btnMedProcedures);
            // replace with page name and then delete comment
            frameMedical.Navigate(new MedProcedurePage(_medicalProfileAnimal, _manager));
        }

        /// <summary>
        /// Stephen Jaurigue
        /// Created: 2023/02/19
        /// </summary>
        /// 
        /// <remarks>
        /// Changed this to return to the page in the _returnPage variable so that this page can navigate back to different pages
        /// </remarks>
        /// 
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/21
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMedBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(_returnPage);
        }
        /// <summary>
        /// Molly Meister
        /// Created: 2023/03/10
        /// </summary>
        /// 
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/21
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMedRecordList_Click(object sender, RoutedEventArgs e)
        {
            ChangeSelectedButton(btnMedRecordList);

            frameMedical.Navigate(new MedicalRecordListPage(_medicalProfileAnimal, _manager));
        }
    }
}
