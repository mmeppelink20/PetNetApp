using DataAccessLayer;
using DataAccessLayerInterfaces;
using DataObjects;
using LogicLayerInterfaces;
using System;
using System.Collections.Generic;

namespace LogicLayer
{
    public class DonationManager : IDonationManager
    {
        private IDonationAccessor donationAccessor = null;
        public DonationManager()
        {
            donationAccessor = new DonationAccessor();
        }
        public DonationManager(IDonationAccessor da)
        {
            donationAccessor = da;
        }

        public List<DonationVM> RetrieveAllDonations()
        {
            try
            {
                return donationAccessor.SelectAllDonations();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to retrieve donations", ex);
            }
        }

        public DonationVM RetrieveDonationByDonationId(int donationId)
        {
            try
            {
                return donationAccessor.SelectDonationByDonationId(donationId);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to retrieve donation", ex);
            }
        }

        public List<DonationVM> RetrieveDonationsByUserId(int usersId)
        {
            try
            {
                return donationAccessor.SelectDonationsByUserId(usersId);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Failed to retrieve donations for this donor.", ex);
            }
        }

        /// <summary>
        /// Author: Gwen Arman
        /// Date: 2023/03/02
        /// Description: Retrieves donations by ShelterId
        /// </summary>
        /// <param name="ShelterId"></param>
        /// <returns></returns>
        public List<DonationVM> RetrieveDonationsByShelterId(int ShelterId)
        {
            try
            {
                return donationAccessor.SelectDonationsByShelterId(ShelterId);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to retrieve donations", ex);
            }
        }

        public List<InKind> RetrieveInKindsByDonationId(int donationId)
        {
            try
            {
                return donationAccessor.SelectInKindsByDonationId(donationId);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to retrieve in-kind donations", ex);
            }
        }

        /// <summary>
        /// Barry Mikulas
        /// created: 2023/03/17
        /// Retrieves donations by fundraising eventId
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public List<DonationVM> RetrieveDonationsByEventId(int eventId)
        {
            try
            {
                return donationAccessor.SelectDonationsByEventId(eventId);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to retrieve donations by EventId", ex);
            }
        }
        /// <summary>
        /// Barry Mikulas
        /// created: 2023/03/17
        /// Retrieves sum of donation amount by fundraising eventId
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public decimal RetrieveSumDonationsByEventId(int eventId)
        {
            try
            {
                return donationAccessor.SelectSumDonationsByEventId(eventId);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to retrieve sum of donations by EventId", ex);
            }
        }

        public int AddDonation(Donation donation)
        {
            int newDonationId = 0;

            try
            {
                newDonationId = donationAccessor.InsertDonation(donation);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to add new Donation record. ", ex);
            }

            return newDonationId;
        }

        public bool AddInKind(InKind inKind)
        {
            bool wasInserted = false;

            try
            {
                wasInserted = (0 < donationAccessor.InsertInKind(inKind));
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to add new InKind record for " + inKind.Description, ex);
            }

            return wasInserted;
        }
    }
}
