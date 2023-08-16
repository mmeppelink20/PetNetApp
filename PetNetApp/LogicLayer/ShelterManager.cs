using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LogicLayerInterfaces;
using DataAccessLayerInterfaces;

using DataAccessLayer; // For testing

using DataObjects;

namespace LogicLayer
{   /// <summary>
    /// Brian Collum
    /// Created: 2023/02/23
    /// ShelterManager relays shelter data between the presentation and data access layers 
    /// </summary>
    public class ShelterManager : IShelterManager
    {
        private IShelterAccessor _shelterAccessor = null;
        public ShelterManager()
        {
            _shelterAccessor = new DataAccessLayer.ShelterAccessor();
        }
        // Unit test constructor
        public ShelterManager(IShelterAccessor shelterAccessor)
        {
            _shelterAccessor = shelterAccessor;
        }
        public List<Shelter> GetShelterList()
        {
            List<Shelter> shelterList = _shelterAccessor.RetrieveShelterList();
            return shelterList;
        }
        public bool AddShelter(string shelterName, string address, string Address2, string zipCode, string phone, string email, string areasOfNeed, bool shelterActive)
        {
            bool result = false;
            try
            {
                //Confirm non-nullable fields have values
                if (!(
                    shelterName == null || shelterName.Equals("")
                    || address == null || address.Equals("")
                    || zipCode == null || zipCode.Equals("")
                    ))
                {
                    result = _shelterAccessor.InsertShelter(shelterName, address, Address2, zipCode, phone, email, areasOfNeed, shelterActive);
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to add shelter.", ex);
            }
            return result;
        }
        public ShelterVM RetrieveShelterVMByShelterID(int shelterID)
        {
            try
            {
                ShelterVM returnShelter = new ShelterVM();
                returnShelter = (ShelterVM)_shelterAccessor.SelectShelterVMByShelterID(shelterID);
                return returnShelter;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed retrieve shelter.", ex);
            }
        }
        public bool EditAddress(Shelter shelter, string newAddress)
        {
            int itemsUpdated = 0;
            try
            {
                if (!(shelter.Address.Equals(newAddress) || newAddress == null || newAddress.Equals("")))
                {
                    itemsUpdated = _shelterAccessor.UpdateAddressByShelterID(shelter.ShelterId, shelter.Address, newAddress);
                }
                if (itemsUpdated == 1) { return true; }
                else { return false; }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed update shelter address.", ex);
            }
        }
        public bool EditAddress2(Shelter shelter, string newAddress2)
        {
            try
            {
                int itemsUpdated = 0;
                if (!shelter.Address2.Equals(newAddress2))
                {
                    itemsUpdated = _shelterAccessor.UpdateAddress2ByShelterID(shelter.ShelterId, newAddress2);
                }
                if (itemsUpdated == 1) { return true; }
                else { return false; }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed update shelter address.", ex);
            }
        }

        public bool EditAreasOfNeed(Shelter shelter, string newAreasOfNeed)
        {
            try
            {
                int itemsUpdated = 0;
                if (!shelter.AreasOfNeed.Equals(newAreasOfNeed))
                {
                    itemsUpdated = _shelterAccessor.UpdateAreasOfNeedByShelterID(shelter.ShelterId, newAreasOfNeed);
                }
                if (itemsUpdated == 1) { return true; }
                else { return false; }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed update shelter areas of need.", ex);
            }
        }
        public bool EditEmail(Shelter shelter, string newEmail)
        {
            try
            {
                int itemsUpdated = 0;
                if (!shelter.Email.Equals(newEmail))
                {
                    itemsUpdated = _shelterAccessor.UpdateEmailByShelterID(shelter.ShelterId, newEmail);
                }
                if (itemsUpdated == 1) { return true; }
                else { return false; }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed update shelter email.", ex);
            }
        }
        public bool EditPhone(Shelter shelter, string newPhone)
        {
            try
            {
                int itemsUpdated = 0;
                if (!shelter.Phone.Equals(newPhone))
                {
                    itemsUpdated = _shelterAccessor.UpdatePhoneByShelterID(shelter.ShelterId, newPhone);
                }
                if (itemsUpdated == 1) { return true; }
                else { return false; }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed update shelter phone number.", ex);
            }
        }
        public bool EditShelterName(Shelter shelter, string newShelterName)
        {
            try
            {
                int itemsUpdated = 0;
                if (!(shelter.ShelterName.Equals(newShelterName) || newShelterName == null || newShelterName.Equals("")))
                {
                    itemsUpdated = _shelterAccessor.UpdateShelterNameByShelterID(shelter.ShelterId, shelter.ShelterName, newShelterName);
                }
                if (itemsUpdated == 1) { return true; }
                else { return false; }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed update shelter name.", ex);
            }
        }
        public bool EditZipCode(Shelter shelter, string newZipcode)
        {
            try
            {
                int itemsUpdated = 0;
                if (!(shelter.ZipCode.Equals(newZipcode) || newZipcode == null || newZipcode.Equals("")))
                {
                    itemsUpdated = _shelterAccessor.UpdateZipCodeByShelterID(shelter.ShelterId, shelter.ZipCode, newZipcode);
                }
                if (itemsUpdated == 1) { return true; }
                else { return false; }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed update shelter zip code.", ex);
            }
        }
        public bool EditActiveStatus(Shelter shelter, bool newActiveStatus)
        {
            try
            {
                int itemsUpdated = 0;
                if (shelter.ShelterActive != newActiveStatus)
                {
                    itemsUpdated = _shelterAccessor.UpdateActiveStatusByShelterID(shelter.ShelterId, shelter.ShelterActive, newActiveStatus);
                }
                if (itemsUpdated == 1) { return true; }
                else { return false; }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed update shelter active status.", ex);
            }
        }
        public bool DeactivateShelter(Shelter shelter)
        {
            try
            {
                int itemsUpdated = 0;
                itemsUpdated = _shelterAccessor.DeactivateShelterByShelterID(shelter.ShelterId);
                if (itemsUpdated == 1) { return true; }
                else { return false; }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed deactivate shelter.", ex);
            }
        }

        public List<HoursOfOperation> RetrieveHoursOfOperationByShelterID(int shelterID)
        {
            try
            {
                List<HoursOfOperation> activeHours = _shelterAccessor.SelectHoursOfOperationByShelterID(shelterID);
                return activeHours;
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Failed to retrieve hours of operation.", ex);
            }
        }

        public bool EditHoursOfOperationByShelterID(int shelterID, int dayOfWeek, HoursOfOperation hours)
        {
            try
            {
                int hoursUpdated = 0;
                hoursUpdated = _shelterAccessor.UpdateHoursOfOperationByShelterID(shelterID, dayOfWeek, hours);
                if (hoursUpdated == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to update hours of operation.", ex);
            }
        }
    }
}
