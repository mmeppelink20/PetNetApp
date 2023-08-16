using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayerInterfaces
{
    public interface IFosterApplicationManager
    {
        /// <summary>
        /// Molly Meister
        /// Created: 2023/04/22
        /// 
        /// Calls the Accessor method to retrieve all foster applications for a user.
        /// </summary>
        /// /// <param name="usersId">the usersId of the user retrieving foster applications for</param>
        /// <exception cref="ApplicationException">If the retrieval fails</exception>
        /// <returns>List of FosterApplicationVM</returns>
        List<FosterApplicationVM> RetrieveAllFosterApplicationsByUsersId(int usersId);
    }
}
