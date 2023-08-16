/// <summary>
/// Nathan Zumsande
/// Created: 2023/04/13
/// Presentation layer methods for the EditInventoryQuantityPage UI
/// </summary>
/// <remarks>
/// Updater Name
/// Updated: yyyy/mm/dd
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

namespace WpfPresentation.Management.Inventory
{
    /// <summary>
    /// Interaction logic for EditInventoryQuantityPage.xaml
    /// </summary>
    public partial class EditInventoryQuantityPage : Page
    {
        MasterManager _manager = null;
        ShelterInventoryItemVM _item = null;
        bool _checkin = false;
        CheckInCheckOutPage _page = null;


        /// <summary>
        /// Nathan Zumsande
        /// Created: 2023/04/13
        /// Initalizes the EditInventoryQuantityPage
        /// </summary>
        /// <param name="manager"></param>
        /// <param name="item"></param>
        /// <param name="checkin"></param>
        /// <param name="page"></param>
        public EditInventoryQuantityPage(MasterManager manager, ShelterInventoryItemVM item, bool checkin, CheckInCheckOutPage page)
        {
            _manager = manager;
            _item = item;
            _checkin = checkin;
            _page = page;
            InitializeComponent();
        }

        /// <summary>
        /// Nathan Zumsande
        /// Created: 2023/04/13
        /// Method to run when the page loads
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditInventoryQuantity_Loaded(object sender, RoutedEventArgs e)
        {
            lblItemName.Content = "Item Name: " + _item.ItemId;
            lblItemTotal.Content = "Item Total Amount: " + _item.Quantity.ToString();
            if (_checkin)
            {
                lbltitle.Content = "Check In";
                lblAmount.Content = "Amount to Add:";
                btnEditQuantity.Content = "Add Amount";
            }
            else
            {
                lbltitle.Content = "Check Out";
                lblAmount.Content = "Amount to Remove:";
                btnEditQuantity.Content = "Remove Amount";
            }
        }

        /// <summary>
        /// Nathan Zumsande
        /// Created: 2023/04/13
        /// Button to edit the quantity of the item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditQuantity_Click(object sender, RoutedEventArgs e)
        {
            if (udAmount.Value <= 0 || udAmount.Value == null)
            {
                PromptWindow.ShowPrompt("Bad Input", "You must enter an amount.", ButtonMode.Ok);
                udAmount.Value = 0;
                udAmount.Focus();
                return;
            }
            int amount = (int)udAmount.Value;
            bool result = false;
            ShelterInventoryItemVM newItem = new ShelterInventoryItemVM
            {
                ShelterId = _item.ShelterId,
                ItemId = _item.ItemId,
                UseStatistic = _item.UseStatistic,
                LastUpdated = DateTime.Now,
                LowInventoryThreshold = _item.LowInventoryThreshold,
                HighInventoryThreshold = _item.HighInventoryThreshold,
                InTransit = _item.InTransit,
                Urgent = _item.Urgent,
                Processing = _item.Processing,
                DoNotOrder = _item.DoNotOrder,
                CustomFlag = _item.CustomFlag
            };
            ShelterItemTransaction itemTransaction = new ShelterItemTransaction
            {
                ShelterId = _item.ShelterId,
                ItemId = _item.ItemId,
                ChangedByUsersId = _manager.User.UsersId,
                QuantityIncrement = amount
            };
            int total = 0;
            if (!_checkin && amount > _item.Quantity)
            {
                PromptWindow.ShowPrompt("Bad Input", "You amount to remove cannot be greater than the total quantity.", ButtonMode.Ok);
                udAmount.Value = 0;
                udAmount.Focus();
                return;
            }
            try
            {
                if (_checkin)
                {
                    total = amount + _item.Quantity;
                    newItem.Quantity = total;
                    if (_manager.ShelterInventoryItemManager.EditShelterInventoryItem(_item, newItem))
                    {
                        itemTransaction.InventoryChangeReasonId = "Check-in";
                        result = _manager.ShelterItemTransactionManager.AddItemTransaction(itemTransaction);
                    }
                }
                else
                {
                    total = _item.Quantity - amount;
                    newItem.Quantity = total;
                    if (_manager.ShelterInventoryItemManager.EditShelterInventoryItem(_item, newItem))
                    {
                        itemTransaction.InventoryChangeReasonId = "Check-out";
                        result = _manager.ShelterItemTransactionManager.AddItemTransaction(itemTransaction);
                    }
                }
                if (result)
                {
                    PromptWindow.ShowPrompt("Quantity Updated", _item.ItemId + " was " + (_checkin ? "Increased" : "Decreased") + " by " + amount + ".", ButtonMode.Ok) ;
                    _page.UpdatePage();
                    NavigationService.Navigate(null);
                }
                else
                {
                    throw new ApplicationException("Item transaction failed");
                }
            }
            catch (Exception ex)
            {
                PromptWindow.ShowPrompt("Error", ex.Message, ButtonMode.Ok);
                _page.UpdatePage();
                NavigationService.Navigate(null);
            }
        }

        /// <summary>
        /// Nathan Zumsande
        /// Created: 2023/04/13
        /// Button to cancel the edit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            PromptSelection selection = PromptWindow.ShowPrompt("Cancel?", "Are you sure you wish to cancel? Changes will not be saved.", ButtonMode.YesNo);
            if (selection == PromptSelection.Yes)
            {
                _page.UpdatePage();
                NavigationService.Navigate(null);
            }
        }
    }
}
