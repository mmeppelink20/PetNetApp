using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataObjects
{
    public class Applicant
    {
        public int ApplicantId { get; set; }
        public int? UserId { get; set; }        // nullable because not all applicants have accounts
        
        [Required(ErrorMessage = "Enter your first name.")]
        public string ApplicantGivenName { get; set; }

        [Required(ErrorMessage = "Enter your last name.")]
        public string ApplicantFamilyName { get; set; }

        [Required(ErrorMessage = "Enter your street address.")]
        public string ApplicantAddress { get; set; }
        public string ApplicantAddress2 { get; set; }

        [Required(ErrorMessage = "Enter your zip code.")]
        public string ApplicantZipCode { get; set; }

        [Required(ErrorMessage = "Enter your phone number.")]
        public string ApplicantPhoneNumber { get; set; }

        [Required(ErrorMessage = "Enter your email.")]
        public string ApplicantEmail { get; set; }

        [Required(ErrorMessage = "Select a home type.")]
        public string HomeTypeId { get; set; }

        [Required(ErrorMessage = "Select a home ownership type.")]
        public string HomeOwnershipId { get; set; }

        [Required(ErrorMessage = "Enter the number of children.")]
        public int NumberOfChildren { get; set; }

        [Required(ErrorMessage = "Enter the number of pets.")]
        public int NumberOfPets { get; set; }
        public bool CurrentlyAcceptingAnimals { get; set; }
    }

    public class ApplicantVM : Applicant
    {
        public List<InspectionVM> Inspections { get; set; }
        public List<AdoptionApplicationVM> AdoptionApplications { get; set; }
        public List<AdoptionPlacementVM> AdoptionPlacements { get; set; }
        public List<FosterApplicationVM> FosterApplications { get; set; }
        public List<FosterRequestVM> FosterRequests { get; set; }   
        public List<FosterRequestResponseVM> FosterRequestResponses { get; set; }
        public List<FosterPlacementVM> FosterPlacements { get; set; }
        public Users ApplicantUser { get; set; }    // for if the applicant has an account
    }
}
