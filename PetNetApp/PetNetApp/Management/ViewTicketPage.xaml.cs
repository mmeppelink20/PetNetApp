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

namespace WpfPresentation.Management
{
    /// <summary>
    /// Interaction logic for ViewTicketPage.xaml
    /// </summary>
    public partial class ViewTicketPage : Page
    {
        private MasterManager _masterManager = MasterManager.GetMasterManager();
        private TicketVM _ticketVM = null;
        private TicketVM _oldTicket = null;
        private ViewTicketList _viewTicketList = null;

        public ViewTicketPage(TicketVM ticketVM, MasterManager manager, ViewTicketList viewTicketList)
        {
            _ticketVM = ticketVM;
            _oldTicket = new TicketVM();
            _oldTicket.TicketId = _ticketVM.TicketId;
            _oldTicket.TicketStatusId = _ticketVM.TicketStatusId;
            _masterManager = manager;
            _viewTicketList = viewTicketList;
            InitializeComponent();

        }

        /// <summary>
        /// Matthew Meppelink
        /// Created: 2023/03/16
        /// 
        /// closes the view ticket menu when clicked
        /// 
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        private void btnCancel(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(null);
        }

        /// <summary>
        /// Matthew Meppelink
        /// Created: 2023/03/16
        /// 
        /// Creates the view ticket page when
        /// the view ticket button is pressed
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            lblTicketNumber.Content = "Ticket: " + _ticketVM.TicketId;
            txtTicketTitle.Text = _ticketVM.TicketTitle;
            txtTicketContext.Text = _ticketVM.TicketContext;
            txtTicketDate.Text = _ticketVM.TicketDate.ToString();
            txtTicketType.Text = _ticketVM.TicketStatusId;
            txtTicketPoster.Text = _ticketVM.Email;
        }

        /// <summary>
        /// Matthew Meppelink
        /// Created: 2023/03/16
        /// 
        /// closes the view ticket page when
        /// clicked, navigates back to the viewticketlist page
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(null);
        }

        /// <summary>
        /// Matthew Meppelink
        /// Created: 2023/03/16
        /// 
        /// page closes when pressing "escape"
        /// 
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        private void Page_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                NavigationService.Navigate(null);
            }
        }

        /// <summary>
        /// Matthew Meppelink
        /// Created: 2023/03/23
        /// 
        /// Updates a ticket's status to closed or open
        /// 
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        private void btnResolve_Click(object sender, RoutedEventArgs e)
        {
            string ticketStatus = _ticketVM.TicketStatusId == "Closed" ? "open" : "close";
            PromptSelection selection = (PromptWindow.ShowPrompt("Resolve?", "Do you want to " + ticketStatus + " this ticket?", ButtonMode.YesNo));
            if (selection == PromptSelection.Yes)
            {
                _ticketVM.TicketStatusId = _ticketVM.TicketStatusId == "Closed" ? "Open" : "Closed";
                try
                {
                    _masterManager.TicketManager.EditTicketStatus(_ticketVM, _oldTicket);

                }
                catch (Exception ex)
                {
                    PromptWindow.ShowPrompt("Error!", "An error occured while processing your request" + ex.InnerException.Message, ButtonMode.Ok);
                }
                _viewTicketList.refreshTickets();
                NavigationService.Navigate(null);
                PromptWindow.ShowPrompt("Success!", "Ticket " + _ticketVM.TicketId + "'s status was succesfully updated to " + _ticketVM.TicketStatusId, ButtonMode.Ok);
            }

        }

        /// <summary>
        /// Matthew Meppelink
        /// Created: 2023/03/23
        /// 
        /// Exits the current screen, sends user back to view ticket list page
        /// 
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(null);
        }
    }
}
