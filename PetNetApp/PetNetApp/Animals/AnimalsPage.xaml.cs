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
using WpfPresentation.Animals.Medical;

namespace WpfPresentation.Animals
{
    /// <summary>
    /// Interaction logic for AnimalsPage.xaml
    /// </summary>
    public partial class AnimalsPage : Page
    {
        private static AnimalsPage _existingAnimalsPage = null;

        private MasterManager _manager = MasterManager.GetMasterManager();
        private Button[] _animalsTabButtons;

        /// <summary>
        /// Author: Stephen Jaurigue
        /// Date: 2023/04/21
        /// </summary>
        static AnimalsPage()
        {
            MasterManager manager = MasterManager.GetMasterManager();
            manager.UserLogin += () => _existingAnimalsPage?.ShowButtonsByRole();
            manager.UserLogout += () =>
            {
                _existingAnimalsPage?.HideAllButtons();
                _existingAnimalsPage?.frameAnimals.Navigate(null);
            };
        }
        /// <summary>
        /// Author: Stephen Jaurigue
        /// Date: 2023/04/21
        /// </summary>
        private AnimalsPage()
        {
            InitializeComponent();
            _animalsTabButtons = new Button[] { btnAdopt, btnSurrender, btnAnimalList, btnMedical };
        }
        /// <summary>
        /// Author: Stephen Jaurigue
        /// Date: 2023/04/21
        /// </summary>
        /// <returns></returns>
        public static AnimalsPage GetAnimalsPage()
        {
            if (_existingAnimalsPage == null)
            {
                _existingAnimalsPage = new AnimalsPage();
                _existingAnimalsPage.ShowButtonsByRole();
            }
            return _existingAnimalsPage;
        }
        /// <summary>
        /// Author: Stephen Jaurigue
        /// Date: 2023/04/21
        /// </summary>
        /// <param name="selectedButton"></param>
        public void ChangeSelectedButton(Button selectedButton)
        {
            UnselectAllButtons();
            selectedButton.Style = (Style)Application.Current.Resources["rsrcSelectedButton"];
        }
        /// <summary>
        /// Author: Stephen Jaurigue
        /// Date: 2023/04/21
        /// </summary>
        private void UnselectAllButtons()
        {
            foreach (Button button in _animalsTabButtons)
            {
                button.Style = (Style)Application.Current.Resources["rsrcUnselectedButton"];
            }
        }

