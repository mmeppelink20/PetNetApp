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
using WpfPresentation.Management;
using WpfPresentation.UserControls;

namespace WpfPresentation.Management
{
    /// <summary>
    /// Interaction logic for ViewKennelPage.xaml
    /// </summary>
    public partial class ViewKennelPage : Page
    {
        private MasterManager masterManager = MasterManager.GetMasterManager();
        private List<KennelVM> kennelVMs = null;
        private List<KennelVM> kennelsToRemove = new List<KennelVM>();

        /// <summary>
        /// Author: Gwen
        /// Date: 4/21/23
        /// </summary>
        public ViewKennelPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Author: Gwen
        /// Date: 4/21/23
        /// </summary>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                kennelVMs = masterManager.KennelManager.RetrieveKennels(masterManager.User.ShelterId.Value);

                for (int i = 0; i < kennelVMs.Count / 4; i++)
                {
                    grdKennel.RowDefinitions.Add(new RowDefinition());
                }

                for (int i = 0; i < kennelVMs.Count; i++)
                {

                    KennelUserControl kennelUserControl = new KennelUserControl();

                    kennelUserControl.lblKennelName.Content += kennelVMs[i].KennelName + ": ";
                    if (kennelVMs[i].Animal == null)
                    {
                        UpdateUserControlStyles(kennelUserControl);
                    }
                    else
                    {
                        try
                        {
                            Images image = masterManager.KennelManager.RetrieveImageByAnimalId(kennelVMs[i].Animal.AnimalId);
                            kennelUserControl.imgAnimalDisplay.Source = masterManager.ImagesManager.RetrieveImageByImages(image);
                        }
                        catch (Exception)
                        {
                            BitmapImage brokenImage = new BitmapImage(new Uri("..\\Images\\BrokenImage.png", UriKind.Relative));
                            kennelUserControl.imgAnimalDisplay.Source = brokenImage;
                        }
                        
                        kennelUserControl.lblKennelName.Content += kennelVMs[i].Animal.AnimalName;
                    }
                    int j = i;
                    kennelUserControl.btnKennel.Click += (obj, arg) => UserControlClick(kennelVMs[j]);
                    kennelUserControl.btnKennelUserControl.Click += (obj, arg) => KennelUserControlClick(kennelVMs[j], kennelUserControl);

                    Grid.SetRow(kennelUserControl, i / 4);
                    Grid.SetColumn(kennelUserControl, i % 4);
                    grdKennel.Children.Add(kennelUserControl);
                }
            }
            catch (Exception ex)
            {
                PromptWindow.ShowPrompt("Error", ex.Message + "\n\n" + ex.InnerException, ButtonMode.Ok);
            }
        }

        /// <summary>
        /// Gwen Arman
        /// Created: 2023/02/01
        /// <param name="kennelVM"></param>
        private void UserControlClick(KennelVM kennelVM)
        {
            if(kennelVM.Animal != null)
            {
                // Created by: Asa
                NavigationService.Navigate(new KenOccupancyUpdate_333(kennelVM));
            } else
            {
                NavigationService.Navigate(new AssignAnimalToKennel(kennelVM));
            }
        }

        /// <summary>
        /// Author: Gwen
        /// Date: 4/21/23
        /// </summary>
        private void KennelUserControlClick(KennelVM kennelVM, KennelUserControl kennelUserControl)
        {
            if (kennelUserControl.grdKennelUserControlBorder.BorderBrush.ToString().Equals("#FF1C6758"))
            {
                kennelsToRemove.Add(kennelVM);
                kennelUserControl.grdKennelUserControlBorder.BorderBrush = new SolidColorBrush(Color.FromRgb(214, 205, 164));
            } 
            else
            {
                kennelsToRemove.Remove(kennelVM);
                kennelUserControl.grdKennelUserControlBorder.BorderBrush = new SolidColorBrush(Color.FromRgb(28, 103, 88));
            }

        }

        /// <summary>
        /// Gwen Arman
        /// Created: 2023/02/01
        /// 
        /// Method updates the KennelUserControl styles depending on whether the kennel 
        /// is occupied
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="kennelUserControl"></param>
        private void UpdateUserControlStyles(KennelUserControl kennelUserControl)
        {
            kennelUserControl.grdKennelUserControl.Background = new SolidColorBrush(Color.FromRgb(238, 242, 230));
            kennelUserControl.lblKennelName.Content += "Empty";
            kennelUserControl.lblKennelName.Foreground = new SolidColorBrush(Color.FromRgb(28, 103, 88));
            kennelUserControl.btnKennel.Content = "Add Animal";
            kennelUserControl.btnKennel.Background = new SolidColorBrush(Color.FromRgb(28, 103, 88));
            kennelUserControl.btnKennel.Foreground = new SolidColorBrush(Color.FromRgb(238, 242, 230));
        }

        /// <summary>
        /// Author: Gwen
        /// Date: 4/21/23
        /// </summary>
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddKennelPage());
        }

        /// <summary>
        /// Author: Gwen
        /// Date: 4/21/23
        /// </summary>
        private void btnRemoveKennel_Click(object sender, RoutedEventArgs e)
        {
            string kennelList = "";
            if(kennelsToRemove.Count == 0)
            {
                PromptWindow.ShowPrompt("Error", "You must select at least one kennel", ButtonMode.Ok);
                return;
            }
            for(int i = 0; i < kennelsToRemove.Count; i++)
            {
                kennelList += kennelsToRemove[i] != kennelsToRemove[kennelsToRemove.Count - 1] 
                    ? kennelsToRemove[i].KennelName + ", " : 
                    kennelsToRemove.Count != 1 ? "and " + kennelsToRemove[i].KennelName : kennelsToRemove[i].KennelName;
            }

            if (PromptWindow.ShowPrompt("Are you sure?", "Remove " + kennelList + "?", ButtonMode.YesNo) == PromptSelection.Yes)
            {

                for (int i = 0; i < kennelsToRemove.Count; i++)
                {
                    if (kennelsToRemove[i].Animal != null)
                    {
                        try
                        {
                            masterManager.KennelManager.RemoveAnimalKennlingByKennelId(kennelsToRemove[i].KennelId);
                        }
                        catch (Exception ex)
                        {
                            PromptWindow.ShowPrompt("Error", ex.Message);
                        }
                    }
                    try
                    {
                        masterManager.KennelManager.EditKennelStatusByKennelId(kennelsToRemove[i].KennelId);
                    }
                    catch (Exception ex)
                    {
                        PromptWindow.ShowPrompt("Error", ex.Message);
                    }
                }
                NavigationService.Navigate(new ViewKennelPage());
            }
            else
            {
                return;
            }
        }
    }
}
