
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayerInterfaces;
using DataObjects;

namespace DataAccessLayerFakes
{
    /// <summary>
    /// Stephen Jaurigue
    /// Created: 2023/02/23
    /// 
    /// The data access layer fake class used for testing purposes while developing institutional entity features and for the logic tests
    /// </summary>
    public class InstitutionalEntityAccessorFake : IInstitutionalEntityAccessor
    {
        private List<InstitutionalEntity> _institutionalEntities = FundraisingFakeData.InstitutionalEntities;
        private List<Tuple<int, int>> _fundraisingCampaignEntities = FundraisingFakeData.FundraisingCampaignEntities;
        public List<InstitutionalEntity> _institutionalEntitiesWithShelterId = new List<InstitutionalEntity>();
        private List<Tuple<int, int>> _fundraisingEventEntities = FundraisingFakeData.FundraisingEventEntities;
        public List<SponsorEvent> _sponsorEvents = new List<SponsorEvent>();

        public InstitutionalEntityAccessorFake()
        {
            // Contact Type Overview
            // 14-17 arent Sponsor
            // 14,17 are Host
            // 15,16 are Contact


            _institutionalEntitiesWithShelterId.Add(new InstitutionalEntity()
            {
                InstitutionalEntityId = 1000,
                CompanyName = "SpaceX",
                GivenName = "Elon",
                FamilyName = "Musk",
                Email = "elon@spacex.com",
                Phone = "4539876541",
                Address = "123 Boca Chica Blvd",
                Address2 = "",
                Zipcode = "78520",
                ContactType = "Host",
                ShelterId = 100000
            });
            _institutionalEntitiesWithShelterId.Add(new InstitutionalEntity()
            {
                InstitutionalEntityId = 1001,
                CompanyName = "SpaceX",
                GivenName = "Elona",
                FamilyName = "Musk",
                Email = "elona@spacex.com",
                Phone = "2539876541",
                Address = "223 Boca Chica Blvd",
                Address2 = null,
                Zipcode = "78520",
                ContactType = "Host",
                ShelterId = 100000
            });
            _institutionalEntitiesWithShelterId.Add(new InstitutionalEntity()
            {
                InstitutionalEntityId = 1002,
                CompanyName = "SpaceX",
                GivenName = "Elono",
                FamilyName = "Musko",
                Email = "elono@spacex.com",
                Phone = "3539876541",
                Address = "323 Boca Chica Blvd",
                Address2 = null,
                Zipcode = "78520",
                ContactType = "Host",
                ShelterId = 100000
            });
            _institutionalEntitiesWithShelterId.Add(new InstitutionalEntity()
            {
                InstitutionalEntityId = 1003,
                CompanyName = "SpaceX",
                GivenName = "Melon",
                FamilyName = "Husk",
                Email = "melon@spacex.com",
                Phone = "6539876541",
                Address = "623 Boca Chica Blvd",
                Address2 = null,
                Zipcode = "78520",
                ContactType = "Sponsor",
                ShelterId = 100000
            });
            _institutionalEntitiesWithShelterId.Add(new InstitutionalEntity()
            {
                InstitutionalEntityId = 1004,
                CompanyName = "SpaceX",
                GivenName = "Lemon",
                FamilyName = "Dusk",
                Email = "lemon@spacex.com",
                Phone = "7539876541",
                Address = "723 Boca Chica Blvd",
                Address2 = null,
                Zipcode = "78520",
                ContactType = "Host",
                ShelterId = 100000
            });
            _institutionalEntitiesWithShelterId.Add(new InstitutionalEntity()
            {
                InstitutionalEntityId = 1005,
                CompanyName = "SpaceX",
                GivenName = "Grape",
                FamilyName = "Mush",
                Email = "Grape@spacex.com",
                Phone = "8539876541",
                Address = "823 Boca Chica Blvd",
                Address2 = null,
                Zipcode = "78520",
                ContactType = "Host",
                ShelterId = 100000
            });
            _institutionalEntitiesWithShelterId.Add(new InstitutionalEntity()
            {
                InstitutionalEntityId = 1006,
                CompanyName = "SpaceX",
                GivenName = "Dragon",
                FamilyName = "Musk",
                Email = "Dragon@spacex.com",
                Phone = "9539876541",
                Address = "923 Boca Chica Blvd",
                Address2 = null,
                Zipcode = "78520",
                ContactType = "Host",
                ShelterId = 100000
            });
            _institutionalEntitiesWithShelterId.Add(new InstitutionalEntity()
            {
                InstitutionalEntityId = 1007,
                CompanyName = "SpaceX",
                GivenName = "Falcon",
                FamilyName = "Musk",
                Email = "Falcon@spacex.com",
                Phone = "1139876541",
                Address = "113 Boca Chica Blvd",
                Address2 = null,
                Zipcode = "78520",
                ContactType = "Sponsor",
                ShelterId = 100000
            });
            _institutionalEntitiesWithShelterId.Add(new InstitutionalEntity()
            {
                InstitutionalEntityId = 1008,
                CompanyName = "SpaceX",
                GivenName = "Glen",
                FamilyName = "Musk",
                Email = "glen@spacex.com",
                Phone = "1239876541",
                Address = "1233 Boca Chica Blvd",
                Address2 = null,
                Zipcode = "78520",
                ContactType = "Sponsor",
                ShelterId = 100000
            });
            _institutionalEntitiesWithShelterId.Add(new InstitutionalEntity()
            {
                InstitutionalEntityId = 1009,
                CompanyName = "SpaceX",
                GivenName = "Nole",
                FamilyName = "Musk",
                Email = "nole@spacex.com",
                Phone = "1339876541",
                Address = "1323 Boca Chica Blvd",
                Address2 = null,
                Zipcode = "78520",
                ContactType = "Contact",
                ShelterId = 100000
            });
            _institutionalEntitiesWithShelterId.Add(new InstitutionalEntity()
            {
                InstitutionalEntityId = 1010,
                CompanyName = "SpaceX",
                GivenName = "Grimes",
                FamilyName = "Musk",
                Email = "grimes@spacex.com",
                Phone = "1439876541",
                Address = "1423 Boca Chica Blvd",
                Address2 = null,
                Zipcode = "78520",
                ContactType = "Contact",
                ShelterId = 100000
            });
            _institutionalEntitiesWithShelterId.Add(new InstitutionalEntity()
            {
                InstitutionalEntityId = 1011,
                CompanyName = "SpaceX",
                GivenName = "X Æ A-12",
                FamilyName = "Musk",
                Email = "xasha12@spacex.com",
                Phone = "4539876541",
                Address = "123 Boca Chica Blvd",
                Address2 = null,
                Zipcode = "78520",
                ContactType = "Contact",
                ShelterId = 100000
            });

            //makes 300 additional InstitutionalEntityFakes
            Random randomNum = new Random();
            //char ranChar;
            //string charList = "abcdefghijklmnopqrstuvwxyz";
            string[] gnList = { "Chris", "Asa", "Mohammed", "Elon", "Mads", "Tyler", "Will", "Keegan", "Chris", "Lemon", "Liam", "Noah", "Oliver", "Elijah", "Mateo", "Lucas", "Olivia", "Emma", "Amelia", "Ava", "Sophia", "Isabella", "Luna", "Mia", "Charlotte", "Evelyn", "Levi", "Asher", "James", "Leo", "Harper", "Scarlett", "Nova", "Aurora", "Ella", "Mila", "Aria", "Ellie", "Gianna", "Sofia", "Grayson", "Ezra", "Luca", "Ethan", "Aiden", "Wyatt", "Sebastian", "Benjamin", "Mason", "Henry" };
            string[] fnList = { "Forsberg", "Armstrong", "Musk", "Rhea", "Tousah", "Hand", "Smith", "Grimes", "Andrews", "Stephenson", "Deegan", "Glasgow", "Johnson", "Williams", "Brown", "Jones", "Garcia", "Miller", "Davis", "Rodriguez", "Martinez", "Hernandez", "Lopez", "Gonzales", "Wilson", "Anderson", "Thomas", "Taylor", "Moore", "Jackson", "Martin", "Lee", "Perez", "Thompson", "White", "Harris", "Sanchez", "Clark", "Ramirez", "Lewis", "Robinson", "Walker", "Young", "Allen", "King", "Wright", "Scott", "Torres", "Nguyen", "Hill", "Flores", "Green", "Adams", "Nelson", "Baker", "Hall", "Rivera", "Campbell", "Mitchell", "Carter", "Roberts", "Gomez", "Phillips", "Evans", "Turner", "Diaz", "Parker", "Cruz", "Edwards", "Collins", "Reyes", "Stewart", "Morris", "Morales", "Murphy", "Cook", "Rogers", "Gutierrez", "Ortiz", "Morgan", "Cooper", "Peterson", "Bailey", "Reed", "Kelly", "Howard", "Ramos", "Kim", "Cox", "Ward", "Richardson", "Watson", "Brooks", "Chavez", "Wood", "James", "Bennet", "Gray", "Mendoza", "Ruiz", "Hughes", "Price", "Alvarez", "Castillo", "Sanders", "Patel", "Myers", "Long", "Ross", "Foster", "Jimenez" };
            string[] cpyList = { "", "Boeing", "SpaceX", "ULA", "Rocket Labs", "Amazon", "Alibaba", "Microsoft", "Apple", "Rockwell", "Nordstrom", "Asus", "Ford", "BMW", "Rivian", "Tesla", "Walmart", "Amazon", "Apple", "CVS Health", "United Health", "Exxon Mobil", "Berkshire Hathaway", "Alphabet", "McKesson", "Amerisource Bergen", "Costco Wholesale", "Cigna", "ATT", "Microsoft", "Cardinal Health", "Chevron", "Home Depot", "Walgreens Boots", "Marathon Petroleum", "Elevance Health", "Kroger", "Ford Motor", "Verizon", "JPMorgan Chase", "General Motors", "Centene", "Meta Platforms", "Comcast", "Phillips 66", "Valero Energy", "Dell Technologies", "Target", "FannieMae", "United Parcel Service", "Lowes", "Bank of America", "Johnson Johnson", "Archer Daniels Midland", "Fed Ex", "Humana", "Wells Fargo", "State Farm", "Pfizer", "Citi", "Pepsi Co", "Intel", "Procter Gamble", "General Electric", "IBM", "Met Life", "Prudential", "Albertsons", "Walt Disney", "Energy Transfer", "Lockheed Martin", "Freddie Mac", "Goldman Sachs", "Raytheon Technologies", "HP", "Boeing", "Morgan Stanley", "HCA Healthcare", "Abb Vie", "Dow", "Tesla", "Allstate", "AIG", "BestBuy", "Charter Communications", "Sysco", "Merck", "New York Life", "Caterpillar", "Cisco Systems", "TJX", "Publix Super Markets", "Conoco Phillips", "Liberty Mutual", "Progressive", "Nationwide", "Tyson Foods", "Bristol Myers Squibb", "Nike", "Deere", "American Express", "Abbott Laboratories", "StoneX", "Plains GP Holdings", "Enterprise Products", "TIAA", "Oracle", "Thermo Fisher Scientific", "Coca Cola", "Genera Dynamics", "CHS", "USAA", "Northwestern Mutual", "Nucor", "Exelon", "Mass Mutual" };
            string[] contactTypes = { "Host", "Sponsor", "Contact" };
            string[] zipcodeList = { "52404", "10013", "29384", "29555", "38001", "00601" };
            for (int i = 0; i < 300; i++)
            {
                string phoneNumber = "";
                for (int j = 0; j < 10; j++)
                {
                    phoneNumber += randomNum.Next(0, 9).ToString();
                }
                randomNum.Next(0, 9);

                //ranChar = charList[randomNum.Next(0, charList.Length)];
                string givenName = gnList[randomNum.Next(0, gnList.Length)];
                string familyName = fnList[randomNum.Next(0, fnList.Length)];
                string cpyName = cpyList[randomNum.Next(0, cpyList.Length)];
                string ct = contactTypes[randomNum.Next(0, contactTypes.Length)];
                string zc = zipcodeList[randomNum.Next(0, zipcodeList.Length)];

                _institutionalEntitiesWithShelterId.Add(new InstitutionalEntity()
                {
                    InstitutionalEntityId = 1011 + i,
                    CompanyName = cpyName,
                    GivenName = givenName,
                    FamilyName = familyName,
                    Email = (givenName + "." + familyName).ToLower() + i + "@" + cpyName.ToLower().Replace(" ", "") + ".com",
                    Phone = phoneNumber,
                    Address = "123 Boca Chica Blvd",
                    Address2 = null,
                    Zipcode = zc,
                    ContactType = ct,
                    ShelterId = 100000
                });
            }




        }
        public List<InstitutionalEntity> SelectFundraisingSponsorsByCampaignId(int fundraisingCampaignId)
        {
            // piece by piece lambda version
            //var entityIdsForCampaign = fundraisingCampaignEntity.Where((join) => join.Item1 == fundraisingCampaignId);
            //var matchingEntitiesFromIds = entityIdsForCampaign.Select((id) => institutionalEntity.First((entity) => entity.InstitutionalEntityId == id.Item2));
            //var matchingEntitiesThatAreSponsors = matchingEntitiesFromIds.Where((entity) => entity.ContactType == "Sponsor");
            //return matchingEntitiesThatAreSponsors.ToList();

            // join lambda version
            //var matchingEntitiesFromIdsJoin = institutionalEntity.Where((entity) => entity.ContactType == "Sponsor").Join(fundraisingCampaignEntity.Where((join) => join.Item1 == fundraisingCampaignId), (entity) => entity.InstitutionalEntityId, (join) => join.Item2, (entity, join) => entity);
            //return matchingEntitiesFromIdsJoin.ToList();

            // linq way
            var sponsors = from institutionalEntityRecord in _institutionalEntities
                           join fundraisingCampaignEntityRecord in _fundraisingCampaignEntities on institutionalEntityRecord.InstitutionalEntityId equals fundraisingCampaignEntityRecord.Item2
                           where fundraisingCampaignEntityRecord.Item1 == fundraisingCampaignId && institutionalEntityRecord.ContactType == "Sponsor"
                           select institutionalEntityRecord;
            return sponsors.ToList();
        }

