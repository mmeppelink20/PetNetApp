/// <summary>
/// Zaid Rachman
/// Created: 2023/03/19
/// 
/// Page that contains details of the selected shelter item
/// 
/// </summary>
///
/// <remarks>
/// Oleksiy Fedchuk
/// Updated: 2023/04/20
/// 
/// Final QA
/// </remarks>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using LogicLayer;

namespace WpfPresentation.Management.Inventory
{
    /// <summary>
    /// Interaction logic for ViewEditShelterInventoryItem.xaml
    /// </summary>
    public partial class ViewEditShelterInventoryItem : Page
    {
        ShelterInventoryItemVM _shelterInventoryItemVM = new ShelterInventoryItemVM();
        Item _item = new Item();
        MasterManager _masterManager = MasterManager.GetMasterManager();
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
        /// <param name="shelterInventoryItemVM"></param>
        public ViewEditShelterInventoryItem(ShelterInventoryItemVM shelterInventoryItemVM)
        {
            _shelterInventoryItemVM = shelterInventoryItemVM;
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

            //inserting page information
            try
            {
                _item = _masterManager.ItemManager.RetrieveItemByItemId(_shelterInventoryItemVM.ItemId);
            }
            catch (Exception)
            {

                PromptWindow.ShowPrompt("Missing Data", "Failed to retrieve item data.");
                return;
            }

            lblItemName.Content = "Item Name: " + _shelterInventoryItemVM.ItemId;
            txtQuantity.Text = _shelterInventoryItemVM.Quantity.ToString();
            txtLowThreshold.Text = _shelterInventoryItemVM.LowInventoryThreshold.ToString();
            txtHighThreshold.Text = _shelterInventoryItemVM.HighInventoryThreshold.ToString();
            txtUseStatistic.Text = _shelterInventoryItemVM.UseStatistic.ToString();

            lblOverLowStock.Visibility = Visibility.Hidden;
            if(_shelterInventoryItemVM.Quantity < _shelterInventoryItemVM.LowInventoryThreshold)
            {
                lblOverLowStock.Visibility = Visibility.Visible;
                lblOverLowStock.Content = "Low Quantity!";
            }

            if (_shelterInventoryItemVM.Quantity > _shelterInventoryItemVM.HighInventoryThreshold)
            {
                lblOverLowStock.Visibility = Visibility.Visible;
                lblOverLowStock.Content = "Overstocked!";
            }

            if (_shelterInventoryItemVM.CustomFlag != null)
            {
                txtCustomFlags.Text = _shelterInventoryItemVM.CustomFlag.ToString();
            }
            else
            {
                txtCustomFlags.Text = "";
            }
            if (_shelterInventoryItemVM.Urgent)
            {
                cbUrgent.IsChecked = true;
            }
            if (_shelterInventoryItemVM.Processing)
            {
                cbProcessing.IsChecked = true;
            }
            if (_shelterInventoryItemVM.DoNotOrder)
            {
                cbDoNotOrder.IsChecked = true;
            }
            if (_shelterInventoryItemVM.InTransit)
            {
                cbInTransit.IsChecked = true;
            }
            lblLastUpdated.Content = "Last Updated: " + _shelterInventoryItemVM.LastUpdated.ToShortDateString();




            lblLocation.Content = "Shelter: " + _shelterInventoryItemVM.ShelterName;
            
            if(_item.CategoryId == null)
            {
                lblCategory.Content = "";
            }
            else
            {
                 lblCategory.Content = UpdateCategory(_item.CategoryId);
            }
           


        }
        /// <summary>
        /// Zaid Rachman
        /// Created: 2023/03/19
        /// 
        /// This method takes in the category list from the Items object and returns them into a displayable string.
        /// </summary>
        /// 
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/20
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="categories"></param>
        /// <returns></returns>
        public string UpdateCategory(List<string> categories)
        {
            string categoryString = "";
            if (categories != null)
            {
                //Formating
                for (int i = 0; i < categories.Count; i++)
                {
                    categoryString += " " + categories[i];

                    if (i == categories.Count - 2)
                    {
                        if (categories.Count > 2)
                        {
                            categoryString += ",";
                        }
                        categoryString += " and";
                    }
                    else if (i < categories.Count - 2)
                    {
                        categoryString += ",";
                    }
                }
            }
            
            
            return categoryString; //Using the CustomFlag property as a way to show all flags
        }


