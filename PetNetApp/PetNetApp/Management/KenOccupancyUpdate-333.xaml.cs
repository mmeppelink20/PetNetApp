/// <summary>
/// Asa Armstrong
/// Created: 2023/02/16
/// 
/// WPF UI logic for removing an AnimalKenneling record.
/// </summary>
///
/// <remarks>
/// Asa Armstrong
/// Updated: 2023/03/01
/// Added Comments.
/// </remarks>

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

namespace WpfPresentation.Management
{
    public partial class KenOccupancyUpdate_333 : Page
    {
        private KennelVM _kennel = new KennelVM();
        private MasterManager _masterManager = MasterManager.GetMasterManager();

        /// <summary>
        /// Author: Asa Armstrong
        /// Date: 2023/04/23
        /// Description: Constructor for page KenOccupancyUpdate_333
        /// </summary>
        /// <param name="kennel"></param>
        public KenOccupancyUpdate_333(KennelVM kennel)
        {
            _kennel = kennel;

            InitializeComponent();
            Populate();
        }

        private void Populate()
        {
            lbl_KennelName.Content = _kennel.KennelName;
            lbl_AnimalNameTitle.Content = "This is " + _kennel.Animal.AnimalName + "'s kennel!";
            lbl_Species.Content = _kennel.AnimalTypeId;
            lbl_Name.Content = _kennel.Animal.AnimalName;
            lbl_Intake.Content = _kennel.Animal.BroughtIn.ToShortDateString();
        }

        /// <summary>
        /// Author: Asa Armstrong
        /// Date: 2023/04/23
        /// Description: Removes the animal from the kennel in the DB.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Remove_Click(object sender, RoutedEventArgs e)
        {
            bool result = false;

            try
            {
                if (PromptSelection.Yes == PromptWindow.ShowPrompt("Remove", "Remove animal from kennel?", ButtonMode.YesNo))
                {
                    result = _masterManager.KennelManager.RemoveAnimalKennelingByKennelIdAndAnimalId(_kennel.KennelId, _kennel.Animal.AnimalId);
                }
            }
            catch (Exception ex)
            {
                PromptWindow.ShowPrompt("Error", ex.Message, ButtonMode.Ok);
            }

            if (result)
            {
                PromptWindow.ShowPrompt("Congrats", "Animal Kenneling removed.", ButtonMode.Ok);
                NavigationService.Navigate(new WpfPresentation.Management.ViewKennelPage());
            }
            else
            {
                PromptWindow.ShowPrompt("Error", "Something went wrong. Try again later.", ButtonMode.Ok);
            }
        }

        /// <summary>
        /// Author: Asa Armstrong
        /// Date: 2023/04/23
        /// Description: Returns from the page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new WpfPresentation.Management.ViewKennelPage());
        }
    }
}