        public List<InstitutionalEntity> SelectAllSponsors()
        {
            var sponsors = from institutionalEntityRecord in _institutionalEntities
                           where institutionalEntityRecord.ContactType == "Sponsor"
                           select institutionalEntityRecord;
            return sponsors.ToList();
        }

        /// <summary>
        /// Updated by Barry Mikulas
        /// 2023/03/05
        /// 
        /// 
        /// </summary>
        /// <param name="shelterId"></param>
        /// <param name="entityType"></param>
        /// <returns></returns>
        public List<InstitutionalEntity> SelectAllInstitutionalEntitiesByShelterIdAndEntityType(int shelterId, string entityType)
        {
            List<InstitutionalEntity> fakes = new List<InstitutionalEntity>();

            fakes = _institutionalEntitiesWithShelterId.Where(i => i.ContactType == entityType).Where(i => i.ShelterId == shelterId).ToList();

            //fakes = _institutionalEntitiesWithShelterId.FindAll(i => i.ContactType == contactType && i.ShelterId == shelterId);

            return fakes;
        }

        public int InsertInstitutionalEntity(InstitutionalEntity institutionalEntity)
        {
            _institutionalEntitiesWithShelterId.Add(institutionalEntity);
            int rows = 0;

            for (int i = 0; i < _institutionalEntitiesWithShelterId.Count; i++)
            {
                if (_institutionalEntitiesWithShelterId[i].InstitutionalEntityId == institutionalEntity.InstitutionalEntityId)
                {
                    rows = 1;
                }
            }
            return rows;
        }