        private void btnAdopt_Click(object sender, RoutedEventArgs e)
        {
            ChangeSelectedButton(btnAdopt);
            // replace with page name and then delete comment
            frameAnimals.Navigate(WpfPresentation.Animals.ViewAllAdoptableAnimalsPage.GetViewAllAdoptableAnimalsPage());

            
        }
        /// <summary>
        /// Author: Stephen Jaurigue
        /// Date: 2023/04/21
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSurrender_Click(object sender, RoutedEventArgs e)
        {
            ChangeSelectedButton(btnSurrender);
            // replace with page name and then delete comment
            frameAnimals.Navigate(new SurrenderFormsPage()); 

        }
        /// <summary>
        /// Author: Stephen Jaurigue
        /// Date: 2023/04/21
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAnimalList_Click(object sender, RoutedEventArgs e)
        {
            ChangeSelectedButton(btnAnimalList);
            // replace with page name and then delete comment
            frameAnimals.Navigate(new AnimalListPage(_manager));
        }
        /// <summary>
        /// Author: Stephen Jaurigue
        /// Date: 2023/04/21
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMedical_Click(object sender, RoutedEventArgs e)
        {
            ChangeSelectedButton(btnMedical);
            frameAnimals.Navigate(MedicalPage.GetMedicalPage(_manager));
        }
        /// <summary>
        /// Author: Stephen Jaurigue
        /// Date: 2023/04/21
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scrollviewer = sender as ScrollViewer;
            if (e.Delta > 0)
            {
                scrollviewer.LineLeft();
            }
            else
            {
                scrollviewer.LineRight();
            }
            e.Handled = true;
        }
        /// <summary>
        /// Author: Stephen Jaurigue
        /// Date: 2023/04/21
        /// </summary>
        private void UpdateScrollButtons()
        {
            if (svAnimalTabs.HorizontalOffset > svAnimalTabs.ScrollableWidth - 0.05)
            {
                btnScrollRight.Visibility = Visibility.Hidden;
            }
            else
            {
                btnScrollRight.Visibility = Visibility.Visible;
            }

            if (svAnimalTabs.HorizontalOffset < 0.05)
            {
                btnScrollLeft.Visibility = Visibility.Hidden;
            }
            else
            {
                btnScrollLeft.Visibility = Visibility.Visible;
            }
        }
        /// <summary>
        /// Author: Stephen Jaurigue
        /// Date: 2023/04/21
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void svAnimalTabs_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            UpdateScrollButtons();
        }
        /// <summary>
        /// Author: Stephen Jaurigue
        /// Date: 2023/04/21
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnScrollRight_Click(object sender, RoutedEventArgs e)
        {
            svAnimalTabs.ScrollToHorizontalOffset(svAnimalTabs.HorizontalOffset + 130);
        }
        /// <summary>
        /// Author: Stephen Jaurigue
        /// Date: 2023/04/21
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnScrollLeft_Click(object sender, RoutedEventArgs e)
        {
            svAnimalTabs.ScrollToHorizontalOffset(svAnimalTabs.HorizontalOffset - 130);
        }
        /// <summary>
        /// Author: Stephen Jaurigue
        /// Date: 2023/04/21
        /// </summary>
        public void HideAllButtons()
        {
            UnselectAllButtons();
            foreach (Button btn in _animalsTabButtons)
            {
                btn.Visibility = Visibility.Collapsed;
            }
        }
        /// <summary>
        /// Author: Stephen Jaurigue
        /// Date: 2023/04/21
        /// </summary>
        public void ShowButtonsByRole()
        {
            HideAllButtons();
            ShowAdoptButtonByRole();
            ShowAnimalListButtonByRole();
            ShowMedicalButtonByRole();
            ShowSurrenderButtonByRole();
        }
        /// <summary>
        /// Author: Stephen Jaurigue
        /// Date: 2023/04/21
        /// </summary>
        public void ShowAdoptButtonByRole()
        {
            string[] allowedRoles = { "Admin", "Manager", "Employee" };
            if (_manager.User.Roles.Exists(role => allowedRoles.Contains(role)))
            {
                btnAdopt.Visibility = Visibility.Visible;
            }
        }
        /// <summary>
        /// Author: Stephen Jaurigue
        /// Date: 2023/04/21
        /// </summary>
        public void ShowAnimalListButtonByRole()
        {
            string[] allowedRoles = { "Admin", "Manager", "Vet","Employee" };
            if (_manager.User.Roles.Exists(role => allowedRoles.Contains(role)))
            {
                btnAnimalList.Visibility = Visibility.Visible;
            }
        }
        /// <summary>
        /// Author: Stephen Jaurigue
        /// Date: 2023/04/21
        /// </summary>
        public void ShowMedicalButtonByRole()
        {
            string[] allowedRoles = { "Admin", "Manager","Vet" };
            if (_manager.User.Roles.Exists(role => allowedRoles.Contains(role)))
            {
                btnMedical.Visibility = Visibility.Visible;
            }
        }
        /// <summary>
        /// Author: Stephen Jaurigue
        /// Date: 2023/04/21
        /// </summary>
        public void ShowSurrenderButtonByRole()
        {
            string[] allowedRoles = { "Admin", "Manager" , "Employee" };
            if (_manager.User.Roles.Exists(role => allowedRoles.Contains(role)))
            {
                btnSurrender.Visibility = Visibility.Visible;
            }
        }
    }
}
