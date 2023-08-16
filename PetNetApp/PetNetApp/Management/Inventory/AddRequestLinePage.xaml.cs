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
using LogicLayer;
using DataObjects;

namespace WpfPresentation.Management.Inventory
{
    /// <summary>
    /// Interaction logic for AddRequestLinePage.xaml
    /// </summary>
    public partial class AddRequestLinePage : Page
    {
        MasterManager _manager = null;
        RequestVM _request = null;
        ShelterInventoryItem _item = null;

        public AddRequestLinePage(MasterManager manager, RequestVM request, ShelterInventoryItem item)
        {
            InitializeComponent();
            _manager = manager;
            _request = request;
            _item = item;
            lblItem.Content = _item.ItemId;
        }

        /// <summary>
        /// Andrew Cromwell
        /// Created: 2023/04/05
        /// 
        /// Prevents non-numbers from being typed into the text box
        /// </summary>
        /// <remarks>
        /// learned how to do this from Kailash Chandra Behera, https://www.kailashsblogs.com/2022/07/wpf-textbox-numeric-only.html
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtQuantity_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        /// <summary>
        /// Andrew Cromwell
        /// Created: 2023/04/05
        /// 
        /// Prevents non-numbers from being pasted into the text box
        /// </summary>
        /// <remarks>
        /// learned how to do this from Kailash Chandra Behera, https://www.kailashsblogs.com/2022/07/wpf-textbox-numeric-only.html
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtQuantity_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(String)))
            {
                string text = (string)e.DataObject.GetData(typeof(String));
                Regex regex = new Regex("[^0-9]+");
                if (regex.IsMatch(text) == true)
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }


        /// <summary>
        /// Andrew Cromwell
        /// Created: 2023/04/05
        /// 
        /// Exits the current screen, sends user back to create shelter request page 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(null);
        }

        /// <summary>
        /// Andrew Cromwell
        /// Created: 2023/04/05
        /// 
        /// Verifies the information entered. If valid, it is added to the request
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (txtQuantity.Text == "" || txtQuantity.Text == null)
            {
                PromptWindow.ShowPrompt("Missing Quantity", "You need to specify the quantity of the item you are requesting.", ButtonMode.Ok);
                txtQuantity.Focus();
                return;
            }

            int quantity;
            try
            {
                quantity = Int32.Parse(txtQuantity.Text);
            }catch(Exception ex)
            {
                PromptWindow.ShowPrompt("Invalid Quantity", "You must only include numbers in the quantity.", ButtonMode.Ok);
                txtQuantity.Focus();
                return;
            }

            if(quantity <= 0)
            {
                PromptWindow.ShowPrompt("Invalid Quantity", "Quantity requested can not be zero or less.", ButtonMode.Ok);
                txtQuantity.Focus();
                return;
            }

            if (quantity >= _item.Quantity)
            {
                PromptWindow.ShowPrompt("Invalid Quantity", "You can not request a higher quantity than the shelter has.", ButtonMode.Ok);
                txtQuantity.Focus();
                return;
            }

            PromptSelection selection = PromptWindow.ShowPrompt("Verify Input", "Verify that the information you entered is corect", ButtonMode.SaveCancel);
            if (selection == PromptSelection.Cancel)
            {
                return;
            }

            RequestResourceLine line = new RequestResourceLine();
            line.ItemId = _item.ItemId;
            line.QuantityRequested = quantity;
            line.Notes = txtNotes.Text;

            _request.RequestLines.Add(line);

            PromptWindow.ShowPrompt("Item Added", "The item was added to your request.", ButtonMode.Ok);
            NavigationService.Navigate(null);
        }

        /// <summary>
        /// Andrew Cromwell
        /// Created: 2023/04/05
        /// 
        /// Exits the current screen, sends user back to create shelter request page 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(null);
        }

        
    }
}
