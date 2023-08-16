using DataObjects;
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

namespace WpfPresentation.UserControls
{
    /// <summary>
    /// Interaction logic for InKindUserControl.xaml
    /// </summary>
    public partial class InKindUserControl : UserControl
    {
        public InKind InKind { get; set; }
        public InKindUserControl(InKind inKind)
        {
            InKind = inKind;
            InitializeComponent();
        }
    }
}
