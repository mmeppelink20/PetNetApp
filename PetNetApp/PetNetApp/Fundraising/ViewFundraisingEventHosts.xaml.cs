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
//using DataAccessLayerFakes;
using WpfPresentation.UserControls;

namespace WpfPresentation.Fundraising
{
    public partial class ViewFundraisingEventHosts : Page
    {
        private static ViewFundraisingEventHosts _viewFundraisingEventHosts = null;

        private string _currentSearchText = "";
        private MasterManager _masterManager = MasterManager.GetMasterManager();
        private bool _needsReloaded = true;
        private List<InstitutionalEntity> _hosts = null;
        private List<InstitutionalEntity> _filteredHosts = null;
        private static Regex _isDigit = new Regex(@"^\d+$");
        private string _contactType = "Host";

        //for testing
        //InstitutionalEntityManager _institutionalEntityManager = new InstitutionalEntityManager(new InstitutionalAccessorFakes());

        // page navigation
        private int _currentPage = 1;
        private int _totalPages = 1;
        private int _itemsPerPage = 10;

        /// <summary>
        /// Author: Asa Armstrong
        /// Date: 2023/04/23
        /// Description: Constructor for page ViewFundraisingEventHosts
        /// </summary>
        private ViewFundraisingEventHosts()
        {
            InitializeComponent();
            cbSort.SelectionChanged += comboChanged;
        }

        public static ViewFundraisingEventHosts GetViewFundraisingEventHosts()
        {
            if (_viewFundraisingEventHosts == null)
            {
                _viewFundraisingEventHosts = new ViewFundraisingEventHosts();
                MasterManager.GetMasterManager().UserLogout += () => _viewFundraisingEventHosts = null;
            }
            _viewFundraisingEventHosts.LoadEventHostsData();

            _viewFundraisingEventHosts._needsReloaded = false;
            return _viewFundraisingEventHosts;
        }

        /// <summary>
        /// Barry Mikulas
        /// Created: 2023/03/02
        /// 
        /// Loads the Fundraising event contacts
        /// of the logged in user and runs the method to filter and sort
        /// <remarks>
        /// Updated: Asa Armstrong
        /// 2023/03/15
        /// Added for Hosts. Changed method name from LoadFundraisingEventContactData() to LoadEventHostsData().
        /// </remarks>
        /// </summary>
        private void LoadEventHostsData()
        {
            try
            {
                _hosts = _masterManager.InstitutionalEntityManager.RetrieveAllInstitutionalEntitiesByShelterIdAndEntityType((int)_masterManager.User.ShelterId, _contactType);
                // for testing
                // _hosts = _institutionalEntityManager.RetrieveAllInstitutionalEntitiesByShelterIdAndContactType((int)_masterManager.User.ShelterId, _contactType);
            }
            catch (ApplicationException ex)
            {
                _hosts = new List<InstitutionalEntity>();
                PromptWindow.ShowPrompt("Error", ex.Message);
            }
            ApplyFundraisingEventContactSort(false);
        }

        /// <summary>
        /// Stephen Jaurigue
        /// Created: 2023/02/24
        /// 
        /// Calls the methods to change navigation buttons and show the appropriate Institutional Entities for the selected page
        /// <remarks>
        /// Updated: Barry Mikulas & Asa Armstrong
        /// 2023/03/15
        /// Modified to work with Institutional Entities (Barry). Added to Hosts (Asa).
        /// </remarks>
        /// </summary>
        private void UpdateUI()
        {
            PopulateNavigationButtons();
            PopulateInstitutionalEntityList();
        }

