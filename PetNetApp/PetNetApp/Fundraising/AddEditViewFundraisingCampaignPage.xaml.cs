///<summary>
/// Stephen Jaurigue
/// Created: 2023/04/21
/// 
/// </summary>
/// <remarks>
/// Oleksiy Fedchuk
/// Updated: 2023/04/27
/// 
/// Final QA
/// </remarks>
using DataObjects;
using LogicLayer;
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
using WpfPresentation.UserControls;

namespace WpfPresentation.Fundraising
{
    /// <summary>
    /// Interaction logic for AddEditViewFundraisingCampaign.xaml
    /// </summary>
    public partial class AddEditViewFundraisingCampaignPage : Page
    {
        private static AddEditViewFundraisingCampaignPage _existingAddEditViewFundraisingCampaignPage = null;

        private MasterManager _masterManager = MasterManager.GetMasterManager();
        private WindowMode _windowMode;

        private FundraisingCampaignVM _oldFundraisingCampaignVM = null;

        public FundraisingCampaignVM FundraisingCampaign
        {
            get { return (FundraisingCampaignVM)GetValue(FundraisingCampaignProperty); }
            set { SetValue(FundraisingCampaignProperty, value); }
        }
        public static readonly DependencyProperty FundraisingCampaignProperty =
            DependencyProperty.Register("FundraisingCampaign", typeof(FundraisingCampaignVM), typeof(AddEditViewFundraisingCampaignPage), new PropertyMetadata(null));

        /// <summary>
        /// Stephen Jaurigue
        /// 2023/04/21
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/27
        /// 
        /// Final QA
        /// </remarks>
        private AddEditViewFundraisingCampaignPage()
        {
            DataContext = this;
            InitializeComponent();
                
        }

        /// <summary>
        /// Stephen Jaurigue
        /// Created: 2023/02/28
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/27
        /// 
        /// Final QA
        /// </remarks>
        /// <returns>An empty page to create a new fundraising campaign</returns>
        public static AddEditViewFundraisingCampaignPage GetAddFundraisingCampaignPage()
        {
            if (_existingAddEditViewFundraisingCampaignPage == null)
            {
                _existingAddEditViewFundraisingCampaignPage = new AddEditViewFundraisingCampaignPage();
            }
            _existingAddEditViewFundraisingCampaignPage.SetupNewFundraisingCampaign();
            return _existingAddEditViewFundraisingCampaignPage;
        }

        /// <summary>
        /// Stephen Jaurigue
        /// Created: 2023/02/28
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/27
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="fundraisingCampaign">The fundraising campaign to edit</param>
        /// <returns>a new or existing fundraising campaign page set up to edit the campaign</returns>
        public static AddEditViewFundraisingCampaignPage GetEditFundraisingCampaignPage(FundraisingCampaignVM fundraisingCampaign)
        {
            try
            {
                fundraisingCampaign.Sponsors = MasterManager.GetMasterManager().InstitutionalEntityManager.RetrieveFundraisingSponsorsByCampaignId(fundraisingCampaign.FundraisingCampaignId);
            }
            catch (Exception ex)
            {
                PromptWindow.ShowPrompt("Error", ex.Message);
                return null;
            }
            if (_existingAddEditViewFundraisingCampaignPage == null)
            {
                _existingAddEditViewFundraisingCampaignPage = new AddEditViewFundraisingCampaignPage();
            }
            _existingAddEditViewFundraisingCampaignPage.SetupEditFundraisingCampaign(fundraisingCampaign);
            return _existingAddEditViewFundraisingCampaignPage;
        }


        /// <summary>
        /// Stephen Jaurigue
        /// Created: 2023/02/28
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/27
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="fundraisingCampaign">The fundraising campaign to view</param>
        /// <returns>a new or existing fundraising campaign page set up to view the campaign</returns>
        public static AddEditViewFundraisingCampaignPage GetViewFundraisingCampaignPage(FundraisingCampaignVM fundraisingCampaign)
        {
            try
            {
                fundraisingCampaign.Sponsors = MasterManager.GetMasterManager().InstitutionalEntityManager.RetrieveFundraisingSponsorsByCampaignId(fundraisingCampaign.FundraisingCampaignId);
            }
            catch (Exception ex)
            {
                PromptWindow.ShowPrompt("Error", ex.Message);
                return null;
            }
            if (_existingAddEditViewFundraisingCampaignPage == null)
            {
                _existingAddEditViewFundraisingCampaignPage = new AddEditViewFundraisingCampaignPage();
            }
            _existingAddEditViewFundraisingCampaignPage.SetupViewFundraisingCampaign(fundraisingCampaign);
            return _existingAddEditViewFundraisingCampaignPage;
        }

