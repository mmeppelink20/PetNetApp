/// <summary>
/// Barry Mikulas
/// Created: 2023/03/01
/// 
/// Methods for Institutional Entity 
/// </summary>
///
/// <remarks>
/// Updater
/// Updated: 
/// Comments:
/// </remarks>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using LogicLayerInterfaces;
using DataAccessLayer;
using DataAccessLayerInterfaces;


namespace LogicLayer
{

    /// <summary>
    /// Stephen Jaurigue
    /// Created: 2023/02/23
    /// 
    /// The Logic Layer class for managing institutional entities
    /// </summary>
    public class InstitutionalEntityManager : IInstitutionalEntityManager
    {

        private IInstitutionalEntityAccessor _institutionalEntityAccessor = null;


        public InstitutionalEntityManager()
        {
            _institutionalEntityAccessor = new InstitutionalEntityAccessor();
        }
        public InstitutionalEntityManager(IInstitutionalEntityAccessor institutionalEntityAccessor)
        {
            _institutionalEntityAccessor = institutionalEntityAccessor;
        }

        public List<InstitutionalEntity> RetrieveAllContact()
        {
            List<InstitutionalEntity> contacts = null;

            try
            {
                contacts = _institutionalEntityAccessor.SelectAllContact();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failded to load contacts", ex);
            }

            return contacts;
        }

        public List<InstitutionalEntity> RetrieveAllHosts()
        {
            List<InstitutionalEntity> hosts = null;
            try
            {
                hosts = _institutionalEntityAccessor.SelectAllHosts();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to load hosts", ex);
            }
            return hosts;
        }

        public List<InstitutionalEntity> RetrieveAllSponsors()
        {
            List<InstitutionalEntity> sponsors = null;
            try
            {
                sponsors = _institutionalEntityAccessor.SelectAllSponsors();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to load sponsors", ex);
            }
            return sponsors;
        }

        public List<InstitutionalEntity> RetrieveFundraisingSponsorsByCampaignId(int campaignId)
        {
            List<InstitutionalEntity> sponsors = null;
            try
            {
                sponsors = _institutionalEntityAccessor.SelectFundraisingSponsorsByCampaignId(campaignId);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to load sponsors", ex);
            }
            return sponsors;
        }
        public List<InstitutionalEntity> RetrieveAllInstitutionalEntitiesByShelterIdAndEntityType(int shelterId, string entityType)
        {
            List<InstitutionalEntity> institutionalEntities = new List<InstitutionalEntity>();
            try
            {
                institutionalEntities = _institutionalEntityAccessor.SelectAllInstitutionalEntitiesByShelterIdAndEntityType(shelterId, entityType);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Failed to load institutional entities", ex);
            }
            return institutionalEntities;
        }

        public InstitutionalEntity RetrieveInstitutionalEntityByInstitutionalEntityId(int institutionalEntityId)
        {
            InstitutionalEntity institutionalEntity = new InstitutionalEntity();
            try
            {
                institutionalEntity = _institutionalEntityAccessor.SelectInstitutionalEntityByInstitutionalEntityId(institutionalEntityId);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Entity record not found.", ex);
            }
            return institutionalEntity;
        }

        public bool AddInstitutionalEntity(InstitutionalEntity institutionalEntity)
        {
            int id = 0;
            try
            {
                id = _institutionalEntityAccessor.InsertInstitutionalEntity(institutionalEntity);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Entity record failed to be added", ex);
            }
            return id != 0;
        }

        public bool EditInstitutionalEntity(InstitutionalEntity oldEntity, InstitutionalEntity newEntity)
        {
            try
            {
                return 1 == _institutionalEntityAccessor.UpdateInstitutionalEntity(oldEntity, newEntity);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while updating record.", ex);
            }
        }

        public List<InstitutionalEntity> RetrieveFundraisingInstitutionalEntitiesByEventIdAndContactType(int eventId, string contactType)
        {
            List<InstitutionalEntity> institutionalEntities = null;
            try
            {
                institutionalEntities = _institutionalEntityAccessor.SelectFundraisingEventInstitutionalEntitiesByEventIdAndContactType(eventId, contactType);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to load event " + contactType.ToLower() + "s.", ex);
            }
            return institutionalEntities;
        }

        public InstitutionalEntity RetrieveInstitutionalEntityByEventIdAndContactType(int fundraisingEventId, string contactType)
        {
            //throw new NotImplementedException();
            InstitutionalEntity institutionalEntity = null;

            try
            {
                institutionalEntity = _institutionalEntityAccessor.SelectInstitutionalEntityByFundraisingEventIdAndContactType(fundraisingEventId, contactType);
            }
            catch (Exception ex)
            {

                throw new AccessViolationException("Failed to load a " + contactType + " for the event.", ex);
            }

            return institutionalEntity;
        }

        public List<SponsorEvent> RetrieveSponsorEventByName(String name)
        {
            List<SponsorEvent> institutionalEntitys = new List<SponsorEvent>();
            try
            {
                institutionalEntitys = _institutionalEntityAccessor.SelectSponsorEventByName(name);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Entity record not found.", ex);
            }
            return institutionalEntitys;
        }
    }
}