        /// <summary>
        /// Zaid Rachman
        /// Created: 2023/03/19
        /// 
        /// Saves changes made to page
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
        private void btnSaveChanges_Click(object sender, RoutedEventArgs e)
        {
            //Validation correct input

            decimal useStatistic;

            int quantity;
            int lowthreshold;
            int highthreshold;

            try
            {
                useStatistic = decimal.Parse(txtUseStatistic.Text);
                if(useStatistic < 0)
                {
                    PromptWindow.ShowPrompt("Use Statistic Input Error", "An Error has occured. Use Statistic can not be below 0. Please Try again");
                    txtLowThreshold.Focus();
                    return;
                }
            }
            catch (Exception)
            {
                PromptWindow.ShowPrompt("Use Statistic Input Error", "An Error has occured. Use Statistic only allows decimals and integers. Please Try again");
                txtUseStatistic.Focus();
                return;
            }
            try
            {
                quantity = int.Parse(txtQuantity.Text);

                if(quantity < 0)
                {
                    PromptWindow.ShowPrompt("Quantity Input Error", "An Error has occured. Quantity can not be below 0. Please Try again");
                    txtQuantity.Focus();
                    return;
                }
            }
            catch (Exception)
            {
                PromptWindow.ShowPrompt("Quantity Input Error", "An Error has occured. Quantity only allows integers as input. Please Try again");
                txtQuantity.Focus();
                return;

            }
            try
            {
                lowthreshold = int.Parse(txtLowThreshold.Text);
                if(lowthreshold < 0)
                {
                    PromptWindow.ShowPrompt("Low Threshold Input Error", "An Error has occured. Low Threshold can not be below 0. Please Try again");
                    txtLowThreshold.Focus();
                    return;
                }
            }
            catch (Exception)
            {
                PromptWindow.ShowPrompt("Low Threshold Input Error", "An Error has occured. Low threshold only allows integers as input. Please Try again");
                txtLowThreshold.Focus();
                return;

            }

            try
            {
                highthreshold = int.Parse(txtHighThreshold.Text);
                if(highthreshold < 0)
                {
                    PromptWindow.ShowPrompt("High Threshold Input Error", "An Error has occured. High Threshold can not be below 0. Please Try again");
                    txtLowThreshold.Focus();
                    return;
                }
            }
            catch (Exception)
            {
                PromptWindow.ShowPrompt("High Threshold Input Error", "An Error has occured. High Threshold only allows integers as input. Please Try again");
                txtHighThreshold.Focus();
                return;

            }



            //Validation if null
            if (txtQuantity.Text == "")
            {
                PromptWindow.ShowPrompt("Date Error", "Please enter an item quantity");
                txtQuantity.Focus();
                return;
            }
            if (txtHighThreshold.Text == "")
            {
                PromptWindow.ShowPrompt("Date Error", "Please enter a high threshold");
                txtHighThreshold.Focus();
                return;
            }
            if (txtLowThreshold.Text == "")
            {
                PromptWindow.ShowPrompt("Date Error", "Please enter a low threshold");
                txtLowThreshold.Focus();
                return;
            }
            if (txtUseStatistic.Text == "")
            {
                PromptWindow.ShowPrompt("Date Error", "Please enter a use statistic");
                txtUseStatistic.Focus();
                return;
            }
            

            ShelterInventoryItemVM updatedShelterItemVM = new ShelterInventoryItemVM
            {

                ShelterId = _shelterInventoryItemVM.ShelterId,
                ItemId = _shelterInventoryItemVM.ItemId,
                Quantity = int.Parse(txtQuantity.Text),
                UseStatistic = decimal.Parse(txtUseStatistic.Text),
                LastUpdated = DateTime.Now, //Capture the current Date the change was made
                LowInventoryThreshold = int.Parse(txtLowThreshold.Text),
                HighInventoryThreshold = int.Parse(txtHighThreshold.Text),
                InTransit = cbInTransit.IsChecked.Value,
                Urgent = cbUrgent.IsChecked.Value,
                Processing = cbProcessing.IsChecked.Value,
                DoNotOrder = cbDoNotOrder.IsChecked.Value,
                CustomFlag = txtCustomFlags.Text,
                ShelterName = _shelterInventoryItemVM.ShelterName//Should not be able to be changed on this page


            };

            try
            {
                _masterManager.ShelterInventoryItemManager.EditShelterInventoryItem(_shelterInventoryItemVM, updatedShelterItemVM);
            }
            catch (Exception)
            {

                PromptWindow.ShowPrompt("Edit Failed", "Failed to update inventory item");
                return;
            }


            NavigationService.Navigate(new ViewShelterInventoryPage());
        }
        /// <summary>
        /// Zaid Rachman
        /// Created: 2023/03/19
        /// 
        /// Take user back to ViewShelterInventory page
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
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ViewShelterInventoryPage());
        }

        /// <summary>
        /// Zaid Rachman
        /// Created: 2023/03/19
        /// 
        /// Validation tool: Prevents User from being able to type anything other than numers 0-9
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
        private void txtQuantity_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }


        /// <summary>
        /// Zaid Rachman
        /// Created: 2023/03/19
        /// 
        /// Validation tool: Prevents User from being able to type anything other than numers 0-9
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
        private void txtLowThreshold_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        /// <summary>
        /// Zaid Rachman
        /// Created: 2023/03/19
        /// 
        /// Validation tool: Prevents User from being able to type anything other than numers 0-9
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
        private void txtHighThreshold_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}




