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
    public class TestManager : ITestManager
    {
        private ITestAccessor _testAccessor;
        public TestManager()
        {
            _testAccessor = new TestAccessor();
        }
        public TestManager(ITestAccessor testAccessor)
        {
            _testAccessor = testAccessor;
        }

        public TestVM RetrieveTestByMedicalRecordId(int medicalRecordId)
        {
            TestVM test = null;
            try
            {
                test = _testAccessor.SelectTestByMedicalRecordId(medicalRecordId);
            }
            catch (Exception up)
            {
                throw new ApplicationException("failed to retrieve data.", up);
            }
            return test;
        }

        public List<Test> RetrieveTestsByAnimalId(int animalId)
        {
            try
            {
                return _testAccessor.SelectTestsByAnimalId(animalId);
            }
            catch (Exception up)
            {
                throw new ApplicationException("Failed to load Tests", up);
            }
        }

        public bool AddTestByMedicalRecordId(Test test, int medicalRecordId)
        {
            bool result = false;
            try
            {
                result = 1 == _testAccessor.InsertTestByMedicalRecordId(test, medicalRecordId);
                if (!result)
                {
                    throw new ApplicationException("Concurrency Conflict");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

    }
}
