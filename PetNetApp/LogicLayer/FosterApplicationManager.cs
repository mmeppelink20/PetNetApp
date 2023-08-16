using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using LogicLayerInterfaces;
using DataAccessLayerInterfaces;
using DataAccessLayer;

namespace LogicLayer
{
    public class FosterApplicationManager : IFosterApplicationManager
    {
        private IFosterApplicationAccessor _fosterApplicationAccessor = null;

        public FosterApplicationManager()
        {
            _fosterApplicationAccessor = new FosterApplicationAccessor();
        }

        public FosterApplicationManager(IFosterApplicationAccessor fosterApplicationAccessor)
        {
            _fosterApplicationAccessor = fosterApplicationAccessor;
        }

        public List<FosterApplicationVM> RetrieveAllFosterApplicationsByUsersId(int usersId)
        {
            List<FosterApplicationVM> applications = null;
            try
            {
                applications = _fosterApplicationAccessor.SelectAllFosterApplicationsByUsersId(usersId);
            }
            catch (Exception e)
            {
                throw new ApplicationException("Failed to retrieve foster applications", e);
            }
            return applications;
        }
    }
}
