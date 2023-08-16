///<summary>
/// William Rients
/// Created: 2023/04/07
/// 
/// </summary>
/// <remarks>
/// Oleksiy Fedchuk
/// Updated: 2023/04/20
/// Final QA
/// </remarks>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class Pledge
    {
        public int PledgeId { get; set; }
        public DateTime Date { get; set; }
        public int DonationId { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public string Message { get; set; }
        [Required]
        public string Target { get; set; }
        [Required]
        public string Requirement { get; set; }
        public bool RequirementMet { get; set; }
        public int UserId { get; set; }
        [Required]
        public string GivenName { get; set; }
        [Required]
        public string FamilyName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        public bool IsContactPreferencePhone { get; set; }
        public int FundraisingEventId { get; set; }
        public bool ReminderSent { get; set; }
        public DateTime ReminderDate { get; set; }
    }

    public class PledgeVM : Pledge
    {
        public decimal DonationAmount { get; set; }
        public DateTime? DonationDate { get; set; }
    }
}
