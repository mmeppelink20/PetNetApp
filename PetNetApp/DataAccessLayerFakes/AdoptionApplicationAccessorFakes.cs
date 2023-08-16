using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessLayerInterfaces;

namespace DataAccessLayerFakes
{
    public class AdoptionApplicationAccessorFakes : IAdoptionApplicationAccessor
    {
        private List<AdoptionApplicationVM> fakeAdoptionApplicationVMs = new List<AdoptionApplicationVM>();
        private List<string> fakeHomeOwnershipTypes = new List<string>();
        private List<string> fakeHomeTypes = new List<string>();

        public AdoptionApplicationAccessorFakes()
        {
            fakeAdoptionApplicationVMs.Add(new AdoptionApplicationVM
            {
                AdoptionApplicationId = 1,
                ApplicantId = 1,
                AnimalId = 1,
                ApplicationStatusId = "Pending",
                AdoptionApplicationDate = DateTime.Today,
                AdoptionAnimal = new Animal(),
                AdoptionApplicant = new Applicant(),
                Status = new ApplicationStatus()
            });

            fakeAdoptionApplicationVMs.Add(new AdoptionApplicationVM
            {
                AdoptionApplicationId = 2,
                ApplicantId = 2,
                AnimalId = 2,
                ApplicationStatusId = "Pending",
                AdoptionApplicationDate = DateTime.Today,
                AdoptionAnimal = new Animal(),
                AdoptionApplicant = new Applicant(),
                Status = new ApplicationStatus()
            });

            fakeHomeOwnershipTypes.Add("Rent");
            fakeHomeOwnershipTypes.Add("Own");

            fakeHomeTypes.Add("Spaceship");
            fakeHomeTypes.Add("Cave");

        }

        public int InsertAdoptionApplication(AdoptionApplicationVM adoptionApplication)
        {
            fakeAdoptionApplicationVMs.Add(adoptionApplication);
            int rows = 0;
            for (int i = 0; i < fakeAdoptionApplicationVMs.Count; i++)
            {
                if(fakeAdoptionApplicationVMs[i].AdoptionApplicationId == adoptionApplication.AdoptionApplicationId)
                {
                    rows = 1;
                }
            }
            return rows;
        }

        public List<AdoptionApplicationVM> SelectAllAdoptionApplicationsByAnimalId(int animalId)
        {
            List<AdoptionApplicationVM> adoptionApplications = new List<AdoptionApplicationVM>();

            foreach(AdoptionApplicationVM app in fakeAdoptionApplicationVMs)
            {
                if(app.AnimalId == animalId)
                {
                    adoptionApplications.Add(app);
                }
            }
            return adoptionApplications;
        }

        public List<AdoptionApplicationVM> SelectAllAdoptionApplicationsByUsersId(int usersId)
        {
            throw new NotImplementedException();
        }

        public List<string> SelectAllHomeOwnershipTypes()
        {
            return fakeHomeOwnershipTypes;
        }

        public List<string> SelectAllHomeTypes()
        {
            return fakeHomeTypes;
        }

        public int UpdateAdoptionApplicationStatusByAnimalIdForApprovedApplication(AdoptionApplicationResponse response)
        {
            int rows = 0;

            for (int i = 0; i < fakeAdoptionApplicationVMs.Count; i++)
            {
                if (fakeAdoptionApplicationVMs[i].AdoptionApplicationId == response.AdoptionApplicationId)
                {
                    fakeAdoptionApplicationVMs[i].ApplicationStatusId = "approved";
                    rows++;
                }
            }
            return rows;
        }
    }
}
