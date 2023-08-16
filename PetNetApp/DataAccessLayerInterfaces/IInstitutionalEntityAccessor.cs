/// <summary>
/// Asa Armstrong
/// Created: 2023/03/01
/// 
/// Data Accessor class to CRUD Institutional Entity objects.
/// </summary>
///
/// <remarks>
/// Asa Armstrong
/// Updated: 2023/03/01
/// Created
/// </remarks>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;


namespace DataAccessLayerInterfaces
{
    public interface IInstitutionalEntityAccessor
    {
        /// <summary>
        /// Stephen Jaurigue
        /// Created: 2023/03/05
        /// 
        /// Gets a list of all the institutional entities that are sponsors for the fundraising campaign
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/27
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="fundraisingCampaignId">Id of the campaign</param>
        /// <returns></returns>
        List<InstitutionalEntity> SelectFundraisingSponsorsByCampaignId(int fundraisingCampaignId);

        /// <summary>
        /// Stephen Jaurigue
        /// Created: 2023/03/05
        /// 
        /// Gets a list of all the institutional entities that are sponsors
        /// </summary>
        /// <returns></returns>
        List<InstitutionalEntity> SelectAllSponsors();

        /// <summary>
        /// Asa Armstrong
        /// Created: 2023/03/01
        /// 
        /// Selects all Institutional Entities for a shelter.
        /// </summary>
        ///
        /// <remarks>
        /// Asa Armstrong
        /// Updated: 2023/03/01
        /// Created
        /// </remarks>
        /// <param name="shelterId">int</param>
        /// <param name="entityType">type of entity to select</param>
        /// <exception cref="SQLException">Select fails.</exception>
        /// <returns>List<InstitutionalEntity></returns>
        List<InstitutionalEntity> SelectAllInstitutionalEntitiesByShelterIdAndEntityType(int shelterId, string entityType);

        /// <summary>
        /// Asa Armstrong
        /// Created: 2023/03/01
        /// 
        /// Selects an Institutional Entity by its Id.
        /// </summary>
        ///
        /// <remarks>
        /// Asa Armstrong
        /// Updated: 2023/03/01
        /// Created
        /// </remarks>
        /// <param name="InstitutionalEntityId">int</param>
        /// <exception cref="SQLException">Select fails.</exception>
        /// <returns>InstitutionalEntityId</returns>
        InstitutionalEntity SelectInstitutionalEntityByInstitutionalEntityId(int institutionalEntityId);

        /// <summary>
        /// Andrew
        /// Created: 3/9/2023
        /// 
        /// Inserts institutional entity into the database
        /// </summary>
        ///
        /// <remarks>
        /// Updater name:
        /// Updated: date
        /// Comments:
        /// </remarks>
        /// <param name="institutionalEntity">The Institutional Entity to be added</param>
        /// <exception cref="ApplicationException">Insert Fails</exception>
        /// <returns>New institutional entity Id</returns>
        int InsertInstitutionalEntity(InstitutionalEntity institutionalEntity);

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/03/09
        /// 
        /// Updates an institutional entity record using an "old" entity
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
        /// <exception cref="ApplicationException">Update Fails</exception>
        int UpdateInstitutionalEntity(InstitutionalEntity oldEntity, InstitutionalEntity newEntity);

        /// <summary>
        /// Barry Mikulas
        /// Created: 2023/03/15
        /// 
        /// Gets a list of all the institutional entities that are of contact type for the fundraising event
        /// Contact types are Sponsor, Contact, Host
        /// </summary>
        /// <param name="fundraisingEventId">Id of the fundraising event</param>
        /// <param name="contactType">type of institutional entity list attempting to retrieve</param>
        /// <returns></returns>
        List<InstitutionalEntity> SelectFundraisingEventInstitutionalEntitiesByEventIdAndContactType(int fundraisingEventId, string contactType);

        /// <summary>
        /// Barry Mikulas
        /// Created: 2023/03/16
        /// 
        /// Gets a single institutional entity for the given event id and contact type.
        /// used to retrieve the Host for an event
        /// </summary>
        /// <param name="fundraisingEventId">Id of the fundraising event</param>
        /// <param name="contactType">type of institutional entity attempting to retrieve<</param>
        /// <returns></returns>
        InstitutionalEntity SelectInstitutionalEntityByFundraisingEventIdAndContactType(int fundraisingEventId, string contactType);
        /// <summary>
        /// Ethan Kline 
        /// Created: 2023/03/30
        /// 
        /// Gets a list of all the institutional entities by name
        /// </summary>
        /// <param name="name">object companyname</param>
        /// <exception cref="ApplicationException">Retrieval Fails</exception>
        List<SponsorEvent> SelectSponsorEventByName(String name);

        List<InstitutionalEntity> SelectAllHosts();
        List<InstitutionalEntity> SelectAllContact();
    }
}