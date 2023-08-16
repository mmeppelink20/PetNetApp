using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayerInterfaces;
using DataObjects;

namespace DataAccessLayerFakes
{
    public class FosterApplicationAccessorFakes : IFosterApplicationAccessor
    {
        private List<FosterApplicationVM> fakeFosterApplicationVM = new List<FosterApplicationVM>();

        public FosterApplicationAccessorFakes()
        {
            fakeFosterApplicationVM.Add(new FosterApplicationVM()
            {
                FosterApplicationId = 1,
                ApplicantId = 1,
                ApplicationStatusId = "Pending",
                FosterApplicationDate = DateTime.Now,
                FosterApplicationStartDate = new DateTime(2023, 05, 01),
                FosterApplicationMaxAnimals = 3
            });
        }

        public List<FosterApplicationVM> SelectAllFosterApplicationsByUsersId(int usersId)
        {
            return fakeFosterApplicationVM;
        }
    }
}
