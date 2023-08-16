/// <summary>
/// Andrew Cromwell
/// 2023/04/04
/// 
/// Interaction logic for CreateShelterRequestPage.xaml
/// </summary>
///
/// <remarks>
/// Brian Collum
/// Updated: 2023/04/21
/// 
/// Added Inventory UI filtering
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
    /// Andrew Cromwell
    /// 2023/04/04
    /// 
    /// Interaction logic for CreateShelterRequestPage.xaml
    /// </summary>
    public partial class CreateShelterRequestPage : Page
    {
        public static CreateShelterRequestPage _existingCreateShelterRequestPage = null;
        private Shelter _shelter = null;
        private MasterManager _manager = null;
        private List<ShelterInventoryItemVM> _shelterInventoryItemVMList = null;
        private RequestVM _request = new RequestVM();

        // Item Filtering
        string _itemNameFilter = null;
        List<ShelterInventoryItemVM> _shelterFilteredInventoryItemVMList = null;

        public CreateShelterRequestPage(MasterManager manager, Shelter shelter)
        {
            InitializeComponent();
            _manager = manager;
            _shelter = shelter;
            _existingCreateShelterRequestPage = this;
            _request.RequestLines = new List<RequestResourceLine>();
            _request.RequestedByUserId = _manager.User.UsersId;
            _request.RecievingShelterId = _shelter.ShelterId;
        }

        public static CreateShelterRequestPage GetCreateShelterRequestPage(MasterManager manager)
        {
            if (manager == null)
            {
                _existingCreateShelterRequestPage = null;
            }
            return _existingCreateShelterRequestPage;
        }

        /// <summary>
        /// Andrew Cromwell
        /// Created: 2023/04/05
        /// 
        /// Populates the datagrid with the items at this page's shelter.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (_request == null)
            {
                NavigationService.Navigate(SelectShelterForRequestPage.GetSelectShelterForRequestPage(_manager));
                return;
            }

            lblShelterName.Content = _shelter.ShelterName;
            try
            {
                _shelterInventoryItemVMList = _manager.ShelterInventoryItemManager.RetrieveInventoryByShelterId(_shelter.ShelterId);
            }
            catch (Exception)
            {

                PromptWindow.ShowPrompt("Missing Data", "Failed to retrieve shelter inventory");
                return;
            }
            try
            {
                UpdateFlags();
                datShelterInventory.ItemsSource = _shelterInventoryItemVMList;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            lblItemsInRequest.Content = "Items In Request: " + _request.RequestLines.Count.ToString();
        }


        /// <summary>
        /// Zaid Rachman
        /// Created: 2023/03/19
        /// 
        /// Populates the flags column
        /// </summary>
        /// <remarks>
        /// Andrew Cromwell
        /// 2023/04/05
        /// Copied from ViewShelterInventoryPage
        /// </remarks>
        private void UpdateFlags()
        {

            List<ShelterInventoryItemVM> shelterItems = _shelterInventoryItemVMList;

            //Creates a list of flags in string form
            foreach (ShelterInventoryItemVM shelter in shelterItems)
            {
                List<string> Flags = new List<string>(); //used to collect all flags for a shelter item
                string flagsList = ""; //used later for formating the flags
                if (shelter.InTransit)
                {
                    Flags.Add("In Transit");
                }
                if (shelter.Urgent)
                {
                    Flags.Add("Urgent");
                }
                if (shelter.Processing)
                {
                    Flags.Add("Processing");
                }
                if (shelter.DoNotOrder)
                {
                    Flags.Add("Do Not Order");
                }
                if (shelter.CustomFlag != "")
                {
                    Flags.Add(shelter.CustomFlag);
                }

                //Formating
                for (int i = 0; i < Flags.Count; i++)
                {
                    flagsList += " " + Flags[i];

                    if (i == Flags.Count - 2)
                    {
                        if (Flags.Count > 2)
                        {
                            flagsList += ",";
                        }
                        flagsList += " and";
                    }
                    else if (i < Flags.Count - 2)
                    {
                        flagsList += ",";
                    }
                }
                shelter.DisplayFlags = flagsList; //Using the CustomFlag property as a way to show all flags
            }

        }

        /// <summary>
        /// Andrew Cromwell
        /// Created: 2023/04/06
        /// 
        /// If the user verifies that they do want to change shelters, they are taken to the SelectShelterForRequestPag.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnChangeShelter_Click(object sender, RoutedEventArgs e)
        {
            PromptSelection selection = PromptWindow.ShowPrompt("Change Shelter", "Are you sure you want to change to shelter you are requesting from?\nThis will clear your current request."
                , ButtonMode.YesNo);
            if (selection == PromptSelection.Yes)
            {
                NavigationService.Navigate(SelectShelterForRequestPage.GetSelectShelterForRequestPage(_manager));
            }
        }

        /// <summary>
        /// Andrew Cromwell
        /// Created: 2023/04/06
        /// 
        /// Launches the AddRequestLinePage for the selected item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddToRequest_Click(object sender, RoutedEventArgs e)
        {
            ShelterInventoryItemVM selectedItem = (ShelterInventoryItemVM)datShelterInventory.SelectedItem;
            if (datShelterInventory.SelectedItem != null)
            {
                if (_request.RequestLines.Count > 0)
                {
                    bool alreadyIn = false;
                    foreach (RequestResourceLine line in _request.RequestLines)
                    {
                        if (line.ItemId.Equals(selectedItem.ItemId))
                        {
                            alreadyIn = true;
                            break;
                        }
                    }
                    if (!alreadyIn)
                    {
                        frmAddItemToRequest.Navigate(new AddRequestLinePage(_manager, _request, selectedItem));
                        Page_Loaded(sender, e);
                    }
                }
                else
                {
                    frmAddItemToRequest.Navigate(new AddRequestLinePage(_manager, _request, selectedItem));
                    Page_Loaded(sender, e);
                }

            }
        }

        private void frmAddItemToRequest_Navigated(object sender, NavigationEventArgs e)
        {
            lblItemsInRequest.Content = "Items In Request: " + _request.RequestLines.Count.ToString();
        }

        private void btnViewRequest_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RequestInProgress(_manager, _request, _shelter));
        }

        private void datShelterInventory_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            btnAddToRequest_Click(sender, e);
        }

        /// <summary>
        /// Andrew Cromwell
        /// Created: 2023/04/07
        /// 
        /// Passes the request to the manager to be saved if the user is done with it.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSendRequest_Click(object sender, RoutedEventArgs e)
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
                        _existingCreateShelterRequestPage = null;
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

        /// <summary>
        /// Brian Collum
        /// Created: 2023/04/21
        /// 
        /// Apply the user's selected search filter to the Inventory UI
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// 
        /// </remarks>
        private void ApplyFilters()
        {
            // Update item name filter
            if (_itemNameFilter == null || _itemNameFilter.Equals("Filter by Name"))
            {
                _itemNameFilter = "";
            }
            // Reset filter list
            _shelterFilteredInventoryItemVMList = new List<ShelterInventoryItemVM>();
            try
            {
                foreach (ShelterInventoryItemVM item in _shelterInventoryItemVMList)
                {
                    // Match name search string
                    if (item.ItemId.IndexOf(_itemNameFilter, 0, StringComparison.OrdinalIgnoreCase) != -1)
                    {
                        _shelterFilteredInventoryItemVMList.Add(item);  // Populate the filtered list
                    }
                }
            }
            catch (Exception ex)
            {
                PromptWindow.ShowPrompt("Error", "Failed to apply item name filter, " + ex.InnerException.Message, ButtonMode.Ok);
            }
            // Refresh the actual view
            datShelterInventory.ItemsSource = _shelterFilteredInventoryItemVMList;  //RefreshShelterInventoryList() doesn't exist in this class so set datagrid here
        }

        /// <summary>
        /// Brian Collum
        /// Created: 2023/04/21
        /// 
        /// Clear the placeholder text from the search by name textbox when the user selects it
        /// </summary>
        /// <remarks>
        /// </remarks>
        private void txtSearchFilter_GotFocus(object sender, RoutedEventArgs e)
        {
            // Clear search box on select
            if (txtSearchFilter.Text == "" || txtSearchFilter.Text == "Filter by Name")
            {
                txtSearchFilter.Text = "";
            }
        }

        /// <summary>
        /// Brian Collum
        /// Created: 2023/04/21
        /// 
        /// Restore the placeholder text to the search by name textbox when the user deselects it
        /// </summary>
        /// <remarks>
        /// </remarks>
        private void txtSearchFilter_LostFocus(object sender, RoutedEventArgs e)
        {
            // Reset placeholder text when text box is empty and user deselects
            if (txtSearchFilter.Text == "")
            {
                txtSearchFilter.Text = "Filter by Name";
            }
        }

        /// <summary>
        /// Brian Collum
        /// Created: 2023/04/21
        /// 
        /// Apply search by name filtering when user presses enter after entering their search query
        /// </summary>
        /// <remarks>
        /// </remarks>
        private void txtSearchFilter_KeyDown(object sender, KeyEventArgs e)
        {
            // When user hits Return after entering text
            if (e.Key == Key.Return)
            {
                try
                {
                    _itemNameFilter = txtSearchFilter.Text;
                    ApplyFilters();
                }
                catch (Exception ex)
                {
                    PromptWindow.ShowPrompt("Error", "Failed to apply item name filter, " + ex.InnerException.Message, ButtonMode.Ok);
                }
            }
        }
    }
}
