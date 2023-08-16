// Created by Asa Armstrong
// Created on 2023/02/02

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessLayerInterfaces;
using DataAccessLayer;
using LogicLayerInterfaces;

namespace LogicLayer
{
    public class DeathManager : IDeathManager
    {
        private IDeathAccessor _deathAccessor = null;

        public DeathManager()
        {
            _deathAccessor = new DeathAccessor();
        }

        public DeathManager(IDeathAccessor deathAccessor)
        {
            _deathAccessor = deathAccessor;
        }

        public bool AddAnimalDeath(Death death)
        {
            bool wasAdded = false;

            try
            {
                wasAdded = (0 < _deathAccessor.InsertAnimalDeath(death));
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return wasAdded;
        }

        public bool EditAnimalDeath(Death newDeath, Death oldDeath)
        {
            bool wasEdited = false;

            try
            {
                wasEdited = (0 < _deathAccessor.UpdateAnimalDeath(newDeath, oldDeath));
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return wasEdited;
        }

        public DeathVM RetrieveAnimalDeath(Animal animal)
        {
            DeathVM deathVM = new DeathVM();

            try
            {
                deathVM = _deathAccessor.SelectAnimalDeath(animal);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return deathVM;
        }
    }
}
