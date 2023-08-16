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
using WpfPresentation.Animals;
using WpfPresentation.Community;
using WpfPresentation.Management;
using WpfPresentation.Shelters;
using LogicLayer;
using System.Diagnostics;
using WpfPresentation.Misc;
using WpfPresentation.Fundraising;
using DataObjects;
using WpfPresentation;

namespace PetNetApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Button[] _mainTabButtons;
        private MasterManager _manager = MasterManager.GetMasterManager();

        public MainWindow()
        {
            InitializeComponent();
            // Remove navigation shortcuts
            NavigationCommands.BrowseBack.InputGestures.Clear();
            NavigationCommands.BrowseForward.InputGestures.Clear();

            // things to do when someone logs in
            _manager.UserLogin += () =>
            {
                ShowButtonsByRole();
                mnuUser.Header = "Hello, " + _manager.User.GivenName;
                mnuLogout.Header = "Log Out";
                frameMain.Navigate(null);
            };
            // things to do when someone logs out
            _manager.UserLogout += () =>
            {
                HideAllButtons();
                mnuUser.Header = "Hello, Guest";
                mnuLogout.Header = "Log In";
                frameMain.Navigate(LandingPage.GetLandingPage(this));
            };
            _mainTabButtons = new Button[] { btnAnimals, btnCommunity, btnEvents, btnShelters, btnFundraising, btnManagement };
            if (_manager.User == null)
            {
                mnuLogout.Header = "Log In";
            }
            else
            {
                mnuLogout.Header = "Log Out";
            }
            ShowButtonsByRole();
        }

        public void ChangeSelectedButton(Button selectedButton)
        {
            UnselectAllButtons();
            selectedButton.Style = (Style)Resources["rsrcSelectedTabButton"];
        }

        private void UnselectAllButtons()
        {
            foreach (Button button in _mainTabButtons)
            {
                button.Style = (Style)Resources["rsrcUnselectedTabButton"];
            }
        }

        private void btnShelters_Click(object sender, RoutedEventArgs e)
        {
            ChangeSelectedButton(btnShelters);
            frameMain.Navigate(ShelterPage.GetShelterPage(_manager));
        }

        private void btnEvents_Click(object sender, RoutedEventArgs e)
        {
            ChangeSelectedButton(btnEvents);
            frameMain.Navigate(WpfPresentation.Events.EventsPage.GetEventsPage());
        }

        private void btnCommunity_Click(object sender, RoutedEventArgs e)
        {
            ChangeSelectedButton(btnCommunity);
            frameMain.Navigate(CommunityPage.GetCommunityPage());
        }

        private void btnAnimals_Click(object sender, RoutedEventArgs e)
        {
            ChangeSelectedButton(btnAnimals);
            frameMain.Navigate(AnimalsPage.GetAnimalsPage());
        }

        private void btnManagement_Click(object sender, RoutedEventArgs e)
        {
            ChangeSelectedButton(btnManagement);
            frameMain.Navigate(ManagementPage.GetManagementPage());
        }

        private void btnProfile_Click(object sender, RoutedEventArgs e)
        {
            UnselectAllButtons();
            if (_manager.User == null)
            {
                frameMain.Navigate(LogInPage.GetLogInPage());
            }
            else
            {
                frameMain.Navigate(UserProfilePage.GetUserProfilePage(_manager.User));
            }
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

        private void UpdateScrollButtons()
        {
            if (svMainTabs.HorizontalOffset > svMainTabs.ScrollableWidth - 0.05)
            {
                btnScrollRight.Visibility = Visibility.Hidden;
            }
            else
            {
                btnScrollRight.Visibility = Visibility.Visible;
            }

            if (svMainTabs.HorizontalOffset < 0.05)
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
            svMainTabs.ScrollToHorizontalOffset(svMainTabs.HorizontalOffset + 160);
        }

        private void btnScrollLeft_Click(object sender, RoutedEventArgs e)
        {
            svMainTabs.ScrollToHorizontalOffset(svMainTabs.HorizontalOffset - 160);
        }

        private void svMainTabs_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            UpdateScrollButtons();
        }

        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            btnMenu.ContextMenu.IsOpen = true;
        }

        private void mnuLogout_Click(object sender, RoutedEventArgs e)
        {
            if (_manager.User == null)
            {
                frameMain.Navigate(LogInPage.GetLogInPage());
            }
            else
            {
                PromptSelection result = PromptWindow.ShowPrompt("Log Out", "Are you sure you want to log out?", ButtonMode.YesNo);

                if (result == PromptSelection.Yes)
                {
                    _manager.User = null;

                }
            }
        }

        public void ShowButtonsByRole()
        {
            HideAllButtons();
            if (_manager.User != null)
            {
                ShowAnimalsButtonByRoles();
                ShowCommunityButtonByRoles();
                ShowEventsButtonByRoles();
                ShowFundraisingButtonByRoles();
                ShowManagementButtonByRoles();
                ShowSheltersButtonByRoles();
            }
        }
        private void ShowAnimalsButtonByRoles()
        {
            string[] allowedRoles = { "Admin", "Manager", "Employee", "Vet" };
            if (_manager.User.Roles.Exists(role => allowedRoles.Contains(role)))
            {
                btnAnimals.Visibility = Visibility.Visible;
            }
        }
        private void ShowCommunityButtonByRoles()
        {
            string[] allowedRoles = {"Admin", "Manager", "Moderator", "Helpdesk" };
            if (_manager.User.Roles.Exists(role => allowedRoles.Contains(role)))
            {
                btnCommunity.Visibility = Visibility.Visible;
            }
        }
        private void ShowEventsButtonByRoles()
        {
            string[] allowedRoles = { "Admin", "Manager", "Marketing", "Marketing"};
            if (_manager.User.Roles.Exists(role => allowedRoles.Contains(role)))
            {
                btnEvents.Visibility = Visibility.Visible;
            }
        }
        private void ShowSheltersButtonByRoles()
        {
            string[] allowedRoles = { "Admin", "Manager", "Maintenance" };
            if (_manager.User.Roles.Exists(role => allowedRoles.Contains(role)))
            {
                btnShelters.Visibility = Visibility.Visible;
            }
        }
        private void ShowFundraisingButtonByRoles()
        {
            string[] allowedRoles = { "Admin", "Manager","Marketing" };
            if (_manager.User.Roles.Exists(role => allowedRoles.Contains(role)))
            {
                btnFundraising.Visibility = Visibility.Visible;
            }
        }
        private void ShowManagementButtonByRoles()
        {
            string[] allowedRoles = { "Admin", "Manager", "Helpdesk", "Employee", "Maintenance" };
            if (_manager.User.Roles.Exists(role => allowedRoles.Contains(role)))
            {
                btnManagement.Visibility = Visibility.Visible;
            }
        }

        private void mnuAccountSettings_Click(object sender, RoutedEventArgs e)
        {
            UnselectAllButtons();
            if (_manager.User == null)
            {
                frameMain.Navigate(LogInPage.GetLogInPage());
            }
            else
            {
                frameMain.Navigate(AccountSettingsPage.GetAccountSettingsPage());
            }
        }

        private void btnPetNetLogo_Click(object sender, RoutedEventArgs e)
        {
            frameMain.Navigate(LandingPage.GetLandingPage(this));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            frameMain.Navigate(LandingPage.GetLandingPage(this));
        }
        private void HideAllButtons()
        {
            UnselectAllButtons();
            foreach (var tab in _mainTabButtons)
            {
                tab.Visibility = Visibility.Collapsed;
            }
        }
        private void btnFundraising_Click(object sender, RoutedEventArgs e)
        {
            ChangeSelectedButton((Button)sender);
            frameMain.Navigate(FundraisingPage.GetFundraisingPage(_manager));
        }
    }
}
