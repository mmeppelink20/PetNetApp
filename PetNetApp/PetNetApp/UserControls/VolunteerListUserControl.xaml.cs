/// <summary>
/// Oleksiy Fedchuk
/// Created: 2023/02/23
/// 
/// These are the small rows that populate the stack pannel on VolunteerListPage
/// </summary>
/// <remarks>
/// Zaid Rachman
/// Updated: 2023/04/24
/// 
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
using DataObjects;
using LogicLayer;

namespace WpfPresentation.UserControls
{
    /// <summary>
    /// Interaction logic for VolunteerListUserControl.xaml
    /// </summary>
    public partial class VolunteerListUserControl : UserControl
    {
        public static double TitleSectionWidth { get; set; } = 200;
        public VolunteerVM VolunteerVM { get; set; }
        public bool UseAlternateColors { get; set; }
        private MasterManager _masterManager = MasterManager.GetMasterManager();
        /// <summary>
        /// Oleksiy Fedchuk
        /// Created: 2023/02/23
        /// 
        /// Sets the row to be a volunteer and alternates the colors
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/24
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="volunteer"></param>
        /// <param name="useAlternateColors"></param>
        public VolunteerListUserControl(VolunteerVM volunteer, bool useAlternateColors)
        {
            VolunteerVM = volunteer;
            UseAlternateColors = useAlternateColors;
            InitializeComponent();
        }
        /// <summary>
        /// Oleksiy Fedchuk
        /// Created: 2023/02/23
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
        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            ((Button)sender).ContextMenu.IsOpen = true;
        }
        /// <summary>
        /// Oleksiy Fedchuk
        /// Created: 2023/02/23
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
        private void menuView_Click(object sender, RoutedEventArgs e)
        {
            PromptWindow.ShowPrompt("View", "Viewing " + VolunteerVM.GivenName + " " + VolunteerVM.FamilyName);
        }
        /// <summary>
        /// Oleksiy Fedchuk
        /// Created: 2023/02/23
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
        private void menuRemove_Click(object sender, RoutedEventArgs e)
        {
            var result = PromptWindow.ShowPrompt("Remove", "Are you sure you want to remove " + VolunteerVM.GivenName + " " + VolunteerVM.FamilyName + "?", ButtonMode.DeleteCancel);
            if(result == PromptSelection.Delete)
            {
                var tempFundraisingEventId = VolunteerVM.FundraisingEventId;
                try
                {
                    _masterManager.VolunteerManager.RemoveVolunteerFromEventbyUsersIdAndFundraisingEventId(VolunteerVM.UsersId, VolunteerVM.FundraisingEventId);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                Events.VolunteerListPage.GetVolunteerListPage(tempFundraisingEventId);
            }
        }
    }
}
