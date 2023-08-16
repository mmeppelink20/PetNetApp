/// <summary>
/// Andrew Cromwell
/// Created: 2023/03/16
/// 
/// A page that is for a specific shelter request 
/// </summary>
/// 
/// <remarks>
/// Oleksiy Fedchuk
/// Updated: 2023/04/17
/// 
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
using LogicLayer;
using DataObjects;

namespace WpfPresentation.Management.Inventory
{
    /// <summary>
    /// Interaction logic for SpecificRequestPage.xaml
    /// </summary>
    public partial class SpecificRequestPage : Page
    {
        private MasterManager _manager;
        private RequestVM _request;

        public SpecificRequestPage(MasterManager manager, RequestVM request)
        {
            InitializeComponent();
            _manager = manager;
            _request = request;
        }

        /// <summary>
        /// Andrew Cromwell
        /// Created: 2023/03/16
        /// 
        /// Displays the items that were requested
        /// </summary>
        /// 
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/17
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            lblRequestFrom.Content = "Request from " + _request.RequestingShelterName + " on " + _request.RequestDate.ToString("yyyy'-'MM'-'dd") + ".";
            foreach (RequestResourceLine line in _request.RequestLines)
            {
                DisplayLine(line);
            }
        }

        /// <summary>
        /// Andrew Cromwell
        /// Created: 2023/03/16
        /// 
        /// dynamically creates the ui elements to show what resources have been requested
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/17
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="line">The RequestResourceLine to display data from.</param>
        private void DisplayLine(RequestResourceLine line)
        {
            Grid grid = new Grid();
            grid.Width = 250;
            grid.Height = 300;
            RowDefinition row1 = new RowDefinition();
            row1.Height = GridLength.Auto;
            RowDefinition row2 = new RowDefinition();
            row2.Height = GridLength.Auto;
            RowDefinition row3 = new RowDefinition();
            row3.Height = GridLength.Auto;
            RowDefinition row4 = new RowDefinition();
            row4.Height = GridLength.Auto;
            grid.RowDefinitions.Add(row1);
            grid.RowDefinitions.Add(row2);
            grid.RowDefinitions.Add(row3);
            grid.RowDefinitions.Add(row4);

            Border border = new Border();
            border.Margin = new Thickness(22, 22, 0, 0);
            border.CornerRadius = new CornerRadius(10);
            border.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF9EC1B0");
            border.Padding = new Thickness(10);
            border.Child = grid;

            var bc = new BrushConverter();

            Label lblItem = new Label();
            lblItem.HorizontalContentAlignment = HorizontalAlignment.Center;
            lblItem.FontSize = 20;
            lblItem.Foreground = (Brush)bc.ConvertFrom("#000000");
            lblItem.Content = line.ItemId;
            lblItem.Margin = new Thickness(5);
            Grid.SetRow(lblItem, 0);
            grid.Children.Add(lblItem);

            Label lblQuantityRequested = new Label();
            lblQuantityRequested.Content = "Quantity Requested: " + line.QuantityRequested.ToString("#,##0");
            lblQuantityRequested.FontSize = 20;
            lblQuantityRequested.Foreground = (Brush)bc.ConvertFrom("#000000");
            lblQuantityRequested.HorizontalContentAlignment = HorizontalAlignment.Center;
            lblQuantityRequested.Margin = new Thickness(5);
            Grid.SetRow(lblQuantityRequested, 1);
            grid.Children.Add(lblQuantityRequested);

            Label lblQuantityHeld = new Label();
            lblQuantityHeld.Content = "Quantity Held: " + line.QuantityAvailable.ToString("#,##0");
            lblQuantityHeld.FontSize = 20;
            lblQuantityHeld.Foreground = (Brush)bc.ConvertFrom("#000000");
            lblQuantityHeld.HorizontalContentAlignment = HorizontalAlignment.Center;
            lblQuantityHeld.Margin = new Thickness(5);
            Grid.SetRow(lblQuantityHeld, 2);
            grid.Children.Add(lblQuantityHeld);

            TextBlock txtNotes = new TextBlock();
            txtNotes.Text = "Notes:\n" + line.Notes;
            txtNotes.HorizontalAlignment = HorizontalAlignment.Center;
            txtNotes.FontSize = 15;
            txtNotes.Margin = new Thickness(5);
            Grid.SetRow(txtNotes, 3);
            grid.Children.Add(txtNotes);

            wrpRequestItems.Children.Add(border);
        }
        /// <summary>
        /// Andrew Cromwell
        /// Created: 2023/03/16
        /// 
        /// returns the user to the shelter request list page
        /// </summary>
        /// 
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/17
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(ViewRequestListPage.GetViewRequestListPage(_manager));
        }

        /// <summary>
        /// Matthew Meppelink
        /// Created: 2023/04/14
        /// 
        /// Acknowledges an inventory request
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAcknowledge_Click(object sender, RoutedEventArgs e)
        {
            if (_request.Acknowledged == true)
            {
                PromptWindow.ShowPrompt("Request Acknowledgment", "This request has already been acknowledged", ButtonMode.Ok);
            }
            else
            {
                try
                {
                    bool newAcnowledged = true;
                    _manager.RequestManager.EditRequestAcknowledge(_request.RequestId, _request.Acknowledged, newAcnowledged);
                    NavigationService.GoBack();
                    PromptWindow.ShowPrompt("Request Acknowledgment", "This request has been acknowledged", ButtonMode.Ok);
                }
                catch (Exception ex)
                {
                    PromptWindow.ShowPrompt("Error", ex.Message + "\n\n" + ex.InnerException.Message, ButtonMode.Ok);
                }
            }
        }
    }
}