        /// <summary>
        /// Stephen Jaurigue
        /// 2023/02/23
        /// 
        /// Updates the controls at the bottom of the page to appear and dissapear as needed.
        /// Also generates the numbered buttons for navigation
        /// 
        /// Updated by: Barry Mikulas & Asa Armstrong
        /// 2023/03/02
        /// Taken from ViewCampaignPage.xaml.cs modified to work with this page (Barry). Added for hosts (Asa)
        /// </summary>
        private void PopulateNavigationButtons()
        {
            btnPreviousPage.Visibility = _currentPage == 1 ? Visibility.Collapsed : Visibility.Visible;
            btnNextPage.Visibility = _currentPage == _totalPages ? Visibility.Collapsed : Visibility.Visible;
            if (_totalPages <= 5)
            {
                btnFirstPage.Visibility = Visibility.Collapsed;
                btnLastPage.Visibility = Visibility.Collapsed;
            }
            else
            {
                btnFirstPage.Visibility = _currentPage > 3 ? Visibility.Visible : Visibility.Collapsed;
                btnLastPage.Visibility = _currentPage <= _totalPages - 3 ? Visibility.Visible : Visibility.Collapsed;
            }
            // Populate Number Buttons
            stackInnerButtons.Children.Clear();
            int startPage = _currentPage - 2;
            int endPage = _currentPage + 2;
            if (endPage > _totalPages)
            {
                startPage -= endPage - _totalPages;
                endPage = _totalPages;
            }
            if (startPage < 1)
            {
                endPage += 1 - startPage;
                startPage = 1;
            }
            if (endPage > _totalPages)
            {
                endPage = _totalPages;
            }
            if (startPage < 1)
            {
                startPage = 1;
            }
            for (int currentPage = startPage; currentPage <= endPage; currentPage++)
            {
                Button currentPageButton = new Button();
                currentPageButton.Content = currentPage.ToString();
                currentPageButton.Width = 40;
                currentPageButton.Height = 40;
                currentPageButton.Margin = new Thickness(2);
                if (currentPage == _currentPage)
                {
                    currentPageButton.IsEnabled = false;
                }
                int page = currentPage;
                currentPageButton.Click += (obj, args) => NavigateToPage(page);
                stackInnerButtons.Children.Add(currentPageButton);
            }
        }

        /// <summary>
        /// Stephen Jaurigue
        /// Created: 2023/02/23
        /// 
        /// Changes the active page to the page number and updates the UI
        /// </summary>
        /// <param name="page"></param>
        private void NavigateToPage(int page)
        {
            _currentPage = page;
            UpdateUI();
        }


        /// <summary>
        /// Stephen Jaurigue
        /// Created: 2023/02/23
        /// 
        /// Shows the selected pages Institutional Entity List or a message if there are no Institutional Entities
        /// Updated by: Barry Mikulas & Asa Armstrong
        /// 2023/03/02
        /// Modified to work with Institutional Entities (Barry). Added for hosts (Asa).
        /// </summary>
        private void PopulateInstitutionalEntityList()
        {
            stackHosts.Children.Clear();
            if (_filteredHosts.Count == 0)
            {
                stackHosts.Visibility = Visibility.Collapsed;
                nothingToShowMessage.Visibility = Visibility.Visible;
            }
            else
            {
                stackHosts.Visibility = Visibility.Visible;
                nothingToShowMessage.Visibility = Visibility.Collapsed;
            }
            int i = 0;
            foreach (InstitutionalEntity institutionalEntity in _filteredHosts.Skip(_itemsPerPage * (_currentPage - 1)).Take(_itemsPerPage))
            {
                ViewFundraisingInstitutionalEntityControl item = new ViewFundraisingInstitutionalEntityControl(institutionalEntity, i % 2 == 0);
                i++;
                stackHosts.Children.Add(item);
            }
        }

        /// <summary>
        /// Stephen Jaurigue
        /// Created: 2023/02/23
        /// 
        /// Loads the data for the  if they haven't already been loaded and don't need reloaded
        /// </summary>
        /// <remarks>
        /// updated by Barry Mikulas & Asa Armstrong
        /// 2023/03/02
        /// modified to work with institutionalEntities (Barry). Added for hosts (Asa)
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (_needsReloaded)
            {
                LoadInstitutionalEntityData();
                _needsReloaded = false;
            }
        }

        /// <summary>
        /// Barry Mikulas
        /// Created: 2023/03/02
        /// 
        /// Loads the Institutional Entities for the shelter of the logged in user with a given contactId
        /// and runs the method to filter and sort
        /// <remarks>
        /// Updated: Asa Armstrong
        /// 2023/03/15
        /// Added for Hosts.
        /// </remarks>
        /// </summary>
        private void LoadInstitutionalEntityData()
        {
            try
            {
                _hosts = _masterManager.InstitutionalEntityManager.RetrieveAllInstitutionalEntitiesByShelterIdAndEntityType((int)_masterManager.User.ShelterId, _contactType);
                // using fake data
                // _hosts = _institutionalEntityManager.RetrieveAllInstitutionalEntitiesByShelterIdAndContactType((int)_masterManager.User.ShelterId, _contactType);

            }
            catch (ApplicationException ex)
            {
                _hosts = new List<InstitutionalEntity>();
                PromptWindow.ShowPrompt("Error", ex.Message);
            }
            ApplyFundraisingInstitutionalEntitySort(false);
        }

