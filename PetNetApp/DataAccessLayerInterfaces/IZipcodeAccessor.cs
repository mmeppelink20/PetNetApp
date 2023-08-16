using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayerInterfaces
{
    public interface IZipcodeAccessor
    {
        /// <summary>
        /// Barry Mikulas
        /// Created: 2023/03/09
        /// 
        /// A method to get CityStateLatLong for a zipcode
        /// </summary>
        /// 
        /// <param name="zipcode">The zipcode to get data for</param>
        /// <exception cref="SQLException">Load Fails</exception>
        /// <returns></returns>
        Zipcode SelectCityStateLatLongByZipcode(string zipcode);
    }

}