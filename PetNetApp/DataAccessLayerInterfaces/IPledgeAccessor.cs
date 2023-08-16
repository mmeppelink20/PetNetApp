using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayerInterfaces
{
    public interface IPledgeAccessor
    {
        /// <summary>
        /// William Rients
        /// Created: 2023/04/04
        /// 
        /// Selects a list of pledgers for a specific eventId
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <exception cref="Exception">No pledgers for this event</exception>
        /// <returns>List of PledgeVM objects</returns>
        List<PledgeVM> SelectAllPledgesByEventId(int eventId);

        /// <summary>
        /// William Rients
        /// Created: 2023/04/04
        /// 
        /// Selects a list of specific pledger by userId
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <exception cref="Exception">No pledges by this user</exception>
        /// <returns>List of PledgeVM objects</returns>
        List<PledgeVM> SelectPledgerByUserId(int userId);

        /// <summary>
        /// William Rients
        /// Created: 2023/04/07
        /// 
        /// Inserts a new Pledge by fundraising
        /// eventId
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <exception cref="Exception">Could not create pledge</exception>
        /// <returns>int of rows affected</returns>
        int InsertPledge(PledgeVM pledgeVM);
        /// <summary>
        /// Zaid Rachman
        /// 2023/04/18
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/20
        /// Final QA
        /// </remarks>
        /// <returns></returns>
        List<PledgeVM> SelectAllPledges();
    }
}
