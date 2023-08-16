/// <summary>
/// Andrew Cromwell
/// Created: 2023/03/15
/// 
/// Contains the fake data and methods needed to pass the RequestManagerTests.
/// </summary> 
        

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayerInterfaces;
using DataObjects;

namespace DataAccessLayerFakes
{
    public class RequestAccessorFakes : IRequestAccessor
    {
        private List<RequestVM> _requests;
        private List<RequestResourceLine> _requestLines;

        /// <summary>
        /// Andrew Cromwell
        /// Created: 2023/03/15
        /// 
        /// Constructs a RequestAccessorFakes with fake data.
        /// </summary>
        public RequestAccessorFakes()
        {
            _requests = new List<RequestVM>()
            {
                new RequestVM()
                {
                    RequestId = 1,
                    RecievingShelterId = 55,
                    RequestedByUserId = 66,
                    RequestDate = DateTime.Now,
                    Acknowledged = false,
                    RequestingShelterName = "DogHouse",
                    GivenName = "Simon",
                    FamilyName = "Peter"
                },
                new RequestVM()
                {
                    RequestId = 2,
                    RecievingShelterId = 66,
                    RequestedByUserId = 55,
                    RequestDate = DateTime.Now,
                    Acknowledged = false,
                    RequestingShelterName = "CatHouse",
                    GivenName = "Andrew",
                    FamilyName = "James"
                },
                new RequestVM()
                {
                    RequestId = 3,
                    RecievingShelterId = 55,
                    RequestedByUserId = 66,
                    RequestDate = DateTime.Now,
                    Acknowledged = false,
                    RequestingShelterName = "DogHouse",
                    GivenName = "Simon",
                    FamilyName = "Peter"
                }
            };
            _requestLines = new List<RequestResourceLine>()
            {
                new RequestResourceLine()
                {
                    RequestId = 1,
                    ItemId = "Air freshiner",
                    QuantityRequested = 500,
                    QuantityAvailable = 600,
                    Notes = "It stinks here"
                },
                new RequestResourceLine()
                {
                    RequestId = 2,
                    ItemId = "Soap",
                    QuantityRequested = 50,
                    QuantityAvailable = 600,
                },
                new RequestResourceLine()
                {
                    RequestId = 2,
                    ItemId = "Leash",
                    QuantityRequested = 600,
                    QuantityAvailable = 600,
                    Notes = "Lots of animals to walk"
                },
                new RequestResourceLine()
                {
                    RequestId = 3,
                    ItemId = "Dog food",
                    QuantityRequested = 500,
                    QuantityAvailable = 600,
                },
                new RequestResourceLine()
                {
                    RequestId = 3,
                    ItemId = "Absorbant Pads",
                    QuantityRequested = 20,
                    Notes = "Lots of messes",
                    QuantityAvailable = 600,
                },
                new RequestResourceLine()
                {
                    RequestId = 3,
                    ItemId = "Air freshiner",
                    QuantityRequested = 5,
                    QuantityAvailable = 300,
                    Notes = "Just a little more"
                }
            };
        }

        public bool InsertInventoryItemRequest(RequestVM request)
        {
            bool success = false;
            int startCountOfRequests = _requests.Count;
            int newRequestId = 4;
            if(request.RequestLines != null)
            {
                foreach (RequestResourceLine line in request.RequestLines)
                {
                    line.RequestId = newRequestId;
                    _requestLines.Add(line);
                }
            }
            
            request.RequestId = newRequestId;
            request.RequestLines = new List<RequestResourceLine>();
            _requests.Add(request);
            int currentCountOfRequests = _requests.Count;
            if (currentCountOfRequests == startCountOfRequests + 1)
            {
                success = true;
            }
            return success;
        }

        public RequestVM SelectRequestResourceLinesByRequestId(RequestVM request)
        {
            request.RequestLines = _requestLines.Where(r => r.RequestId == request.RequestId).ToList();
            return request;
        }

        public List<RequestVM> SelectRequestsByShelterSentTo(int ShelterId)
        {
            return _requests.Where(r => r.RecievingShelterId == ShelterId).OrderByDescending(r => r.RequestDate).ToList();
        }

        public int UpdateRequestAcknowledge(int requestId, bool oldAcknowledge, bool newAcknowledge)
        {
            int result = 0;

            foreach (Request request in _requests)
            {
                if (request.RequestId == requestId)
                {
                    request.Acknowledged = newAcknowledge;
                    result = 1;
                }
            }

            return result;
        }
    }
}
