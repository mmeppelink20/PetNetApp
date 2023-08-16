using DataObjects;
using LogicLayer;
using PetNetApp;
//using PetNetApp;
using System;
using System.Collections.Generic;
using System.Globalization;
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
using WpfPresentation.Community;
using WpfPresentation.Fundraising;

namespace WpfPresentation.Events
{
    /// <summary>
    /// Interaction logic for AddFundraisingEvent.xaml
    /// </summary>
    public partial class AddFundraisingEvent : Page
    {
        private List<string> _hour;
        private List<string> _minutes;
        public FundraisingEvent _fundraisingEvent;
        public InstitutionalEntity host;
        private List<FundraisingCampaignVM> _fundraisingCampaigns;
        private List<InstitutionalEntity> _institutionalEntities;
        private MasterManager _masterManager = MasterManager.GetMasterManager();
        private UsersVM _users;

        public AddFundraisingEvent()
        {
            InitializeComponent();
            _hour = new List<string>() {
                "00", "01", "02", "03", "04", "05", "06",
                "07", "08", "09", "10", "11", "12"
            };
            _minutes = new List<string>()
            {
                "00", "15", "30", "45"
            };
            _fundraisingEvent = new FundraisingEvent();
            _users = _masterManager.User;
        }

        /// <summary>
        /// Author: Hoang Chu
        /// 04/21/2023
        /// </summary>
        /// <returns></returns>
        private bool CreateFundraisingEventValidator()
        {
            bool isSuccess = true;

            if (tbEventTitle.Text == "" || tbEventTitle.Text == null)
            {
                lblTitleError.Visibility = Visibility.Visible;
                return isSuccess = false;
            }
            else
            {
                lblTitleError.Visibility = Visibility.Hidden;
            }

            if (tbDescription.Text == "" || tbDescription.Text == null)
            {
                lblDescriptionError.Visibility = Visibility.Visible;
                return isSuccess = false;
            }
            else
            {
                lblDescriptionError.Visibility = Visibility.Hidden;
            }

            if (cbxAMorPM.Text == "PM" && cbxAMorPMEnd.Text == "AM")
            {
                PromptWindow.ShowPrompt("Error", "You can not pick time like that \n\n Please pick again");
                return isSuccess = false;
            }

            if (cbxAMorPM.Text == cbxAMorPMEnd.Text)
            {
                if (int.Parse(cbxStartTimeHour.Text) <= int.Parse(cbxEndTimeHour.Text))
                {
                    if (int.Parse(cbxStartTimeMinutes.Text) > int.Parse(cbxEndTimeMinutes.Text))
                    {
                        PromptWindow.ShowPrompt("Error", "You can not pick time like that \n\n Please pick again");
                        return isSuccess = false;
                    }
                }
                else
                {
                    PromptWindow.ShowPrompt("Error", "You can not pick time like that \n\n Please pick again");
                    return isSuccess = false;
                }
            }

            _fundraisingEvent.Title = tbEventTitle.Text;
            _fundraisingEvent.Description = tbDescription.Text;
            string startTime = datePicker.Text + " " + cbxStartTimeHour.Text + ":"
                + cbxStartTimeMinutes.Text + " " + cbxAMorPM.Text;
            _fundraisingEvent.StartTime = DateTime.Parse(startTime);
            string endTime = datePicker.Text + " " + cbxEndTimeHour.Text + ":"
                + cbxEndTimeMinutes.Text + " " + cbxAMorPMEnd.Text;
            _fundraisingEvent.EndTime = DateTime.Parse(endTime);
            _fundraisingEvent.UsersId = _users.UsersId;
            _fundraisingEvent.ShelterId = _users.ShelterId.Value;
            foreach (FundraisingCampaign campaign in _fundraisingCampaigns)
            {
                if (campaign.Title == cbCampaign.Text)
                {
                    _fundraisingEvent.CampaignId = campaign.FundraisingCampaignId;
                }
            }
            foreach (InstitutionalEntity h in _institutionalEntities)
            {
                if (h.CompanyName == cbHost.Text)
                {
                    host = h;
                }
            }
            return isSuccess;
        }

