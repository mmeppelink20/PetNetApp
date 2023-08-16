/// <summary>
/// Chris Dreismeier
/// Created: 2023/02/09
/// 
/// Fake data for ScheduleManager Test
/// </summary>
///
/// <remarks>
/// Updater Name
/// Updated: yyyy/mm/dd
/// </remarks>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayerInterfaces;
using DataObjects;

namespace DataAccessLayerFakes
{
    
    public class ScheduleAccessorFakes : IScheduleAccessor
    {
        private List<ScheduleVM> fakeSchedules = new List<ScheduleVM>();

        public ScheduleAccessorFakes()
        {
            fakeSchedules.Add(new ScheduleVM()
            {
                ScheduleId = 100000,
                UserId = 100000,
                StartTime = new DateTime(DateTime.Now.Year, 2,10,7,0,0),
                EndTime = new DateTime(DateTime.Now.Year, 2, 10, 15, 0, 0),
                GivenName = "Test",
                FamilyName = "User"
            });
            fakeSchedules.Add(new ScheduleVM()
            {
                ScheduleId = 100001,
                UserId = 100000,
                StartTime = new DateTime(DateTime.Now.Year, 2, 11, 7, 0, 0),
                EndTime = new DateTime(DateTime.Now.Year, 2, 11, 15, 0, 0),
                GivenName = "Test",
                FamilyName = "User"
            });
            fakeSchedules.Add(new ScheduleVM()
            {
                ScheduleId = 100002,
                UserId = 100000,
                StartTime = new DateTime(DateTime.Now.Year, 2, 12, 7, 0, 0),
                EndTime = new DateTime(DateTime.Now.Year, 2, 12, 15, 0, 0),
                GivenName = "Test",
                FamilyName = "User"
            });
            fakeSchedules.Add(new ScheduleVM()
            {
                ScheduleId = 100003,
                UserId = 100001,
                StartTime = new DateTime(DateTime.Now.Year, 2, 10, 7, 0, 0),
                EndTime = new DateTime(DateTime.Now.Year, 2, 10, 15, 0, 0),
                GivenName = "Chris",
                FamilyName = "User"
            });

        }

        public int DeleteScheduleVM(int scheduleId)
        {
            throw new NotImplementedException();
        }

        public int InsertSchedulebyUserid(ScheduleVM scheduleVM)
        {
            int result = fakeSchedules.Count();

            fakeSchedules.Add(scheduleVM);

            result = fakeSchedules.Count() - result;
            
            return result;
        }

        public List<ScheduleVM> SelectScheduleByDate(DateTime selectedDate)
        {
            List<ScheduleVM> schedules = new List<ScheduleVM>();
            foreach (var schedule in fakeSchedules)
            {
                if (DateTime.Equals(schedule.StartTime.Date,selectedDate.Date))
                {
                    schedules.Add(schedule);
                }
            }
            return schedules;
        }

        public List<ScheduleVM> SelectScheduleByUser(int userId)
        {
            List<ScheduleVM> schedules = new List<ScheduleVM>();
            foreach (var schedule in fakeSchedules)
            {
                if (schedule.UserId == userId)
                {
                    schedules.Add(schedule);
                }
            }
            return schedules;
        }

        public int UpdateScheduleVM(ScheduleVM oldSchedule, ScheduleVM newSchedule)
        {
            int result = 0;

            for (int i = 0; i < fakeSchedules.Count; i++)
            {
                if (fakeSchedules[i].ScheduleId == oldSchedule.ScheduleId)
                {
                    // the real database will check for every editable field in the stored procedure
                    fakeSchedules[i].EndTime = fakeSchedules[i].EndTime == oldSchedule.EndTime ? fakeSchedules[i].EndTime = newSchedule.EndTime : oldSchedule.EndTime;

                    result++;
                    break;
                }
            }

            return result;
        }
    }
}
