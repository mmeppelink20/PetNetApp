/// <summary>
/// Andrew Schneider
/// Created: 2023/04/05
/// 
/// Interaction logic for ViewDonationReceiptWindow.xaml
/// </summary>
///
/// <remarks>
/// Oleksiy Fedchuk
/// Updated: 2023/04/14
/// 
/// FinalQA
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
using System.Windows.Shapes;
using DataObjects;
using LogicLayer;

namespace WpfPresentation.Fundraising
{
    public partial class ViewDonationReceiptWindow : Window
    {
        private int _donationId;
        private MasterManager _manager = MasterManager.GetMasterManager();
        private DonationVM _donationVM = new DonationVM();
        private Users _user = new Users();
        private List<InKind> _inKindList = new List<InKind>();
        private ShelterVM _shelter = new ShelterVM();
        private bool _hasUser = false;
        private string _financialDetailsString;
        private List<string> _inKindStringsList = new List<string>();

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/04/06
        /// 
        /// Public constructor for ViewDonationReceiptWindow 
        /// </summary>
        ///
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/14
        /// 
        /// FinalQA
        /// </remarks>
        /// <param name="donationId">The Id of the donation to be displayed</param>
        public ViewDonationReceiptWindow(int donationId)
        {
            _donationId = donationId;
            InitializeComponent();
        }

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/04/06
        /// 
        /// Window loaded method that calls PopulatePage().
        /// </summary>
        ///
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/14
        /// 
        /// FinalQA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            PopulatePage();
        }

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/04/06
        /// 
        /// Helper method that calls a variety of methods to populate the receipt
        /// </summary>
        ///
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/14
        /// 
        /// FinalQA
        /// </remarks>
        private void PopulatePage()
        {
            GetDonationVM();
            if(_donationVM.UserId != null)
            {
                GetUser();
            }
            string shelterName = GetShelterName();
            lblShelterNameMessage.Content = "Your donation" + shelterName + " was submitted";
            PopulateContactInfoStackPanel();
            PopulateFinancialStackPanel();

            if (_donationVM.HasInKindDonation)
            {
                GetInKindList();
                stkInKind.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/04/06
        /// 
        /// Helper method for retrieving the Donation VM using the Id passed
        /// to the constructor
        /// </summary>
        ///
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/14
        /// 
        /// FinalQA
        /// </remarks>
        /// <exception cref="Exception">Retrieve DonationVM fails</exception>
        private void GetDonationVM()
        {
            try
            {
                _donationVM = _manager.DonationManager.RetrieveDonationByDonationId(_donationId);
            }
            catch (Exception ex)
            {
                PromptWindow.ShowPrompt("Error", "Failed to retreive donation.\n" + ex, ButtonMode.Ok);
                this.Close();
            }
        }

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/04/06
        /// 
        /// Helper method for getting the user using the User Id on the DonationVM
        /// </summary>
        ///
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/14
        /// 
        /// FinalQA
        /// </remarks>
        /// <exception cref="Exception">Retrieve user object fails</exception>
        private void GetUser()
        {
            try
            {
                _user = _manager.UsersManager.RetrieveUserByUsersId((int)_donationVM.UserId);
                _hasUser = true;
            }
            catch (Exception ex)
            {
                PromptWindow.ShowPrompt("Error", "Failed to retreive donation.\n" + ex, ButtonMode.Ok);
            }
        }

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/04/06
        /// 
        /// Helper method for getting the In-kind List from the database and calling
        /// PopulateInKindList if that action is successful.
        /// </summary>
        ///
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/14
        /// 
        /// FinalQA
        /// </remarks>
        /// <exception cref="Exception">Retrieve in-kind list fails</exception>
        private void GetInKindList()
        {
            try
            {
                _inKindList = _manager.DonationManager.RetrieveInKindsByDonationId(_donationVM.DonationId);
                PopulateInKindListWrapPanel();
            }
            catch (Exception ex)
            {
                PromptWindow.ShowPrompt("Error", "Failed to retreive in-kind donation list.\n" + ex, ButtonMode.Ok);
            }
        }

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/04/06
        /// 
        /// Helper method for getting the shelter name from the database using the shelter Id
        /// on the DonationVM object.
        /// </summary>
        ///
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/14
        /// 
        /// FinalQA
        /// </remarks>
        /// <exception cref="Exception">Retrieve shelter object fails</exception>
        /// <returns>Shelter name</returns>
        private string GetShelterName()
        {
            string shelterName = " to ";
            try
            {
                _shelter = _manager.ShelterManager.RetrieveShelterVMByShelterID(_donationVM.ShelterId);
            }
            catch (Exception)
            {
                return shelterName = "";
            }
            
            if(_shelter.ShelterName == null || _shelter.ShelterName == "")
            {
                return shelterName = "";
            }
            else
            {
                return shelterName += _shelter.ShelterName;
            }
        }

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/04/06
        /// 
        /// Helper method to call the necessary helper methods to populate the contact info stack panel
        /// </summary>
        ///
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/14
        /// 
        /// FinalQA
        /// </remarks>
        private void PopulateContactInfoStackPanel()
        {
            // Assign name label
            AssignNameLabel();

            // Assign phone label
            if (_donationVM.Phone != null && _donationVM.Phone != "")
            {
                lblPhone.Content += Regex.Replace(_donationVM.Phone, @"(\d{3})(\d{3})(\d{4})", "$1-$2-$3");
            }
            else if (_hasUser && _user.Phone != null && _user.Phone != "")
            {
                lblPhone.Content += Regex.Replace(_user.Phone, @"(\d{3})(\d{3})(\d{4})", "$1-$2-$3");
            }
            else
            {
                lblPhone.Content += "N/A";
            }

            // Assign email label
            if (_donationVM.Email != null && _donationVM.Email != "")
            {
                lblEmail.Content += _donationVM.Email;
            }
            else if (_hasUser)
            {
                lblEmail.Content += _user.Email;
            }
            else
            {
                lblEmail.Content += "N/A";
            }

            // Assign date label
            DateTime date = (DateTime) _donationVM.DateDonated;
            lblDate.Content += date.ToShortDateString();

            // Assign message label
            if (_donationVM.Message != null && _donationVM.Message.Length > 0)
            {
                txtMessage.Text += _donationVM.Message;
            }
            else
            {
                txtMessage.Text += "N/A";
            }
        }

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/04/07
        /// 
        /// Helper method to populate the financial donation stack panel
        /// </summary>
        ///
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/14
        /// 
        /// FinalQA
        /// </remarks>
        private void PopulateFinancialStackPanel()
        {
            if (_donationVM.Amount != null && _donationVM.Amount > 0)
            {
                string target = (_donationVM.Target == null || _donationVM.Target == "")
                                    ? "" : "to " + _donationVM.Target;
                if(target.Length > 20)
                {

                    target = target.Substring(0, 20) + "...";
                }
                _financialDetailsString = _donationVM.PaymentMethod + ": $" +
                    _donationVM.Amount + "  " + target;
                lblFinancialDetails.Content = _financialDetailsString;
                lblFinancialDetails.Margin = new Thickness(10, 0, 0, 0);
                stkFinancial.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/04/06
        /// 
        /// Helper method to run a serious of checks and assign the appropriate
        /// content to lblName.
        /// </summary>
        ///
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/14
        /// 
        /// FinalQA
        /// </remarks>
        private void AssignNameLabel()
        {
            if((_donationVM.GivenName == null || _donationVM.GivenName == "")
                && _donationVM.FamilyName != null && _donationVM.FamilyName != "")
            {
                lblName.Content += _donationVM.FamilyName;
            }
            else if ((_donationVM.FamilyName == null || _donationVM.FamilyName == "")
                && _donationVM.GivenName != null && _donationVM.GivenName != "")
            {
                lblName.Content += _donationVM.GivenName;
            }
            else if (_donationVM.FamilyName != null && _donationVM.FamilyName != ""
                && _donationVM.GivenName != null && _donationVM.GivenName != "")
            {
                lblName.Content += _donationVM.GivenName + " " + _donationVM.FamilyName;
            }
            else if (_hasUser && (_user.GivenName == null || _user.GivenName == ""))
            {
                lblName.Content += _user.FamilyName;
            }
            else if (_hasUser)
            {
                lblName.Content += _user.GivenName + " " + _user.FamilyName;
            }
            else
            {
                lblName.Content += "N/A";
            }
        }

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/04/06
        /// 
        /// Helper method to call the necessary helper methods to populate the In-kind list stack panel
        /// </summary>
        ///
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/14
        /// 
        /// FinalQA
        /// </remarks>
        private void PopulateInKindListWrapPanel()
        {
            foreach (var donation in _inKindList)
            {
                string target = (donation.Target == null || donation.Target == "")
                                    ? "" : "to " + donation.Target;
                if (target.Length > 20)
                {
                    target = target.Substring(0, 20) + "...";
                }
                string donationString = donation.Description + "  x" + donation.Quantity + "  " + target;
                _inKindStringsList.Add(donationString);
                Label label = new Label();
                label.Content = donationString;
                label.Margin = new Thickness(10, 0, 0, 0);
                label.Foreground = (Brush)new BrushConverter().ConvertFrom("#FFEEF2E6");
                wrpInKind.Children.Add(label);
            }
        }

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/04/11
        /// 
        /// Helper method that creates a FlowDocument to send to the PrintDialog
        /// created in the btnPrint_click event handler.
        /// </summary>
        ///
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/14
        /// 
        /// FinalQA
        /// </remarks>
        /// <returns>FlowDocument object</returns>
        private FlowDocument CreateReceipt()
        {
            FlowDocument doc = new FlowDocument();
            Section sec = new Section();

            Paragraph p1 = new Paragraph();
            Bold bld = new Bold();
            bld.Inlines.Add(new Run(lblShelterNameMessage.Content.ToString()));
            p1.Inlines.Add(bld);
            sec.Blocks.Add(p1);

            Paragraph p2 = new Paragraph();
            Underline underline1 = new Underline();
            underline1.Inlines.Add(new Run(lblContactInformationTitle.Content.ToString() + "\n"));
            string name = lblName.Content.ToString() + "\n";
            string phone = lblPhone.Content.ToString() + "\n";
            string email = lblEmail.Content.ToString() + "\n";
            string date = lblDate.Content.ToString() + "\n";
            string message = txtMessage.Text;
            p2.Inlines.Add(underline1);
            p2.Inlines.Add(name);
            p2.Inlines.Add(phone);
            p2.Inlines.Add(email);
            p2.Inlines.Add(date);
            p2.Inlines.Add(message);
            sec.Blocks.Add(p2);

            if(stkFinancial.Visibility == Visibility.Visible)
            {
                Paragraph p3 = new Paragraph();
                Underline underline2 = new Underline();
                underline2.Inlines.Add(new Run(lblFinancialTitle.Content.ToString() + "\n"));
                p3.Inlines.Add(underline2);
                p3.Inlines.Add(_financialDetailsString);
                sec.Blocks.Add(p3);
            }
            
            if(stkInKind.Visibility == Visibility.Visible)
            {
                Paragraph p4 = new Paragraph();
                Underline underline3 = new Underline();
                underline3.Inlines.Add(new Run(lblInKind.Content.ToString() + "\n"));
                p4.Inlines.Add(underline3);
                var lastLine = _inKindStringsList.Last();
                foreach (var line in _inKindStringsList)
                {
                    string lineText = line;
                    if(line != lastLine)
                    {
                        lineText += "\n";
                    }
                    p4.Inlines.Add(lineText);
                }
                sec.Blocks.Add(p4);
            }

            Paragraph p5 = new Paragraph();
            Bold bld2 = new Bold();
            bld2.Inlines.Add(new Run(lblThankYou.Content.ToString()));
            p5.Inlines.Add(bld2);
            sec.Blocks.Add(p5);

            doc.Blocks.Add(sec);
            return doc;
        }

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/04/06
        /// 
        /// Event handler that closes the View Donation Receipt Window
        /// </summary>
        ///
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/14
        /// 
        /// FinalQA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/04/06
        /// 
        /// Event handler that closes the View Donation Receipt Window
        /// </summary>
        ///
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/14
        /// 
        /// FinalQA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCloseWindowX_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/04/07
        /// 
        /// Event handler that calls CreateReceipt to build the FlowDocument 
        /// and then passes the FlowDocument to the PrintDialog object.
        /// </summary>
        ///
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/14
        /// 
        /// FinalQA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDlg = new PrintDialog();

            FlowDocument doc = CreateReceipt();
            doc.Name = "FlowDoc";
            IDocumentPaginatorSource idpSource = doc;

            printDlg.PrintDocument(idpSource.DocumentPaginator, "Hello WPF Printing.");
        }
    }
}
