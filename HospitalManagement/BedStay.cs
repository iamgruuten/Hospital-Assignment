//============================================================
// Student Number	: S10198298, S10196949
// Student Name	: Lee Quan Sheng, Ong Jia Cheng
// Module  Group	: T03
//============================================================

using System;

namespace HospitalManagement
{
    internal class BedStay
    {
        public DateTime StartBedStay { get; set; }
        public DateTime? EndBedStay { get; set; }
        public Bed Bed { get; set; }

        public BedStay(DateTime startBedStay, Bed bed)
        {
            StartBedStay = startBedStay;
            Bed = bed;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}