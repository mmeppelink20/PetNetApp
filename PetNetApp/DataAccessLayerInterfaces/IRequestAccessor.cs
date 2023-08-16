using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayerInterfaces
{
    public interface IRequestAccessor
    {
        /// <summary>
        /// Andrew Cromwell
        /// Created: 2023/03/10
        /// 
        /// Selects the requests sent to a spcific shelter
        /// </summary>
        /// 
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/17
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="shelterId">The shelter Id of the shelter the request was sent to</param>
        /// <exception cref="Exception">Select Fails</exception>
        /// <returns>List of RequestVM</returns>
        List<RequestVM> SelectRequestsByShelterSentTo(int ShelterId);

        /// <summary>
        /// Andrew Cromwell
        /// Created: 2023/03/10
        /// 
        /// Adds each RequestResourceLine for a Request to the RequestResourceLines property of the Request
        /// </summary>        
        /// <param name="request">The RequestVM to set the RequestResourceLines propertie of</param>
        /// <exception cref="Exception">Select Fails</exception>
        /// <returns>The RequestVM that was passed in affter it gets updated</returns>
        RequestVM SelectRequestResourceLinesByRequestId(RequestVM request);

        /// <summary>
        /// Andrew Cromwell
        /// Created: 2023/04/06
        /// 
        /// Inserts a new request into the request table
        /// </summary>        
        /// <param name="request">The RequestVM with the data to insert</param>
        /// <exception cref="Exception">Insert Fails</exception>
        /// <returns>bool of whether the insert was successful</returns>
        bool InsertInventoryItemRequest(RequestVM request);

        /// <summary>
        /// Matthew Meppelink
        /// Created: 2023/04/13
        /// 
        /// updates an inventory requests acknowledgment
        /// </summary>        
        /// <param name="requestId">
        /// <param name="oldAcknowledge">
        /// <param name="newAcknowledge">
        /// <exception cref="Exception">Update Fails</exception>
        /// <returns>int of number of rows affected</returns>
        int UpdateRequestAcknowledge(int requestId, bool oldAcknowledge, bool newAcknowledge);
    }
}
