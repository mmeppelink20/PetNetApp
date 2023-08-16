/// <summary>
/// Alexis Oetken
/// Created: 2023/04/20
/// 
/// Bookmark Object Class
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


namespace DataAccessLayerInterfaces
{
    /// <summary>
    /// Alex Oetken
    /// Created: 2023/04/22
    /// IShelterAccessor interface governing access to the ShelterAccessor class in DataAccessLayer
    /// </summary>
    public interface ISurrenderFormAccessor
    {
        /// <summary>
        /// Alex Oetken
        /// Created: 2023/04/22
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
        /// Alex Oetken
        /// Created: 2023/04/22
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
        int InsertSurrenderForm(string AnimalType, string ReasonForSurrender, bool SpayOrNeuterStatus, string ContactPhone, string ContactEmail);
        /// <summary>
        /// Alex Oetken
        /// Created: 2023/04/22
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/23
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="SurrenderFormID"></param>
        /// <returns></returns>
        int RemoveSurrenderForm(int SurrenderFormID);
   
    }
}