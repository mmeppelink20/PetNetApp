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
    /// Brian Collum
    /// Created: 2023/02/23
    /// ShelterAccessorFakes contains fake data for ShelterManager unit tests
    /// </summary>
    public class ShelterAccessorFakes : IShelterAccessor
    {
        List<Shelter> shelterList = new List<Shelter>();
        List<ShelterVM> shelterVMList = new List<ShelterVM>();
        List<List<HoursOfOperation>> hoursOfOperationList = new List<List<HoursOfOperation>>();

        public ShelterAccessorFakes()
        {
            shelterList.Add(new Shelter
            {
                ShelterId = 0,
                ShelterName = "Test Shelter 00",
                Address = "Fake area 00",
                Address2 = "Fake address 00",
                ZipCode = "50001",
                Phone = "000-000-0000",
                Email = "zero@zero.zerp",
                AreasOfNeed = "There is no shelter here",
                ShelterActive = false
            });
            shelterList.Add(new Shelter
            {
                ShelterId = 1,
                ShelterName = "Test Shelter 01",
                Address = "Fake area 01",
                Address2 = "Fake address 01",
                ZipCode = "50001",
                Phone = "555-666-7777",
                Email = "fake@fake.fake",
                AreasOfNeed = "Need fake things",
                ShelterActive = true
            });
            shelterList.Add(new Shelter
            {
                ShelterId = 2,
                ShelterName = "Test Shelter 02",
                Address = "Fake area 02",
                Address2 = null,
                ZipCode = "50002",
                Phone = null,
                Email = null,
                AreasOfNeed = null,
                ShelterActive = false
            });

            // New shelter hours list not to mess with existing tests

            List<HoursOfOperation> HoursOfOperation1 = new List<HoursOfOperation>
                {
                    new HoursOfOperation { OpenHour = TimeSpan.Parse("09:00:00"), CloseHour = TimeSpan.Parse("05:00:00")},
                    new HoursOfOperation { OpenHour = TimeSpan.Parse("10:00:00"), CloseHour = TimeSpan.Parse("06:00:00")},
                    new HoursOfOperation { OpenHour = TimeSpan.Parse("10:00:00"), CloseHour = TimeSpan.Parse("06:00:00")},
                    new HoursOfOperation { OpenHour = TimeSpan.Parse("09:00:00"), CloseHour = TimeSpan.Parse("05:00:00")},
                    new HoursOfOperation { OpenHour = TimeSpan.Parse("11:00:00"), CloseHour = TimeSpan.Parse("07:00:00")},
                    new HoursOfOperation { OpenHour = TimeSpan.Parse("11:00:00"), CloseHour = TimeSpan.Parse("07:00:00")},
                    new HoursOfOperation { OpenHour = TimeSpan.Parse("09:00:00"), CloseHour = TimeSpan.Parse("05:00:00")}
                };
            List<HoursOfOperation> HoursOfOperation2 = new List<HoursOfOperation>
                {
                    new HoursOfOperation { OpenHour = TimeSpan.Parse("09:40:00"), CloseHour = TimeSpan.Parse("05:00:00")},
                    new HoursOfOperation { OpenHour = TimeSpan.Parse("10:23:00"), CloseHour = TimeSpan.Parse("06:00:00")},
                    new HoursOfOperation { OpenHour = TimeSpan.Parse("10:03:00"), CloseHour = TimeSpan.Parse("06:00:00")},
                    new HoursOfOperation { OpenHour = TimeSpan.Parse("09:43:00"), CloseHour = TimeSpan.Parse("05:00:00")},
                    new HoursOfOperation { OpenHour = TimeSpan.Parse("11:03:00"), CloseHour = TimeSpan.Parse("07:00:00")},
                    new HoursOfOperation { OpenHour = TimeSpan.Parse("11:32:00"), CloseHour = TimeSpan.Parse("07:00:00")},
                    new HoursOfOperation { OpenHour = TimeSpan.Parse("09:40:00"), CloseHour = TimeSpan.Parse("05:00:00")}
                };
            List<HoursOfOperation> HoursOfOperation3 = new List<HoursOfOperation>
                {
                    new HoursOfOperation { OpenHour = TimeSpan.Parse("09:59:00"), CloseHour = TimeSpan.Parse("05:00:00")},
                    new HoursOfOperation { OpenHour = TimeSpan.Parse("10:58:00"), CloseHour = TimeSpan.Parse("06:00:00")},
                    new HoursOfOperation { OpenHour = TimeSpan.Parse("10:57:00"), CloseHour = TimeSpan.Parse("06:00:00")},
                    new HoursOfOperation { OpenHour = TimeSpan.Parse("09:56:00"), CloseHour = TimeSpan.Parse("05:00:00")},
                    new HoursOfOperation { OpenHour = TimeSpan.Parse("11:55:00"), CloseHour = TimeSpan.Parse("07:00:00")},
                    new HoursOfOperation { OpenHour = TimeSpan.Parse("11:54:00"), CloseHour = TimeSpan.Parse("07:00:00")},
                    new HoursOfOperation { OpenHour = TimeSpan.Parse("09:53:00"), CloseHour = TimeSpan.Parse("05:00:00")}
                };

            hoursOfOperationList.Add(HoursOfOperation1);
            hoursOfOperationList.Add(HoursOfOperation2);
            hoursOfOperationList.Add(HoursOfOperation3);

        }
        public int DeactivateShelterByShelterID(int shelterID)
        {
            shelterList[shelterID].ShelterActive = false;
            return 1;
        }
        public bool InsertShelter(string shelterName, string address, string Address2, string zipCode, string phone, string email, string areasOfNeed, bool shelterActive)
        {
            Shelter newShelter = new Shelter();
            newShelter.ShelterId = 000003;  // Arbitrary value must be added
            newShelter.ShelterName = shelterName;
            newShelter.Address = address;
            newShelter.Address2 = Address2;
            newShelter.ZipCode = zipCode;
            newShelter.Phone = phone;
            newShelter.Email = email;
            newShelter.AreasOfNeed = areasOfNeed;
            newShelter.ShelterActive = shelterActive;
            shelterList.Add(newShelter);
            return true;
        }
        public List<Shelter> RetrieveShelterList()
        {
            return shelterList;
        }
        public ShelterVM SelectShelterVMByShelterID(int shelterID)
        {
            int i = 0;
            List<ShelterVM> shelterVMs = new List<ShelterVM>();
            foreach (var shelter in shelterList)
            {
                shelterVMs.Add(new ShelterVM
                {
                    ShelterId = shelter.ShelterId,
                    ShelterName = shelter.ShelterName,
                    Address = shelter.Address,
                    Address2 = shelter.Address,
                    ZipCode = shelter.ZipCode,
                    Phone = shelter.Phone,
                    Email = shelter.Email,
                    AreasOfNeed = shelter.AreasOfNeed,
                    ShelterActive = shelter.ShelterActive,
                    HoursOfOperation = hoursOfOperationList[i]
                });
                i++;
            }
            shelterVMList = shelterVMs;

            ShelterVM returnShelter = new ShelterVM();
            returnShelter.ShelterId = shelterList[shelterID].ShelterId;
            returnShelter.ShelterName = shelterList[shelterID].ShelterName;
            returnShelter.Address = shelterList[shelterID].Address;
            returnShelter.Address2 = shelterList[shelterID].Address2;
            returnShelter.ZipCode = shelterList[shelterID].ZipCode;
            returnShelter.Phone = shelterList[shelterID].Phone;
            returnShelter.Email = shelterList[shelterID].Email;
            returnShelter.AreasOfNeed = shelterList[shelterID].AreasOfNeed;
            returnShelter.ShelterActive = shelterList[shelterID].ShelterActive;
            returnShelter.HoursOfOperation = shelterVMs[shelterID].HoursOfOperation;
            return returnShelter;
        }
        public int UpdateActiveStatusByShelterID(int shelterID, bool oldActiveStatus, bool newActiveStatus)
        {
            if (oldActiveStatus != newActiveStatus)
            {
                shelterList[shelterID].ShelterActive = newActiveStatus;
                return 1;
            }
            else
            {
                return 0;
            }
        }
        public int UpdateAddressByShelterID(int shelterID, string oldAddress, string newAddress)
        {
            if (oldAddress != newAddress)
            {
                shelterList[shelterID].Address = newAddress;
                return 1;
            }
            else
            {
                return 0;
            }
        }
        public int UpdateAddress2ByShelterID(int shelterID, string newAddress2)
        {
            shelterList[shelterID].Address2 = newAddress2;
            return 1;
        }
        public int UpdateAreasOfNeedByShelterID(int shelterID, string newAreasOfNeed)
        {
            shelterList[shelterID].AreasOfNeed = newAreasOfNeed;
            return 1;
        }
        public int UpdateEmailByShelterID(int shelterID, string newEmail)
        {
            shelterList[shelterID].Email = newEmail;
            return 1;
        }
        public int UpdatePhoneByShelterID(int shelterID, string newPhone)
        {
            shelterList[shelterID].Phone = newPhone;
            return 1;
        }
        public int UpdateShelterNameByShelterID(int shelterID, string oldShelterName, string newShelterName)
        {
            if (oldShelterName != newShelterName)
            {
                shelterList[shelterID].ShelterName = newShelterName;
                return 1;
            }
            else
            {
                return 0;
            }
        }
        public int UpdateZipCodeByShelterID(int shelterID, string oldZipCode, string newZipCode)
        {
            if (oldZipCode != newZipCode)
            {
                shelterList[shelterID].ZipCode = newZipCode;
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public List<HoursOfOperation> SelectHoursOfOperationByShelterID(int shelterID)
        {
            List<ShelterVM> shelterVM = shelterVMList;
            return shelterVMList[shelterID].HoursOfOperation;
        }

        public int UpdateHoursOfOperationByShelterID(int shelterID, int dayOfWeek, HoursOfOperation hours)
        {
            if ((hours.OpenHour.ToString() != shelterVMList[shelterID].HoursOfOperation[dayOfWeek - 1].OpenHour.ToString()) || (hours.CloseHour.ToString() != shelterVMList[shelterID].HoursOfOperation[dayOfWeek - 1].CloseHour.ToString()))
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}
