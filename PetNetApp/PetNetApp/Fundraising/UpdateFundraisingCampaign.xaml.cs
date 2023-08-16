/// <summary>
/// Andrew Schneider
/// Created: 2023/03/23
/// 
/// Interaction logic for UpdateFundraisingCampaign.xaml
/// </summary>
/// <remarks>
/// Oleksiy Fedchuk
/// Updated: 2023/04/27
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

namespace WpfPresentation.Fundraising
{
    public partial class UpdateFundraisingCampaign : Page
    {
        private FundraisingCampaignVM _oldFundraisingCampaignVM = new FundraisingCampaignVM();
        private FundraisingCampaignVM _newFundraisingCampaignVM = new FundraisingCampaignVM();
        private CampaignUpdate _campaignUpdate = new CampaignUpdate();
        private MasterManager _masterManager = MasterManager.GetMasterManager();
        private decimal _displayedTotalAmountRaised = 0.00m;
        private int _displayedTotalNumOfAttendees = 0;
        private int _displayedTotalNumAnimalsAdopted = 0;
        private bool _campgainComplete = false;

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/03/23
        /// 
        /// Public constructor for UpdateFundraisingCampaign which assigns values and calls
        /// InitializeComponent().
        /// 
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/27
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="fundraisingCampaignVM"></param>
        public UpdateFundraisingCampaign(FundraisingCampaignVM fundraisingCampaignVM)
        {
            _oldFundraisingCampaignVM = fundraisingCampaignVM;
            _displayedTotalAmountRaised = _oldFundraisingCampaignVM.AmountRaised;
            _displayedTotalNumOfAttendees = _oldFundraisingCampaignVM.NumOfAttendees;
            _displayedTotalNumAnimalsAdopted = _oldFundraisingCampaignVM.NumAnimalsAdopted;
            _campgainComplete = _oldFundraisingCampaignVM.Complete;
            InitializeComponent();
        }
        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/03/23
        /// 
        /// Page Loaded method. Calls UpdateUI() helper method.
        /// 
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/27
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateUI();
        }

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/03/28
        /// 
        /// Helper method to update the UI by assigning values to the total labels
        /// and campaign title and giving focus to the update title text box.
        /// </summary>
        ///
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/27
        /// 
        /// Final QA
        /// </remarks>
        private void UpdateUI()
        {
            txtCampaignTitle.Text = _oldFundraisingCampaignVM.Title;
            txtUpdateTitle.Focus();
            lblAmountRaised.Content = "Total: $" + _displayedTotalAmountRaised.ToString();
            lblNumOfAttendees.Content = "Total: " + _displayedTotalNumOfAttendees.ToString();
            lblNumAnimalsAdopted.Content = "Total: " + _displayedTotalNumAnimalsAdopted.ToString();
            txtAmountRaised.Text = "";
            txtNumOfAttendees.Text = "";
            txtNumAnimalsAdopted.Text = "";
            if (_campgainComplete)
            {
                ckbComplete.IsChecked = true;
            }
            
        }

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/03/23
        /// 
        /// Click event method for the "Update" button. Calls ValidateAndAssignInput method and if
        /// it returns true an attempt is made add a Campaign Update record and to update the Campaign
        /// Results. If successful a popup is shown and the UI is updated so new totals can be seen.
        /// </summary>
        ///
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/27
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            bool success = false;
            if (ValidateAndAssignInput())
            {
                _campaignUpdate.CampaignId = _oldFundraisingCampaignVM.FundraisingCampaignId;
                if (ckbComplete.IsChecked == true)
                {
                    _newFundraisingCampaignVM.Complete = true;
                }

                try
                {
                    success = _masterManager.FundraisingCampaignManager.AddCampaignUpdate(_campaignUpdate);
                    success = _masterManager.FundraisingCampaignManager.EditFundraisingCampaignResults(_oldFundraisingCampaignVM,
                                                                                                         _newFundraisingCampaignVM);
                }
                catch (Exception ex)
                {
                    PromptWindow.ShowPrompt("Error", "Saving new record failed.\n" + ex, ButtonMode.Ok);
                }
            }

