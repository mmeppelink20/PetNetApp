using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayerInterfaces
{
  
    public interface IEventAccessor
    {  
        
        /// <summary>
       /// Ethan Kline 
       /// Created: 2023/04/7
       /// get all events by visabilaty
       /// </summary>
       /// <exception cref="SQLException">Select Fails</exception>
       /// <returns>all visable Events </returns>
        List<Event> SelectAllEvent();

        /// <summary>
        /// Ethan Kline 
        /// Created: 2023/04/14
        /// Deletes a event
        /// </summary>
        /// <exception cref="ApplicationException">If the delete fails</exception>
        /// <param name="eventid"></param>
        /// <returns>deletes Event at id </returns>
        int DeleteEventByEventId(int eventid);
        /// <summary>
        /// Inserts an event.
        /// </summary>
        /// <param name="ivent"></param>
        /// <exception cref="ApplicationException">If the add fails</exception>
        /// <returns>Rows affected</returns>
        int AddEvents(Event ivent);
        /// <summary>
        /// Ethan Kline
        /// created 2023/04/14
        /// get a liat of eventtypes
        /// </summary>
        /// <exception cref="ApplicationException">If the select fails</exception>
        /// <returns>list of eventtypes</returns>
        List<EventType> SelectAllEventType();
        /// <summary>
        /// Ethan Kline
        /// created 2023/04/14
        /// get a list of shelters
        /// </summary>
        /// <exception cref="ApplicationException">If the select fails</exception>
        /// <returns>list of shelters</returns>
        List<Shelter> SelectAllShelter();
        /// <summary>
        /// Ethan Kline
        /// created 2023/04/20
        /// change an old event in to a new event
        /// </summary>
        /// <exception cref="ApplicationException">If the edit fails</exception>
        /// <param name="oldevent"></param>
        /// <param name="newevent"></param>
        /// <returns>Rows affected</returns>
        int UpdateEvent(Event oldevent, Event newevent);
    }
}
