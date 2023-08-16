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
    /// Andrew Cromwell
    /// 2023/04/06
    /// 
    /// Interaction logic for RequestInProgress.xaml
    /// </summary>
    public partial class RequestInProgress : Page
    {
        private MasterManager _manager = null;
        private RequestVM _request = null;
        private Shelter _shelter = null;

        public RequestInProgress(MasterManager manager, RequestVM request, Shelter shelter)
        {
            InitializeComponent();
            _manager = manager;
            _request = request;
            _shelter = shelter;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            datRequestLineList.ItemsSource = _request.RequestLines;
        }

        private void btnClearRequest_Click(object sender, RoutedEventArgs e)
        {
            _request.RequestLines = new List<RequestResourceLine>();
            Page_Loaded(sender, e);
        }

        /// <summary>
        /// Andrew Cromwell
        /// Created: 2023/04/07
        /// 
        /// Passes the request to the manager to be saved if the user is done with it.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRequestSupplies_Click(object sender, RoutedEventArgs e)
        {
            if (_request.RequestLines.Count == 0)
            {
                PromptWindow.ShowPrompt("Send Request", "You can not send a request with no items in it.", ButtonMode.Ok);
                return;
            }

            PromptSelection selection = PromptWindow.ShowPrompt("Send Request", "Are you sure you are ready to send your Request."
                , ButtonMode.YesNo);
            if (selection == PromptSelection.Yes)
            {
                try
                {
                    bool success;
                    success = _manager.RequestManager.AddInventoryItemRequest(_request);
                    if (success)
                    {
                        PromptWindow.ShowPrompt("Success", "Your request was sent to " + _shelter.ShelterName, ButtonMode.Ok);
                        _request = null;
                        CreateShelterRequestPage.GetCreateShelterRequestPage(null);
                        NavigationService.Navigate(SelectShelterForRequestPage.GetSelectShelterForRequestPage(_manager));
                    }
                    else
                    {
                        PromptWindow.ShowPrompt("Error", "Something went wrong. The request was not sent", ButtonMode.Ok);
                    }
                }
                catch (Exception ex)
                {
                    PromptWindow.ShowPrompt("Error", ex.Message + "\n\n" + ex.InnerException.Message, ButtonMode.Ok);
                }
            }
        }
    }
}
