///<summary>
///Molly Meister
///2023/02/17
/// 
/// </summary>
/// <remarks>
/// Zaid Rachman
/// Updated: 2023/04/21
/// Final QA
/// </remarks>
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

namespace WpfPresentation.Animals
{
    /// <summary>
    /// Interaction logic for MedicalFilesPage.xaml
   
    /// </summary>
    public partial class MedicalFilesPage : Page
    {
        private Animal _animal = null;
        private List<Images> _imagesList;
        private MasterManager _manager = MasterManager.GetMasterManager();
        private ToolTip _rowImageTooltip;
        private Image _rowTooltipImage = new Image() { MaxHeight = 500, MaxWidth = 500, Stretch = Stretch.Uniform, StretchDirection = StretchDirection.Both };

        /// <summary>
        /// Stephen Jaurique
        /// 2023/02/26
        /// 
        /// 
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/21
        /// Final QA
        /// </remarks>
        /// <param name="animal"></param>
        /// <param name="masterManager"></param>
        public MedicalFilesPage(Animal animal, MasterManager masterManager)
        {
            _rowImageTooltip = new ToolTip();
            _rowImageTooltip.Content = _rowTooltipImage;
            _animal = animal;
            _manager = masterManager;
            InitializeComponent();
        }
        /// <summary>
        /// Stephen Jaurique
        /// 2023/02/26
        /// 
        /// 
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/21
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Loaded_1(object sender, RoutedEventArgs e)
        {
            PopulatePage();
        }
        /// <summary>
        /// Stephen Jaurique
        /// 2023/02/26
        /// 
        /// 
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/21
        /// Final QA
        /// </remarks>
        private void PopulatePage()
        {
            if (_imagesList == null || _imagesList.Count == 0)
            {
                try
                {
                    lblNoFiles.Visibility = Visibility.Hidden;
                    _imagesList = _manager.ImagesManager.RetrieveMedicalImagesByAnimalId(_animal.AnimalId);
                    datAdditionalFiles.ItemsSource = _imagesList;

                }
                catch (Exception ex)
                {
                    PromptWindow.ShowPrompt("Error", ex.Message + "\n\n" + ex.InnerException.Message);
                }
            }
        }
        /// <summary>
        /// Andrew Cromwell
        /// 
        /// 2023/02/26
        /// 
        /// 
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/21
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridRow_MouseEnter(object sender, MouseEventArgs e)
        {
            var row = e.Source as DataGridRow;
            try
            {
                _rowTooltipImage.Source = _manager.ImagesManager.RetrieveImageByImages((Images)row.Item);
                row.ToolTip = _rowImageTooltip;
            }
            catch (Exception ex)
            {
                row.ToolTip = ex.Message;
            }
        }
    }
}
