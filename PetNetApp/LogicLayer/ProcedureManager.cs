/// <summary>
/// Andrew Cromwell
/// Created: 2023/02/08
/// 
/// Main logic for geting and saving procedure records
/// </summary>
///
/// <remarks>
/// Andrew Cromwell
/// Updated: 2023/02/15
/// Added EditProcedureByMedicalRecordIdAndProcedureId and GetProceduresByAnimalId
/// </remarks>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessLayer;
using DataAccessLayerInterfaces;
using LogicLayerInterfaces;

namespace LogicLayer
{
    public class ProcedureManager : IProcedureManager
    {
        private IProcedureAccessor _procedureAccessor = null;
        

        public ProcedureManager()
        {
            _procedureAccessor = new ProcedureAccessor();
        }

        public ProcedureManager(IProcedureAccessor procedureAccessor)
        {
            _procedureAccessor = procedureAccessor;
        }

        /// <summary>
        /// Andrew Cromwell
        /// Created: 2023/02/08
        /// 
        /// Takes procedure, passes it to the ProcedureAccessor, receives a response 
        /// from the accessor, and returns a respnonse about whether the procedure was
        /// added to the database.
        /// </summary>
        /// <param name="procedure">the procedure to be added to the db</param>
        /// <param name="medicalRecordId">the id of the medical record associated with the procedure</param>
        /// <exception cref="ApplicationException">Insert Fails</exception>
        /// <returns>Rows affected</returns>
        public bool AddProcedureByMedicalRecordId(Procedure procedure, int medicalRecordId)
        {
            bool success = false;
            int expectedRowsAffected = 2;
            try
            {
                int result = _procedureAccessor.InsetProcedureByMedicalRecordId(procedure, medicalRecordId);
                if(result == expectedRowsAffected)
                {
                    success = true;
                }
            } catch(Exception ex)
            {
                throw new ApplicationException("An error occurred. The procedure was not saved.", ex);
            }
            return success;
            
        }

        /// <summary>
        /// Andrew Cromwell
        /// Created: 2023/02/15
        /// 
        /// Takes two procedures, passes them to the ProcedureAccessor, receives a response 
        /// from the accessor, and returns a respnonse about whether the old procedure was
        /// updated to be the new procedure the database.
        /// </summary>
        /// <param name="procedure">the procedure to replace the old procedure in the db</param>
        /// <param name="oldProcedure">the procedure to be overwriten</param>
        /// <exception cref="ApplicationException">Update Fails</exception>
        /// <returns>Rows affected</returns>
        public bool EditProcedureByProcedureId(Procedure procedure, Procedure oldProcedure)
        {
            bool success = false;
            int expectedRowsAffected = 1;
            try
            {
                int result = _procedureAccessor.UpdateProcedureByProcedureId(procedure, oldProcedure);
                if(result == expectedRowsAffected)
                {
                    success = true;
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred. The procedure was not saved.", ex);
            }
            return success;
        }

        public ProcedureVM RetrieveProcedureByMedicalRecordId(int medicalRecordId)
        {
            ProcedureVM procedure = null;
            try
            {
                procedure = _procedureAccessor.SelectProcedureByMedicalRecordId(medicalRecordId);
            }
            catch (Exception up)
            {
                throw new ApplicationException("Data not found.", up);
            }
            return procedure;
        }

        /// <summary>
        /// Andrew Cromwell
        /// Created: 2023/02/15
        /// </summary>
        /// <param name="animalId">the id of the animal to return the procedure records of</param>
        /// <exception cref="ApplicationException">Faild to retrieve procedures</exception>
        /// <returns>List of the procedures associated with the animal</returns>
        public List<ProcedureVM> RetrieveProceduresByAnimalId(int animalId)
        {
            List<ProcedureVM> procedures = new List<ProcedureVM>();
            try
            {
                procedures = _procedureAccessor.SelectProceduresByAnimalId(animalId);
            } catch(Exception ex)
            {
                throw new ApplicationException("An error occurred. The procedures could not be retreived.", ex);
            }
            return procedures;
        }
    }
}
