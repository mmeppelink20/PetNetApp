/// <summary>
/// Ethan Kline
/// Created: 04/14/2023
/// 
/// Edit Events class
/// </summary>
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
using System.Text.RegularExpressions;
using DataObjects;
using LogicLayer;

namespace WpfPresentation.Events
{
   
    /// <summary>
    /// Interaction logic for EditEventPage.xaml
    /// </summary>
    public partial class EditEventPage : Page
    {
        private MasterManager _manager;
        private EventVM _eventVM = null;
        private bool add;
        private List<EventType> _eventTypes;
        private List<Shelter> _shelters;
        /// <summary>
        /// Ethan Kline
        /// Created: 04/14/2023
        /// 
        /// generic constructor
        /// </summary>
        public EditEventPage()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Ethan Kline
        /// Created: 04/14/2023
        /// 
        /// constructor that takes a event and a master manager
        /// </summary>
        /// <param name="eventVM"></param>
        /// <param name="manager"></param>
        public EditEventPage(EventVM eventVM,MasterManager manager)
        {
            InitializeComponent();
            _eventVM = eventVM;
            _manager = manager;
            add = false;
        }
        /// <summary>
        /// Ethan Kline
        /// Created: 04/14/2023
        /// 
        /// constructor that takes a master manager
        /// </summary>
        /// <param name="manager"></param>
        public EditEventPage( MasterManager manager)
        {
            InitializeComponent();
            _manager = manager;
            add = true;
        }

        /// <summary>
        /// Ethan Kline
        /// Created: 04/14/2023
        /// 
        /// number only helper method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        /// <summary>
        /// Ethan Kline
        /// Created: 04/14/2023
        /// 
        /// on load fill the combo boxs and pre populate if editing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                _eventTypes = _manager.EventManager.SelectAllEventType();
                cboEventTypeId.ItemsSource = from t in _eventTypes select t.EventTypeId;

                _shelters = _manager.EventManager.SelectAllShelter();
                cboShelterId.ItemsSource = from s in _shelters select s.ShelterId;

