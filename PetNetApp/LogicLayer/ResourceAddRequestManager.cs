/// <summary>
/// Andrew Schneider
/// Created: 2023/03/30
/// 
/// Logic layer for ResourceAddRequest
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
using LogicLayerInterfaces;

namespace LogicLayer
{
    public class ResourceAddRequestManager : IResourceAddRequestManager
    {
        private IResourceAddRequestAccessor _resourceAddRequestAccessor = null;

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/03/30
        /// 
        /// </summary>
        /// <returns>ResourceAddRequestManager</returns>
        /// /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// </remarks>
        public ResourceAddRequestManager()
        {
            _resourceAddRequestAccessor = new DataAccessLayer.ResourceAddRequestAccessor();
        }

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/03/30
        /// 
        /// Constructor for fake data and testing
        /// </summary>
        /// <param name="resourceAddRequestAccessor">The instance of the fake dataaccess object</param>
        /// <returns>ResourceAddRequestManager</returns>
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// </remarks>
        public ResourceAddRequestManager(IResourceAddRequestAccessor resourceAddRequestAccessor)
        {
            _resourceAddRequestAccessor = resourceAddRequestAccessor;
        }

        
        public bool AddResourceAddRequest(ResourceAddRequest resourceAddRequest)
        {
            try
            {
                return 1 == _resourceAddRequestAccessor.InsertResourceAddRequest(resourceAddRequest);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while adding the request.", ex);
            }
        }

        public bool EditResourceAddRequestActiveField(ResourceAddRequest oldResourceAddRequest, ResourceAddRequest newResourceAddRequest)
        {
            try
            {
                return 1 == _resourceAddRequestAccessor.UpdateResourceAddRequestActiveField(oldResourceAddRequest, newResourceAddRequest);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while updating record.", ex);
            }
        }

        public List<ResourceAddRequest> RetrieveActiveResourceAddRequestsByShelterId(int shelterId)
        {
            List<ResourceAddRequest> resourceAddRequests;

            try
            {
                resourceAddRequests = _resourceAddRequestAccessor.SelectActiveResourceAddRequestsByShelterId(shelterId);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data not found.", ex);
            }
            return resourceAddRequests;
        }
    }
}
