/// <summary>
/// Chris Dreismeier
/// Created: 2023/02/09
/// 
/// 
/// Class for interationg with the full schedule 
/// </summary>
/// 
/// <remarks>
/// Updater Name
/// Updated: yyyy/mm/dd
/// </remarks>

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
using LogicLayer;
using DataObjects;

namespace WpfPresentation.Management
{
    /// <summary>
    /// Interaction logic for SchedulePage.xaml
    /// </summary>
    public partial class SchedulePage : Page
    {
        public DateTime _selectedDate;
        public MasterManager _masterManager = MasterManager.GetMasterManager();
        public SchedulePage()
        {
            InitializeComponent();
            LoadCmbBox();
            btnDeleteSchedule.Visibility = Visibility.Hidden;
        }

        public SchedulePage(UsersVM user)
        {
            InitializeComponent();
            LoadCmbBox();
            CboVolunteers.SelectedValue = user.UsersId;
            PopulateDatGridByUserId(user);
            btnDeleteSchedule.Visibility = Visibility.Hidden;
        }




        private void date_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {  
            if ( date.SelectedDate != null)
            {
                PopulateDatGridByDate((DateTime)date.SelectedDate);
               
            }
        }
        private void CboVolunteers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UsersVM user = (UsersVM)CboVolunteers.SelectedItem;
            PopulateDatGridByUserId(user);
        }
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            CboVolunteers.SelectedItem = null;
            datScheduledPerson.ItemsSource = null;
            hideShowDeleteButton();
        }
        private void btnAddSchedule_Click(object sender, RoutedEventArgs e)
        {
            UsersVM selectedUser = (UsersVM)CboVolunteers.SelectedItem;

            if(CboVolunteers.SelectedItem == null)
            {
                PromptWindow.ShowPrompt("Error", "No user selected \n please select a user.");

            }
            else
            {
                var addEditSchedule = new AddEditSchedule(selectedUser);
                addEditSchedule.Owner = Window.GetWindow(this);
                if ((bool)addEditSchedule.ShowDialog())
                {
                    PopulateDatGridByUserId(selectedUser);
                }

            }
            
        }

        private void btnEditSchedule_Click(object sender, RoutedEventArgs e)
        {
            ScheduleVM schedule = (ScheduleVM)datScheduledPerson.SelectedItem;
            if(datScheduledPerson.SelectedItem == null)
            {
                PromptWindow.ShowPrompt("Error", "No schedule selected \n please select a schedule.");
            }
            else
            {
                var addEditSchedule = new AddEditSchedule(schedule);
                addEditSchedule.Owner = Window.GetWindow(this);
                if ((bool)addEditSchedule.ShowDialog())
                {
                    if((UsersVM)CboVolunteers.SelectedItem != null)
                    {
                        PopulateDatGridByUserId((UsersVM)CboVolunteers.SelectedItem);
                    }
                    else
                    {
                        PopulateDatGridByDate((DateTime)date.SelectedDate);
                    }
                    
                }
                else
                {
                    
                }
            }
        }

        private void btnDeleteSchedule_Click(object sender, RoutedEventArgs e)
        {
            ScheduleVM scheduleVMToDelete = (ScheduleVM)datScheduledPerson.SelectedItem;
            try
            {
                _masterManager.ScheduleManager.DeleteScheduleVM(scheduleVMToDelete.ScheduleId);
            }
            catch (Exception ex)
            {
                PromptWindow.ShowPrompt("Error", ex.Message);
            }
            if ((UsersVM)CboVolunteers.SelectedItem != null)
            {
                PopulateDatGridByUserId((UsersVM)CboVolunteers.SelectedItem);
            }
            else
            {
                PopulateDatGridByDate((DateTime)date.SelectedDate);
            }

        }

        private void datScheduledPerson_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            hideShowDeleteButton();
        }

        private void LoadCmbBox()
        {

            try
            {
                CboVolunteers.ItemsSource = _masterManager.UsersManager.RetrieveUserByRole("Volunteer", 100000);
                CboVolunteers.SelectedValuePath = "UsersId";
            }
            catch (Exception ex)
            {

                PromptWindow.ShowPrompt("Error", ex.Message);
            }

        }
        private void PopulateDatGridByUserId(UsersVM user)
        {
            if (CboVolunteers.SelectedItem != null)
            {
                date.SelectedDate = null;
                try
                {
                    datScheduledPerson.ItemsSource = _masterManager.ScheduleManager.RetrieveScheduleByUserId(user.UsersId);
                }
                catch (Exception ex)
                {
                    PromptWindow.ShowPrompt("Error", ex.Message);
                }
            }
        }
        private void PopulateDatGridByDate(DateTime dateSelected)
        {
            CboVolunteers.SelectedItem = null;
            try
            {
                datScheduledPerson.ItemsSource = _masterManager.ScheduleManager.RetrieveScheduleByDate((DateTime)date.SelectedDate);
            }
            catch (Exception ex)
            {
                PromptWindow.ShowPrompt("Error", ex.Message);
            }
        }

        private void hideShowDeleteButton()
        {
            
            if(datScheduledPerson.SelectedItem == null)
            {
                btnDeleteSchedule.Visibility = Visibility.Hidden;
            }
            else
            {
                btnDeleteSchedule.Visibility = Visibility.Visible;
            }
        }
    }
}
