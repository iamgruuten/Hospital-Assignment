//============================================================
// Student Number	: S10198298, S10196949
// Student Name	: Lee Quan Sheng, Ong Jia Cheng
// Module  Group	: T03
//============================================================
using System;

namespace HospitalManagement
{
    internal class ClassABed : Bed
    {
        public bool AccompanyingPerson { get; set; }

        public ClassABed(int wardNo, int bedNo, double dailyRate, bool available) : base(wardNo, bedNo, dailyRate, available)
        {
        }

        public override double CalculateCharges(string citizenStatus, int noOfDays)
        {
            int extra = 0;
            if (AccompanyingPerson  == true)
            {
                 extra = 100;
            }
            return (noOfDays * (DailyRate + extra));
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}