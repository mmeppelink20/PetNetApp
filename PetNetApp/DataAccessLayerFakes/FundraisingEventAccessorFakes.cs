using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessLayerInterfaces;

namespace DataAccessLayerFakes
{
    public class FundraisingEventAccessorFakes : IFundraisingEventAccessor
    {
        private Random rand = new Random();
        private List<FundraisingEvent> fundraisingEventsFake = new List<FundraisingEvent>();
        private List<Tuple<int, int>> fundraiserAnimal = new List<Tuple<int, int>>();
        private List<Tuple<int, int>> fundraisingEventEntity = new List<Tuple<int, int>>();
        private List<FundraisingEventVM> fakeFundraisingEvents = null;
        private List<FundraisingEventVM> _fundraisingEvents = FundraisingFakeData.FundraisingEvents;
        private List<FundraisingEventVM> fakeFundraisingEventsActive = FundraisingFakeData.FundraisingEventsActive;
        private List<Shelter> fakeShelterList = FundraisingFakeData.Shelters;

        public FundraisingEventAccessorFakes()
        {
            fundraisingEventsFake.Add(new FundraisingEventVM()
            {
                FundraisingEventId = 100000,
                Title = "Test 1",
                UsersId = 100000,
                Description = "This is a test"
            });
            fundraisingEventsFake.Add(new FundraisingEventVM()
            {
                FundraisingEventId = 100001,
                Title = "Test 2",
                UsersId = 100000,
                Description = "This is a test"
            });
            fundraisingEventsFake.Add(new FundraisingEventVM()
            {
                FundraisingEventId = 100003,
                Title = "Test 3",
                UsersId = 100000,
                Description = "This is a test"
            });

            InsertFundraiserAnimal(100000, 100000);
            InsertFundraiserAnimal(100000, 100001);
            InsertFundraiserAnimal(100001, 100003);
            InsertFundraiserAnimal(100002, 100004);

            InsertFundraisingEventEntity(100000, 100000);
            InsertFundraisingEventEntity(100000, 100001);
            InsertFundraisingEventEntity(100000, 100002);
            InsertFundraisingEventEntity(100001, 100003);
            InsertFundraisingEventEntity(100002, 100004);
            InsertFundraisingEventEntity(100002, 100005);
        }
        public int DeactivateFundraisingEvent(int fundraisingEventId)
        {
            int rowAffected = 0;
            foreach (FundraisingEvent fundraisingEvent in fundraisingEventsFake)
            {
                if (fundraisingEvent.FundraisingEventId == fundraisingEventId)
                {
                    fundraisingEvent.Hidden = true;
                    rowAffected = 1;
                }
            }
            return rowAffected;
        }

