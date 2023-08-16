/// <summary>
/// Zaid Rachman
/// Created: 2023/04/19
/// 
/// 
/// </summary>
/// <remarks>
/// Oleksiy Fedchuk
/// Updated: 2023/04/20
/// Final QA
/// </remarks>
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
using DataObjects;

namespace WpfPresentation.Events
{
    /// <summary>
    /// Interaction logic for ViewEventGraphsPage.xaml
    /// </summary>
    public partial class ViewEventGraphsPage : Page
    {
        private static ViewEventGraphsPage _existingViewEventGraphsPage = null;

        MasterManager _masterManager = null;
        List<PledgeVM> _allPledges = null;
        List<DonationVM> _allDonations = null;

        List<PledgeVM> _filteredPledges = null;
        List<DonationVM> _filteredDonations = null;

        
        /// <summary>
        /// Zaid Rachman
        /// Created: 2023/04/20
        /// 
        /// 
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/20
        /// Final QA
        /// </remarks>
        private ViewEventGraphsPage()
        {

            InitializeComponent();

           _masterManager = MasterManager.GetMasterManager();
        }

        /// <summary>
        /// Zaid Rachman
        /// 2023/04/20
        /// 
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/20
        /// Final QA
        /// </remarks>
        /// <returns></returns>
        public static ViewEventGraphsPage GetViewEventGraphsPage()
        {
            if(_existingViewEventGraphsPage == null)
            {
                _existingViewEventGraphsPage = new ViewEventGraphsPage();
            }

            return _existingViewEventGraphsPage;
        }



        /// <summary>
        /// Zaid Rachman
        /// 2023/04/19
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/20
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                _allPledges = _masterManager.PledgeManager.RetrieveAllPledges();
                _allDonations = _masterManager.DonationManager.RetrieveAllDonations();
                _allDonations.RemoveAll(d => d.Amount == null);

            }
            catch (Exception ex)
            {
                PromptWindow.ShowPrompt("Data Retrieval Error", "Could not retrieve data. " + ex.Message);
                return;
            }
           
            dpStartDate.SelectedDate = DateTime.Now;          
            dpEndDate.SelectedDate = DateTime.Now;
            

