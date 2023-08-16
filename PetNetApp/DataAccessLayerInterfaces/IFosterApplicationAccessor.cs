using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayerInterfaces
{
    public interface IFosterApplicationAccessor
    {

        /// <summary>
        /// Molly Meister
        /// Created: 2023/04/19
        /// 
        /// Retrieves all foster applications for a user.
        /// Returns rows affected.
        /// </summary>
        /// <param name="usersId">the ID of the user to get applications for.</param>
        /// <exception cref="SQLException">retrieval fails</exception>
        /// <returns>List of AdoptionApplicationVM</returns>
        List<FosterApplicationVM> SelectAllFosterApplicationsByUsersId(int usersId);
    }
}
