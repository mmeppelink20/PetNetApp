using DataAccessLayer;
using DataAccessLayerInterfaces;
using LogicLayerInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class FosterManager : IFosterManager
    {

        IFosterAccessor _fosterAccessor = null;

        public FosterManager()
        {
            _fosterAccessor = new FosterAccessor();

        }

        public FosterManager(IFosterAccessor fosterAccessor)
        {
            _fosterAccessor = fosterAccessor;
        }


        public int EditCurrentlyAcceptingAnimalsByUsersId(int usersId, bool onOff)
        {
            int result = 0;

            try
            {
                result = _fosterAccessor.UpdateCurrentlyAcceptingAnimalsByUsersId(usersId, onOff);
            }
            catch (Exception up)
            {

                throw up;
            }

            return result;
        }

        public bool RetrieveCurrentlyAcceptingAnimalsByUsersId(int usersId)
        {
            bool isAccepting = false;

            try
            {
                isAccepting = _fosterAccessor.SelectCurrentlyAcceptingAnimalsByUsersId(usersId);
            }
            catch (Exception up)
            {
                throw new ArgumentNullException("Null value given.", up);
            }

            return isAccepting;

        }

        public int RetrieveNumberOfAnimalsApprovedByUsersId(int usersId)
        {
            int animalsApproved = 0;

            try
            {
                animalsApproved = _fosterAccessor.SelectNumberOfAnimalsApprovedByUsersId(usersId);
            }
            catch (Exception up)
            {
                throw new ApplicationException("Unable to retrieve approved animal number.", up);
            }

            return animalsApproved;
        }

        public int RetrieveNumberOfAnimalsFostererHasByUsersId(int usersId)
        {
            int animalsFostering = 0;

            try
            {
                animalsFostering = _fosterAccessor.SelectNumberOfAnimalsFostererHasByUsersId(usersId);
            }
            catch (Exception up)
            {
                throw new ApplicationException("Unable to retrieve count of animals fostering.", up);
            }

            return animalsFostering;
        }
    }
}
