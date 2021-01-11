//============================================================
// Student Number	: S10198298, S10196949
// Student Name	: Lee Quan Sheng, Ong Jia Cheng
// Module  Group	: T03
//============================================================

using System.Collections.Generic;

namespace HospitalManagement
{
    internal class Adult : Patient
    {
        public double MedisaveBalance { get; set; }

        public Adult(string id, string name, int age, char gender, string citizenStatus, string status, Stay stay, double medisaveBalance) :
            base(id, name, age, gender, citizenStatus, status, stay)
        {
            MedisaveBalance = medisaveBalance;
        }

        public override double CalculateCharges()
        {
            double totalPrice = base.CalculateCharges();
            return totalPrice;
        }

        public override string ToString()
        {
            if (CitizenStatus == "SC" || CitizenStatus == "PR")
            {
                return base.ToString();
            }
            else
            {
                return base.ToString() + "," + MedisaveBalance;
            }
        }
    }
}