            if (success)
            {
                _campgainComplete = _newFundraisingCampaignVM.Complete;
                UpdateUI();
                PromptWindow.ShowPrompt("Success", "Campaign record updated", ButtonMode.Ok);
                NavigationService.Navigate(WpfPresentation.Fundraising.ViewCampaignsPage.GetViewCampaignsPage());
            }
        }

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/03/23
        /// 
        /// Click event method for the "Cancel" button. A popup is shown to confirm if the user actually 
        /// intends to stop creating a campaign update without saving. 
        /// </summary>
        ///
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/27
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            var result = PromptWindow.ShowPrompt("Discard Changes", "Are you sure you want to cancel?\n" +
                                                    "Campaign will not be updated.", ButtonMode.YesNo);
            if (result == PromptSelection.Yes)
            {
                NavigationService.Navigate(WpfPresentation.Fundraising.ViewCampaignsPage.GetViewCampaignsPage());
            }
        }

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/03/23
        /// 
        /// Helper method that validates the input and assigns input to new objects
        /// if the validation passes.
        /// </summary>
        ///
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/27
        /// 
        /// Final QA
        /// </remarks>
        /// <returns>Bool indicating success</returns>
        private bool ValidateAndAssignInput()
        {
            bool isValid = true;
            decimal amountRaised = 0.00m;
            int numOfAttendees = 0;
            int numAnimalsAdopted = 0;

            // Ifs to assign "0" if any of the text boxes are empty
            if(txtAmountRaised.Text == "")
            {
                txtAmountRaised.Text = "0";
            }

            if (txtNumOfAttendees.Text == "")
            {
                txtNumOfAttendees.Text = "0";
            }

            if (txtNumAnimalsAdopted.Text == "")
            {
                txtNumAnimalsAdopted.Text = "0";
            }

            // Validate title
            if (!txtUpdateTitle.Text.IsValidTitle())
            {
                lblUpdateTitleError.Visibility = Visibility.Visible;
                isValid = false;
            }
            else
            {
                _campaignUpdate.UpdateTitle = txtUpdateTitle.Text;
                lblUpdateTitleError.Visibility = Visibility.Collapsed;
            }

            // Parse and validate amount raised
            if (!decimal.TryParse(txtAmountRaised.Text, out amountRaised))
            {
                lblAmountRaisedError.Visibility = Visibility.Visible;
                isValid = false;
            }
            else
            {
                if ((amountRaised + _displayedTotalAmountRaised) < 0 || (amountRaised + _displayedTotalAmountRaised) > 9999999.99m)
                {
                    lblAmountRaisedError.Visibility = Visibility.Visible;
                    isValid = false;
                }
                else
                {
                    _displayedTotalAmountRaised += amountRaised;
                    _newFundraisingCampaignVM.AmountRaised = amountRaised + _oldFundraisingCampaignVM.AmountRaised;
                    lblAmountRaisedError.Visibility = Visibility.Collapsed;
                }
            }

            // Parse and validate number of attendees and animals adopted
            if (!int.TryParse(txtNumOfAttendees.Text, out numOfAttendees)
                || !int.TryParse(txtNumAnimalsAdopted.Text, out numAnimalsAdopted))
            {
                lblTotalsError.Visibility = Visibility.Visible;
                isValid = false;
            }
            else
            {
                if((numOfAttendees + _displayedTotalNumOfAttendees) < 0
                    || (numAnimalsAdopted + _displayedTotalNumAnimalsAdopted) < 0)
                {
                    lblTotalsError.Visibility = Visibility.Visible;
                    isValid = false;
                }
                else
                {
                    _displayedTotalNumOfAttendees += numOfAttendees;
                    _displayedTotalNumAnimalsAdopted += numAnimalsAdopted;
                    _newFundraisingCampaignVM.NumOfAttendees = numOfAttendees + _oldFundraisingCampaignVM.NumOfAttendees;
                    _newFundraisingCampaignVM.NumAnimalsAdopted = numAnimalsAdopted + _oldFundraisingCampaignVM.NumAnimalsAdopted;
                    lblTotalsError.Visibility = Visibility.Collapsed;
                }
            }

            // Validate update description/note
            if (!txtUpdateDescription.Text.IsValidRequiredLongDescription())
            {
                lblUpdateDescriptionError.Visibility = Visibility.Visible;
                isValid = false;
            }
            else
            {
                _campaignUpdate.UpdateDescription = txtUpdateDescription.Text;
                lblUpdateDescriptionError.Visibility = Visibility.Collapsed;
            }
            return isValid;
        }
    }
}
