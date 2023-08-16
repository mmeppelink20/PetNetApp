/// <summary>
/// Asa Armstrong
/// Created: 2023/03/22
/// 
/// WPF UI logic for adding and editing Reports on Fosters. Uses FosterApplicationResponse Data Object and Database Object.
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
    public partial class AddEditReportOnFoster : Page
    {
        private MasterManager _masterManager = MasterManager.GetMasterManager();
        private bool isEditMode = false;
        private FosterApplicationVM _fosterApplication = null;
        private int _fosterApplicationId = -1;

        private FosterApplicationResponseVM _oldFosterApplicationResponse = new FosterApplicationResponseVM();
        private FosterApplicationResponseVM _responseVM = new FosterApplicationResponseVM();

        /// <summary>
        /// Author: Asa Armstrong
        /// Date: 2023/04/23
        /// Description: Constructor for page AddEditReportOnFoster
        /// </summary>
        /// <param name="fosterApplicationId">The int Id of the FosterApplication</param>
        /// <returns>AddEditReportOnFoster</returns>
        public AddEditReportOnFoster(int fosterApplicationId)
        {
            _fosterApplicationId = fosterApplicationId;
            InitializeComponent();
            setupPage();
        }

        /// <summary>
        /// Author: Asa Armstrong
        /// Date: 2023/04/23
        /// Description: Constructor for page AddEditReportOnFoster
        /// </summary>
        /// <param name="fosterApplication">The FosterApplication that this response is for</param>
        /// <returns>AddEditReportOnFoster</returns>
        public AddEditReportOnFoster(FosterApplicationVM fosterApplication)
        {
            _fosterApplication = fosterApplication;
            _fosterApplicationId = fosterApplication.FosterApplicationId;
            InitializeComponent();
            setupPage();
        }

        private void setupPage()
        {
            try
            {
                _oldFosterApplicationResponse = _masterManager.FosterApplicationResponseManager.RetrieveFosterApplicationResponse(_fosterApplicationId);
                if (!(_oldFosterApplicationResponse.FosterApplicationResponseId == 0)) // a record exists so edit mode
                {
                    isEditMode = true;
                    setPageForEditMode();
                }
                else // not edit mode
                {
                    txt_FosterName.Text = _fosterApplication.FosterApplicationApplicant.ApplicantGivenName.ToString() + " " + _fosterApplication.FosterApplicationApplicant.ApplicantFamilyName.ToString();
                    txt_FosterAccountID.Text = _fosterApplication.ApplicantId.ToString();
                }
            }
            catch (Exception ex)
            {
                PromptWindow.ShowPrompt("Error", ex.Message, ButtonMode.Ok);
            }
        }

        private void setPageForEditMode()
        {
            lbl_Title.Content = "Update Foster Application Response";
            txt_Comments.Text = _oldFosterApplicationResponse.FosterApplicationResponseNotes;
            txt_FosterAccountID.Text = _oldFosterApplicationResponse.ApplicantId.ToString();
            txt_FosterName.Text = _oldFosterApplicationResponse.FosterApplicantGivenName + " " + _oldFosterApplicationResponse.FosterApplicantFamilyName;
            txt_FosterReportID.Text = _oldFosterApplicationResponse.FosterApplicationResponseId.ToString();
            rad_ApprovedYes.IsChecked = _oldFosterApplicationResponse.Approved;

            txt_FosterAccountID.Visibility = Visibility.Hidden;
            txt_DateCreated.Visibility = Visibility.Visible;
            lbl_FosterAccountID.Visibility = Visibility.Hidden;
            lbl_DateCreated.Visibility = Visibility.Visible;

            txt_DateCreated.Text = _oldFosterApplicationResponse.FosterApplicationResponseDate.ToLongDateString();
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
                _responseVM.Approved = (rad_ApprovedYes.IsChecked.Equals(true) ? true : false);
                _responseVM.FosterApplicationResponseNotes = txt_Comments.Text;
                if (!ValidationHelpers.IsValidLongDescription(_responseVM.FosterApplicationResponseNotes))
                {
                    throw new ApplicationException("Invalid Comments");
                }
                _responseVM.UsersId = _masterManager.User.UsersId;
                _responseVM.FosterApplicationId = _fosterApplicationId; // or _fosterApplication.FosterApplicationId

                if (!isEditMode)// not edit mode
                {
                    if (_masterManager.FosterApplicationResponseManager.AddFosterApplicationResponse(_responseVM))
                    {
                        PromptWindow.ShowPrompt("Congratulations!", "Record Added", ButtonMode.Ok);
                        // setupPage();
                        Window.GetWindow(this).Close();
                    }
                    else
                    {
                        PromptWindow.ShowPrompt("Error", "Record Not Added", ButtonMode.Ok);
                    }
                }
                else // edit mode
                {
                    if(_masterManager.FosterApplicationResponseManager.EditFosterApplicationResponse(_responseVM, _oldFosterApplicationResponse))
                    {
                        PromptWindow.ShowPrompt("Congratulations!", "Record Updated", ButtonMode.Ok);
                        //setupPage();
                        Window.GetWindow(this).Close();
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
            PromptSelection result = PromptWindow.ShowPrompt("Confirm", "Are you sure you want to cancel? \n\n Any unsaved changes will be lost.", ButtonMode.YesNo);
            if (result == PromptSelection.Yes)
            {
                var window = Window.GetWindow(this);
                window.Close();
            }

        }
    }
}
