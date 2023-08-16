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
using WpfPresentation.Community.UsersControl;
using LogicLayer;
using DataObjects;
using WpfPresentation.Community;

namespace WpfPresentation.Community
{
    /// <summary>
    /// Interaction logic for CustomerRecordsPage.xaml
    /// </summary>
    /// /// <summary>
    /// Teft Francisco
    /// Created: 2023/02/14
    /// 
    /// 
    /// 
    /// </summary>
    /// Page for adopter info.
    public partial class CustomerRecordsPage : Page
    {

        private Users _user = null;
        private MasterManager _mastermanager = MasterManager.GetMasterManager();

        public CustomerRecordsPage(Users users)
        {
            _user = users;
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ReloadUI();
        }

        private void ReloadUI()
        {
            lblRecordTitle.Content = "Records for adopter: " + _user.GivenName + " " + _user.FamilyName;

            // If you want to add more fields please make sure they reload properly here.

            txtFirstName.Text = _user.GivenName;
            txtLastName.Text = _user.FamilyName;
            txtEmail.Text = _user.Email;
            txtPhone.Text = _user.Phone;
            txtAddress.Text = _user.Address;

            if (datAdoptionList.Items.Count > 0)
            {
                noPreviousRecords.Visibility = Visibility.Hidden;
                datAdoptionList.Visibility = Visibility.Visible;
                datAdoptionList.IsEnabled = true;
            }
            else
            {
                noPreviousRecords.Visibility = Visibility.Visible;
                datAdoptionList.Visibility = Visibility.Hidden;
                datAdoptionList.IsEnabled = false;
            }
        }

        private void btnBackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new UserManagementPage());
        }

        private void datAdoptionList_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                datAdoptionList.ItemsSource = _mastermanager.UsersManager.RetrieveAdoptionRecordsByUserID(_user.UsersId);
                ReloadUI();
                if (datAdoptionList.Columns.Count != 0)
                {
                    datAdoptionList.Columns[0].Header = "Animal Name";
                    datAdoptionList.Columns[1].Header = "Animal Species";
                    datAdoptionList.Columns[2].Header = "Animal Breed";
                    datAdoptionList.Columns[3].Header = "Old Animal ID";
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("An error has occured.", ex.Message, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
