// Created by Asa Armstrong
// 2023/03/23

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class FosterApplicationResponse
    {
        public int FosterApplicationResponseId { get; set; }
        public int FosterApplicationId { get; set; }
        public int UsersId { get; set; }
        public bool Approved { get; set; }
        public DateTime FosterApplicationResponseDate { get; set; }
        public string FosterApplicationResponseNotes { get; set; }

        /// <summary>
        /// Asa Armstrong
        /// Created: 2023/03/23
        /// 
        /// Returns true if two FosterApplicationResponses are equal.
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// <returns>True if all the properties of the two objects are the same.</returns>
        public override bool Equals(object obj)
        {
            try
            {
                FosterApplicationResponse response2 = (FosterApplicationResponse)obj;

                return (this.FosterApplicationResponseId == response2.FosterApplicationResponseId) &&
                    (this.FosterApplicationId == response2.FosterApplicationId) &&
                    (this.UsersId == response2.UsersId) &&
                    (this.Approved == response2.Approved) &&
                    (this.FosterApplicationResponseDate == response2.FosterApplicationResponseDate) &&
                    (this.FosterApplicationResponseNotes == response2.FosterApplicationResponseNotes);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }

    public class FosterApplicationResponseVM : FosterApplicationResponse
    {
        public FosterApplication FosterApplicationResponseFosterApplication { get; set; }
        public Users Responder { get; set; }
        public int ApplicantId { get; set; }
        public string FosterApplicantGivenName { get; set; }
        public string FosterApplicantFamilyName { get; set; }
    }
}
