//============================================================
// Student Number	: S10198298, S10196949
// Student Name	: Lee Quan Sheng, Ong Jia Cheng
// Module  Group	: T03
//============================================================
using System;

namespace HospitalManagement
{
    internal class ClassBBed : Bed
    {
        public bool AirCon { get; set; }

        public ClassBBed(int wardNo, int bedNo, double dailyRate, bool available) : base(wardNo, bedNo, dailyRate, available)
        {
        }

        public override double CalculateCharges(string citizenStatus, int noOfDays)
        {

            double ExtraCost = 0;
            int noOfWeeks;

            double total = noOfDays * DailyRate;
            if (citizenStatus == "SC")  total = total * 0.3;
            else if (citizenStatus == "PR") total = total * 06;

            if(AirCon == true)
            {
                if(noOfDays > 6)
                {
                    noOfWeeks = (int)Math.Ceiling((double) noOfDays / 7);
                    ExtraCost = noOfWeeks * 50;
                }
                else
                {
                    ExtraCost = 50;
                }

                total = total + ExtraCost;
            }

            return total;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}