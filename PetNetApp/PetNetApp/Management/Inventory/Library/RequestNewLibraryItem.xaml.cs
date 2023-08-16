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

namespace WpfPresentation.Management.Inventory.Library
{
    /// <summary>
    /// Andrew Cromwell
    /// 2023/04/19
    /// 
    /// Interaction logic for RequestNewLibraryItem.xaml
    /// </summary>
    public partial class RequestNewLibraryItem : Page
    {
        private MasterManager _manager;


        /// <summary>
        /// Andrew Cromwell
        /// Created: 2023/04/19
        /// 
        /// Constructor of RequestNewLibraryItem
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/21
        /// final QA
        /// </remarks>
        /// <param name="manager"></param>
        public RequestNewLibraryItem(MasterManager manager)
        {
            InitializeComponent();
            _manager = manager;
        }


        /// <summary>
        /// Andrew Cromwell
        /// Created: 2023/04/20
        /// 
        /// Method to add a ResourceAddRequest
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/21
        /// final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMakeRequest_Click(object sender, RoutedEventArgs e)
        {
            if(txtname.Text == "" || txtname.Text == null)
            {
                PromptWindow.ShowPrompt("Bad Input", "You must enter a Name for your Request.", ButtonMode.Ok);
                txtname.Focus();
                return;
            }

            if (txtname.Text.Length > 100)
            {
                PromptWindow.ShowPrompt("Bad Input", "The name must not be longer than 100 letters.", ButtonMode.Ok);
                txtname.Focus();
                return;
            }

            if (txtnotes.Text == "" || txtnotes.Text == null)
            {
                PromptWindow.ShowPrompt("Bad Input", "You need to make a note about why you need this resource.", ButtonMode.Ok);
                txtnotes.Focus();
                return;
            }

            if (txtnotes.Text.Length > 2500)
            {
                PromptWindow.ShowPrompt("Bad Input", "Your notes can not be longer than 2,500 letters.", ButtonMode.Ok);
                txtnotes.Focus();
                return;
            }

            PromptSelection selection = PromptWindow.ShowPrompt("Send Request", "Are you sure you are ready to send your Request."
                , ButtonMode.YesNo);
            if(selection == PromptSelection.Yes)
            {
                bool success = false;
                ResourceAddRequest resourceAddRequest = new ResourceAddRequest();
                resourceAddRequest.ShelterId = _manager.User.ShelterId.Value;
                resourceAddRequest.UsersId = _manager.User.UsersId;
                resourceAddRequest.Title = txtname.Text;
                resourceAddRequest.Note = txtnotes.Text;
                resourceAddRequest.Active = true;

                try
                {
                    success = _manager.ResourceAddRequestManager.AddResourceAddRequest(resourceAddRequest);
                    if (success)
                    {
                        PromptWindow.ShowPrompt("Success", "Your request for a new item to be added to the library has been made", ButtonMode.Ok);
                        NavigationService.Navigate(null);
                    }
                    else
                    {
                        PromptWindow.ShowPrompt("Error", "Something went wrong. Your request was not submited.", ButtonMode.Ok);
                    }
                }
                catch(Exception ex)
                {
                    PromptWindow.ShowPrompt("Error", ex.Message + "\n\n" + ex.InnerException.Message, ButtonMode.Ok);
                }
            }
        }


        /// <summary>
        /// Andrew Cromwell
        /// Created: 2023/04/19
        /// 
        /// Method to cancel the Request Library Item
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/21
        /// final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            PromptSelection selection = PromptWindow.ShowPrompt("Cancel?", "Are you sure you wish to cancel? Changes will not be saved.", ButtonMode.YesNo);
            if (selection == PromptSelection.Yes)
            {
                NavigationService.Navigate(null);
            }
        }
    }
}
