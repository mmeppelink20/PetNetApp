using DataObjects;
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
using System.Windows.Shapes;

namespace WpfPresentation.Events
{
    /// <summary>
    /// Interaction logic for SponsorListWindow.xaml
    /// </summary>
    public partial class SponsorListWindow : Window
    {
        private List<InstitutionalEntity> _sponsors;
        public InstitutionalEntity returnValue;
        private List<InstitutionalEntity> sponsorSearch;
        public SponsorListWindow(List<InstitutionalEntity> sponsors)
        {
            InitializeComponent();
            _sponsors = sponsors;
            returnValue = null;
        }

        /// <summary>
        /// Author: Hoang Chu
        /// 04/27/2023
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCanle_Click(object sender, RoutedEventArgs e)
        {
            returnValue = null;
            this.DialogResult = false;
        }

        /// <summary>
        /// Author: Hoang Chu
        /// 04/27/2023
        /// 
        /// </summary>
        private void SponsorSearching()
        {
            sponsorSearch = new List<InstitutionalEntity>();
            if (txtSearchSponsor.Text.ToLower() == "")
            {
                foreach (InstitutionalEntity sponsor in _sponsors)
                {
                    sponsorSearch.Add(sponsor);
                }
            }
            else
            {
                foreach (InstitutionalEntity sponsor in _sponsors)
                {
                    if (sponsor.CompanyName.ToLower().Contains(txtSearchSponsor.Text.ToLower()))
                    {
                        sponsorSearch.Add(sponsor);
                    }
                }
            }
            PopulateSponsors();
        }

        /// <summary>
        /// Author: Hoang Chu
        /// 04/27/2023
        /// 
        /// </summary>
        /// <param name="sponsor"></param>
        private void DisplaySponsors(InstitutionalEntity sponsor)
        {
            UCEventSponsor ucEventSponsor = new UCEventSponsor();
            ucEventSponsor.lblSponsorName.Content = sponsor.CompanyName;
            ucEventSponsor.btnView.Click += (obj, arg) => BtnView_Click(sponsor);
            ucEventSponsor.btnAdd.Click += (obj, arg) => BtnAdd_Click(sponsor);
            ucEventSponsor.Margin = new Thickness(0, 0, 10, 0);
            stpEventSponsors.Children.Add(ucEventSponsor);
        }

        /// <summary>
        /// Author: Hoang Chu
        /// 04/27/2023
        /// 
        /// </summary>
        /// <param name="sponsor"></param>
        private void BtnAdd_Click(InstitutionalEntity sponsor)
        {
            returnValue = sponsor;
            this.DialogResult = false;
        }

        /// <summary>
        /// Author: Hoang Chu
        /// 04/27/2023
        /// 
        /// </summary>
        /// <param name="sponsor"></param>
        private void BtnView_Click(InstitutionalEntity sponsor)
        {
            PromptWindow.ShowPrompt("Sponsor Detail", "Name: " + sponsor.CompanyName + "\n\n" 
                + "Email: " + sponsor.Email + "\n\n" + "Phone Number: " + sponsor.Phone);
        }

        /// <summary>
        /// Author: Hoang Chu
        /// 04/27/2023
        /// 
        /// </summary>
        private void PopulateSponsors()
        {
            stpEventSponsors.Children.Clear();
            foreach (InstitutionalEntity sponsor in sponsorSearch)
            {
                DisplaySponsors(sponsor);
            }
        }

        /// <summary>
        /// Author: Hoang Chu
        /// 04/27/2023
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SponsorSearching();
        }

        /// <summary>
        /// Author: Hoang Chu
        /// 04/27/2023
        /// 
        /// </summary>
        /// <returns></returns>
        public InstitutionalEntity GetReturnValue()
        {
            return returnValue;
        }

        /// <summary>
        /// Author: Hoang Chu
        /// 04/27/2023
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSearchSponsor_TextChanged(object sender, TextChangedEventArgs e)
        {
            SponsorSearching();
        }
    }
}
