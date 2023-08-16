/// <summary>
/// Oleksiy Fedchuk
/// Created: 2023/02/24
/// 
/// 
/// Logic Layer for Volunteer
/// </summary>
/// 
/// <remarks>
/// Zaid Rachman
/// Updated: 2023/04/24
/// 
/// Final QA
/// </remarks>
using DataAccessLayer;
using DataAccessLayerInterfaces;
using DataObjects;
using LogicLayerInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class VolunteerManager : IVolunteerManager
    {
        private IVolunteerAccessor _volunteerAccessor = null;

        public VolunteerManager()
        {
            _volunteerAccessor = new VolunteerAccessor();
        }

        public VolunteerManager(IVolunteerAccessor volunteerAccessor)
        {
            _volunteerAccessor = volunteerAccessor;
        }

        public bool AddVolunteerToEventbyVolunteerAndEventId(int userId, int fundraisingEventId)
        {
            bool result = false;
            try
            {
                if(_volunteerAccessor.InsertVolunteerToEventbyVolunteerAndEventId(userId, fundraisingEventId) == 1)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return result;
        }

        public bool RemoveVolunteerFromEventbyUsersIdAndFundraisingEventId(int usersId, int fundraisingEventId)
        {
            bool result = false;
            try
            {
                if (_volunteerAccessor.DeleteVolunteerFromEventbyUsersIdAndFundraisingEventId(usersId, fundraisingEventId) == 1)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return result;
        }

        public List<int> RetrieveAllVolunteers()
        {
            List<int> volunteers = null;

            try
            {
                volunteers = _volunteerAccessor.SelectAllVolunteers();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data not found.", ex);
            }

            return volunteers;
        }

        public List<VolunteerVM> RetrieveVolunteersbyFundraisingEventId(int fundraisingEventId)
        {
            List<VolunteerVM> volunteers = null;

            try
            {
                volunteers = _volunteerAccessor.SelectVolunteersbyFundraisingEventId(fundraisingEventId);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Data not found.", ex);
            }

            return volunteers;
        }
    }
}
