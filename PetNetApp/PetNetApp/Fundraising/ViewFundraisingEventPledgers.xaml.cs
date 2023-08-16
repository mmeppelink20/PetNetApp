using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using DataAccessLayerFakes;
using DataObjects;
using LogicLayer;
using WpfPresentation.Fundraising;
using WpfPresentation.UserControls;

namespace WpfPresentation.Fundraising
{
    /// <summary>
    /// Interaction logic for ViewFundraisingEventPledgers.xaml
    /// </summary>
    public partial class ViewFundraisingEventPledgers : Page
    {
        private List<PledgeVM> _pledgeVMs = null;
        private List<PledgeVM> _filteredPledgeVMs = new List<PledgeVM>();
        private FundraisingEvent _fundraisingEvent = null;
        MasterManager _masterManager = null;


        public ViewFundraisingEventPledgers(FundraisingEvent fundraisingEvent, MasterManager masterManager)
        {
            InitializeComponent();
            _fundraisingEvent = fundraisingEvent;
            _masterManager = masterManager;
        }

        /// <summary>
        /// William Rients
        /// Created: 2023/04/04
        /// 
        /// When page loads, a list of PledgeVMs is
        /// retrieved by eventId and the stackpannel is
        /// populated. If there is not data, a message is shown
        /// that says there is no data
        /// </summary>
        ///
        /// <remarks>
        /// Andrew Schneider
        /// Updated: 2023/04/21 
        /// Moved Page_Loaded functionality into its own method - populatePage() so that
        /// it can be called when the page loads and when a filter/sort has been applied.
        /// 
        /// Zaid Rachman
        /// Updated: 2023/04/27
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            stackHeader.Children.Clear();

            LoadPledgers();
            if (_pledgeVMs.Count == 1)
            {
                lblHeader.Content = "Pledger from " + "\"" + _fundraisingEvent.Title + "\"" + " on " + _fundraisingEvent.StartTime.Value.ToShortDateString();
            }
            else if (_pledgeVMs.Count > 1)
            {
                lblHeader.Content = "Pledgers from " + "\"" + _fundraisingEvent.Title + "\"" + " on " + _fundraisingEvent.StartTime.Value.ToShortDateString();
            }

            FundraisingEventPledgerHeaderControl header = new FundraisingEventPledgerHeaderControl();
            stackHeader.Children.Add(header);

            ResetFilters();
            PopulatePage(_pledgeVMs);
        }

        /// <summary>
        /// William Rients
        /// Created: 2023/04/04
        /// 
        /// When page loads, a list of PledgeVMs is
        /// retrieved by eventId and the stackpannel is
        /// populated. If there is not data, a message is shown
        /// that says there is no data
        /// </summary>
        ///
        /// <remarks>
        /// Andrew Schneider
        /// Updated: 2023/04/21 
        /// Moved some Page_Loaded functionality into its own method, populatePage(), so that
        /// it can be called when the page loads and when a filter/sort has been applied.
        /// 
        /// Zaid Rachman
        /// Updated: 2023/04/27
        /// Final QA
        /// </remarks>
        /// <param name="pledgeVMs">A list of pledges, full or filtered</param>
        public void PopulatePage(List<PledgeVM> pledgeVMs)
        {
            stackPledgers.Children.Clear();

            if (pledgeVMs.Count == 0)
            {
                stackPledgers.Visibility = Visibility.Collapsed;
                stackHeader.Visibility = Visibility.Collapsed;
                nothingToShow.Visibility = Visibility.Visible;
            }
            else
            {
                stackPledgers.Visibility = Visibility.Visible;
                stackHeader.Visibility = Visibility.Visible;
                nothingToShow.Visibility = Visibility.Collapsed;
            }

            int i = 0;
            foreach (PledgeVM pledge in pledgeVMs)
            {
                ViewFundraisingEventPledgersControl item = new ViewFundraisingEventPledgersControl(pledge, _fundraisingEvent, i % 2 == 0);
                i++;
                stackPledgers.Children.Add(item);
            }
        }

