using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayerInterfaces;
using DataObjects;

namespace DataAccessLayerFakes
{
    public class FosterAccessorFakes : IFosterAccessor
    {
        private Users user = new Users();
        private Applicant applicant = new Applicant();
        private List<FosterPlacement> fosterPlacement = new List<FosterPlacement>();

        public FosterAccessorFakes()
        {
            user = new Users()
            {
                UsersId = 99999,
                GivenName = "Tessa",
                FamilyName = "Berg",
                Address = "123 Main St",
                Address2 = "Apt 205",
                Email = "tessa@company.com",
                GenderId = "Female",
                PronounId = "It/Its",
                Phone = "3195555555",
                Active = true,
                Suspend = false
            };

            applicant = new Applicant()
            {
                ApplicantId = 1000,
                UserId = user.UsersId,
                ApplicantGivenName = user.GivenName,
                ApplicantFamilyName = user.FamilyName,
                ApplicantAddress = user.Address,
                ApplicantAddress2 = user.Address2,
                ApplicantZipCode = user.Zipcode,
                ApplicantPhoneNumber = user.Phone,
                ApplicantEmail = user.Email,
                HomeOwnershipId = "Renting",
                HomeTypeId = "Apartment",
                NumberOfPets = 2,
                NumberOfChildren = 0,
                CurrentlyAcceptingAnimals = true
            };

            fosterPlacement.Add(new FosterPlacement()
            {
                FosterPlacementId = 100000,
                AnimalId = 100000,
                ApplicantId = 100000,
                FosterStartDate = DateTime.Now,
                FosterEndDate = DateTime.Now,
                FosterAnimalReturned = false
            });

            fosterPlacement.Add(new FosterPlacement()
            {
                FosterPlacementId = 100000,
                AnimalId = 100001,
                ApplicantId = 100000,
                FosterStartDate = DateTime.Now,
                FosterEndDate = DateTime.Now,
                FosterAnimalReturned = false
            });

            fosterPlacement.Add(new FosterPlacement()
            {
                FosterPlacementId = 100000,
                AnimalId = 100002,
                ApplicantId = 100000,
                FosterStartDate = DateTime.Now,
                FosterEndDate = DateTime.Now,
                FosterAnimalReturned = false
            });
        }

        public bool SelectCurrentlyAcceptingAnimalsByUsersId(int usersId)
        {
            bool result = false;


            return result;
        }

        public int SelectNumberOfAnimalsApprovedByUsersId(int usersId)
        {
            throw new NotImplementedException();
        }

        public int SelectNumberOfAnimalsFostererHasByUsersId(int usersId)
        {
            throw new NotImplementedException();
        }

        public int UpdateCurrentlyAcceptingAnimalsByUsersId(int usersId, bool onOff)
        {
            throw new NotImplementedException();
        }
    }
}
