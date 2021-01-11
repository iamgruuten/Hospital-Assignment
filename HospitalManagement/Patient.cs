//============================================================
// Student Number	: S10198298, S10196949
// Student Name	: Lee Quan Sheng, Ong Jia Cheng
// Module  Group	: T03
//============================================================

using System;

namespace HospitalManagement
{
    internal abstract class Patient
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public char Gender { get; set; }
        public string CitizenStatus { get; set; }
        public string Status { get; set; }
        public Stay Stay { get; set; }

        public Patient(string id, string name, int age, char gender, string citizenStatus, string status, Stay stay)
        {
            Id = id;
            Name = name;
            Age = age;
            Gender = gender;
            CitizenStatus = citizenStatus;
            Status = status;
            Stay = stay;
        }

        public virtual double CalculateCharges()
        {
            double totalPrice = 0;
            int noOfdays = 0;

            foreach (BedStay b in Stay.BedStayList)
            {
                noOfdays = Convert.ToInt32((b.EndBedStay.Value - b.StartBedStay).TotalDays) + 1;

                double price = b.Bed.CalculateCharges(CitizenStatus, noOfdays);
                totalPrice = totalPrice + price;
            }
            return totalPrice;
        }

        public override string ToString()
        {
            return Name + "," + Id + "," + Age + "," + Gender + "," + CitizenStatus;
        }
    }
}