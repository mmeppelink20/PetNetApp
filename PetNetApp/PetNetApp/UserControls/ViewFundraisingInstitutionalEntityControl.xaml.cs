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
    public partial class ViewFundraisingInstitutionalEntityControl : UserControl
    {
        public static double CompanyNameSectionWidth { get; set; } = 200;
        public static double GivenNameSectionWidth { get; set; } = 125;
        public static double FamilyNameSectionWidth { get; set; } = 125;
        public static double EmailSectionWidth { get; set; } = 125;
        public InstitutionalEntity InstitutionalEntity { get; set; }
        public bool UseAlternateColors { get; set; }


        public ViewFundraisingInstitutionalEntityControl(InstitutionalEntity institutionalEntity, bool useAlternateColors)
        {
            InstitutionalEntity = institutionalEntity;
            UseAlternateColors = useAlternateColors;
            InitializeComponent();
        }
        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            ((Button)sender).ContextMenu.IsOpen = true;
        }

        private void menuEdit_Click(object sender, RoutedEventArgs e)
        {
            string windowMode = "edit";
            AddEditInstitutionalEntity addEditInstitutionalEntity = new AddEditInstitutionalEntity(InstitutionalEntity, windowMode, InstitutionalEntity.ContactType);
            addEditInstitutionalEntity.Owner = Window.GetWindow(this);
            addEditInstitutionalEntity.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            addEditInstitutionalEntity.ShowDialog();
        }

        private void menuView_Click(object sender, RoutedEventArgs e)
        {
            string windowMode = "view";
            AddEditInstitutionalEntity addEditInstitutionalEntity = new AddEditInstitutionalEntity(InstitutionalEntity, windowMode, InstitutionalEntity.ContactType);
            addEditInstitutionalEntity.Owner = Window.GetWindow(this);
            addEditInstitutionalEntity.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            addEditInstitutionalEntity.ShowDialog();
        }

        private void Label_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            menuView_Click(sender, e);
        }

    }
}
