/// <summary>
/// Molly Meister
/// Created: 04/14/2023
/// 
/// ViewApplication class
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
using PetNetApp;

namespace WpfPresentation.Animals
{
    /// <summary>
    /// Interaction logic for ViewApplication.xaml
    /// </summary>
    public partial class ViewApplication : Page
    {
        private AdoptionApplicationVM _adoptionApplication = null;
        private FosterApplicationVM _fosterApplication = null;
        private Applicant _applicant = null;
        private AnimalVM _animal = null;
        private MasterManager _manager = MasterManager.GetMasterManager();
    
        /// <summary>
        /// Molly Meister
        /// Created: 04/14/2023
        /// 
        /// Custom constructor for ViewApplication that requires an AdoptionApplicationVM, Applicant and AnimalVM object.
        /// Initializes _adoptionApplication, _applicant and _animal.
        /// </summary>
        ///
        /// <param name="application"></param>
        /// <param name="applicant"></param>
        /// <param name="animal"></param>
        public ViewApplication(AdoptionApplicationVM application, Applicant applicant, AnimalVM animal)
        {
            _adoptionApplication = application;
            _applicant = applicant;
            _animal = animal;
            InitializeComponent();
        }

        /// <summary>
        /// Molly Meister
        /// Created: 04/14/2023
        /// 
        /// Custom constructor for ViewApplication that requires a FosterApplicationVM and Applicant object.
        /// Initializes _applicant and _fosterApplication.
        /// </summary>
        ///
        /// <param name="application"></param>
        /// <param name="applicant"></param>
        public ViewApplication(FosterApplicationVM application, Applicant applicant)
        {
            _applicant = applicant;
            _fosterApplication = application;
            InitializeComponent();
        }


        /// <summary>
        /// Molly Meister
        /// Created: 04/14/2023
        /// 
        /// Logic to populate the view with the AdoptionApplication and Applicant objects passed to the constructor.
        /// </summary>
        public void PopulateApplication()
        {
            
            if(IsAdoptionApplication())
            {
                if (_applicant != null && _adoptionApplication != null && _animal != null)
                {
                    lblTitle.Content = _applicant.ApplicantGivenName + "'s Application For " + _animal.AnimalName;
                    txtApplicationStatus.Text = _adoptionApplication.ApplicationStatusId;
                    txtApplicationDate.Text = _adoptionApplication.AdoptionApplicationDate.ToShortDateString();
                    txtApplicantGivenName.Text = _applicant.ApplicantGivenName;
                    txtApplicantFamilyName.Text = _applicant.ApplicantFamilyName;
                    txtApplicantEmail.Text = _applicant.ApplicantEmail;
                    txtApplicantPhoneNumber.Text = _applicant.ApplicantPhoneNumber;
                    txtApplicantAddress1.Text = _applicant.ApplicantAddress;
                    txtApplicantAddress2.Text = _applicant.ApplicantAddress2;
                    txtApplicantZipCode.Text = _applicant.ApplicantZipCode;
                    txtApplicantHomeType.Text = _applicant.HomeTypeId;
                    txtApplicantOwnership.Text = _applicant.HomeOwnershipId;
                    txtApplicantPets.Text = _applicant.NumberOfPets.ToString();
                    txtApplicantChildren.Text = _applicant.NumberOfChildren.ToString();
                }
            }
            else
            {
                if (_applicant != null && _fosterApplication != null)
                {
                    lblTitle.Content = _applicant.ApplicantGivenName + "'s Foster Application";
                    txtApplicationStatus.Text = _fosterApplication.ApplicationStatusId;
                    txtApplicationDate.Text = _fosterApplication.FosterApplicationDate.ToShortDateString();
                    txtApplicantGivenName.Text = _applicant.ApplicantGivenName;
                    txtApplicantFamilyName.Text = _applicant.ApplicantFamilyName;
                    txtApplicantEmail.Text = _applicant.ApplicantEmail;
                    txtApplicantPhoneNumber.Text = _applicant.ApplicantPhoneNumber;
                    txtApplicantAddress1.Text = _applicant.ApplicantAddress;
                    txtApplicantAddress2.Text = _applicant.ApplicantAddress2;
                    txtApplicantZipCode.Text = _applicant.ApplicantZipCode;
                    txtApplicantHomeType.Text = _applicant.HomeTypeId;
                    txtApplicantOwnership.Text = _applicant.HomeOwnershipId;
                    txtApplicantPets.Text = _applicant.NumberOfPets.ToString();
                    txtApplicantChildren.Text = _applicant.NumberOfChildren.ToString();
                    grdFosterOptions.Visibility = Visibility.Visible;
                    txtDateAvailable.Text = _fosterApplication.FosterApplicationStartDate.ToShortDateString();
                    txtMaxAnimals.Text = _fosterApplication.FosterApplicationMaxAnimals.ToString();
                }
            }
            
        }

