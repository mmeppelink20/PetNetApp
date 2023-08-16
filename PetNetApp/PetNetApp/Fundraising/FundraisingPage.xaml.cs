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
using DataObjects;

namespace WpfPresentation.Fundraising
{
    /// <summary>
    /// Interaction logic for ManagementPage.xaml
    /// </summary>
    public partial class FundraisingPage : Page
    {
        private static FundraisingPage _existingFundraisingPage = null;
      

        private MasterManager _manager = null;
        private Button[] _fundraisingPageButtons;
        /// <summary>
        /// Author: Stephen Jaurigue
        /// Date: 2023/04/21
        /// </summary>
        static FundraisingPage()
        {
            MasterManager manager = MasterManager.GetMasterManager();
            manager.UserLogin += () => _existingFundraisingPage?.ShowButtonsByRole();
            manager.UserLogout += () =>
            {
                _existingFundraisingPage?.HideAllButtons();
                _existingFundraisingPage?.frameFundraising.Navigate(null);
            };
        }
        /// <summary>
        /// Author: Stephen Jaurigue
        /// Date: 2023/04/21
        /// </summary>
        /// <param name="manager"></param>
        private FundraisingPage(MasterManager manager)
        {
            InitializeComponent();
            _manager = manager;
            _fundraisingPageButtons = new Button[] { btnCampaigns, btnDonations, btnViewContacts, btnEvents, btnViewSponsors, btnHosts };
        }

        /// <summary>
        /// Stephen Jaurigue
        /// Created: 2023/02/20
        /// 
        /// Returns existing FundraisingPage or new if none exists
        /// </summary>
        /// <param name="manager">The existing instance of the master manager that the program is using</param>
        public static FundraisingPage GetFundraisingPage(MasterManager manager)
        {
            if (_existingFundraisingPage == null)
            {
                _existingFundraisingPage = new FundraisingPage(manager);
                _existingFundraisingPage.ShowButtonsByRole();
            }
            return _existingFundraisingPage;
        }
        /// <summary>
        /// Author: Stephen Jaurigue
        /// Date: 2023/04/21
        /// </summary>
        /// <param name="selectedButton"></param>
        public void ChangeSelectedButton(Button selectedButton)
        {
            UnselectAllButtons();
            selectedButton.Style = (Style)Application.Current.Resources["rsrcSelectedButton"];
        }
        /// <summary>
        /// Author: Stephen Jaurigue
        /// Date: 2023/04/21
        /// </summary>
        private void UnselectAllButtons()
        {
            foreach (Button button in _fundraisingPageButtons)
            {
                button.Style = (Style)Application.Current.Resources["rsrcUnselectedButton"];
            }
        }
        /// <summary>
        /// Author: Stephen Jaurigue
        /// Date: 2023/04/21
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <summary>
        /// Author: Stephen Jaurigue
        /// Date: 2023/04/21
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void svManagementPageTabs_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            UpdateScrollButtons();
        }
        /// <summary>
        /// Author: Stephen Jaurigue
        /// Date: 2023/04/21
        /// </summary>
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
        /// <summary>
        /// Author: Stephen Jaurigue
        /// Date: 2023/04/21
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnScrollRight_Click(object sender, RoutedEventArgs e)
        {
            svManagementPageTabs.ScrollToHorizontalOffset(svManagementPageTabs.HorizontalOffset + 130);
        }
        /// <summary>
        /// Author: Stephen Jaurigue
        /// Date: 2023/04/21
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnScrollLeft_Click(object sender, RoutedEventArgs e)
        {
            svManagementPageTabs.ScrollToHorizontalOffset(svManagementPageTabs.HorizontalOffset - 130);
        }
        /// <summary>
        /// Author: Stephen Jaurigue
        /// Date: 2023/04/21
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCampaigns_Click(object sender, RoutedEventArgs e)
        {
            ChangeSelectedButton(btnCampaigns);
            frameFundraising.Navigate(ViewCampaignsPage.GetViewCampaignsPage());
        }

        private void btnDonations_Click(object sender, RoutedEventArgs e)
        {
            ChangeSelectedButton(btnDonations);
            frameFundraising.Navigate(ViewDonationsPage.ExistingDonationPage);
        }
        /// <summary>
        /// Author: Stephen Jaurigue
        /// Date: 2023/04/21
        /// </summary>
        public void HideAllButtons()
        {
            UnselectAllButtons();
            foreach (Button btn in _fundraisingPageButtons)
            {
                btn.Visibility = Visibility.Collapsed;
            }
        }

