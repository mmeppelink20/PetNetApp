using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayerInterfaces;
using DataObjects;

namespace DataAccessLayerFakes
{
    public class EventAccessorFakes : IEventAccessor
    {
        public List<Event> fakeEvents = new List<Event>();
        public List<EventType> fakeEventTypes = new List<EventType>();
        public List<Shelter> fakeshelter = new List<Shelter>();
        public Event oldevent = new Event();
        public Event newevent = new Event();



        public EventAccessorFakes()
        {
            fakeEvents.Add(new Event()
            {
                Eventid = 10000,
                EventTypeid = "good",
                Shelterid =1,
                EventTitle ="happy dogs",
                EventVisible=true
            });
            fakeEvents.Add(new Event()
            {
                Eventid = 10001,
                EventTypeid = "good",
                Shelterid = 1,
                EventTitle = "sad dogs",
                EventVisible = true
            });
            fakeEventTypes.Add(new EventType()
            {
                EventTypeId = "dog",
                EventTypeDescription="happy"
            });
            fakeshelter.Add(new Shelter()
            {
                ShelterId =1,
                ShelterActive=true
            });
        }
        

        public int AddEvents(Event ivent)
        {
            //throw new NotImplementedException();
            int row;
            int row2;
            row = fakeEvents.Count;
            fakeEvents.Add(ivent);
            row2 = fakeEvents.Count - row;
            return row2;

            //return fakeEvents.Count;
        }

        public int DeleteEventByEventId(int eventid)
        {
            //throw new NotImplementedException();
            int result = fakeEvents.Count;
            for (int i = fakeEvents.Count - 1; i > -1; i--)
            {
                if (fakeEvents[i].Eventid == eventid)
                {
                    fakeEvents.Remove(fakeEvents[i]);
                    result -= fakeEvents.Count;
                }
            }
            return result;
            //return fakeEvents[0].Eventid;
        }

        public List<Event> SelectAllEvent()
        {
            return fakeEvents;
            //return fakeEvents.Where(e => e.EventVisible == true).ToList();
        }

        public List<EventType> SelectAllEventType()
        {
            //throw new NotImplementedException();
            return fakeEventTypes;
        }

        public List<Shelter> SelectAllShelter()
        {
            //throw new NotImplementedException();
            return fakeshelter;
        }
        

        public int UpdateEvent(Event oldevent, Event newevent)
        {
            int rowsAffected = 0;
            int rows = fakeEvents.Count;

            //Event testevent = new Event();
            for (int i = 0; i < rows; i++)
            {
                if (fakeEvents[i].Eventid == oldevent.Eventid)
                {
                    fakeEvents[i].Eventid = newevent.Eventid;
                    fakeEvents[i].EventTitle = newevent.EventTitle;
                    rowsAffected += 1;
                }
            }
            return rowsAffected;

        }
    }
}
