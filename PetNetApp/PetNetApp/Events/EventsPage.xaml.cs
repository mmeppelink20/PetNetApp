/// <summary>
/// Oleksiy Fedchuk
/// Created: 2023/02/23
/// 
/// The is the navigation bar for the Events tab
/// </summary>
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
    /// Interaction logic for EventsPage.xaml
    /// </summary>
    public partial class EventsPage : Page
    {
        private static EventsPage _existingEventsPage = null;

        private MasterManager _manager = MasterManager.GetMasterManager();
        private Button[] _eventsTabButtons;

        /// <summary>
        /// Oleksiy Fedchuk
        /// Created: 2023/02/23
        /// 
        /// Initializes the page and added the tab buttons
        /// </summary>
        public EventsPage()
        {
            InitializeComponent();
            _eventsTabButtons = new Button[] { btnEvents, btnCreateEvents, btnEventResults };
        }
        /// <summary>
        /// Oleksiy Fedchuk
        /// Created: 2023/02/23
        /// 
        /// Gets a new eventsPage if it does not exist yet, otherwise, uses the existing events page
        /// </summary>
        /// <returns>EventsPage</returns>
        public static EventsPage GetEventsPage()
        {
            if (_existingEventsPage == null)
            {
                _existingEventsPage = new EventsPage();
            }
            return _existingEventsPage;
        }
        /// <summary>
        /// Oleksiy Fedchuk
        /// Created: 2023/02/23
        /// 
        /// Changes the selected button to the button selected
        /// </summary>
        /// <param name="selectedButton"></param>
        private void ChangeSelectedButton(Button selectedButton)
        {
            UnselectAllButtons();
            selectedButton.Style = (Style)Application.Current.Resources["rsrcSelectedButton"];
        }
        /// <summary>
        /// Oleksiy Fedchuk
        /// Created: 2023/02/23
        /// 
        /// </summary>
        private void UnselectAllButtons()
        {
            foreach (Button button in _eventsTabButtons)
            {
                button.Style = (Style)Application.Current.Resources["rsrcUnselectedButton"];
            }
        }
        /// <summary>
        /// Oleksiy Fedchuk
        /// Created: 2023/02/23
        /// 
        /// Vertical scroll for if the tab buttons are not visable
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
        /// Oleksiy Fedchuk
        /// Created: 2023/02/23
        /// 
        /// </summary>
        private void UpdateScrollButtons()
        {
            if (svEventsTabs.HorizontalOffset > svEventsTabs.ScrollableWidth - 0.05)
            {
                btnScrollRight.Visibility = Visibility.Hidden;
            }
            else
            {
                btnScrollRight.Visibility = Visibility.Visible;
            }

            if (svEventsTabs.HorizontalOffset < 0.05)
            {
                btnScrollLeft.Visibility = Visibility.Hidden;
            }
            else
            {
                btnScrollLeft.Visibility = Visibility.Visible;
            }
        }

        private void svEventTabs_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            UpdateScrollButtons();
        }

        private void btnScrollRight_Click(object sender, RoutedEventArgs e)
        {
            svEventsTabs.ScrollToHorizontalOffset(svEventsTabs.HorizontalOffset + 130);
        }

        private void btnScrollLeft_Click(object sender, RoutedEventArgs e)
        {
            svEventsTabs.ScrollToHorizontalOffset(svEventsTabs.HorizontalOffset - 130);
        }

        private void btnEvents_Click(object sender, RoutedEventArgs e)
        {
            ChangeSelectedButton((Button)sender);
            frameEvents.Navigate(new ViewEvents(_manager));
        }

        private void btnCreateEvents_Click(object sender, RoutedEventArgs e)
        {
            ChangeSelectedButton((Button)sender);
            frameEvents.Navigate(new AddFundraisingEvent());
        }

        private void btnEventResults_Click(object sender, RoutedEventArgs e)
        {
            ChangeSelectedButton((Button)sender);

            frameEvents.Navigate(Events.ViewEventGraphsPage.GetViewEventGraphsPage());
        }
    }
}
