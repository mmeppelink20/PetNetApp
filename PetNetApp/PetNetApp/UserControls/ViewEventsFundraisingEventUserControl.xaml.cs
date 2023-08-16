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
using WpfPresentation.Fundraising;
using LogicLayer;

namespace WpfPresentation.UserControls
{
    /// <summary>
    /// Interaction logic for ViewEventsFundraisingEventUserControl.xaml
    /// </summary>
    public partial class ViewEventsFundraisingEventUserControl : UserControl
    {
        public delegate void DeletedAction();
        public event DeletedAction EventDeleted;
        public static double TitleSectionWidth { get; set; } = 200;
        public static double StartDateSectionWidth { get; set; } = 150;
        public static double StartTimeSectionWidth { get; set; } = 150;
        public MasterManager _masterManager = MasterManager.GetMasterManager();
        public FundraisingEventVM FundraisingEvent { get; set; }
        public bool UseAlternateColors { get; set; }

        public ViewEventsFundraisingEventUserControl(FundraisingEventVM fundraisingEvent, bool useAlternateColors)
        {
            FundraisingEvent = fundraisingEvent;
            UseAlternateColors = useAlternateColors;
            InitializeComponent();
        }


        private void menuEdit_Click(object sender, RoutedEventArgs e)
        {
            //PromptWindow.ShowPrompt("Edit", "Editing " + FundraisingEvent.Title + " event for date: " + FundraisingEvent.StartTime);
            NavigationService.GetNavigationService(this).Navigate(new Events.EditFundraisingEvent(FundraisingEvent));
        }
        private void menuView_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GetNavigationService(this).Navigate(AddEditViewUpdateFundraisingEventPage.GetViewFundraisingEventPage(FundraisingEvent));

        }

        private void menuDelete_Click(object sender, RoutedEventArgs e)
        {
            if (PromptWindow.ShowPrompt("Delete", "Are you sure you want to delete " + FundraisingEvent.Title + " event for date: " + FundraisingEvent.StartTime, ButtonMode.DeleteCancel) == PromptSelection.Delete)
            {
                try
                {
                    if (_masterManager.FundraisingEventManager.DeactivateFundraisingEvent(FundraisingEvent.FundraisingEventId))
                    {
                        OnEventDeleted();
                    }
                }
                catch (Exception ex)
                {
                    PromptWindow.ShowPrompt("Error", ex.Message);
                }
            }
        }


        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            ((Button)sender).ContextMenu.IsOpen = true;
        }

        protected virtual void OnEventDeleted()
        {
            DeletedAction deletedAction = EventDeleted;
            deletedAction?.Invoke();
        }
        private void menuUpdate_Click(object sender, RoutedEventArgs e)
        {
           // PromptWindow.ShowPrompt("Update", "Updating " + FundraisingEvent.Title + " event for date: " + FundraisingEvent.StartTime);
            NavigationService.GetNavigationService(this).Navigate(AddEditViewUpdateFundraisingEventPage.GetUpdateFundraisingEventPage(FundraisingEvent));
        }

        private void menuAddPledge_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GetNavigationService(this).Navigate(new CreateNewPledge(FundraisingEvent.FundraisingEventId, _masterManager));
        }

        private void menuAddDonation_Click(object sender, RoutedEventArgs e)
        {
            // added by Asa Armstrong
            NavigationService.GetNavigationService(this).Navigate(new EnterDonation(FundraisingEvent.FundraisingEventId));
        }

        /// <summary>
        /// Oleksiy Fedchuk
        /// Created: 2023/04/20
        /// 
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/24
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuVolunteer_Click(object sender, RoutedEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            nav.Navigate(Events.VolunteerListPage.GetVolunteerListPage(FundraisingEvent.FundraisingEventId));
        }
    }
}
