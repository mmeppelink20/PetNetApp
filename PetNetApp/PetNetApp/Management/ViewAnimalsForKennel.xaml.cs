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
using DataObjects;
using LogicLayer;

namespace WpfPresentation.Management
{
    /// <summary>
    /// Interaction logic for ViewAnimalsForKennel.xaml
    /// </summary>
    public partial class ViewAnimalsForKennel : Window
    {
        private List<Animal> _animals = null;
        private Kennel _Kennel = null;
        MasterManager _masterManger = MasterManager.GetMasterManager();
        public Animal SelectedAnimal { get; set; }

        public ViewAnimalsForKennel(Kennel kennel)
        {
            InitializeComponent();
            _Kennel = kennel;
        }

        /// <summary>
        /// William Rients
        /// Created: 2023/02/17
        /// 
        /// When window loads, a data grid is
        /// populated with animals available for kenneling
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                _animals = _masterManger.KennelManager.RetrieveAllAnimalsForKennel(_Kennel.ShelterId);
                if (_animals.Count > 0)
                {
                    datAnimals.ItemsSource = _animals;
                } 
                else
                {
                    PromptWindow.ShowPrompt("Error", "No animals avaliable for kenneling.", ButtonMode.Ok);
                    this.Close();
                }
                
            }
            catch (Exception ex)
            {
                PromptWindow.ShowPrompt("Error", ex.Message + "\n\n" + ex.InnerException.Message, ButtonMode.Ok);
            }
                
        }

        /// <summary>
        /// William Rients
        /// Created: 2023/02/17
        /// 
        /// When double clicking a specific row,
        /// that animal is selected to be assigned 
        /// to a kennel
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        private void datAnimals_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SelectedAnimal = (Animal)(datAnimals.SelectedItem);
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
