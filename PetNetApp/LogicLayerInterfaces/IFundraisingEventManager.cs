using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayerInterfaces
{
    public interface IFundraisingEventManager
    {
        /// <summary>
        /// Barry Mikulas
        /// Created: 2023/03/05
        /// 
        /// A method to get the fundraising events for this shelter
        /// </summary>
        /// 
        /// <param name="shelterId">The shelter Id of the logged in user to retrieve all the events for</param>
        /// <exception cref="SQLException">Load Fails</exception>
        /// <returns>List<FundraisingEvent></FundraisingEvent></returns>
        List<FundraisingEventVM> RetrieveAllFundraisingEventsByShelterId(int shelterId);

        /// <summary>
        /// Stephen Jaurigue
        /// Created: 2023/04/06
        /// 
        /// A method to get the fundraising events for the campaign
        /// </summary>
        /// <param name="campaignId">the campaign to get events for</param>
        /// <returns>List of Fundraising Events for the campaign</returns>
        List<FundraisingEventVM> RetrieveAllFundraisingEventsByCampaignId(int campaignId);

        /// <summary>
        /// Barry Mikulas
        /// Created: 2023/03/05
        /// 
        /// A method to get the fundraising events for this shelter
        /// </summary>
        /// 
        /// <param name="oldFundraisingEventVM">the original event object</param>
        /// <param name="newFundraisingEventVM">the updated event object</param>
        /// <exception cref="ApplicationException">Edit fails</exception>
        /// <returns>bool representing success of the event update</returns>
        bool EditFundraisingEventResults(FundraisingEventVM oldFundraisingEventVM, FundraisingEventVM newFundraisingEventVM);

        /// <summary>
        /// Barry Mikulas
        /// Created: 2023/03/30
        /// 
        /// A method to to get a fundraising event by its event id
        /// </summary>
        /// <param name="fundraisingEventId">the id of the event to retrieve.</param>
        /// <returns>FundraisingEventVM</returns>
        FundraisingEventVM RetrieveFundraisingEventByFundraisingEventId(int fundraisingEventId);
        /// <summary>
        /// Author: Hoang Chu
        /// 04/27/2023
        /// 
        /// </summary>
        /// <param name="fundraisingEvent"></param>
        /// <returns></returns>
        int AddFundraisingEvent(FundraisingEvent fundraisingEvent);
        /// <summary>
        /// Author: Hoang Chu
        /// 04/27/2023
        /// 
        /// </summary>
        /// <param name="fundRaisingEventId"></param>
        /// <param name="animalId"></param>
        /// <returns></returns>
        bool AddFundraiserAnimal(int fundRaisingEventId, int animalId);
        /// <summary>
        /// Author: Hoang Chu
        /// 04/27/2023
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="contactId"></param>
        /// <returns></returns>
        bool AddFundraisingEventEntity(int eventId, int contactId);
        FundraisingEvent FindFundraisingEvent(int eventId);
        /// <summary>
        /// Author: Hoang Chu
        /// 04/27/2023
        /// 
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        List<InstitutionalEntity> RetrieveSponsorByEventId(int eventId);
        /// <summary>
        /// Author: Hoang Chu
        /// 04/27/2023
        /// 
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        List<InstitutionalEntity> RetrieveContactByEventId(int eventId);
        /// <summary>
        /// Author: Hoang Chu
        /// 04/27/2023
        /// 
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="shelterId"></param>
        /// <returns></returns>
        List<Animal> RetrieveAnimalByEventId(int eventId, int shelterId);
        /// <summary>
        /// Author: Hoang Chu
        /// 04/27/2023
        /// 
        /// </summary>
        /// <param name="fundraisingEvent"></param>
        /// <returns></returns>
        bool UpdateFundraisingEvent(FundraisingEventVM fundraisingEvent);
        /// <summary>
        /// Author: Hoang Chu
        /// 04/27/2023
        /// 
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        bool DeactivateFundraisingEvent(int eventId);
        /// <summary>
        /// Barry Mikulas
        /// Created: 2023/04/15
        /// A method to to get a list of all Active Fundraising Events for a shelter
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// 2023/04/26
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="shelterId">id of the shelter to view events for</param>
        /// <returns>List of all active fundraising events by shelterId</returns>
        List<FundraisingEventVM> RetrieveAllActiveFundraisingEventsByShelterId(int shelterId);

        /// <summary>
        /// Barry Mikulas
        /// Created: 2023/04/15
        /// A method to to get a list of all Active Fundraising Events
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// 2023/04/26
        /// 
        /// Final QA
        /// </remarks>
        /// <returns>List of all active fundraising events</returns>
        List<FundraisingEventVM> RetrieveAllActiveFundraisingEvents();

    }
}
