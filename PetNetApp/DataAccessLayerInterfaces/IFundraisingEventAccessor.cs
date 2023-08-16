using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayerInterfaces
{
    public interface IFundraisingEventAccessor
    {
        /// <summary>
        /// Barry Mikulas
        /// Created: 2023/03/05
        /// 
        /// A method to get the fundraising events for the logged in user's shelter id
        /// </summary>
        /// 
        /// <param name="shelterId">ShelterId to select all the Fundraising Events for</param>
        /// <exception cref="SQLException">Load Fails</exception>
        /// <returns>List<FundraisingEvent></returns>
        List<FundraisingEventVM> SelectAllFundraisingEventsByShelterId(int shelterId);

        /// <summary>
        /// Stephen Jaurigue
        /// Created: 2023/04/06
        /// 
        /// A method to get the fundraising events for the campaign
        /// </summary>
        /// <param name="campaignId">the campaign to get events for</param>
        /// <returns>a list of events for the campaign</returns>
        List<FundraisingEventVM> SelectAllFundraisingEventsByCampaignId(int campaignId);


        /// <summary>
        /// Barry Mikulas
        /// Created: 2023/03/30
        /// 
        /// A method to update a fundraising event object, with event update information
        /// </summary>
        /// <param name="oldFundraisingEventVM">the original funraising event object</param>
        /// <param name="newFundraisingEventVM">the updated fundraising event objecxt</param>
        /// <returns>the number of events updated</returns>
        int UpdateFundraisingEventResults(FundraisingEventVM oldFundraisingEventVM, FundraisingEventVM newFundraisingEventVM);

        /// <summary>
        /// Barry Mikulas 
        /// 2023/03/30
        /// 
        /// Loads a fundraising event by its event id
        /// </summary>
        /// <param name="fundraisingEventId">The id of the fundraising event to load</param>
        /// <returns>A Fundraising Event VM</returns>
        FundraisingEventVM SelectFundraisingEventByFundraisingEventId(int fundraisingEventId);
        /// <summary>
        /// Author: Hoang Chu
        /// 04/27/2023
        /// 
        /// </summary>
        /// <param name="fundraisingEvent"></param>
        /// <returns></returns>
        int InsertFundraisingEvent(FundraisingEvent fundraisingEvent);
        /// <summary>
        /// Author: Hoang Chu
        /// 04/27/2023
        /// 
        /// </summary>
        /// <param name="fundraisingEventId"></param>
        /// <param name="animalId"></param>
        /// <returns></returns>
        int InsertFundraiserAnimal(int fundraisingEventId, int animalId);
        /// <summary>
        /// Author: Hoang Chu
        /// 04/27/2023
        /// 
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="contactId"></param>
        /// <returns></returns>
        int InsertFundraisingEventEntity(int eventId, int contactId);
        /// <summary>
        /// Author: Hoang Chu
        /// 04/27/2023
        /// 
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        FundraisingEvent SelectFundraisingEvent(int eventId);
        /// <summary>
        /// Author: Hoang Chu
        /// 04/27/2023
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        List<int> SelectContactByFundraisingEvent(int eventId);
        /// <summary>
        /// Author: Hoang Chu
        /// 04/27/2023
        /// 
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        List<int> SelectSponsorByFundraisingEvent(int eventId);
        /// <summary>
        /// Author: Hoang Chu
        /// 04/27/2023
        /// 
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        List<int> SelectAnimalByFundraisingEvent(int eventId);
        /// <summary>
        /// Author: Hoang Chu
        /// 04/27/2023
        /// 
        /// </summary>
        /// <param name="fundraisingEvent"></param>
        /// <returns></returns>
        int UpdateFundraisingEvent(FundraisingEventVM fundraisingEvent);
        /// <summary>
        /// Author: Hoang Chu
        /// 04/27/2023
        /// 
        /// </summary>
        /// <param name="fundraisingEventId"></param>
        /// <returns></returns>
        int DeactivateFundraisingEvent(int fundraisingEventId);

        /// <summary>
        /// Barry Mikulas
        /// Created: 2023/04/15
        /// 
        /// A method to get the Active Fundraising Events for a shelter id
        /// 
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// 2023/04/26
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="shelterId">ShelterId to select all the Fundraising Events for</param>
        /// <exception cref="SQLException">Load Fails</exception>
        /// <returns>List of Fundraising Events</returns>
        List<FundraisingEventVM> SelectAllActiveFundraisingEventsByShelterId(int shelterId);

        /// <summary>
        /// Barry Mikulas
        /// Created: 2023/04/15
        /// 
        /// A method to get all Active Fundraising Events
        /// 
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// 2023/04/26
        /// 
        /// Final QA
        /// </remarks>
        /// <returns>List of Fundraising Events</returns>
        List<FundraisingEventVM> SelectAllActiveFundraisingEvents();
    }
}
