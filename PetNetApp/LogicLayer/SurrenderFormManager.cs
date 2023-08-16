/// <summary>
/// Alexis Oetken
/// Created: 2023/04/20
/// 
/// </summary>
///
/// <remarks>
/// Oleksiy Fedchuk
/// Updated: 2023/04/23
/// 
/// Final QA
/// </remarks>
using DataAccessLayerInterfaces;
using DataObjects;
using LogicLayerInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace LogicLayer
{
    public class SurrenderFormManager : ISurrenderFormManager
    {
        private ISurrenderFormAccessor _surrenderFormAccessor = null; 

        public SurrenderFormManager()
        {
            _surrenderFormAccessor = new DataAccessLayer.SurrenderFormAccessor();
        }

        public SurrenderFormManager(ISurrenderFormAccessor surrenderFormAccessor)
        {
            _surrenderFormAccessor = surrenderFormAccessor; 
        }

        public static List<SurrenderForm.SurrenderFormVM> AllSurrenderForms { get; set; }

        public bool InsertSurrenderForm(string AnimalType, string ReasonForSurrender, bool SpayOrNeuterStatus, string ContactPhone, string ContactEmail)
        {
            bool result = false;

            try
            {
                result = 1 == _surrenderFormAccessor.InsertSurrenderForm(AnimalType, ReasonForSurrender, SpayOrNeuterStatus, ContactPhone, ContactEmail);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public bool RemoveSurrenderForm(int SurrenderFormID)
        {
            bool result = false;

            try
            {
                result = 1 == _surrenderFormAccessor.RemoveSurrenderForm(SurrenderFormID);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result; 
        }

        public List<SurrenderForm> RetrieveAllSurrenderForms()
        {

            try
            {
                List<SurrenderForm> surrenderForms = null;
                surrenderForms = _surrenderFormAccessor.RetrieveAllSurrenderForms();
                return surrenderForms;
            }
            catch (Exception ex)
            {
                throw ex;
            }

           
        }
    }
}
