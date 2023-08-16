/// <summary>
/// Andrew Schneider
/// Created: 2023/04/12
/// 
/// Interaction logic for ViewAllAdoptableAnimalsPage.xaml
/// Copied from AnimalListUserControl.xaml.cs
/// </summary>
///
/// <remarks>
/// Updater Name
/// Updated: yyyy/mm/dd
/// </remarks>
using System;
using LogicLayer;
using DataObjects;
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
    public partial class AdoptableAnimalListUserControl : UserControl
    {
        private AnimalVM _adoptableAnimal = null;
        private MasterManager _masterManager = MasterManager.GetMasterManager();

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/04/12
        /// 
        /// Public constructor for AdoptableAnimalListUserControl
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="adoptableAnimal">The animal for the user control</param>
        public AdoptableAnimalListUserControl(AnimalVM adoptableAnimal)
        {
            _adoptableAnimal = adoptableAnimal;
            InitializeComponent();
        }

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/04/12
        /// 
        /// Click event handler for the user control button. Navigates to the adoption
        /// profile of the animal whose user control has been clicked.
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnViewAdoptableAnimalProfile_Click(object sender, RoutedEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            nav.Navigate(new WpfPresentation.Animals.ViewAdoptableAnimalProfile(_adoptableAnimal.AnimalId));
        }

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/04/12
        /// 
        /// Click event handler for the user control animal image. Navigates to the
        /// adoption profile of the animal whose user control has been clicked.
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imgAdoptableAnimalDisplay_MouseDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            nav.Navigate(new WpfPresentation.Animals.ViewAdoptableAnimalProfile(_adoptableAnimal.AnimalId));
        }
    }
}