        /// <summary>
        /// Stephen Jaurigue
        /// Created: 2023/02/24
        /// 
        /// Takes the loaded Institutional Entity list and applies filters and sorts it, then updates the navigation
        /// information and finally updates the UI
        /// </summary>
        /// Modified from original code from ViewCampaignsPage.xaml.cs
        /// <param name="resetPage"></param>
        private void ApplyFundraisingInstitutionalEntitySort(bool resetPage = true)
        {
            Func<InstitutionalEntity, string> sortMethod = null;
            switch (((string)((ComboBoxItem)cbSort.SelectedValue).Content).ToLower())
            {
                case "given name":
                    sortMethod = new Func<InstitutionalEntity, string>(ie => ie.GivenName);
                    break;
                case "family name":
                    sortMethod = new Func<InstitutionalEntity, string>(ie => ie.FamilyName);
                    break;
                case "company name":
                    sortMethod = new Func<InstitutionalEntity, string>(ie => ie.CompanyName);
                    break;
                case "email":
                    sortMethod = new Func<InstitutionalEntity, string>(ie => ie.Email);
                    break;
                default:
                    sortMethod = new Func<InstitutionalEntity, string>(ie => ie.InstitutionalEntityId.ToString());
                    break;
            }
            _filteredHosts = _hosts.Where(SearchForTextInInstitutionalEntity).OrderBy(sortMethod).ToList();
            UpdateNavigationInformation();
            _currentPage = resetPage ? 1 : _currentPage > _totalPages ? _totalPages : _currentPage;
            UpdateUI();
        }


        /// <summary>
        /// Stephen Jaurigue
        /// Created: 2023/02/23
        /// 
        /// Updates the total pages needed for all the campaigns
        /// </summary>
        private void UpdateNavigationInformation()
        {
            _totalPages = (_filteredHosts.Count - 1) / _itemsPerPage + 1;
        }


