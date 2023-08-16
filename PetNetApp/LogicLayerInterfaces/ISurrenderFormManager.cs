/// <summary>
/// Alexis Oetken
/// Created: 2023/04/20
/// 
/// 
/// </summary>
///
/// <remarks>
/// Oleksiy Fedchuk
/// Updated: 2023/04/23
/// 
/// Final QA
/// </remarks>

using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayerInterfaces
{
    public interface ISurrenderFormManager
    {
        /// <summary>
        /// Alexis Oetken
        /// Created: 2023/04/20
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/23
        /// 
        /// Final QA
        /// </remarks>
        /// <returns></returns>
        List<SurrenderForm> RetrieveAllSurrenderForms();
        /// <summary>
        /// Alexis Oetken
        /// Created: 2023/04/20
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/23
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="AnimalType"></param>
        /// <param name="ReasonForSurrender"></param>
        /// <param name="SpayOrNeuterStatus"></param>
        /// <param name="ContactPhone"></param>
        /// <param name="ContactEmail"></param>
        /// <returns></returns>
        bool InsertSurrenderForm(string AnimalType, string ReasonForSurrender, bool SpayOrNeuterStatus, string ContactPhone, string ContactEmail);
        /// <summary>
        /// Alexis Oetken
        /// Created: 2023/04/20
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/23
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="SurrenderFormID"></param>
        /// <returns></returns>
        bool RemoveSurrenderForm(int SurrenderFormID); 


    }
}