        public int UpdateInstitutionalEntity(InstitutionalEntity oldEntity, InstitutionalEntity newEntity)
        {
            int result = 0;

            for (int i = 0; i < _institutionalEntitiesWithShelterId.Count; i++)
            {
                if (_institutionalEntitiesWithShelterId[i].InstitutionalEntityId == oldEntity.InstitutionalEntityId)
                {
                    // the real database will check for every editable field in the stored procedure
                    _institutionalEntitiesWithShelterId[i].Address = _institutionalEntitiesWithShelterId[i].Address == oldEntity.Address
                        ? _institutionalEntitiesWithShelterId[i].Address = newEntity.Address : oldEntity.Address;

                    result++;
                    break;
                }
            }

            return result;
        }

        public InstitutionalEntity SelectInstitutionalEntityByInstitutionalEntityId(int institutionalEntityId)
        {
            InstitutionalEntity institutionalEntity = new InstitutionalEntity();

            foreach (InstitutionalEntity fakeEntity in _institutionalEntitiesWithShelterId)
            {
                if (fakeEntity.InstitutionalEntityId == institutionalEntityId)
                {
                    institutionalEntity = fakeEntity;
                    return institutionalEntity;
                }
            }
            if (institutionalEntity == null)
            {
                throw new ApplicationException("Entity not found");
            }
            return institutionalEntity;
        }

