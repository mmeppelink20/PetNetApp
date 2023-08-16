using DataAccessLayerInterfaces;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayerFakes
{
    public class DonationAccessorFake : IDonationAccessor
    {
        private List<DonationVM> fakeDonations = new List<DonationVM>();
        public DonationAccessorFake()
        {
            fakeDonations.Add(new DonationVM
            {
                DonationId = 1,
                UserId = 1,
                User = new Users() { GivenName = "Gwen", FamilyName = "Arman"},
                ShelterId = 1,
                Amount = 100.00M,
                Message = "I want to help",
                DateDonated = DateTime.Today,
                HasInKindDonation = true,
                Anonymous = false,
                Target = "To help",
                PaymentMethod = "Cash",
                FundraisingEventId = 1000,
                ShelterName = "Doggy Care",
                InKindList = new List<InKind>()
                {
                    new InKind()
                    {
                        InKindId = 1, DonationId = 1, Description = "Dog Toys",
                        Quantity = 10, Target = "To Help", Recieved = true
                    },
                    new InKind()
                    {
                        InKindId = 2, DonationId = 1, Description = "Cat Toys",
                        Quantity = 10, Target = "To Help", Recieved = true
                    },
                    new InKind()
                    {
                        InKindId = 3, DonationId = 1, Description = "Rabbit Food",
                        Quantity = 15, Target = "To Help", Recieved = true
                    }
                }
            });
            fakeDonations.Add(new DonationVM
            {
                DonationId = 2,
                ShelterId = 1,
                Amount = 150.00M,
                Message = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" + 
                "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
                DateDonated = DateTime.Today,
                GivenName = "John",
                FamilyName = "Smith",
                HasInKindDonation = false,
                Anonymous = false,
                Target = "Word " + "Word " + "Word " + "Word " + "Word " + "Word " + "Word " + "Word " + "Word " + "Word " + "Word " + "Word " + "Word " + "Apple " + "Word " + "Word " + "Word " + "Word " +
                "Word " + "Word " + "Test " + "Test " + "Test " + "Test " + "Test " + "Test " + "Test " + "Test " + "Test " + "Test " + "Test " + "Test " + "Test " + "Test " + "Test " + "Test ",
                PaymentMethod = "Cash",
                ShelterName = "Kitty Care",
                FundraisingEventId = 1000
            });
            fakeDonations.Add(new DonationVM
            {
                DonationId = 3,
                ShelterId = 1,
                Amount = 110.00M,
                Message = "In honor of Buddy Senior",
                DateDonated = DateTime.Today,
                GivenName = "John",
                FamilyName = "Smith",
                HasInKindDonation = false,
                Anonymous = false,
                Target = "To help",
                PaymentMethod = "Cash",
                ShelterName = "Snakey Care",
                FundraisingEventId = 1001
            });
            fakeDonations.Add(new DonationVM
            {
                DonationId = 4,
                UserId = 2,
                User = new Users() { GivenName = "Hoang", FamilyName = "Chu" },
                ShelterId = 1,
                Amount = 9.99M,
                Message = "Một khoản đóng góp để giúp đỡ. Gia đình tôi rất thích nơi trú ẩn này.",
                DateDonated = DateTime.Today,
                HasInKindDonation = false,
                Anonymous = false,
                Target = "To help",
                PaymentMethod = "Cash",
                ShelterName = "Animal Care",
            });
        }

        public List<DonationVM> SelectAllDonations()
        {
            return fakeDonations;
        }

        public DonationVM SelectDonationByDonationId(int donationID)
        {
            return fakeDonations.Find(d => d.DonationId == donationID);
        }

        public List<DonationVM> SelectDonationsByEventId(int eventId)
        {

            return fakeDonations.Where(fd => fd.FundraisingEventId == eventId).ToList();
            throw new NotImplementedException();
        }

        public List<DonationVM> SelectDonationsByShelterId(int ShelterId)
        {
            return fakeDonations.Where(d => d.ShelterId == ShelterId).ToList();
        }

        public List<DonationVM> SelectDonationsByUserId(int usersId)
        {
            List<DonationVM> fakeSortedDonations = new List<DonationVM>();
            foreach (var donation in fakeDonations)
            {
                if (donation.UserId == usersId)
                {
                    fakeSortedDonations.Add(donation);
                }
            }
            return fakeSortedDonations;
        }

        public List<InKind> SelectInKindsByDonationId(int donationId)
        {
            return fakeDonations.First(don => don.DonationId == donationId).InKindList;
        }

        public decimal SelectSumDonationsByEventId(int eventId)
        {
            return fakeDonations.Where(fd => fd.FundraisingEventId == eventId).ToList().Sum(fd => fd.Amount).GetValueOrDefault();
            // throw new NotImplementedException();
        }

        public int InsertDonation(Donation donation)
        {
            int newDonationId = 0;
            int oldFakeDonationsCount = fakeDonations.Count;

            try
            {
                donation.DonationId = fakeDonations.Count + 1;
                fakeDonations.Add((DonationVM)donation);

                if (fakeDonations.Count > oldFakeDonationsCount)
                {
                    newDonationId = donation.DonationId;
                }
                else
                {
                    throw new ApplicationException("Donation not added.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return newDonationId;
        }

        public int InsertInKind(InKind inKind)
        {
            int rowsAffected = 0;

            try
            {
                var donation = fakeDonations.FirstOrDefault(d => d.DonationId == inKind.DonationId);

                if (donation.InKindList == null)
                {
                    donation.InKindList = new List<InKind>();
                }

                int oldInKindCount = donation.InKindList.Count;
                inKind.InKindId = oldInKindCount + 1;
                donation.InKindList.Add(inKind);
                rowsAffected = donation.InKindList.Count - oldInKindCount;

                if (rowsAffected > 0)
                {
                    donation.HasInKindDonation = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return rowsAffected;
        }
    }
}
