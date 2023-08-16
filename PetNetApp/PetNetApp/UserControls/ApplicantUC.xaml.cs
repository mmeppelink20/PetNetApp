/// <summary>
/// Molly Meister
/// Created: 2023/04/23
/// 
///ApplicantUC user control class.
/// </summary>
///
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
using WpfPresentation.Animals;

namespace WpfPresentation.UserControls
{
    /// <summary>
    /// Interaction logic for ApplicantUC.xaml
    /// </summary>
    public partial class ApplicantUC : UserControl
    {
        private Applicant _applicant;
        private AdoptionApplicationVM _adoptionApplication;
        private AnimalVM _animal;
        private FosterApplicationVM _fosterapplication;
        private MasterManager _manager = MasterManager.GetMasterManager();

        /// <summary>
        /// Molly Meister
        /// Created: 2023/04/23
        /// 
        /// Custom constructor for ApplicantUC that requires an Applicant, AdoptionApplicationVM and AnimalVM object.
        /// Initializes _adoptionApplication, _applicant and _animal.
        /// </summary>
        /// <param name="applicant"></param>
        /// <param name="application"></param>
        /// <param name="animal"></param>
        public ApplicantUC(Applicant applicant, AdoptionApplicationVM application, AnimalVM animal)
        {
            _adoptionApplication = application;
            _applicant = applicant;
            _animal = animal;
            InitializeComponent();
        }

        /// <summary>
        /// Molly Meister
        /// Created: 2023/04/23
        /// 
        /// Custom constructor for ApplicantUC that requires an Applicant and FosterApplicationVM object.
        /// Initializes _applicant and _fosterApplication.
        /// </summary>
        /// <param name="applicant"></param>
        /// <param name="application"></param>
        public ApplicantUC(Applicant applicant, FosterApplicationVM application)
        {
            _applicant = applicant;
            _fosterapplication = application;
            InitializeComponent();
        }

        /// <summary>
        /// Molly Meister
        /// Created: 2023/04/23
        /// 
        /// Button click handler to conditionally pass instance variables and navigate to a new instance of ViewApplication.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnViewApplication_Click(object sender, RoutedEventArgs e)
        {
            foreach (Window wnd in Application.Current.Windows)
            {
                if (wnd is AdoptionApplicantsWindow)
                {
                    if (IsAdoptionApplication())
                    {
                        var myWindow = Window.GetWindow(this);
                        myWindow.Close();
                        MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
                        ViewApplication viewApplication = new ViewApplication(_adoptionApplication, _applicant, _animal);
                        viewApplication.btnBackToAnimalProfile.Content = "Back to Animal Profile";
                        mainWindow.frameMain.Navigate(AnimalsPage.GetAnimalsPage().frameAnimals.Content = viewApplication);
                        return;
                    }
                }
            }
               
                
                    if (IsAdoptionApplication())
                    {
                        MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
                        ViewApplication viewApplication = new ViewApplication(_adoptionApplication, _applicant, _animal);
                        viewApplication.btnBackToAnimalProfile.Content = "Back To User Profile";
                        mainWindow.frameMain.Navigate(viewApplication);
                    }
                    else
                    {
                        MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
                        ViewApplication viewApplication = new ViewApplication(_fosterapplication, _applicant);
                        viewApplication.btnBackToAnimalProfile.Content = "Back To User Profile";
                        mainWindow.frameMain.Navigate(viewApplication);
                    }
            
        }

        /// <summary>
        /// Molly Meister
        /// Created 2023/04/23
        /// 
        /// Button click handler to pass the Applicant UserVM and navigate to a new instance of the UserProfilePage.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnViewProfile_Click(object sender, RoutedEventArgs e)
        {
            UsersVM userVM;
            Users user;
            //List<Role> userRoles = new List<Role>();
            try
            {
                user = _manager.UsersManager.RetrieveUserByUsersId((int)_applicant.UserId);
                userVM = new UsersVM()
                {
                    UsersId = user.UsersId,
                    GenderId = user.GenderId,
                    PronounId = user.PronounId,
                    ShelterId = user.ShelterId == null ? null : user.ShelterId,
                    GivenName = user.GivenName,
                    FamilyName = user.FamilyName,
                    Email = user.Email,
                    Address = user.Address,
                    Address2 = user.Address2,
                    Zipcode = user.Zipcode,
                    Phone = user.Phone,
                    CreationDate = user.CreationDate,
                    Active = user.Active,
                    Suspend = user.Suspend,
                    Roles = _manager.UsersManager.RetrieveRolesByUsersId((int)_applicant.UserId)
                };

                var myWindow = Window.GetWindow(this);
                myWindow.Close();
                MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
                mainWindow.frameMain.Navigate(Misc.UserProfilePage.GetUserProfilePage(userVM));

            }
            catch (Exception up)
            {
                PromptWindow.ShowPrompt("Error", "There was an error retrieving the data. \n\n" + up.InnerException.Message);
            } 
        }

        /// <summary>
        /// Molly Meister
        /// Created 2023/04/23
        /// 
        /// Helper method to determine if the instance of the ApplicantUC is for an adoption or foster application
        /// for conditional logic throughout the class.
        /// </summary>
        /// <returns>bool</returns>
        private bool IsAdoptionApplication()
        {
            bool adoptionApplication;
            if (_adoptionApplication != null && _fosterapplication == null)
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
