using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataAccessLayerInterfaces;
using LogicLayerInterfaces;
using DataObjects;

namespace LogicLayer
{
    public class RequestManager : IRequestManager
    {
        private IRequestAccessor _requestAccessor = null;

        public RequestManager()
        {
            _requestAccessor = new RequestAccessor();
        }

        public RequestManager(IRequestAccessor requestAccessor)
        {
            _requestAccessor = requestAccessor;
        }

        public bool AddInventoryItemRequest(RequestVM request)
        {
            bool success = false;
            try
            {
                success = _requestAccessor.InsertInventoryItemRequest(request);
            }catch(Exception ex)
            {
                throw new ApplicationException("Error. The request was not sent.", ex);
            }
            return success;
        }

        public bool EditRequestAcknowledge(int requestId, bool oldAcknowledge, bool newAcknowledge)
        {
            bool result = false;
            try
            {
                result = 1 == _requestAccessor.UpdateRequestAcknowledge(requestId, oldAcknowledge, newAcknowledge);
                if (!result)
                {
                    throw new ApplicationException("update of request failed");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("update of request failed", ex);
            }
            return result;
        }

        public List<RequestVM> RetrieveRequestsByShelterId(int shelterId)
        {
            List<RequestVM> requests = null;
            try
            {
                requests = _requestAccessor.SelectRequestsByShelterSentTo(shelterId);
                foreach(RequestVM request in requests)
                {
                    _requestAccessor.SelectRequestResourceLinesByRequestId(request);
                }
            }catch(Exception ex)
            {
                throw new ApplicationException("Could not retreive request records.", ex);
            }
            return requests;
        }
    }
}