            ApplyGraphFilter(((string)((ComboBoxItem)cbFilter.SelectedValue).Content).ToLower());
            SetGraphs();
        }


        /// <summary>
        /// Zaid Rachman
        /// 2023/04/19
        /// 
        /// Calculates the sum of all pledge amounts
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/20
        /// Final QA
        /// </remarks>
        /// <param name="pledgeList"></param>
        /// <returns></returns>
        private decimal CalculatePledgeTotal(List<PledgeVM> pledgeList)
        {
            decimal pledgeSum = 0;

            if(pledgeList != null){
                foreach (PledgeVM pledgeVM in pledgeList)
                {
                    pledgeSum = pledgeSum + pledgeVM.Amount;

                }
                

            }
            return pledgeSum;

        }

        /// <summary>
        /// Zaid Rachman
        /// 2023/04/19
        /// 
        /// Calculates the sum of all donation amounts
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/20
        /// Final QA
        /// </remarks>
        /// <param name="donationVMs"></param>
        /// <returns></returns>
        private decimal? CalculateDonationTotal(List<DonationVM> donationVMs)
        {
            decimal? donationSum = 0;

            if(donationVMs != null)
            {
                foreach(DonationVM donationVM in donationVMs)
                {
                    donationSum = donationSum + donationVM.Amount;
                }
            }

            return donationSum;
        }

        /// <summary>
        /// Zaid Rachman
        /// 2023/04/19
        /// 
        /// Calculates the sum of all outstanding pledges
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/20
        /// Final QA
        /// </remarks>
        /// <param name="pledgeVMs"></param>
        /// <returns></returns>
        private decimal? CalculatePledgesOutstandingTotal(List<PledgeVM> pledgeVMs)
        {
            decimal? outstandingPledgesSum = 0;

            foreach(PledgeVM pledgeVM in pledgeVMs)
            {
                if(pledgeVM.DonationId != 0) //Donation ID is nullable in the database, but in the accessor,
                {                            //if the db value is null then it defaults the ID to 0
                    try
                    {
                        DonationVM donation = _masterManager.DonationManager.RetrieveDonationByDonationId(pledgeVM.DonationId);
                        outstandingPledgesSum = outstandingPledgesSum + (pledgeVM.Amount - donation.Amount);
                    }
                    catch(Exception ex)
                    {
                        throw ex;
                    }

                }
                else
                {
                    outstandingPledgesSum = outstandingPledgesSum + pledgeVM.Amount;
                }
                
            }
            return outstandingPledgesSum;
        }

        /// <summary>
        /// Zaid Rachman
        /// 2023/04/20
        /// 
        /// Counts the amount of pledges whos goal has not been reached
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/20
        /// Final QA
        /// </remarks>
        /// <param name="pledgeVMs"></param>
        /// <returns></returns>
        private int PledgesOutstandingCount(List<PledgeVM> pledgeVMs)
        {
            int count = 0;

            foreach (PledgeVM pledgeVM in pledgeVMs)
            {
                if (pledgeVM.DonationId != 0) //Donation ID is nullable in the database, but in the accessor,
                {                            //if the db value is null then it defaults the ID to 0
                    try
                    {
                        DonationVM donation = _masterManager.DonationManager.RetrieveDonationByDonationId(pledgeVM.DonationId);
                        if(!(pledgeVM.Amount - donation.Amount == 0)) //if donation amount does not equal 0, pledge has not reached it's goal
                        {
                            count++;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
            return count;
        }
        /// <summary>
        /// Zaid Rachman
        /// 2023/04/20
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/20
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (dpStartDate.Text == "")
            {
                PromptWindow.ShowPrompt("Date Error", "Please Enter the date a start date");
                dpStartDate.Focus();
                return;
            }
            if (dpEndDate.Text == "")
            {
                PromptWindow.ShowPrompt("Date Error", "Please Enter the date an end date");
                dpEndDate.Focus();
                return;
            }

            if(dpStartDate.SelectedDate.Value > dpEndDate.SelectedDate.Value)
            {
                PromptWindow.ShowPrompt("Date Error", "Start Date cannot be later than End Date");
                dpEndDate.SelectedDate = dpStartDate.SelectedDate.Value;
                dpStartDate.Focus();
                return;
            }

            try
            {
                _allPledges = _masterManager.PledgeManager.RetrieveAllPledges();
                _allDonations = _masterManager.DonationManager.RetrieveAllDonations();

            }
            catch (Exception ex)
            {
                PromptWindow.ShowPrompt("Data Retrieval Error", "Could not retrieve data. " + ex.Message);
                return;
                
            }

           
           
                                    
            ApplyGraphFilter(((string)((ComboBoxItem)cbFilter.SelectedValue).Content).ToLower());

            SetGraphs();

          
        }

        /// <summary>
        /// Zaid Rachman
        /// 2023/04/20
        /// 
        /// Filters graph results
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/20
        /// Final QA
        /// </remarks>
        /// <param name="option"></param>
        private void ApplyGraphFilter(string option)
        {
            switch (option)
            {
                case "custom":
                    ToggleCustomFilter(true);
                    _filteredDonations = _allDonations.Where(d => d.DateDonated >= dpStartDate.SelectedDate.Value
                                                                                && d.DateDonated <= dpEndDate.SelectedDate.Value).ToList();
                    _filteredPledges = _allPledges.Where(d => d.Date >= dpStartDate.SelectedDate.Value
                                                                                && d.Date <= dpEndDate.SelectedDate.Value).ToList();
                    break;
                case "last 6 months":
                    ToggleCustomFilter(false);
                    _filteredDonations = _allDonations.Where(d => d.DateDonated >= DateTime.Now.AddMonths(-6)
                                                                                && d.DateDonated <= DateTime.Now).ToList();
                    _filteredPledges = _allPledges.Where(d => d.Date >= DateTime.Now.AddMonths(-6)
                                                                                && d.Date <= DateTime.Now).ToList();
                    SetGraphs();
                    break;

                case "last year":
                    ToggleCustomFilter(false);
                    _filteredDonations = _allDonations.Where(d => d.DateDonated >= DateTime.Now.AddYears(-1)
                                                                                && d.DateDonated <= DateTime.Now).ToList();

                    _filteredPledges = _allPledges.Where(d => d.Date >= DateTime.Now.AddYears(-1)
                                                                                && d.Date <= DateTime.Now.AddDays(1)).ToList();
                                                                                
                    SetGraphs();
                    break;
                case "all time":
                    {
                        _filteredDonations = _allDonations;
                        _filteredPledges = _allPledges;
                        Console.WriteLine(_allPledges.Count);

                        SetGraphs();
                        break;
                    }

                default: //Default is last 30 days
                    ToggleCustomFilter(false);
                    _filteredDonations = _allDonations.Where(d => d.DateDonated >= DateTime.Now.AddDays(-30)
                                                                                && d.DateDonated <= DateTime.Now).ToList();


                    _filteredPledges = _allPledges.Where(d => d.Date >= DateTime.Now.AddDays(-30)
                                                                                && d.Date <= DateTime.Now).ToList();
                                                                                
                    SetGraphs();
                    break;

            }
                

            
        }
        /// <summary>
        /// Zaid Rachman
        /// 2023/04/20
        /// 
        /// Toggles Custom Filters
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/20
        /// Final QA
        /// </remarks>
        /// <param name="toggle"></param>
        private void ToggleCustomFilter(bool toggle)
        {
            dpEndDate.IsEnabled = toggle;
            dpStartDate.IsEnabled = toggle;
            btnUpdate.IsEnabled = toggle;

       
        }

        /// <summary>
        /// Zaid Rachman
        /// 2023/04/20
        /// 
        /// Sets and refreshes graphs
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/20
        /// Final QA
        /// </remarks>
        private void SetGraphs()
        {
            decimal totalPledgeAmount = CalculatePledgeTotal(_filteredPledges);
            
            decimal? totalDonationAmount = CalculateDonationTotal(_filteredDonations);
            decimal? totalOutstandingPledge = CalculatePledgesOutstandingTotal(_filteredPledges);

            int donationCount = _filteredDonations.Count;
            int pledgesCount = _filteredPledges.Count;
            int pledgeOutstandingCount = PledgesOutstandingCount(_filteredPledges);
            int pledgeCompletedCount = pledgesCount - pledgeOutstandingCount;
            decimal? avgDonation;
            decimal? avgPledge;
            decimal pledgeConverted; 

            
            if (donationCount == 0)
            {
                avgDonation = 0;
            }
            else
            {
                avgDonation = totalDonationAmount / donationCount;
            }

            
            if (pledgesCount != 0)
            {
                avgPledge = totalPledgeAmount / pledgesCount;
                pledgeConverted = (((decimal)pledgeCompletedCount / (decimal)pledgesCount));
                
               
            }
            else
            {
                avgPledge = 0;
                pledgeConverted = 0;
            }

            //spTotalsGraph setup
            double[] totalandCountGraphPositions = { 0, 1, 2 };  //spTotalsGraph and spCountGraph both have 3 positions, so both use 'GraphPositions' for their positions
            double[] spTotalsGraphValues = { (double)totalDonationAmount, (double)totalPledgeAmount, (double)totalOutstandingPledge };
            string[] spTotalsGraphLabels = { "Total Donation $" + totalDonationAmount, "Total Pledge Amount $" + totalPledgeAmount, "Total Outstanding $" + totalOutstandingPledge };
            spTotalsGraph.Plot.Clear();
            spTotalsGraph.Plot.AddBar(spTotalsGraphValues, totalandCountGraphPositions).FillColor = System.Drawing.ColorTranslator.FromHtml("#3D8361");
            spTotalsGraph.Plot.XTicks(totalandCountGraphPositions, spTotalsGraphLabels);
            spTotalsGraph.Plot.SetAxisLimits(yMin: 0);
            spTotalsGraph.Plot.Title("Donations \\ Pledges By $");

            spTotalsGraph.Refresh();

            //spCountGraph setup
            double[] spCountGraphValues = { (double)donationCount, (double)pledgesCount, (double)pledgeOutstandingCount };
            string[] spCountGraphLabels = { "# of Donations: " + donationCount, "# of Pledges: " + pledgesCount, "# of Pledges Outstanding: " + pledgeOutstandingCount };
            spCountGraph.Plot.Clear();
            spCountGraph.Plot.AddBar(spCountGraphValues, totalandCountGraphPositions).FillColor = System.Drawing.ColorTranslator.FromHtml("#3D8361");
            spCountGraph.Plot.XTicks(totalandCountGraphPositions, spCountGraphLabels);
            spCountGraph.Plot.SetAxisLimits(yMin: 0);
            spCountGraph.Plot.Title("Donations \\ Pledges By #");

            spCountGraph.Refresh();

            //spAverageGraph
            double[] averageGraphPositions = { 0, 1 };
            double[] spAverageGraphValues = {   (double)avgDonation,
                                                (double)avgPledge
                                                };
            string[] spAverageGraphLabels = { "Average Donation $" + Decimal.Round((decimal)avgDonation, 2), "Avg Pledges $" + Decimal.Round((decimal)avgPledge, 2)};
            spAverageGraph.Plot.Clear();
            spAverageGraph.Plot.AddBar(spAverageGraphValues, averageGraphPositions).FillColor = System.Drawing.ColorTranslator.FromHtml("#3D8361");
            spAverageGraph.Plot.XTicks(averageGraphPositions, spAverageGraphLabels);
            spAverageGraph.Plot.SetAxisLimits(yMin: 0);
            spAverageGraph.Plot.Title("Pledge & Donation Averages");

            spAverageGraph.Refresh();

            //spPledgeConversionGraph
            double[] spPledgeConversionGraphPosition = { 0 };
            double[] spPledgeConversionGraphValues = {  (double)pledgeConverted };
            string[] spPledgeConversionGraphLabels = { "Pledge Conversion Rate: " + (double)Decimal.Round(pledgeConverted * 100, 2) + "%"};
            spPledgeConversionGraph.Plot.Clear();
            spPledgeConversionGraph.Plot.AddBar(spPledgeConversionGraphValues, spPledgeConversionGraphPosition).FillColor = System.Drawing.ColorTranslator.FromHtml("#3D8361");
            spPledgeConversionGraph.Plot.XTicks(spPledgeConversionGraphPosition, spPledgeConversionGraphLabels);
            spPledgeConversionGraph.Plot.YAxis.TickLabelFormat("P1", dateTimeFormat: false);
            spPledgeConversionGraph.Plot.SetAxisLimits(yMin: 0);
            spPledgeConversionGraph.Plot.Title("Pledge Conversion Rate");

            spPledgeConversionGraph.Refresh();
        }
        /// <summary>
        /// Zaid Rachman
        /// 2023/04/20
        /// 
        /// Activates filter when dropdown is closed
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/20
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbFilter_DropDownClosed(object sender, EventArgs e)
        {
            string filterString = ((string)((ComboBoxItem)cbFilter.SelectedValue).Content).ToLower();
            if (filterString.ToLower().Equals("custom"))
            {
                
                ToggleCustomFilter(true);
            }
            else
            {
                ToggleCustomFilter(false);
            }

            ApplyGraphFilter(filterString);

        }
    }

    


}
