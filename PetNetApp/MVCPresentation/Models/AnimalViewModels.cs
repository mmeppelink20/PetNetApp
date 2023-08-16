using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataObjects;

namespace MVCPresentation.Models
{
    public class AdoptableAnimalModel
    {
        public AnimalVM AnimalVM { get; set; }
        public string ShelterName { get; set; }
        public string AnimalImageSource { get; set; }
    }
}