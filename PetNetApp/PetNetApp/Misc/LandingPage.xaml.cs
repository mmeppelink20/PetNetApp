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
using LogicLayer;
using DataObjects;
using PetNetApp;

namespace WpfPresentation.Misc
{
    /// <summary>
    /// Mads Rhea
    /// Created: 2023/02/15
    /// 
    /// WPF for the frame of the "Landing Page".
    /// </summary>
    ///
    /// <remarks>
    /// Updater Name
    /// Updated: yyyy/mm/dd
    /// </remarks>
    public partial class LandingPage : Page
    {
        private static LandingPage _existingLanding = null;
        private MasterManager _manager = MasterManager.GetMasterManager();
        private MainWindow _mainWindow = null;
        private int _num = 0;
        private List<string> bannerImages = null;

        public LandingPage()
        {
            InitializeComponent();
            BannerImageLinks();
        }

        public static LandingPage GetLandingPage(MainWindow mainWindow)
        {
           
            _existingLanding = new LandingPage();


            _existingLanding._mainWindow = mainWindow;

            return _existingLanding;
        }

        /// <summary>
        /// [Mads Rhea - 2023/02/19]
        /// Adds the image link strings into "bannerImages" and then transforms them into a List of ImageSource objects.
        /// </summary>
        private List<ImageSource> BannerImageLinks()
        {
            bannerImages = new List<string>()
            {
                "../../Images/Placeholder/bannerImage_1.jpg",
                "../../Images/Placeholder/bannerImage_2.jpg",
                "../../Images/Placeholder/bannerImage_3.jpg",
                "../../Images/Placeholder/bannerImage_4.jpg",
                "../../Images/Placeholder/bannerImage_5.jpg",
                "../../Images/Placeholder/bannerImage_6.jpg",
                "../../Images/Placeholder/bannerImage_7.jpg",
                "../../Images/Placeholder/bannerImage_8.jpg",
                "../../Images/Placeholder/bannerImage_9.jpg",
                "../../Images/Placeholder/bannerImage_10.jpg"
            };

            Uri imageUri = null;
            BitmapImage imageBitmap = null;
            List<ImageSource> imageList = new List<ImageSource>();

            foreach (var imgString in bannerImages)
            {
                imageUri = new Uri(imgString, UriKind.Relative);
                imageBitmap = new BitmapImage(imageUri);
                imageList.Add(imageBitmap);
            }

            return imageList;

        }

        /// <summary>
        /// [Mads Rhea - 2023/02/19]
        /// Changes banner image when user clicks either the left or right arrows that appear when the user hovers over the banner.
        /// </summary>
        private void ChangeBannerImageOnBannerClick()
        {
            if (_num <= 0)
            {
                _num = bannerImages.Count - 1;
            }
            else if (_num >= bannerImages.Count - 1)
            {
                _num = 0;
            }

            ImageBrush imageBrush = new ImageBrush(BannerImageLinks()[_num]);
            canvasBanner.Background = imageBrush;
        }

        private void canvasBanner_MouseDown(object sender, MouseButtonEventArgs e)
        {
            int position = (int)e.GetPosition(canvasBanner).X;

            if (position <= canvasBanner.ActualWidth / 4)
            {
                _num--;
                ChangeBannerImageOnBannerClick();
            }
            else if (position >= canvasBanner.ActualWidth / 4)
            {
                _num++;
                ChangeBannerImageOnBannerClick();
            }
        }

        private void canvasBanner_MouseLeave(object sender, MouseEventArgs e)
        {
            imgLeftArrow.Visibility = Visibility.Hidden;
            imgRightArrow.Visibility = Visibility.Hidden;
            imgRightArrow.Visibility = Visibility.Hidden;
            rectRightArrow.Visibility = Visibility.Hidden;
        }

        private void canvasBanner_MouseMove(object sender, MouseEventArgs e)
        {
            int position = (int)e.GetPosition(canvasBanner).X;

            if (position <= canvasBanner.ActualWidth / 4)
            {
                imgLeftArrow.Visibility = Visibility.Visible;
                rectLeftArrow.Visibility = Visibility.Visible;
                imgRightArrow.Visibility = Visibility.Hidden;
                rectRightArrow.Visibility = Visibility.Hidden;
            }
            else if (position >= canvasBanner.ActualWidth / 4 * 3)
            {
                imgLeftArrow.Visibility = Visibility.Hidden;
                rectLeftArrow.Visibility = Visibility.Hidden;
                imgRightArrow.Visibility = Visibility.Visible;
                rectRightArrow.Visibility = Visibility.Visible;
            }
            else
            {
                imgLeftArrow.Visibility = Visibility.Hidden;
                rectLeftArrow.Visibility = Visibility.Hidden;
                imgRightArrow.Visibility = Visibility.Hidden;
                rectRightArrow.Visibility = Visibility.Hidden;

            }
        }

        private void canvasBanner_MouseEnter(object sender, MouseEventArgs e)
        {
            Canvas.SetTop(imgLeftArrow, (canvasBanner.ActualHeight / 2) - (imgLeftArrow.Height / 2));
            Canvas.SetTop(imgRightArrow, (canvasBanner.ActualHeight / 2) - (imgRightArrow.Height / 2));
            rectLeftArrow.Width = canvasBanner.ActualWidth / 4;
            rectLeftArrow.Height = canvasBanner.ActualHeight;
            rectRightArrow.Width = canvasBanner.ActualWidth / 4;
            rectRightArrow.Height = canvasBanner.ActualHeight;
        }

        private void frameLandingPage_Loaded(object sender, RoutedEventArgs e)
        {
            if(_manager.User == null)
            {
                frameLandingPage.Navigate(LandingBodyLoggedInPage.GetLandingBodyLoggedInPage(_mainWindow));
            }
        }
    }
}
