using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayerInterfaces
{
    public interface IZipcodeManager
    {

        /// <summary>
        /// Barry Mikulas
        /// Created: 2023/03/09
        /// 
        /// A method to get a zip code object for the zipcode
        /// </summary>
        /// 
        /// <param name="shelterId">The Shelters Id to get the Fundraising Campaigns for</param>
        /// <exception cref="SQLException">Load Fails</exception>
        /// <returns>Zipcode</returns>
        Zipcode RetrieveCityStateLatLongByZipcode(string zipcode);
        
    }
}
