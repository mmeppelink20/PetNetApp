using DataObjects;
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
using WpfPresentation.Animals;
using WpfPresentation.Management;

namespace WpfPresentation.Management
{
    /// <summary>
    /// Interaction logic for AssignAnimalToKennel.xaml
    /// </summary>
    public partial class AssignAnimalToKennel : Page
    {
        MasterManager _masterManager = MasterManager.GetMasterManager();
        Kennel _kennel = new Kennel();

        public AssignAnimalToKennel(Kennel kennel)
        {
            InitializeComponent();
            _kennel = kennel;
        }

        /// <summary>
        /// William Rients
        /// Created: 2023/02/17
        /// 
        /// When page loads, kennel name is added to
        /// header of page and text box is disabled
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            lblKennelNumber.Content = _kennel.KennelName;
            lblKennelTitle.Content = "Add Animal to " + _kennel.KennelName;
            txtAnimalID.IsEnabled = false;
        }

        /// <summary>
        /// William Rients
        /// Created: 2023/02/17
        /// 
        /// When button is clicked, animal is assigned to
        /// specific kennel
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string animalId = txtAnimalID.Text;

            if (!animalId.All(char.IsDigit))
            {
                PromptWindow.ShowPrompt("Error", "Please select an Animal from the list.", ButtonMode.Ok);
                txtAnimalID.Focus();
                return;
            }
            if (animalId == "")
            {
                PromptWindow.ShowPrompt("Error", "Animal Id cannot be empty", ButtonMode.Ok);
                txtAnimalID.Focus();
                return;
            }

            try
            {
                if (_masterManager.KennelManager.AddAnimalIntoKennelByAnimalId(_kennel.KennelId, Int32.Parse(animalId)))
                {
                    PromptWindow.ShowPrompt("Success", "Animal added to kennel", ButtonMode.Ok);
                    NavigationService.Navigate(new ViewKennelPage());
                }
                else
                {
                    PromptWindow.ShowPrompt("Error", "Failed to insert animal into kennel.", ButtonMode.Ok);
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
        /// When clicked, a window opens with a data grid
        /// of animals that can be assigned to a kennel
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        private void btnViewAnimalList_Click(object sender, RoutedEventArgs e)
        {
            //open window to select an animal
            var animalListWindow = new ViewAnimalsForKennel(_kennel);
            animalListWindow.ShowDialog();

            //populate text box with animal object selected from list
            try
            {
                if (animalListWindow.SelectedAnimal != null)
                {
                    txtAnimalID.Text = animalListWindow.SelectedAnimal.AnimalId.ToString();
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
        /// Goes back to list of kennels page
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        private void btnGoBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ViewKennelPage());
        }

        /// <summary>
        /// William Rients
        /// Created: 2023/02/17
        /// 
        /// Goes back to list of kennels page
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            if (PromptWindow.ShowPrompt("Question", "Go back?", ButtonMode.YesNo) == PromptSelection.Yes)
            {
                NavigationService.Navigate(new ViewKennelPage());
            }
        }
    }
}
