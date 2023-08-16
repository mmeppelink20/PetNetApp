/// <summary>
/// Created: 2023/02/01
/// 
/// Items object
/// </summary>
/// <remarks>
/// Oleksiy Fedchuk
/// Updated: 2023/04/13
/// 
/// FinalQA
/// </remarks>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DataObjects
{
    public class Donation
    {
        public int DonationId { get; set; }
        public int? UserId { get; set; }
        [Required]
        public int ShelterId { get; set; }
        [Required]
        public decimal? Amount { get; set; }
        public string Message { get; set; }
        public DateTime? DateDonated { get; set; }
        [Required]
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public bool HasInKindDonation { get; set; }
        public bool Anonymous { get; set; }
        public string Target { get; set; }
        public string PaymentMethod { get; set; }
        public int? ScheduledDonationId { get; set; }
        public int? FundraisingEventId { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Email { get; set; }
    }

    public class DonationVM : Donation
    {
        public List<InKind> InKindList { get; set; }
        public Users User { get; set; }
        public string ShelterName { get; set; }
    }
}
