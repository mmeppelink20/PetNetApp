using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayerInterfaces;
using DataAccessLayerInterfaces;
using DataAccessLayer;
using DataObjects;

namespace LogicLayer
{
    public class EventManager:IEventManager
    {
        private IEventAccessor _eventAccessor = null;

        public EventManager()
        {
            _eventAccessor = new EventAccessor();
        }

        public EventManager(IEventAccessor eventAccessor)
        {
            _eventAccessor = eventAccessor;
        }
        public List<Event> SelectAllEvent()
        {
            List<Event> events = new List<Event>();

            try
            {
                events = _eventAccessor.SelectAllEvent();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data not found.", ex);
            }

            return events;
        }

        public bool DeleteEventByEventId(int eventid)
        {
            try
            {
                return (0 < _eventAccessor.DeleteEventByEventId(eventid));
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Could not delete event.", ex);
            }
            return true;
        }
        public bool AddEvent(Event ivent)
        {
            bool result = false;
            try
            {
                result = (1 == _eventAccessor.AddEvents(ivent));
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Add failed.", ex); 
            }
            return result;
        }
        public List<EventType> SelectAllEventType()
        {
            List<EventType> eventTypes = new List<EventType>();

            try
            {
                eventTypes = _eventAccessor.SelectAllEventType();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data not found.", ex);
            }

            return eventTypes;
        }
        public List<Shelter> SelectAllShelter()
        {
            List<Shelter> shelters = new List<Shelter>();
            try
            {
                shelters = _eventAccessor.SelectAllShelter();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data not found.", ex);
            }

            return shelters;
        }
        public bool EditEvent(Event oldevent, Event newevent)
        {
            bool result = false;
            try
            {
                result = (1 == _eventAccessor.UpdateEvent(oldevent, newevent));

            }
            catch (Exception ex)
            {
                throw new ApplicationException("Update failed.", ex);
            }
            return result;
        }
    }
}