        /// <summary>
        /// William Rients
        /// Created: 2023/04/04
        /// 
        /// Checks to see if the initial list of 
        /// pledgers is null, if null it gets a list
        /// of pledgers by eventId
        /// </summary>
        ///
        /// <remarks>
        /// Andrew Schneider
        /// Updated: 2023/04/22
        /// Added navigation to the catch
        /// 
        /// Zaid Rachman
        /// Updated: 2023/04/27
        /// Final QA
        /// </remarks>
        private void LoadPledgers()
        {
            if (_pledgeVMs == null)
            {
                try
                {
                    // Database data
                    _pledgeVMs = _masterManager.PledgeManager.RetrieveAllPledgesByEventId(_fundraisingEvent.FundraisingEventId);

                    // Accessor fake data
                    //PledgeManager _pm = new PledgeManager(new PledgeAccessorFakes());
                    //_pledgeVMs = _pm.RetrieveAllPledgesByEventId(_fundraisingEvent.FundraisingEventId);
                }
                catch (Exception ex)
                {
                    PromptWindow.ShowPrompt("Error", ex.Message, ButtonMode.Ok);
                    NavigationService.Navigate(new ViewFundraisingEventsPage());
                }
            }
        }

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/04/21
        /// 
        /// Helper method that resets filter options when page is loaded or
        /// filter "Reset" button (label) is clicked.
        /// </summary>
        ///
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/27
        /// Final QA
        /// </remarks>
        public void ResetFilters()
        {
            dpStartDate.SelectedDate = null;
            dpEndDate.SelectedDate = null;
            cmbFilterPledgeAmount.SelectedIndex = 0;
            ckbPledgeCompleted.IsChecked = false;
            ckbPledgeNotCompleted.IsChecked = false;
            cmbSortOrder.SelectedIndex = 0;
        }

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/04/21
        /// 
        /// Click event method to open Filter Pledges popup if
        /// it's closed and close it if it's open.
        /// </summary>
        ///
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/27
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFilterSortPledges_Click(object sender, RoutedEventArgs e)
        {
            if (popFilterSortPledges.IsOpen == false)
            {
                popFilterSortPledges.IsOpen = true;
            }
            else
            {
                popFilterSortPledges.IsOpen = false;
            }
        }

