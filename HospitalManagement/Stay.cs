//============================================================
// Student Number	: S10198298, S10196949
// Student Name	: Lee Quan Sheng, Ong Jia Cheng
// Module  Group	: T03
//============================================================

using System;
using System.Collections.Generic;

namespace HospitalManagement
{
    internal class Stay
    {
        public DateTime AdmittedDate { get; set; }
        public DateTime? DischargeDate { get; set; }
        public bool IsPaid { get; set; }
        public List<MedicalRecord> MedicalRecordList { get; set; }

        public List<BedStay> BedStayList { get; set; }
        public Patient Patient { get; set; }

        public Stay(DateTime admittedDate, Patient patient)
        {
            AdmittedDate = admittedDate;
            Patient = patient;
        }

        public void AddMedicalRecord(MedicalRecord medicalRecord)
        {
            if (MedicalRecordList == null)
            {
                MedicalRecordList = new List<MedicalRecord> { medicalRecord };
            }
            else
            {
                MedicalRecordList.Add(medicalRecord);
            }
        }

        public void AddBedStay(BedStay bedStay)
        {
            if (BedStayList == null)
            {
                BedStayList = new List<BedStay> { bedStay };
            }
            else
            {
                BedStayList.Add(bedStay);
            }
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}