        /// <summary>
        /// Author: Hoang Chu
        /// 04/21/2023
        /// </summary>
        private void DateAndTimePickerStartUp()
        {
            datePicker.BlackoutDates.Add(new CalendarDateRange(DateTime.MinValue, DateTime.Now));
            datePicker.SelectedDate = DateTime.Now.AddDays(1);

            foreach (string h in _hour)
            {
                cbxStartTimeHour.Items.Add(h);
                cbxEndTimeHour.Items.Add(h);
            }
            cbxStartTimeHour.SelectedItem = "00";
            cbxEndTimeHour.SelectedItem = "00";

            foreach (string m in _minutes)
            {
                cbxStartTimeMinutes.Items.Add(m);
                cbxEndTimeMinutes.Items.Add(m);
            }
            cbxStartTimeMinutes.SelectedItem = "00";
            cbxEndTimeMinutes.SelectedItem = "00";

            cbxAMorPM.Items.Add("AM");
            cbxAMorPM.Items.Add("PM");
            cbxAMorPM.SelectedItem = "AM";
            cbxAMorPMEnd.Items.Add("AM");
            cbxAMorPMEnd.Items.Add("PM");
            cbxAMorPMEnd.SelectedItem = "PM";
        }

        /// <summary>
        /// Author: Hoang Chu
        /// 04/21/2023
        /// </summary>
        private void DisplayItemsForComboBox()
        {
            foreach (FundraisingCampaign campaign in _fundraisingCampaigns)
            {
                cbCampaign.Items.Add(campaign.Title);
            }
            cbCampaign.SelectedIndex = 0;
            foreach (InstitutionalEntity institutional in _institutionalEntities)
            {
                cbHost.Items.Add(institutional.CompanyName);
            }
            cbHost.SelectedIndex = 0;
        }

        /// <summary>
        /// Author: Hoang Chu
        /// 04/21/2023
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
           
            try
            {
                int shelterId = _users.ShelterId.Value;
                _fundraisingCampaigns = _masterManager.FundraisingCampaignManager.RetrieveAllFundraisingCampaignsByShelterId(shelterId);
                _institutionalEntities = _masterManager.InstitutionalEntityManager.RetrieveAllHosts();
            }
            catch (Exception ex)
            {
                PromptWindow.ShowPrompt("Error", ex.Message);
            }
            DateAndTimePickerStartUp();
            DisplayItemsForComboBox();
        }

        /// <summary>
        /// Author: Hoang Chu
        /// 04/21/2023
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if (CreateFundraisingEventValidator())
            {
                NavigationService.Navigate(new AddFundraisingEvent2(_fundraisingEvent, host));
            }
        }

        /// <summary>
        /// Author: Hoang Chu
        /// 04/21/2023
        /// </summary>
        /// <returns></returns>
        public FundraisingEvent GetFundraisingEvent()
        {
            return _fundraisingEvent;
        }

        /// <summary>
        /// Author: Hoang Chu
        /// 04/21/2023
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            if (PromptWindow.ShowPrompt("Cancel", "Are you really want to cancel?", ButtonMode.YesNo) 
                == PromptSelection.Yes)
            {
                var mainWindow = (MainWindow)MainWindow.GetWindow(this);
                FundraisingPage fundraisingPage = FundraisingPage.GetFundraisingPage(_masterManager);
                fundraisingPage.ChangeSelectedButton(fundraisingPage.btnEvents);
                mainWindow.ChangeSelectedButton(mainWindow.btnFundraising);
                mainWindow.frameMain.Navigate(fundraisingPage);
                fundraisingPage.frameFundraising.Navigate(ViewFundraisingEventsPage.GetViewFundraisingEvents());
            }
        }
    }
}
