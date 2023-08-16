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

namespace WpfPresentation.UserControls
{
    /// <summary>
    /// Interaction logic for ViewSpecificPledgerControl.xaml
    /// </summary>
    public partial class ViewSpecificPledgerControl : UserControl
    {
        public static double TitleWidth { get; set; } = 250;
        public static double AmountWidth { get; set; } = 250;
        public static double DateWidth { get; set; } = 250;
        public static double DonationAmountWidth { get; set; } = 250;
        public static double DonationDateWidth { get; set; } = 250;
        public PledgeVM PledgeVM { get; set; }
        public FundraisingEvent FundraisingEvent { get; set; }
        public bool UseAlternateColors { get; set; }

        public ViewSpecificPledgerControl(PledgeVM pledgeVM, FundraisingEvent fundraisingEvent, bool useAlternateColors)
        {
            FundraisingEvent = fundraisingEvent;
            PledgeVM = pledgeVM;
            UseAlternateColors = useAlternateColors;
            InitializeComponent();
        }
    }
}
