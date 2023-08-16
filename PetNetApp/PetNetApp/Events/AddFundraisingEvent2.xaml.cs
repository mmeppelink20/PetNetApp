using LogicLayer;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfPresentation.Community;
using PetNetApp;
using WpfPresentation.Fundraising;

namespace WpfPresentation.Events
{
    /// <summary>
    /// Interaction logic for AddFundraisingEvent2.xaml
    /// </summary>
    public partial class AddFundraisingEvent2 : Page
    {
        private MasterManager _masterManager = MasterManager.GetMasterManager();
        private List<InstitutionalEntity> _sponsorList;
        private List<InstitutionalEntity> sponsorSelected;
        private List<InstitutionalEntity> _contactList;
        private List<InstitutionalEntity> contactSelected;
        private List<Animal> _animalsList;
        private List<Animal> animalsSelected;
        private FundraisingEvent _fundraisingEvent;
        private InstitutionalEntity _host;
        private Images eventImage;

        public AddFundraisingEvent2(FundraisingEvent fundraisingEvent, InstitutionalEntity host)
        {
            InitializeComponent();
            sponsorSelected = new List<InstitutionalEntity>();
            animalsSelected = new List<Animal>();
            contactSelected = new List<InstitutionalEntity>();
            _host = host;
            _fundraisingEvent = fundraisingEvent;
        }

        /// <summary>
        /// Author: Hoang Chu
        /// 04/27/2023
        /// 
        /// </summary>
        /// <param name="sponsor"></param>
        private void DisplaySponsor(InstitutionalEntity sponsor)
        {
            UCSponsor ucSponsor = new UCSponsor();
            ucSponsor.lblSponsorName.Content = sponsor.CompanyName;
            ucSponsor.btnRemove.Click += (obj, arg) => RemoveSponsor_MouseClick(sponsor);
            ucSponsor.Margin = new Thickness(0, 0, 10, 5);
            stpSponsorList.Children.Add(ucSponsor);
        }

        /// <summary>
        /// Author: Hoang Chu
        /// 04/27/2023
        /// 
        /// </summary>
        /// <param name="contact"></param>
        private void DisplayContact(InstitutionalEntity contact)
        {
            UCContact ucContact = new UCContact();
            ucContact.lblContactName.Content = contact.CompanyName;
            ucContact.btnRemove.Click += (obj, arg) => RemoveContact_MouseClick(contact);
            ucContact.Margin = new Thickness(0, 0, 10, 5);
            stpContactList.Children.Add(ucContact);
        }

        /// <summary>
        /// Author: Hoang Chu
        /// 04/27/2023
        /// 
        /// </summary>
        /// <param name="animal"></param>
        private void DisplayAnimal(Animal animal)
        {
            UCAnimalTakeToEvent ucAnimalTakeToEvent = new UCAnimalTakeToEvent();
            ucAnimalTakeToEvent.lblAnimalName.Content = animal.AnimalName;
            ucAnimalTakeToEvent.btnRemove.Click += (obj, arg) => RemoveAnimal_MouseClick(animal);
            ucAnimalTakeToEvent.Margin = new Thickness(0, 0, 10, 5);
            stpAnimalList.Children.Add(ucAnimalTakeToEvent);
        }

        /// <summary>
        /// Author: Hoang Chu
        /// 
        /// 04/27/2023
        /// </summary>
        /// <param name="animal"></param>
        private void RemoveAnimal_MouseClick(Animal animal)
        {
            animalsSelected.Remove(animal);
            PopulateAnimalList();
        }

        /// <summary>
        /// Author: Hoang Chu
        /// 04/27/2023
        /// 
        /// </summary>
        /// <param name="contact"></param>
        private void RemoveContact_MouseClick(InstitutionalEntity contact)
        {
            contactSelected.Remove(contact);
            PopulateContactList();
        }

