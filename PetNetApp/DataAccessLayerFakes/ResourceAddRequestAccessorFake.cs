/// <summary>
/// Andrew Schneider
/// Created: 2023/03/30
/// 
/// Access layer fake for ResourceAddRequest
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
    public class ResourceAddRequestAccessorFake : IResourceAddRequestAccessor
    {
        private List<ResourceAddRequest> _fakeResourceAddRequsts = new List<ResourceAddRequest>();

        public ResourceAddRequestAccessorFake()
        {
            _fakeResourceAddRequsts.Add(new ResourceAddRequest
            {
                ResourceAddRequestId = 100000,
                ShelterId = 100000,
                UsersId = 100000,
                Title = "Dog toys",
                Note = "Special toys to keep dogs occupied",
                Active = true
            });
            _fakeResourceAddRequsts.Add(new ResourceAddRequest
            {
                ResourceAddRequestId = 100001,
                ShelterId = 100000,
                UsersId = 100000,
                Title = "Snake toys",
                Note = "Special toys to keep snakes occupied",
                Active = false
            });
            _fakeResourceAddRequsts.Add(new ResourceAddRequest
            {
                ResourceAddRequestId = 100002,
                ShelterId = 100000,
                UsersId = 100000,
                Title = "Turtle toys",
                Note = "Special toys to keep turtles occupied",
                Active = true
            });
        }

        public int InsertResourceAddRequest(ResourceAddRequest resourceAddRequest)
        {
            _fakeResourceAddRequsts.Add(resourceAddRequest);
            return 1;
        }

        public List<ResourceAddRequest> SelectActiveResourceAddRequestsByShelterId(int shelterId)
        {
            return _fakeResourceAddRequsts.Where(r => r.Active == true && r.ShelterId == shelterId).ToList();
        }

        public int UpdateResourceAddRequestActiveField(ResourceAddRequest oldResourceAddRequest, ResourceAddRequest newResourceAddRequest)
        {
            int result = 0;

            for (int i = 0; i < _fakeResourceAddRequsts.Count; i++)
            {
                if (_fakeResourceAddRequsts[i].ResourceAddRequestId == oldResourceAddRequest.ResourceAddRequestId)
                {
                    // the real database will check for every editable field in the stored procedure
                    _fakeResourceAddRequsts[i].Active = _fakeResourceAddRequsts[i].Active == oldResourceAddRequest.Active
                        ? _fakeResourceAddRequsts[i].Active = newResourceAddRequest.Active : oldResourceAddRequest.Active;

                    result++;
                    break;
                }
            }

            return result;
        }
    }
}
