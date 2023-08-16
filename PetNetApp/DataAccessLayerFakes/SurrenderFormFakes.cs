using DataAccessLayerInterfaces;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DataObjects.SurrenderForm;

namespace DataAccessLayerFakes
{
    public class SurrenderFormFakes : ISurrenderFormAccessor
    {
        private List<SurrenderForm> surrenderForms = new List<SurrenderForm>();
        private List<SurrenderFormVM> surrenderFormVMs = new List<SurrenderFormVM>();
        private ISurrenderFormAccessor surrenderFormAccessor;

        public SurrenderFormFakes()
        {
            surrenderFormVMs.Add(new SurrenderFormVM
            {
                SurrenderFormID = 100000,
                AnimalType = "Cat",
                ReasonForSurrender = "Moving Away",
                SpayOrNeuterStatus = false,
                ContactPhone = "3191234567",
                ContactEmail = "Contact@email.com"
            });

            surrenderFormVMs.Add(new SurrenderFormVM
            {
                SurrenderFormID = 100230,
                AnimalType = "Butterfly",
                ReasonForSurrender = "Too emotional",
                SpayOrNeuterStatus = false,
                ContactPhone = "3191234567",
                ContactEmail = "Contact@email.com"
            });

            surrenderFormVMs.Add(new SurrenderFormVM
            {
                SurrenderFormID = 100500,
                AnimalType = "Dog",
                ReasonForSurrender = "Won't pay rent",
                SpayOrNeuterStatus = true,
                ContactPhone = "3191234567",
                ContactEmail = "Contact@email.com"
            });
        }

        public int InsertSurrenderForm(string AnimalType, string ReasonForSurrender, bool SpayOrNeuterStatus, string ContactPhone, string ContactEmail)
        {
            int rowsAffected = 0;

            int existingRows = surrenderFormVMs.Count();
            surrenderFormVMs.Add(new SurrenderFormVM
            {
                SurrenderFormID = 200000,
                AnimalType = "Dog",
                ReasonForSurrender = "Won't pay rent",
                SpayOrNeuterStatus = true,
                ContactPhone = "3191234567",
                ContactEmail = "Contact@email.com"
            });

            rowsAffected = surrenderFormVMs.Count - existingRows;
            return rowsAffected;

        }

         public List<SurrenderForm> RetrieveAllSurrenderForms()
        {
            List<SurrenderForm> surrenderForms = new List<SurrenderForm>();

            foreach (SurrenderFormVM form in surrenderFormVMs)
            {   
                surrenderForms.Add(form);
            }

            return surrenderForms;

        }

        public int RemoveSurrenderForm(int SurrenderFormID)
        {
            int rowsAffected = 0;

            if (surrenderFormVMs.Remove(surrenderFormVMs.FirstOrDefault(v => v.SurrenderFormID == SurrenderFormID)))
            {
                rowsAffected++;
            }

            return rowsAffected;

        }
    }
}
