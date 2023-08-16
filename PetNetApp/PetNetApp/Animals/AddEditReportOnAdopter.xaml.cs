/// <summary>
/// Asa Armstrong
/// Created: 2023/03/30
/// 
/// WPF UI logic for adding and editing Reports on Adopters. Uses AdoptionApplicationResponse Data Object and Database Object.
/// </summary>
///
/// <remarks>
/// 
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
    /// Interaction logic for AddEditReportOnAdopter.xaml
    /// </summary>
    public partial class AddEditReportOnAdopter : Page
    {
        private MasterManager _masterManager = MasterManager.GetMasterManager();
        private bool isEditMode = false;
        // private AdoptionApplication _adoptionApplication = null;
        private int _adoptionApplicationId = -1;

        private AdoptionApplicationResponseVM _oldAdoptionApplicationResponse = new AdoptionApplicationResponseVM();
        private AdoptionApplicationResponseVM _response = new AdoptionApplicationResponseVM();

        /// <summary>
        /// Author: Asa Armstrong
        /// Date: 2023/04/23
        /// Description: Constructor for page AddEditReportOnAdopter
        /// </summary>
        /// <param name="adoptionApplicationId"></param>
        public AddEditReportOnAdopter(int adoptionApplicationId)
        {
            _adoptionApplicationId = adoptionApplicationId;
            InitializeComponent();
            setupPage();
        }

        /// <summary>
        /// Author: Asa Armstrong
        /// Date: 2023/04/23
        /// Description: Constructor for page AddEditReportOnAdopter
        /// </summary>
        /// <param name="adoptionApplication"></param>
        /*
        public AddEditReportOnAdopter(AdoptionApplication adoptionApplication)
        {
            _adoptionApplication = adoptionApplication;
            InitializeComponent();
        }
        */

        private void setupPage()
        {
            try
            {
                _oldAdoptionApplicationResponse = _masterManager.AdoptionApplicationResponseManager.RetrieveAdoptionApplicationResponse(_adoptionApplicationId);
                if (!_oldAdoptionApplicationResponse.AdoptionApplicationId.Equals(0)) // record exists
                {
                    isEditMode = true;
                    setPageForEditMode();
                }
                else // not edit mode
                {
                    // txt_AdopterName.Text = _adoptionApplication -> Given and Family name
                    // txt_AdopterAccountID.Text = _adoptionApplication -> ApplicantId
                }
            }
            catch (Exception ex)
            {
                PromptWindow.ShowPrompt("Error", ex.Message, ButtonMode.Ok);
            }
        }

        private void setPageForEditMode()
        {
            lbl_Title.Content = "Update Adoption Application Response";
            txt_Comments.Text = _oldAdoptionApplicationResponse.AdoptionApplicationResponseNotes;
            //txt_AdopterAccountID.Text = _oldAdoptionApplicationResponse.ApplicantId.ToString();
            //txt_AdopterName.Text = _oldAdoptionApplicationResponse.AdoptionApplicantGivenName + " " + _oldAdoptionApplicationResponse.AdoptionApplicantFamilyName;
            txt_AdoptionResponseID.Text = _oldAdoptionApplicationResponse.AdoptionApplicationResponseId.ToString();
            rad_ApprovedYes.IsChecked = _oldAdoptionApplicationResponse.Approved;

            txt_AdopterAccountID.Visibility = Visibility.Hidden;
            txt_DateCreated.Visibility = Visibility.Visible;
            lbl_AdopterAccountID.Visibility = Visibility.Hidden;
            lbl_DateCreated.Visibility = Visibility.Visible;

            txt_DateCreated.Text = _oldAdoptionApplicationResponse.AdoptionApplicationResponseDate.ToLongDateString();
        }

        /// <summary>
        /// Author: Asa Armstrong
        /// Date: 2023/04/23
        /// Description: Adds a new record to the DB or Edits an existing one.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _response.Approved = (rad_ApprovedYes.IsChecked.Equals(true) ? true : false);
                _response.AdoptionApplicationResponseNotes = txt_Comments.Text;
                if (!ValidationHelpers.IsValidLongDescription(_response.AdoptionApplicationResponseNotes))
                {
                    throw new ApplicationException("Invalid Comments");
                }
                _response.ResponderUserId = _masterManager.User.UsersId;
                _response.AdoptionApplicationId = _adoptionApplicationId; // or _adoptionApplication.AdoptionApplicationId

                if (!isEditMode)// not edit mode
                {
                    //if (_masterManager.AdoptionApplicationResponseManager.AddAdoptionApplicationResponse(_response))
                    //{
                    //    PromptWindow.ShowPrompt("Congratulations!", "Record Added", ButtonMode.Ok);
                    //    setupPage();
                    //}
                    //else
                    //{
                    //    PromptWindow.ShowPrompt("Error", "Record Not Added", ButtonMode.Ok);
                    //}
                }
                else // edit mode
                {
                    if (_masterManager.AdoptionApplicationResponseManager.EditAdoptionApplicationResponse(_response, _oldAdoptionApplicationResponse))
                    {
                        PromptWindow.ShowPrompt("Congratulations!", "Record Updated", ButtonMode.Ok);
                        setupPage();
                    }
                    else
                    {
                        PromptWindow.ShowPrompt("Error", "Record Not Updated", ButtonMode.Ok);
                    }
                }
            }
            catch (Exception ex)
            {
                PromptWindow.ShowPrompt("Error", ex.Message, ButtonMode.Ok);
            }
        }

        /// <summary>
        /// Author: Asa Armstrong
        /// Date: 2023/04/23
        /// Description: Cancels the Add/Edit and returns from the page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            if (PromptWindow.ShowPrompt("Confirm Cancel", "Cancel and return?", ButtonMode.YesNo).Equals(PromptSelection.Yes))
            {
                if (NavigationService.CanGoBack)
                {
                    NavigationService.GoBack();
                }
                else
                {
                    //NavigationService.Navigate(new WpfPresentation.Animals.AnimalsPage());
                }
            }
        }
    }
}
