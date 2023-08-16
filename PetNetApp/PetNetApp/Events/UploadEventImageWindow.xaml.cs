using DataObjects;
using LogicLayer;
using Microsoft.Win32;
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

namespace WpfPresentation.Events
{
    /// <summary>
    /// Interaction logic for UploadEventImageWindow.xaml
    /// </summary>
    public partial class UploadEventImageWindow : Window
    {
        private MasterManager _masteManager = MasterManager.GetMasterManager();
        private bool _imageSelected = false;
        private OpenFileDialog _fileDialog = new OpenFileDialog();
        private Images image;

        public UploadEventImageWindow()
        {
            InitializeComponent();
            image = null;
        }

        /// <summary>
        /// Author: Hoang Chu
        /// 04/27/2023
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBrowseFiles_Click(object sender, RoutedEventArgs e)
        {
            _fileDialog.Filter = "Images|*.png;*.jpg;*.gif;*.jpeg;*.tiff;*.tif;*.webp;*.wav;*.bmp;*.exif";
            _fileDialog.Multiselect = false;

            _imageSelected = _fileDialog.ShowDialog() == true;

            if (_imageSelected)
            {
                try
                {
                    imgSelectedImage.Source = new BitmapImage(new Uri(_fileDialog.FileName));
                    string filename = _fileDialog.SafeFileName;
                    txtFileUpload.Text = filename;
                }
                catch
                {
                    PromptWindow.ShowPrompt("Error", "Not a valid image");
                }
            }
            else
            {
                txtFileUpload.Text = "";
                imgSelectedImage.Source = null;
            }
        }

        /// <summary>
        /// Author: Hoang Chu
        /// 04/27/2023
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelUpload_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        /// <summary>
        /// Author: Hoang Chu
        /// 04/27/2023
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUploadFile_Click(object sender, RoutedEventArgs e)
        {
            if (_imageSelected)
            {
                try
                {
                    image = _masteManager.ImagesManager.AddImageByUri(_fileDialog.FileName);
                    PromptWindow.ShowPrompt("Message", "Successful to add new image.");
                    this.DialogResult = false;
                }
                catch (Exception ex)
                {
                    PromptWindow.ShowPrompt("Error", "Can not save this Image. \n\n" + ex.Message);
                }
            }
        }

        /// <summary>
        /// Author: Hoang Chu
        /// 04/27/2023
        /// 
        /// </summary>
        /// <returns></returns>
        public Images GetImage()
        {
            return image;
        }
    }
}
