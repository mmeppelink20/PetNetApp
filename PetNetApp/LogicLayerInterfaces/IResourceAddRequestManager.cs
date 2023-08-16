/// <summary>
/// Andrew Schneider
/// Created: 2023/03/30
/// 
/// Interface for ResourceAddRequestManager
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

namespace LogicLayerInterfaces
{
    public interface IResourceAddRequestManager
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
        /// <exception cref="ApplicationException">Retrieval Fails</exception>
        /// <returns>A list of ResourceAddRequest objects</returns>
        List<ResourceAddRequest> RetrieveActiveResourceAddRequestsByShelterId(int shelterId);

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/03/30
        /// 
        /// Edits a ResourceAddRequest record from being active to being inactive
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// example:  Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="oldResourceAddRequest">ResourceAddRequest object holding old data</param>
        /// <param name="newResourceAddRequest">ResourceAddRequest object holding new edited data</param>
        /// <exception cref="ApplicationException">Edit Fails</exception>
        /// <returns>Boolean representing success or failure</returns>
        bool EditResourceAddRequestActiveField(ResourceAddRequest oldResourceAddRequest, ResourceAddRequest newResourceAddRequest);

        /// <summary>
        /// Andrew Cromwell
        /// Created: 2023/04/19
        /// 
        /// Adds a new ResourceAddRequest to the database
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/21
        /// final QA
        /// </remarks>
        /// <param name="resourceAddRequest">ResourceAddRequest to add</param>
        /// <exception cref="ApplicationException">Addition Fails</exception>
        /// <returns>Boolean representing success or failure</returns>
        bool AddResourceAddRequest(ResourceAddRequest resourceAddRequest);
    }
}
