/// <summary>
/// Zaid Rachman
/// Created: 2023/03/19
/// 
/// </summary>
///
/// <remarks>
/// Oleksiy Fedchuk
/// Updated: 2023/04/20
/// 
/// Final QA
/// 
/// Nathan Zumsande
/// Updated: 2023/04/25
/// Added the print cart button and print cart click event
/// </remarks>
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
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
using System.Windows.Forms;

namespace WpfPresentation.Management.Inventory
{
    /// <summary>
    /// Interaction logic for ViewEditCartPage.xaml
    /// </summary>
    public partial class ViewEditCartPage : Page
    {
        List<ShelterInventoryItemVM> _shelterInventoryItemVMCart = new List<ShelterInventoryItemVM>();

        //Size to change the cart width so the grids width is fully printed
        private const int DATA_GRID_PRINT_WIDTH = 780;
        //x coordiante for the start of the display rectangle
        private const double X_COORDINATE = 5;
        //y coordiante for the start of the display rectangle
        private const double Y_COORDINATE = 5;

        /// <summary>
        /// Zaid Rachman
        /// Created: 2023/03/19
        /// 
        /// </summary>
        ///
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/20
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="shelterInventoryItemVMs"></param>
        public ViewEditCartPage(List<ShelterInventoryItemVM> shelterInventoryItemVMs)
        {
            _shelterInventoryItemVMCart = shelterInventoryItemVMs;
            InitializeComponent();
        }
        /// <summary>
        /// Zaid Rachman
        /// Created: 2023/03/19
        /// 
        /// </summary>
        ///
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/20
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            datCartList.ItemsSource = _shelterInventoryItemVMCart;
        }
        /// <summary>
        /// Zaid Rachman
        /// Created: 2023/03/19
        /// 
        /// </summary>
        ///
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/20
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ViewShelterInventoryPage(_shelterInventoryItemVMCart));
        }
        /// <summary>
        /// Zaid Rachman
        /// Created: 2023/03/19
        /// 
        /// </summary>
        ///
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/20
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClearCart_Click(object sender, RoutedEventArgs e)
        {
            _shelterInventoryItemVMCart.Clear();
            NavigationService.Navigate(new ViewEditCartPage(_shelterInventoryItemVMCart));
        }

        /// <summary>
        /// Nathan Zumsande
        /// Created: 2023/04/25
        /// 
        /// Click event to popup the print dialog in order to print the cart grid
        /// </summary>
        ///
        /// <remarks>
        /// Zaid Rachman
        /// 2023/04/26
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrintCart_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.PrintDialog Printdlg = new System.Windows.Controls.PrintDialog();

            //set width of data grid to fit onto the page
            datCartList.Width = DATA_GRID_PRINT_WIDTH;
            if ((bool)Printdlg.ShowDialog().GetValueOrDefault())
            {
                Size pageSize = new Size(Printdlg.PrintableAreaWidth, Printdlg.PrintableAreaHeight);
                // sizing of the element.
                datCartList.Measure(pageSize);
                datCartList.Arrange(new Rect(X_COORDINATE, Y_COORDINATE, pageSize.Width, pageSize.Height));
                Printdlg.PrintVisual(datCartList, Title);
            }

            //reset the width of the datagrid to be auto
            datCartList.Width = Double.NaN;
        }
    }
}
