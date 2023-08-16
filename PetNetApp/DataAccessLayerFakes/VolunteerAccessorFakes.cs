/// <summary>
/// Oleksiy Fedchuk
/// Created: 2023/02/24
/// 
/// 
/// Class for the creation of Volunteer Accessor Fakes
/// </summary>
/// <remarks>
/// Zaid Rachman
/// Updated: 2023/04/24
/// 
/// Final QA
/// </remarks>
using DataAccessLayerInterfaces;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayerFakes
{
    public class VolunteerAccessorFakes : IVolunteerAccessor
    {
        List<VolunteerVM> volunteers = new List<VolunteerVM>();
        List<VolunteerVM> fakeVolunteers = new List<VolunteerVM>();

        public VolunteerAccessorFakes()
        {
            fakeVolunteers.Add(new VolunteerVM
            {
                UsersId = 100000,
                FundraisingEventId = 100000
            });
            fakeVolunteers.Add(new VolunteerVM
            {
                UsersId = 100001,
                FundraisingEventId = 100000
            });
            fakeVolunteers.Add(new VolunteerVM
            {
                UsersId = 100002,
                FundraisingEventId = 100000
            });
            fakeVolunteers.Add(new VolunteerVM
            {
                UsersId = 100002,
                FundraisingEventId = 100001
            });
            fakeVolunteers.Add(new VolunteerVM
            {
                UsersId = 100003,
                FundraisingEventId = 100001
            });
        }

        public int DeleteVolunteerFromEventbyUsersIdAndFundraisingEventId(int usersId, int fundraisingEventId)
        {
            int rowsAffected = 0;

            if(fakeVolunteers.Remove(fakeVolunteers.FirstOrDefault(v => v.UsersId == usersId && v.FundraisingEventId == fundraisingEventId)))
            {
                rowsAffected++;
            }

            return rowsAffected;
        }

        public int InsertVolunteerToEventbyVolunteerAndEventId(int userId, int fundraisingEventId)
        {
            int rowsAffected = 0;

            int existingRows = fakeVolunteers.Count;
            fakeVolunteers.Add(new VolunteerVM
            {
                UsersId = userId,
                FundraisingEventId = fundraisingEventId
            });
            rowsAffected = fakeVolunteers.Count - existingRows;

            return rowsAffected;
        }

        public List<int> SelectAllVolunteers()
        {
            List<int> volunteers = new List<int>();

            foreach (VolunteerVM volunteer in fakeVolunteers)
            {
                volunteers.Add(volunteer.UsersId);
            }

            return volunteers;
        }

        public List<VolunteerVM> SelectVolunteersbyFundraisingEventId(int fundraisingEventId)
        {
            return fakeVolunteers.Where(v => v.FundraisingEventId == fundraisingEventId).ToList();
        }
    }
}
