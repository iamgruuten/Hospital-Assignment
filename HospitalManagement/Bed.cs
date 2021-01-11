//============================================================
// Student Number	: S10198298, S10196949
// Student Name	: Lee Quan Sheng, Ong Jia Cheng
// Module  Group	: T03
//============================================================

using System;

namespace HospitalManagement
{
    internal abstract class Bed
    {
        private int wardNo;

        public int WardNo
        {
            get { return wardNo; }
            set { wardNo = value; }
        }

        private int bedNo;

        public int BedNo
        {
            get { return bedNo; }
            set { bedNo = value; }
        }

        private double dailyRate;

        public double DailyRate
        {
            get { return dailyRate; }
            set { dailyRate = value; }
        }

        private bool available;

        public bool Available
        {
            get { return available; }
            set { available = value; }
        }

        public Bed(int wardNo, int bedNo, double dailyRate, bool available)
        {
            WardNo = wardNo;
            BedNo = bedNo;
            DailyRate = dailyRate;
            Available = available;
        }

        public abstract double CalculateCharges(String citizenStatus, int noOfDays);

        public override string ToString()
        {
            return "wardNo" + wardNo + "BedNo" + BedNo + "DailyRate" + DailyRate + "Availability" + Available;
        }
    }
}