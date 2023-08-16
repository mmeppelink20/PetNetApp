/// <summary>
/// Matthew Meppelink
/// Created: 2023/02/05
/// 
/// Contains code to dynamically create all of the necessary elements
/// for a view all animals screen.
/// </summary>
///
/// <remarks>
/// Zaid Rachman
/// Updated: 2023/04/21
/// Final QA
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
using LogicLayerInterfaces;

namespace WpfPresentation.Animals.Medical
{
    /// <summary>
    /// Interaction logic for MedicalPage.xaml
    /// </summary>
    public partial class MedicalPage : Page
    {
        private static MedicalPage _existingMedicalPage = null;

        private MasterManager _manager = null;

        private IAnimalManager _animalManager = null;
        private List<Animal> _animals = null;

        private Grid grid = null;
        /// <summary>
        /// Stephen Jaurigue
        /// 2023/02/11
        /// 
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/21
        /// Final QA
        /// </remarks>
        /// <param name="manager"></param>
        public MedicalPage(MasterManager manager)
        {
            InitializeComponent();
            _manager = manager;
            _animalManager = _manager.AnimalManager;
        }

        /// <summary>
        /// Stephen Jaurigue
        /// 2023/02/11
        /// 
        /// </summary>
        ///  <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/21
        /// Final QA
        /// </remarks>
        /// <param name="manager"></param>
        /// <returns></returns>
        public static MedicalPage GetMedicalPage(MasterManager manager)
        {
            if (_existingMedicalPage == null)
            {
                _existingMedicalPage = new MedicalPage(manager);
            }
            return _existingMedicalPage;
        }

        /// <summary>
        /// 
        /// </summary>
        ///  <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/21
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pgMedicalAnimalsView_Loaded(object sender, RoutedEventArgs e)
        {
            wrpMedicalAnimalList.Children.Clear(); // this prevents getting dupe animals when loading page a second time
            try
            {
                _animals = _animalManager.RetrieveAllAnimals("");
                foreach (Animal animal in _animals)
                {
                    CreateAnimalBox(animal);
                }
            }
            catch (Exception ex)
            {
                PromptWindow.ShowPrompt("Error", ex.Message + "\n" + ex.InnerException.Message, ButtonMode.Ok);
            }
        }

        /// <summary>
        /// Matthew Meppelink
        /// Created: 2023/02/16
        /// 
        /// dynamically creates the ui elements for a passed in animal
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: 2023/02/16 
        /// removed reduntant code
        /// </remarks>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/21
        /// Final QA
        /// </remarks>
        /// <param name="animal"></param>
        private void CreateAnimalBox(Animal animal)
        {
            grid = new Grid();
            grid.Width = 250;
            grid.Height = 300;

            Border border = new Border();
            border.Margin = new Thickness(22, 22, 0, 0);
            border.CornerRadius = new CornerRadius(10);
            border.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF9EC1B0");

            Button button = new Button();
            button.VerticalAlignment = VerticalAlignment.Bottom;
            button.Margin = new Thickness(48, 42, 25, 25);
            button.Height = 50;

            Border imageBorder = new Border();
            imageBorder.CornerRadius = new CornerRadius(10);
            imageBorder.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#5F987A");
            imageBorder.Margin = new Thickness(48, 42, 25, 85);

            Image image = new Image();
            image.Source = new BitmapImage(new Uri("/WpfPresentation;component/Images/no_image.png", UriKind.Relative));
            
            try
            {
                List<Images> animalImages = _manager.ImagesManager.RetrieveAnimalImagesByAnimalId(animal.AnimalId);
                if (animalImages.Count != 0)
                {
                    image.Source = _manager.ImagesManager.RetrieveImageByImages(animalImages[0]);
                }
            } 
            catch (Exception ex)
            {
                image.Source = new BitmapImage(new Uri("/WpfPresentation;component/Images/BrokenImageGreen.png", UriKind.Relative));
            }
            image.Height = 175;
            image.Width = 175;
            image.HorizontalAlignment = HorizontalAlignment.Center;
            image.Margin = new Thickness(55, 49, 32, 92);

            grid.Children.Add(border);
            grid.Children.Add(imageBorder);
            grid.Children.Add(image);
            grid.Children.Add(button);

            button.Content = animal.AnimalName;

            image.Cursor = Cursors.Hand;

            image.MouseLeftButtonDown += (s, e) =>
            {
                NavigationService.Navigate(new MedicalNavigationPage(_manager, animal));
            };

            button.Click += (s, e) =>
            {
                NavigationService.Navigate(new MedicalNavigationPage(_manager, animal));
            };

            wrpMedicalAnimalList.Children.Add(grid);
        }

        /// <summary>
        /// Matthew Meppelink
        /// Created: 2023/02/16
        /// 
        /// refreshes the list of animals, passing in a string, animalName,
        /// to be passed to the logic layer, and eventually data access layer
        /// to retrieve the necessary data
        /// 
        /// </summary>
        ///
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/21
        /// Final QA
        /// </remarks>
        /// <param name="animalName"></param>
        private void RefreshListOfAnimals(String animalName)
        {
            try
            {
                _animals = _animalManager.RetrieveAllAnimals(animalName);
            }
            catch (Exception ex)
            {
                PromptWindow.ShowPrompt("Error",ex.Message + "\n\n" + ex.InnerException.Message);
            }

            wrpMedicalAnimalList.Children.Clear();

            foreach (Animal animal in _animals)
            {
                CreateAnimalBox(animal);
            }
        }
        /// <summary>
        /// Stephen Jaurigue
        /// 2023/02/11
        /// 
        /// </summary>
        ///  <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/21
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSearchMedicalAnimals_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                RefreshListOfAnimals(txtSearchMedicalAnimals.Text);
            }
        }
    }
}
