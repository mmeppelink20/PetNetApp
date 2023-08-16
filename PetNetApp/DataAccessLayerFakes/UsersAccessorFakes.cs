using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayerFakes;
using DataAccessLayerInterfaces;
using DataObjects;

namespace DataAccessLayerFakes
{
    /// <summary>
    /// Mads Rhea
    /// Created: 2023/01/27
    /// 
    /// Data Access Layer Fakes made for methods in the UsersAccessor.
    /// </summary>
    ///
    /// <remarks>
    /// Updater Barry Mikulas
    /// Updated: 02/26/2023
    /// Added SuspendEmployee = false, to users
    /// Also added roles to all users in fakeUsers
    /// </remarks>
    public class UsersAccessorFakes : IUsersAccessor
    {
        private List<Users> _fakeUsers = new List<Users>();
        private List<UsersVM> fakeUsers = new List<UsersVM>();
        private List<string> fakePassword = new List<string>();
        private List<string> fakeGenders = new List<string>();
        private List<string> fakePronouns = new List<string>();
        private List<string> fakeRoles = new List<string>();
        private Users fakeUser = new Users();


        public UsersAccessorFakes()
        {
            fakeUser = new Users()
            {
                UsersId = 1000,
                GivenName = "Stephan",
                FamilyName = "technowiz",
                Email = "Stephan@company.com",
                Address = "4150 riverview road",
                Zipcode = "52411",
                Phone = "319-123-1325",
                Active = true,
                Suspend = false
            };
            fakeUsers.Add(new UsersVM()
            {
                UsersId = 1000,
                ShelterId = 1,
                GivenName = "Stephan",
                FamilyName = "technowiz",
                Email = "Stephan@company.com",
                Address = "4150 riverview road",
                Zipcode = "52411",
                Phone = "319-123-1325",
                Active = true,
                Suspend = false,
                Roles = new List<string>(),
                AdoptionRecords = new List<UsersAdoptionRecords>()
            });
            fakeUsers.Add(new UsersVM()
            {
                UsersId = 1001,
                ShelterId = 1,
                GivenName = "Chris",
                FamilyName = "Dreismeier",
                Email = "Chris@company.com",
                Address = "4150 Chestnut road",
                Zipcode = "52411",
                Phone = "319-789-1325",
                Active = true,
                Suspend = false,
                Roles = new List<string>(),
                AdoptionRecords = new List<UsersAdoptionRecords>()
            });
            fakeUsers.Add(new UsersVM()
            {
                UsersId = 1002,
                ShelterId = 1,
                GivenName = "Asa",
                FamilyName = "arm",
                Email = "Asa@company.com",
                Address = "1234 Minden road",
                Zipcode = "12345",
                Phone = "319-567-1325",
                Active = true,
                Suspend = false,
                Roles = new List<string>(),
                AdoptionRecords = new List<UsersAdoptionRecords>()
            });
            fakeUsers.Add(new UsersVM()
            {
                UsersId = 1003,
                ShelterId = 2,
                GivenName = "Andrew",
                FamilyName = "bob",
                Email = "Andrew@company.com",
                Address = "5678 mapleview road",
                Zipcode = "54321",
                Phone = "319-321-1325",
                Active = true,
                Suspend = false,
                Roles = new List<string>(),
                AdoptionRecords = new List<UsersAdoptionRecords>()
            });
            fakeUsers.Add(new UsersVM()
            {
                UsersId = 999995,
                GivenName = "Mads",
                FamilyName = "Rhea",
                Email = "mads@company.com",
                Address = "3763 lacina drive",
                Zipcode = "52240",
                Phone = "319-594-3138",
                Active = true,
                Suspend = false,
                Roles = new List<string>() { "Helpdesk", "Marketing", "Admin" },
                AdoptionRecords = new List<UsersAdoptionRecords>()
            });
            fakeUsers.Add(new UsersVM()
            {
                UsersId = 999994,
                GivenName = "Alex",
                FamilyName = "Oetken",
                Email = "alex@company.com",
                Address = "811 Kirkwood Parkway",
                Zipcode = "52404",
                Phone = "319-111-2222",
                Active = true,
                Suspend = true,
                Roles = new List<string>() { "Vet", "Maintenance", "Admin" },
                AdoptionRecords = new List<UsersAdoptionRecords>()
            });

            //one user test requires 3 volunteers - if more volunteer roles are added if will cause it to fail -  Barry
            fakeUsers[0].Roles.Add("Volunteer");
            fakeUsers[0].Roles.Add("Admin");
            fakeUsers[1].Roles.Add("Volunteer");
            fakeUsers[2].Roles.Add("Volunteer");

            fakePassword.Add("9c9064c59f1ffa2e174ee754d2979be80dd30db552ec03e7e327e9b1a4bd594e");
            fakePassword.Add("newuser");

            fakeGenders.Add("Male");
            fakeGenders.Add("Female");
            fakeGenders.Add("Other");
            fakeGenders.Add("Prefer not to disclose.");

            fakePronouns.Add("He/Him");
            fakePronouns.Add("She/Her");
            fakePronouns.Add("They/Them");
            fakePronouns.Add("Any/All");

            fakeRoles.Add("Admin");
            fakeRoles.Add("Vet");
            fakeRoles.Add("Fosterer");

            // Fakes for the adoption record functionaly (Refered to as CustomerRecords in the drive and github) - Teft
            fakeUsers[0].AdoptionRecords.Add(new UsersAdoptionRecords()
            {
                animalName = "Tom",
                animalSpecies = "Cat",
                animalBreed = "Norwegian Forest Cat",
                oldAnimalId = 100032
            });
            fakeUsers[0].AdoptionRecords.Add(new UsersAdoptionRecords()
            {
                animalName = "Tina",
                animalSpecies = "Dog",
                animalBreed = "Golden Retriever",
                oldAnimalId = 100033
            });
            fakeUsers[0].AdoptionRecords.Add(new UsersAdoptionRecords()
            {
                animalName = "Deek",
                animalSpecies = "Rat",
                animalBreed = "Rat",
                oldAnimalId = 100034
            });
            fakeUsers[1].AdoptionRecords.Add(new UsersAdoptionRecords()
            {
                animalName = "Mort",
                animalSpecies = "Chicken",
                animalBreed = "Big Chicken",
                oldAnimalId = 100035
            });
            fakeUsers[1].AdoptionRecords.Add(new UsersAdoptionRecords()
            {
                animalName = "Thomas",
                animalSpecies = "Fish",
                animalBreed = "Goldfish",
                oldAnimalId = 100036
            });
            fakeUsers[1].AdoptionRecords.Add(new UsersAdoptionRecords()
            {
                animalName = "Bart",
                animalSpecies = "Ferret",
                animalBreed = "Longest Ferret",
                oldAnimalId = 100037
            });
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

        /// <summary>
        /// [Mads Rhea - 2023/02/15]
        /// Tests to see if method can successfully find and return a user based on email and passwordHash.
        /// </summary>
        /// <returns>int</returns>
        public int AuthenticateUserWithEmailAndPasswordHash(string email, string passwordHash)
        {
            int numAuthenticated = 0;

            for (int i = 0; i < fakeUsers.Count; i++)
            {
                if (fakeUsers[i].Email == email)
                {
                    if (fakePassword[0] == passwordHash && fakeUsers[i].Active && !fakeUsers[i].Suspend)
                    {
                        numAuthenticated += 1;
                    }
                }
            }

            return numAuthenticated;
        }

        /// <summary>
        /// [Mads Rhea - 2023/02/15]
        /// Tests to see if method successfully returns all items from fakeGenders.
        /// </summary>
        /// <returns>List of strings</returns>
        public List<string> SelectAllGenders()
        {
            return fakeGenders;
        }

        /// <summary>
        /// [Mads Rhea - 2023/02/15]
        /// Tests to see if method successfully returns all items from fakePronouns.
        /// </summary>
        /// <returns>List of strings</returns>
        public List<string> SelectAllPronouns()
        {
            return fakePronouns;
        }

        /// <summary>
        /// [Mads Rhea - 2023/02/15]
        /// Tests to see if roles can be selected by UsersId
        /// </summary>
        /// 
        /// <returns> List of strings </returns>
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

        /// <summary>
        /// [Mads Rhea - 2023/02/15]
        /// Tests to see if SelectUserByEmail successfully selects a user by their email.
        /// </summary>
        /// <returns>UsersVM</returns>
        public UsersVM SelectUserByEmail(string email)
        {
            UsersVM user = new UsersVM();

            foreach (var fakeUser in fakeUsers)
            {
                if (fakeUser.Email == email)
                {
                    user = fakeUser;
                    user.Roles = SelectRolesByUserID(fakeUser.UsersId);
                    break;
                }

            }

            if (user == null)
            {
                throw new ApplicationException("User not found.");
            }

            return user;
        }

        /// <summary>
        /// [Alex Oetken - 2023/02/??]
        /// Tests to see if the method successfully deactivates a user.
        /// </summary>
        /// <returns>int</returns>
        public int DeactivateUserAccount(int UserId)
        {
            var matchUsers = fakeUsers.Where(user => user.UsersId == UserId);

            foreach (Users currentUser in matchUsers)
            {
                currentUser.Active = false;
            }

            return matchUsers.Count();

        }

        /// <summary>
        /// [Alex Oetken - 2023/02/??]
        /// Tests to see if the method successfully injects a new user into the Users table.
        /// </summary>
        /// <returns>int</returns>
        public int CreateNewUser(Users user, string PasswordHash)
        {
            _fakeUsers.Add(user);

            return _fakeUsers.Count();
        }

        /// <summary>
        /// [Mads Rhea - 2023/03/23]
        /// Tests to see if UpdateUserDetails successfully updates a varity of user details.
        /// </summary>
        /// <returns>int</returns>
        public int UpdateUserDetails(Users oldUser, Users updatedUser)
        {
            int rowsAffected = 0;

            for (int i = 0; i < fakeUsers.Count; i++)
            {
                int index = -1;
                if (fakeUsers[i].GivenName == oldUser.GivenName &&
                    fakeUsers[i].FamilyName == oldUser.FamilyName &&
                    fakeUsers[i].GenderId == oldUser.GenderId &&
                    fakeUsers[i].PronounId == oldUser.PronounId &&
                    fakeUsers[i].Address == oldUser.Address &&
                    fakeUsers[i].Address2 == oldUser.Address2 &&
                    fakeUsers[i].Phone == oldUser.Phone &&
                    fakeUsers[i].Zipcode == oldUser.Zipcode)
                {
                    index = i;

                    fakeUsers[i].GivenName = updatedUser.GivenName;
                    fakeUsers[i].FamilyName = updatedUser.FamilyName;
                    fakeUsers[i].GenderId = updatedUser.GenderId;
                    fakeUsers[i].PronounId = updatedUser.PronounId;
                    fakeUsers[i].Address = updatedUser.Address;
                    fakeUsers[i].Address2 = updatedUser.Address2;
                    fakeUsers[i].Phone = updatedUser.Phone;
                    fakeUsers[i].Zipcode = updatedUser.Zipcode;

                    rowsAffected += 1;
                }
            }

            return rowsAffected;
        }

        /// <summary>
        /// [Mads Rhea - 2023/02/15]
        /// Tests to see if the method successfully updates the password hash.
        /// </summary>
        /// <returns>int</returns>
        public int UpdatePasswordHash(string email, string oldPasswordHash, string newPasswordHash)
        {
            int rowsAffected = 0;

            for (int i = 0; i < fakeUsers.Count; i++)
            {
                int index = -1;
                if (fakeUsers[i].Email == email)
                {
                    index = i;

                    if (fakePassword[index] == oldPasswordHash)
                    {
                        fakePassword[index] = newPasswordHash;
                        rowsAffected += 1;
                    }
                }
            }

            return rowsAffected;
        }

        /// <summary>
        /// Chris Dreismeier
        /// Created: 2023/03/02
        /// 
        /// Selects all users with a certain role
        /// </summary>
        /// 
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="RoleId"></param>
        public List<UsersVM> SelectUserByRole(string roleId, int shelterId)
        {
            List<UsersVM> users = new List<UsersVM>();

            foreach (var user in fakeUsers)
            {
                foreach (var role in user.Roles)
                {
                    if (role == roleId)
                    {
                        users.Add(user);
                    }
                }
            }
            return users;
        }

        /// <summary>
        /// [Mads Rhea - 2023/02/23]
        /// Tests to see if the method successfully updates the users email.
        /// </summary>
        /// <returns>int</returns>
        public int UpdateUserEmail(string oldEmail, string newEmail, string passwordHash)
        {
            int rowsAffected = 0;

            foreach (var fakeUser in fakeUsers)
            {
                if (fakeUser.Email == oldEmail && fakePassword[1] == passwordHash)
                {
                    fakeUser.Email = newEmail;
                    rowsAffected++;
                }

            }

            return rowsAffected;
        }

        /// <summary>
        /// Zaid Rachman
        /// Created:2023/02/15
        /// 
        /// Used to check if the user exists in the database.
        /// </summary>
        /// <returns>List<Users></returns>
        public List<UsersVM> SelectUsersByUsersId(int usersId)
        {
            List<UsersVM> userfakes = new List<UsersVM>();
            foreach (UsersVM fakeUser in fakeUsers)
            {
                if (usersId == fakeUser.UsersId)
                {
                    userfakes.Add(fakeUser);
                }
            }
            return userfakes;
        }

        public Users SelectUserByUsersId(int UsersId)
        {
            return fakeUser;
            //throw new NotImplementedException();
        }

        public UsersVM SelectUserByUsersIdWithRoles(int UsersId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Teft Francisco
        /// Created: 2023/14/02
        /// 
        /// Updates a user's active status with their user ID and active status as a boolean.
        /// </summary>
        ///
        /// <remarks>
        ///
        /// </remarks>
        public int UpdateUserActive(int userId, bool active)
        {
            int result = 0;

            foreach (var fakeUser in fakeUsers)
            {
                if (fakeUser.UsersId == userId)
                {
                    if (active == false)
                    {
                        fakeUser.Active = false;
                        result = 1;
                        return result;
                    }
                    else
                    {
                        fakeUser.Active = true;
                        result = 1;
                        return result;
                    }
                }
                if (fakeUser == null)
                {
                    throw new ApplicationException("User not found.");
                }
            }
            return result;
        }

        /// <summary>
        /// Barry Mikulas
        /// Created:2023/02/26
        /// 
        /// suspend or unsuspend user - returns number of records update
        /// </summary>
        /// <param name="suspend">if true set suspend to true, otherwise false</param>
        /// <param name="usersId"></param>
        /// <returns>int</returns>
        public int UpdateUserSuspend(int usersId, bool suspend)
        {
            int result = 0;

            foreach (var fakeUser in fakeUsers)
            {
                if (fakeUser.UsersId == usersId)
                {
                    if (suspend == false)
                    {
                        fakeUser.Suspend = false;
                        result = 1;
                        return result;
                    }
                    else
                    {
                        fakeUser.Suspend = true;
                        result = 1;
                        return result;
                    }
                }
                if (fakeUser == null)
                {
                    throw new ApplicationException("User not found.");
                }
            }
            return result;

            // return 1; green test
            //throw new NotImplementedException();
        }

        public int SelectCountActiveUnsuspendedUsersByRole(string roleId)
        {
            int result = 0;

            foreach (var fakeUser in fakeUsers)
            {
                //make sure user is active and not suspended
                if (fakeUser.Active && !fakeUser.Suspend)
                {
                    if (fakeUser.Roles.Contains(roleId))
                    {
                        result++;
                        continue;
                    }
                }
                if (fakeUser == null)
                {
                    throw new ApplicationException("User not found.");
                }
            }
            return result;
            //return 2; // green test
            //throw new NotImplementedException(); // red test
        }

        /// <summary>
        /// Teft Francisco
        /// Created: 2023/02/03
        /// 
        /// Selects a user's past adoption records by their user id.
        /// </summary>
        ///
        /// <remarks>
        ///
        /// </remarks>
        public List<UsersAdoptionRecords> SelectAdoptionRecordsByUserID(int usersId)
        {
            List<UsersAdoptionRecords> usersAdoptionRecords = new List<UsersAdoptionRecords>();

            foreach (var fakeUser in fakeUsers)
            {
                if (fakeUser.UsersId == usersId)
                {
                    foreach (var fakeRecord in fakeUser.AdoptionRecords)
                    {
                        usersAdoptionRecords.Add(fakeRecord);
                    }
                }
                if (fakeUser == null)
                {
                    throw new ApplicationException("User not found.");
                }
            }
            return usersAdoptionRecords;
        }

        //mads
        public List<string> SelectAllRoles()
        {
            return fakeRoles;
        }

        //mads - come back to
        public UsersVM AuthenticateUser(string email, string passwordHash)
        {
            UsersVM user = new UsersVM();
            foreach (var fakeUser in fakeUsers)
            {
                if (fakeUser.Email.Equals(email) && fakePassword[1].Equals(passwordHash))
                {
                    user = new UsersVM()
                    {
                        UsersId = fakeUser.UsersId,
                        ShelterId = (fakeUser.ShelterId == null) ? null : fakeUser.ShelterId,
                        GivenName = fakeUser.GivenName,
                        FamilyName = fakeUser.FamilyName,
                        Email = fakeUser.Email,
                        Address = fakeUser.Address,
                        Address2 = fakeUser.Address2,
                        Zipcode = fakeUser.Zipcode,
                        Phone = fakeUser.Phone,
                        Active = fakeUser.Active,
                        Suspend = fakeUser.Suspend,
                        Roles = fakeUser.Roles,
                        AdoptionRecords = fakeUser.AdoptionRecords
                    };
                }
            }

            return user;
        }

        public int UpdateUserShelterid(int userid, int shelterid, int? oldShelterId)
        {
            int rowsAffected = 0;

            try
            {
                fakeUsers.FirstOrDefault(u => u.UsersId == userid && u.ShelterId == oldShelterId).ShelterId = shelterid;
                if (fakeUsers.FirstOrDefault(u => u.UsersId == userid).ShelterId == shelterid)
                {
                    rowsAffected = 1;
                }
                else
                {
                    rowsAffected = 0;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return rowsAffected;
        }

        public int InsertOrDeleteUserRole(int usersId, string role, bool delete = false)
        {
            int result = 0;

            foreach (var fakeUser in fakeUsers)
            {
                foreach (var roles in fakeUser.Roles)
                {
                    if (fakeUser.UsersId == usersId && roles != role)
                    {
                        fakeUser.Roles.Add(role);
                        result++;
                    }
                    else if (fakeUser.UsersId == usersId && roles == role)
                    {
                        fakeUser.Roles.Remove(roles);
                    }
                }
            }

            return result;
        }
    }
}
