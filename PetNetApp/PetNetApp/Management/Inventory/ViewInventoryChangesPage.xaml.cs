/// <summary>
/// Your Name
/// Created: 2023/02/28
/// 
/// Actual summary of the class if needed, example is for DTO
/// Class for the creation of User Objects with set data fields
/// </summary>

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
    /// Interaction logic for ViewInventoryChangesPage.xaml
    /// </summary>
    public partial class ViewInventoryChangesPage : Page
    {
        private static ViewInventoryChangesPage _existingViewInventoryChangesPage = null;

        private MasterManager _manager;
        private List<ShelterItemTransactionVM> _shelterItemTransactions;

        public ViewInventoryChangesPage(MasterManager manager)
        {
            InitializeComponent();
            _manager = manager;
        }

        public static ViewInventoryChangesPage GetViewInventoryChangesPage(MasterManager manager)
        {
            if (_existingViewInventoryChangesPage == null)
            {
                _existingViewInventoryChangesPage = new ViewInventoryChangesPage(manager);
            }
            return _existingViewInventoryChangesPage;
        }

        /// <summary>
        /// Andrew Cromwell
        /// Created: 2023/03/01
        /// 
        /// Formats a ShelterItemTransaction to a lable to display on the page.
        /// </summary>
        /// <rmarks>
        /// Nathan Zumsande
        /// Updated : 2023/04/21
        /// Fixed the diplay for the date time so it displayed the actual time and not
        /// just 12:00:00
        /// </rmarks>
        /// <param name="transaction">The ShelterItemTransactionVM to be displayed</param>
        /// <param name="index">int that helps determin which background color to use so that it alternates</param>
        private void DisplayInventoryChangeRecord(ShelterItemTransactionVM transaction, int index)
        {
            Label lblTransaction = new Label();

            string transactionTag;
            transactionTag = transaction.GivenName + " " + transaction.FamilyName + " ";
            if (transaction.InventoryChangeReasonId.ToLower() == "check-in")
            {
                transactionTag = transactionTag + "checked-in ";
            }
            else
            {
                transactionTag = transactionTag + "checked-out ";
            }
            if (Math.Abs(transaction.QuantityIncrement) == 1)
            {
                transactionTag = transactionTag + Math.Abs(transaction.QuantityIncrement) + " unit ";
            }
            else
            {
                transactionTag = transactionTag + Math.Abs(transaction.QuantityIncrement) + " units ";
            }
            transactionTag = transactionTag + "of " + transaction.ItemId + " on " + transaction.DateChanged  + ".";
            lblTransaction.Content = transactionTag;

            var bc = new BrushConverter();
            if (index % 2 == 0)
            {
                lblTransaction.Background = (Brush)bc.ConvertFrom("#3D8361");
            }
            else
            {
                lblTransaction.Background = (Brush)bc.ConvertFrom("#D6CDA4");
            }

            lblTransaction.VerticalContentAlignment = VerticalAlignment.Center;
            lblTransaction.FontSize = 20;
            lblTransaction.Foreground = (Brush)bc.ConvertFrom("#000000");
            lblTransaction.Padding = new Thickness(50, 0, 0, 0);
            lblTransaction.Height = 50;

            stpInventroyItemTransactionList.Children.Add(lblTransaction);
        }

        /// <summary>
        /// Your Name
        /// Created: 2023/03/28
        /// 
        /// Retrieves ShelterItemTransactionVMs and sends each to DisplayInventoryChangeRecord.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            stpInventroyItemTransactionList.Children.Clear();
            try
            {
                _shelterItemTransactions = _manager.ShelterItemTransactionManager.RetrieveInventoryTransactionByShelterId((int)_manager.User.ShelterId);
            }catch(Exception ex)
            {
                PromptWindow.ShowPrompt("Error", ex.Message + "\n" + ex.InnerException.Message, ButtonMode.Ok);
            }
            int index = 0;
            if (_shelterItemTransactions.Count != 0 && _shelterItemTransactions != null)
            {
                foreach (ShelterItemTransactionVM transaction in _shelterItemTransactions)
                {
                    DisplayInventoryChangeRecord(transaction, index);
                    index++;
                }
            }
            else
            {
                Label lblNoTransactions = new Label();
                lblNoTransactions.VerticalContentAlignment = VerticalAlignment.Center;
                lblNoTransactions.HorizontalContentAlignment = HorizontalAlignment.Center;
                lblNoTransactions.FontSize = 20;
                lblNoTransactions.Content = "no changes have been made in the past month";
                stpInventroyItemTransactionList.Children.Add(lblNoTransactions);
            }
        }
    }
}
