using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayerInterfaces;
using DataObjects;
using DataAccessLayerInterfaces;
using DataAccessLayer;

namespace LogicLayer
{
    public class AdoptionApplicationManager : IAdoptionApplicationManager
    {
        private IAdoptionApplicationAccessor _adoptionApplicationAccessor = null;

        public AdoptionApplicationManager()
        {
            _adoptionApplicationAccessor = new AdoptionApplicationAccessor();
        }

        public AdoptionApplicationManager(IAdoptionApplicationAccessor adoptionApplicationAccessor)
        {
            _adoptionApplicationAccessor = adoptionApplicationAccessor;
        }

        public bool AddAdoptionApplication(AdoptionApplicationVM adoptionApplication)
        {
            bool success = false;
            try
            {
                if (1 == _adoptionApplicationAccessor.InsertAdoptionApplication(adoptionApplication))
                {
                    success = true;
                }
            }
            catch (Exception up)
            {
                throw new ApplicationException("Adoption application failed to add", up);
            }
            return success;
        }

        public bool EditAdoptionApplicationStatusByAnimalIdForApprovedApplication(AdoptionApplicationResponse response)
        {
            bool updated = false;

            try
            {
                updated = 0 < _adoptionApplicationAccessor.UpdateAdoptionApplicationStatusByAnimalIdForApprovedApplication(response);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return updated;
        }

        public List<AdoptionApplicationVM> RetrieveAllAdoptionApplicationsByAnimalId(int animalId)
        {
            List<AdoptionApplicationVM> applications = null;
            try
            {
                applications = _adoptionApplicationAccessor.SelectAllAdoptionApplicationsByAnimalId(animalId);
            }
            catch (Exception e)
            {
                throw new ApplicationException("Failed to retrieve adoption applications", e);
            }
            return applications;
        }

        public List<string> RetrieveAllHomeOwnershipTypes()
        {
            List<string> types = null;
            try
            {
                types = _adoptionApplicationAccessor.SelectAllHomeOwnershipTypes();
            }
            catch (Exception e)
            {
                throw new ApplicationException("Failed to retrieve home ownership types", e);
            }
            return types;
        }

        public List<string> RetrieveAllHomeTypes()
        {
            List<string> types = null;
            try
            {
                types = _adoptionApplicationAccessor.SelectAllHomeTypes();
            }
            catch (Exception e)
            {
                throw new ApplicationException("Failed to retrieve home types", e);
            }
            return types;
        }

        public List<AdoptionApplicationVM> RetrieveAllAdoptionApplicationsByUsersId(int usersId)
        {
            List<AdoptionApplicationVM> applications = null;
            try
            {
                applications = _adoptionApplicationAccessor.SelectAllAdoptionApplicationsByUsersId(usersId);
            }
            catch (Exception e)
            {
                throw new ApplicationException("Failed to retrieve adoption applications", e);
            }
            return applications;
        }
    }
}
