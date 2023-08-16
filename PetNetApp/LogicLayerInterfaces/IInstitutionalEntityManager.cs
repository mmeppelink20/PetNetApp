/// <summary>
/// Barry Mikulas
/// Created: 2023/03/01
/// 
/// Contains interfaces for Instutional Entity functions
/// </summary>
/// <remarks>
/// Oleksiy Fedchuk
/// Updated: 2023/04/27
/// 
/// Final QA
/// </remarks>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayerInterfaces
{
    public interface IInstitutionalEntityManager
    {
        /// <summary>
        /// Stephen Jaurigue
        /// Created: 2023/03/04
        /// 
        /// Uses the Accessor method to retrieve Sponsors for the campaign and rewraps exceptions
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/27
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="campaignId"></param>
        /// <returns></returns>
        List<InstitutionalEntity> RetrieveFundraisingSponsorsByCampaignId(int campaignId);

        /// <summary>
        /// Stephen Jaurigue
        /// Created: 2023/03/05
        /// 
        /// Uses the Accessor method to retrieve all Institutional entities that are sponsors
        /// </summary>
        /// <returns></returns>
        List<InstitutionalEntity> RetrieveAllSponsors();

        /// <summary>
        /// Barry Mikulas
        /// Created: 2023/03/04
        /// 
        /// Uses the Accessor method to retrieve Institutional Entities of the ContactType
        /// for the event and rewraps exceptions
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="contactType">of types Sponsor, Host, or Contact</param>
        /// <returns></returns>
        List<InstitutionalEntity> RetrieveFundraisingInstitutionalEntitiesByEventIdAndContactType(int eventId, string contactType);

        /// <summary>
        /// Barry Mikulas
        /// Created: 2023/03/01
        /// 
        /// Retrieves a list of all InstitutionalEntities for by shelterId and entityType
        /// </summary>
        ///
        /// <remarks>
        /// Updater
        /// Updated: 2023/02/24
        /// Added Comment.
        /// </remarks>
        /// <param name="shelterId"/>
        /// <param name="entityType"/>
        /// <exception cref="SQLException">Retrieve fails.</exception>
        /// <returns>List of InstitutionalEntity</returns>
        List<InstitutionalEntity> RetrieveAllInstitutionalEntitiesByShelterIdAndEntityType(int shelterId, string entityType);

        /// <summary>
        /// Barry Mikulas
        /// Created: 2023/03/01
        /// 
        /// Retrieves a list of all InstitutionalEntities for a shelter
        /// </summary>
        ///
        /// <remarks>
        /// Updater
        /// Updated: 2023/02/24
        /// Added Comment.
        /// </remarks>
        /// <param name="institutionalEntityId"/>
        /// <exception cref="SQLException">Retrieve fails.</exception>
        /// <returns>Returns an InstitutionalEntity</returns>
        InstitutionalEntity RetrieveInstitutionalEntityByInstitutionalEntityId(int institutionalEntityId);

        /// <summary>
        /// Andrew
        /// Created: 3/9/2023
        /// 
        /// Adds institutional entity to the database
        /// </summary>
        ///
        /// <remarks>
        /// Updater name:
        /// Updated: date
        /// Comments:
        /// </remarks>
        /// <param name="shelterId">The Id of the shelter to be added</param>
        /// <exception cref="ApplicationException">Insert Fails</exception>
        /// <returns>Boolean indicating success</returns>
        bool AddInstitutionalEntity(InstitutionalEntity institutionalEntity);

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/03/09
        /// 
        /// Edots an institutional entity record using an "old" entity
        /// object and a "new" edited entity object
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// example:  Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="oldEntity">Entity object holding old data</param>
        /// <param name="newEntity">Entity object holding new edited data</param>
        /// <exception cref="ApplicationException">Edit Fails</exception>
        bool EditInstitutionalEntity(InstitutionalEntity oldEntity, InstitutionalEntity newEntity);

        /// <summary>
        /// Barry Mikulas
        /// Created 03/16/2023
        /// 
        /// Retrieve an institutional entity for an event given an eventId and contactType
        /// Mainly used for contact type Host
        /// </summary>
        /// 
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// example:  Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="fundraisingEventId"></param>
        /// <param name="contactType"></param>
        /// <returns></returns>
        InstitutionalEntity RetrieveInstitutionalEntityByEventIdAndContactType(int fundraisingEventId, string contactType);

        /// <summary>
        /// Ethan Kline 
        /// Created: 2023/03/30
        /// 
        /// Retrieves a list of all InstitutionalEntities by name
        /// </summary>
        ///
        /// <param name="name"/>
        /// <exception cref="SQLException">Retrieve fails.</exception>
        /// <returns>List of InstitutionalEntity</returns>
        List<SponsorEvent> RetrieveSponsorEventByName(String name);

        List<InstitutionalEntity> RetrieveAllHosts();

        List<InstitutionalEntity> RetrieveAllContact();
    }
}
