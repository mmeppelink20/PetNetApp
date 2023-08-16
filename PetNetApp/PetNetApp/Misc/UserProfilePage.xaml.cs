/// <summary>
/// Molly Meister & Mads Rhea
/// Created: 04/14/2023
/// 
/// UserProfilePage class
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
using LogicLayer;
using DataObjects;
using PetNetApp;
using WpfPresentation.UserControls;
using WpfPresentation.Community;

namespace WpfPresentation.Misc
{
    public partial class UserProfilePage : Page
    {
        private static UserProfilePage _existingProfilePage = null;
        private UsersVM _user;
        private MasterManager _manager = MasterManager.GetMasterManager();
        private Button[] _profileTabButtons;
        private List<AdoptionApplicationVM> _adoptionApplicationList = null;
        private List<FosterApplicationVM> _fosterApplicationList = null;

        /// <summary>
        /// Molly Meister
        /// Created 2023/04/23
        /// 
        /// Custom constructor for the UserProfilePage that requires a UserVM object.
        /// Initializes the _user and _profileTabButtons.
        /// </summary>
        /// <param name="user"></param>
        public UserProfilePage()
        {
            InitializeComponent();
            _profileTabButtons = new Button[] { btnPendingAdoptionApplications, btnFosterApplications};
        }

        /// <summary>
        /// Mads Rhea
        /// Created ?
        /// </summary>
        /// <remarks>
        /// Molly Meister
        /// Updated: 2023/04/23
        /// 
        /// Updates the method to require a UserVM object and pass it to the new instance of the UserProfilePage being created.
        /// </remarks>
        /// <param name="mainWindow"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public static UserProfilePage GetUserProfilePage(UsersVM user)
        {
            if (_existingProfilePage == null)
            {
                _existingProfilePage = new UserProfilePage();
            }
            _existingProfilePage._user = user;
            _existingProfilePage.PopulateProfile();

            return _existingProfilePage;
        }

        /// <summary>
        /// Molly Meister
        /// Created 2023/04/23
        /// 
        /// Page load logic. Calles UnselectAllButtons() and PopulateProfile() methods.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UnselectAllButtons();
            ClearTabBox();
            PopulateProfile();
        }

