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

namespace WpfPresentation.UserControls
{
    /// <summary>
    /// Interaction logic for AnimalListUserControl.xaml
    /// </summary>
    public partial class AnimalListUserControl : UserControl
    {
        private AnimalVM _animal = null;
        private MasterManager _manager = MasterManager.GetMasterManager();
        
        public AnimalListUserControl(Animal animal)
        {
            InitializeComponent();
            _animal =_manager.AnimalManager.RetrieveAnimalByAnimalId(animal.AnimalId, animal.AnimalShelterId);
            try
            {
                List<Images> images = _manager.ImagesManager.RetriveImageByAnimalId(animal.AnimalId);
                if (images.Count != 0)
                {
                    imgAnimal.Source = _manager.ImagesManager.RetrieveImageByImages(images[0]);
                }
                else
                {
                    imgAnimal.Source = new BitmapImage(new Uri("/WpfPresentation;component/Images/no_image.png", UriKind.Relative));
                }
            }
            catch (Exception ex)
            {
                imgAnimal.Source = new BitmapImage(new Uri("/WpfPresentation;component/Images/BrokenImageGreen.png", UriKind.Relative));
            }
        }

        private void btnViewAnimalProfile_Click(object sender, RoutedEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            nav.Navigate(new WpfPresentation.Animals.EditDetailAnimalProfile(_manager, _animal));
        }
    }
}
