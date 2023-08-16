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
using PetNetApp;

namespace WpfPresentation.Management
{
    /// <summary>
    /// Interaction logic for VolunteerManagment.xaml
    /// </summary>
    /// 
    /// <summary>
    /// Chris Dreismeier
    /// Created: 2023/02/03
    /// 
    /// 
    /// 
    /// </summary>
    /// Page for volunteermanagemnt view and edit volunteers
    /// 
    /// 
    /// <remarks>
    /// Teft Francisco
    /// Updated: 2023/2/24
    /// Added navigation methods to the "Edit Volunteer Information" button.
    /// </remarks>
    public partial class VolunteerManagment : Page
    {
        private List<UsersVM> _users = null;
        private MasterManager _mastermanager = MasterManager.GetMasterManager();
        public VolunteerManagment()
        {
            InitializeComponent();
            try
            {
                _users = _mastermanager.UsersManager.RetrieveUserByRole("Volunteer" ,_mastermanager.User.ShelterId.Value);
                datVolunteer.ItemsSource = _users;
            }
            catch (Exception ex)
            {
                PromptWindow.ShowPrompt("Error", ex.Message);
            }
            
        }

        private void txtboxsearch_KeyUp(object sender, KeyEventArgs e)
        {
            var filtered = _users.Where(user => user.GivenName.StartsWith(txtboxsearch.Text, StringComparison.CurrentCultureIgnoreCase));
            if (rdbPhone.IsChecked == true)
            {
                filtered = _users.Where(user => user.Phone.StartsWith(txtboxsearch.Text, StringComparison.CurrentCultureIgnoreCase));
            }
            if (rdbAddress.IsChecked == true)
            {
                filtered = _users.Where(user => user.Address.StartsWith(txtboxsearch.Text, StringComparison.CurrentCultureIgnoreCase));
            }


            datVolunteer.ItemsSource = filtered;
        }

        private void btnEditSchedule_Click(object sender, RoutedEventArgs e)
        {
            UsersVM user = (UsersVM)datVolunteer.SelectedItem;
            if(datVolunteer.SelectedItem != null)
            {
                NavigationService.Navigate(new SchedulePage(user));
                var page = ManagementPage.GetManagementPage(_mastermanager);
                page.ChangeSelectedButton(page.btnSchedule);
            }
            else
            {
                PromptWindow.ShowPrompt("No User Selected", "There is no user selected");
            }
            
        }

        private void btnEditVolunteer_Click(object sender, RoutedEventArgs e)
        {
            if (datVolunteer.SelectedItem != null)
            {
                NavigationService.Navigate(new VolunteerInfoPage((UsersVM)datVolunteer.SelectedItem));
            }
            else
            {
                PromptWindow.ShowPrompt("Error", "You must select a user to edit their information!");
            }
        }
    }
}
