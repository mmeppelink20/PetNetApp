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
    /// Interaction logic for ViewFundraisingEventPage.xaml
    /// </summary>
    public partial class ViewFundraisingEventsPage : Page
    {

        private static ViewFundraisingEventsPage _existingViewFundraisingEventsPage = null;

        // setup
        private string _currentSearchText = "";
        private MasterManager _masterManager = MasterManager.GetMasterManager();
        private bool _needsReloaded = true;
        private List<FundraisingEventVM> _fundraisingEvents = null;
        private List<FundraisingEventVM> _filteredFundraisingEvents = null;
        private static Regex _isDigit = new Regex(@"^\d+$");

        // page navigation
        private int _currentPage = 1;
        private int _totalPages = 1;
        private int _itemsPerPage = 10;

        public ViewFundraisingEventsPage()
        {
            InitializeComponent();
            cbFilter.SelectionChanged += comboChanged;
            cbSort.SelectionChanged += comboChanged;
        }
        /// <summary>
        /// Barry Mikulas
        /// Created 2023/03/05
        /// 
        /// Gets the existing FundraisingEventsPage or new if it doesn't exist.
        /// </summary>
        /// <returns></returns>
        public static ViewFundraisingEventsPage GetViewFundraisingEvents()
        {
            if (_existingViewFundraisingEventsPage == null)
            {
                _existingViewFundraisingEventsPage = new ViewFundraisingEventsPage();
            }
            _existingViewFundraisingEventsPage.LoadFundraisingEventsData();

            _existingViewFundraisingEventsPage._needsReloaded = false;

            return _existingViewFundraisingEventsPage;
        }
        private void UpdateUI()
        {
            PopulateNavigationButtons();
            PopulateEventList();
        }
        private void LoadFundraisingEventsData()
        {
            try
            {
                _fundraisingEvents = _masterManager.FundraisingEventManager.RetrieveAllFundraisingEventsByShelterId((int)_masterManager.User.ShelterId);
            }
            catch (Exception ex)
            {
                _fundraisingEvents = new List<FundraisingEventVM>();
                PromptWindow.ShowPrompt("Error", ex.Message);
            }
            ApplyFundraisingEventFilterAndSort(false);
        }
        private void ApplyFundraisingEventFilterAndSort(bool resetPage = true)
        {
            Func<FundraisingEventVM, string> sortMethod = null;
            switch (((string)((ComboBoxItem)cbSort.SelectedValue).Content).ToLower())
            {
                case "title":
                    sortMethod = new Func<FundraisingEvent, string>(fe => fe.Title);
                    break;
                case "start date":
                    sortMethod = new Func<FundraisingEvent, string>(fe => fe.StartTime != null ? fe.StartTime.Value.ToString("yyyy MM dd") : "");
                    break;
                default:
                    sortMethod = new Func<FundraisingEvent, string>(fc => fc.FundraisingEventId.ToString());
                    break;
            }
            Func<FundraisingEventVM, bool> filterMethod = null;
            switch (((string)((ComboBoxItem)cbFilter.SelectedValue).Content).ToLower())
            {
                case "completed":
                    filterMethod = new Func<FundraisingEvent, bool>(fe => fe.Complete && !fe.Hidden);
                    break;
                case "both":
                    filterMethod = new Func<FundraisingEvent, bool>(fe => !fe.Complete || fe.Complete);
                    break;
                case "hidden":
                    filterMethod = new Func<FundraisingEventVM, bool>(fe => fe.Hidden);
                    break;
                case "visible":
                    filterMethod = new Func<FundraisingEventVM, bool>(fe => !fe.Hidden);
                    break;
                case "ongoing":
                default:
                    filterMethod = new Func<FundraisingEvent, bool>(fc => !fc.Complete && !fc.Hidden);
                    break;
            }
            _filteredFundraisingEvents = _fundraisingEvents.Where(filterMethod).Where(SearchForTextInFundraisingEvent).OrderBy(sortMethod).ToList();
            UpdateNavigationInformation();
            _currentPage = resetPage ? 1 : _currentPage > _totalPages ? _totalPages : _currentPage;
            UpdateUI();
        }
        private bool SearchForTextInFundraisingEvent(FundraisingEventVM fundraisingEvent)
        {
            return fundraisingEvent.Title?.IndexOf(_currentSearchText, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    fundraisingEvent.Description?.IndexOf(_currentSearchText, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    (fundraisingEvent.StartTime != null ? fundraisingEvent.StartTime.Value.ToString("MM/dd/yyyy").Contains(_currentSearchText) : false) ||
                    (fundraisingEvent.EndTime != null ? fundraisingEvent.EndTime.Value.ToString("MM/dd/yyyy").Contains(_currentSearchText) : false) ||
                    (fundraisingEvent.StartTime != null ? fundraisingEvent.StartTime.Value.ToString("M/d/yyyy").Contains(_currentSearchText) : false) ||
                    (fundraisingEvent.EndTime != null ? fundraisingEvent.EndTime.Value.ToString("M/d/yyyy").Contains(_currentSearchText) : false);
        }
        private void UpdateNavigationInformation()
        {
            _totalPages = (_filteredFundraisingEvents.Count - 1) / _itemsPerPage + 1;
        }
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
        private void NavigateToPage(int page)
        {
            _currentPage = page;
            UpdateUI();
        }
        private void PopulateEventList()
        {
            stackEvents.Children.Clear();
            if (_filteredFundraisingEvents.Count == 0)
            {
                stackEvents.Visibility = Visibility.Collapsed;
                nothingToShowMessage.Visibility = Visibility.Visible;
            }
            else
            {
                stackEvents.Visibility = Visibility.Visible;
                nothingToShowMessage.Visibility = Visibility.Collapsed;
            }
            int i = 0;
            foreach (FundraisingEventVM fundraisingEvent in _filteredFundraisingEvents.Skip(_itemsPerPage * (_currentPage - 1)).Take(_itemsPerPage))
            {
                ViewEventsFundraisingEventUserControl item = new ViewEventsFundraisingEventUserControl(fundraisingEvent, i % 2 == 0);
                item.EventDeleted += () =>
                {
                    LoadFundraisingEventsData();
                };
                i++;
                stackEvents.Children.Add(item);
            }
        }
        private void btnNextPage_Click(object sender, RoutedEventArgs e)
        {
            _currentPage++;
            UpdateUI();
        }
        private void btnPreviousPage_Click(object sender, RoutedEventArgs e)
        {
            _currentPage--;
            UpdateUI();
        }
        private void btnAddEvent_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new WpfPresentation.Events.AddFundraisingEvent());
        }
        private void btnNavigatePage_Click(object sender, RoutedEventArgs e)
        {
            NavigateToTypedPage();
        }
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
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            TrySearch();
        }
        private void comboChanged(object sender, RoutedEventArgs e)
        {
            ApplyFundraisingEventFilterAndSort();
        }
        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            _needsReloaded = true;
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (_needsReloaded)
            {
                LoadFundraisingEventsData();
                _needsReloaded = false;
            }
        }
        private void btnFirstPage_Click(object sender, RoutedEventArgs e)
        {
            _currentPage = 1;
            UpdateUI();
        }
        private void btnLastPage_Click(object sender, RoutedEventArgs e)
        {
            _currentPage = _totalPages;
            UpdateUI();
        }
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
        private void tbPage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                NavigateToTypedPage();
            }
        }
        private void tbSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                TrySearch();
            }
        }
        private void TrySearch()
        {
            string newSearchText = tbSearch.Text.ToLower().Trim();
            if (newSearchText != _currentSearchText)
            {
                _currentSearchText = newSearchText;
                ApplyFundraisingEventFilterAndSort();
            }
        }
    }
}