        /// <summary>
        /// William Rients
        /// Created: 2023/04/04
        /// 
        /// Button to go back to the list of
        /// events
        /// </summary>
        ///
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/27
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ViewFundraisingEventsPage());
        }


        // Popup methods
        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/04/21
        /// 
        /// Main click event method for filtering pledges. Checks if any dates have been selected, 
        /// if combo box selections have been made, or checkboxes checked. Sets booleans if any are
        /// used and calls the appropriate methods. Finally, calls populatePage() passing the appro-
        /// priate pledge list.
        /// </summary>
        ///
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/27
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblFilter_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _filteredPledgeVMs.Clear();

            bool datePickerUsed = false;
            bool cmbAmountUsed = false;
            bool checkBoxesUsed = false;
            bool sortUsed = false;

            if (dpStartDate.SelectedDate != null || dpEndDate.SelectedDate != null)
            {
                datePickerUsed = true;
                FilterPledgesByDate();
            }

            if (cmbFilterPledgeAmount.SelectedIndex != 0)
            {
                cmbAmountUsed = true;
                FilterPledgesByPledgeAmount(datePickerUsed);
            }

            if (ckbPledgeCompleted.IsChecked == true || ckbPledgeNotCompleted.IsChecked == true)
            {
                checkBoxesUsed = true;
                FilterPledgesByCompletionStatus(datePickerUsed || cmbAmountUsed ? true : false);
            }

            if (cmbSortOrder.SelectedIndex != 0 && datePickerUsed || cmbAmountUsed || checkBoxesUsed)
            {
                sortUsed = true;
                SortPledges(true);
            }

            if (cmbSortOrder.SelectedIndex != 0 && !datePickerUsed && !cmbAmountUsed && !checkBoxesUsed)
            {
                sortUsed = true;
                SortPledges(false);
            }

            PopulatePage(datePickerUsed || cmbAmountUsed || checkBoxesUsed || sortUsed ?
                         _filteredPledgeVMs : _pledgeVMs);
        }

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/04/22
        /// 
        /// Helper method to find pledge records between the specified dates. If only one date is
        /// provided the null one is set to DateTime.MinValue if it's the start date and DateTime.MaxValue
        /// if it's the end date. If both are supplied and end date is earlier it is also set to
        /// DateTime.MaxValue.
        /// </summary>
        ///
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/27
        /// Final QA
        /// </remarks>
        public void FilterPledgesByDate()
        {
            DateTime startDate = new DateTime();
            DateTime endDate = new DateTime();
            if (dpStartDate.SelectedDate == null)
            {
                startDate = DateTime.MinValue.AddDays(1);
            }
            else
            {
                startDate = dpStartDate.SelectedDate.Value;
            }

            if (dpEndDate.SelectedDate == null)
            {
                endDate = DateTime.MaxValue.AddDays(-1);
            }
            else
            {
                endDate = dpEndDate.SelectedDate.Value;
            }

            if (endDate < startDate)
            {
                endDate = DateTime.MaxValue.AddDays(-1);
            }

            foreach (var pledge in _pledgeVMs)
            {
                if (pledge.Date > startDate.AddDays(-1) && pledge.Date < endDate.AddDays(1))
                {
                    _filteredPledgeVMs.Add(pledge);
                }
            }
        }

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/04/22
        /// 
        /// Helper method using a switch statement to sort pledges by amount pledged. If datePickerUsed
        /// is true _filteredPledgeVMs is filtered. If datePickerUsed is false _pledgeVMs is filtered and
        /// the filtered results are assigned to _filteredPledgeVMs.
        /// </summary>
        ///
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/27
        /// Final QA
        /// </remarks>
        /// <param name="datePickerUsed">Boolean indicating if the date picker was used</param>
        private void FilterPledgesByPledgeAmount(bool datePickerUsed)
        {
            decimal pledgeFilterAmount = 0;

            switch (cmbFilterPledgeAmount.SelectedIndex)
            {
                case 1: // $1
                    pledgeFilterAmount = 1;
                    break;
                case 2: // $5 or Less
                    pledgeFilterAmount = 5;
                    break;
                case 3: // $10 or Less
                    pledgeFilterAmount = 10;
                    break;
                case 4: // $25 or Less
                    pledgeFilterAmount = 25;
                    break;
                case 5: // $50 or Less
                    pledgeFilterAmount = 50;
                    break;
                case 6: // $100 or Less
                    pledgeFilterAmount = 100;
                    break;
                case 7: // $250 or Less
                    pledgeFilterAmount = 250;
                    break;
                case 8: // $500 or Less
                    pledgeFilterAmount = 500;
                    break;
                case 9: // $1000 or Less
                    pledgeFilterAmount = 1000;
                    break;
                case 10: // $1000 +
                    pledgeFilterAmount = 1000;
                    if (datePickerUsed)
                    {
                        _filteredPledgeVMs = _filteredPledgeVMs.Where(p => p.Amount > pledgeFilterAmount).ToList();
                    }
                    else
                    {
                        _filteredPledgeVMs = _pledgeVMs.Where(p => p.Amount > pledgeFilterAmount).ToList();
                    }
                    return;
                default:
                    break;
            }

            if (datePickerUsed)
            {
                _filteredPledgeVMs = _filteredPledgeVMs.Where(p => p.Amount <= pledgeFilterAmount).ToList();
            }
            else
            {
                _filteredPledgeVMs = _pledgeVMs.Where(p => p.Amount <= pledgeFilterAmount).ToList();
            }
        }

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/04/22
        /// 
        /// Helper method that filters based on the checkboxes. Completed means there is a
        /// donation Id that accompanies the pledge, indicating the pledge has been converted
        /// to a donation and therefore completed.
        /// </summary>
        ///
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/27
        /// Final QA
        /// </remarks>
        /// <param name="comboBoxOrDatePickerUsed">Boolean for if combo box or date picker have been used</param>
        public void FilterPledgesByCompletionStatus(bool comboBoxOrDatePickerUsed)
        {
            if (comboBoxOrDatePickerUsed == false)
            {
                _filteredPledgeVMs.AddRange(_pledgeVMs);
            }

            if (ckbPledgeCompleted.IsChecked == true && ckbPledgeNotCompleted.IsChecked == false)
            {
                _filteredPledgeVMs = _filteredPledgeVMs.Where(pledge => pledge.DonationId != 0).ToList();
            }
            if (ckbPledgeCompleted.IsChecked == false && ckbPledgeNotCompleted.IsChecked == true)
            {
                _filteredPledgeVMs = _filteredPledgeVMs.Where(pledge => pledge.DonationId == 0).ToList();
            }
        }

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/04/22
        /// 
        /// Helper method that sorts pledges by date ascending or descending as selected. If any
        /// filters have been applied it is the filtered list that is sorted. If no filters have
        /// been applied then the entire list (_pledgeVMs) is sorted.
        /// </summary>
        ///
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/27
        /// Final QA
        /// </remarks>
        /// <param name="filtersUsed">Boolean indicating if any filters have been used</param>
        public void SortPledges(bool filtersUsed)
        {
            List<PledgeVM> pledges = new List<PledgeVM>();
            pledges.AddRange(filtersUsed ? _filteredPledgeVMs : _pledgeVMs);

            switch (cmbSortOrder.SelectedIndex)
            {
                case 1: // Most recent
                    _filteredPledgeVMs = pledges.OrderByDescending(pledge => pledge.Date).ThenBy(pledge => pledge.FamilyName).ToList();
                    break;
                case 2: // Oldest
                    _filteredPledgeVMs = pledges.OrderBy(pledge => pledge.Date).ThenBy(pledge => pledge.FamilyName).ToList();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/04/21
        /// 
        /// Click event method that calls loadPledgers(), resetFilters(), and
        /// populatePage(_pledgeVMs) when the Reset button (label) is clicked
        /// </summary>
        ///
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/27
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblReset_MouseDown(object sender, MouseButtonEventArgs e)
        {
            LoadPledgers();
            ResetFilters();
            PopulatePage(_pledgeVMs);
        }


        // Event handlers for checking popup check boxes when label is clicked
        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/04/21
        /// 
        /// Event handler method to change the accompanying checkbox when it's
        /// label is clicked. Checks box if unchecked and unchecks box if checked.
        /// </summary>
        ///
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/27
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblPledgeCompleted_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (ckbPledgeCompleted.IsChecked == true)
            {
                ckbPledgeCompleted.IsChecked = false;
            }
            else
            {
                ckbPledgeCompleted.IsChecked = true;
            }
        }

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/04/21
        /// 
        /// Event handler method to change the accompanying checkbox when it's
        /// label is clicked. Checks box if unchecked and unchecks box if checked.
        /// </summary>
        ///
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/27
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblPledgeNotCompleted_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (ckbPledgeNotCompleted.IsChecked == true)
            {
                ckbPledgeNotCompleted.IsChecked = false;
            }
            else
            {
                ckbPledgeNotCompleted.IsChecked = true;
            }
        }


        // Event handlers for changing various popup labels' colors on mouse over
        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/04/21
        /// 
        /// Event handler method to change the color of the label when the mouse enters
        /// </summary>
        ///
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/27
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblFilter_MouseEnter(object sender, MouseEventArgs e)
        {
            lblFilter.Foreground = (Brush)new BrushConverter().ConvertFrom("#FF1C6758");
        }

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/04/21
        /// 
        /// Event handler method to change the color of the label when the mouse leaves
        /// </summary>
        ///
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/27
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblFilter_MouseLeave(object sender, MouseEventArgs e)
        {
            lblFilter.Foreground = (Brush)new BrushConverter().ConvertFrom("#FFEEF2E6");
        }

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/04/21
        /// 
        /// Event handler method to change the color of the label when the mouse enters
        /// </summary>
        ///
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/27
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblReset_MouseEnter(object sender, MouseEventArgs e)
        {
            lblReset.Foreground = (Brush)new BrushConverter().ConvertFrom("#FF1C6758");
        }

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/04/21
        /// 
        /// Event handler method to change the color of the label when the mouse leaves
        /// </summary>
        ///
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/27
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblReset_MouseLeave(object sender, MouseEventArgs e)
        {
            lblReset.Foreground = (Brush)new BrushConverter().ConvertFrom("#FFEEF2E6");
        }

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/04/21
        /// 
        /// Event handler method to change the color of the label when the mouse enters
        /// </summary>
        ///
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/27
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblPledgeCompleted_MouseEnter(object sender, MouseEventArgs e)
        {
            lblPledgeCompleted.Foreground = (Brush)new BrushConverter().ConvertFrom("#FF1C6758");
        }

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/04/21
        /// 
        /// Event handler method to change the color of the label when the mouse leaves
        /// </summary>
        ///
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/27
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblPledgeCompleted_MouseLeave(object sender, MouseEventArgs e)
        {
            lblPledgeCompleted.Foreground = (Brush)new BrushConverter().ConvertFrom("#FFEEF2E6");
        }

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/04/21
        /// 
        /// Event handler method to change the color of the label when the mouse enters
        /// </summary>
        ///
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/27
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblPledgeNotCompleted_MouseEnter(object sender, MouseEventArgs e)
        {
            lblPledgeNotCompleted.Foreground = (Brush)new BrushConverter().ConvertFrom("#FF1C6758");
        }

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/04/21
        /// 
        /// Event handler method to change the color of the label when the mouse leaves
        /// </summary>
        ///
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/27
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblPledgeNotCompleted_MouseLeave(object sender, MouseEventArgs e)
        {
            lblPledgeNotCompleted.Foreground = (Brush)new BrushConverter().ConvertFrom("#FFEEF2E6");
        }

    }
}