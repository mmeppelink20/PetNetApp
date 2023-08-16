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
using WpfPresentation.Management;
using WpfPresentation.Management.Inventory;
using WpfPresentation.Management.Inventory.Library;

namespace WpfPresentation.Management
{
    /// <summary>
    /// Interaction logic for ManagementPage.xaml
    /// </summary>
    public partial class ManagementPage : Page
    {
        private static ManagementPage _existingManagementPage = null;

        private MasterManager _manager = MasterManager.GetMasterManager();
        private Button[] _managementPageButtons;

        static ManagementPage()
        {
            MasterManager manager = MasterManager.GetMasterManager();
            manager.UserLogin += () => _existingManagementPage?.ShowButtonsByRole();
            manager.UserLogout += () =>
            {
                _existingManagementPage?.HideAllButtons();
                _existingManagementPage?.frameManagement.Navigate(null);
            };
        }

        public static ManagementPage GetManagementPage(MasterManager manager)
        {
            if (_existingManagementPage == null)
            {
                _existingManagementPage = new ManagementPage(manager);
            }
            return _existingManagementPage;
        }

        public ManagementPage()
        {
            InitializeComponent();
            _managementPageButtons = new Button[] { btnInventory, btnKennel, btnTickets, btnVolunteer, btnSchedule };
        }

        public ManagementPage(MasterManager manager)
        {
            InitializeComponent();
            _managementPageButtons = new Button[] { btnInventory, btnKennel, btnTickets, btnVolunteer, btnSchedule };
            _manager = manager;
        }

        public void HideAllButtons()
        {
            UnselectAllButtons();
            foreach (Button btn in _managementPageButtons)
            {
                btn.Visibility = Visibility.Collapsed;
            }
        }
        public void ShowButtonsByRole()
        {
            HideAllButtons();
            ShowInventoryButtonByRole();
            ShowKennelButtonByRole();
            ShowScheduleButtonByRole();
            ShowTicketsButtonByRole();
            ShowVolunteerButtonByRole();
        }
        public void ShowInventoryButtonByRole()
        {
            string[] allowedRoles = { "Admin", "Manager", "Employee" };
            if (_manager.User.Roles.Exists(role => allowedRoles.Contains(role)))
            {
                btnInventory.Visibility = Visibility.Visible;
            }
        }
        public void ShowKennelButtonByRole()
        {
            string[] allowedRoles = { "Admin", "Manager", "Maintenance" };
            if (_manager.User.Roles.Exists(role => allowedRoles.Contains(role)))
            {
                btnKennel.Visibility = Visibility.Visible;
            }
        }

        public void ShowTicketsButtonByRole()
        {
            string[] allowedRoles = { "Admin", "Manager", "Helpdesk", "Maintenance" };
            if (_manager.User.Roles.Exists(role => allowedRoles.Contains(role)))
            {
                btnTickets.Visibility = Visibility.Visible;
            }
        }
        public void ShowVolunteerButtonByRole()
        {
            string[] allowedRoles = { "Admin", "Manager" };
            if (_manager.User.Roles.Exists(role => allowedRoles.Contains(role)))
            {
                btnVolunteer.Visibility = Visibility.Visible;
            }
        }
        public void ShowScheduleButtonByRole()
        {
            string[] allowedRoles = { "Admin", "Manager", "Employee" };
            if (_manager.User.Roles.Exists(role => allowedRoles.Contains(role)))
            {
                btnSchedule.Visibility = Visibility.Visible;
            }
        }
        public static ManagementPage GetManagementPage()
        {
            if (_existingManagementPage == null)
            {
                _existingManagementPage = new ManagementPage();
                _existingManagementPage.ShowButtonsByRole();
            }
            return _existingManagementPage;
        }
        public void ChangeSelectedButton(Button selectedButton)
        {
            UnselectAllButtons();
            selectedButton.Style = (Style)Application.Current.Resources["rsrcSelectedButton"];
        }
        private void UnselectAllButtons()
        {
            foreach (Button button in _managementPageButtons)
            {
                button.Style = (Style)Application.Current.Resources["rsrcUnselectedButton"];
            }
        }
        private void btnInventory_Click(object sender, RoutedEventArgs e)
        {
            ChangeSelectedButton(btnInventory);
            frameManagement.Navigate(InventoryNavigationPage.GetInventoryNavigationPage(_manager));
        }
        private void btnTickets_Click(object sender, RoutedEventArgs e)
        {
            ChangeSelectedButton(btnTickets);
            // replace with page name and then delete comment
            frameManagement.Navigate(new ViewTicketList(_manager));
        }
        private void btnKennel_Click(object sender, RoutedEventArgs e)
        {
            ChangeSelectedButton(btnKennel);
            // replace with page name and then delete comment
            frameManagement.Navigate(new ViewKennelPage());
        }
        private void btnVolunteer_Click(object sender, RoutedEventArgs e)
        {
            ChangeSelectedButton(btnVolunteer);
            // replace with page name and then delete comment
            frameManagement.Navigate(new Management.VolunteerManagment());
            
        }
        private void btnSchedule_Click(object sender, RoutedEventArgs e)
        {
            ChangeSelectedButton(btnSchedule);
            // replace with page name and then delete comment
            frameManagement.Navigate(new Management.SchedulePage());
        }
        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scrollviewer = sender as ScrollViewer;
            if (e.Delta > 0)
            {
                scrollviewer.LineLeft();
            }
            else
            {
                scrollviewer.LineRight();
            }
            e.Handled = true;
        }
         private void svManagementPageTabs_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            UpdateScrollButtons();
        }
        private void UpdateScrollButtons()
        {
            if (svManagementPageTabs.HorizontalOffset > svManagementPageTabs.ScrollableWidth - 0.05)
            {
                btnScrollRight.Visibility = Visibility.Hidden;
            }
            else
            {
                btnScrollRight.Visibility = Visibility.Visible;
            }

            if (svManagementPageTabs.HorizontalOffset < 0.05)
            {
                btnScrollLeft.Visibility = Visibility.Hidden;
            }
            else
            {
                btnScrollLeft.Visibility = Visibility.Visible;
            }
        }
        private void btnScrollRight_Click(object sender, RoutedEventArgs e)
        {
            svManagementPageTabs.ScrollToHorizontalOffset(svManagementPageTabs.HorizontalOffset + 130);
        }
        private void btnScrollLeft_Click(object sender, RoutedEventArgs e)
        {
            svManagementPageTabs.ScrollToHorizontalOffset(svManagementPageTabs.HorizontalOffset - 130);
        }

    }
}
