//============================================================
// Student Number	: S10198298, S10196949
// Student Name	: Lee Quan Sheng, Ong Jia Cheng
// Module  Group	: T03
//============================================================

using System;

namespace HospitalManagement
{
    internal class MedicalRecord
    {
        public string Diagnosis { get; set; }
        public double Temperature { get; set; }
        public DateTime DatetimeEntered { get; set; }

        public MedicalRecord(string diagnosis, double temperature, DateTime datetimeEntered)
        {
            Diagnosis = diagnosis;
            Temperature = temperature;
            DatetimeEntered = datetimeEntered;
        }
    }
}