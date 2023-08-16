using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayerInterfaces
{
    public interface IFosterManager
    {
        //<summary>
        /// Mads Rhea
        /// Created: 2023/02/05
        /// 
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/28
        /// 
        /// Final QA
        /// </remarks>
        int RetrieveNumberOfAnimalsApprovedByUsersId(int usersId);

        //<summary>
        /// Mads Rhea
        /// Created: 2023/02/05
        /// 
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/28
        /// 
        /// Final QA
        /// </remarks>
        int RetrieveNumberOfAnimalsFostererHasByUsersId(int usersId);

        //<summary>
        /// Mads Rhea
        /// Created: 2023/02/05
        /// 
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/28
        /// 
        /// Final QA
        /// </remarks>
        int EditCurrentlyAcceptingAnimalsByUsersId(int usersId, bool onOff);

        //<summary>
        /// Mads Rhea
        /// Created: 2023/02/05
        /// 
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/28
        /// 
        /// Final QA
        /// </remarks>
        bool RetrieveCurrentlyAcceptingAnimalsByUsersId(int usersId);

    }
}
