/// <summary>
/// Oleksiy Fedchuk
/// Created: 2023/04/20
/// 
/// </summary>
/// <remarks>
/// Zaid Rachman
/// Updated: 2023/04/24
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

namespace WpfPresentation.Events
{
    /// <summary>
    /// Interaction logic for AddVolunteerToEvent.xaml
    /// </summary>
    public partial class AddVolunteerToEvent : Page
    {
        private MasterManager _masterManager = MasterManager.GetMasterManager();
        private static AddVolunteerToEvent _existingVolunteerList = null;
        private int _eventId = 0;
        /// <summary>
        /// Oleksiy Fedchuk
        /// Created: 2023/04/20
        /// 
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/24
        /// 
        /// Final QA
        /// </remarks>
        public AddVolunteerToEvent()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Oleksiy Fedchuk
        /// Created: 2023/04/21
        /// 
        /// Gets the AddVolunteerToEvent list
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/24
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public static AddVolunteerToEvent GetAddVolunteerToEvent(int eventId)
        {
            if(_existingVolunteerList == null)
            {
                _existingVolunteerList = new AddVolunteerToEvent();
            }
            _existingVolunteerList._eventId = eventId;

            return _existingVolunteerList;
        }
        /// <summary>
        /// Oleksiy Fedchuk
        /// Created: 2023/04/21
        /// 
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/24
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            PopulatePage();
        }
        /// <summary>
        /// Oleksiy Fedchuk
        /// Created: 2023/04/20
        /// 
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/24
        /// 
        /// Final QA
        /// </remarks>
        private void PopulatePage()
        {
            List<Users> users = new List<Users>();
            try
            {
                List<VolunteerVM> volunteerList = _masterManager.VolunteerManager.RetrieveVolunteersbyFundraisingEventId(_eventId);
                List<int> allVolunteerIds = _masterManager.VolunteerManager.RetrieveAllVolunteers();

                if(volunteerList.Count == 0)
                {
                    foreach (int integer in allVolunteerIds)
                    {
                        users.Add(_masterManager.UsersManager.RetrieveUserByUsersId(integer));
                    }
                }
                else if (volunteerList.Count > 0)
                {
                    foreach (int ids in allVolunteerIds)
                    {
                        bool flag = false;
                        foreach (VolunteerVM volunteer in volunteerList)
                        {
                            if (ids == volunteer.UsersId)
                            {
                                flag = true;
                                break;
                            }
                        }
                        if (flag == false)
                        {
                            users.Add(_masterManager.UsersManager.RetrieveUserByUsersId(ids));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            datVolunteerList.ItemsSource = users;
        }
        /// <summary>
        /// Oleksiy Fedchuk
        /// Created: 2023/04/21
        /// 
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/24
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (datVolunteerList.SelectedItem != null)
            {
                var tempUser = (Users)datVolunteerList.SelectedItem;
                try
                {
                    _masterManager.VolunteerManager.AddVolunteerToEventbyVolunteerAndEventId(tempUser.UsersId, _eventId);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                NavigationService.Navigate(VolunteerListPage.GetVolunteerListPage(_eventId));
            }
        }
        /// <summary>
        /// Oleksiy Fedchuk
        /// Created: 2023/04/21
        /// 
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/24
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(VolunteerListPage.GetVolunteerListPage(_eventId));
        }
    }
}
