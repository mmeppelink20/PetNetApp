using DataObjects;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfPresentation.Shelters
{
    /// <summary>
    /// Interaction logic for HoursOfOperation.xaml
    /// </summary>
    public partial class HoursOfOperation : Window
    {
        private MasterManager _manager = null;
        private ShelterManager _shelterManager = null;
        private int shelterID;
        private int _dayOfWeek;
        private string openHourStatus;
        private string closeHourStatus;
        private bool openHourUnsaved;
        private bool closeHourUnsaved;
        private string _editMode;
        private List<DataObjects.HoursOfOperation> hoursOfOperation = new List<DataObjects.HoursOfOperation>();
        private DataObjects.HoursOfOperation hoursToUpdate = new DataObjects.HoursOfOperation { OpenHour = TimeSpan.Zero, CloseHour = TimeSpan.Zero };


        public HoursOfOperation(MasterManager manager, ShelterVM shelter, string editMode)
        {
            _manager = manager;
            _shelterManager = new ShelterManager();
            _editMode = editMode;
            shelterID = shelter.ShelterId;

            // IF DEBUGGING UNCOMMENT THIS AND CHANGE THE VALUE TO THE MODE YOU WANT TO TEST. ONCE DONE RECOMMENT IT.
            // _editMode = "editshelter";
            //

            InitializeComponent();

            // Check if editing or viewing, IF DEBUGGING CHANGE THIS TO THE EDIT MODE YOU WANT.
            if (_editMode == "editshelter" || _editMode == "addshelter")
            {
                // Show full edit mode. (Default state of window.)
            }
            // Default to view mode if not specifcially adding or editing a shelter. (This prevents errors with bad edit mode strings)
            else
            {
                btnSaveChanges.IsEnabled = false;
                btnSaveChanges.Visibility = Visibility.Hidden;
                txtOpenHour.IsReadOnly = true;
                txtCloseHour.IsReadOnly = true;
                Grid.SetColumn(btnCancel, 4);
            }

            try
            {
                hoursOfOperation = _manager.ShelterManager.RetrieveHoursOfOperationByShelterID(shelter.ShelterId);
            }
            catch (Exception ex)
            {
                PromptWindow.ShowPrompt("An error has occured, cannot retrieve the shelter's hours of operation.", ex.ToString());
            }

            string today = System.DateTime.Now.DayOfWeek.ToString();
            lblDayToEdit.Content = "Hours of Operation for " + today + ":";
            switch (today)
            {
                case "Sunday":
                    try
                    {
                        _dayOfWeek = 1;
                        txtOpenHour.Text = hoursOfOperation[0].OpenHour.ToString(@"hh\:mm");
                        txtCloseHour.Text = hoursOfOperation[0].CloseHour.ToString(@"hh\:mm");
                        FixValuesAboveTwelve();
                        FixButtonsForInput(0);
                    }
                    catch (Exception)
                    {
                        PromptWindow.ShowPrompt("No hours found!", "No hours found for Sunday, please enter the hours the shelter will be operational.");
                    }
                    break;
                case "Monday":
                    try
                    {
                        _dayOfWeek = 2;
                        txtOpenHour.Text = hoursOfOperation[1].OpenHour.ToString(@"hh\:mm");
                        txtCloseHour.Text = hoursOfOperation[1].CloseHour.ToString(@"hh\:mm");
                        FixValuesAboveTwelve();
                        FixButtonsForInput(1);
                    }
                    catch (Exception)
                    {
                        PromptWindow.ShowPrompt("No hours found!", "No hours found for Monday, please enter the hours the shelter will be operational.");
                    }
                    break;
                case "Tuesday":
                    try
                    {
                        _dayOfWeek = 3;
                        txtOpenHour.Text = hoursOfOperation[2].OpenHour.ToString(@"hh\:mm");
                        txtCloseHour.Text = hoursOfOperation[2].CloseHour.ToString(@"hh\:mm");
                        FixValuesAboveTwelve();
                        FixButtonsForInput(2);
                    }
                    catch (Exception)
                    {
                        PromptWindow.ShowPrompt("No hours found!", "No hours found for Tuesday, please enter the hours the shelter will be operational.");
                    }
                    break;

                case "Wednesday":
                    try
                    {
                        _dayOfWeek = 4;
                        txtOpenHour.Text = hoursOfOperation[3].OpenHour.ToString(@"hh\:mm");
                        txtCloseHour.Text = hoursOfOperation[3].CloseHour.ToString(@"hh\:mm");
                        FixValuesAboveTwelve();
                        FixButtonsForInput(3);
                    }
                    catch (Exception)
                    {
                        PromptWindow.ShowPrompt("No hours found!", "No hours found for Wednesday, please enter the hours the shelter will be operational.");
                    }
                    break;

                case "Thursday":
                    try
                    {
                        _dayOfWeek = 5;
                        txtOpenHour.Text = hoursOfOperation[4].OpenHour.ToString(@"hh\:mm");
                        txtCloseHour.Text = hoursOfOperation[4].CloseHour.ToString(@"hh\:mm");
                        FixValuesAboveTwelve();
                        FixButtonsForInput(4);
                    }
                    catch (Exception)
                    {
                        PromptWindow.ShowPrompt("No hours found!", "No hours found for Thursday, please enter the hours the shelter will be operational.");
                    }
                    break;

                case "Friday":
                    try
                    {
                        _dayOfWeek = 6;
                        txtOpenHour.Text = hoursOfOperation[5].OpenHour.ToString(@"hh\:mm");
                        txtCloseHour.Text = hoursOfOperation[5].CloseHour.ToString(@"hh\:mm");
                        FixValuesAboveTwelve();
                        FixButtonsForInput(5);
                    }
                    catch (Exception)
                    {
                        PromptWindow.ShowPrompt("No hours found!", "No hours found for Friday, please enter the hours the shelter will be operational.");
                    }
                    break;

                case "Saturday":
                    try
                    {
                        _dayOfWeek = 7;
                        txtOpenHour.Text = hoursOfOperation[6].OpenHour.ToString(@"hh\:mm");
                        txtCloseHour.Text = hoursOfOperation[6].CloseHour.ToString(@"hh\:mm");
                        FixValuesAboveTwelve();
                        FixButtonsForInput(6);
                    }
                    catch (Exception)
                    {
                        PromptWindow.ShowPrompt("No hours found!", "No hours found for Saturday, please enter the hours the shelter will be operational.");
                    }
                    break;


            }
        }

        // All button methods.

        /// <summary>
        /// Teft Francisco
        /// Created: 2023/03/17
        /// Changes the hours of operation listed to the selected day.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSunday_Click(object sender, RoutedEventArgs e)
        {
            _dayOfWeek = 1;
            lblDayToEdit.Content = "Hours of Operation for Sunday:";
            try
            {
                txtOpenHour.Text = hoursOfOperation[0].OpenHour.ToString(@"hh\:mm");
                txtCloseHour.Text = hoursOfOperation[0].CloseHour.ToString(@"hh\:mm");
                FixValuesAboveTwelve();
                FixButtonsForInput(0);
            }
            catch (Exception)
            {
                PromptWindow.ShowPrompt("No hours found!", "No hours found for Sunday, please enter the hours the shelter will be operational.");
            }
        }

        /// <summary>
        /// Teft Francisco
        /// Created: 2023/03/17
        /// Changes the hours of operation listed to the selected day.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMonday_Click(object sender, RoutedEventArgs e)
        {
            _dayOfWeek = 2;
            lblDayToEdit.Content = "Hours of Operation for Monday:";
            try
            {
                txtOpenHour.Text = hoursOfOperation[1].OpenHour.ToString(@"hh\:mm");
                txtCloseHour.Text = hoursOfOperation[1].CloseHour.ToString(@"hh\:mm");
                FixValuesAboveTwelve();
                FixButtonsForInput(1);
            }
            catch (Exception)
            {
                PromptWindow.ShowPrompt("No hours found!", "No hours found for Monday, please enter the hours the shelter will be operational.");
            }
        }

        /// <summary>
        /// Teft Francisco
        /// Created: 2023/03/17
        /// Changes the hours of operation listed to the selected day.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTuesday_Click(object sender, RoutedEventArgs e)
        {
            _dayOfWeek = 3;
            lblDayToEdit.Content = "Hours of Operation for Tuesday:";
            try
            {
                txtOpenHour.Text = hoursOfOperation[2].OpenHour.ToString(@"hh\:mm");
                txtCloseHour.Text = hoursOfOperation[2].CloseHour.ToString(@"hh\:mm");
                FixValuesAboveTwelve();
                FixButtonsForInput(2);
            }
            catch (Exception)
            {
                PromptWindow.ShowPrompt("No hours found!", "No hours found for Tuesday, please enter the hours the shelter will be operational.");
            }
        }

        /// <summary>
        /// Teft Francisco
        /// Created: 2023/03/17
        /// Changes the hours of operation listed to the selected day.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnWednesday_Click(object sender, RoutedEventArgs e)
        {
            _dayOfWeek = 4;
            lblDayToEdit.Content = "Hours of Operation for Wednesday:";
            try
            {
                txtOpenHour.Text = hoursOfOperation[3].OpenHour.ToString(@"hh\:mm");
                txtCloseHour.Text = hoursOfOperation[3].CloseHour.ToString(@"hh\:mm");
                FixValuesAboveTwelve();
                FixButtonsForInput(3);
            }
            catch (Exception)
            {
                PromptWindow.ShowPrompt("No hours found!", "No hours found for Wednesday, please enter the hours the shelter will be operational.");
            }
        }

        /// <summary>
        /// Teft Francisco
        /// Created: 2023/03/17
        /// Changes the hours of operation listed to the selected day.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnThursday_Click(object sender, RoutedEventArgs e)
        {
            _dayOfWeek = 5;
            lblDayToEdit.Content = "Hours of Operation for Thursday:";
            try
            {
                txtOpenHour.Text = hoursOfOperation[4].OpenHour.ToString(@"hh\:mm");
                txtCloseHour.Text = hoursOfOperation[4].CloseHour.ToString(@"hh\:mm");
                FixValuesAboveTwelve();
                FixButtonsForInput(4);
            }
            catch (Exception)
            {
                PromptWindow.ShowPrompt("No hours found!", "No hours found for Thursday, please enter the hours the shelter will be operational.");
            }
        }

        /// <summary>
        /// Teft Francisco
        /// Created: 2023/03/17
        /// Changes the hours of operation listed to the selected day.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFriday_Click(object sender, RoutedEventArgs e)
        {
            _dayOfWeek = 6;
            lblDayToEdit.Content = "Hours of Operation for Friday:";
            try
            {
                txtOpenHour.Text = hoursOfOperation[5].OpenHour.ToString(@"hh\:mm");
                txtCloseHour.Text = hoursOfOperation[5].CloseHour.ToString(@"hh\:mm");
                FixValuesAboveTwelve();
                FixButtonsForInput(5);
            }
            catch (Exception)
            {
                PromptWindow.ShowPrompt("No hours found!", "No hours found for Friday, please enter the hours the shelter will be operational.");
            }
        }

        /// <summary>
        /// Teft Francisco
        /// Created: 2023/03/17
        /// Changes the hours of operation listed to the selected day.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaturday_Click(object sender, RoutedEventArgs e)
        {
            _dayOfWeek = 7;
            lblDayToEdit.Content = "Hours of Operation for Saturday:";
            try
            {
                txtOpenHour.Text = hoursOfOperation[6].OpenHour.ToString(@"hh\:mm");
                txtCloseHour.Text = hoursOfOperation[6].CloseHour.ToString(@"hh\:mm");
                FixValuesAboveTwelve();
                FixButtonsForInput(6);
            }
            catch (Exception)
            {
                PromptWindow.ShowPrompt("No hours found!", "No hours found for Saturday, please enter the hours the shelter will be operational.");
            }
        }

        /// <summary>
        /// Teft Francisco
        /// Created: 2023/03/17
        /// Saves changes made to the hours of operation.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveChanges_Click(object sender, RoutedEventArgs e)
        {
            txtCloseHour.Text = ValidateInput(txtCloseHour.Text);
            txtOpenHour.Text = ValidateInput(txtOpenHour.Text);

            string openTimeString;
            string closeTimeString;

            DateTime openTimeToParse;
            DateTime closeTimeToParse;

            TimeSpan timeToParse;
            try
            {
                bool updateSuccess = false;
                if (openHourStatus.Equals("pm"))
                {
                    openTimeString = txtOpenHour.Text + " PM";
                    openTimeToParse = DateTime.ParseExact(openTimeString, "hh:mm tt", CultureInfo.InvariantCulture);
                } 
                else
                {
                    openTimeString = txtOpenHour.Text + " AM";
                    openTimeToParse = DateTime.ParseExact(openTimeString, "hh:mm tt", CultureInfo.InvariantCulture);
                }

                if (closeHourStatus.Equals("pm"))
                {
                    closeTimeString = txtCloseHour.Text + " PM";
                    closeTimeToParse = DateTime.ParseExact(closeTimeString, "hh:mm tt", CultureInfo.InvariantCulture);
                } 
                else
                {
                    closeTimeString = txtCloseHour.Text + " AM";
                    closeTimeToParse = DateTime.ParseExact(closeTimeString, "hh:mm tt", CultureInfo.InvariantCulture);
                }

                timeToParse = openTimeToParse.TimeOfDay;
                hoursToUpdate.OpenHour = timeToParse;
                timeToParse = closeTimeToParse.TimeOfDay;
                hoursToUpdate.CloseHour = timeToParse;

                if (hoursToUpdate.OpenHour > hoursToUpdate.CloseHour)
                {
                    PromptWindow.ShowPrompt("Error!", "The opening hour can't be later than the closing hour!.");
                } else
                {
                    updateSuccess = _shelterManager.EditHoursOfOperationByShelterID(shelterID, _dayOfWeek, hoursToUpdate);
                }

                
                if (updateSuccess == false)
                {
                    PromptWindow.ShowPrompt("Error!", "Failed to update hours.");
                }
                else
                {
                    PromptWindow.ShowPrompt("Success!", "Hours of operation updated!");
                    hoursOfOperation = _shelterManager.RetrieveHoursOfOperationByShelterID(shelterID);
                    openHourUnsaved = false;
                    closeHourUnsaved = false;
                }
            }
            catch (Exception ex)
            {
                PromptWindow.ShowPrompt("Error!, Failed to retrieve hours.", ex.ToString());
            }
        }
        /// <summary>
        /// Teft Francisco
        /// Created: 2023/03/17
        /// Exits the window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            if (((_editMode == "editshelter" || _editMode == "addshelter") && openHourUnsaved == true) || (_editMode == "editshelter" || _editMode == "addshelter") && closeHourUnsaved == true)
            {
                var result = PromptWindow.ShowPrompt("Unsaved changes!", "Exit now and lose your changes?", ButtonMode.YesNo);
                if (result == PromptSelection.Yes)
                {
                    this.Close();
                }
            }
            else
            {
                this.Close();
            }
        }

        // Formatting methods.

        /// <summary>
        /// Teft Francisco
        /// Created: 2023/03/18
        /// Removes special characters from the string.
        /// </summary>
        /// <param name="timeToFix"></param>
        private string FixHourFormatting(string timeToFix)
        {
            // Unfortunately the Key class does not account for special characters, so we manually remove those.
            var charsToRemove = new string[] { "!", "@", "#", "$", "%", "^", "&", "*", "(", ")", ";" };
            var fixedString = timeToFix;
            foreach (var c in charsToRemove)
            {
                fixedString = fixedString.Replace(c, string.Empty);
            }
            return fixedString;
        }

        /// <summary>
        /// Teft Francisco
        /// Created: 2023/03/18
        /// Takes values above twelve and interprets them correctly.
        /// </summary>
        private void FixValuesAboveTwelve()
        {
            if (txtOpenHour.Text.Substring(0, 2).Contains(":"))
            {
                txtOpenHour.Text = "0" + txtOpenHour.Text;
            }

            TimeSpan timeToFix = TimeSpan.Parse(txtOpenHour.Text);
            DateTime fixedTime = DateTime.Today.Add(timeToFix);
            String fixedOpeningTime = fixedTime.ToString("hh:mm tt");
            txtOpenHour.Text = fixedOpeningTime.Substring(0, 5);

            if (fixedOpeningTime.Substring(6, 2).Equals("PM"))
            {
                openHourStatus = "pm";
                btnOpenHourAMPM.Content = "(PM)";
            } else
            {
                openHourStatus = "am";
                btnOpenHourAMPM.Content = "(AM)";
            }

            if (txtCloseHour.Text.Substring(0, 2).Contains(":"))
            {
                txtCloseHour.Text = "0" + txtCloseHour.Text;
            }

            if (txtCloseHour.Text.Substring(0, 2).Contains(":"))
            {
                txtCloseHour.Text = "0" + txtCloseHour.Text;
            }

            timeToFix = TimeSpan.Parse(txtCloseHour.Text);
            fixedTime = DateTime.Today.Add(timeToFix);
            String fixedClosingTime = fixedTime.ToString("hh:mm tt");
            txtCloseHour.Text = fixedClosingTime.Substring(0, 5);

            if (fixedClosingTime.Substring(6, 2).Equals("PM"))
            {
                closeHourStatus = "pm";
                btnCloseHourAMPM.Content = "(PM)";
            }
            else
            {
                closeHourStatus = "am";
                btnCloseHourAMPM.Content = "(AM)";
            }


            if (txtOpenHour.Text.Substring(0, 2).Contains(":"))
            {
                txtOpenHour.Text = "0" + txtOpenHour.Text;
            }


            if (txtCloseHour.Text.Substring(0, 2).Contains(":"))
            {
                txtCloseHour.Text = "0" + txtCloseHour.Text;
            }
        }
        
        /// <summary>
        /// Teft Francisco
        /// Created: 2023/03/18
        /// Parses strings to see if they properly work as TimeSpan objects and shows an error if it doesn't.
        /// </summary>
        private string ValidateInput(string stringToValidate)
        {

            try
            {
                stringToValidate = TimeSpan.Parse(stringToValidate).ToString(@"hh\:mm");
            }
            catch (Exception)
            {
                PromptWindow.ShowPrompt("Incorrect time format!", "Please enter a valid time.");
            }


            return stringToValidate;
        }

        /// <summary>
        /// Teft Francisco
        /// Created: 2023/03/18
        /// Corrects the AM/PM buttons on the text boxes by checking the hoursOfOperation array to see if they take place before or after 12 PM, also takes into account if in viewmode.
        /// </summary>
        private void FixButtonsForInput(int dayOfWeek) 
        {
            if (hoursOfOperation[dayOfWeek].OpenHour > TimeSpan.Parse("11:59:59"))
            {
                openHourStatus = "pm";
                btnOpenHourAMPM.Content = "(PM)";
                btnCloseHourAMPM.IsEnabled = false;
            }
            else
            {
                openHourStatus = "am";
                btnOpenHourAMPM.Content = "(AM)";
                btnCloseHourAMPM.IsEnabled = true;
            }

            if (hoursOfOperation[dayOfWeek].CloseHour > TimeSpan.Parse("11:59:59"))
            {
                closeHourStatus = "pm";
                btnCloseHourAMPM.Content = "(PM)";
            }
            else
            {
                closeHourStatus = "am";
                btnCloseHourAMPM.Content = "(AM)";
            }

            if (_editMode.Equals("viewshelter"))
            {
                btnCloseHourAMPM.IsEnabled = false;
                btnOpenHourAMPM.IsEnabled = false;
            }
        }

        // All methods for the Open Hour (AM) time box.

        /// <summary>
        /// Teft Francisco
        /// Created: 2023/03/18
        /// Handles bad input for the time box of AM.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtOpenHour_KeyDown(object sender, KeyEventArgs e)
        {
            openHourUnsaved = true;
            try
            {
                // This is awful and probably could be done better, but this is the only way I could get it to work effectively.

                // All allowed buttons for the text field.
                if (e.Key == Key.D1 || e.Key == Key.D2 || e.Key == Key.D3 || e.Key == Key.D4 || e.Key == Key.D5 || e.Key == Key.D6 || e.Key == Key.D7 || e.Key == Key.D8
                    || e.Key == Key.D9 || e.Key == Key.D0 || e.Key == Key.OemSemicolon || e.Key == Key.Back)
                {
                    // Detect if semicolon is pressed, if so automatically convert to proper formatting.
                    if (e.Key == Key.OemSemicolon && txtOpenHour.SelectionStart == 2)
                    {
                        txtOpenHour.Text = txtOpenHour.Text + ":";
                        txtOpenHour.Focus();
                        txtOpenHour.SelectionStart = txtOpenHour.Text.Length;
                        txtOpenHour.SelectionLength = 0;
                        e.Handled = true;
                    }
                    // If semicolon is NOT pressed at the proper space automatically place it.
                    else if (e.Key != Key.OemSemicolon && txtOpenHour.SelectionStart == 2)
                    {
                        txtOpenHour.Text = txtOpenHour.Text + ":";
                        txtOpenHour.Focus();
                        txtOpenHour.SelectionStart = txtOpenHour.Text.Length;
                        txtOpenHour.SelectionLength = 0;

                        e.Handled = false;
                    }
                    // Restrict semicolon placement outside of the 3rd character spot.
                    else if (e.Key == Key.OemSemicolon && txtOpenHour.SelectionStart != 2)
                    {
                        e.Handled = true;
                    }
                    // If content of textbox exceeds allowed characters handle the key to restrict input.
                    else if (txtOpenHour.Text.Length > 4 && e.Key != Key.Back)
                    {
                        e.Handled = true;
                    }
                    else
                    {
                        e.Handled = false;
                    }
                }
                else
                {
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                PromptWindow.ShowPrompt("An error has occured", ex.ToString());
            }
        }

        /// <summary>
        /// Teft Francisco
        /// Created: 2023/03/17
        /// Handles bad input for the time box of AM AFTER the text has been changed. This is needed to remove special characters.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtOpenHour_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtOpenHour.Text = FixHourFormatting(txtOpenHour.Text);
            if (txtOpenHour.Text.Length > 4)
                txtOpenHour.SelectionStart = txtOpenHour.Text.Length;
            txtOpenHour.SelectionLength = 0;
        }

        /// <summary>
        /// Teft Francisco
        /// Created: 2023/03/17
        /// Restricts paste functionality for the AM time box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtOpenHour_PreviewCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (e.Command == ApplicationCommands.Cut ||
                 e.Command == ApplicationCommands.Copy ||
                 e.Command == ApplicationCommands.Paste)
            {

                e.CanExecute = false;
                e.Handled = true;
            }
        }

        /// <summary>
        /// Teft Francisco
        /// Created: 2023/03/17
        /// Applies validation on focus lost.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtOpenHour_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Int32.Parse(txtOpenHour.Text.Substring(0, 2)) > 12 && Int32.Parse(txtOpenHour.Text.Substring(0, 2)) <= 24)
                {
                    try
                    {
                        string firstpart = "" + (Int32.Parse(txtOpenHour.Text.Substring(0, 2)) - 12);
                        string lastpart = txtOpenHour.Text.Substring(txtOpenHour.Text.Length - 3);
                        txtOpenHour.Text = firstpart + lastpart;
                    }
                    catch (Exception)
                    {
                        PromptWindow.ShowPrompt("Error detected!", "Try re-entering the time for the opening hour.");
                    }
                }
            }
            catch (Exception)
            {

                PromptWindow.ShowPrompt("Error detected!", "Try re-entering the time for the opening hour.");
            }
            

            txtOpenHour.Text = ValidateInput(txtOpenHour.Text);
        }

        /// <summary>
        /// Teft Francisco
        /// Created: 2023/03/17
        /// Swaps to AM or PM on the Open Hour box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenHourAMPM_Click(object sender, RoutedEventArgs e)
        {
            if (btnOpenHourAMPM.Content.Equals("(AM)"))
            {
                openHourStatus = "pm";
                btnOpenHourAMPM.Content = "(PM)";
                closeHourStatus = "pm";
                btnCloseHourAMPM.Content = "(PM)";
                btnCloseHourAMPM.IsEnabled = false;

            } else
            {
                openHourStatus = "am";
                btnOpenHourAMPM.Content = "(AM)";
                btnCloseHourAMPM.IsEnabled = true;
            }
        }

        // All methods for the Close Hour (PM) time box.

        /// <summary>
        /// Teft Francisco
        /// Created: 2023/03/17
        /// Updates and removes illegal characters on key presses.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCloseHour_KeyDown(object sender, KeyEventArgs e)
        {
            closeHourUnsaved = true;
            try
            {
                // This is awful and probably could be done better, but this is the only way I could get it to work effectively.

                // All allowed buttons for the text field.
                if (e.Key == Key.D1 || e.Key == Key.D2 || e.Key == Key.D3 || e.Key == Key.D4 || e.Key == Key.D5 || e.Key == Key.D6 || e.Key == Key.D7 || e.Key == Key.D8
                    || e.Key == Key.D9 || e.Key == Key.D0 || e.Key == Key.OemSemicolon || e.Key == Key.Back)
                {
                    // Detect if semicolon is pressed, if so automatically convert to proper formatting.
                    if (e.Key == Key.OemSemicolon && txtCloseHour.SelectionStart == 2)
                    {
                        txtCloseHour.Text = txtCloseHour.Text + ":";
                        txtCloseHour.Focus();
                        txtCloseHour.SelectionStart = txtCloseHour.Text.Length;
                        txtCloseHour.SelectionLength = 0;
                        e.Handled = true;
                    }
                    // If semicolon is NOT pressed at the proper space automatically place it.
                    else if (e.Key != Key.OemSemicolon && txtCloseHour.SelectionStart == 2)
                    {
                        txtCloseHour.Text = txtCloseHour.Text + ":";
                        txtCloseHour.Focus();
                        txtCloseHour.SelectionStart = txtCloseHour.Text.Length;
                        txtCloseHour.SelectionLength = 0;
                        e.Handled = false;
                    }
                    // Restrict semicolon placement outside of the 3rd character spot.
                    else if (e.Key == Key.OemSemicolon && txtCloseHour.SelectionStart != 2)
                    {
                        e.Handled = true;
                    }
                    // If content of textbox exceeds allowed characters handle the key to restrict input.
                    else if (txtCloseHour.Text.Length > 4 && e.Key != Key.Back)
                    {
                        e.Handled = true;
                    }
                    else
                    {
                        e.Handled = false;
                    }
                }
                else
                {
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                PromptWindow.ShowPrompt("An error has occured", ex.ToString());
            }
        }

        /// <summary>
        /// Teft Francisco
        /// Created: 2023/03/17
        /// Updates and removes illegal characters on key presses.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCloseHour_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtCloseHour.Text = FixHourFormatting(txtCloseHour.Text);
            if (txtCloseHour.Text.Length > 4)
                txtCloseHour.SelectionStart = txtCloseHour.Text.Length;
            txtCloseHour.SelectionLength = 0;
        }

        /// <summary>
        /// Teft Francisco
        /// Created: 2023/03/17
        /// Restricts illegal content by restricting paste functionality.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCloseHour_PreviewCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (e.Command == ApplicationCommands.Cut ||
                 e.Command == ApplicationCommands.Copy ||
                 e.Command == ApplicationCommands.Paste)
            {

                e.CanExecute = false;
                e.Handled = true;
            }
        }
        /// <summary>
        /// Teft Francisco
        /// Created: 2023/03/17
        /// Applies validation on focus lost.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCloseHour_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Int32.Parse(txtCloseHour.Text.Substring(0, 2)) > 12 && Int32.Parse(txtCloseHour.Text.Substring(0, 2)) <= 24)
                {
                    try
                    {
                        string firstpart = "" + (Int32.Parse(txtCloseHour.Text.Substring(0, 2)) - 12);
                        string lastpart = txtCloseHour.Text.Substring(txtCloseHour.Text.Length - 3);
                        txtCloseHour.Text = firstpart + lastpart;
                    }
                    catch (Exception)
                    {
                        PromptWindow.ShowPrompt("Error detected!", "Try re-entering the time for the closing hour.");
                    }
                }
            }
            catch (Exception)
            {

                PromptWindow.ShowPrompt("Error detected!", "Try re-entering the time for the closing hour.");
            }
            

            txtCloseHour.Text = ValidateInput(txtCloseHour.Text);
        }


        /// <summary>
        /// Teft Francisco
        /// Created: 2023/03/17
        /// Swaps the mode of the close hour textbox to or from AM or PM.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCloseHourAMPM_Click(object sender, RoutedEventArgs e)
        {
            if (btnCloseHourAMPM.Content.Equals("(AM)"))
            {
                closeHourStatus = "pm";
                btnCloseHourAMPM.Content = "(PM)";
            }
            else
            {
                closeHourStatus = "am";
                btnCloseHourAMPM.Content = "(AM)";
            }
        }
    }
}
