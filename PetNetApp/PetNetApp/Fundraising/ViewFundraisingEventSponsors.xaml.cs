using DataAccessLayerFakes;
using DataObjects;
using LogicLayer;
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
using WpfPresentation.UserControls;

namespace WpfPresentation.Fundraising
{
    /// <summary>
    /// Interaction logic for ViewFundraisingEventSponsors.xaml
    /// </summary>
    public partial class ViewFundraisingEventSponsors : Page
    {
        private static ViewFundraisingEventSponsors _existingViewFundraisingEventSponsors = null;

        private string _currentSearchText = "";
        private MasterManager _masterManager = MasterManager.GetMasterManager();
        private bool _needsReloaded = true;
        private List<InstitutionalEntity> _fundraisingEventSponsors = null;
        private List<InstitutionalEntity> _filteredFundraisingEventSponsors = null;
        private static Regex _isDigit = new Regex(@"^\d+$");
        private string _entityType = "Sponsor";


  

        private int _currentPage = 1;
        private int _totalPages = 1;
        private int _itemsPerPage = 10;

        public ViewFundraisingEventSponsors()
        {
            InitializeComponent();
            cbSort.SelectionChanged += comboChanged;
        }

        public static ViewFundraisingEventSponsors GetViewEventSponsors()
        {
            if (_existingViewFundraisingEventSponsors == null)
            {
                _existingViewFundraisingEventSponsors = new ViewFundraisingEventSponsors();
                MasterManager.GetMasterManager().UserLogout += () => _existingViewFundraisingEventSponsors = null;
            }

            _existingViewFundraisingEventSponsors.LoadInstitutionalEntityData();

            _existingViewFundraisingEventSponsors._needsReloaded = false;
            return _existingViewFundraisingEventSponsors;

        }

        private void btnNavigatePage_Click(object sender, RoutedEventArgs e)
        {
            NavigateToTypedPage();
        }

        private void btnFirstPage_Click(object sender, RoutedEventArgs e)
        {
            _currentPage = 1;
            UpdateUI();
        }

        private void btnPreviousPage_Click(object sender, RoutedEventArgs e)
        {
            _currentPage = 1;
            UpdateUI();
        }

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

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddEditInstitutionalEntity addEditInstitutionalEntity = new AddEditInstitutionalEntity(_entityType);
            addEditInstitutionalEntity.Owner = Window.GetWindow(this);
            addEditInstitutionalEntity.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            addEditInstitutionalEntity.ShowDialog();
            NavigationService.Navigate(new ViewFundraisingEventSponsors());
        }

        /// <summary>
        /// Stephen Jaurigue
        /// Created: 2023/02/23
        /// 
        /// Loads the data for the  if they haven't already been loaded and don't need reloaded
        /// </summary>
        /// <remarks>
        /// updated by Barry Mikulas
        /// 2023/03/02
        /// modified to work with institutionalEntities
        /// updated by William Rients
        /// 2023/03/02
        /// modified to work with sponsors page
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
        /// William Rients
        /// Created: 2023/03/09
        /// 
        /// Loads the Institutional Entities for the shelter of the logged in user with a given contactId
        /// of the logged in user and runs the method to filter and sort
        /// </summary>
        private void LoadInstitutionalEntityData()
        {
            try
            {
                _fundraisingEventSponsors = _masterManager.InstitutionalEntityManager.RetrieveAllInstitutionalEntitiesByShelterIdAndEntityType((int)_masterManager.User.ShelterId, _entityType);
            }
            catch (ApplicationException ex)
            {
                _fundraisingEventSponsors = new List<InstitutionalEntity>();
                PromptWindow.ShowPrompt("Error", ex.Message);
            }
            ApplyFundraisingEventSponsorsort(false);
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
        /// Updated by: Barry Mikulas
        /// 2023/03/02
        /// Modified to work with Institutional Entities
        /// Updated by: William Rients
        /// 2023/03/09
        /// Modified to work with Sponsors page
        /// </summary>
        private void PopulateInstitutionalEntityList()
        {
            stacksponsors.Children.Clear();
            if (_filteredFundraisingEventSponsors.Count == 0)
            {
                stacksponsors.Visibility = Visibility.Collapsed;
                nothingToShowMessage.Visibility = Visibility.Visible;
            }
            else
            {
                stacksponsors.Visibility = Visibility.Visible;
                nothingToShowMessage.Visibility = Visibility.Collapsed;
            }
            int i = 0;
            foreach (InstitutionalEntity institutionalEntity in _filteredFundraisingEventSponsors.Skip(_itemsPerPage * (_currentPage - 1)).Take(_itemsPerPage))
            {
                ViewFundraisingInstitutionalEntityControl item = new ViewFundraisingInstitutionalEntityControl(institutionalEntity, i % 2 == 0);
                i++;
                stacksponsors.Children.Add(item);
            }
        }

        /// <summary>
        /// Stephen Jaurigue
        /// 2023/02/23
        /// 
        /// Updates the controls at the bottom of the page to appear and dissapear as needed.
        /// Also generates the numbered buttons for navigation
        /// 
        /// Updated by: William Rients
        /// 2023/03/09
        /// Taken from ViewCampaignPage.xaml.cs modified to work with this page
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
        /// Created: 2023/02/24
        /// 
        /// Calls the methods to change navigation buttons and show the appropriate Institutional Entities for the selected page
        /// <remarks>
        /// Updated: Barry Mikulas
        /// 2023/03/02
        /// Modified to work with Institutional Entities
        /// Updated: William Rients
        /// 2023/03/09
        /// Modified to work with Sponsors page
        /// </remarks>
        /// </summary>
        private void UpdateUI()
        {
            PopulateNavigationButtons();
            PopulateInstitutionalEntityList();
        }

        /// <summary>
        /// Stephen Jaurigue
        /// Created: 2023/02/23
        /// 
        /// Updates the total pages needed for all the campaigns
        /// </summary>
        private void UpdateNavigationInformation()
        {
            _totalPages = (_filteredFundraisingEventSponsors.Count - 1) / _itemsPerPage + 1;
        }

        /// <summary>
        /// Stephen Jaurigue
        /// Created: 2023/02/24
        /// 
        /// Takes a institutional entity and returns true if it contains the search text in its given name, family name, company namestart or email (case insensitive)
        /// </summary>
        /// <remarks>
        /// updated by: Barry Mikulas
        /// updated 2023/03/02
        /// modified from ViewCampaignPage.xaml
        /// Updated: William Rients
        /// 2023/03/09
        /// Modified from ViewCampaignPage.xaml
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
        /// William Rients
        /// Created: 2023/03/00
        /// 
        /// Takes the loaded Fundraising Sponsors list and applies filters and sorts it,
        /// then updates the navigation
        /// information and finally updates the UI
        /// </summary>
        /// <param name="resetPage"></param>
        private void ApplyFundraisingEventSponsorsort(bool resetPage = true)
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

            _filteredFundraisingEventSponsors = _fundraisingEventSponsors.Where(SearchForTextInInstitutionalEntity).OrderBy(sortMethod).ToList();
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
            ApplyFundraisingEventSponsorsort();
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
                ApplyFundraisingEventSponsorsort();
            }
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

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            TrySearch();
        }

    }
}
