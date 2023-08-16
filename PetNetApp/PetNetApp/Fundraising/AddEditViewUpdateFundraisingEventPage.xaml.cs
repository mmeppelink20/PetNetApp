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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfPresentation.Fundraising;
using WpfPresentation.UserControls;
using System.Text.RegularExpressions;

namespace WpfPresentation.Fundraising
{
    /// <summary>
    /// Interaction logic for AddEditFundraisingEventPage.xaml
    /// </summary>
    public partial class AddEditViewUpdateFundraisingEventPage : Page
    {
        private static AddEditViewUpdateFundraisingEventPage _existingAddEditViewUpdateFundraisingEventPage = null;

        private MasterManager _masterManager = MasterManager.GetMasterManager();
        private PageMode _pageMode;
        private static decimal _amtRaised = 0.00m;

        private FundraisingEventVM _oldFundraisingEventVM = null;

        public FundraisingEventVM FundraisingEvent
        {
            get { return (FundraisingEventVM)GetValue(FundraisingEventProperty); }
            set { SetValue(FundraisingEventProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FundraisingEvent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FundraisingEventProperty =
            DependencyProperty.Register("FundraisingEvent", typeof(FundraisingEventVM), typeof(AddEditViewUpdateFundraisingEventPage), new PropertyMetadata(null));


        /// <summary>
        /// Barry Mikulas
        /// Created: 2023/03/14
        /// 
        /// Default page constructor.
        /// 
        /// </summary>
        public AddEditViewUpdateFundraisingEventPage()
        {
            DataContext = this;
            InitializeComponent();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fundraisingEvent"></param>
        public AddEditViewUpdateFundraisingEventPage(FundraisingEventVM fundraisingEvent)
        {
            SetupViewFundraisingEvent(fundraisingEvent);
        }


        /// <summary>
        /// Barry Mikulas
        /// Created 2023/03/05
        /// </summary>
        /// <param name="fundraisingEvent">The fundraising campaign to view</param>
        /// <returns>a new or existing fundraising campaign page set up to view the event</returns>
        public static AddEditViewUpdateFundraisingEventPage GetViewFundraisingEventPage(FundraisingEventVM fundraisingEvent)
        {
            try
            {
                GetSponsorsContactsHostPets(fundraisingEvent);
                //TODO: get pet list
            }
            catch (Exception ex)
            {
                PromptWindow.ShowPrompt("Error", ex.Message);
                return null;
            }
            if (_existingAddEditViewUpdateFundraisingEventPage == null)
            {
                _existingAddEditViewUpdateFundraisingEventPage = new AddEditViewUpdateFundraisingEventPage();
            }
            _existingAddEditViewUpdateFundraisingEventPage.SetupViewFundraisingEvent(fundraisingEvent);


            return _existingAddEditViewUpdateFundraisingEventPage;
        }

        /// <summary>
        /// Barry Mikulas
        /// Created: 2023/03/14
        /// 
        /// Update mode only allows changes to all but Event Cost, Number of Attendees, Number of Animals Adopted. These are changed through update.
        /// </summary>
        /// <param name="fundraisingEvent">The fundraising event to edit</param>
        /// <returns>New or exisiting fundraising event set up to edit the event</returns>
        public static AddEditViewUpdateFundraisingEventPage GetEditFundraisingEventPage(FundraisingEventVM fundraisingEvent)
        {
            try
            {
                GetSponsorsContactsHostPets(fundraisingEvent);
                // get pet list

            }
            catch (Exception ex)
            {
                PromptWindow.ShowPrompt("Error", ex.Message);
                return null;
            }
            if (_existingAddEditViewUpdateFundraisingEventPage == null)
            {
                _existingAddEditViewUpdateFundraisingEventPage = new AddEditViewUpdateFundraisingEventPage();
            }
            _existingAddEditViewUpdateFundraisingEventPage.setupEditFundraisingEvent(fundraisingEvent);


            return _existingAddEditViewUpdateFundraisingEventPage;
        }

        /// <summary>
        /// Barry Mikulas
        /// Created: 2023/03/14
        /// 
        /// Update mode only allows changes to  Notes, Event Cost, Number of Attendees, Number of Animals Adopted
        /// </summary>
        /// <param name="fundraisingEvent">The fundraising event to update</param>
        /// <returns>New or exisiting fundraising event set up to update the event</returns>
        public static AddEditViewUpdateFundraisingEventPage GetUpdateFundraisingEventPage(FundraisingEventVM fundraisingEvent)
        {
            try
            {
                GetSponsorsContactsHostPets(fundraisingEvent);
                // get pet list
            }
            catch (Exception ex)
            {
                PromptWindow.ShowPrompt("Error", ex.Message);
                return null;
            }
            if (_existingAddEditViewUpdateFundraisingEventPage == null)
            {
                _existingAddEditViewUpdateFundraisingEventPage = new AddEditViewUpdateFundraisingEventPage();
            }
            _existingAddEditViewUpdateFundraisingEventPage.setupUpdateFundraisingEvent(fundraisingEvent);


            return _existingAddEditViewUpdateFundraisingEventPage;
        }
        /// <summary>
        /// Barry Mikulas
        /// Created: 2023/03/14
        /// 
        /// Method retrieves information to complete the FunraisinfEventVM.
        /// 
        /// </summary>
        /// <param name="fundraisingEvent"></param>
        private static void GetSponsorsContactsHostPets(FundraisingEventVM fundraisingEvent)
        {
            // get sponsors
            fundraisingEvent.Sponsors = MasterManager.GetMasterManager().InstitutionalEntityManager.RetrieveFundraisingInstitutionalEntitiesByEventIdAndContactType(fundraisingEvent.FundraisingEventId, "Sponsor");
            // get contacts
            fundraisingEvent.Contacts = MasterManager.GetMasterManager().InstitutionalEntityManager.RetrieveFundraisingInstitutionalEntitiesByEventIdAndContactType(fundraisingEvent.FundraisingEventId, "Contact");
            // get host
            fundraisingEvent.Host = MasterManager.GetMasterManager().InstitutionalEntityManager.RetrieveInstitutionalEntityByEventIdAndContactType(fundraisingEvent.FundraisingEventId, "Host");
            if (fundraisingEvent.Host == null || fundraisingEvent.Host.InstitutionalEntityId == 0)
            {
                fundraisingEvent.Host = new InstitutionalEntity()
                {
                    Address = "Host needs to be added.",
                    CompanyName = "Host needs to be added."
                };
            }
            // get campaign
            if (fundraisingEvent.CampaignId != null)
            {
                fundraisingEvent.Campaign = MasterManager.GetMasterManager().FundraisingCampaignManager.RetrieveFundraisingCampaignByFundraisingCampaignId((int)fundraisingEvent.CampaignId);
            }
            else
            {
                fundraisingEvent.Campaign = new FundraisingCampaignVM();
                fundraisingEvent.Campaign.Title = "Campaign needs to be added.";
            }
            // get animals for the event
            fundraisingEvent.Animals = MasterManager.GetMasterManager().AnimalManager.RetrieveAnimalsByFundrasingEventId((int)fundraisingEvent.FundraisingEventId);

            // get donations for the event
            _amtRaised = MasterManager.GetMasterManager().DonationManager.RetrieveSumDonationsByEventId((int)fundraisingEvent.FundraisingEventId);
        }

        /// <summary>
        /// Barry Mikulas
        /// Created: 2023/03/14
        /// </summary>
        /// <returns>An empty page to create a new fundraising event</returns>
        public static AddEditViewUpdateFundraisingEventPage GetAddFundraisingEventPage()
        {
            if (_existingAddEditViewUpdateFundraisingEventPage == null)
            {
                _existingAddEditViewUpdateFundraisingEventPage = new AddEditViewUpdateFundraisingEventPage();
            }
            _existingAddEditViewUpdateFundraisingEventPage.setupNewFundraisingEvent();
            return _existingAddEditViewUpdateFundraisingEventPage;
        }

        /// <summary>
        /// Barry Mikulas
        /// Created on: 2023/04/03
        /// 
        /// Prepares UI for edit mode
        /// 
        /// </summary>
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            //PromptWindow.ShowPrompt("Edit", "Edit Clicked");
            //setupEditFundraisingEvent(FundraisingEvent);
            NavigationService.GetNavigationService(this).Navigate(new WpfPresentation.Events.EditFundraisingEvent(FundraisingEvent));
        }
        /// <summary>
        /// Barry Mikulas
        /// Created on: 2023/04/03
        /// 
        /// Prepares UI for Update mode
        /// 
        /// </summary>
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            setupUpdateFundraisingEvent(FundraisingEvent);
        }
        /// <summary>
        /// Barry Mikulas
        /// Created on: 2023/04/03
        /// 
        /// Closes view mode of UI and returns to events list
        /// 
        /// </summary>
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(ViewFundraisingEventsPage.GetViewFundraisingEvents());
        }
        /// <summary>
        /// Barry Mikulas
        /// Created on: 2023/04/03
        /// 
        /// Ends the addition\edit\update of a view
        /// Returns to event list on new.
        /// Returns to view mode of event on edit and update.
        /// 
        /// </summary>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            switch (_pageMode)
            {
                case PageMode.New:
                    NavigationService.Navigate(ViewFundraisingEventsPage.GetViewFundraisingEvents());
                    break;
                case PageMode.Edit:
                    SetupViewFundraisingEvent(_oldFundraisingEventVM);
                    break;
                case PageMode.Update:
                    SetupViewFundraisingEvent(_oldFundraisingEventVM);
                    break;
                default:
                    break;
            }

        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            switch (_pageMode)
            {
                // different save methods as update does not allow edits on only 4 fields
                case PageMode.New:
                    PromptWindow.ShowPrompt("Not Implemented", "Save feature is not implemented for adding new events.");
                    break;
                case PageMode.Edit:
                    PromptWindow.ShowPrompt("Not Implemented", "Save feature is not implemented for saving event edits.");
                    break;
                case PageMode.Update:
                    SaveEventResultUpdates();
                    break;
                case PageMode.View:
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Barry Mikulas
        /// Created on: 03/30/2023
        /// 
        /// Will validate event results fields and then save to database
        /// 
        /// </summary>
        private void SaveEventResultUpdates()
        {
            if (!ValidateFundraisingEventResults())
            {
                return;
            }
            try
            {
                if (!_masterManager.FundraisingEventManager.EditFundraisingEventResults(_oldFundraisingEventVM, FundraisingEvent))
                {
                    PromptWindow.ShowPrompt("Error", "The fundraising result update was not saved");
                    ReloadFundraisingEventInViewMode();
                    return;
                }
                PromptWindow.ShowPrompt("Update Saved", FundraisingEvent.Title + "  event has been updated.");
                SetupViewFundraisingEvent(FundraisingEvent);
            }
            catch (Exception ex)
            {
                PromptWindow.ShowPrompt("Error", ex.Message);
            }
            ReloadUI();

        }


        /// <summary>
        /// Barry Mikulas
        /// Created on: 2023/04/03
        /// 
        /// refreshes the UI by faking a change in the fundraising event property
        /// so all bindings are redrawn in the wpf
        /// 
        /// </summary>
        private void ReloadUI()
        {
            var temp = FundraisingEvent;
            FundraisingEvent = null;
            FundraisingEvent = temp;
        }

        /// <summary>
        /// Barry Mikulas
        /// Created on: 04/03/2023
        /// 
        /// Reloads a campaign event after a failed update save.
        /// Usually update save fails when changes made to a fundraising event 
        /// during the time the user was updating the event.
        /// 
        /// </summary>
        private void ReloadFundraisingEventInViewMode()
        {
            var freshCampaignEventLoad = _masterManager.FundraisingEventManager.RetrieveFundraisingEventByFundraisingEventId(_oldFundraisingEventVM.FundraisingEventId);
            GetSponsorsContactsHostPets(freshCampaignEventLoad);
            SetupViewFundraisingEvent(freshCampaignEventLoad);
        }

        /// <summary>
        /// Barry Mikulas
        /// Created on: 04/03/2023
        /// 
        /// 
        /// </summary>
        /// <returns>bool true if event results fields validate</returns>
        private bool ValidateFundraisingEventResults()
        {
            bool updateDataValid = true;
            decimal eventCost = 0.00m;
            int numOfAttendees = 0;
            int numAnimalsAdopted = 0;

            if (!FundraisingEvent.UpdateNotes.IsValidLongDescription())
            {
                lblNotesError.Visibility = Visibility.Visible;
                updateDataValid = false;
            }
            else
            {
                lblNotesError.Visibility = Visibility.Collapsed;
            }

            // Parse and validate event cost
            if (!decimal.TryParse(tbEventCost.Text, out eventCost))
            {
                DisplayResultError(1);
                return false;
            }
            else
            {
                if (eventCost < 0 || eventCost > 9999.99m)
                {
                    DisplayResultError(1);
                    return false;
                }
                else
                {
                    lblEventCostError.Visibility = Visibility.Collapsed;
                }
            }

            //parse and validate number of attendees
            if (!int.TryParse(tbNumAttendees.Text, out numOfAttendees))
            {
                DisplayResultError(2);
                return false;
            }
            else
            {
                if (numOfAttendees < 0 || numOfAttendees > 9999)
                {
                    DisplayResultError(2);
                    return false;
                }
                else
                {
                    lblNumAttendeesError.Visibility = Visibility.Collapsed;
                }
            }

            //parse and validate number of animals adopted
            if (!int.TryParse(tbNumAnimalsAdopted.Text, out numAnimalsAdopted))
            {
                DisplayResultError(3);
                return false;
            }
            else
            {
                if (numAnimalsAdopted < 0 || numAnimalsAdopted > 999)
                {
                    DisplayResultError(3);
                    return false;
                }
                else
                {
                    lblNumAnimalsAdoptedError.Visibility = Visibility.Collapsed;
                }
            }


            return updateDataValid;
        }

        /// <summary>
        /// Barry Mikulas
        /// Created on: 04/03/2023
        /// 
        ///  Helper  method that displays one of 3 UI error messages 
        ///  and hides the others
        /// 
        /// </summary>
        private void DisplayResultError(int errorNum)
        {
            switch (errorNum)
            {
                case 1:
                    lblEventCostError.Visibility = Visibility.Visible;
                    lblNumAttendeesError.Visibility = Visibility.Collapsed;
                    lblNumAnimalsAdoptedError.Visibility = Visibility.Collapsed;
                    break;
                case 2:
                    lblEventCostError.Visibility = Visibility.Collapsed;
                    lblNumAttendeesError.Visibility = Visibility.Visible;
                    lblNumAnimalsAdoptedError.Visibility = Visibility.Collapsed;
                    break;
                case 3:
                    lblEventCostError.Visibility = Visibility.Collapsed;
                    lblNumAttendeesError.Visibility = Visibility.Collapsed;
                    lblNumAnimalsAdoptedError.Visibility = Visibility.Visible;
                    break;
                default:
                    break;
            }

        }

        /// <summary>
        /// Barry Mikulas
        /// Created on: 2023/04/03
        /// 
        /// Opens add host dialog - not implemented as add host done in othe area
        /// 
        /// </summary>
        private void btnAddHost_Click(object sender, RoutedEventArgs e)
        {
            PromptWindow.ShowPrompt("Add Host", "Add Host Not implemented");
        }
        /// <summary>
        /// Barry Mikulas
        /// Created on: 2023/04/03
        /// 
        /// Opens add campaign dialog - not implemented as add host done in othe area
        /// 
        /// </summary>
        private void btnAddCampaign_Click(object sender, RoutedEventArgs e)
        {
            PromptWindow.ShowPrompt("Add Campaign", "Add Campaign Not implemented");
        }
        /// <summary>
        /// Barry Mikulas
        /// Created on: 2023/04/03
        /// 
        /// Opens add sponsor dialog - not implemented as add host done in othe area
        /// 
        /// </summary>
        private void btnAddSponsors_Click(object sender, RoutedEventArgs e)
        {
            PromptWindow.ShowPrompt("Not Implemented", "Add sponsor is not fully implemented.");
            // return;
            AddFundraisingCampaignSponsorsWindow addFundraisingCampaignSponsorsWindow = new AddFundraisingCampaignSponsorsWindow(FundraisingEvent.Sponsors, "Sponsor");
            addFundraisingCampaignSponsorsWindow.Owner = Window.GetWindow(this);
            addFundraisingCampaignSponsorsWindow.ShowDialog();
            ClearAndPopulateContactType("Sponsor");

        }
        /// <summary>
        /// Barry Mikulas
        /// Created on: 2023/04/03
        /// 
        /// Opens add contact dialog - not implemented as add host done in othe area
        /// 
        /// </summary>
        private void btnAddContacts_Click(object sender, RoutedEventArgs e)
        {
            PromptWindow.ShowPrompt("Not Implemented", "Add contact is not fully implemented.");
            //return;
            AddFundraisingCampaignSponsorsWindow addFundraisingCampaignSponsorsWindow = new AddFundraisingCampaignSponsorsWindow(FundraisingEvent.Contacts, "Contact");
            addFundraisingCampaignSponsorsWindow.Owner = Window.GetWindow(this);
            addFundraisingCampaignSponsorsWindow.ShowDialog();
            ClearAndPopulateContactType("Contact");

        }
        /// <summary>
        /// Barry Mikulas
        /// Created on: 2023/04/03
        /// 
        /// Opens add pet dialog - not implemented as add host done in othe area
        /// 
        /// </summary>
        private void btnAddPet_Click(object sender, RoutedEventArgs e)
        {
            PromptWindow.ShowPrompt("Not Implemented", "Add pet is not implemented.");

        }

        /// <summary>
        /// Barry Mikulas
        /// Created on: 2023/04/03
        /// 
        /// Sets up page for addition of a new event.
        /// 
        /// </summary>
        private void setupNewFundraisingEvent()
        {
            _pageMode = PageMode.New;
            lblHeader.Content = "New Fundraising Event";
            FundraisingEvent = new FundraisingEventVM()
            {
                UsersId = _masterManager.User.UsersId,
                ShelterId = _masterManager.User.ShelterId.Value,
                Sponsors = new List<InstitutionalEntity>(),
                Contacts = new List<InstitutionalEntity>(),
                Host = new InstitutionalEntity(),
                Hidden = false,
                Complete = false,
                Cost = 0m,
                NumOfAttendees = 0,
                NumAnimalsAdopted = 0
            };
            tbAmountRaised.Text = "TBD";
            AddEditMode();
            SaveCloseButtonsEnabled();
        }
        /// <summary>
        /// Barry Mikulas
        /// Created on: 2023/04/03
        /// 
        /// Sets up page for viewing of an exisitng event.
        /// 
        /// </summary>
        private void SetupViewFundraisingEvent(FundraisingEventVM fundraisingEvent)
        {
            _pageMode = PageMode.View;
            lblHeader.Content = "View Fundraising Event #" + fundraisingEvent.FundraisingEventId;
            FundraisingEvent = fundraisingEvent;
            datPets.ItemsSource = fundraisingEvent.Animals;
            tbAmountRaised.Text = _amtRaised == 0.00m ? "N\\A" : _amtRaised.ToString("c");
            ViewMode();
            EditUpdateDeleteButtonsEnabled();
            ClearAndPopulateContactType("Sponsor");
            ClearAndPopulateContactType("Contact");
            //add pets
        }
        /// <summary>
        /// Barry Mikulas
        /// Created on: 2023/04/03
        /// 
        /// Sets up page for editing of an exisitng event.
        /// 
        /// </summary>
        private void setupEditFundraisingEvent(FundraisingEventVM fundraisingEvent)
        {
            _pageMode = PageMode.Edit;
            lblHeader.Content = "Edit Fundraising Event #" + fundraisingEvent.FundraisingEventId;
            FundraisingEvent = fundraisingEvent.Copy();
            _oldFundraisingEventVM = fundraisingEvent;
            datPets.ItemsSource = fundraisingEvent.Animals;
            tbAmountRaised.Text = _amtRaised == 0.00m ? "N\\A" : _amtRaised.ToString("c");
            AddEditMode();
            SaveCloseButtonsEnabled();
            ClearAndPopulateContactType("Sponsor");
            ClearAndPopulateContactType("Contact");
            //add pets
        }
        /// <summary>
        /// Barry Mikulas
        /// Created on: 2023/04/03
        /// 
        /// Sets up page for updating of an exisitng event.
        /// 
        /// </summary>
        private void setupUpdateFundraisingEvent(FundraisingEventVM fundraisingEvent)
        {
            _pageMode = PageMode.Update;
            lblHeader.Content = "Update Fundraising Event #" + fundraisingEvent.FundraisingEventId;
            FundraisingEvent = fundraisingEvent.Copy();
            _oldFundraisingEventVM = fundraisingEvent;
            datPets.ItemsSource = fundraisingEvent.Animals;
            tbAmountRaised.Text = _amtRaised == 0.00m ? "N\\A" : _amtRaised.ToString("c");
            UpdateMode();
            SaveCloseButtonsEnabled();
            ClearAndPopulateContactType("Sponsor");
            ClearAndPopulateContactType("Contact");
            //add pets
        }

        /// <summary>
        /// Barry Mikulas
        /// Created on: 2023/04/03
        /// 
        /// loads the contact and sponsor for view and edit mode
        /// 
        /// </summary>
        /// <remarks>
        /// Updated:
        /// Barry Mikulas
        /// 2023/04/27 - added New User control that shows Company name and added BTNView click to show details
        /// </remarks>
        private void ClearAndPopulateContactType(string contactType)
        {
            switch (contactType)
            {
                case "Sponsor":
                    stackSponsors.Children.Clear();
                    foreach (var sponsor in FundraisingEvent.Sponsors)
                    {
                        
                        var sponsorControl = new InstitutionalEntityCompanyNameUserControl(sponsor, !(_pageMode != PageMode.Edit), false);
                        sponsorControl.btnView.Click += (obj, arg) => BTNView_Click(sponsor, "Sponsor");
                        stackSponsors.Children.Add(sponsorControl);
                    }
                    break;
                case "Contact":
                    stackContacts.Children.Clear();
                    foreach (var contact in FundraisingEvent.Contacts)
                    {
                        var sponsorControl = new InstitutionalEntityCompanyNameUserControl(contact, !(_pageMode != PageMode.Edit), false);
                        sponsorControl.btnView.Click += (obj, arg) => BTNView_Click(contact, "Contact");
                        stackContacts.Children.Add(sponsorControl);
                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Barry Mikulas
        /// Created: 2023/04/27
        /// 
        /// Used to display Institutional Entity details when clicking view in the user control
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="entityType"></param>
        private void BTNView_Click(InstitutionalEntity entity, string entityType)
        {
            PromptWindow.ShowPrompt(entityType + " Detail", entity.CompanyName + "\n"
                 + "-----------------\n" 
                 + entity.GivenName + " " + entity.FamilyName + "\n"
                 + "-----------------\n"
                 + entity.Email + "\n"
                 + "-----------------\n"
                 + Regex.Replace(entity.Phone, @"(\d{3})(\d{3})(\d{4})", "($1) $2-$3")
                 );
        }

        /// <summary>
        /// Barry Mikulas
        /// Created on: 2023/04/03
        /// 
        /// Removed sponsor from list when remove clicked in add sponsor popup
        /// 
        /// </summary>
        private void btnRemoveSponsor_Click(object sender, RoutedEventArgs args, InstitutionalEntity institutionalEntity)
        {
            FundraisingEvent.Sponsors.Remove(institutionalEntity);
            ClearAndPopulateContactType("Sponsor"); ;
        }
        /// <summary>
        /// Barry Mikulas
        /// Created on: 2023/04/03
        /// 
        /// Removed contact from list when remove clicked in add sponsor popup
        /// 
        /// </summary>        
        private void btnRemoveContact_Click(object sender, RoutedEventArgs args, InstitutionalEntity institutionalEntity)
        {
            FundraisingEvent.Contacts.Remove(institutionalEntity);
            ClearAndPopulateContactType("Contact"); ;
        }

        /// <summary>
        /// Barry Mikulas
        /// Created on: 2023/04/03
        /// 
        /// sets up page for view mode.
        /// 
        /// </summary> 
        private void ViewMode()
        {
            tbEventTitle.IsEnabled = false;
            tbHost.IsEnabled = false;
            tbCampaign.IsEnabled = false;
            btnAddCampaign.IsEnabled = false;
            btnAddCampaign.Visibility = Visibility.Collapsed;
            btnAddHost.IsEnabled = false;
            btnAddHost.Visibility = Visibility.Collapsed;
            tbAddress.IsEnabled = false;
            dpStartTime.IsEnabled = false;
            dpEndTime.IsEnabled = false;
            tbDescription.IsEnabled = false;
            tbAdditionalInfo.IsEnabled = false;
            tbNotes.IsEnabled = false;
            tbAmountRaised.IsEnabled = false;
            tbEventCost.IsEnabled = false;
            tbNumAttendees.IsEnabled = false;
            tbNumAnimalsAdopted.IsEnabled = false;
            btnAddContacts.IsEnabled = false;
            btnAddPet.IsEnabled = false;
            btnAddSponsors.IsEnabled = false;
            gridDescriptionContacts.Visibility = Visibility.Visible;
            gridSponsorsPets.Visibility = Visibility.Visible;
            ckbComplete.Visibility = Visibility.Visible;
            ckbComplete.IsEnabled = false;
            lblHeader.Focus();
        }
        /// <summary>
        /// Barry Mikulas
        /// Created on: 2023/04/03
        /// 
        /// sets up page for add and edit modes.
        /// 
        /// </summary> 
        private void AddEditMode()
        {
            tbEventTitle.IsEnabled = true;
            tbHost.IsEnabled = false;
            tbCampaign.IsEnabled = false;
            btnAddCampaign.IsEnabled = true;
            btnAddCampaign.Visibility = Visibility.Visible;
            btnAddHost.IsEnabled = true;
            btnAddHost.Visibility = Visibility.Visible;
            tbAddress.IsEnabled = false;
            dpStartTime.IsEnabled = true;
            dpEndTime.IsEnabled = true;
            tbDescription.IsEnabled = true;
            tbAdditionalInfo.IsEnabled = true;
            tbNotes.IsEnabled = false;
            tbAmountRaised.IsEnabled = false;
            tbEventCost.IsEnabled = false;
            tbNumAttendees.IsEnabled = false;
            tbNumAnimalsAdopted.IsEnabled = false;
            btnAddContacts.IsEnabled = true;
            btnAddPet.IsEnabled = true;
            btnAddSponsors.IsEnabled = true;
            btnAddContacts.Visibility = Visibility.Visible;
            btnAddPet.Visibility = Visibility.Visible;
            btnAddSponsors.Visibility = Visibility.Visible;
            gridDescriptionContacts.Visibility = Visibility.Visible;
            gridSponsorsPets.Visibility = Visibility.Visible;
            ckbComplete.Visibility = Visibility.Hidden;
            ckbComplete.IsEnabled = false;
            tbEventTitle.Focus();
            tbEventTitle.SelectAll();
        }
        /// <summary>
        /// Barry Mikulas
        /// Created on: 2023/04/03
        /// 
        /// sets up page for update mode.
        /// 
        /// </summary> 
        private void UpdateMode()
        {
            tbEventTitle.IsEnabled = false;
            tbHost.IsEnabled = false;
            tbCampaign.IsEnabled = false;
            btnAddCampaign.IsEnabled = false;
            btnAddCampaign.Visibility = Visibility.Collapsed;
            btnAddHost.IsEnabled = false;
            btnAddHost.Visibility = Visibility.Collapsed;
            tbAddress.IsEnabled = false;
            dpStartTime.IsEnabled = false;
            dpEndTime.IsEnabled = false;
            tbDescription.IsEnabled = false;
            tbAdditionalInfo.IsEnabled = false;
            tbNotes.IsEnabled = true;
            tbAmountRaised.IsEnabled = false;
            tbEventCost.IsEnabled = true;
            tbNumAttendees.IsEnabled = true;
            tbNumAnimalsAdopted.IsEnabled = true;
            btnAddContacts.IsEnabled = false;
            btnAddPet.IsEnabled = false;
            btnAddSponsors.IsEnabled = false;
            btnAddContacts.Visibility = Visibility.Collapsed;
            btnAddPet.Visibility = Visibility.Collapsed;
            btnAddSponsors.Visibility = Visibility.Collapsed;
            gridDescriptionContacts.Visibility = Visibility.Collapsed;
            gridSponsorsPets.Visibility = Visibility.Collapsed;
            ckbComplete.Visibility = Visibility.Visible;
            ckbComplete.IsEnabled = true;
            tbEventCost.Focus();
            tbEventCost.SelectAll();
        }

        /// <summary>
        /// Barry Mikulas
        /// Created on: 2023/04/03
        /// 
        /// Sets up button panels on page for edit, add, and update modes.
        /// 
        /// </summary> 
        private void SaveCloseButtonsEnabled()
        {
            if (_pageMode == PageMode.Update)
            {
                stackSaveCancel.IsEnabled = false;
                stackSaveCancel.Visibility = Visibility.Collapsed;
            }
            else
            {
                stackSaveCancel.IsEnabled = true;
                stackSaveCancel.Visibility = Visibility.Visible;
            }
            stackEditUpdateDelete.IsEnabled = false;
            stackEditUpdateDelete.Visibility = Visibility.Collapsed;
            stackSaveCancelBottom.IsEnabled = true;
            stackSaveCancelBottom.Visibility = Visibility.Visible;
            stackEditCloseBottom.IsEnabled = false;
            stackEditCloseBottom.Visibility = Visibility.Collapsed;
        }
        /// <summary>
        /// Barry Mikulas
        /// Created on: 2023/04/03
        /// 
        /// Sets up button panels on page for view mode.
        /// 
        /// </summary> 
        private void EditUpdateDeleteButtonsEnabled()
        {
            btnAddContacts.Visibility = Visibility.Collapsed;
            btnAddPet.Visibility = Visibility.Collapsed;
            btnAddSponsors.Visibility = Visibility.Collapsed;
            stackEditUpdateDelete.IsEnabled = true;
            stackEditUpdateDelete.Visibility = Visibility.Visible;
            stackSaveCancel.IsEnabled = false;
            stackSaveCancel.Visibility = Visibility.Collapsed;
            stackSaveCancelBottom.IsEnabled = false;
            stackSaveCancelBottom.Visibility = Visibility.Collapsed;
            stackEditCloseBottom.IsEnabled = true;
            stackEditCloseBottom.Visibility = Visibility.Visible;
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
        /// Barry Mikulas
        /// Created: 2023/03/09
        /// 
        /// When a user types in a textbox this will prevent them from entering anything but a decimal with 2 decimal places
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        private void DecimalNumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"^[0-9]{0,4}(\.[0-9]{0,2})?$");
            string amount = tbEventCost.Text.Insert(tbEventCost.CaretIndex, e.Text);
            if (!regex.IsMatch(amount))
            {
                e.Handled = true;
            }
        }


        /// <summary>
        /// Barry Mikulas
        /// Created: 2023/03/09
        /// 
        /// handler that checks that calls a method to check input in text box
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        private void tbNumAttendees_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            NumberValidationTextBox(sender, e);
        }
        /// <summary>
        /// Barry Mikulas
        /// Created: 2023/03/09
        /// 
        /// handler that checks that calls a method to check input in text box
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        private void tbNumAnimalsAdopted_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            NumberValidationTextBox(sender, e);
        }
        /// <summary>
        /// Barry Mikulas
        /// Created: 2023/03/09
        /// 
        /// handler that checks that calls a method to check input in text box
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        private void tbEventCost_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            DecimalNumberValidationTextBox(sender, e);
        }


        /// <summary>
        /// Barry Mikulas
        /// Created: 2023/03/09
        /// 
        /// handler that checks validation of text entry after field looses focus
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        private void tbEventCost_LostFocus(object sender, RoutedEventArgs e)
        {
            ValidateFundraisingEventResults();
        }
        /// <summary>
        /// Barry Mikulas
        /// Created: 2023/03/09
        /// 
        /// handler that checks validation of text entry after field looses focus
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        private void tbNumAttendees_LostFocus(object sender, RoutedEventArgs e)
        {
            ValidateFundraisingEventResults();
        }
        /// <summary>
        /// Barry Mikulas
        /// Created: 2023/03/09
        /// 
        /// handler that checks validation of text entry after field looses focus
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        private void tbNumAnimalsAdopted_LostFocus(object sender, RoutedEventArgs e)
        {
            ValidateFundraisingEventResults();

        }

        /// <summary>
        /// Barry Mikulas
        /// Created: 2023/03/14
        /// 
        /// Event handler for click of the Pledgers button.
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPledgers_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ViewFundraisingEventPledgers(FundraisingEvent, _masterManager));
        }
    }
    public enum PageMode
    {
        New,
        Edit,
        Update,
        View
    }
}
