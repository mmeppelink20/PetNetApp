/*
 * Created By Asa Armstrong
 * Created on 2023/04/06
 */

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
using WpfPresentation.UserControls;

namespace WpfPresentation.Fundraising
{
    /// <summary>
    /// Interaction logic for EnterDonation.xaml
    /// </summary>
    public partial class EnterDonation : Page
    {
        private MasterManager _masterManager = MasterManager.GetMasterManager();
        private List<InKind> _inKinds = new List<InKind>();
        private Users _user = null;

        public Donation Donation
        {
            get { return (Donation)GetValue(DonationProperty); }
            set { SetValue(DonationProperty, value); }
        }
        public static readonly DependencyProperty DonationProperty =
            DependencyProperty.Register("Donation", typeof(Donation), typeof(EnterDonation),
                new PropertyMetadata(null));

        /// <summary>
        /// Author: Asa Armstrong
        /// Date: 2023/04/23
        /// Description: Constructor for page EnterDonation
        /// </summary>
        /// <param name="fundraisingEventId">If the Donation is for a Fundraising Event</param>
        /// <param name="scheduledDonationId">If the Donation is for a Sceduled Donation</param>
        public EnterDonation(int? fundraisingEventId = null, int? scheduledDonationId = null)
        {
            Donation = new Donation();
            Donation.PaymentMethod = "Card";
            Donation.FundraisingEventId = fundraisingEventId;
            Donation.ScheduledDonationId = scheduledDonationId;
            DataContext = this;
            InitializeComponent();
        }

        /// <summary>
        /// Author: Asa Armstrong
        /// Date: 2023/04/23
        /// Description: Searches the DB for the email to connect the donation to a user.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_SearchUserByEmail_Click(object sender, RoutedEventArgs e)
        {
            SearchUserByEmail();
        }

        /// <summary>
        /// Author: Asa Armstrong
        /// Date: 2023/04/23
        /// Description: Searches the DB for the email to connect the donation to a user. Deletes the user from the 
        /// donation if searched with an empty string.
        /// </summary>
        private void SearchUserByEmail()
        {
            if (txt_Email.Text == null || txt_Email.Text.Length == 0)
            {
                if (_user == null || _user.UsersId.Equals(0))
                {
                    PromptWindow.ShowPrompt("Error", "Email Cannot be empty.");
                }
                else // erases the user if searched again with a blank string
                {
                    _user = null;
                    PromptWindow.ShowPrompt("User Removed", "The User was removed from this donation.");
                }
            }
            else if (!txt_Email.Text.Equals(null) && txt_Email.Text.Length > 0)
            {
                try
                {
                    Users user = new Users();
                    user = _masterManager.UsersManager.RetrieveUserObjectByEmail(txt_Email.Text);
                    if (!(user == null) && !user.UsersId.Equals(0))
                    {
                        _user = new Users();
                        _user = user;
                        Donation.UserId = _user.UsersId;
                        txt_GivenName.Text = _user.GivenName;
                        txt_FamilyName.Text = _user.FamilyName;
                        txt_PhoneNumber.Text = _user.Phone;
                        Donation.GivenName = _user.GivenName;
                        Donation.FamilyName = _user.FamilyName;
                        Donation.Phone = _user.Phone;
                    }
                    else
                    {
                        Donation.UserId = null;
                    }
                }
                catch (Exception ex)
                {
                    Donation.UserId = null;
                    PromptWindow.ShowPrompt("Error", "No match found. " + ex.Message);
                }
            }
        }

        /// <summary>
        /// Author: Asa Armstrong
        /// Date: 2023/04/23
        /// Description: Adds the donation and any In-Kinds to the DB. Then shows a receipt of the donation 
        /// if successful.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Submit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Donation.Amount == 0 || Donation.Amount == null)
                {
                    Donation.Amount = null;
                    Donation.PaymentMethod = null;
                }
                if (_inKinds.Where(ik => ik.Recieved == true).Count() > 0)
                {
                    Donation.HasInKindDonation = true;
                }
                if((Donation.GivenName == null || Donation.GivenName.Length == 0) &&
                    (Donation.FamilyName == null || Donation.FamilyName.Length == 0) &&
                    Donation.UserId == null)
                {
                    Donation.Anonymous = true;
                }

