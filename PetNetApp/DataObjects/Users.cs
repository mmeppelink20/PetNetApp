/// <summary>
/// Mads Rhea
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

namespace DataObjects
{
    public class Users
    {
        public int UsersId { get; set; }
        public string GenderId { get; set; }
        public string PronounId { get; set; }
        public int? ShelterId { get; set; }
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string Zipcode { get; set; }
        public string Phone { get; set; }
        public DateTime CreationDate { get; set; }
        public bool Active { get; set; }
        public bool Suspend { get; set; }
    }

    public class UsersVM : Users
    {
        public List<string> Roles {get;set;}
        public List<ScheduleVM> Schedule { get; set; }
        public List<UsersAdoptionRecords> AdoptionRecords { get; set; }
    }

    public class UsersAdoptionRecords
    {
        public string animalName { get; set; }
        public string animalSpecies { get; set; }
        public string animalBreed { get; set; }
        public int oldAnimalId { get; set; }
    }
}
