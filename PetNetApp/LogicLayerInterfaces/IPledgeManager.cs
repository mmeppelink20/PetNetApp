using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayerInterfaces
{
    public interface IPledgeManager
    {
        /// <summary>
        /// William Rients
        /// Created: 2023/04/04
        /// 
        /// Selects a list of pledgers by eventId
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <exception cref="Exception">No pledgers to be selected</exception>
        /// <returns>List of PledgeVM objects</returns>
        List<PledgeVM> RetrieveAllPledgesByEventId(int eventId);

        /// <summary>
        /// William Rients
        /// Created: 2023/04/04
        /// 
        /// Selects a list of pledges by userId
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <exception cref="Exception">No pledges to be selected</exception>
        /// <returns>List of PledgeVM objects</returns>
        List<PledgeVM> RetrieveSpecificPledgerByUserId(int userId);

        /// <summary>
        /// William Rients
        /// Created: 2023/04/07
        /// 
        /// Creates a new pledger by fundraising
        /// eventId
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <exception cref="Exception">Failed to create new pledge.</exception>
        /// <returns>bool if rows affected is greater tha 0</returns>
        bool CreatePledge(PledgeVM pledgeVM);

        /// <summary>
        /// Zaid Rachman
        /// Created: 2023/04/13
        /// 
        /// Retrieves all pledges
        /// 
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/20
        /// Final QA
        /// </remarks>
        /// <returns></returns>
        List<PledgeVM> RetrieveAllPledges();
    }
}
