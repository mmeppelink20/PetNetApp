/// <summary>
/// Zaid Rachman
/// Created: 2023/02/09
/// 
/// Vaccination Accessor Fake. Used for VaccinationManagerTests
/// 
/// </summary>
///
/// <remarks>
/// Updater Name
/// Updated: yyyy/mm/dd
/// </remarks>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayerInterfaces;
using DataObjects;

namespace DataAccessLayerFakes
{
    public class VaccinationAccessorFake : IVaccinationAccessor
    {
        private List<Vaccination> fakeVaccinations = new List<Vaccination>();
        private List<MedicalRecord> fakeMedicalRecords = new List<MedicalRecord>();


        public VaccinationAccessorFake()
        {
            //adding fake medicalRecords information
            fakeMedicalRecords.Add(new MedicalRecord
            {
                MedicalRecordId = 999999,
                AnimalId = 999999
            });
            //adding fake vaccination information
            fakeVaccinations.Add(new Vaccination()
            {
                VaccineId = 999999,
                MedicalRecordId = 999999,
                UserId = 999999,
                VaccineName = "TestVaccine1",
                VaccineAdminsterDate = new DateTime(2000, 12, 12)
            });
            fakeVaccinations.Add(new Vaccination()
            {
                VaccineId = 999998,
                MedicalRecordId = 999998,
                UserId = 999999,
                VaccineName = "TestVaccine1",
                VaccineAdminsterDate = new DateTime(2000, 12, 12)
            });
            fakeVaccinations.Add(new Vaccination()
            {
                VaccineId = 999997,
                MedicalRecordId = 999997,
                UserId = 999999,
                VaccineName = "TestVaccine1",
                VaccineAdminsterDate = new DateTime(2000, 12, 12)
            });
            fakeVaccinations.Add(new VaccinationVM()
            {
                VaccineId = 666,
                MedicalRecordId = 666,
                UserId = 666,
                VaccineName = "TestVaccine1",
                VaccineAdminsterDate = new DateTime(2023, 03, 03)
            });

        }
        public int InsertVaccination(Vaccination vaccination, int animalId)
        {
            int newRows = 0;
            int existingRows = fakeVaccinations.Count;
            fakeVaccinations.Add(vaccination);
            newRows = fakeVaccinations.Count - existingRows;
            return newRows;
        }

        public VaccinationVM SelectVaccinationByMedicalRecordId(int medicalRecordId)
        {
            VaccinationVM vaccination = null;
            foreach (var v in fakeVaccinations)
            {
                if (v.MedicalRecordId == medicalRecordId)
                {
                    vaccination = (VaccinationVM)v;
                }
            }
            return vaccination;
        }

        public List<Vaccination> SelectVaccinationsByAnimalId(int animalId)
        {
            List<Vaccination> animalVaccinations = new List<Vaccination>();

            foreach (var fakeMedicalRecord in fakeMedicalRecords)
            {
                if (fakeMedicalRecord.AnimalId == animalId)
                {
                    foreach (var fakeVaccine in fakeVaccinations)
                    {
                        if (fakeMedicalRecord.MedicalRecordId == fakeVaccine.MedicalRecordId)
                        {
                            animalVaccinations.Add(fakeVaccine);
                        }
                    }
                }
            }
            return animalVaccinations;
        }

        public int UpdateVaccination(Vaccination oldVaccination, Vaccination newVaccination)
        {

            int rowsAffected = 0;

            int rows = fakeVaccinations.Count;

            Vaccination testVaccination = new Vaccination();
            for (int i = 0; i < rows; i++)
            {
                //Console.WriteLine(oldVaccination.VaccinationId);
                if (fakeVaccinations[i].VaccineId == oldVaccination.VaccineId)
                {
                    Console.WriteLine("Success");
                    fakeVaccinations[i].VaccineId = newVaccination.VaccineId;
                    fakeVaccinations[i].MedicalRecordId = newVaccination.MedicalRecordId;
                    fakeVaccinations[i].VaccineName = newVaccination.VaccineName;
                    fakeVaccinations[i].VaccineAdminsterDate = newVaccination.VaccineAdminsterDate;
                    fakeVaccinations[i].UserId = newVaccination.UserId;
                    rowsAffected = rowsAffected + 1;

                }


            }


            return rowsAffected;


        }
    }
}
