using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessLayerInterfaces;

namespace DataAccessLayerFakes
{
    public class UsersAccessorFake : IUsersAccessor
    {
        private List<UsersVM> fakeUsers = new List<UsersVM>();
        private List<string> fakePassword = new List<string>();

        public UsersAccessorFake()
        {
            fakeUsers.Add(new UsersVM()
            {
                UsersId = 100000,
                GenderId = "Unknow",
                ShelterId = 100000,
                GivenName = "Test",
                FamilyName = "User",
                Email = "test@gmail.com",
                Address = "33 Sage Road",
                Zipcode = "52404",
                Phone = "123456789",
                CreationDate = DateTime.Now,
                Active = true,
                SuspendEmployee = false
            });

            fakeUsers.Add(new UsersVM()
            {
                UsersId = 100001,
                GenderId = "Unknow",
                ShelterId = 100000,
                GivenName = "Test",
                FamilyName = "User2",
                Email = "test2@gmail.com",
                Address = "33 Sage Road",
                Zipcode = "52404",
                Phone = "123456789",
                CreationDate = DateTime.Now,
                Active = true,
                SuspendEmployee = false
            });

            fakeUsers.Add(new UsersVM()
            {
                UsersId = 100002,
                GenderId = "Unknow",
                ShelterId = 100000,
                GivenName = "Test",
                FamilyName = "User3",
                Email = "test3@gmail.com",
                Address = "33 Sage Road",
                Zipcode = "52404",
                Phone = "123456789",
                CreationDate = DateTime.Now,
                Active = true,
                SuspendEmployee = false
            });

            fakePassword.Add("9c9064c59f1ffa2e174ee754d2979be80dd30db552ec03e7e327e9b1a4bd594e");

        }

        public int AuthenticateUserWithEmailAndPasswordHash(string email, string passwordHash)
        {
            int numAuthenticated = 0;

            for (int i = 0; i < fakeUsers.Count; i++)
            {
                if (fakeUsers[i].Email == email)
                {
                    if (fakePassword[i] == passwordHash && fakeUsers[i].Active && !fakeUsers[i].SuspendEmployee)
                    {
                        numAuthenticated += 1;
                    }
                }
            }

            return numAuthenticated;
        }

        /// <summary>
        /// Hoang Chu
        /// Created: 2023/02/01
        /// 
        /// 
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <returns>List<Users></returns>

        public List<UsersVM> SelectAllEmployees()
        {
            return fakeUsers;
        }

        public List<string> SelectAllGenders()
        {
            throw new NotImplementedException();
        }

        public List<string> SelectAllPronouns()
        {
            throw new NotImplementedException();
        }

        public List<string> SelectRolesByUserID(int userId)
        {
            List<string> roles = new List<string>();

            foreach (var fakeUser in fakeUsers)
            {
                if (fakeUser.UsersId == userId)
                {
                    roles = fakeUser.Roles;
                    break;
                }

            }

            return roles;
        }

        public UsersVM SelectUserByEmail(string email)
        {
            UsersVM user = new UsersVM();

            foreach (var fakeUser in fakeUsers)
            {
                if (fakeUser.Email == email)
                {
                    user = fakeUser;
                    user.Roles = new List<string>();
                    break;
                }

            }

            if (user == null)
            {
                throw new ApplicationException("User not found.");
            }

            return user;
        }

        public List<UsersVM> SelectUserByRole(string RoleId)
        {
            throw new NotImplementedException();
        }
    }
}
