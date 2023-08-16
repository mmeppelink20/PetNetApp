/// <summary>
/// Oleksiy Fedchuk
/// Created: 2023/02/23
/// 
/// This is the Volunteer List Page
/// </summary>
/// <remarks>
/// Zaid Rachman
/// Updated: 2023/04/24
/// 
/// Final QA
/// </remarks>
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

namespace WpfPresentation.Events
{
    /// <summary>
    /// Interaction logic for VolunteerListPage.xaml
    /// </summary>
    public partial class VolunteerListPage : Page
    {
        private static VolunteerListPage _existingVolunteerList = null;

        private string _currentSearchText = "";
        private MasterManager _masterManager = MasterManager.GetMasterManager();
        private List<VolunteerVM> _volunteers = null;
        private List<VolunteerVM> _filteredVolunteers = null;
        private int _eventId = 0;
        private bool _needsReloaded = true;
        private static Regex _isDigit = new Regex(@"^\d+$");

        private int _currentPage = 1;
        private int _totalPages = 1;
        private int _itemsPerPage = 10;

        /// <summary>
        /// Oleksiy Fedchuk
        /// Created: 2023/02/23
        /// 
        /// Initializes the page
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/24
        /// 
        /// Final QA
        /// </remarks>
        public VolunteerListPage()
        {
            InitializeComponent();
            cboSort.SelectionChanged += ComboChanged;
        }

        /// <summary>
        /// Oleksiy Fedchuk
        /// Created: 2023/02/23
        /// 
        /// Gets a list of volunteers that are part of the event based off of the eventId that's passed
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/24
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public static VolunteerListPage GetVolunteerListPage(int eventId)
        {
            if (_existingVolunteerList == null)
            {
                _existingVolunteerList = new VolunteerListPage();
            }
            _existingVolunteerList._eventId = eventId;
            _existingVolunteerList.LoadVolunteerData();

            _existingVolunteerList._needsReloaded = false;
            return _existingVolunteerList;
        }

