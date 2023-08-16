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
    /// Interaction logic for CreateNewTicket.xaml
    /// </summary>
    public partial class CreateNewTicket : Page
    {
        private MasterManager _masterManager = null;

        public CreateNewTicket(MasterManager manager)
        {
            InitializeComponent();
            _masterManager = manager;

        }

        /// <summary>
        /// William Rients
        /// Created: 2023/03/03
        /// 
        /// When button is clicked,
        /// returns to view all tickets page
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ViewTicketList(_masterManager));
        }

        /// <summary>
        /// William Rients
        /// Created: 2023/03/03
        /// 
        /// When button is clicked,
        /// validates text boxes and
        /// creates a new ticket and updates
        /// the view all tickets page
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (txtTicketReason.Text == "" || txtTicketReason.Text == null)
            {
                PromptWindow.ShowPrompt("Error", "Please fill out all fields");
                return;
            }
            if (txtTicketContent.Text == "" || txtTicketContent == null)
            {
                PromptWindow.ShowPrompt("Error", "Please fill out all fields");
                return;
            }
            if (txtTicketReason.Text.Length > 500 || txtTicketContent.Text.Length > 500)
            {
                PromptWindow.ShowPrompt("Error", "Ticket Reason and Ticket Context cannot be longer than 500 characters.");
                return;
            }

            try
            {
                Ticket ticket = new Ticket();

                ticket.TicketTitle = txtTicketReason.Text;
                ticket.TicketContext = txtTicketContent.Text;
                ticket.TicketStatusId = "Open";

                if (_masterManager.TicketManager.CreateNewTicket(_masterManager.User.UsersId, ticket.TicketStatusId, ticket.TicketTitle, ticket.TicketStatusId))
                {
                    PromptWindow.ShowPrompt("Success", "Ticket created!");
                    NavigationService.Navigate(new ViewTicketList(_masterManager));
                }
                else
                {
                    PromptWindow.ShowPrompt("Error", "Failed to create ticket.");
                    NavigationService.Navigate(new ViewTicketList(_masterManager));
                }

            }
            catch (Exception ex)
            {
                PromptWindow.ShowPrompt("Error creating a new ticker.", ex.Message);
            }
        }
    }
}