        public int InsertFundraiserAnimal(int fundraisingEventId, int animalId)
        {
            int rowAffected = 0;

            try
            {
                fundraiserAnimal.Add(new Tuple<int, int>(fundraisingEventId, animalId));
                rowAffected = 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return rowAffected;
        }

        public int InsertFundraisingEvent(FundraisingEvent fundraisingEvent)
        {
            int rowAffected = 0;
            try
            {
                fundraisingEventsFake.Add(fundraisingEvent);
                rowAffected = 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return rowAffected;
        }

        public int InsertFundraisingEventEntity(int eventId, int contactId)
        {
            int rowAffected = 0;

            try
            {
                fundraisingEventEntity.Add(new Tuple<int, int>(eventId, contactId));
                rowAffected = 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return rowAffected;
        }

        public List<int> SelectAnimalByFundraisingEvent(int eventId)
        {
            List<int> animalIdList = new List<int>();

            foreach (var animal in fundraiserAnimal)
            {
                if (animal.Item1 == eventId)
                {
                    animalIdList.Add(animal.Item2);
                }
            }

            return animalIdList;
        }

        public List<int> SelectContactByFundraisingEvent(int eventId)
        {
            List<int> contactIdList = new List<int>();

            foreach (var contact in fundraisingEventEntity)
            {
                if (contact.Item1 == eventId)
                {
                    contactIdList.Add(contact.Item2);
                }
            }

            return contactIdList;
        }

        public FundraisingEvent SelectFundraisingEvent(int eventId)
        {
            FundraisingEvent result = new FundraisingEvent();
            foreach (FundraisingEvent fundraisingEvent in fundraisingEventsFake)
            {
                if (fundraisingEvent.FundraisingEventId == eventId)
                {
                    result = fundraisingEvent;
                }
            }
            return result;
        }

        public List<int> SelectSponsorByFundraisingEvent(int eventId)
        {
            List<int> sponsorIdList = new List<int>();

            foreach (var contact in fundraisingEventEntity)
            {
                if (contact.Item1 == eventId)
                {
                    sponsorIdList.Add(contact.Item2);
                }
            }

            return sponsorIdList;
        }

        public int UpdateFundraisingEvent(FundraisingEventVM fundraisingEvent)
        {
            int rowAffected = 0;
            foreach (FundraisingEvent fEvent in fundraisingEventsFake)
            {
                if (fEvent.FundraisingEventId == fundraisingEvent.FundraisingEventId)
                {
                    fEvent.UsersId = fundraisingEvent.UsersId;
                    fEvent.CampaignId = fundraisingEvent.CampaignId == null ? null : fundraisingEvent.CampaignId;
                    fEvent.ShelterId = fundraisingEvent.ShelterId;
                    fEvent.ImageId = fundraisingEvent.ImageId;
                    fEvent.Hidden = fundraisingEvent.Hidden;
                    fEvent.Title = fundraisingEvent.Title;
                    fEvent.StartTime = fundraisingEvent.StartTime;
                    fEvent.EndTime = fundraisingEvent.EndTime;
                    fEvent.Description = fundraisingEvent.Description;
                    fEvent.NumOfAttendees = fundraisingEvent.NumOfAttendees;
                    fEvent.AdditionalInfo = fundraisingEvent.AdditionalInfo;
                }
            }
            return rowAffected;
        }

        public List<FundraisingEventVM> SelectAllFundraisingEventsByCampaignId(int campaignId)
        {
            int fundraisingEventId = 100000;
            string[] etList = { "Dogs", "Cats", "Frogs", "Snakes", "Turtles" };
            fakeFundraisingEvents = new List<FundraisingEventVM>()
            {
                new FundraisingEventVM(){ FundraisingEventId = fundraisingEventId++, UsersId = 100000, CampaignId = 100000, ShelterId = 100006, ImageId = "",  Complete=false, Hidden=false, Title="Fundraising Event for " + etList[rand.Next(0, etList.Length)] + rand.Next(100),StartTime=DateTime.Today + TimeSpan.FromDays(4), EndTime = DateTime.Today + TimeSpan.FromDays(4) + TimeSpan.FromHours(4) , Description="Garble", AdditionalInfo="Things to know", Cost = 0.00M, NumOfAttendees = rand.Next(100), NumAnimalsAdopted = rand.Next(20), UpdateNotes = "Nothing to report"},
                new FundraisingEventVM(){ FundraisingEventId = fundraisingEventId++, UsersId = 100000, CampaignId = 100000, ShelterId = 100006, ImageId = "",  Complete=false, Hidden=false, Title="Fundraising Event for " + etList[rand.Next(0, etList.Length)] + rand.Next(100),StartTime=DateTime.Today + TimeSpan.FromDays(4), EndTime = DateTime.Today + TimeSpan.FromDays(4) + TimeSpan.FromHours(4) , Description="Garble", AdditionalInfo="Things to know", Cost = 0.00M, NumOfAttendees = rand.Next(100), NumAnimalsAdopted = rand.Next(20), UpdateNotes = "Nothing to report"},
                new FundraisingEventVM(){ FundraisingEventId = fundraisingEventId++, UsersId = 100000, CampaignId = 100000, ShelterId = 100006, ImageId = "",  Complete=false, Hidden=false, Title="Fundraising Event for " + etList[rand.Next(0, etList.Length)] + rand.Next(100),StartTime=DateTime.Today + TimeSpan.FromDays(4), EndTime = DateTime.Today + TimeSpan.FromDays(4) + TimeSpan.FromHours(4) , Description="Garble", AdditionalInfo="Things to know", Cost = 0.00M, NumOfAttendees = rand.Next(100), NumAnimalsAdopted = rand.Next(20), UpdateNotes = "Nothing to report"},
                new FundraisingEventVM(){ FundraisingEventId = fundraisingEventId++, UsersId = 100000, CampaignId = 100000, ShelterId = 100006, ImageId = "",  Complete=false, Hidden=false, Title="Fundraising Event for " + etList[rand.Next(0, etList.Length)] + rand.Next(100),StartTime=DateTime.Today + TimeSpan.FromDays(4), EndTime = DateTime.Today + TimeSpan.FromDays(4) + TimeSpan.FromHours(4) , Description="Garble", AdditionalInfo="Things to know", Cost = 0.00M, NumOfAttendees = rand.Next(100), NumAnimalsAdopted = rand.Next(20), UpdateNotes = "Nothing to report"},
                new FundraisingEventVM(){ FundraisingEventId = fundraisingEventId++, UsersId = 100000, CampaignId = 100004, ShelterId = 100006, ImageId = "",  Complete=false, Hidden=false, Title="Fundraising Event for " + etList[rand.Next(0, etList.Length)] + rand.Next(100),StartTime=DateTime.Today + TimeSpan.FromDays(4), EndTime = DateTime.Today + TimeSpan.FromDays(4) + TimeSpan.FromHours(4) , Description="Garble", AdditionalInfo="Things to know", Cost = 0.00M, NumOfAttendees = rand.Next(100), NumAnimalsAdopted = rand.Next(20), UpdateNotes = "Nothing to report"},
                new FundraisingEventVM(){ FundraisingEventId = fundraisingEventId++, UsersId = 100000, CampaignId = 100002, ShelterId = 100006, ImageId = "",  Complete=false, Hidden=false, Title="Fundraising Event for " + etList[rand.Next(0, etList.Length)] + rand.Next(100),StartTime=DateTime.Today + TimeSpan.FromDays(4), EndTime = DateTime.Today + TimeSpan.FromDays(4) + TimeSpan.FromHours(4) , Description="Garble", AdditionalInfo="Things to know", Cost = 0.00M, NumOfAttendees = rand.Next(100), NumAnimalsAdopted = rand.Next(20), UpdateNotes = "Nothing to report"}
            };
            return fakeFundraisingEvents.Where((e) => e.CampaignId == campaignId).ToList();
        }

        public List<FundraisingEventVM> SelectAllFundraisingEventsByShelterId(int shelterId)
        {
            //throw new NotImplementedException();
            int fundraisingEventId = 100000;
            string[] etList = { "Dogs", "Cats", "Frogs", "Snakes", "Turtles" };
            fakeFundraisingEvents = new List<FundraisingEventVM>()
            {
                new FundraisingEventVM(){ FundraisingEventId = fundraisingEventId++, UsersId = 100000, CampaignId = 100000, ShelterId = 100006, ImageId = "",  Complete=false, Hidden=false, Title="Fundraising Event for " + etList[rand.Next(0, etList.Length)] + rand.Next(100),StartTime=DateTime.Today + TimeSpan.FromDays(4), EndTime = DateTime.Today + TimeSpan.FromDays(4) + TimeSpan.FromHours(4) , Description="Garble", AdditionalInfo="Things to know", Cost = 100.00M, NumOfAttendees = 65, NumAnimalsAdopted = 4, UpdateNotes = ""},
                new FundraisingEventVM(){ FundraisingEventId = fundraisingEventId++, UsersId = 100000, CampaignId = 100000, ShelterId = 100006, ImageId = "",  Complete=false, Hidden=false, Title="Fundraising Event for " + etList[rand.Next(0, etList.Length)] + rand.Next(100),StartTime=DateTime.Today + TimeSpan.FromDays(4), EndTime = DateTime.Today + TimeSpan.FromDays(4) + TimeSpan.FromHours(4) , Description="Garble", AdditionalInfo="Things to know", Cost = 0.00M, NumOfAttendees = rand.Next(100), NumAnimalsAdopted = rand.Next(20), UpdateNotes = ""},
                new FundraisingEventVM(){ FundraisingEventId = fundraisingEventId++, UsersId = 100000, CampaignId = 100000, ShelterId = 100006, ImageId = "",  Complete=false, Hidden=false, Title="Fundraising Event for " + etList[rand.Next(0, etList.Length)] + rand.Next(100),StartTime=DateTime.Today + TimeSpan.FromDays(4), EndTime = DateTime.Today + TimeSpan.FromDays(4) + TimeSpan.FromHours(4) , Description="Garble", AdditionalInfo="Things to know", Cost = 0.00M, NumOfAttendees = rand.Next(100), NumAnimalsAdopted = rand.Next(20), UpdateNotes = ""},
                new FundraisingEventVM(){ FundraisingEventId = fundraisingEventId++, UsersId = 100000, CampaignId = 100000, ShelterId = 100006, ImageId = "",  Complete=false, Hidden=false, Title="Fundraising Event for " + etList[rand.Next(0, etList.Length)] + rand.Next(100),StartTime=DateTime.Today + TimeSpan.FromDays(4), EndTime = DateTime.Today + TimeSpan.FromDays(4) + TimeSpan.FromHours(4) , Description="Garble", AdditionalInfo="Things to know", Cost = 0.00M, NumOfAttendees = rand.Next(100), NumAnimalsAdopted = rand.Next(20), UpdateNotes = ""},
                new FundraisingEventVM(){ FundraisingEventId = fundraisingEventId++, UsersId = 100000, CampaignId = 100000, ShelterId = 100006, ImageId = "",  Complete=false, Hidden=false, Title="Fundraising Event for " + etList[rand.Next(0, etList.Length)] + rand.Next(100),StartTime=DateTime.Today + TimeSpan.FromDays(4), EndTime = DateTime.Today + TimeSpan.FromDays(4) + TimeSpan.FromHours(4) , Description="Garble", AdditionalInfo="Things to know", Cost = 0.00M, NumOfAttendees = rand.Next(100), NumAnimalsAdopted = rand.Next(20), UpdateNotes = ""},
                new FundraisingEventVM(){ FundraisingEventId = fundraisingEventId++, UsersId = 100000, CampaignId = 100000, ShelterId = 100006, ImageId = "",  Complete=false, Hidden=false, Title="Fundraising Event for " + etList[rand.Next(0, etList.Length)] + rand.Next(100),StartTime=DateTime.Today + TimeSpan.FromDays(4), EndTime = DateTime.Today + TimeSpan.FromDays(4) + TimeSpan.FromHours(4) , Description="Garble", AdditionalInfo="Things to know", Cost = 0.00M, NumOfAttendees = rand.Next(100), NumAnimalsAdopted = rand.Next(20), UpdateNotes = ""}
            };

            for (int i = 0; i < 3000; i++)
            {
                int days = rand.Next(10);
                int siRand = rand.Next(4);
                fakeFundraisingEvents.Add(new FundraisingEventVM()
                {
                    FundraisingEventId = fundraisingEventId++,
                    UsersId = 100000,
                    CampaignId = 100000,
                    ShelterId = 100000 + siRand,
                    ImageId = "",
                    Complete = false,
                    Hidden = false,
                    Title = "Fundraising Event for " + etList[rand.Next(0, etList.Length)] + rand.Next(100),
                    StartTime = DateTime.Today + TimeSpan.FromDays(days),
                    EndTime = DateTime.Today + TimeSpan.FromDays(days) + TimeSpan.FromHours(4),
                    Description = "Garble",
                    AdditionalInfo = "Things to know",
                    Cost = (decimal)rand.NextDouble() * rand.Next(2000),
                    NumOfAttendees = rand.Next(100),
                    NumAnimalsAdopted = rand.Next(20),
                    UpdateNotes = "Nothing to report"
                });
            }

            return fakeFundraisingEvents.Where(fe => fe.ShelterId == shelterId).ToList();
        }

        public FundraisingEventVM SelectFundraisingEventByFundraisingEventId(int fundraisingEventId)
        {
            return _fundraisingEvents.First(fundraisingEvent => fundraisingEvent.FundraisingEventId == fundraisingEventId);
        }

        public int UpdateFundraisingEventResults(FundraisingEventVM oldFundraisingEventVM, FundraisingEventVM newFundraisingEventVM)
        {
            // throw new NotImplementedException();
            int recordsChanged = 0;
            var testEvent = _fundraisingEvents.Find(fundraisingEvent => fundraisingEvent.FundraisingEventId == oldFundraisingEventVM.FundraisingEventId);
            //simulate check for concurrency
            if (oldFundraisingEventVM.Cost != testEvent.Cost || oldFundraisingEventVM.NumOfAttendees != testEvent.NumOfAttendees || oldFundraisingEventVM.NumAnimalsAdopted != testEvent.NumAnimalsAdopted || oldFundraisingEventVM.UpdateNotes != testEvent.UpdateNotes)
            {
                return 0;
            }
            else
            {
                //remove exisiting fundraising event with id that matches old event
                _fundraisingEvents.Remove(testEvent);
                //add the updated event in
                _fundraisingEvents.Add(newFundraisingEventVM);
                recordsChanged++;
                return recordsChanged;

            }
        }

        public List<FundraisingEventVM> SelectAllActiveFundraisingEventsByShelterId(int shelterId)
        {
            //throw new NotImplementedException();
            //List<FundraisingEventVM> fundraisingEvents = new List<FundraisingEventVM>();
            //fundraisingEvents.Add(new FundraisingEventVM { FundraisingEventId = 1000006, Description = "Test Event", ShelterId = 1000006 });
            
        var fundraisingEvents = from eventrecord in fakeFundraisingEventsActive
                                join shelter in fakeShelterList on eventrecord.ShelterId equals shelter.ShelterId
                                where eventrecord.Hidden == false && eventrecord.Complete == false && eventrecord.ShelterId == shelterId && shelter.ShelterActive == true
                                select eventrecord;

            return fundraisingEvents.ToList();
        }

        public List<FundraisingEventVM> SelectAllActiveFundraisingEvents()
        {
            //throw new NotImplementedException();
            //List<FundraisingEventVM> fundraisingEvents = new List<FundraisingEventVM>();
            //fundraisingEvents.Add(new FundraisingEventVM { FundraisingEventId = 1000006, Description = "Test Event", ShelterId = 1000006 });
            //fundraisingEvents.Add(new FundraisingEventVM { FundraisingEventId = 1000006, Description = "Test Event", ShelterId = 1000006 });
            //fundraisingEvents.Add(new FundraisingEventVM { FundraisingEventId = 1000006, Description = "Test Event", ShelterId = 1000006 });
            //fundraisingEvents.Add(new FundraisingEventVM { FundraisingEventId = 1000006, Description = "Test Event", ShelterId = 1000006 });

            var fundraisingEvents = from eventrecord in fakeFundraisingEventsActive
                                    join shelter in fakeShelterList on eventrecord.ShelterId equals shelter.ShelterId
                                    where eventrecord.Hidden == false && eventrecord.Complete == false && shelter.ShelterActive == true
                                    select eventrecord;

            return fundraisingEvents.ToList();

        }
    }
}
