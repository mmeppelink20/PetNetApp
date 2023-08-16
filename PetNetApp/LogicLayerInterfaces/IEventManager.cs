using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayerInterfaces
{
    public interface IEventManager
    {

        /// <summary>
        /// Ethan Kline 
        /// Created: 2023/04/7
        /// get all visable events
        /// </summary>
        /// <exception cref="SQLException">Select Fails</exception>
        /// <returns>all visable Events </returns>

        List<Event> SelectAllEvent();
        /// <summary>
        /// Ethan Kline 
        /// Created: 2023/04/7
        /// Delete Event By EventId
        /// 
        /// </summary>
        /// <exception cref="ApplicationException">If the delete fails</exception>
        /// <param name="eventid"></param>
        /// <returns>if deleted worked</returns>
        bool DeleteEventByEventId(int eventid);
        /// <summary>
        /// Ethan Kline 
        /// Created: 2023/04/7
        /// add an event
        /// </summary> 
        /// <exception cref="ApplicationException">If the add fails</exception>
        /// <param name="ivent"></param>
        /// <returns>if add worked</returns>
        bool AddEvent(Event ivent);
        /// <summary>
        /// Ethan Kline 
        /// Created: 2023/04/19
        /// gets list of eventtypes
        /// </summary> 
        /// <exception cref="ApplicationException">If the select fails</exception>
        /// <param name="ivent"></param>
        /// <returns>if list of eventtypes was grabed</returns>
        List<EventType> SelectAllEventType();
        /// <summary>
        /// Ethan Kline 
        /// Created: 2023/04/19
        /// gets list of shelters
        /// </summary> 
        /// <exception cref="ApplicationException">If the select fails</exception>
        /// <returns>if list of shelters was grabed</returns>
        List<Shelter> SelectAllShelter();
        /// <summary>
        /// Ethan Kline 
        /// Created: 2023/04/19
        /// updates an old event to a new event
        /// </summary> 
        /// <exception cref="ApplicationException">If the edit fails</exception>
        /// <returns>if event was updated</returns>
        bool EditEvent(Event oldevent, Event newevent);
    }
}
