using LogicLayer;
using PetNetApp;
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

namespace WpfPresentation.Misc
{
    /// <summary>
    /// Mads Rhea
    /// Created: 2023/02/24
    /// 
    /// WPF for the body section of the "Landing Page" (user logged in)
    /// </summary>
    ///
    /// <remarks>
    /// Updater Name
    /// Updated: yyyy/mm/dd
    /// </remarks>
    public partial class LandingBodyLoggedInPage : Page
    {
        private MainWindow _mainWindow = null;
        private static LandingBodyLoggedInPage _existingLandingBodyLoggedIn = null;
        private MasterManager _manager = MasterManager.GetMasterManager();

        public LandingBodyLoggedInPage()
        {
            InitializeComponent();
        }

        public static LandingBodyLoggedInPage GetLandingBodyLoggedInPage(MainWindow mainWindow)
        {
            if (_existingLandingBodyLoggedIn == null)
            {
                _existingLandingBodyLoggedIn = new LandingBodyLoggedInPage();
            }

            _existingLandingBodyLoggedIn._mainWindow = mainWindow;

            return _existingLandingBodyLoggedIn;
        }

        private void btnLogIn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(LogInPage.GetLogInPage());
        }

        private void btnSignUp_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(SignUp.GetSignUpPage());
        }
    }
}
