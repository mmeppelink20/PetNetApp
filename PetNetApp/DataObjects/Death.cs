using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class Death
    {
        public int DeathId { get; set; }
        public int UsersId { get; set; }
        public int AnimalId { get; set; }
        public DateTime DeathDate { get; set; }
        public string DeathCause { get; set; }
        public string DeathDisposal { get; set; }
        public DateTime DeathDisposalDate { get; set; }
        public string DeathNotes { get; set; }
    }

    public class DeathVM : Death
    {
        public string AnimalName { get; set; }
        public string AnimalType { get; set; }
        public string AnimalBreed { get; set; }
        public string AnimalGender { get; set; }
    }
}
