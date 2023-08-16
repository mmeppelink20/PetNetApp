// Created By Asa Armstrong
// Created On 2023/03/23
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayerInterfaces;
using DataObjects;

namespace DataAccessLayerFakes
{
    public class FosterApplicationResponseAccessorFakes : IFosterApplicationResponseAccessor
    {
        private List<FosterApplicationResponse> _responses;

        public FosterApplicationResponseAccessorFakes()
        {
            _responses = new List<FosterApplicationResponse>()
            {
                new FosterApplicationResponse()
                {
                    FosterApplicationResponseId = 999_999,
                    FosterApplicationId = 999_999,
                    UsersId = 999_999,
                    Approved = false,
                    FosterApplicationResponseDate = DateTime.Now,
                    FosterApplicationResponseNotes = "notes"
                }
            };
        }

        public int InsertFosterApplicationResponse(FosterApplicationResponse fosterApplicationResponse)
        {
            int result = _responses.Count;

            try
            {
                _responses.Add(fosterApplicationResponse);
                result = _responses.Count - result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public FosterApplicationResponseVM SelectFosterApplicationResponseByFosterApplicationId(int fosterApplicationId)
        {
            FosterApplicationResponseVM response = new FosterApplicationResponseVM();

            try
            {
                var tempResponse = _responses.FirstOrDefault(d => d.FosterApplicationId == fosterApplicationId);
                response.FosterApplicationResponseDate = tempResponse.FosterApplicationResponseDate;
                response.FosterApplicationResponseId = tempResponse.FosterApplicationResponseId;
                response.FosterApplicationResponseNotes = tempResponse.FosterApplicationResponseNotes;
                response.Approved = tempResponse.Approved;
                response.UsersId = tempResponse.UsersId;
                response.FosterApplicationId = tempResponse.FosterApplicationId;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return response;
        }

        public int UpdateFosterApplicationResponse(FosterApplicationResponse newFosterApplicationResponse, FosterApplicationResponse oldFosterApplicationResponse)
        {
            int result = 0;

            var response = _responses.FirstOrDefault(d => d.FosterApplicationResponseId == oldFosterApplicationResponse.FosterApplicationResponseId);
            if (!response.Equals(null))
            {
                response = newFosterApplicationResponse;
            }

            if (_responses.FirstOrDefault(d => d.FosterApplicationResponseId == oldFosterApplicationResponse.FosterApplicationResponseId).Equals(newFosterApplicationResponse))
            {
                result = 1;
            }

            return result;
        }
    }
}
