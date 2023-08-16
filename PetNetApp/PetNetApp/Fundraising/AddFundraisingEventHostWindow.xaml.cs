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
using System.Windows.Shapes;
using LogicLayer;
using DataObjects;

namespace WpfPresentation.Fundraising
{
    /// <summary>
    /// Interaction logic for AddFundraisingEventHostWindow.xaml
    /// </summary>
    public partial class AddFundraisingEventHostWindow : Window
    {
        private InstitutionalEntity _selectedEventHost = null;
        private MasterManager _masterManager = MasterManager.GetMasterManager();
        private List<InstitutionalEntity> _allHosts = null;
        private string _currentSearch = "";

        public AddFundraisingEventHostWindow(InstitutionalEntity selectedEventHost)
        {
            DataContext = this;
            _selectedEventHost = selectedEventHost;
            try
            {
                _allHosts = _masterManager.InstitutionalEntityManager.RetrieveAllInstitutionalEntitiesByShelterIdAndEntityType((int)_masterManager.User.ShelterId,"Host");
            }
            catch (Exception ex)
            {
                PromptWindow.ShowPrompt("Error", ex.Message);
                _allHosts = new List<InstitutionalEntity>();
            }
            InitializeComponent();
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ClearAndPopulateAddInstitutionalEntities();
        }

        private void ClearAndPopulateAddInstitutionalEntities()
        {
            datHost.ItemsSource = null;
            
            if(_allHosts != null)
            {
                datHost.ItemsSource = _allHosts;
            }
          
        }

        private void ptbSearchText_KeyDown(object sender, KeyEventArgs e)
        {
            var filtered = _allHosts.Where(host => host.GivenName.StartsWith(ptbSearchText.Text, StringComparison.CurrentCultureIgnoreCase));

            datHost.ItemsSource = filtered;
        }

        private void btnDone_Click(object sender, RoutedEventArgs e)
        {
            _selectedEventHost = (InstitutionalEntity)datHost.SelectedItem;
            
            if (_selectedEventHost != null)
            {
                this.Close();
            }
            else
            {
                PromptWindow.ShowPrompt("Error", "Please select a host");
            }
        }
    }
}
