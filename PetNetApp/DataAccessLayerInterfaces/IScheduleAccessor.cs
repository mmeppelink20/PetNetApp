using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayerInterfaces
{
    public interface IScheduleAccessor
    {
        /// <summary>
        /// Chris Dreismeier
        /// Created: 2023/02/09
        /// 
        /// Gets all people who are scheduled on the date passed through
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="selectedDate">date the user selected to see who is scheduled that day</param>
        /// <exception cref="SQLException">Select fails</exception>
        /// <returns>List<ScheduleVM></returns>
        List<ScheduleVM> SelectScheduleByDate(DateTime selectedDate);
        /// <summary>
        /// Chris Dreismeier
        /// Created: 2023/02/17
        /// 
        /// Gets all of the Schedules that for the user passed through
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="userId">The user that you want to see specific schedule</param>
        /// <exception cref="SQLException">Select fails</exception>
        /// <returns>List<ScheduleVM></returns>
        List<ScheduleVM> SelectScheduleByUser(int userId);
        /// <summary>
        /// Chris Dreismeier
        /// Created: 2023/03/03
        /// 
        /// inserts a new schedule record into the database
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// example: 
        /// </remarks>
        /// <param name="scheduleVM">The user that you want to see specific schedule</param>
        /// <exception cref="SQLException">insert fails</exception>
        /// <returns>int</returns>
        int InsertSchedulebyUserid(ScheduleVM scheduleVM);

        /// <summary>
        /// Chris Dreismeier
        /// Created: 2023/03/10
        /// 
        /// Updates an existing schedule record into the database
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="scheduleVM">The schedule with the updated data</param>
        /// /// <param name="oldscheduleVM">The schedule with the old schedule data</param>
        /// <exception cref="SQLException">Data failed to be inserted</exception>
        /// <returns>true or false if record updated</returns>	
        int UpdateScheduleVM(ScheduleVM oldSchedule, ScheduleVM newSchedule);

        /// <summary>
        /// Chris Dreismeier
        /// Created: 2023/03/10
        /// 
        /// Updates an existing schedule record into the database
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="scheduleId">The id of the schedule being deleted</param>
        /// <exception cref="SQLException">Data failed to be deleted</exception>
        /// <returns>rows affected</returns>	
        int DeleteScheduleVM(int scheduleId);
    }
}