        /// <summary>
        /// Stephen Jaurigue
        /// Created: 2023/02/24
        /// 
        /// Takes a institutional entity and returns true if it contains the search text in its given name, family name, company namestart or email (case insensitive)
        /// </summary>
        /// <remarks>
        /// updated by: Barry Mikulas & Asa Armstrong
        /// updated 2023/03/02
        /// modified from ViewCampaignPage.xaml (Barry). Added for hosts (Asa)
        /// </remarks>
        /// <param name="institutionalEntity"/>
        /// <returns>Wether there is any matching data to the Current Search Text</returns>
        private bool SearchForTextInInstitutionalEntity(InstitutionalEntity institutionalEntity)
        {
            return institutionalEntity.GivenName?.IndexOf(_currentSearchText, StringComparison.OrdinalIgnoreCase) >= 0 ||
                   institutionalEntity.FamilyName?.IndexOf(_currentSearchText, StringComparison.OrdinalIgnoreCase) >= 0 ||
                   institutionalEntity.CompanyName?.IndexOf(_currentSearchText, StringComparison.OrdinalIgnoreCase) >= 0 ||
                   institutionalEntity.Email?.IndexOf(_currentSearchText, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        /// <summary>
        /// Stephen Jaurigue
        /// Created: 2023/02/23
        /// 
        /// Changes the Needs Loaded indicator to say that the page needs reloaded when the user refocuses
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param
        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            _needsReloaded = true;
        }

        /// <summary>
        /// Stephen Jaurigue
        /// Created: 2023/02/23
        /// 
        /// Searches for the text inside the campaigns on enter key pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                TrySearch();
            }
        }

        /// <summary>
        /// Stephen Jaurigue
        /// Created: 2023/02/23
        /// 
        /// If the text has changed since last search, searches for the new text
        /// </summary>
        private void TrySearch()
        {
            string searchText = tbSearch.Text.ToLower().Trim();
            if (searchText != _currentSearchText)
            {
                _currentSearchText = searchText;
                ApplyFundraisingEventContactSort();
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            TrySearch();
        }

        private void btnAddHost_Click(object sender, RoutedEventArgs e)
        {
            AddEditInstitutionalEntity addEditInstitutionalEntity = new AddEditInstitutionalEntity(_contactType);
            addEditInstitutionalEntity.Owner = Window.GetWindow(this);
            addEditInstitutionalEntity.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            addEditInstitutionalEntity.ShowDialog();
            NavigationService.Navigate(new ViewFundraisingEventHosts());
        }

        private void btnFirstPage_Click(object sender, RoutedEventArgs e)
        {
            _currentPage = 1;
            UpdateUI();
        }

        /// <summary>
        /// Stephen Jaurigue
        /// Created: 2023/02/23
        /// 
        /// Goes to the previous page and updates the UI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPreviousPage_Click(object sender, RoutedEventArgs e)
        {
            _currentPage--;
            UpdateUI();
        }

        /// <summary>
        /// Stephen Jaurigue
        /// Created: 2023/02/23
        /// 
        /// Goes to the next page and updates the UI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNextPage_Click(object sender, RoutedEventArgs e)
        {
            _currentPage++;
            UpdateUI();
        }

        private void btnLastPage_Click(object sender, RoutedEventArgs e)
        {
            _currentPage = _totalPages;
            UpdateUI();
        }

        private void tbPage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                NavigateToTypedPage();
            }
        }

        /// <summary>
        /// Stephen Jaurigue
        /// Created: 2023/02/23
        /// 
        /// Navigates to the page indicated by the user in the page number textbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NavigateToTypedPage()
        {
            if (IsValidPage(tbPage.Text))
            {
                _currentPage = int.Parse(tbPage.Text);
                UpdateUI();
            }
            else
            {
                tbPage.Text = _currentPage.ToString();
            }
        }

        /// <summary>
        /// Stephen Jaurigue
        /// Created: 2023/02/23
        /// 
        /// Makes sure the typed page is only digits and is within the range of pages
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        private bool IsValidPage(string page)
        {
            if (page.Length < 8 && _isDigit.IsMatch(page))
            {
                int selectedPage = int.Parse(page);
                if (selectedPage >= 1 && selectedPage <= _totalPages)
                {
                    return true;
                }
            }
            return false;
        }

        private void btnNavigatePage_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Barry Mikulas
        /// Created: 2023/03/01
        /// 
        /// Takes the loaded Fundraising contacts list and applies filters and sorts it,
        /// then updates the navigation
        /// information and finally updates the UI
        /// <remarks>
        /// Updated: Asa Armstrong
        /// 2023/03/15
        /// Added for Hosts.
        /// </remarks>
        /// </summary>
        /// <param name="resetPage"></param>
        private void ApplyFundraisingEventContactSort(bool resetPage = true)
        {
            Func<InstitutionalEntity, string> sortMethod = null;
            switch (((string)((ComboBoxItem)cbSort.SelectedValue).Content).ToLower())
            {
                case "family name":
                    sortMethod = new Func<InstitutionalEntity, string>(ie => ie.FamilyName);
                    break;
                case "given name":
                    sortMethod = new Func<InstitutionalEntity, string>(ie => ie.GivenName);
                    break;
                case "company name":
                    sortMethod = new Func<InstitutionalEntity, string>(ie => ie.CompanyName);
                    break;
                case "email":
                    sortMethod = new Func<InstitutionalEntity, string>(ie => ie.Email);
                    break;
                default:
                    sortMethod = new Func<InstitutionalEntity, string>(fc => fc.Email);
                    break;
            }

            _filteredHosts = _hosts.Where(SearchForTextInInstitutionalEntity).OrderBy(sortMethod).ToList();
            UpdateNavigationInformation();
            _currentPage = resetPage ? 1 : _currentPage > _totalPages ? _totalPages : _currentPage;

            UpdateUI();

        }


        /// <summary>
        /// Stephen Jaurigue
        /// Created: 2023/02/23
        /// 
        /// Updates the display to be in the new selected order and resets the page back to the beginning
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboChanged(object sender, RoutedEventArgs e)
        {
            ApplyFundraisingEventContactSort();
        }

        private void Page_GotFocus(object sender, RoutedEventArgs e)
        {
            Page_Loaded(sender, e);
        }
    }
}