        /// <summary>
        /// Oleksiy Fedchuk
        /// Created: 2023/02/23
        /// 
        /// Loads the list of volunteers
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/24
        /// 
        /// Final QA
        /// </remarks>
        private void LoadVolunteerData()
        {
            try
            {
                _volunteers = _masterManager.VolunteerManager.RetrieveVolunteersbyFundraisingEventId(_eventId);
                ApplyVolunteerSort(false);
            }
            catch (Exception ex)
            {
                _volunteers = new List<VolunteerVM>();
                PromptWindow.ShowPrompt("Error", "Volunteers were unable to be added. \n" + ex.Message);
            }
        }
        /// <summary>
        /// Oleksiy Fedchuk
        /// Created: 2023/02/23
        /// 
        /// Applys the combo box sort option if applicable
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/24
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="resetPage"></param>
        private void ApplyVolunteerSort(bool resetPage = true)
        {
            Func<VolunteerVM, string> sortMethod = null;
            switch (((string)((ComboBoxItem)cboSort.SelectedValue).Content).ToLower())
            {
                case "alphabetically":
                    sortMethod = new Func<VolunteerVM, string>(fc => fc.GivenName);
                    break;
                default:
                    sortMethod = new Func<VolunteerVM, string>(fc => fc.FundraisingEventId.ToString());
                    break;
            }
            _filteredVolunteers = _volunteers.Where(SearchForTextInVolunteer).OrderBy(sortMethod).ToList();
            UpdateNavigationInformation();
            _currentPage = resetPage ? 1 : _currentPage > _totalPages ? _totalPages : _currentPage;
            UpdateUI();
        }
        /// <summary>
        /// Oleksiy Fedchuk
        /// Created: 2023/03/09
        /// 
        /// Checks names and compares it to the searched phrase
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/24
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="volunteerVM"></param>
        /// <returns></returns>
        private bool SearchForTextInVolunteer(VolunteerVM volunteerVM)
        {
            return volunteerVM.GivenName?.IndexOf(_currentSearchText, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    volunteerVM.FamilyName?.IndexOf(_currentSearchText, StringComparison.OrdinalIgnoreCase) >= 0;
        }
        /// <summary>
        /// Oleksiy Fedchuk
        /// Created: 2023/03/09
        /// 
        /// Updates the amount of pages are necessary for the list
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/24
        /// 
        /// Final QA
        /// </remarks>
        private void UpdateNavigationInformation()
        {
            _totalPages = (_filteredVolunteers.Count - 1) / _itemsPerPage + 1;
        }
        /// <summary>
        /// Oleksiy Fedchuk
        /// Created: 2023/03/09
        /// 
        /// </summary>
        /// 
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/4/20
        /// 
        /// Added a try-catch statement that grabs a campaigns title and changes lblTitle to include it.
        /// 
        /// Zaid Rachman
        /// Updated: 2023/04/24
        /// 
        /// Final QA
        /// </remarks>
        private void UpdateUI()
        {
            try
            {
                lblTitle.Content = "Volunteer List for " + _masterManager.FundraisingEventManager.RetrieveFundraisingEventByFundraisingEventId(_eventId).Title;
            }
            catch (Exception ex)
            {
                PromptWindow.ShowPrompt("Error", "Unable to grab the event title. \n" + ex.Message);
                return;
            }
            
            lblTotalPages.Content = "Total " + _totalPages + " page(s)";
            PopulateNavigationButtons();
            PopulateVolunteerList();
        }
        /// <summary>
        /// Oleksiy Fedchuk
        /// Created: 2023/03/09
        /// 
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/24
        /// 
        /// Final QA
        /// </remarks>
        private void PopulateNavigationButtons()
        {
            btnPrevious.Visibility = _currentPage == 1 ? Visibility.Collapsed : Visibility.Visible;
            btnNext.Visibility = _currentPage == _totalPages ? Visibility.Collapsed : Visibility.Visible;

            gridPageButtons.Children.Clear();
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
                gridPageButtons.Children.Add(currentPageButton);
            }
        }
        /// <summary>
        /// Oleksiy Fedchuk
        /// Created: 2023/03/09
        /// 
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/24
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="page"></param>
        private void NavigateToPage(int page)
        {
            _currentPage = page;
            UpdateUI();
        }
        /// <summary>
        /// Oleksiy Fedchuk
        /// Created: 2023/03/09
        /// 
        /// </summary>
        private void PopulateVolunteerList()
        {
            stackVolunteers.Children.Clear();
            if (_filteredVolunteers.Count == 0)
            {
                stackVolunteers.Visibility = Visibility.Collapsed;
                nothingToShowMessage.Visibility = Visibility.Visible;
            }
            else
            {
                stackVolunteers.Visibility = Visibility.Visible;
                nothingToShowMessage.Visibility = Visibility.Collapsed;
            }
            int i = 0;
            foreach (VolunteerVM volunteer in _filteredVolunteers.Skip(_itemsPerPage * (_currentPage - 1)).Take(_itemsPerPage))
            {
                UserControls.VolunteerListUserControl item = new UserControls.VolunteerListUserControl(volunteer, i % 2 == 0);
                i++;
                stackVolunteers.Children.Add(item);
            }
        }
        /// <summary>
        /// Oleksiy Fedchuk
        /// Created: 2023/03/09
        /// 
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/24
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrevious_Click(object sender, RoutedEventArgs e)
        {
            _currentPage--;
            UpdateUI();
        }
        /// <summary>
        /// Oleksiy Fedchuk
        /// Created: 2023/03/09
        /// 
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/24
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPageSearch_Click(object sender, RoutedEventArgs e)
        {
            NavigateToTypedPage();
        }
        /// <summary>
        /// Oleksiy Fedchuk
        /// Created: 2023/03/09
        /// 
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/24
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            _currentPage++;
            UpdateUI();
        }
        /// <summary>
        /// Oleksiy Fedchuk
        /// Created: 2023/03/09
        /// 
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/24
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            TrySearch();
        }
        /// <summary>
        /// Oleksiy Fedchuk
        /// Created: 2023/03/09
        /// 
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/24
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSort_Click(object sender, RoutedEventArgs e)
        {
            TrySearch();
        }
        /// <summary>
        /// Oleksiy Fedchuk
        /// Created: 2023/03/09
        /// 
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/24
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSearch_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtSearch.Text == "Search Name...")
            {
                txtSearch.Text = "";
            }
            else
            {
                txtSearch.Text = txtSearch.Text;
            }
        }
        /// <summary>
        /// Oleksiy Fedchuk
        /// Created: 2023/03/09
        /// 
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/24
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSearch_LostFocus(object sender, RoutedEventArgs e)
        {
            string tempText = txtSearch.Text;

            if (tempText == "")
            {
                txtSearch.Text = "Search Name...";
            }
            else
            {
                txtSearch.Text = tempText;
            }
        }
        /// <summary>
        /// Oleksiy Fedchuk
        /// Created: 2023/03/09
        /// 
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/24
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (_needsReloaded)
            {
                LoadVolunteerData();
                _needsReloaded = false;
            }
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            _needsReloaded = true;
        }
        /// <summary>
        /// Oleksiy Fedchuk
        /// Created: 2023/03/09
        /// 
        /// Checks if the inputed page is a valid page
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/24
        /// 
        /// Final QA
        /// </remarks>
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
        /// Oleksiy Fedchuk
        /// Created: 2023/03/09
        /// 
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/24
        /// 
        /// Final QA
        /// </remarks>
        private void NavigateToTypedPage()
        {
            if (IsValidPage(txtPageLookup.Text))
            {
                _currentPage = int.Parse(txtPageLookup.Text);
                UpdateUI();
            }
            else
            {
                txtPageLookup.Text = _currentPage.ToString();
            }
        }
        /// <summary>
        /// Oleksiy Fedchuk
        /// Created: 2023/03/09
        /// 
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/24
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPageLookup_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                NavigateToTypedPage();
            }
        }
        /// <summary>
        /// Oleksiy Fedchuk
        /// Created: 2023/03/09
        /// 
        /// Attempts to search for volunteers based off of what was typed
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/24
        /// 
        /// Final QA
        /// </remarks>
        private void TrySearch()
        {
            string newSearchText = txtSearch.Text.ToLower().Trim();
            if(!newSearchText.Equals("search name..."))
            {
                if (newSearchText != _currentSearchText)
                {
                    _currentSearchText = newSearchText;
                    ApplyVolunteerSort();
                }
            }
        }
        /// <summary>
        /// Oleksiy Fedchuk
        /// Created: 2023/03/09
        /// 
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/24
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                TrySearch();
            }
        }
        /// <summary>
        /// Oleksiy Fedchuk
        /// Created: 2023/03/09
        /// 
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/24
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboChanged(object sender, RoutedEventArgs e)
        {
            ApplyVolunteerSort();
        }
        /// <summary>
        /// Oleksiy Fedchuk
        /// Created: 2023/04/20
        /// 
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/24
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddVolunteer_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(AddVolunteerToEvent.GetAddVolunteerToEvent(_eventId));
        }
    }
}
