using DataObjects;
using LogicLayer;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using WpfPresentation.UserControls;

namespace WpfPresentation.Fundraising
{
    /// <summary>
    /// Interaction logic for ViewDonationsPage.xaml
    /// </summary>
    public partial class ViewDonationsPage : Page
    {
        private MasterManager masterManager = MasterManager.GetMasterManager();
        private static ViewDonationsPage existingViewDonationsPage = null;
        private List<DonationVM> donationVMs = null;
        private List<DonationVM> _filteredDonationVMs = new List<DonationVM>();
        private Dictionary<string, List<DonationVM>> _donorNamesAndDonationsDict 
            = new Dictionary<string, List<DonationVM>>();
        private List<string> _filterCategories = new List<string>()
        {
            "Date",
            "Donor's Name",
            "Amount"
        };
        private List<string> _dateRanges = new List<string>()
        {
            "Last Day",
            "Last Week",
            "Last Month",
            "Last 6 Months",
            "Last Year"
        };
        private List<string> _donationAmountStrings = new List<string>()
        {
            "$1",
            "$5 or Less",
            "$10 or Less",
            "$25 or Less",
            "$50 or Less",
            "$100 or Less",
            "$250 or Less",
            "$500 or Less",
            "$1000 or Less",
            "$1000 +"
        };

        /// <summary>
        /// Author: Gwen
        /// Date: 4/21/23
        /// </summary>
        public static ViewDonationsPage ExistingDonationPage 
        {
            get 
            {
                if (existingViewDonationsPage == null)
                {
                    return existingViewDonationsPage = new ViewDonationsPage();
                }
                return existingViewDonationsPage;
            }
             private set { }
        }

        /// <summary>
        /// Author: Gwen
        /// Date: 4/21/23
        /// </summary>
        public ViewDonationsPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ResetFilters();
            PopulatePage();
        }

        /// <summary>
        /// Gwen Arman
        /// Created: 2023/03/03
        /// 
        /// </summary>
        ///
        /// <remarks>
        /// Updater: Andrew Schneider
        /// Updated: 2023/03/18
        /// Originally part of Page_Loaded but moved to a separate method so it could be called
        /// from Page_Loaded and the "Reset" button on the Filter Donations popup.
        /// </remarks>
        private void PopulatePage()
        {
            spDonations.Children.Clear();

            try
            {
                donationVMs = masterManager.DonationManager.RetrieveDonationsByShelterId(masterManager.User.ShelterId.Value);

                for (int i = 0; i < donationVMs.Count; i++)
                {
                    donationVMs[i].GivenName = donationVMs[i].UserId != null ? donationVMs[i].User.GivenName : donationVMs[i].GivenName;
                    donationVMs[i].FamilyName = donationVMs[i].UserId != null ? donationVMs[i].User.FamilyName : donationVMs[i].FamilyName;
                    DonationUserControl donationUserControl = new DonationUserControl(donationVMs[i], i % 2 == 1);

                    spDonations.Children.Add(donationUserControl);
                }

                PopulateDonorNameOptionComboBox();
            }
            catch (Exception ex)
            {
                PromptWindow.ShowPrompt("Error", ex.Message);
            }
        }

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/03/15
        /// 
        /// Click event method to open Filter Donations popup if
        /// it's closed and close it if it's open.
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFilterDonations_Click(object sender, RoutedEventArgs e)
        {
            if(popFilterDonations.IsOpen == false)
            {
                popFilterDonations.IsOpen = true;
            }
            else
            {
                popFilterDonations.IsOpen = false;
            }
        }

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/03/19
        /// 
        /// Main click event method for filtering donations. Checks if any options have been selected from
        /// the combo boxes and calls the appropriate methods. Calls method to check checkboxes, passing 
        /// boolean to indicate if the combo boxes have been used. Displays message if there are no matches
        /// for the filter. Adds all of the filtered donations to spDonations to be displayed.
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblFilter_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _filteredDonationVMs.Clear();
            spDonations.Children.Clear();

            if (cmbFilterCategory.SelectedItem != null && cmbFilterOption.SelectedItem != null)
            {
                switch (cmbFilterCategory.SelectedItem.ToString())
                {
                    case "Date":
                        FilterDonationsByDates();
                        break;
                    case "Donor's Name":
                        FilterDonationsByDonor();
                        break;
                    case "Amount":
                        FilterDonationsByDonationAmount();
                        break;
                    default:
                        break;
                }
                CheckMiscellaneousFilters(true);
            }
            else
            {
                CheckMiscellaneousFilters(false);
            }

