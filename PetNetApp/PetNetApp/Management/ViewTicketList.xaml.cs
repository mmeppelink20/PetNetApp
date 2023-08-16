/// <summary>
/// Mads Rhea
/// Created: 2023/02/05
/// 
/// </summary>
/// <remarks>
/// Oleksiy Fedchuk
/// Updated: 2023/04/28
/// 
/// Final QA
/// </remarks>
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfPresentation.Management;

namespace WpfPresentation.Management
{
    /// <summary>
    /// Interaction logic for ViewTicketList.xaml
    /// </summary>
    public partial class ViewTicketList : Page
    {
        private MasterManager _masterManager = MasterManager.GetMasterManager();
        private List<TicketVM> _ticketVMs = null;


        public ViewTicketList(MasterManager manager)
        {
            InitializeComponent();
            _masterManager = manager;
            cmboSortStatus.SelectedItem = null;
            cmboSortUser.SelectedItem = null;
            btnSortStartDate.SelectedDate = null;
            btnSortEndDate.SelectedDate = null;
            btnSortEndDate.IsEnabled = false;
        }

        /// <summary>
        /// William Rients
        /// Created: 2023/02/17
        /// 
        /// When page loads, a list of tickets is
        /// retrieved and the data grid is populated
        /// </summary>
        ///
        /// <remarks>
        /// Mads Rhea
        /// Updated: 2023/04/24
        /// Added sort combobox item sources
        /// </remarks>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/28
        /// 
        /// Final QA
        /// </remarks>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                _ticketVMs = _masterManager.TicketManager.RetrieveAllTickets();
                if (_ticketVMs.Count > 0)
                {
                    datTickList.ItemsSource = _ticketVMs;
                }
                else
                {
                    PromptWindow.ShowPrompt("Error", "No tickets avaliable.", ButtonMode.Ok);
                }
                cmboSortUser.ItemsSource = _masterManager.TicketManager.RetrieveAllEmailsFromTickets();
                cmboSortStatus.ItemsSource = _masterManager.TicketManager.RetrieveAllTicketStatusId();
            }
            catch (Exception ex)
            {
                PromptWindow.ShowPrompt("Error", ex.InnerException.Message);
            }

            

        }

        /// <summary>
        /// William Rients
        /// Created: 2023/02/17
        /// 
        /// When search box is selected, 
        /// "Search..." is removed
        /// </summary>
        ///
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/28
        /// 
        /// Final QA
        /// </remarks>
        private void txtSearch_GotFocus(object sender, RoutedEventArgs e)
        {
            if(txtSearch.Text == "" || txtSearch.Text == "Search...")
            {
                txtSearch.Text = "";
            }
        }

        /// <summary>
        /// William Rients
        /// Created: 2023/02/17
        /// 
        /// When search box is not focused,
        /// "Search..." is placed in the search box
        /// </summary>
        ///
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/28
        /// 
        /// Final QA
        /// </remarks>
        private void txtSearch_LostFocus(object sender, RoutedEventArgs e)
        {
            if(txtSearch.Text == "")
            {
                txtSearch.Text = "Search...";
            }
        }

        /// <summary>
        /// Matthew Meppelink
        /// Created: 2023/03/01
        /// 
        /// refreshes the list of tickets with the passed in list of
        /// TicketVM
        /// 
        /// </summary>
        ///
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/28
        /// 
        /// Final QA
        /// </remarks>
        private void refreshTickets(List<TicketVM> ticketVM)
        {
            datTickList.ItemsSource = ticketVM;
        }

        /// <summary>
        /// Matthew Meppelink
        /// Created: 2023/03/01
        /// 
        /// refreshes the list of tickets 
        /// 
        /// </summary>
        ///
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/28
        /// 
        /// Final QA
        /// </remarks>
        public void refreshTickets()
        {
            datTickList.ItemsSource = null;
            datTickList.ItemsSource = _ticketVMs;
        }

        /// <summary>
        /// Matthew Meppelink
        /// Created: 2023/03/01
        /// 
        /// calls one of two refreshTickets methods based on the content of txtSearch
        /// 
        /// 
        /// </summary>
        ///
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/28
        /// 
        /// Final QA
        /// </remarks>
        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Return)
            {
                if (txtSearch.Text.Length != 0)
                {
                    refreshTickets(searchResults(_ticketVMs));
                }
                else
                {
                    refreshTickets(_ticketVMs);
                }
            }

        }

        /// <summary>
        /// Matthew Meppelink
        /// Created: 2023/03/01
        /// 
        /// searches the provided list of TicketVM for the provided search query
        /// in txtSearch
        /// 
        /// </summary>
        ///
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/28
        /// 
        /// Final QA
        /// </remarks>
        private List<TicketVM> searchResults(List<TicketVM> tickets)
        {
            List<TicketVM> ticketVM = new List<TicketVM>();

            foreach(TicketVM ticket in tickets)
            {
                // implements similar functionality to the sql "like" keyword.
                if (ticket.TicketTitle.IndexOf(txtSearch.Text, 0, StringComparison.OrdinalIgnoreCase) != -1)
                {
                    ticketVM.Add(ticket);
                }

            }

            return ticketVM;
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (txtSearch.Text.Length != 0 && txtSearch.Text != "Search...")
            {
                refreshTickets(searchResults(_ticketVMs));
            }
            else
            {
                refreshTickets(_ticketVMs);
            }
        }

        /// <summary>
        /// William Rients
        /// Created: 2023/03/03
        /// 
        /// When button is clicked,
        /// create a ticket page is opened
        /// </summary>
        ///
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/28
        /// 
        /// Final QA
        /// </remarks>
        private void btnNewTicket_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CreateNewTicket(_masterManager));
        }

        /// <summary>
        /// Matthew Meppelink
        /// Created: 2023/03/16
        /// 
        /// Opens ticket information when ticket
        /// button is clicked
        /// </summary>
        ///
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/28
        /// 
        /// Final QA
        /// </remarks>
        private void btnTicket_Click(object sender, RoutedEventArgs e)
        {
            DataGrid dataGrid = datTickList;
            DataGridRow Row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(dataGrid.SelectedIndex);
            DataGridCell RowAndColumn = (DataGridCell)dataGrid.Columns[0].GetCellContent(Row).Parent;
            string CellValue = ((TextBlock)RowAndColumn.Content).Text;

            foreach (TicketVM ticketVM in _ticketVMs)
            {
                if (ticketVM.TicketId == Int32.Parse(CellValue))
                {
                    frmTicketViewPage.Navigate(new ViewTicketPage(ticketVM, _masterManager, this));
                }
            }
        }


        /// <summary>
        /// Mads Rhea
        /// Created: 2023/04/20
        /// 
        /// Expands the "Sort By" submenu
        /// 
        /// 
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/28
        /// 
        /// Final QA
        /// </remarks>
        private void btnSortBy_Click(object sender, RoutedEventArgs e)
        {

            if (stkpnlSortBy.Visibility == Visibility.Visible)
            {
                if ((string)btnSortBy.Content == "Reset")
                {
                    cmboSortStatus.SelectedItem = null;
                    cmboSortUser.SelectedItem = null;
                    btnSortStartDate.SelectedDate = null;
                    btnSortEndDate.SelectedDate = null;
                    btnSortEndDate.IsEnabled = false;
                    refreshTickets();

                }
                else
                {
                    stkpnlSortBy.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                stkpnlSortBy.Visibility = Visibility.Visible;
            }

            btnSortBy.Content = "Sort By:";

        }

        /// <summary>
        /// Mads Rhea
        /// Created: 2023/04/20
        /// 
        /// Clears other filters and updates ticket list
        /// based on user selected from dropdown
        /// 
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/28
        /// 
        /// Final QA
        /// </remarks>
        private void cmboSortUser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnSortBy.Content = "Reset";
            cmboSortStatus.SelectedItem = null;
            btnSortStartDate.SelectedDate = null;
            btnSortEndDate.SelectedDate = null;
            btnSortEndDate.IsEnabled = false;

            if (cmboSortUser.SelectedItem != null)
            {
                try
                {

                    List<TicketVM> tickets = _masterManager.TicketManager.RetrieveTicketsByEmail(cmboSortUser.SelectedItem.ToString());
                    refreshTickets(tickets);
                }
                catch (Exception up)
                {
                    throw up;
                }
            }
        }

        /// <summary>
        /// Mads Rhea
        /// Created: 2023/04/20
        /// 
        /// Clears other filters and updates ticket list
        /// based on TicketStatusId selected
        /// 
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/28
        /// 
        /// Final QA
        /// </remarks>
        private void cmboSortStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnSortBy.Content = "Reset";
            cmboSortUser.SelectedItem = null;
            btnSortStartDate.SelectedDate = null;
            btnSortEndDate.SelectedDate = null;
            btnSortEndDate.IsEnabled = false;

            if (cmboSortStatus.SelectedItem != null)
            {
                try
                {
                    List<TicketVM> tickets = _masterManager.TicketManager.RetrieveTicketsByTicketStatusId(cmboSortStatus.SelectedItem.ToString());
                    refreshTickets(tickets);
                }
                catch (Exception up)
                {
                    throw up;
                }
            }
        }

        /// <summary>
        /// Mads Rhea
        /// Created: 2023/04/20
        /// 
        /// Clears other filters and updates ticket list
        /// based on startdate/enddate given
        /// 
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/28
        /// 
        /// Final QA
        /// </remarks>
        private void btnSortStartDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {   
            btnSortBy.Content = "Reset";
            cmboSortStatus.SelectedItem = null;
            cmboSortUser.SelectedItem = null;
            btnSortEndDate.IsEnabled = true;

            if (btnSortStartDate.SelectedDate != null && btnSortEndDate.SelectedDate == null)
            {
                try
                {
                    DateTime date = (DateTime)btnSortStartDate.SelectedDate;

                    List<TicketVM> tickets = _masterManager.TicketManager.RetrieveTicketsByDate(date.ToShortDateString());
                    refreshTickets(tickets);
                    btnSortEndDate.DisplayDateStart = date;
                    btnSortEndDate.IsEnabled = true;
                }
                catch (Exception up)
                {
                    throw up;
                }
            }
            else if (btnSortStartDate.SelectedDate != null && btnSortEndDate.IsEnabled && btnSortEndDate.SelectedDate != null)
            {
                try
                {
                    DateTime sdate = (DateTime)btnSortStartDate.SelectedDate;
                    DateTime edate = (DateTime)btnSortEndDate.SelectedDate;

                    List<TicketVM> tickets = _masterManager.TicketManager.RetrieveTicketsByDate(sdate.ToShortDateString(), edate.ToShortDateString());
                    refreshTickets(tickets);

                    btnSortEndDate.DisplayDateStart = sdate;
                }
                catch (Exception up)
                {
                    throw up;
                }
            }
        }

        /// <summary>
        /// Mads Rhea
        /// Created: 2023/04/20
        /// 
        /// Clears other filters and updates ticket list
        /// based on startdate and enddate given
        /// 
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/28
        /// 
        /// Final QA
        /// </remarks>
        private void btnSortEndDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            btnSortBy.Content = "Reset";
            cmboSortStatus.SelectedItem = null;
            cmboSortUser.SelectedItem = null;

            if (btnSortEndDate.SelectedDate != null)
            {
                try
                {
                    DateTime sdate = (DateTime)btnSortStartDate.SelectedDate;
                    DateTime edate = (DateTime)btnSortEndDate.SelectedDate;

                    List<TicketVM> tickets = _masterManager.TicketManager.RetrieveTicketsByDate(sdate.ToShortDateString(), edate.ToShortDateString());
                    refreshTickets(tickets);

                    btnSortEndDate.DisplayDateStart = sdate;
                }
                catch (Exception up)
                {
                    throw up;
                }
            }
        }

        /// <summary>
        /// Mads Rhea
        /// Created: 2023/04/20
        /// 
        /// Updates the "Sort By" buttons text
        /// when the mouse enters the element
        /// 
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/28
        /// 
        /// Final QA
        /// </remarks>
        private void btnSortBy_MouseEnter(object sender, MouseEventArgs e)
        {
            if ((string)btnSortBy.Content == "Sort By:" && stkpnlSortBy.Visibility == Visibility.Visible)
            {
                btnSortBy.Content = "Close?";
            }
            else if ((string)btnSortBy.Content == "Sort By:" && stkpnlSortBy.Visibility == Visibility.Collapsed)
            {
                btnSortBy.Content = "Open?";
            }
        }

        /// <summary>
        /// Mads Rhea
        /// Created: 2023/04/20
        /// 
        /// Updates the "Sort By" buttons text 
        /// when the mouse leaves the element
        /// 
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/28
        /// 
        /// Final QA
        /// </remarks>
        private void btnSortBy_MouseLeave(object sender, MouseEventArgs e)
        {
            if ((string)btnSortBy.Content == "Close?" || (string)btnSortBy.Content == "Open?")
            {
                btnSortBy.Content = "Sort By:";
            }
        }
    }
    }

