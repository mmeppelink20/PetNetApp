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
using DataObjects;
using LogicLayer;
using WpfPresentation.UserControls;

namespace WpfPresentation.Fundraising
{
    /// <summary>
    /// Interaction logic for ViewSpecificPledger.xaml
    /// </summary>
    public partial class ViewSpecificPledger : Page
    {
        List<PledgeVM> _pledgeVMs = null;
        FundraisingEvent _fundraisingEvent = new FundraisingEvent();
        private int _userId;
        MasterManager _masterManager = MasterManager.GetMasterManager();

        public ViewSpecificPledger(FundraisingEvent fundraisingEvent, int userId)
        {
            InitializeComponent();
            _fundraisingEvent = fundraisingEvent;
            _userId = userId;
        }

        /// <summary>
        /// William Rients
        /// Created: 2023/04/04
        /// 
        /// When page loads, a list of pledgers is selected,
        /// if the pledgers userId is 0 (no account), a message
        /// will say the user doesn't have an account and
        /// return to the last page
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            loadPledger();
            if (_userId != 0)
            {
                lblHeader.Content = _pledgeVMs[0].GivenName + " " + _pledgeVMs[0].FamilyName + "'s" + " Pledge History";
                if (_pledgeVMs.Count == 0)
                {
                    stackPledgers.Visibility = Visibility.Collapsed;
                    nothingToShow.Visibility = Visibility.Visible;
                }
                else
                {
                    stackPledgers.Visibility = Visibility.Visible;
                    nothingToShow.Visibility = Visibility.Collapsed;
                }
                int i = 0;
                foreach (PledgeVM pledge in _pledgeVMs)
                {
                    ViewSpecificPledgerControl item = new ViewSpecificPledgerControl(pledge, _fundraisingEvent, i % 2 == 0);
                    i++;
                    stackPledgers.Children.Add(item);
                }
            }
            else
            {
                NavigationService.Navigate(new ViewFundraisingEventPledgers(_fundraisingEvent, _masterManager));
                PromptWindow.ShowPrompt("Error", "This Pledger does not have an account.");
            }
        }

        /// <summary>
        /// William Rients
        /// Created: 2023/04/04
        /// 
        /// If the initial list of pledgers is null,
        /// it will select a list of specific pledges 
        /// by userId
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        private void loadPledger()
        {
            if (_pledgeVMs == null)
            {
                try
                {
                    _pledgeVMs = _masterManager.PledgeManager.RetrieveSpecificPledgerByUserId(_userId);
                }
                catch (Exception ex)
                {
                    PromptWindow.ShowPrompt("Error", ex.Message);
                }
            }
        }

        /// <summary>
        /// William Rients
        /// Created: 2023/04/04
        /// 
        /// Button to go back to page for pledgers from
        /// a specific event
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ViewFundraisingEventPledgers(_fundraisingEvent, _masterManager));
        }
    }
}