        /// <summary>
        /// Author: Hoang Chu
        /// 04/27/2023
        /// 
        /// </summary>
        /// <param name="sponsor"></param>
        private void RemoveSponsor_MouseClick(InstitutionalEntity sponsor)
        {
            sponsorSelected.Remove(sponsor);
            PopulateSponsorList();
        }

        /// <summary>
        /// Author: Hoang Chu
        /// 04/27/2023
        /// 
        /// </summary>
        private void PopulateSponsorList()
        {
            stpSponsorList.Children.Clear();
            foreach (InstitutionalEntity sponsor in sponsorSelected)
            {
                DisplaySponsor(sponsor);
            }
        }

        /// <summary>
        /// Author: Hoang Chu
        /// 04/27/2023
        /// 
        /// </summary>
        private void PopulateContactList()
        {
            stpContactList.Children.Clear();
            foreach (InstitutionalEntity contact in contactSelected)
            {
                DisplayContact(contact);
            }
        }

        /// <summary>
        /// Author: Hoang Chu
        /// 04/27/2023
        /// 
        /// </summary>
        private void PopulateAnimalList()
        {
            stpAnimalList.Children.Clear();
            foreach (Animal animal in animalsSelected)
            {
                DisplayAnimal(animal);
            }
        }

        /// <summary>
        /// Author: Hoang Chu
        /// 04/27/2023
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSponsorContact_Click(object sender, RoutedEventArgs e)
        {
            var sponsorContactWindow = new SponsorListWindow(_sponsorList);
            sponsorContactWindow.ShowDialog();

            if (sponsorContactWindow.GetReturnValue() != null)
            {
                InstitutionalEntity sponsor = sponsorContactWindow.GetReturnValue();
                if (!sponsorSelected.Contains(sponsor))
                {
                    sponsorSelected.Add(sponsor);
                }
                else
                {
                    PromptWindow.ShowPrompt("Warning", "This sponsor has been selected");
                }
            }
            PopulateSponsorList();
        }

        /// <summary>
        /// Author: Hoang Chu
        /// 04/27/2023
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnChooseContact_Click(object sender, RoutedEventArgs e)
        {
            var contactWindow = new ContactListWindow(_contactList);
            contactWindow.ShowDialog();
            if(contactWindow.GetReturnValue() != null)
            {
                InstitutionalEntity contact = contactWindow.GetReturnValue();
                if (!contactSelected.Contains(contact))
                {
                    contactSelected.Add(contact);
                }
                else
                {
                    PromptWindow.ShowPrompt("Warning", "This contact has been selected");
                }
            }
            PopulateContactList();
        }

        /// <summary>
        /// Author: Hoang Chu
        /// 04/27/2023
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnChooseAnimal_Click(object sender, RoutedEventArgs e)
        {
            var animalTakeToEventWindow = new AnimalListWindow(_animalsList);
            animalTakeToEventWindow.ShowDialog();
            if (animalTakeToEventWindow.GetReturnValuen() != null)
            {
                Animal selectedAnimal = animalTakeToEventWindow.GetReturnValuen();
                if (!animalsSelected.Contains(selectedAnimal))
                {
                    animalsSelected.Add(selectedAnimal);
                }
                else
                {
                    PromptWindow.ShowPrompt("Warning", "This animal has been selected");
                }
            }
            PopulateAnimalList();
        }

