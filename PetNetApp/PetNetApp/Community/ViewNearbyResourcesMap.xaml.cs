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

namespace WpfPresentation.Community
{
    /// <summary>
    /// Interaction logic for ViewNearbyResourcesMap.xaml
    /// </summary>
    public partial class Resources : Page
    {
        public Resources()
        {
            InitializeComponent();

        }

        private void kennelsMouseDown(object sender, MouseButtonEventArgs e)
        {

            if (KennelPopup.IsOpen == true)
            {
                KennelPopup.IsOpen = false;
            }
            else
            {
                KennelPopup.IsOpen = true;
            }

        }

        private void vetsMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (VetPopup.IsOpen == true)
            {
                VetPopup.IsOpen = false;
            }
            else
            {
                VetPopup.IsOpen = true;
            }
        }

        private void shelterMouseDown(object sender, MouseButtonEventArgs e)
        {
            {
                if (ShelterPopup.IsOpen == true)
                {
                    ShelterPopup.IsOpen = false;
                }
                else
                {
                    ShelterPopup.IsOpen = true;
                }
            }
        }


        private void vetsChecked(object sender, RoutedEventArgs e)
        {
            vetsMD.Visibility = Visibility.Visible;
        }

        private void kennelsChecked(object sender, RoutedEventArgs e)
        {
            kennelsMD.Visibility = Visibility.Visible;
        }

        private void sheltersChecked(object sender, RoutedEventArgs e)
        {
            shelterMD.Visibility = Visibility.Visible;

        }

        private void vetsUnchecked(object sender, RoutedEventArgs e)
        {
            vetsMD.Visibility = Visibility.Hidden;
        }

        private void kennelsUnchecked(object sender, RoutedEventArgs e)
        {
            kennelsMD.Visibility = Visibility.Hidden;
        }

        private void sheltersUnchecked(object sender, RoutedEventArgs e)
        {
            shelterMD.Visibility = Visibility.Hidden;
        }
    }
}