        /// <summary>
        /// Author: Stephen Jaurigue
        /// 2023/04/21
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/27
        /// 
        /// Final QA
        /// </remarks>
        private void HideErrors()
        {
            lblTitleError.Visibility = Visibility.Collapsed;
            lblDescriptionError.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Author: Stephen Jaurigue
        /// Date: 2023/04/21
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/27
        /// 
        /// Final QA
        /// </remarks>
        private void SetupNewFundraisingCampaign()
        {
            _windowMode = WindowMode.New;
            HideErrors();
            lblHeader.Content = "New Fundraising Campaign";
            FundraisingCampaign = new FundraisingCampaignVM()
            {
                UsersId = _masterManager.User.UsersId,
                ShelterId = _masterManager.User.ShelterId.Value,
                Sponsors = new List<InstitutionalEntity>(),
                Active = true
            };
            dpStartDate.DisplayDateStart = DateTime.Today;
            AddEditMode();
            stackEditClose.IsEnabled = false;
            stackEditClose.Visibility = Visibility.Collapsed;
            stackSaveCancel.IsEnabled = true;
            stackSaveCancel.Visibility = Visibility.Visible;
            ClearAndPopulateSponsors();
        }

        /// <summary>
        /// Author: Stephen Jaurigue
        /// Date: 2023/04/21
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/27
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="fundraisingCampaign"></param>
        private void SetupViewFundraisingCampaign(FundraisingCampaignVM fundraisingCampaign)
        {
            _windowMode = WindowMode.View;
            HideErrors();
            lblHeader.Content = "View Fundraising Campaign";
            FundraisingCampaign = fundraisingCampaign;
            ViewMode();
            stackEditClose.IsEnabled = true;
            stackEditClose.Visibility = Visibility.Visible;
            stackSaveCancel.IsEnabled = false;
            stackSaveCancel.Visibility = Visibility.Collapsed;
            ClearAndPopulateSponsors();
        }

        /// <summary>
        /// Author: Stephen Jaurigue
        /// Date: 2023/04/21
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/27
        /// 
        /// Final QA
        /// </remarks>
        private void ClearAndPopulateSponsors()
        {
            stackSponsors.Children.Clear();
            foreach (var sponsor in FundraisingCampaign.Sponsors)
            {
                var sponsorControl = new InstitutionalEntityUserControl(sponsor, _windowMode != WindowMode.View, false);
                sponsorControl.btnRemove.Click += (sender, args) => btnRemoveSponsor_Click(sender, args, sponsor);
                stackSponsors.Children.Add(sponsorControl);
            }
        }

        /// <summary>
        /// Author: Stephen Jaurigue
        /// Date: 2023/04/21
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/27
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="institutionalEntity"></param>
        private void btnRemoveSponsor_Click(object sender, RoutedEventArgs e, InstitutionalEntity institutionalEntity)
        {
            FundraisingCampaign.Sponsors.Remove(institutionalEntity);
            ClearAndPopulateSponsors();
        }

        /// <summary>
        /// Author: Stephen Jaurigue
        /// Date: 2023/04/21
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/27
        /// 
        /// Final QA
        /// </remarks>
        private void ViewMode()
        {
            tbTitle.IsReadOnly = true;
            tbDescription.IsReadOnly = true;
            dpEndDate.IsEnabled = false;
            dpStartDate.IsEnabled = false;
            btnAddSponsors.IsEnabled = false;
            btnCancel.IsCancel = false;
            btnClose.IsCancel = true;
            btnSave.IsDefault = false;
            btnEdit.IsDefault = true;
        }

        /// <summary>
        /// Author: Stephen Jaurigue
        /// Date: 2023/04/21
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/27
        /// 
        /// Final QA
        /// </remarks>
        private void AddEditMode()
        {
            tbTitle.IsReadOnly = false;
            tbDescription.IsReadOnly = false;
            dpEndDate.IsEnabled = true;
            dpStartDate.IsEnabled = true;
            btnAddSponsors.IsEnabled = true;
            btnClose.IsCancel = false;
            btnCancel.IsCancel = true;
            btnEdit.IsDefault = false;
            btnSave.IsDefault = false;
            tbTitle.Focus();
            tbTitle.SelectAll();
        }

        /// <summary>
        /// Author: Stephen Jaurigue
        /// Date: 2023/04/21
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/27
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="fundraisingCampaign"></param>
        private void SetupEditFundraisingCampaign(FundraisingCampaignVM fundraisingCampaign)
        {
            _windowMode = WindowMode.Edit;
            HideErrors();
            lblHeader.Content = "Edit Fundraising Campaign";
            FundraisingCampaign = fundraisingCampaign.Copy();
            _oldFundraisingCampaignVM = fundraisingCampaign;

            AddEditMode();
            stackEditClose.IsEnabled = false;
            stackEditClose.Visibility = Visibility.Collapsed;
            stackSaveCancel.IsEnabled = true;
            stackSaveCancel.Visibility = Visibility.Visible;
            ClearAndPopulateSponsors();
        }

        /// <summary>
        /// Author: Stephen Jaurigue
        /// Date: 2023/04/21
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/27
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dpStartDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            dpEndDate.DisplayDateStart = dpStartDate.SelectedDate;
            if (dpEndDate.SelectedDate <= dpStartDate.SelectedDate)
            {
                dpEndDate.SelectedDate = null;
            }
        }

