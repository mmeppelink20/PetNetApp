using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayerInterfaces;
using DataObjects;

namespace DataAccessLayerFakes
{
    public class AdoptionApplicationResponseAccessorFakes : IAdoptionApplicationResponseAccessor
    {
        private List<AdoptionApplicationResponseVM> fakeResponses = new List<AdoptionApplicationResponseVM>();
        public AdoptionApplicationResponseAccessorFakes()
        {
            fakeResponses.Add(new AdoptionApplicationResponseVM
            {
                AdoptionApplicationResponseId = 1,
                AdoptionApplicationId = 1,
                ResponderUserId = 100000,
                Approved = true,
                AdoptionApplicationResponseDate = DateTime.Now,
                AdoptionApplicationResponseNotes = "dog did a heckin like",
                Application = new AdoptionApplication(),
                Responder = new Users()
            });
        }

        public int InsertAdoptionApplicationResponseByAdoptionApplicationId(AdoptionApplicationResponseVM adoptionApplicationResponseVM)
        {
            fakeResponses.Add(adoptionApplicationResponseVM);
            int rows = 0;
            for (int i = 0; i < fakeResponses.Count; i++)
            {
                if (fakeResponses[i].AdoptionApplicationResponseId == adoptionApplicationResponseVM.AdoptionApplicationResponseId)
                {
                    rows = 1;
                }
            }
            return rows;
        }

        public AdoptionApplicationResponseVM SelectAdoptionApplicationResponseByAdoptionApplicationId(int adoptionApplicationId)
        {
            try
            {
                var response = fakeResponses.FirstOrDefault(d => d.AdoptionApplicationResponseId == adoptionApplicationId);
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int UpdateAdoptionApplicationResponse(AdoptionApplicationResponse newAdoptionApplicationResponse, AdoptionApplicationResponse oldAdoptionApplicationResponse)
        {
            int result = 0;

            var response = fakeResponses.FirstOrDefault(d => d.AdoptionApplicationResponseId == oldAdoptionApplicationResponse.AdoptionApplicationResponseId);
            if (!(response == null))
            {
                response = (AdoptionApplicationResponseVM)newAdoptionApplicationResponse;
            }

            if (response.AdoptionApplicationResponseId == newAdoptionApplicationResponse.AdoptionApplicationResponseId &&
                response.AdoptionApplicationResponseDate == newAdoptionApplicationResponse.AdoptionApplicationResponseDate &&
                response.AdoptionApplicationId == newAdoptionApplicationResponse.AdoptionApplicationId &&
                response.AdoptionApplicationResponseNotes == newAdoptionApplicationResponse.AdoptionApplicationResponseNotes &&
                response.ResponderUserId == newAdoptionApplicationResponse.ResponderUserId)
            {
                result = 1;
            }

            return result;
        }
    }
}