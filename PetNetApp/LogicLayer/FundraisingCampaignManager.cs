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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using LogicLayerInterfaces;
using DataAccessLayerInterfaces;
using DataAccessLayer;

namespace LogicLayer
{
    /// <summary>
    /// Stephen Jaurigue
    /// Created: 2023/02/23
    /// 
    /// The Logic Layer class for managing fundraising campaigns
    /// </summary>
    public class FundraisingCampaignManager : IFundraisingCampaignManager
    {
        private IFundraisingCampaignAccessor _fundraisingCampaignAccessor = null;

        public FundraisingCampaignManager()
        {
            _fundraisingCampaignAccessor = new FundraisingCampaignAccessor();
        }

        public FundraisingCampaignManager(IFundraisingCampaignAccessor fundraisingCampaignAccessor)
        {
            _fundraisingCampaignAccessor = fundraisingCampaignAccessor;
        }

        public bool AddFundraisingCampaign(FundraisingCampaignVM fundraisingCampaign)
        {
            bool success = false;
            try
            {
                int campaignId = _fundraisingCampaignAccessor.InsertFundraisingCampaign(fundraisingCampaign);
                if (campaignId != 0)
                {
                    success = true;
                    fundraisingCampaign.FundraisingCampaignId = campaignId;
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to add the new FundraisingCampaign", ex);
            }
            return success;
        }

        public bool EditFundraisingCampaignDetails(FundraisingCampaignVM oldFundraisingCampaignVM, FundraisingCampaignVM newFundraisingCampaignVM)
        {
            bool success = false;
            try
            {
                success = _fundraisingCampaignAccessor.UpdateFundraisingCampaignDetails(oldFundraisingCampaignVM, newFundraisingCampaignVM) != 0;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to add the new FundraisingCampaign", ex);
            }
            return success;
        }

        public bool RemoveFundraisingCampaign(FundraisingCampaignVM fundraisingCampaign)
        {
            bool success = false;
            try
            {
                if (_fundraisingCampaignAccessor.DeleteFundraisingCampaign(fundraisingCampaign) == 1)
                {
                    success = true;
                    fundraisingCampaign.Active = false;
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to add the new FundraisingCampaign", ex);
            }
            return success;
        }

        public List<FundraisingCampaignVM> RetrieveAllActiveFundraisingCampaigns()
        {
            List<FundraisingCampaignVM> campaigns = null;
            try
            {
                campaigns = _fundraisingCampaignAccessor.SelectAllActiveFundraisingCampaigns();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to load Campaigns", ex);
            }
            return campaigns;
        }

        public List<FundraisingCampaignVM> RetrieveAllActiveFundraisingCampaignsByShelterId(int shelterId)
        {
            List<FundraisingCampaignVM> campaigns = null;
            try
            {
                campaigns = _fundraisingCampaignAccessor.SelectAllActiveFundraisingCampaignsByShelterId(shelterId);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to load Campaigns", ex);
            }
            return campaigns;
        }

        public List<FundraisingCampaignVM> RetrieveAllFundraisingCampaignsByShelterId(int shelterId)
        {
            List<FundraisingCampaignVM> campaigns = null;
            try
            {
                campaigns = _fundraisingCampaignAccessor.SelectAllFundraisingCampaignsByShelterId(shelterId);
            }
            catch(Exception ex)
            {
                throw new ApplicationException("Failed to load Campaigns", ex);
            }
            return campaigns;
        }

        public FundraisingCampaignVM RetrieveFundraisingCampaignByFundraisingCampaignId(int fundraisingCampaignId)
        {
            FundraisingCampaignVM fundraisingCampaign = null;
            try
            {
                fundraisingCampaign = _fundraisingCampaignAccessor.SelectFundraisingCampaignByFundraisingCampaignId(fundraisingCampaignId);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to load the fundraising campaign", ex);
            }
            return fundraisingCampaign;
        }

        public bool AddCampaignUpdate(CampaignUpdate campaignUpdate)
        {
            bool success = false;
            try
            {
                int campaignUpdateId = _fundraisingCampaignAccessor.InsertCampaignUpdate(campaignUpdate);
                if (campaignUpdateId != 0)
                {
                    success = true;
                    campaignUpdate.CampaignUpdateId = campaignUpdateId;
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to add the new Campagin Update", ex);
            }
            return success;
        }

        public bool EditFundraisingCampaignResults(FundraisingCampaignVM oldFundraisingCampaignVM, FundraisingCampaignVM newFundraisingCampaignVM)
        {
            bool success = false;
            try
            {
                success = _fundraisingCampaignAccessor.UpdateFundraisingCampaignResults(oldFundraisingCampaignVM, newFundraisingCampaignVM) != 0;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to update the campaign results", ex);
            }
            return success;
        }
    }
}
