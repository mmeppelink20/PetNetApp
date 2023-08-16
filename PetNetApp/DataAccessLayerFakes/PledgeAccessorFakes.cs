using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessLayerInterfaces;

namespace DataAccessLayerFakes
{
    public class PledgeAccessorFakes : IPledgeAccessor
    {
        private List<PledgeVM> _fakePledgeVMs = new List<PledgeVM>();
        private List<PledgeVM> _specificPledgerVM = new List<PledgeVM>();
        private List<PledgeVM> _createPledge = new List<PledgeVM>();
        private PledgeVM _fakePledgeVM = new PledgeVM();

        public PledgeAccessorFakes()
        {

            //makes 300 additional InstitutionalEntityFakes
            Random randomNum = new Random();
            int[] fndEvntIdList = {100000, 100001, 100002 };
            decimal[] amList = {100, 200, 300, 400, 500, 600, 700, 800, 900, 1000 };
            string[] mssList = { "For the dog", "For my cat", "Take my money", "I hope this helps", "I have extra money" };
            string[] gnList = { "Chris", "Asa", "Mohammed", "Elon", "Mads", "Tyler", "Will", "Keegan", "Chris", "Lemon", "Liam", "Noah", "Oliver", "Elijah", "Mateo", "Lucas", "Olivia", "Emma", "Amelia", "Ava", "Sophia", "Isabella", "Luna", "Mia", "Charlotte", "Evelyn", "Levi", "Asher", "James", "Leo", "Harper", "Scarlett", "Nova", "Aurora", "Ella", "Mila", "Aria", "Ellie", "Gianna", "Sofia", "Grayson", "Ezra", "Luca", "Ethan", "Aiden", "Wyatt", "Sebastian", "Benjamin", "Mason", "Henry" };
            string[] fnList = { "Forsberg", "Armstrong", "Musk", "Rhea", "Tousah", "Hand", "Smith", "Grimes", "Andrews", "Stephenson", "Deegan", "Glasgow", "Johnson", "Williams", "Brown", "Jones", "Garcia", "Miller", "Davis", "Rodriguez", "Martinez", "Hernandez", "Lopez", "Gonzales", "Wilson", "Anderson", "Thomas", "Taylor", "Moore", "Jackson", "Martin", "Lee", "Perez", "Thompson", "White", "Harris", "Sanchez", "Clark", "Ramirez", "Lewis", "Robinson", "Walker", "Young", "Allen", "King", "Wright", "Scott", "Torres", "Nguyen", "Hill", "Flores", "Green", "Adams", "Nelson", "Baker", "Hall", "Rivera", "Campbell", "Mitchell", "Carter", "Roberts", "Gomez", "Phillips", "Evans", "Turner", "Diaz", "Parker", "Cruz", "Edwards", "Collins", "Reyes", "Stewart", "Morris", "Morales", "Murphy", "Cook", "Rogers", "Gutierrez", "Ortiz", "Morgan", "Cooper", "Peterson", "Bailey", "Reed", "Kelly", "Howard", "Ramos", "Kim", "Cox", "Ward", "Richardson", "Watson", "Brooks", "Chavez", "Wood", "James", "Bennet", "Gray", "Mendoza", "Ruiz", "Hughes", "Price", "Alvarez", "Castillo", "Sanders", "Patel", "Myers", "Long", "Ross", "Foster", "Jimenez" };
            string phNumber = "";
            for (int j = 0; j < 10; j++)
            {
                phNumber += randomNum.Next(0, 9).ToString();
            }
            randomNum.Next(0, 9);
            string[] emList = { "Chris@company.com", "Asa@company.com", "Mohammed@company.com", "Elon@company.com", "Mads@company.com", "Tyler@company.com", "Will@company.com" };
            decimal[] donAmountList = { 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000 };

            for (int i = 0; i < 100; i++)
            {
                int fundEventId = fndEvntIdList[randomNum.Next(0, fndEvntIdList.Length)];
                decimal amount = amList[randomNum.Next(0, amList.Length)];
                string message = mssList[randomNum.Next(0, mssList.Length)];
                string givenName = gnList[randomNum.Next(0, gnList.Length)];
                string familyName = fnList[randomNum.Next(0, fnList.Length)];
                string email = emList[randomNum.Next(0, emList.Length)];
                decimal donationAmount = donAmountList[randomNum.Next(0, donAmountList.Length)];

                _fakePledgeVMs.Add(new PledgeVM()
                {
                    PledgeId = 100000,
                    DonationId = 0,
                    UserId = 0,
                    FundraisingEventId = fundEventId,
                    Date = DateTime.Now,
                    Amount = amount,
                    Message = message,
                    Target = null,
                    GivenName = givenName,
                    FamilyName = familyName,
                    Phone = phNumber,
                    Email = email,
                    DonationAmount = donationAmount
                });
            }
            
            // list of pledges from a specific user
            _specificPledgerVM.Add(new PledgeVM()
            {
                DonationId = 100000,
                UserId = 100000,
                FundraisingEventId = 100000,
                Date = DateTime.Now,
                Amount = 100,
                Message = "For my friends dog",
                GivenName = "John",
                FamilyName = "Don",
                Phone = "1234567890",
                Email = "JohnDon@Company.com",
                DonationAmount = 1000,
                DonationDate = DateTime.Now
            });
            _specificPledgerVM.Add(new PledgeVM()
            {
                DonationId = 100001,
                UserId = 10000,
                FundraisingEventId = 100000,
                Date = DateTime.Now,
                Amount = 200,
                Message = "For my friends cat",
                GivenName = "John",
                FamilyName = "Don",
                Phone = "1234567890",
                Email = "JohnDon@Company.com",
                DonationAmount = 1000000,
                DonationDate = DateTime.Now
            });
            _specificPledgerVM.Add(new PledgeVM()
            {
                DonationId = 100002,
                UserId = 10000,
                FundraisingEventId = 100000,
                Date = DateTime.Now,
                Amount = 300,
                Message = "For my friends rat",
                GivenName = "John",
                FamilyName = "Don",
                Phone = "1234567890",
                Email = "JohnDon@Company.com",
                DonationAmount = 1000000000,
                DonationDate = DateTime.Now
            });

            _createPledge.Add(new PledgeVM()
            {
                UserId = 100000,
                FundraisingEventId = 100000,
                Amount = 100,
                Target = "Dog",
                Requirement = "Goal: $1000",
                Message = "For my dog",
                GivenName = "John",
                FamilyName = "Don",
                Phone = "7141234566",
                Email = "jonDon@company.com",
                IsContactPreferencePhone = false
            });
        }

        public int InsertPledge(PledgeVM pledgeVM)
        {
            int result = 0;
            int existing = _createPledge.Count;
            _createPledge.Add(pledgeVM);
            result = _createPledge.Count - existing;
            return result;
        }

        public List<PledgeVM> SelectAllPledges()
        {
            List<PledgeVM> fakes = new List<PledgeVM>();

            fakes = _fakePledgeVMs;

            return fakes;
        }

        public List<PledgeVM> SelectAllPledgesByEventId(int eventId)
        {
            List<PledgeVM> fakes = new List<PledgeVM>();

            fakes = _fakePledgeVMs;

            return fakes;
        }

        public List<PledgeVM> SelectPledgerByUserId(int userId)
        {
            List<PledgeVM> fakes = new List<PledgeVM>();

            fakes = _specificPledgerVM;

            return fakes;
        }
    }
}
