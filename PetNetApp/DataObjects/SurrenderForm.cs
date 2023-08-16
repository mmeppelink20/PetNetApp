/// <summary>
/// Alexis Oetken
/// Created: 2023/04/20
/// 
/// Bookmark Object Class
/// 
/// </summary>
///
/// <remarks>
/// Oleksiy Fedchuk
/// Updated: 2023/04/23
/// 
/// Final QA
/// </remarks>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class SurrenderForm
    {

        public string AnimalType { get; set; }

        public string ReasonForSurrender { get; set; }

        public bool SpayOrNeuterStatus { get; set; }

        public string ContactPhone { get; set; }

        public string ContactEmail { get; set; }

       

        public int? SurrenderFormID { get; set; }


        public class SurrenderFormVM : SurrenderForm
        {
            public List<SurrenderForm> AllSurrenderForms { get; set; }
        }


    }
}
