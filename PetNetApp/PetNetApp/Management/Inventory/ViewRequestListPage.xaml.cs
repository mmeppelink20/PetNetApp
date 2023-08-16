/// <summary>
/// Andrew Cromwell
/// Created: 2023/03/16
/// 
/// A page that contains a list of shelter requests
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
    /// Interaction logic for ViewRequestListPage.xaml
    /// </summary>
    public partial class ViewRequestListPage : Page
    {
        private static ViewRequestListPage _existingViewRequestListPage = null;

        private MasterManager _manager;
        private List<RequestVM> _requests;

        /// <summary>
        /// Andrew Cromwell
        /// Created: 2023/03/16
        /// 
        /// Initial constructor for the shelter request list page
        /// </summary>
        /// 
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/17
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="manager"></param>
        public ViewRequestListPage(MasterManager manager)
        {
            InitializeComponent();
            _manager = manager;
        }

        /// <summary>
        /// Andrew Cromwell
        /// Created: 2023/03/16
        /// 
        /// Gets the shelter request list page
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/17
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="manager"></param>
        /// <returns></returns>
        public static ViewRequestListPage GetViewRequestListPage(MasterManager manager)
        {
            if (_existingViewRequestListPage == null)
            {
                _existingViewRequestListPage = new ViewRequestListPage(manager);
            }
            return _existingViewRequestListPage;
        }

        /// <summary>
        /// Andrew Cromwell
        /// Created: 2023/03/16
        /// 
        /// retreives a list of requests and displays them
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
            stpRequestList.Children.Clear();
            try
            {
                _requests = _manager.RequestManager.RetrieveRequestsByShelterId(_manager.User.ShelterId.Value);
                for(int i = 0; i < _requests.Count; i++)
                {
                    AddRequestToList(_requests[i], i);
                }
            }catch(Exception ex)
            {
                PromptWindow.ShowPrompt("Error", ex.Message + "\n\n" + ex.InnerException.Message);
            }
            if(_requests.Count == 0 || _requests == null)
            {
                Label lblNoRequests = new Label();                
                lblNoRequests.Margin = new Thickness(20, 20, 20, 20);
                lblNoRequests.Content = "No Requests Found.";
                lblNoRequests.FontSize = 20;
                lblNoRequests.HorizontalContentAlignment = HorizontalAlignment.Center;
                lblNoRequests.VerticalContentAlignment = VerticalAlignment.Center;
                stpRequestList.Children.Add(lblNoRequests);
            }
        }

        /// <summary>
        /// Andrew Cromwell
        /// Created: 2023/03/16
        /// 
        /// dynamically creates the ui elements for the list of requests received
        /// </summary>
        /// <remarks>
        /// Andrew Cromwell
        /// 2023/04/14
        /// 
        /// Altered the page and this method so that when a checkbox is checked, acknowleged requests are shown,
        /// but are not shown if the box is not checked.
        /// 
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/17
        /// 
        /// Final QA
        /// </remarks>
        /// 
        /// <param name="request">The request to add to the list</param>
        /// <param name="index">int used to alternate the background color of list items</param>
        private void AddRequestToList(RequestVM request, int index)
        {
            if(request.Acknowledged == false || chkShowAcknowleged.IsChecked == true)
            {
                Grid grid = new Grid();
                grid.Height = 100;
                ColumnDefinition col1 = new ColumnDefinition();
                col1.Width = GridLength.Auto;
                ColumnDefinition col2 = new ColumnDefinition();
                col2.Width = GridLength.Auto;
                ColumnDefinition col3 = new ColumnDefinition();
                col3.Width = GridLength.Auto;
                grid.ColumnDefinitions.Add(col1);
                grid.ColumnDefinitions.Add(col2);
                grid.ColumnDefinitions.Add(col3);

                var bc = new BrushConverter();
                if (index % 2 == 0)
                {
                    grid.Background = (Brush)bc.ConvertFrom("#3D8361");
                }
                else
                {
                    grid.Background = (Brush)bc.ConvertFrom("#D6CDA4");
                }

                Label lblFromShelter = new Label();
                lblFromShelter.Content = "Request from " + request.RequestingShelterName + " on " + request.RequestDate.ToString("yyyy'-'MM'-'dd") + ".";
                lblFromShelter.Margin = new Thickness(20, 20, 20, 20);
                Grid.SetColumn(lblFromShelter, 0);
                lblFromShelter.FontSize = 20;
                lblFromShelter.Foreground = (Brush)bc.ConvertFrom("#000000");
                lblFromShelter.HorizontalContentAlignment = HorizontalAlignment.Center;
                lblFromShelter.VerticalContentAlignment = VerticalAlignment.Center;

                int totalItemsRequested = 0;
                foreach(RequestResourceLine line in request.RequestLines)
                {
                    totalItemsRequested += line.QuantityRequested;
                }
                Label lblTotalItems = new Label();
                lblTotalItems.Content = totalItemsRequested.ToString("#,##0") + " items requested.";
                lblTotalItems.Margin = new Thickness(20, 32, 20, 20);
                Grid.SetColumn(lblTotalItems, 1);
                lblTotalItems.FontSize = 20;
                lblTotalItems.Foreground = (Brush)bc.ConvertFrom("#000000");
                lblFromShelter.HorizontalContentAlignment = HorizontalAlignment.Center;
                lblFromShelter.VerticalContentAlignment = VerticalAlignment.Center;

                Button button = new Button();
                button.Content = "View Request";
                button.Width = 125;
                button.Margin = new Thickness(20, 20, 20, 20);
                button.Height = 50;
                button.Click += (s, e) =>
                {
                    NavigationService.Navigate(new SpecificRequestPage(_manager, request));
                };
                Grid.SetColumn(button, 2);
                lblFromShelter.HorizontalContentAlignment = HorizontalAlignment.Center;
                lblFromShelter.VerticalContentAlignment = VerticalAlignment.Center;
                if(index % 2 == 0)
                {
                    button.Background = (Brush)bc.ConvertFrom("#D6CDA4");
                    button.Foreground = (Brush)bc.ConvertFrom("#000000");
                }

                grid.Children.Add(lblFromShelter);
                grid.Children.Add(lblTotalItems);
                grid.Children.Add(button);
                stpRequestList.Children.Add(grid);
            }
            
        }

        /// <summary>
        /// Andrew Cromwell
        /// Created: 2023/04/14
        /// 
        /// refreshes the page when the box to show acknowleged requests is clicked
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
        private void chkShowAcknowleged_Click(object sender, RoutedEventArgs e)
        {
            Page_Loaded(sender, e);
        }
    }
}
