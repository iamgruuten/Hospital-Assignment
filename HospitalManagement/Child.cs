//============================================================
// Student Number	: S10198298, S10196949
// Student Name	: Lee Quan Sheng, Ong Jia Cheng
// Module  Group	: T03
//============================================================

namespace HospitalManagement
{
    internal class Child : Patient
    {
        public double CdaBalance { get; set; }

        public Child(string id, string name, int age, char gender, string citizenStatus, string status, Stay stay, double cdaBalance) :
            base(id, name, age, gender, citizenStatus, status, stay)
        {
            CdaBalance = cdaBalance;
        }

        public override double CalculateCharges()
        {
            double totalPrice = base.CalculateCharges();
            return totalPrice;
        }

        public override string ToString()
        {
            if (CitizenStatus == "SC")
            {
                return base.ToString();
            }
            else
            {
                return base.ToString() + "," + CdaBalance;
            }
        }
    }
}