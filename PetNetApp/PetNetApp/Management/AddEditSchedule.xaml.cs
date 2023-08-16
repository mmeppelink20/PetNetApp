/// <summary>
/// Chris Dreismeier
/// Created: 2023/03/03
/// 
/// 
/// Class for creating and editing volunteer schedules
/// </summary>
///
/// <remarks>
/// Updater Name
/// Updated: yyyy/mm/dd
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
using System.Windows.Shapes;
using DataObjects;
using LogicLayer;

namespace WpfPresentation.Management
{
    /// <summary>
    /// Interaction logic for AddEditSchedule.xaml
    /// </summary>
    public partial class AddEditSchedule : Window
    {
        private UsersVM _userVM = null;
        private ScheduleVM _scheduleVM = null;
        private bool _editmode = false;
        public MasterManager _masterManager = MasterManager.GetMasterManager();
        public AddEditSchedule()
        {
            InitializeComponent();
        }
        public AddEditSchedule(UsersVM userVM)
        {
            InitializeComponent();
            _userVM = userVM;
            _scheduleVM = new ScheduleVM();
        }

        public AddEditSchedule(ScheduleVM scheduleVM)
        {
            InitializeComponent();
            _scheduleVM = scheduleVM;
            _editmode = true;
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            dtpStart.Minimum = DateTime.Today;
            dtpEnd.Minimum = DateTime.Today;
            
            if(!_editmode)
            {
                lblUsersName.Content = _userVM.GivenName + " " + _userVM.FamilyName;
            }
            else
            {
                lblUsersName.Content = _scheduleVM.GivenName + " " + _scheduleVM.FamilyName;
                dtpStart.Value = _scheduleVM.StartTime;
                dtpEnd.Value = _scheduleVM.EndTime;
                btnAdd.Content = "Save";
            }
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            
            if(PromptWindow.ShowPrompt("Close", "Are you sure you want to cancel?", ButtonMode.YesNo) == PromptSelection.Yes)
            {
                this.Close();
            }
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if(dtpStart.Value == null)
            {
                PromptWindow.ShowPrompt("Missing Input", "You need to select a start date", ButtonMode.Ok);
                return;
            }

            if(dtpEnd.Value == null)
            {
                PromptWindow.ShowPrompt("Missing Input", "You need to select an end date", ButtonMode.Ok);
                return;
            }

            if(dtpEnd.Value < dtpStart.Value)
            {
                PromptWindow.ShowPrompt("Input Error", "End date cannot be before start date", ButtonMode.Ok);
                return;
            }

            if(dtpStart.Value == dtpEnd.Value)
            {
                PromptWindow.ShowPrompt("Input Error", "Start and end cannot be the same", ButtonMode.Ok);
                return;
            }
            DateTime startTime = (DateTime)dtpStart.Value;
            DateTime endTime = (DateTime)dtpEnd.Value;



            if (_editmode)
            {
                ScheduleVM oldSchedule = new ScheduleVM();
                oldSchedule.StartTime = _scheduleVM.StartTime;
                oldSchedule.EndTime = _scheduleVM.EndTime;

                _scheduleVM.StartTime = (DateTime)dtpStart.Value;
                _scheduleVM.EndTime = (DateTime)dtpEnd.Value;
                try
                {
                    if(_masterManager.ScheduleManager.EditScheduleVM(oldSchedule, _scheduleVM))
                    {
                        this.DialogResult = true;
                    }
                    
                }
                catch (Exception ex)
                {
                    PromptWindow.ShowPrompt("Error", ex.Message, ButtonMode.Ok);
                    this.DialogResult = false;
                }

            }
            else
            {
                _scheduleVM.StartTime = startTime;
                _scheduleVM.EndTime = endTime;
                _scheduleVM.UserId = _userVM.UsersId;
                try
                {
                    if(_masterManager.ScheduleManager.AddSchedulebyUserId(_scheduleVM))
                    {
                        this.DialogResult = true;
                    }
                }
                catch (Exception ex)
                {
                    PromptWindow.ShowPrompt("Error", ex.Message, ButtonMode.Ok);
                    this.DialogResult = false;
                }
            }
        }
    }
}
