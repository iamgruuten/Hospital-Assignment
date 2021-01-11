//============================================================
// Student Number	: S10198298, S10196949
// Student Name	: Lee Quan Sheng, Ong Jia Cheng
// Module  Group	: T03
//============================================================

namespace HospitalManagement
{
    internal class Senior : Patient
    {
        public Senior(string id, string name, int age, char gender, string citizenStatus, string status, Stay stay) :
            base(id, name, age, gender, citizenStatus, status, stay)
        {
        }

        public override double CalculateCharges()
        {
            double totalPrice = base.CalculateCharges();
            double deductedPrice = totalPrice * 0.5;
            return deductedPrice;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}