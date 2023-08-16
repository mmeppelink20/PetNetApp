// Created By Asa Armstrong
// Created On 2023/02/02

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayerInterfaces;
using DataObjects;

namespace DataAccessLayerFakes
{
    public class DeathAccessorFake : IDeathAccessor
    {
        private List<Death> _deaths;

        public DeathAccessorFake()
        {
            _deaths = new List<Death>()
            {
                new DeathVM()
                {
                    DeathId = 999_999,
                    UsersId = 999_999,
                    AnimalId = 999_999,
                    DeathDate = DateTime.Now,
                    DeathCause = "death cause",
                    DeathDisposal = "death disposal",
                    DeathDisposalDate = DateTime.Now,
                    DeathNotes = "death notes",
                    AnimalName = "Animal Name"
                }
            };
        }

        public int InsertAnimalDeath(Death death)
        {
            int result = _deaths.Count;

            try
            {
                _deaths.Add(death);
                result = _deaths.Count - result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public DeathVM SelectAnimalDeath(Animal animal)
        {
            Death death = new Death();

            try
            {
                death = _deaths.FirstOrDefault(d => d.AnimalId == animal.AnimalId);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return (DeathVM)death;
        }

        public int UpdateAnimalDeath(Death newDeath, Death oldDeath)
        {
            int result = 0;

            var death = _deaths.FirstOrDefault(d => d.DeathId == oldDeath.DeathId);
            if (!death.Equals(null))
            {
                death = newDeath;
            }

            if (_deaths.FirstOrDefault(d => d.DeathId == oldDeath.DeathId).Equals(newDeath))
            {
                result = 1;
            }

            return result;
        }
    }
}