        public List<InstitutionalEntity> SelectFundraisingEventInstitutionalEntitiesByEventIdAndContactType(int fundraisingEventId, string contactType)
        {
            // throw new NotImplementedException();
            var institutionalEntities = from institutionalEntityRecord in _institutionalEntities
                                        join fundraisingEventEntityRecord in _fundraisingEventEntities on institutionalEntityRecord.InstitutionalEntityId equals fundraisingEventEntityRecord.Item2
                                        where fundraisingEventEntityRecord.Item1 == fundraisingEventId && institutionalEntityRecord.ContactType == contactType
                                        select institutionalEntityRecord;

            return institutionalEntities.ToList();
        }

        public InstitutionalEntity SelectInstitutionalEntityByFundraisingEventIdAndContactType(int fundraisingEventId, string contactType)
        {
//            throw new NotImplementedException();

            var institutionalEntity = from institutionalEntityRecord in _institutionalEntities
                                      join fundraisingEventEntityRecord in _fundraisingEventEntities on institutionalEntityRecord.InstitutionalEntityId equals fundraisingEventEntityRecord.Item2
                                      where fundraisingEventEntityRecord.Item1 == fundraisingEventId && institutionalEntityRecord.ContactType == contactType
                                      select institutionalEntityRecord;

            return (InstitutionalEntity)institutionalEntity.FirstOrDefault();
        }

        public List<InstitutionalEntity> SelectAllHosts()
        {
            var hosts = from institutionalEntityRecord in _institutionalEntities
                           where institutionalEntityRecord.ContactType == "Host"
                        select institutionalEntityRecord;
            return hosts.ToList();
        }

        public List<InstitutionalEntity> SelectAllContact()
        {
            var contact = from institutionalEntityRecord in _institutionalEntities
                        where institutionalEntityRecord.ContactType == "Contact"
                        select institutionalEntityRecord;
            return contact.ToList();
        }

        public List<SponsorEvent> SelectSponsorEventByName(string name)
        {
            return _sponsorEvents.Where(i => i.CompanyName == name).ToList();
        }
    }
}
