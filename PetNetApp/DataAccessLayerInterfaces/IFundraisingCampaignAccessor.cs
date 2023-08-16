/// <summary>
/// Author: Stephen Jaurigue
/// Date: 2023/04/21
/// 
/// </summary>
/// <remarks>
/// Oleksiy Fedchuk
/// Updated: 2023/04/27
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
    public interface IFundraisingCampaignAccessor
    {
        /// <summary>
        /// Stephen Jaurigue
        /// Created: 2023/02/23
        /// 
        /// A method to get the fundraising campaigns for this shelter
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/27
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="shelterId">The Shelters Id to get the Fundraising Campaigns for</param>
        /// <exception cref="SQLException">Load Fails</exception>
        /// <returns>List<FundraisingCampaign></FundraisingCampaign></returns>
        List<FundraisingCampaignVM> SelectAllFundraisingCampaignsByShelterId(int shelterId);

        /// <summary>
        /// Stephen Jaurigue
        /// Created: 2023/03/24
        /// 
        /// A method to get the active fundraising campaigns for all active shelters
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/27
        /// 
        /// Final QA
        /// </remarks>
        /// <exception cref="SQLException">Load Fails</exception>
        /// <returns>List<FundraisingCampaign></FundraisingCampaign></returns>
        List<FundraisingCampaignVM> SelectAllActiveFundraisingCampaigns();

        /// <summary>
        /// Stephen Jaurigue
        /// Created: 2023/03/24
        /// 
        /// A method to get the active fundraising campaigns for this active shelter
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/27
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="shelterId">The Shelters Id to get the Fundraising Campaigns for</param>
        /// <exception cref="SQLException">Load Fails</exception>
        /// <returns>List<FundraisingCampaign></FundraisingCampaign></returns>
        List<FundraisingCampaignVM> SelectAllActiveFundraisingCampaignsByShelterId(int shelterId);

        /// <summary>
        /// Stephen Jaurigue
        /// Created: 2023/03/02
        /// 
        /// A method to create a new fundraising campaign for this shelter
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/27
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="fundraisingCampaign"></param>
        /// <returns>Total Number of Rows affected</returns>
        int InsertFundraisingCampaign(FundraisingCampaignVM fundraisingCampaign);

        int UpdateFundraisingCampaignDetails(FundraisingCampaignVM oldFundraisingCampaignVM, FundraisingCampaignVM newFundraisingCampaignVM);

        /// <summary>
        /// Stephen Jaurigue
        /// Created: 2023/03/04
        /// 
        /// Loads a Fundraising Campaign VM by its id
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/27
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="fundraisingCampaignId">The Id of the Fundraising Campaign to load</param>
        /// <returns>A Fundraising Campaign VM</returns>
        FundraisingCampaignVM SelectFundraisingCampaignByFundraisingCampaignId(int fundraisingCampaignId);

        /// <summary>
        /// Stephen Jaurigue
        /// Created: 2023/03/07
        /// 
        /// Deactivates the record for this fundraising campaign
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/27
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="fundraisingCampaign">Campaign to deactivate</param>
        /// <returns>the number of campaigns deactivated</returns>
        int DeleteFundraisingCampaign(FundraisingCampaignVM fundraisingCampaign);

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/03/23
        /// 
        /// A method to create a new fundraising campaign update
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/27
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="campaignUpdate">The campaign update record</param>
        /// <returns>ID of the update record created</returns>
        int InsertCampaignUpdate(CampaignUpdate campaignUpdate);

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/03/23
        /// 
        /// A method to update the fundraising campaign results
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/27
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="oldFundraisingCampaignVM">The original campaign record</param>
        /// <param name="newFundraisingCampaignVM">The new campaign record</param>
        int UpdateFundraisingCampaignResults(FundraisingCampaignVM oldFundraisingCampaignVM,
            FundraisingCampaignVM newFundraisingCampaignVM);
    }
}
