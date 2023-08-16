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

namespace WpfPresentation.Animals
{
    /// <summary>
    /// Interaction logic for AnimalUpdatesWindow.xaml
    /// </summary>
    public partial class AnimalUpdatesWindow : Window
    {
        private List<AnimalUpdates> _animalUpdates = new List<AnimalUpdates>();
        public AnimalUpdatesWindow(List<AnimalUpdates> animalUpdates)
        {
            InitializeComponent();
            _animalUpdates.AddRange(animalUpdates.Skip(1));
        }

        private void DisplayAnimalUpdate(AnimalUpdates au)
        {
            UCAnimalUpdates ucAnimalUpdate = new UCAnimalUpdates();
            ucAnimalUpdate.lblDateUpdate.Content = au.AnimalRecordDate.ToShortDateString();
            ucAnimalUpdate.tbAnimalUpdate.Text = au.AnimalRecordNotes;
            stpAnimalUpdates.Children.Add(ucAnimalUpdate);
        }

        private void PopulateAnimalUpdate()
        {
            stpAnimalUpdates.Children.Clear();
            if (_animalUpdates.Count() > 0)
            {
                foreach (AnimalUpdates au in _animalUpdates)
                {
                    DisplayAnimalUpdate(au);
                }
            }
            else
            {
                stpAnimalUpdates.Children.Add(tbNoUpdate);
            }
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            PopulateAnimalUpdate();
        }
    }
}
