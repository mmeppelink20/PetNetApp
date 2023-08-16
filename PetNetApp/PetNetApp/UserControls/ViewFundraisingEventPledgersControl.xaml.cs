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

namespace WpfPresentation.UserControls
{
    /// <summary>
    /// Interaction logic for ViewFundraisingInstitutionalEntityControl.xaml
    /// </summary>
    public partial class ViewFundraisingEventPledgersControl : UserControl
    {
        public static double AmountWidth { get; set; } = 175;
        public static double DonationAmountWidth { get; set; } = 150;
        public static double GivenNameWidth { get; set; } = 150;
        public static double FamilyNameWidth { get; set; } = 150;
        public static double DateWidth { get; set; } = 150;
        public PledgeVM PledgeVM { get; set; }
        public FundraisingEvent FundraisingEvent { get; set; }
        public bool UseAlternateColors { get; set; }


        public ViewFundraisingEventPledgersControl(PledgeVM pledgeVM, FundraisingEvent fundraisingEvent, bool useAlternateColors)
        {
            PledgeVM = pledgeVM;
            FundraisingEvent = fundraisingEvent;
            UseAlternateColors = useAlternateColors;
            InitializeComponent();
        }

        private void btnView_Click(object sender, RoutedEventArgs e)
        {
            ViewSpecificPledger viewSpecificPledger = new ViewSpecificPledger(FundraisingEvent,PledgeVM.UserId);
            NavigationService.GetNavigationService(this).Navigate(viewSpecificPledger);
        }
    }
}
