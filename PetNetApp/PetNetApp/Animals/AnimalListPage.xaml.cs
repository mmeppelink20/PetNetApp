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
using WpfPresentation.UserControls;

namespace WpfPresentation.Animals
{
    /// <summary>
    /// Interaction logic for AnimalListPage.xaml
    /// </summary>
    public partial class AnimalListPage : Page
    {
        private static AnimalListPage _existingAnimalListPage = null;
        private MasterManager _masterManager = null;
        private List<Animal> _animals = null; 


        public AnimalListPage(MasterManager manager)
        {
            InitializeComponent();
            _masterManager = manager;
        }

        public static AnimalListPage GetAnimalListPage(MasterManager manager)
        {
            if (_existingAnimalListPage == null)
            {
                _existingAnimalListPage = new AnimalListPage(manager);
            }
            return _existingAnimalListPage;
        }

        private void Page_Loaded_1(object sender, RoutedEventArgs e)
        {
            try
            {
                _animals = _masterManager.AnimalManager.RetrieveAllAnimals(_masterManager.User.ShelterId.Value);
                // help from gwen, populate AnimalListPage with user controls
                for (int i = 0; i < _animals.Count / 4; i++)
                {
                    grdAnimalList.RowDefinitions.Add(new RowDefinition());
                }

                for (int i = 0; i < _animals.Count; i++)
                {
                    int j = i;
                    AnimalListUserControl animalListUserControl = new AnimalListUserControl(_animals[j]);
                    animalListUserControl.lblAnimalListAnimalName.Content = _animals[j].AnimalName;
                    animalListUserControl.lblAnimalListAnimalID.Content = _animals[j].AnimalId;
                    

                    Grid.SetRow(animalListUserControl, i / 4);
                    Grid.SetColumn(animalListUserControl, i % 4);
                    grdAnimalList.Children.Add(animalListUserControl);
                }
            }
            catch (Exception up)
            {
                PromptWindow.ShowPrompt("Error", up.Message + "\n\n" + up.InnerException);
                return;
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddAnimalPage(_masterManager));
        }
    }
}