                if (add == false)
                {
                    Populate();
                }
            }
            catch (Exception ex)
            {

                PromptWindow.ShowPrompt("Error","an error has occurred");
                NavigationService.GoBack();
            }


        }
        /// <summary>
        /// Ethan Kline
        /// Created: 04/14/2023
        /// 
        /// populate for edit mode
        /// </summary>
        private void Populate()
        {
            if (_eventVM != null)
            {
                Console.WriteLine(_eventVM.EventStart);
                Console.WriteLine(_eventVM.EventEnd);
                cboEventTypeId.SelectedItem = _eventVM.EventTypeid.ToString();
                cboShelterId.SelectedItem = _eventVM.Shelterid;
                txtEventTitle.Text=_eventVM.EventTitle.ToString();
                txtEventDescription.Text= _eventVM.EventDescription.ToString();

                txtEventStart.Value = _eventVM.EventStart;
                txtEventEnd.Value = _eventVM.EventEnd;

                txtEventAddress.Text=_eventVM.EventAddress.ToString();
                txtEventZipcode.Text=_eventVM.EventZipcode.ToString();
                txtZipcode.Text = _eventVM.Zipcode.ToString();
            }
        }
        /// <summary>
        /// Ethan Kline
        /// Created: 04/14/2023
        /// 
        /// if you click this you will go back to the view event page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
        /// <summary>
        /// Ethan Kline
        /// Created: 04/14/2023
        /// 
        /// this button will cheak that all the fields are filled in correctly and ether add or edit your event 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditSave_Click(object sender, RoutedEventArgs e)
        {
            if (add == false)
            {
                if (cboEventTypeId.Text.ToString() == "")
                {
                    PromptWindow.ShowPrompt("Error","You must enter a event type.");
                    txtEventStart.Focus();
                    return;
                }
                if (cboShelterId.Text.ToString() == "")
                {
                    PromptWindow.ShowPrompt("Error", "You must enter a shelter id.");
                    txtEventStart.Focus();
                    return;
                }
                if (txtEventTitle.Text.ToString() == "")
                {
                    PromptWindow.ShowPrompt("Error", "You must enter a event title.");
                    //MessageBox.Show("You must enter a event title.");
                    txtEventStart.Focus();
                    return;
                }
                if (txtEventDescription.Text.ToString() == "")
                {
                    PromptWindow.ShowPrompt("Error", "You must enter a event description.");
                    //MessageBox.Show("You must enter a event description.");
                    txtEventStart.Focus();
                    return;
                }
                if (txtEventStart.Value.ToString() == "")
                {
                    PromptWindow.ShowPrompt("Error", "You must enter a start time.");
                    //MessageBox.Show("You must enter a start time.");
                    txtEventStart.Focus();
                    return;
                }
                if (txtEventStart.Value < DateTime.Now)
                {
                    PromptWindow.ShowPrompt("Error", "You must enter a start time in the future.");

                    //MessageBox.Show("You must enter a start time in the future.");
                    txtEventStart.Focus();
                    return;
                }
                if (txtEventEnd.Value.ToString() == "")
                {
                    PromptWindow.ShowPrompt("Error", "You must enter a end time.");

                    //MessageBox.Show("You must enter a end time.");
                    txtEventStart.Focus();
                    return;
                }
                if (txtEventEnd.Value < txtEventStart.Value)
                {
                    PromptWindow.ShowPrompt("Error", "You must enter a end time after the start time.");

                    //MessageBox.Show("You must enter a end time after the start time.");
                    txtEventStart.Focus();
                    return;
                }
                if (txtEventAddress.Text.ToString() == "")
                {
                    PromptWindow.ShowPrompt("Error", "You must enter an address.");
                    txtEventStart.Focus();
                    return;
                }
                if (txtEventZipcode.Text.ToString() == "")
                {
                    PromptWindow.ShowPrompt("Error", "You must enter a event zipcode.");

                    //MessageBox.Show("You must enter a event zipcode.");
                    txtEventStart.Focus();
                    return;
                }

                if (txtZipcode.Text.ToString() == "")
                {
                    PromptWindow.ShowPrompt("Error", "You must enter a zipcode.");

                    //MessageBox.Show("You must enter a zipcode.");
                    txtEventStart.Focus();
                    return;
                }

                Event ivent = new Event()
                {
                    EventTypeid = cboEventTypeId.Text,
                    Shelterid = Int32.Parse( cboShelterId.Text),
                    EventTitle = txtEventTitle.Text,
                    EventDescription = txtEventDescription.Text,
                    EventStart = DateTime.Parse( txtEventStart.Text),
                    EventEnd = DateTime.Parse(txtEventEnd.Text),
                    EventAddress = txtEventAddress.Text,
                    EventZipcode = txtEventZipcode.Text,
                    EventVisible = true,
                    Zipcode= txtZipcode.Text,
                    Eventid = _eventVM.Eventid
                };

                try
                {
                    if (_manager.EventManager.EditEvent(_eventVM, ivent))
                    {
                        NavigationService.GoBack();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Note failed to save.", ex.Message);
                }

            }
            else
            {
                if (cboEventTypeId.Text.ToString() == "")
                {
                    PromptWindow.ShowPrompt("Error", "You must enter a event type.");
                    txtEventStart.Focus();
                    return;
                }
                if (cboShelterId.Text.ToString() == "")
                {
                    PromptWindow.ShowPrompt("Error", "You must enter a shelter id.");
                    txtEventStart.Focus();
                    return;
                }
                if (txtEventTitle.Text.ToString() == "")
                {
                    PromptWindow.ShowPrompt("Error", "You must enter a event title.");
                    //MessageBox.Show("You must enter a event title.");
                    txtEventStart.Focus();
                    return;
                }
                if (txtEventDescription.Text.ToString() == "")
                {
                    PromptWindow.ShowPrompt("Error", "You must enter a event description.");
                    //MessageBox.Show("You must enter a event description.");
                    txtEventStart.Focus();
                    return;
                }
                if (txtEventStart.Value.ToString() == "")
                {
                    PromptWindow.ShowPrompt("Error", "You must enter a start time.");
                    //MessageBox.Show("You must enter a start time.");
                    txtEventStart.Focus();
                    return;
                }
                if (txtEventStart.Value < DateTime.Now)
                {
                    PromptWindow.ShowPrompt("Error", "You must enter a start time in the future.");

                    //MessageBox.Show("You must enter a start time in the future.");
                    txtEventStart.Focus();
                    return;
                }
                if (txtEventEnd.Value.ToString() == "")
                {
                    PromptWindow.ShowPrompt("Error", "You must enter a end time.");

                    //MessageBox.Show("You must enter a end time.");
                    txtEventStart.Focus();
                    return;
                }
                if (txtEventEnd.Value < txtEventStart.Value)
                {
                    PromptWindow.ShowPrompt("Error", "You must enter a end time after the start time.");

                    //MessageBox.Show("You must enter a end time after the start time.");
                    txtEventStart.Focus();
                    return;
                }
                if (txtEventAddress.Text.ToString() == "")
                {
                    PromptWindow.ShowPrompt("Error", "You must enter an address.");
                    txtEventStart.Focus();
                    return;
                }
                if (txtEventZipcode.Text.ToString() == "")
                {
                    PromptWindow.ShowPrompt("Error", "You must enter a event zipcode.");

                    //MessageBox.Show("You must enter a event zipcode.");
                    txtEventStart.Focus();
                    return;
                }

                if (txtZipcode.Text.ToString() == "")
                {
                    PromptWindow.ShowPrompt("Error", "You must enter a zipcode.");

                    //MessageBox.Show("You must enter a zipcode.");
                    txtEventStart.Focus();
                    return;
                }

                if (cboEventTypeId.SelectedItem==null||cboShelterId.SelectedItem==null||txtEventTitle.Text==null||txtEventDescription.Text==null||txtEventStart.Value==null||txtEventEnd.Value==null||txtEventAddress.Text==null||txtEventZipcode.Text==null||txtZipcode.Text==null)
                {
                    MessageBox.Show("some data was not enered right");
                    return;
                }
                Event ivent = new Event()
                {
                    EventTypeid = cboEventTypeId.SelectedItem.ToString(),
                    Shelterid = Int32.Parse(cboShelterId.Text),
                    EventTitle = txtEventTitle.Text,
                    EventDescription = txtEventDescription.Text,
                    EventStart = DateTime.Parse(txtEventStart.Text),
                    EventEnd = DateTime.Parse(txtEventEnd.Text),
                    EventAddress = txtEventAddress.Text,
                    EventZipcode = txtEventZipcode.Text,
                    Zipcode = txtZipcode.Text,
                    EventVisible=true
                    
                };
                try
                {
                    if (_manager.EventManager.AddEvent(ivent))
                    {
                        NavigationService.GoBack();
                    }
                }
                catch (Exception ex)
                {
                    PromptWindow.ShowPrompt("Error", "There was an error adding your event. \n\n Please ensure your zip code is valid.");
                    //MessageBox.Show("Note failed to save try a valid zipcode.", ex.Message);
                }
            }
        }
    }
}
