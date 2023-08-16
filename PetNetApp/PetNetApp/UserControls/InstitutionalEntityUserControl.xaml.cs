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
    /// Interaction logic for InstitutionalEntityUserControl.xaml
    /// </summary>
    public partial class InstitutionalEntityUserControl : UserControl
    {
        public InstitutionalEntity InstitutionalEntity { get; set; }
        public InstitutionalEntityUserControl(InstitutionalEntity institutionalEntity, bool Editable, bool addMode)
        {
            DataContext = this;
            InstitutionalEntity = institutionalEntity;
            InitializeComponent();
            if (!Editable)
            {
                btnRemove.Visibility = Visibility.Collapsed;
                btnAdd.Visibility = Visibility.Collapsed;
            }
            else if (addMode)
            {
                btnRemove.Visibility = Visibility.Collapsed;
            }
            else
            {
                btnAdd.Visibility = Visibility.Collapsed;
            }
        }
    }
}