        /// <summary>
        /// Author: Stephen Jaurigue
        /// Date: 2023/04/21
        /// </summary>
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
            switch(_windowMode)
            {
                case WindowMode.New:
                    NavigationService.Navigate(ViewCampaignsPage.GetViewCampaignsPage());
                    break;
                case WindowMode.Edit:
                    SetupViewFundraisingCampaign(_oldFundraisingCampaignVM);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Author: Stephen Jaurigue
        /// Date: 2023/04/21
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/27
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateFundraisingCampaign())
            {
                return;
            }
            try
            {
                switch (_windowMode)
                {
                    case WindowMode.New:
                        if (!_masterManager.FundraisingCampaignManager.AddFundraisingCampaign(FundraisingCampaign))
                        {
                            PromptWindow.ShowPrompt("Error", "The fundraising campaign was not added");
                            break;
                        }
                        SetupViewFundraisingCampaign(FundraisingCampaign);
                        break;
                    case WindowMode.Edit:
                        if (!FundraisingCampaign.Active && PromptSelection.Yes == PromptWindow.ShowPrompt("Deleted Record", "This record has been deleted, would you like to restore it?", ButtonMode.YesNo))
                        {
                            FundraisingCampaign.Active = true;
                        }
                        if (!_masterManager.FundraisingCampaignManager.EditFundraisingCampaignDetails(_oldFundraisingCampaignVM, FundraisingCampaign))
                        {
                            PromptWindow.ShowPrompt("Error", "The fundraising campaign was not changed");
                            // reload data because there was a concurrency issue
                            ReloadFundraisingCampaignDataAndReturnToViewMode();
                            break;
                        }
                        SetupViewFundraisingCampaign(FundraisingCampaign);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                PromptWindow.ShowPrompt("Error", ex.Message + "\n\n" + ex.InnerException.Message);
            }
            RefreshUI();
        }
        /// <summary>
        /// Author: Stephen Jaurigue
        /// Date: 2023/04/21
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/27
        /// 
        /// Final QA
        /// </remarks>
        /// <returns></returns>
        private bool ValidateFundraisingCampaign()
        {
            bool isValid = true;
            if (!FundraisingCampaign.Title.IsValidTitle())
            {
                lblTitleError.Visibility = Visibility.Visible;
                isValid = false;
            }
            else
            {
                lblTitleError.Visibility = Visibility.Collapsed;
            }

            if (!FundraisingCampaign.Description.IsValidRequiredShortDescription())
            {
                lblDescriptionError.Visibility = Visibility.Visible;
                isValid = false;
            }
            else
            {
                lblDescriptionError.Visibility = Visibility.Collapsed;
            }
            return isValid;
        }

        /// <summary>
        /// Author: Stephen Jaurigue
        /// Date: 2023/04/21
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/27
        /// 
        /// Final QA
        /// </remarks>
        private void ReloadFundraisingCampaignDataAndReturnToViewMode()
        {
            try
            {
                var reloadedCampaign = _masterManager.FundraisingCampaignManager.RetrieveFundraisingCampaignByFundraisingCampaignId(_oldFundraisingCampaignVM.FundraisingCampaignId);
                reloadedCampaign.Sponsors = _masterManager.InstitutionalEntityManager.RetrieveFundraisingSponsorsByCampaignId(reloadedCampaign.FundraisingCampaignId);
                SetupViewFundraisingCampaign(reloadedCampaign);
            }
            catch (Exception ex)
            {
                PromptWindow.ShowPrompt("Error", "Failed to reload campaign data \n\n" + ex.Message);
            }
        }

        /// <summary>
        /// Stephen Jaurigue
        /// Created: 2023/03/02
        /// 
        /// fakes a change in the Fundraising campaign property to trigger a redraw on all bindings in wpf
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/27
        /// 
        /// Final QA
        /// </remarks>
        private void RefreshUI()
        {
            var temp = FundraisingCampaign;
            FundraisingCampaign = null;
            FundraisingCampaign = temp;
        }

        /// <summary>
        /// Author: Stephen Jaurigue
        /// Date: 2023/04/21
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/27
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            SetupEditFundraisingCampaign(FundraisingCampaign);
        }

        /// <summary>
        /// Author: Stephen Jaurigue
        /// Date: 2023/04/21
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/27
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(ViewCampaignsPage.GetViewCampaignsPage());
        }

        /// <summary>
        /// Author: Stephen Jaurigue
        /// Date: 2023/04/21
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/27
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddSponsors_Click(object sender, RoutedEventArgs e)
        {
            AddFundraisingCampaignSponsorsWindow addFundraisingCampaignSponsorsWindow = new AddFundraisingCampaignSponsorsWindow(FundraisingCampaign.Sponsors);
            addFundraisingCampaignSponsorsWindow.Owner = Window.GetWindow(this);
            addFundraisingCampaignSponsorsWindow.ShowDialog();
            ClearAndPopulateSponsors();
        }
    }

    enum WindowMode
    {
        New,
        Edit,
        View
    }
}
