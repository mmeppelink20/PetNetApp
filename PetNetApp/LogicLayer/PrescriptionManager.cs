using DataAccessLayer;
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
    // Created by Tyler Hand
    // Created on 2023/03/25

    public class PrescriptionManager : IPrescriptionManager
    {
        private IPrescriptionAccessor _prescriptionAccessor = null;
        public PrescriptionManager()
        {
            _prescriptionAccessor = new PrescriptionAccessor();

         }
        public PrescriptionManager(IPrescriptionAccessor Pa)
        {
            _prescriptionAccessor = Pa;
        }

        public bool AddPresciptionByMedicalRecordId(Prescription prescription, int medicalRecordId)
        {
            bool success = false;
            int expectedRowsAffected = 1;
            try
            {
                int result = _prescriptionAccessor.InsetPrescriptionByMedicalRecordId(prescription, medicalRecordId);
                if (result == expectedRowsAffected)
                {
                    success = true;
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred. The prescription was not saved.", ex);
            }
            return success;

        }

        public List<PrescriptionVM> RetrievePrescriptions(int animalId)
        {
            try
            {
                return _prescriptionAccessor.SelectAllPrescriptions(animalId);
            }
            catch (Exception e)
            {
                throw new ApplicationException("Failed to retrieve Prescriptions", e);
            }
        }
    }
}