        /// <remarks>
        /// Updater: Barry Mikulas
        /// Updated: 2023/03/01
        /// added call to ShowContactsButtonByRole()
        /// </remarks>
        public void ShowButtonsByRole()
        {
            HideAllButtons();
            ShowCampaignsButtonByRole();
            ShowContactsButtonByRole();
            ShowDonationsButtonByRole();
            ShowEventsButtonByRole();
            ShowSponsorsButtonByRole();
            ShowHostsButtonByRole();
        }


        /// <summary>
        /// Barry Mikulas
        /// Created: 2023/03/05
        /// 
        /// Show events button if user has appropriate permissions
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// </remarks>
        private void ShowEventsButtonByRole()
        {
            string[] allowedRoles = { "Admin", "Manager", "Marketing" };
            if (_manager.User.Roles.Exists(role => allowedRoles.Contains(role)))
            {
                btnEvents.Visibility = Visibility.Visible;
            }
        }
        public void ShowCampaignsButtonByRole()
        {
            string[] allowedRoles = { "Admin", "Manager", "Marketing" };
            if (_manager.User.Roles.Exists(role => allowedRoles.Contains(role)))
            {
                btnCampaigns.Visibility = Visibility.Visible;
            }
        }
        /// <summary>
        /// Author: Stephen Jaurigue
        /// Date: 2023/04/21
        /// </summary>
        public void ShowDonationsButtonByRole()
        {
            string[] allowedRoles = { "Admin", "Manager", "Marketing" };
            if (_manager.User.Roles.Exists(role => allowedRoles.Contains(role)))
            {
                btnDonations.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// Barry Mikulas
        /// Created: 2023/03/01
        /// 
        /// Show contacts button if user has appropriate permissions
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// </remarks>
        public void ShowContactsButtonByRole()
        {
            string[] allowedRoles = { "Admin", "Manager", "Marketing" };
            if (_manager.User.Roles.Exists(role => allowedRoles.Contains(role)))
            {
                btnViewContacts.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// William Rients
        /// Created: 2023/03/10
        /// 
        /// Show sponsors button if user has appropriate permissions
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// </remarks>
        private void ShowSponsorsButtonByRole()
        {
            string[] allowedRoles = { "Admin", "Manager", "Marketing" };
            if (_manager.User.Roles.Exists(role => allowedRoles.Contains(role)))
            {
                btnViewSponsors.Visibility = Visibility.Visible;
            }
        }


        /// <summary>
        /// Barry Mikulas
        /// Created: 2023/03/01
        /// 
        /// Redirected to view contacts page
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// </remarks>
        private void btnViewContacts_Click(object sender, RoutedEventArgs e)
        {
            ChangeSelectedButton((Button)sender);
            frameFundraising.Navigate(ViewFundraisingEventContacts.GetViewEventContacts());
        }

        /// <summary>
        /// Barry Mikulas
        /// Created: 2023/03/05
        /// 
        /// Redirected to view fundraising Events page
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// </remarks>
        private void btnEvents_Click(object sender, RoutedEventArgs e)
        {
            ChangeSelectedButton((Button)sender);
            // replace with page name and then delete comment
            frameFundraising.Navigate(WpfPresentation.Fundraising.ViewFundraisingEventsPage.GetViewFundraisingEvents());
        }

        private void btnViewSponsors_Click(object sender, RoutedEventArgs e)
        {
            ChangeSelectedButton((Button)sender);
            frameFundraising.Navigate(ViewFundraisingEventSponsors.GetViewEventSponsors());
        }

        /// <summary>
        /// Asa Armstrong
        /// Created: 2023/03/01
        /// 
        /// Show Host button if user has the role "Admin", "Manager", or "Marketing".
        /// </summary>
        ///
        /// <remarks>
        /// Asa Armstrong
        /// Updated: 2023/03/01 
        /// Created
        /// </remarks>
        public void ShowHostsButtonByRole()
        {
            string[] allowedRoles = { "Admin", "Manager", "Marketing" };
            if (_manager.User.Roles.Exists(role => allowedRoles.Contains(role)))
            {
                btnHosts.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// Asa Armstrong
        /// Created: 2023/03/01
        /// 
        /// Button click for Hosts tab button
        /// </summary>
        ///
        /// <remarks>
        /// Asa Armstrong
        /// Updated: 2023/03/01 
        /// Created
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnHosts_Click(object sender, RoutedEventArgs e)
        {
            ChangeSelectedButton((Button)sender);
            // replace with page name and then delete comment
            frameFundraising.Navigate(ViewFundraisingEventHosts.GetViewFundraisingEventHosts());
        }
    }
}
