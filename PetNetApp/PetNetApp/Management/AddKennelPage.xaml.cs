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

namespace WpfPresentation.Management
{
    /// <summary>
    /// Interaction logic for AddKennelPage.xaml
    /// </summary>
    public partial class AddKennelPage : Page
    {
        private MasterManager masterManager = MasterManager.GetMasterManager();

        /// <summary>
        /// Author: Gwen
        /// Date: 4/21/23
        /// </summary>
        public AddKennelPage()
        {
            InitializeComponent();
            try
            {
                var dropDownList = masterManager.KennelManager.RetrieveAnimalTypes();
                cbAnimalType.ItemsSource = dropDownList;
                
            }
            catch (Exception ex)
            {
                PromptWindow.ShowPrompt("Error", ex.Message, ButtonMode.Ok);
            }
        }

        /// <summary>
        /// Author: Gwen
        /// Date: 4/21/23
        /// </summary>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ViewKennelPage());
        }

        /// <summary>
        /// Author: Gwen
        /// Date: 4/21/23
        /// </summary>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInputs()) 
            {
                return;
            };

            Kennel kennel = new Kennel();

            kennel.ShelterId = masterManager.User.ShelterId.Value;
            kennel.KennelName = txtKennelName.Text;
            kennel.AnimalTypeId = cbAnimalType.SelectedItem.ToString();

            try
            {
                masterManager.KennelManager.AddKennel(kennel);
                NavigationService.Navigate(new ViewKennelPage());
            }
            catch (Exception ex)
            {
                PromptWindow.ShowPrompt("Error", ex.Message);
            }
        }

        /// <summary>
        /// Author: Gwen
        /// Date: 4/21/23
        /// </summary>
        private bool ValidateInputs()
        {
            if (txtKennelName.Text.Equals("") || cbAnimalType.SelectedItem == null)
            {
                PromptWindow.ShowPrompt("Error", "Please fill out all fields");
                return false;
            }
            if (txtKennelName.Text.Length > 50)
            {
                PromptWindow.ShowPrompt("Error", "Kennel Name can not be longer than 50 characters");
                return false;
            }
            return true;
        }
    }
}
