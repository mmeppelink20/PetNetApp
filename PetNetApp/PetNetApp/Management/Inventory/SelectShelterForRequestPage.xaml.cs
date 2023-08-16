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
using LogicLayer;
using DataObjects;

namespace WpfPresentation.Management.Inventory
{
    /// <summary>
    /// Andrew Cromwell
    /// 2023/04/05
    /// 
    /// Interaction logic for SelectShelterForRequestPage.xaml
    /// </summary>
    public partial class SelectShelterForRequestPage : Page
    {
        private static SelectShelterForRequestPage _existingSelectShelterForRequestPage;
        private MasterManager _manager = null;
        private List<Shelter> _shelters = new List<Shelter>();

        public SelectShelterForRequestPage(MasterManager manager)
        {
            InitializeComponent();
            _manager = manager;
            try
            {
                _shelters = _manager.ShelterManager.GetShelterList();                
            }
            catch (Exception up)
            {
                PromptWindow.ShowPrompt("Error", up.Message + "\n\n" + up.InnerException);
                return;
            }
        }

        public static SelectShelterForRequestPage GetSelectShelterForRequestPage(MasterManager manager)
        {
            if (_existingSelectShelterForRequestPage == null)
            {
                _existingSelectShelterForRequestPage = new SelectShelterForRequestPage(manager);
            }
            return _existingSelectShelterForRequestPage;
        }

        /// <summary>
        /// Andrew Cromwell
        /// Created: 2023/04/06
        /// 
        /// Creates buttons so that the user can select any shelter except their own.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblSelectShelter_Loaded(object sender, RoutedEventArgs e)
        {
            wrpShelters.Children.Clear();
            foreach(Shelter shelter in _shelters)
            {
                if(shelter.ShelterId != _manager.User.ShelterId.Value)
                {
                    Grid grid = new Grid();
                    grid.Width = 250;
                    grid.Height = 120;

                    Border border = new Border();
                    border.Margin = new Thickness(22, 22, 0, 0);
                    border.CornerRadius = new CornerRadius(10);
                    border.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF9EC1B0");

                    Button button = new Button();
                    button.VerticalAlignment = VerticalAlignment.Bottom;
                    button.Margin = new Thickness(48, 42, 25, 25);
                    button.Height = 50;
                                        
                    grid.Children.Add(border);                    
                    grid.Children.Add(button);

                    button.Content = shelter.ShelterName;
                                        
                    button.Click += (s, ev) =>
                    {
                        NavigationService.Navigate(new CreateShelterRequestPage(_manager, shelter));
                    };

                    wrpShelters.Children.Add(grid);
                }
            }
        }
    }
}
