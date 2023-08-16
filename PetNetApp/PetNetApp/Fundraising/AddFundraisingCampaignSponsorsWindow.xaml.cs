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
using System.Windows.Shapes;
using WpfPresentation.UserControls;

namespace WpfPresentation.Fundraising
{

    /// <summary>
    /// Interaction logic for AddFundraisingCampaignSponsorsWindow.xaml
    /// </summary>
    public partial class AddFundraisingCampaignSponsorsWindow : Window
    {
        private List<InstitutionalEntity> _currentSponsors = null;
        private List<InstitutionalEntity> _allSponsors = null;
        private List<InstitutionalEntity> _addedSponsors = new List<InstitutionalEntity>();
        private MasterManager _masterManager = MasterManager.GetMasterManager();
        private string _currentSearch = "";
        private string _entityType = null;



        public InstitutionalEntity SelectedInstitutionalEntity
        {
            get { return (InstitutionalEntity)GetValue(SelectedInstitutionalEntityProperty); }
            set { SetValue(SelectedInstitutionalEntityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedInstitutionalEntity.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedInstitutionalEntityProperty =
            DependencyProperty.Register("SelectedInstitutionalEntity", typeof(InstitutionalEntity), typeof(AddFundraisingCampaignSponsorsWindow), new PropertyMetadata(null));


        public AddFundraisingCampaignSponsorsWindow(List<InstitutionalEntity> currentSponsors)
        {
            DataContext = this;
            _currentSponsors = currentSponsors;
            try
            {
                _allSponsors = _masterManager.InstitutionalEntityManager.RetrieveAllSponsors();
            }
            catch (Exception ex)
            {
                PromptWindow.ShowPrompt("Error", ex.Message);
                _allSponsors = new List<InstitutionalEntity>();
            }
            InitializeComponent();
        }


        public AddFundraisingCampaignSponsorsWindow(List<InstitutionalEntity> currentSponsors, string entityType)
        {
            DataContext = this;
            _currentSponsors = currentSponsors;
            _entityType = entityType;
            try
            {
                _allSponsors = _masterManager.InstitutionalEntityManager.RetrieveAllInstitutionalEntitiesByShelterIdAndEntityType((int)_masterManager.User.ShelterId, entityType);
            }
            catch (Exception ex)
            {
                PromptWindow.ShowPrompt("Error", ex.Message);
                _allSponsors = new List<InstitutionalEntity>();
            }
            InitializeComponent();

        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ClearAndPopulateAddInstitutionalEntities();
            if (_entityType != null)
            {
                lblTitle.Content = "Add Event " + _entityType;
                ptbSearchText.DefaultText = "Search " + _entityType;
                btnNewSponsor.Content = "Add New " + _entityType;
            }
            CloseSelectedInstitutionalEntity();
        }

        private void CloseSelectedInstitutionalEntity()
        {
            SelectedInstitutionalEntity = null;
            grdViewInstitutionalEntity.Visibility = Visibility.Collapsed;
        }

        private void ClearAndPopulateAddInstitutionalEntities()
        {
            stackAddSponsors.Children.Clear();
            var sponsorsNotCurrentlySponsoringAndMatchingSearch = _allSponsors.Where((newSponsor) => !_currentSponsors.Exists((currentSponsor) => currentSponsor.InstitutionalEntityId == newSponsor.InstitutionalEntityId) &&
                                                                                                        !_addedSponsors.Exists((addedSponsor) => addedSponsor.InstitutionalEntityId == newSponsor.InstitutionalEntityId));
            if (_currentSearch != "")
            {
                sponsorsNotCurrentlySponsoringAndMatchingSearch = sponsorsNotCurrentlySponsoringAndMatchingSearch.Where((sponsor) => (sponsor.GivenName + " " + sponsor.FamilyName).IndexOf(_currentSearch, StringComparison.InvariantCultureIgnoreCase) >= 0 ||
                sponsor.Email.IndexOf(_currentSearch, StringComparison.InvariantCultureIgnoreCase) >= 0 || sponsor.CompanyName?.IndexOf(_currentSearch, StringComparison.InvariantCultureIgnoreCase) >= 0 ||
                sponsor.Phone?.IndexOf(_currentSearch, StringComparison.InvariantCultureIgnoreCase) >= 0 || sponsor.Address?.IndexOf(_currentSearch, StringComparison.InvariantCultureIgnoreCase) >= 0);
            }
            foreach (var item in sponsorsNotCurrentlySponsoringAndMatchingSearch)
            {
                var institutionalEntityControl = new InstitutionalEntityUserControl(item, true, true);
                institutionalEntityControl.btnAdd.Click += (sender, args) => btnAddSponsor_Click(sender, args, item);
                institutionalEntityControl.btnView.Click += (sender, args) => btnViewSponsor_Click(sender, args, item);
                stackAddSponsors.Children.Add(institutionalEntityControl);
            }
        }

        private void btnAddSponsor_Click(object sender, RoutedEventArgs e, InstitutionalEntity institutionalEntity)
        {
            _addedSponsors.Add(institutionalEntity);
            ClearAndPopulateAddInstitutionalEntities();
        }

        private void btnViewSponsor_Click(object sender, RoutedEventArgs e, InstitutionalEntity institutionalEntity)
        {
            SelectedInstitutionalEntity = institutionalEntity;
            grdViewInstitutionalEntity.Visibility = Visibility.Visible;
            ClearAndPopulateAddInstitutionalEntities();
        }

        private void btnNewSponsor_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Not implimented");
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            UpdateSearchIfTextChanged();
        }

        private void UpdateSearchIfTextChanged()
        {
            if (ptbSearchText.Text.Trim().ToLower() != _currentSearch)
            {
                _currentSearch = ptbSearchText.Text.Trim().ToLower();
                ClearAndPopulateAddInstitutionalEntities();
            }
        }

        private void ptbSearchText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                UpdateSearchIfTextChanged();
            }
        }

        private void btnViewInstitutionalEntityClose_Click(object sender, RoutedEventArgs e)
        {
            CloseSelectedInstitutionalEntity();
        }

        private void btnDone_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in _addedSponsors)
            {
                _currentSponsors.Add(item);
            }
            this.Close();
        }

        private void btnAddSelectedInstitutionalEntity_Click(object sender, RoutedEventArgs e)
        {
            btnAddSponsor_Click(sender, e, SelectedInstitutionalEntity);
            CloseSelectedInstitutionalEntity();
        }
    }
}
