/// <summary>
/// Nathan Zumsande
/// Created: 2023/03/08
/// 
/// Presentation layer methods for the Contact Page
/// </summary>
///
/// <remarks>
/// Updater Name
/// Updated: yyyy/mm/dd
/// example: Fixed a problem when user inputs bad data
/// </remarks>
using LogicLayer;
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

namespace WpfPresentation.Shelters
{
    /// <summary>
    /// Interaction logic for ContactPage.xaml
    /// </summary>
    public partial class ContactPage : Page
    {
        private MasterManager _masterManager;
        private Shelter _shelter;
        private List<UsersVM> _users = null;

        /// <summary>
        /// Nathan Zumsande
        /// Created: 2023/03/08
        /// 
        /// Empty constructor
        /// </summary>
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        public ContactPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Nathan Zumsande
        /// Created: 2023/03/08
        /// 
        /// Initalization of the Contact Page 
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="masterManager"></param>
        /// <param name="shelter"></param>
        public ContactPage(MasterManager masterManager, Shelter shelter)
        {
            _masterManager = masterManager;
            _shelter = shelter;
            InitializeComponent();
        }


        /// <summary>
        /// Nathan Zumsande
        /// Created: 2023/03/08
        /// Sets page when loaded
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Contact_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                _users = _masterManager.UsersManager.RetrieveUserByRole("Manager", _shelter.ShelterId);
                if(_users.Count != 0 && _users != null)
                {
                    datContact.ItemsSource = _users;
                }
                else
                {
                    datContact.Visibility = Visibility.Hidden;
                    lblGrid.Content = "No Managers found at selected shelter.";
                }
            }
            catch (Exception ex)
            {
                PromptWindow.ShowPrompt("Error", ex.Message);
            }
        }

        /// <summary>
        /// Nathan Zumsande
        /// Created: 2023/03/08
        /// Closes the Contact Page
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(null);
        }
    }
}
