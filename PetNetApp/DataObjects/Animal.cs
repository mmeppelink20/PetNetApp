using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace DataObjects
{
    public class Animal
    {
        public int AnimalId { get; set; }
        [Display(Name = "Animal Name")]
        public string AnimalName { get; set; }
        [Display(Name = "Type")]
        public string AnimalTypeId { get; set; }
        [Display(Name = "Breed")]
        public string AnimalBreedId { get; set; }
        public string Personality { get; set; }
        public string Description { get; set; }
        public DateTime BroughtIn { get; set; }
        public string MicrochipNumber { get; set; }
        public bool Aggressive { get; set; }
        public string AggressiveDescription { get; set; }
        public bool ChildFriendly { get; set; }
        public bool NeuterStatus { get; set; }
        public string Notes { get; set; }
        public string AnimalStatusId { get; set; }
        public int AnimalShelterId { get; set; }
    }

    public class AnimalVM : Animal
    {
        public string AnimalStatusDescription { get; set; }
        public string KennelName { get; set; }
        public string AnimalGender { get; set; }
        public string AnimalTypeDescription { get; set; }
        public string AnimalBreedDescription { get; set; }
        public List<MedicalRecord> MedicalNotes { get; set; }
        public DeathVM AnimalDeath { get; set; }
        public List<Images> AnimalImages { get; set; }
    }
}
