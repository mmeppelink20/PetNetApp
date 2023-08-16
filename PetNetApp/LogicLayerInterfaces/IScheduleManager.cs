using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayerInterfaces
{
    public interface IScheduleManager
    {
        /// <summary>
        /// Chris Dreismeier
        /// Created: 2023/02/17
        /// 
        /// Retrieves the schedule of the person passed through
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="userId">The Id of the user for whose schedule you looking for</param>
        /// <exception cref="SQLException">Data failed to be retrieved</exception>
        /// <returns>List of Schedules</returns>
        List<ScheduleVM> RetrieveScheduleByDate(DateTime selectedDate);

        /// <summary>
        /// Chris Dreismeier
        /// Created: 2023/02/09
        /// 
        /// Retrieves all people schedule on passed day from the accessor
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="selectedDate">The date that you want to all people schedules</param>
        /// <exception cref="SQLException">Data failed to be retrieved</exception>
        /// <returns>List of Schedules</returns>	
        List<ScheduleVM> RetrieveScheduleByUserId(int userId);

        /// <summary>
        /// Chris Dreismeier
        /// Created: 2023/03/03
        /// 
        /// Inserts a new schedule record into the database
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="scheduleVM">The schedule to be added</param>
        /// <exception cref="SQLException">Data failed to be inserted</exception>
        /// <returns>true or false if record insterted</returns>	
        bool AddSchedulebyUserId(ScheduleVM scheduleVM);
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
        bool EditScheduleVM(ScheduleVM oldSchedule, ScheduleVM newSchedule);

        /// <summary>
        /// Chris Dreismeier
        /// Created: 2023/03/24
        /// 
        /// Deletes an existing schedule record into the database
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// /// <param name="scheduleId">The id of the schedule to be deleted</param>
        /// <exception cref="SQLException">Data failed to be deleted</exception>
        /// <returns>true or false if record deleted</returns>	
        bool DeleteScheduleVM(int scheduleId);
    }
}
