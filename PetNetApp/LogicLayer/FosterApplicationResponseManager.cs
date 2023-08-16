// Created by Asa Armstrong
// Created on 2023/03/23

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayerInterfaces;
using DataObjects;
using DataAccessLayerInterfaces;
using DataAccessLayer;

namespace LogicLayer
{
    public class FosterApplicationResponseManager : IFosterApplicationResponseManager
    {
        private IFosterApplicationResponseAccessor _fosterApplicationResponseAccessor = null;

        public FosterApplicationResponseManager()
        {
            _fosterApplicationResponseAccessor = new FosterApplicationResponseAccessor();
        }

        public FosterApplicationResponseManager(IFosterApplicationResponseAccessor fosterApplicationResponseAccessor)
        {
            _fosterApplicationResponseAccessor = fosterApplicationResponseAccessor;
        }

        public bool AddFosterApplicationResponse(FosterApplicationResponse fosterApplicationResponse)
        {
            bool wasAdded = false;

            try
            {
                wasAdded = (0 < _fosterApplicationResponseAccessor.InsertFosterApplicationResponse(fosterApplicationResponse));
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return wasAdded;
        }

        public bool EditFosterApplicationResponse(FosterApplicationResponse newFosterApplicationResponse, FosterApplicationResponse oldFosterApplicationResponse)
        {
            bool wasEdited = false;

            try
            {
                wasEdited = (0 < _fosterApplicationResponseAccessor.UpdateFosterApplicationResponse(newFosterApplicationResponse, oldFosterApplicationResponse));
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return wasEdited;
        }

        public FosterApplicationResponseVM RetrieveFosterApplicationResponse(int fosterApplicationId)
        {
            FosterApplicationResponseVM responseVM = new FosterApplicationResponseVM();

            try
            {
                responseVM = _fosterApplicationResponseAccessor.SelectFosterApplicationResponseByFosterApplicationId(fosterApplicationId);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return responseVM;
        }
    }
}