        /// <summary>
        /// Author: Hoang Chu
        /// 04/27/2023
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                int shelterId = 100000; // Need to change later
                _sponsorList = _masterManager.InstitutionalEntityManager.RetrieveAllSponsors();
                _contactList = _masterManager.InstitutionalEntityManager.RetrieveAllContact();
                _animalsList = _masterManager.AnimalManager.RetrieveAllAnimals(shelterId);
            }
            catch (Exception ex)
            {
                PromptWindow.ShowPrompt("Error", ex.Message);
            }
            PopulateSponsorList();
            PopulateContactList();
            PopulateAnimalList();
        }

        /// <summary>
        /// Author: Hoang Chu
        /// 04/27/2023
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUploadImage_Click(object sender, RoutedEventArgs e)
        {
            var uploadImage = new UploadEventImageWindow();
            uploadImage.ShowDialog();
            if (uploadImage.GetImage() != null)
            {
                eventImage = uploadImage.GetImage();
            }
        }

        /// <summary>
        /// Author: Hoang Chu
        /// 04/27/2023
        /// 
        /// </summary>
        /// <returns></returns>
        private bool PageValidation()
        {
            bool isSuccess = true;

            lblContactError.Visibility = Visibility.Hidden;
            if (contactSelected.Count == 0)
            {
                lblContactError.Visibility = Visibility.Visible;
                return isSuccess = false;
            }
            else
            {
                if (eventImage != null)
                {
                    _fundraisingEvent.ImageId = eventImage.ImageId;
                }
                _fundraisingEvent.Hidden = (bool)cbIsHidden.IsChecked;
                _fundraisingEvent.NumOfAttendees = animalsSelected.Count();
            }

            return isSuccess;
        }

        /// <summary>
        /// Author: Hoang Chu
        /// 04/27/2023
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            int id = 0;
            if (PageValidation())
            {
                try
                {
                    id = _masterManager.FundraisingEventManager.AddFundraisingEvent(_fundraisingEvent);
                    // 3 for-each loop
                    contactSelected.Add(_host);
                    foreach (InstitutionalEntity contact in contactSelected)
                    {
                        try
                        {
                            _masterManager.FundraisingEventManager.AddFundraisingEventEntity(id, contact.InstitutionalEntityId);
                        }
                        catch (Exception ex)
                        {
                            PromptWindow.ShowPrompt("Error", "Can not add contact \n\n" + ex);
                        }
                    }

                    foreach (InstitutionalEntity sponsor in sponsorSelected)
                    {
                        try
                        {
                            _masterManager.FundraisingEventManager.AddFundraisingEventEntity(id, sponsor.InstitutionalEntityId);
                        }
                        catch (Exception ex)
                        {
                            PromptWindow.ShowPrompt("Error", "Can not add sponsor \n\n" + ex);
                        }
                    }

                    foreach (Animal animal in animalsSelected)
                    {
                        try
                        {
                            _masterManager.FundraisingEventManager.AddFundraiserAnimal(id, animal.AnimalId);
                        }
                        catch (Exception ex)
                        {
                            PromptWindow.ShowPrompt("Error", "Can not add fundraiser animal \n\n" + ex);
                        }
                    }

                    PromptWindow.ShowPrompt("Message", "Successful to add new fundraising event");
                    NavigateToViewFundraisingEvent();
                }
                catch (Exception ex)
                {
                    PromptWindow.ShowPrompt("Error", "Can not add new fundraising event \n\n" + ex);
                }
            }
        }

        /// <summary>
        /// Author: Hoang Chu
        /// 04/27/2023
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            if (PromptWindow.ShowPrompt("Cancel", "Are you really want to cancel?", ButtonMode.YesNo)
                == PromptSelection.Yes)
            {
                NavigateToViewFundraisingEvent();
            }
        }

        /// <summary>
        /// Author: Hoang Chu
        /// 04/27/2023
        /// 
        /// </summary>
        private void NavigateToViewFundraisingEvent()
        {
            var mainWindow = (MainWindow)MainWindow.GetWindow(this);
            FundraisingPage fundraisingPage = FundraisingPage.GetFundraisingPage(_masterManager);
            fundraisingPage.ChangeSelectedButton(fundraisingPage.btnEvents);
            mainWindow.ChangeSelectedButton(mainWindow.btnFundraising);
            mainWindow.frameMain.Navigate(fundraisingPage);
            fundraisingPage.frameFundraising.Navigate(ViewFundraisingEventsPage.GetViewFundraisingEvents());
        }
    }
}
