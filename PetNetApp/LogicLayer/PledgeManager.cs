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
    public class PledgeManager : IPledgeManager
    {
        private IPledgeAccessor _pledgeAccessor = null;

        public PledgeManager()
        {
            _pledgeAccessor = new PledgeAccessor();
        }

        public PledgeManager(IPledgeAccessor pledgeAccessor)
        {
            _pledgeAccessor = pledgeAccessor;
        }

        public bool CreatePledge(PledgeVM pledgeVM)
        {
            bool result = false;

            try
            {
                if (0 < _pledgeAccessor.InsertPledge(pledgeVM))
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to create new pledge.", ex);
            }
            return result;
        }

        public List<PledgeVM> RetrieveAllPledges()
        {
            List<PledgeVM> pledgeVMs = null;
            try
            {
                pledgeVMs = _pledgeAccessor.SelectAllPledges();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was an error retrieving pledge data.", ex);
            }
            return pledgeVMs;
        }

        public List<PledgeVM> RetrieveAllPledgesByEventId(int eventId)
        {
            List<PledgeVM> pledgeVMs = null;
            try
            {
                pledgeVMs = _pledgeAccessor.SelectAllPledgesByEventId(eventId);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was an error retrieving pledge data.", ex);
            }
            return pledgeVMs;
        }

        public List<PledgeVM> RetrieveSpecificPledgerByUserId(int userId)
        {
            List<PledgeVM> pledgeVMs = null;
            try
            {
                pledgeVMs = _pledgeAccessor.SelectPledgerByUserId(userId);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was an error retrieving pledger data.", ex);
            }
            return pledgeVMs;
        }
    }
}
