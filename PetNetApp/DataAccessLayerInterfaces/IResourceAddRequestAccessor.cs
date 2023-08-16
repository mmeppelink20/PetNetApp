/// <summary>
/// Andrew Schneider
/// Created: 2023/03/30
/// 
/// Interface for ResourceAddRequestAccessor
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
using DataObjects;

namespace DataAccessLayerInterfaces
{
    public interface IResourceAddRequestAccessor
    {
        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/03/30
        /// 
        /// Retrieves all active Resource Add Requests by Shelter Id
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// example:  Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="shelterId">The shelter Id of the resource add request to be returned</param>
        /// <exception cref="ApplicationException">Select Fails</exception>
        /// <returns>A list of ResourceAddRequest objects</returns>
        List<ResourceAddRequest> SelectActiveResourceAddRequestsByShelterId(int shelterId);

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/03/30
        /// 
        /// Updates a ResourceAddRequest record from being active to being inactive
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// example:  Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="oldResourceAddRequest">ResourceAddRequest object holding old data</param>
        /// <param name="newResourceAddRequest">ResourceAddRequest object holding new edited data</param>
        /// <exception cref="ApplicationException">Update Fails</exception>
        /// <returns>Int represting rows edited</returns>
        int UpdateResourceAddRequestActiveField(ResourceAddRequest oldResourceAddRequest, ResourceAddRequest newResourceAddRequest);

        /// <summary>
        /// Andrew Cromwell
        /// Created: 2023/04/19
        /// 
        /// Inserts a new ResourceAddRequest into the database
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/21
        /// final QA
        /// </remarks>
        /// <param name="resourceAddRequest">ResourceAddRequest to insert</param>
        /// <exception cref="ApplicationException">Insert Fails</exception>
        /// <returns>Int represting rows affected</returns>
        int InsertResourceAddRequest(ResourceAddRequest resourceAddRequest);
    }
}
