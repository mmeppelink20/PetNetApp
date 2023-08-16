using DataObjects;
using LogicLayerInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayerFakes;

namespace LogicLayer
{
    public class MasterManager
    {
        private static MasterManager _existingMasterManager = null;
        private UsersVM _user;
        public UsersVM User
        {
            get => _user;
            set
            {
                _user = value;
                if (value == null)
                {
                    OnUserLogout();
                }
                else
                {
                    OnUserLogin();
                }
            }
        }
        public delegate void UserChangedAction();
        public event UserChangedAction UserLogout;
        public event UserChangedAction UserLogin;
        public IKennelManager KennelManager { get; private set; }
        public IUsersManager UsersManager { get; private set; }
        public IDeathManager DeathManager { get; private set; }
        public IAnimalManager AnimalManager { get; private set; }
        public IAnimalUpdatesManager AnimalUpdatesManager { get; private set; }
        public IScheduleManager ScheduleManager { get; set; }
        public ITestManager TestManager { get; private set; }
        public IRoleManager RoleManager { get; private set; }
        public ITicketManager TicketManager { get; private set; }
        public IProcedureManager ProcedureManager { get; private set; }
        public IMedicalRecordManager MedicalRecordManager { get; private set; }
        public IFundraisingCampaignManager FundraisingCampaignManager { get; private set; }
        public IShelterItemTransactionManager ShelterItemTransactionManager { get; private set; }
        public IDonationManager DonationManager { get; private set; }
        public IImagesManager ImagesManager { get; private set; }
        public IInstitutionalEntityManager InstitutionalEntityManager { get; private set; }
        public IFundraisingEventManager FundraisingEventManager { get; set; }
        public IZipcodeManager ZipcodeManager { get; set; }
        public IRequestManager RequestManager { get; private set; }
        public IVaccinationManager VaccinationManager { get; set; }
        public IAdoptionApplicationManager AdoptionApplicationManager { get; set; }
        public IShelterInventoryItemManager ShelterInventoryItemManager { get; set; }
        public IShelterManager ShelterManager { get; set; }
        public IItemManager ItemManager { get; set; }
        public IVolunteerManager VolunteerManager { get; set; }
        public IPostManager PostManager { get; set; }
        public IReplyManager ReplyManager { get; set; }
        public IFosterApplicationResponseManager FosterApplicationResponseManager { get; set; }
        public IResourceAddRequestManager ResourceAddRequestManager { get; set; }
        public IFosterManager FosterManager { get; set; }
        public IPledgeManager PledgeManager { get; set; }
        public IEventManager EventManager { get; set; }
        public IAdoptionApplicationResponseManager AdoptionApplicationResponseManager { get; set; }
        public IFosterApplicationManager FosterApplicationManager { get; set; }
        public IPrescriptionManager PrescriptionManager { get; private set; }


        private MasterManager()
        {
            KennelManager = new KennelManager();
            UsersManager = new UsersManager();
            DeathManager = new DeathManager();
            AnimalManager = new AnimalManager();
            AnimalUpdatesManager = new AnimalUpdatesManager();
            ScheduleManager = new ScheduleManager();
            TestManager = new TestManager();
            RoleManager = new RoleManager();
            TicketManager = new TicketManager();
            ProcedureManager = new ProcedureManager();
            MedicalRecordManager = new MedicalRecordManager();
            FundraisingCampaignManager = new FundraisingCampaignManager();
            InstitutionalEntityManager = new InstitutionalEntityManager();
            ImagesManager = new ImagesManager();
            ShelterItemTransactionManager = new ShelterItemTransactionManager();
            ImagesManager = new ImagesManager();
            DonationManager = new DonationManager();
            FundraisingEventManager = new FundraisingEventManager();
            ZipcodeManager = new ZipcodeManager();
            RequestManager = new RequestManager();
            VaccinationManager = new VaccinationManager();
            ShelterInventoryItemManager = new ShelterInventoryItemManager();
            ShelterManager = new ShelterManager();
            ItemManager = new ItemManager();
            VolunteerManager = new VolunteerManager();
            PostManager = new PostManager();
            ReplyManager = new ReplyManager();
            AdoptionApplicationManager = new AdoptionApplicationManager();
            FosterApplicationResponseManager = new FosterApplicationResponseManager();
            ResourceAddRequestManager = new ResourceAddRequestManager();
            FosterManager = new FosterManager();
            PledgeManager = new PledgeManager();
            EventManager = new EventManager();
            AdoptionApplicationResponseManager = new AdoptionApplicationResponseManager();
            FosterApplicationManager = new FosterApplicationManager();
            PrescriptionManager = new PrescriptionManager();


            //for testing from dev page
            //User = new UsersVM()
            //{
            //    UsersId = 100004,
            //    ShelterId = 100000,
            //    GivenName = "Barry",
            //    FamilyName = "Mikulas",
            //    Email = "bmikulas@company.com",
            //    Address = "4150 riverview road",
            //    Zipcode = "52411",
            //    Phone = "319-123-1325",
            //    Active = true,
            //    Suspend = false,
            //    Roles = new List<string>() { "Admin" }
            //};
        }

        public static MasterManager GetMasterManager()
        {
            if (_existingMasterManager == null)
            {
                _existingMasterManager = new MasterManager();
            }
            return _existingMasterManager;
        }
        protected virtual void OnUserLogout()
        {
            UserChangedAction handler = UserLogout;
            handler?.Invoke();
            
        }
        protected virtual void OnUserLogin()
        {
            UserChangedAction handler = UserLogin;
            handler?.Invoke();
        }
    }
}
