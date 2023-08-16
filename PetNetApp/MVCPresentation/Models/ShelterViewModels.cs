using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCPresentation.Models
{
    public class EditHoursOfOperationViewModel
    {
        public string DayOfWeek { get; set; }
        public string OpenHour { get; set; }
        public string CloseHour { get; set; }
    }
}