            if (_filteredDonationVMs.Count == 0)
            {
                PromptWindow.ShowPrompt("", "No donations matched the filter", ButtonMode.Ok);
                PopulatePage();
            }

            for (int i = 0; i < _filteredDonationVMs.Count; i++)
            {
                DonationUserControl donationUserControl = new DonationUserControl(_filteredDonationVMs[i], i % 2 == 1);
                spDonations.Children.Add(donationUserControl);
            }
        }

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/03/18
        /// 
        /// Event method that changes the available options in the Filter Options
        /// combo box based on what has been selected in the Filter Categories combo box.
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbFilterCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(cmbFilterCategory.SelectedItem == null)
            {
                cmbFilterOption.IsEnabled = false;
                return;
            }
            if(cmbFilterCategory.SelectedValue.Equals("Date"))
            {
                cmbFilterOption.ItemsSource = _dateRanges;
                cmbFilterOption.IsEnabled = true;

            }
            else if(cmbFilterCategory.SelectedValue.Equals("Donor's Name"))
            {
                cmbFilterOption.ItemsSource = _donorNamesAndDonationsDict.Keys;
                cmbFilterOption.IsEnabled = true;
            }
            else if(cmbFilterCategory.SelectedValue.Equals("Amount"))
            {
                cmbFilterOption.ItemsSource = _donationAmountStrings;
                cmbFilterOption.IsEnabled = true;
            }
            else
            {
                cmbFilterOption.IsEnabled = false;
                return;
            }
        }

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/03/18
        /// 
        /// Helper method to combine family and given names into a single string
        /// and place it in a dictionary object as the key for the donation 
        /// record object from which the name came.
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        private void PopulateDonorNameOptionComboBox()
        {
            string name;
            foreach (var donationVM in donationVMs)
            {
                // Construct name based on which parts of name are part of the record
                if((donationVM.FamilyName == null || donationVM.FamilyName == "") 
                    && (donationVM.GivenName == null || donationVM.GivenName == ""))
                {
                    name = "No name";
                }
                else if(donationVM.FamilyName == null || donationVM.FamilyName == "")
                {
                    name = donationVM.GivenName;
                }
                else if (donationVM.GivenName == null || donationVM.GivenName == "")
                {
                    name = donationVM.FamilyName;
                }
                else
                {
                    name = donationVM.FamilyName + ", " + donationVM.GivenName;
                }

                // If the name (family by itself, given by itself, or full combination) is in the dictionary
                // add the donation to the associated list otherwise create a new entry in the dictionary
                if (_donorNamesAndDonationsDict.ContainsKey(name))
                {
                    _donorNamesAndDonationsDict[name].Add(donationVM);
                }
                else
                {
                    List<DonationVM> donationList = new List<DonationVM> { donationVM };
                    _donorNamesAndDonationsDict.Add(name, donationList);
                }
            }
        }

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/03/19
        /// 
        /// Helper method using switch statement to filter donations by date according
        /// to user's selection.
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        private void FilterDonationsByDates()
        {
            int dateRange = 0;
            switch (cmbFilterOption.SelectedItem.ToString())
            {
                case "Last Day":
                    dateRange = 1;
                    break;
                case "Last Week":
                    dateRange = 7;
                    break;
                case "Last Month":
                    dateRange = 30;
                    break;
                case "Last 6 Months":
                    dateRange = 180;
                    break;
                case "Last Year":
                    dateRange = 365;
                    break;
                default:
                    break;
            }

            for (int i = 0; i < donationVMs.Count; i++)
            {
                if (!(donationVMs[i].DateDonated < (DateTime.Today - TimeSpan.FromDays(dateRange))))
                {
                    _filteredDonationVMs.Add(donationVMs[i]);
                }
            }
        }

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/03/20
        /// 
        /// Helper method that searches _donorNamesAndDonationsDict dictionary for the
        /// seleced donor name that serves as the key for lists of DonationVM objects.
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        private void FilterDonationsByDonor()
        {
            _filteredDonationVMs = _donorNamesAndDonationsDict[cmbFilterOption.SelectedItem.ToString()];
        }

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/03/19
        /// 
        /// Helper method using switch statement to filter donations by the amount of the donation.
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        private void FilterDonationsByDonationAmount()
        {
            decimal donationFilterAmount = 0;
            switch (cmbFilterOption.SelectedItem.ToString())
            {
                case "$1":
                    donationFilterAmount = 1;
                    break;
                case "$5 or Less":
                    donationFilterAmount = 5;
                    break;
                case "$10 or Less":
                    donationFilterAmount = 10;
                    break;
                case "$25 or Less":
                    donationFilterAmount = 25;
                    break;
                case "$50 or Less":
                    donationFilterAmount = 50;
                    break;
                case "$100 or Less":
                    donationFilterAmount = 100;
                    break;
                case "$250 or Less":
                    donationFilterAmount = 250;
                    break;
                case "$500 or Less":
                    donationFilterAmount = 500;
                    break;
                case "$1000 or Less":
                    donationFilterAmount = 1000;
                    break;
                case "$1000 +":
                    donationFilterAmount = 1000;
                    for (int i = 0; i < donationVMs.Count; i++)
                    {
                        if (donationVMs[i].Amount == null)
                        {
                            continue;
                        }
                        if (donationVMs[i].Amount > donationFilterAmount)
                        {
                            _filteredDonationVMs.Add(donationVMs[i]);
                        }
                    }
                    return;
                default:
                    break;
            }

            for (int i = 0; i < donationVMs.Count; i++)
            {
                if (donationVMs[i].Amount == null)
                {
                    continue;
                }
                if (donationVMs[i].Amount <= donationFilterAmount)
                {
                    _filteredDonationVMs.Add(donationVMs[i]);
                }
            }
        }

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/03/20
        /// 
        /// Helper method that sees if any miscellaneous filter checkboxes have been 
        /// checked and applies filters accordingly.
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="comboBoxesUsed">Boolean for if combo boxes have been used</param>
        private void CheckMiscellaneousFilters(bool comboBoxesUsed)
        {
            if(comboBoxesUsed == false)
            {
                _filteredDonationVMs = donationVMs;
            }

            IEnumerable<DonationVM> _filteredDonationVMsIE = _filteredDonationVMs.AsEnumerable();

            if(ckbHasMessage.IsChecked == true && ckbNoMessage.IsChecked == false)
            {
                _filteredDonationVMsIE = _filteredDonationVMsIE.Where(d => d.Message != "" && d.Message != null);
            }
            if (ckbHasMessage.IsChecked == false && ckbNoMessage.IsChecked == true)
            {
                _filteredDonationVMsIE = _filteredDonationVMsIE.Where(d => d.Message == "" || d.Message == null);
            }
            if (ckbShowFinancialDonations.IsChecked == true && ckbShowInKindDonations.IsChecked == false)
            {
                _filteredDonationVMsIE = _filteredDonationVMsIE.Where(d => d.HasInKindDonation == false);
            }
            if(ckbShowFinancialDonations.IsChecked == false && ckbShowInKindDonations.IsChecked == true)
            {
                _filteredDonationVMsIE = _filteredDonationVMsIE.Where(d => d.HasInKindDonation == true);
            }

            _filteredDonationVMs = _filteredDonationVMsIE.ToList();
        }

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/03/20
        /// 
        /// Helper method that resets filter options when page is loaded or
        /// filter "Reset" button (label) is clicked.
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        private void ResetFilters()
        {
            cmbFilterCategory.ItemsSource = null;
            cmbFilterOption.ItemsSource = null;
            cmbFilterCategory.ItemsSource = _filterCategories;
            ckbHasMessage.IsChecked = false;
            ckbNoMessage.IsChecked = false;
            ckbShowFinancialDonations.IsChecked = false;
            ckbShowInKindDonations.IsChecked = false;
        }

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/03/19
        /// 
        /// Click event method that calls PopulatePage() when the Reset label is clicked
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// </remarks>
        private void lblReset_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ResetFilters();
            PopulatePage();
        }

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/03/16
        /// 
        /// Event handler method to change the accompanying checkbox when it's
        /// label is clicked. Checks box if unchecked and unchecks box if checked.
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblNoMessage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (ckbNoMessage.IsChecked == true)
            {
                ckbNoMessage.IsChecked = false;
            }
            else
            {
                ckbNoMessage.IsChecked = true;
            }
        }

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/03/16
        /// 
        /// Event handler method to change the accompanying checkbox when it's
        /// label is clicked. Checks box if unchecked and unchecks box if checked.
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblHasMessage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (ckbHasMessage.IsChecked == true)
            {
                ckbHasMessage.IsChecked = false;
            }
            else
            {
                ckbHasMessage.IsChecked = true;
            }
        }

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/03/16
        /// 
        /// Event handler method to change the accompanying checkbox when it's
        /// label is clicked. Checks box if unchecked and unchecks box if checked.
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblShowFinancialDonations_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (ckbShowFinancialDonations.IsChecked == true)
            {
                ckbShowFinancialDonations.IsChecked = false;
            }
            else
            {
                ckbShowFinancialDonations.IsChecked = true;
            }
        }

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/03/16
        /// 
        /// Event handler method to change the accompanying checkbox when it's
        /// label is clicked. Checks box if unchecked and unchecks box if checked.
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblShowInKindDonations_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (ckbShowInKindDonations.IsChecked == true)
            {
                ckbShowInKindDonations.IsChecked = false;
            }
            else
            {
                ckbShowInKindDonations.IsChecked = true;
            }
        }

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/03/17
        /// 
        /// Event handler method to change the color of the label when the mouse enters
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblReset_MouseEnter(object sender, MouseEventArgs e)
        {
            lblReset.Foreground = (Brush)new BrushConverter().ConvertFrom("#FF1C6758");
        }

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/03/17
        /// 
        /// Event handler method to change the color of the label when the mouse leaves
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblReset_MouseLeave(object sender, MouseEventArgs e)
        {
            lblReset.Foreground = (Brush)new BrushConverter().ConvertFrom("#FFEEF2E6");
        }

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/03/17
        /// 
        /// Event handler method to change the color of the label when the mouse enters
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblFilter_MouseEnter(object sender, MouseEventArgs e)
        {
            lblFilter.Foreground = (Brush)new BrushConverter().ConvertFrom("#FF1C6758");
        }

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/03/17
        /// 
        /// Event handler method to change the color of the label when the mouse leaves
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblFilter_MouseLeave(object sender, MouseEventArgs e)
        {
            lblFilter.Foreground = (Brush)new BrushConverter().ConvertFrom("#FFEEF2E6");
        }

        /// <summary>
        /// William Rients
        /// Created: 4/21/23
        /// 
        /// Generates a flow document based 
        /// on the list of donations on the page
        /// 
        /// </summary>
        /// <returns>FlowDocument</returns>
        private FlowDocument generateReport()
        {
            FlowDocument report = new FlowDocument();

            Paragraph heading = new Paragraph(new Run("Donation Report"+ "\n"));
            heading.FontSize = 24;
            heading.FontWeight = FontWeights.Bold;
            report.Blocks.Add(heading);

            Paragraph body = new Paragraph();
            body.FontSize = 12;
            foreach (DonationVM donation in donationVMs)
            {
                body.Inlines.Add("Name: " + donation.GivenName + " " + donation.FamilyName + "\n");
                body.Inlines.Add("Amount: " + donation.Amount + "\n");
                body.Inlines.Add("Date: " + donation.DateDonated + "\n");
                body.Inlines.Add("Message: " + donation.Message + "\n\n");

            }
            report.Blocks.Add(body);

            return report;
        }

        /// <summary>
        /// William Rients
        /// Created: 4/21/23
        /// 
        /// Calls the generateReport method and
        /// opens a save dialog prompting the user 
        /// to pick a location to save the report
        /// 
        /// </summary>
        /// <returns></returns>
        private void btnReport_Click(object sender, RoutedEventArgs e)
        {
            FlowDocument document = generateReport();

            TextRange range = new TextRange(document.ContentStart, document.ContentEnd);
            string plainText = range.Text;

            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Text files (*.txt)|*.txt";
            dialog.FileName = "DonationReport.txt";
            if (dialog.ShowDialog() == true)
            {
                using (StreamWriter writer = new StreamWriter(dialog.FileName))
                {
                    writer.Write(plainText);
                }
            }
        }

        /// <summary>
        /// Author: Asa Armstrong
        /// Date: 2023/04/23
        /// Description: Navigates to the EnterDonation Page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEnterDonation_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EnterDonation());
        }
    }
}
