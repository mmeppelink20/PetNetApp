/// <summary>
/// Molly Meister
/// Created: 04/14/2023
/// 
/// AdoptionApplicantsWindow class
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
using System.Windows.Shapes;
using DataObjects;
using LogicLayer;
using LogicLayerInterfaces;
using WpfPresentation.UserControls;

namespace WpfPresentation.Animals
{
    /// <summary>
    /// Interaction logic for AdoptionApplicantsWindow.xaml
    /// </summary>
    public partial class AdoptionApplicantsWindow : Window
    {
        private MasterManager _manager = MasterManager.GetMasterManager();
        private AnimalVM _animal = null;
        private List<AdoptionApplicationVM> _adoptionApplicationList = null;
        private List<AdoptionApplicationVM> _pendingAdoptionApplicationList = null;

        /// <summary>
        /// Molly Meister
        /// Created: 04/14/2023
        /// 
        /// Custom constructor for AdoptionApplicantsWindow that requires an AnimalVM and MasterManager object.
        /// Initalizes the _animal, _manager, _adoptionApplicationList and _pendingAdoptionApplicationList
        /// </summary>
        ///
        /// <param name="animal"></param>
        /// <param name="manager"></param>
        public AdoptionApplicantsWindow(AnimalVM animal, MasterManager manager)
        {
            _manager = manager;
            _animal = animal;
            _adoptionApplicationList = new List<AdoptionApplicationVM>();
            _pendingAdoptionApplicationList = new List<AdoptionApplicationVM>();
            InitializeComponent();
        }

        /// <summary>
        /// Molly Meister
        /// Created: 04/14/2023
        /// 
        /// Logic for AdoptionApplicantsWindow load.
        /// Sets the header of the page and calls the method to populate the applicant list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            lblTitle.Content = "Applicants For " + _animal.AnimalName;
            PopulateApplicantList(_animal.AnimalId);
        }

        /// <summary>
        /// Molly Meister
        /// Created: 04/14/2023
        /// 
        /// Logic to populate the window with applicants for each adoption application for the animal with a user control.
        /// </summary>
        /// <param name="animalId"></param>
        public void PopulateApplicantList(int animalId)
        {
            try
            {
                _adoptionApplicationList = _manager.AdoptionApplicationManager.RetrieveAllAdoptionApplicationsByAnimalId(animalId);
                if(_adoptionApplicationList.Count > 0)
                {
                    foreach (AdoptionApplicationVM application in _adoptionApplicationList)
                    {
                        if(application.ApplicationStatusId.Equals("Pending"))
                        {
                            _pendingAdoptionApplicationList.Add(application);
                        }
                    }
                    if(_pendingAdoptionApplicationList.Count > 0)
                    {
                        foreach (AdoptionApplicationVM application in _adoptionApplicationList)
                        {
                                GetApplicantUC(application);
                        }

                    }
                    else
                    {
                        lblNoApplicants.Visibility = Visibility.Visible;
                        grdApplicantList.Visibility = Visibility.Hidden;
                    }
                }
                else
                {
                    lblNoApplicants.Visibility = Visibility.Visible;
                    grdApplicantList.Visibility = Visibility.Hidden;
                }

            }
            catch (Exception up)
            {
                PromptWindow.ShowPrompt("Error", "Failed to retrieve applicants \n\n" + up.Message);
            }
        }

        /// <summary>
        /// Molly Meister
        /// Created: 04/14/2023
        /// 
        /// Logic to create an ApplicantUC user control.
        /// </summary>
        /// <param name="animalId"></param>
        public void GetApplicantUC(AdoptionApplicationVM application)
        {
            Applicant applicant = application.AdoptionApplicant;
            ApplicantUC applicantUC = new ApplicantUC(applicant, application, _animal);

            applicantUC.lblUsersAccountName.Content = applicant.ApplicantGivenName + " " + applicant.ApplicantFamilyName;
            applicantUC.lblUsersEmail.Content = applicant.ApplicantEmail;
            
            if(application.AdoptionApplicant.UserId == null)
            {
                applicantUC.btnViewProfile.Visibility = Visibility.Hidden;
                applicantUC.btnNoProfile.Visibility = Visibility.Visible;
            }
            spApplicantList.Children.Add(applicantUC);
        }

        /// <summary>
        /// Molly Meister
        /// Created: 04/14/2023
        /// 
        /// Close button handler that closes the AdoptionApplicantsWindow.
        /// </summary>
        /// <param name="animalId"></param>
        private void btnCloseWindowX_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