        /// <summary>
        /// Molly Meister
        /// Created 2023/04/23
        /// 
        /// Button click handler to return to the list of users.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBackToList_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new WpfPresentation.Community.UserManagementPage());
        }

        /// <summary>
        /// Molly Meister
        /// Created 2023/04/23
        /// 
        /// Populates the profile with the UserVM properties initialized to the _user.
        /// </summary>
        private void PopulateProfile()
        {
            bool acceptingAnimals = false;
            lblDisplayName.Content = _user.GivenName + " " + _user.FamilyName;
            if(_user.Roles != null)
            {
                foreach (string role in _user.Roles)
                {
                    if (role.Equals("Fosterer"))
                    {
                        try
                        {
                            acceptingAnimals = _manager.FosterManager.RetrieveCurrentlyAcceptingAnimalsByUsersId(_user.UsersId);
                            if (acceptingAnimals)
                            {
                                lblFosterToggle.Visibility = Visibility.Visible;
                            }
                            else
                            {
                                lblFosterToggle.Visibility = Visibility.Visible;
                                lblFosterToggle.Content = "Not available to foster";
                                lblFosterToggle.Foreground = new SolidColorBrush(Colors.Red);
                            }
                        }
                        catch (Exception up)
                        {
                            lblFosterToggle.Visibility = Visibility.Hidden;
                        }
                    }
                }
            }
            
        }

        /// <summary>
        /// Molly Meister
        /// Created 2023/04/23
        /// 
        /// Button click handler to view all adoption applications for the user.
        /// Calls ChangeSelectedButton() and PopulatePendingAdoptionApplications if the _adoptionApplicationList doesn't
        /// already hold a reference to the applications.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPendingAdoptionApplications_Click(object sender, RoutedEventArgs e)
        {
            ChangeSelectedButton(btnPendingAdoptionApplications);
            if(_adoptionApplicationList == null)
            {
                PopulatePendingAdoptionApplications(_user.UsersId);
            }
            
        }

        /// <summary>
        /// Molly Meister
        /// Created 2023/04/23
        /// 
        /// Populates a stack panel with ApplicantUC user controls for every adoption application.
        /// Calls GetAdoptionApplicantUC().
        /// </summary>
        /// <param name="userId"></param>
        public void PopulatePendingAdoptionApplications(int userId)
        {
            try
            {
                _adoptionApplicationList = _manager.AdoptionApplicationManager.RetrieveAllAdoptionApplicationsByUsersId(userId);
                if (_adoptionApplicationList.Count > 0)
                {
                    foreach (AdoptionApplicationVM application in _adoptionApplicationList)
                    {
                        GetAdoptionApplicantUC(application);
                    }
                }
                else
                {
                    scrTabBox.Visibility = Visibility.Hidden;
                    spTabBox.Visibility = Visibility.Hidden;
                    lblNoApplications.Visibility = Visibility.Visible;
                }
            }
            catch (Exception up)
            {
                PromptWindow.ShowPrompt("Error", "Failed to retrieve applications. \n\n" + up.Message);
            }
        }

        /// <summary>
        /// Molly Meister
        /// Created 2023/04/23
        /// 
        /// Populates a stack panel with ApplicantUC user controls for every foster application.
        /// Calls GetFosterApplicantUC().
        /// </summary>
        /// <param name="userId"></param>
        public void PopulatePendingFosterApplications(int userId)
        {
            try
            {
                _fosterApplicationList = _manager.FosterApplicationManager.RetrieveAllFosterApplicationsByUsersId(userId);
                if (_fosterApplicationList.Count > 0)
                {
                    scrTabBox.Visibility = Visibility.Visible;
                    spTabBox.Visibility = Visibility.Visible;
                    foreach (FosterApplicationVM application in _fosterApplicationList)
                    {
                        GetFosterApplicantUC(application);
                    }
                }
                else
                {
                    scrTabBox.Visibility = Visibility.Hidden;
                    spTabBox.Visibility = Visibility.Hidden;
                    lblNoApplications.Visibility = Visibility.Visible;
                }
            }
            catch (Exception up)
            {
                PromptWindow.ShowPrompt("Error", "Failed to retrieve applications. \n\n" + up.Message);
            }
        }

        /// <summary>
        /// Molly Meister
        /// Created 2023/04/23
        /// 
        /// Creates the ApplicantUC user control for each AdoptionApplicationVM.
        /// </summary>
        /// <param name="application"></param>
        public void GetAdoptionApplicantUC(AdoptionApplicationVM application)
        {
            Applicant applicant = application.AdoptionApplicant;
            AnimalVM animal;
            try
            {
                animal = _manager.AnimalManager.RetrieveAnimalAdoptableProfile(application.AnimalId);
                ApplicantUC applicantUC = new ApplicantUC(applicant, application, animal);

                applicantUC.lblUsersAccountName.Content = animal.AnimalName;
                applicantUC.lblUsersEmail.Content = "Shelter Id: " + animal.AnimalShelterId;
                applicantUC.btnViewProfile.Visibility = Visibility.Hidden;
                applicantUC.lblApplicationStatus.Visibility = Visibility.Visible;
                applicantUC.lblApplicationStatus.Content = "Status: " + application.ApplicationStatusId;
                spTabBox.Children.Add(applicantUC);
            }
            catch (Exception up)
            {
                PromptWindow.ShowPrompt("Error", "Failed to retrieve applications. \n\n" + up.Message);
            }
            
        }

        /// <summary>
        /// Molly Meister
        /// Created 2023/04/23
        /// 
        /// Creates the ApplicantUC user control for each FosterApplicationVM.
        /// </summary>
        /// <param name="application"></param>
        public void GetFosterApplicantUC(FosterApplicationVM application)
        {
            Applicant applicant = application.FosterApplicationApplicant;
            try
            {
                ApplicantUC applicantUC = new ApplicantUC(applicant, application);

                applicantUC.lblUsersAccountName.Content = applicant.ApplicantGivenName + " " + applicant.ApplicantFamilyName;
                applicantUC.lblUsersEmail.Content = "Application Date: " + application.FosterApplicationDate;
                applicantUC.btnViewProfile.Visibility = Visibility.Hidden;
                applicantUC.lblApplicationStatus.Visibility = Visibility.Visible;
                applicantUC.lblApplicationStatus.Content = "Status: " + application.ApplicationStatusId;
                spTabBox.Children.Add(applicantUC);
            }
            catch (Exception up)
            {
                PromptWindow.ShowPrompt("Error", "Failed to retrieve applications. \n\n" + up.Message);
            }

        }

        /// <summary>
        /// Stephen Jaurigue & Molly Meister
        /// 2023/04/23
        /// 
        /// Changes the styling of the selected tab button and resets styling for all unselected buttons.
        /// Calls UnselectAllButtons() and ClearTabBox().
        /// 
        /// </summary>
        /// <param name="selectedButton"></param>
        private void ChangeSelectedButton(Button selectedButton)
        {
            UnselectAllButtons();
            selectedButton.Style = (Style)Application.Current.Resources["rsrcSelectedButton"];
            selectedButton.BorderBrush = Brushes.Red;
            ClearTabBox();
        }

        /// <summary>
        /// Stephen Jaurigue & Molly Meister
        /// 2023/04/23
        /// 
        /// Resets the styling for all tab buttons not selected.
        /// 
        /// </summary>
        private void UnselectAllButtons()
        {
            foreach (Button button in _profileTabButtons)
            {
                button.Style = (Style)Application.Current.Resources["rsrcUnselectedButton"];
            }
        }

        /// <summary>
        /// Molly Meister
        /// Created 2023/04/23
        /// 
        /// Button click handler to view all foster applications for the user.
        /// Calls ChangeSelectedButton() and PopulatePendingFosterApplications if the _forsterApplicationList doesn't
        /// already hold a reference to the applications.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFosterApplications_Click(object sender, RoutedEventArgs e)
        {
            ChangeSelectedButton(btnFosterApplications);
            if (_fosterApplicationList == null)
            {
                PopulatePendingFosterApplications(_user.UsersId);
            }
        }

        /// <summary>
        /// Molly Meister
        /// 2023/04/23
        /// 
        /// Clears the stack panel containing the list, hides lblNoApplications if it's been made visible, and 
        /// resets _adoptionApplicationList and _fosterApplicationList to null.
        /// </summary>
        public void ClearTabBox()
        {
            if(spTabBox.Children.Count > 0)
            {
                //spTabBox.Children.RemoveAt(spTabBox.Children.Count - 1);
                spTabBox.Children.Clear();
            }
            _adoptionApplicationList = null;
            _fosterApplicationList = null;
            lblNoApplications.Visibility = Visibility.Hidden;
        }
    }
}
