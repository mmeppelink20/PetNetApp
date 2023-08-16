/// <summary>
/// Mads Rhea
/// Created: 2023/02/05
/// 
/// </summary>
/// <remarks>
/// Oleksiy Fedchuk
/// Updated: 2023/04/28
/// 
/// Final QA
/// </remarks>
using DataObjects;
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
    /// Alex Oetken
    /// Created: 2023/02/05
    /// 
    /// WPF for deactivate button page within "Account Settings"
    /// </summary>
    ///
    /// <remarks>
    /// Oleksiy Fedchuk
    /// Updated: 2023/04/28
    /// 
    /// Final QA
    /// </remarks>
    public partial class DeactivateButtonPage : Page
    {
        private static DeactivateButtonPage _existingDeactivateButton = null;
        private MasterManager _manager = MasterManager.GetMasterManager();
        private bool isAvaliable = false;
        private int animalsFostering = 0;
        private int animalsCanFoster = 0;
        /// <summary>
        /// Mads Rhea
        /// Created: 2023/02/05
        /// 
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/28
        /// 
        /// Final QA
        /// </remarks>
        public DeactivateButtonPage()
        {
            InitializeComponent();
            try
            {
                isAvaliable = _manager.FosterManager.RetrieveCurrentlyAcceptingAnimalsByUsersId(_manager.User.UsersId);
                animalsFostering = _manager.FosterManager.RetrieveNumberOfAnimalsFostererHasByUsersId(_manager.User.UsersId);
                animalsCanFoster = _manager.FosterManager.RetrieveNumberOfAnimalsApprovedByUsersId(_manager.User.UsersId);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        /// <summary>
        /// Mads Rhea
        /// Created: 2023/02/05
        /// 
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/28
        /// 
        /// Final QA
        /// </remarks>
        /// <returns></returns>
        public static DeactivateButtonPage GetDeactivateButtonPage()
        {
            if (_existingDeactivateButton == null)
            {
                _existingDeactivateButton = new DeactivateButtonPage();
            }

            return _existingDeactivateButton;
        }
        /// <summary>
        /// Mads Rhea
        /// Created: 2023/02/05
        /// 
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/28
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeactivateButton_Click(object sender, RoutedEventArgs e)
        {
            var result = PromptWindow.ShowPrompt("Deactivate Account", "Are you sure? This action cannot be undone.", ButtonMode.YesNo);

            if (result == PromptSelection.Yes)
            {
                try
                {
                    if (_manager.UsersManager.DeactivateUserAccount(_manager.User.UsersId))
                    {
                        PromptWindow.ShowPrompt("Success", "Account has been deactivated!");
                        _manager.User = null;
                    }
                }
                catch (Exception ex)
                {
                    PromptWindow.ShowPrompt("Error", ex.Message);

                }
            }
        }
        /// <summary>
        /// Mads Rhea
        /// Created: 2023/02/05
        /// 
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/28
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            txtblkDeactivateWarning.Text = "WARNING: Clicking the button below will deactivate your account!\nThe only way you can get your account back is having an admin reactivate it for you.\n\nBE SURE THIS IS WHAT YOU WANT TO DO BEFORE PROCEEDING.";
            RoleToggle();
            
        }
        /// <summary>
        /// Mads Rhea
        /// Created: 2023/02/05
        /// 
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/28
        /// 
        /// Final QA
        /// </remarks>
        private void RoleToggle()
        {

            List<string> userRoles = new List<string>();
            try
            {
                userRoles = _manager.UsersManager.RetrieveRolesByUsersId(_manager.User.UsersId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            foreach (string role in userRoles)
            {
                if (role == "Fosterer")
                {
                    ShowFosterToggle();
                }
            }
        }
        /// <summary>
        /// Mads Rhea
        /// Created: 2023/02/05
        /// 
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/28
        /// 
        /// Final QA
        /// </remarks>
        private void ShowFosterToggle()
        {
            lblNoOfAnimals.Content = animalsFostering + " Fostering / " + animalsCanFoster + " Max Number";

            ToggleFosterButtonText();

            gridSettings.RowDefinitions[1].Height = new GridLength(50);
            if (animalsFostering >= animalsCanFoster) 
            {
                gridSettings.RowDefinitions[2].Height = new GridLength(58);
                btnToggleFoster.IsEnabled = false;
                lblToggleFosterText1.Visibility = Visibility.Visible;
                lblToggleFosterText2.Visibility = Visibility.Visible;
                rectDecor2.Visibility = Visibility.Visible;
            }
            gridSettings.RowDefinitions[3].Height = new GridLength(100);
            gridSettings.RowDefinitions[4].Height = new GridLength(25);

            lblToggleFosterHeader.Visibility = Visibility.Visible;
            rectDecor.Visibility = Visibility.Visible;
            rectDecor1.Visibility = Visibility.Visible;
            
            btnToggleFoster.Visibility = Visibility.Visible;
            lblNoOfAnimals.Visibility = Visibility.Visible;
            if (animalsFostering < animalsCanFoster)
            {
                btnToggleFoster.IsEnabled = true;
            }    
        }
        /// <summary>
        /// Mads Rhea
        /// Created: 2023/02/05
        /// 
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/28
        /// 
        /// Final QA
        /// </remarks>
        private void ToggleFosterButtonText()
        {
            if (!isAvaliable)
            {
                btnToggleFoster.Content = "Unavailable";
            }
            else if(isAvaliable && (animalsFostering >= animalsCanFoster))
            {
                try
                {
                    _manager.FosterManager.EditCurrentlyAcceptingAnimalsByUsersId(_manager.User.UsersId, !isAvaliable);

                }
                catch (Exception)
                {
                    throw;
                }

                btnToggleFoster.Content = "Limit Reached";
            }
            else
            {
                btnToggleFoster.Content = "Available";
            }
        }
        /// <summary>
        /// Mads Rhea
        /// Created: 2023/02/05
        /// 
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/28
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnToggleFoster_Click(object sender, RoutedEventArgs e)
        {
            if (animalsFostering < animalsCanFoster)
            {
                var message = MessageBox.Show("Are you sure you want to turn your availability " + (isAvaliable ? "off?" : "on?"), "You're currently marked as " + (isAvaliable ? "\"Avaliable\"" : "\"Unavaliable\""), MessageBoxButton.YesNo);

                if (message == MessageBoxResult.Yes)
                {
                    try
                    {
                        isAvaliable = !isAvaliable;
                        _manager.FosterManager.EditCurrentlyAcceptingAnimalsByUsersId(_manager.User.UsersId, isAvaliable);
                        ToggleFosterButtonText();
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
            }
        }
    }
}