                if (Donation.HasInKindDonation || Donation.Amount != null)
                {
                    foreach (InKind inKind in _inKinds)
                    {
                        if ((inKind.Description == null || inKind.Description.Length == 0 || inKind.Description.Length > 225) && inKind.Recieved)
                        {
                            throw new ApplicationException("In-Kind items must have a name.");
                        }
                    }
                        Donation.ShelterId = (int)_masterManager.User.ShelterId;
                    int donationId = _masterManager.DonationManager.AddDonation(Donation);
                    foreach (InKind inKind in _inKinds)
                    {
                        if (inKind.Recieved)
                        {
                            inKind.DonationId = donationId;
                            _masterManager.DonationManager.AddInKind(inKind);
                        }
                    }
                    var viewDonationReceiptWindow = new WpfPresentation.Fundraising.ViewDonationReceiptWindow(donationId);
                    viewDonationReceiptWindow.Owner = Window.GetWindow(this);
                    viewDonationReceiptWindow.ShowDialog();
                    NavigateBackToPreviousPage();
                }
                else
                {
                    PromptWindow.ShowPrompt("Error", "A Donation must have at least an Amount or an In-Kind item.");
                }
            }
            catch (Exception ex)
            {
                PromptWindow.ShowPrompt("Error", "Something went wrong trying to submit. " + ex.Message);
            }
        }

        /// <summary>
        /// Author: Asa Armstrong
        /// Date: 2023/04/23
        /// Description: Cancels and returns to previous page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            if (PromptSelection.Yes == PromptWindow.ShowPrompt("Cancel?", "Cancel and Return?", ButtonMode.YesNo))
            {
                NavigateBackToPreviousPage();
            }
        }

        private void NavigateBackToPreviousPage()
        {
            if (Donation.FundraisingEventId != null)
            {
                NavigationService.Navigate(WpfPresentation.Fundraising.ViewFundraisingEventsPage.GetViewFundraisingEvents());
            }
            else if (Donation.ScheduledDonationId != null)
            {
                // navigate to scheduled donation page
                NavigationService.Navigate(ViewDonationsPage.ExistingDonationPage);
            }
            else
            {
                NavigationService.Navigate(ViewDonationsPage.ExistingDonationPage);
            }
        }

        /// <summary>
        /// Author: Asa Armstrong
        /// Date: 2023/04/23
        /// Description: Searches the DB for the email to connect the donation to a user.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_Email_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Return)
            {
                SearchUserByEmail();
            }
        }

        /// <summary>
        /// Author: Asa Armstrong
        /// Date: 2023/04/23
        /// Description: Adds another In-Kind Donation User Control to add more In-Kinds to the donation.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_AddAnotherInKind_Click(object sender, RoutedEventArgs e)
        {
            btn_AddAnotherInKind.Content = "Add Another In-Kind";
            int fakeInKindId = (int)DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            _inKinds.Add(new InKind() { InKindId = fakeInKindId });
            stk_InKindDonation.Children.Add(new InKindDonationUserControl(_inKinds.First(i => i.InKindId == fakeInKindId)));
        }

        /// <summary>
        /// Author: Asa Armstrong
        /// Date: 2023/04/23
        /// Description: Validates the donation amount.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_Amount_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            DecimalNumberValidationTextBox(sender, e);
        }

        /// <summary>
        /// Barry Mikulas
        /// Created: 2023/03/09
        /// 
        /// When a user types in a textbox this will prevent them from entering anything but a decimal with 2 decimal places
        /// </summary>
        ///
        /// <remarks>
        /// Asa Armstrong
        /// Updated: 2023/04/20
        /// example: Made it so you can only enter up to 5 digits before the decimal and only up to 2 digits after the decimal
        /// </remarks>
        private void DecimalNumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"^([0-9]{0,5})(\.[0-9]{0,2})?$");
            string amount = txt_Amount.Text.Insert(txt_Amount.CaretIndex, e.Text);

            if (!regex.IsMatch(amount))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Barry Mikulas
        /// Created: 2023/03/09
        /// 
        /// When a user types in a textbox this will prevent them from entering anything but a digit
        /// this is based on https://stackoverflow.com/a/12721673
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        /// <summary>
        /// Author: Asa Armstrong
        /// Date: 2023/04/23
        /// Description: Toggles the Card fields for the donation if it's a Card donation or not.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbx_Method_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Donation.PaymentMethod == "Card")
            {
                lbl_CardNumber.Visibility = Visibility.Visible;
                lbl_CardholdersName.Visibility = Visibility.Visible;
                lbl_CVV.Visibility = Visibility.Visible;
                lbl_ExpDate.Visibility = Visibility.Visible;

                txt_CardNumber.Visibility = Visibility.Visible;
                txt_CardholdersName.Visibility = Visibility.Visible;
                txt_CVV.Visibility = Visibility.Visible;
                txt_ExpDate.Visibility = Visibility.Visible;
            }
            else
            {
                lbl_CardNumber.Visibility = Visibility.Hidden;
                lbl_CardholdersName.Visibility = Visibility.Hidden;
                lbl_CVV.Visibility = Visibility.Hidden;
                lbl_ExpDate.Visibility = Visibility.Hidden;

                txt_CardNumber.Visibility = Visibility.Hidden;
                txt_CardholdersName.Visibility = Visibility.Hidden;
                txt_CVV.Visibility = Visibility.Hidden;
                txt_ExpDate.Visibility = Visibility.Hidden;
            }
        }

        /// <summary>
        /// Author: Asa Armstrong
        /// Date: 2023/04/23
        /// Description: Makes the ExpDate have to be in the correct format.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_ExpDate_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"^([0-9]{0,2})(\/[0-9]{0,2})?$");
            string amount = txt_ExpDate.Text.Insert(txt_ExpDate.CaretIndex, e.Text);

            if (!regex.IsMatch(amount))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Author: Asa Armstrong
        /// Date: 2023/04/23
        /// Description: Only allows numbers in the CVV field.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_CVV_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            NumberValidationTextBox(sender, e);
        }

        /// <summary>
        /// Author: Asa Armstrong
        /// Date: 2023/04/23
        /// Description: Only allows numbers in the CardNumber field.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_CardNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            NumberValidationTextBox(sender, e);
        }

        /// <summary>
        /// Author: Asa Armstrong
        /// Date: 2023/04/23
        /// Description: Only allows numbers in the PhoneNumber field.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_PhoneNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            NumberValidationTextBox(sender, e);
        }
    }
}