        /// <summary>
        /// Molly Meister
        /// Created: 04/14/2023
        /// 
        /// Logic for page load.
        /// Calles the PopulateApplication() method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            PopulateApplication();
        }

        /// <summary>
        /// Molly Meister
        /// Created: 04/14/2023
        /// 
        /// Button click handler to the profile they came from, either the animal or user profile.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBackToAnimalProfile_Click(object sender, RoutedEventArgs e)
        {
            if(btnBackToAnimalProfile.Content.Equals("Back To User Profile"))
            {
                UsersVM userVM = new UsersVM();
                try
                {
                    Users user = _manager.UsersManager.RetrieveUserByUsersId((int)_applicant.UserId);
                    if(user != null)
                    {
                        userVM.UsersId = user.UsersId;
                        userVM.GenderId = user.GenderId;
                        userVM.PronounId = user.PronounId;
                        userVM.ShelterId = user.ShelterId == null ? null : user.ShelterId;
                        userVM.GivenName = user.GivenName;
                        userVM.FamilyName = user.FamilyName;
                        userVM.Email = user.Email;
                        userVM.Address = user.Address;
                        userVM.Address2 = user.Address2;
                        userVM.Zipcode = user.Zipcode;
                        userVM.Phone = user.Phone;
                        userVM.CreationDate = user.CreationDate;
                        userVM.Active = user.Active;
                        userVM.Suspend = user.Suspend;
                        userVM.Roles = _manager.UsersManager.RetrieveRolesByUsersId((int)_applicant.UserId);

                        MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
                        mainWindow.frameMain.Navigate(Misc.UserProfilePage.GetUserProfilePage(userVM));
                    }
                }
                catch (Exception up)
                {
                    PromptWindow.ShowPrompt("Error", "We're sorry, there was an error processing your request." + up.Message);
                    NavigationService.Navigate(new WpfPresentation.Community.UserManagementPage());
                }
            }
            else
            {
                MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
                mainWindow.frameMain.Navigate(AnimalsPage.GetAnimalsPage().frameAnimals.Content = new ViewAdoptableAnimalProfile(_animal.AnimalId));
            }
        }

        /// <summary>
        /// Molly Meister
        /// Created: 04/14/2023
        /// 
        /// Button handler to open the ApplicationResponseWindow to approve or deny the application.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreateReport_Click(object sender, RoutedEventArgs e)
        {
            if(IsAdoptionApplication())
            {
                ApplicationResponseWindow win = new ApplicationResponseWindow(_adoptionApplication);
                win.Show();
            }
            else
            {
                ApplicationResponseWindow win = new ApplicationResponseWindow(_fosterApplication);
                win.Show();
            }
            
        }

        /// <summary>
        /// Molly Meister
        /// Created 2023/04/23
        /// 
        /// Helper method to determine if the instance of the ApplicationResponseWindow is for an adoption or foster application
        /// for conditional logic throughout the class.
        /// </summary>
        /// <returns>bool</returns>
        private bool IsAdoptionApplication()
        {
            bool adoptionApplication;
            if(_adoptionApplication != null && _fosterApplication == null)
            {
                adoptionApplication = true;
            }
            else
            {
                adoptionApplication = false;
            }
            return adoptionApplication;
        }
    }
}
