/// <summary>
/// Ethan Kline
/// Created: 04/14/2023
/// 
/// View Events class
/// </summary>
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
using System.Windows.Shapes;
using System.Windows.Navigation;


namespace WpfPresentation.Events
{
   
    public partial class ViewEvents : Page
    {
        private MasterManager _manager;
        private List<Event> _events;
        /// <summary>
        /// Ethan Kline
        /// Created: 04/14/2023
        /// 
        /// Custum constructor for Events class that takes in a master manager
        /// </summary>
        /// <param name="masterManager"></param>
        public ViewEvents( MasterManager masterManager)
        {
            InitializeComponent();
            _manager = masterManager;
        }

        /// <summary>
        /// Ethan Kline
        /// Created: 04/14/2023
        /// logic to load the data grid with rows on startup
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            datVeiwEventsGrid.ItemsSource = null;
            try
            {
                _events = _manager.EventManager.SelectAllEvent();

            }
            catch (Exception ex)
            {
                PromptWindow.ShowPrompt("An Error occurred", ex.Message + "\n" + ex.InnerException, ButtonMode.Ok);
            }
            if (_events.Count != 0)
            {
                datVeiwEventsGrid.ItemsSource = _events;
                try
                {
                    datVeiwEventsGrid.Columns.RemoveAt(0);
                    datVeiwEventsGrid.Columns.RemoveAt(0);
                    datVeiwEventsGrid.Columns.RemoveAt(0);
                    datVeiwEventsGrid.Columns.RemoveAt(4);
                    datVeiwEventsGrid.Columns.RemoveAt(4);
                    datVeiwEventsGrid.Columns.RemoveAt(4);
                }
                catch (Exception ex)
                {
                    PromptWindow.ShowPrompt("An Error occurred", ex.Message + "\n" + ex.InnerException, ButtonMode.Ok);
                }
            }
            else
            {
                List<string> noRecordMessage = new List<string>();
                datVeiwEventsGrid.ItemsSource = noRecordMessage;
                datVeiwEventsGrid.Columns[0].Header = "No Events Available";
            }

        }
        /// <summary>
        /// Ethan Kline
        /// Created: 04/14/2023
        /// if a row is selected ask if you want to turn of visability on click
        /// </summary> 
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_delete_Click(object sender, RoutedEventArgs e)
        {
            if (datVeiwEventsGrid.SelectedItems.Count == 0)
            {
                return;
            }
            PromptSelection resalt = PromptWindow.ShowPrompt("Conferm","Are you sure you want to delete this",ButtonMode.YesNo);
            if (resalt == PromptSelection.Yes)
            {
                var ivent = (EventVM)datVeiwEventsGrid.SelectedItem;
                try
                {
                    _manager.EventManager.DeleteEventByEventId(ivent.Eventid);
                }
                catch (Exception ex)
                {

                    throw ex;
                }
                //_manager.EventManager.DeleteEventByEventId(ivent.Eventid);
                NavigationService.Navigate(new ViewEvents(_manager));
            }
          
        }
        /// <summary>
        /// Ethan Kline
        /// Created: 04/14/2023
        /// on click add an event
        /// </summary> 
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EditEventPage(_manager));
        }
        /// <summary>
        /// Ethan Kline
        /// Created: 04/14/2023
        /// if a row is selected edit a event on click
        /// </summary> 
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_edit_Click(object sender, RoutedEventArgs e)
        {
            if (datVeiwEventsGrid.SelectedItems.Count == 0)
            {
                return;
            }
            var eventVM = (EventVM)datVeiwEventsGrid.SelectedItem;
            NavigationService.Navigate(new EditEventPage(eventVM, _manager));
        }
        /// <summary>
        /// Ethan Kline
        /// Created: 04/14/2023
        /// if a row is selected edit a event on click
        /// </summary> 
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void datVeiwEventsGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var EventVM = (EventVM)datVeiwEventsGrid.SelectedItem;
            NavigationService.Navigate(new EditEventPage(EventVM, _manager));
        }
    }
}
