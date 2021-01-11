//============================================================
// Student Number	: S10198298, S10196949
// Student Name	: Lee Quan Sheng, Ong Jia Cheng
// Module  Group	: T03
//============================================================
using System;
namespace HospitalManagement
{
    internal class ClassCBed : Bed
    {
        public bool PortableTv { get; set; }

        public ClassCBed(int wardNo, int bedNo, double dailyRate, bool available) : base(wardNo, bedNo, dailyRate, available)
        {
        }

        public override double CalculateCharges(string citizenStatus, int noOfDays)
        {

            double total = noOfDays * DailyRate;
            double ExtraCost = 0;

            if (citizenStatus == "SC") total = total * 0.2;
            else if (citizenStatus == "PR") total = total * 0.4;

            if (PortableTv == true) ExtraCost = 30;

            return total + ExtraCost;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}