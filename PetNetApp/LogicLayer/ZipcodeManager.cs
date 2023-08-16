using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using LogicLayerInterfaces;
using DataAccessLayer;
using DataAccessLayerInterfaces;

namespace LogicLayer
{
    public class ZipcodeManager : IZipcodeManager
    {
        private IZipcodeAccessor _zipcodeAccessor = null;

        /// <summary>
        /// Barry Mikulas
        /// Created: 2023/03/09
        /// 
        /// </summary>
        /// <returns>ZipcodeManager</returns>
        public ZipcodeManager()
        {
            _zipcodeAccessor = new ZipcodeAccessor();
        }

        /// <summary>
        /// Barry Mikulas
        /// Created: 2023/03/09
        /// 
        /// Constructor for fake data and testing
        /// </summary>
        /// <returns>ZipcodeManager</returns>
        public ZipcodeManager(IZipcodeAccessor zipcodeAccessor)
        {
            _zipcodeAccessor = zipcodeAccessor;
        }

        public Zipcode RetrieveCityStateLatLongByZipcode(string zipcode)
        {
            Zipcode _zipcodeObj = null;
            //throw new NotImplementedException();
            try
            {
                _zipcodeObj = _zipcodeAccessor.SelectCityStateLatLongByZipcode(zipcode);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Failed to load zipcode information", ex);
            }

            return _zipcodeObj;
        }
    }
}
