//============================================================
// Student Number	: S10198298, S10196949
// Student Name	: Lee Quan Sheng, Ong Jia Cheng
// Module  Group	: T03
//============================================================

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace HospitalManagement
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            int option;
            List<Bed> bedsLists = new List<Bed>();
            initBed(bedsLists);
            List<Patient> patientList = new List<Patient>();
            initPatient(patientList);

            do
            {
                option = Menu();

                switch (option)
                {
                    case 0:
                        Console.WriteLine("Program Exited");
                        break;
                    case 1:
                        Console.WriteLine("Option 1. View All Patients");
                        ListPatients(patientList, null);
                        break;

                    case 2:
                        ListBeds(bedsLists);
                        break;

                    case 3:
                        PromptPatient(patientList);
                        break;

                    case 4:
                        PromptBed(bedsLists);
                        break;

                    case 5:
                        RegisterStay(patientList, bedsLists);
                        break;

                    case 6:
                        RetrievePatient(patientList);
                        break;

                    case 7:
                        List<string> statuses = new List<string> { "Admitted" };
                        AddMedicalRecord(patientList, statuses);
                        break;

                    case 8:
                        ViewMedicalRecords(patientList);
                        break;

                    case 9:
                        TransferPatientBed(patientList, bedsLists);
                        break;

                    case 10:
                        DischargePayment(patientList, bedsLists);
                        break;

                    case 11:
                        CurrencyAPI();
                        break;

                    case 12:
                        PM25API pM = getPM25API().Result;
                        displayPM25api(pM);
                        break;
                }
            } while (option != 0);
        }

        private static int Menu()
        {
            Console.WriteLine();
            List<string> choices = new List<string>
            {
                "Exit",
                "View all patients",
                "View all beds",
                "Register patient",
                "Add new bed",
                "Register a hospital stay",
                "Retrieve patient details",
                "Add medical record entry",
                "View medical records",
                "Transfer patient to another bed",
                "Discharge and payment",
                "Display currencies exchange rate",
                "Display PM 2.5 information"
            };

            Console.Write("MENU\n====\n");
            for (int i = 1; i < choices.Count; i++)
            {
                Console.WriteLine("{0}. {1}", i, choices[i]);
            }
            Console.Write("0. Exit\n\nEnter your option: ");
            string ioption = Console.ReadLine();
            int option;
            while (!int.TryParse(ioption, out option) || option < 0 || option > 12) 
            {
                Console.Write("Input error. Enter your option again: ");
                ioption = Console.ReadLine();
            }
            Console.WriteLine();
            return option;
        }

        private static void ListPatients(List<Patient> pList, List<string> statuses) // option 1
        {
            Console.WriteLine(" {0, -12} {1, -12} {2, -12} {3, -12} {4, -12} {5, -12}",
                "Name", "ID No.", "Age", "Gender", "Citizenship", "Status");

            foreach (Patient p in pList)
            {
                if (statuses == null || statuses.Contains(p.Status))
                {
                    Console.WriteLine(" {0, -12} {1, -12} {2, -12} {3, -12} {4, -12} {5, -12}",
                        p.Name, p.Id, p.Age, p.Gender, p.CitizenStatus, p.Status);
                }
            }
            Console.WriteLine();
        }

        private static void ListBeds(List<Bed> bedList) // option 2
        {
            Console.WriteLine(" {0, -8} {1, -10} {2, -10} {3, -12} {4, -12} {5, -12}", "No", "Type", "Ward No", "Bed No", "Daily Rate", "Available");
            int count = 0;
            foreach (Bed bed in bedList)
            {
                count = count + 1;
                Console.WriteLine(" {0, -8} {1, -10} {2, -10} {3, -12} {4, -12} {5, -12} ", count, SearchBedType(bed), bed.WardNo, bed.BedNo, bed.DailyRate, bed.Available);
            }
        }

        private static void PromptPatient(List<Patient> pList) // option 3 input
        {
            Console.WriteLine("Option 3. Register patient");
            
            Console.Write("Enter Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter Identification Number: ");
            string id = Console.ReadLine();

            string iage = ""; int age = 0; bool boolean = true;
            do
            {
                if (boolean == false)
                {
                    Console.Write("Age value is invalid! Type 'X' to exit.\n");
                }
                Console.Write("Enter Age: ");
                iage = Console.ReadLine();
                if (iage == "X") Environment.Exit(0);
                else if (!int.TryParse(iage, out age) || age < 0) boolean = false;
                else boolean = true;
            } while (boolean == false);

            string igender = ""; char gender = 'X';
            do
            {
                if (boolean == false)
                {
                    Console.Write("Gender value is invalid! Type 'X' to exit.\n");
                }
                Console.Write("Enter Gender [M/F]: ");
                igender = Console.ReadLine();
                if (igender == "X") Environment.Exit(0);
                else if (!char.TryParse(igender, out gender) || (gender != 'M' && gender != 'F')) boolean = false;
                else boolean = true;
            } while (boolean == false);

            string citizenStatus = "";
            do
            {
                if (boolean == false)
                {
                    Console.Write("Citizenship status is invalid! Type 'X' to exit.\n");
                }
                Console.Write("Enter Citizenship Status [SC/PR/Foreigner]: ");
                citizenStatus = Console.ReadLine();
                if (citizenStatus == "X") Environment.Exit(0);
                else if (citizenStatus != "SC" && citizenStatus != "PR" && citizenStatus != "Foreigner") boolean = false;
                else boolean = true;
            } while (boolean == false);

            RegisterPatient(pList, name, id, age, gender, citizenStatus);
        }

        private static void RegisterPatient(List<Patient> pList, string name, string id, int age, char gender, string citizenStatus) // option 3
        {
            string isubsidiser = ""; double subsidiser = 0; bool boolean = true; string text = "";
            if (age <= 12)
            {
                if (citizenStatus == "SC")
                {
                    do
                    {
                        if (boolean == false)
                        {
                            Console.Write("CDA balance invalid! Type 'X' to exit.\n");
                        }
                        Console.Write("Enter Child Development Account (CDA) Balance: ");
                        isubsidiser = Console.ReadLine();
                        if (isubsidiser == "X") Environment.Exit(0);
                        else if (!double.TryParse(isubsidiser, out subsidiser))
                        {
                            boolean = false;
                        }
                        else boolean = true;
                    } while (boolean == false);
                }
                Child c = new Child(id, name, age, gender, citizenStatus, "Registered", null, subsidiser);
                text = c.ToString();
                pList.Add(c);
            }
            else if (age <= 64)
            {
                if (citizenStatus == "SC" || citizenStatus == "PR")
                {
                    do
                    {
                        if (boolean == false)
                        {
                            Console.Write("Medisave balance invalid! Type 'X' to exit.\n");
                        }
                        Console.Write("Enter Medisave Balance: ");
                        isubsidiser = Console.ReadLine();
                        if (isubsidiser == "X") Environment.Exit(0);
                        else if (!double.TryParse(isubsidiser, out subsidiser))
                        {
                            boolean = false;
                        }
                        else boolean = true;
                    } while (boolean == false);
                }

                Adult a = new Adult(id, name, age, gender, citizenStatus, "Registered", null, subsidiser);
                text = a.ToString();
                pList.Add(a);
            }
            else
            {
                Senior s = new Senior(id, name, age, gender, citizenStatus, "Registered", null);
                text = s.ToString();
                pList.Add(s);
            }
            using (StreamWriter file = new StreamWriter("patients.csv", true))
            {
                file.Write("\n" + text);
            }
            Console.WriteLine("\n{0} is registered successfully.\n", name);
        }

        private static void PromptBed(List<Bed> bedList)
        {
            bool bAvailable = true;

            Console.WriteLine("Option 4. Add new bed");

            Console.Write("Enter Ward Type [A/B/C]: ");
            string wardType = Console.ReadLine();
            string[] wardCheck = { "A", "B", "C" };

            if (!wardCheck.Contains(wardType))
            {
                Console.WriteLine("Wrong input");
                return;
            }

            Console.Write("Enter Ward No.: ");
            string wardNo = Console.ReadLine();

            if (!Int32.TryParse(wardNo, out int iwardNo))
            {
                Console.WriteLine("Wrong input");
                return;
            }

            Console.Write("Enter Bed No.: ");
            string bedNo = Console.ReadLine();

            if (!Int32.TryParse(bedNo, out int ibedNo))
            {
                Console.WriteLine("Wrong input");
                return;
            }

            Console.Write("Enter Daily Rate: ");
            string dailyRate = Console.ReadLine();

            if (!double.TryParse(dailyRate, out double iDailyRate))
            {
                Console.WriteLine("Wrong input");
                return;
            }

            Console.Write("Enter Available: [Y/N]");
            string available = Console.ReadLine().ToUpper();
            string[] availability = { "Y", "N" };
            if (!availability.Contains(available))
            {
                Console.WriteLine("Wrong input");
                return;
            }

            if (available == "Y") bAvailable = true;
            else if (available == "N") bAvailable = false;



            AddBed(bedList, wardType, iwardNo, ibedNo, iDailyRate, bAvailable);
        } // option 4 input

        private static void AddBed(List<Bed> bedList, string wardType, int wardNo, int bedNo, double dailyRate, bool available) 
        {

         
            //Add data to list
            Bed bed = null;
            if (wardType == "A")
            {
                bed = new ClassABed(wardNo, bedNo, dailyRate, available);
            }
            else if (wardType == "B")
            {
                bed = new ClassBBed(wardNo, bedNo, dailyRate, available);
            }
            else if (wardType == "C")
            {
                bed = new ClassCBed(wardNo, bedNo, dailyRate, available);
            }

            //Check if ward number is existed
            //get wardObject
            foreach (Bed item in bedList)
            {
                if(item.WardNo == bed.WardNo && item.BedNo == bed.BedNo && item.GetType().ToString() == bed.GetType().ToString())
                {
                    Console.WriteLine("Bed is already existed!");
                    Console.ReadKey();
                    return;
                }
            }

            bedList.Add(bed);

            string bAvailable = null;
            if (available == true) bAvailable = "Yes";
            else if (available == false) bAvailable = "No";
            //Add data to excel file
            using (StreamWriter file = new StreamWriter("beds.csv", true))
            {
                string data = "\n" + wardType + "," + wardNo.ToString() + "," + bedNo.ToString() + "," + bAvailable + "," + dailyRate.ToString();
                file.Write(data);
                file.Close();
            }

            Console.WriteLine("Added Successfully");
        }

        private static void RegisterStay(List<Patient> pList, List<Bed> bList) // option 5
        {
            Console.WriteLine("Option 5. Register a hospital stay");
            List<string> statuses = new List<string> { "Registered", "Discharged" };

            (Patient patient, Bed bed, DateTime bedStayDate) = ChangeBedStay(pList, bList, statuses);

            Stay stay = new Stay(bedStayDate, patient);
            BedStay bedStay = new BedStay(bedStayDate, bed);
            stay.AddBedStay(bedStay);
            patient.Stay = stay;
            patient.Status = "Admitted";
            bed.Available = false;
            Console.WriteLine("Stay registration successful!");
        }

        private static void RetrievePatient(List<Patient> pList) // option 6
        {
            Console.WriteLine("Option 6. Retrieve Patient Details");
            ListPatients(pList, null);

            bool boolean = true; string id = ""; Patient patient = null;
            do
            {
                if (boolean == false)
                {
                    Console.Write("Patient does not exist! Type 'X' to exit.\n");
                }
                Console.Write("Enter patient ID number: ");
                id = Console.ReadLine();
                if (id == "X") Environment.Exit(0);
                    patient = FindPatient(pList, id);
                if (patient is null) boolean = false;
                else boolean = true;
            } while (boolean == false);

            RetrievePatientInfo(patient);
            Console.WriteLine();
            RetrieveAdmiDis(patient);

            if (patient.Stay != null)
            {
                if (patient.Stay.IsPaid)
                {
                    Console.WriteLine("Payment status: Paid");
                }
                else
                {
                    Console.WriteLine("Payment status: Unpaid");
                }

                if (patient.Stay.BedStayList != null)
                {
                    foreach (BedStay b in patient.Stay.BedStayList)
                    {
                        Console.WriteLine("======");
                        RetrieveBedStay(b);
                    }
                }
                else
                {
                    Console.WriteLine("No Bed Stay records found");
                }
            }
        }

        private static void AddMedicalRecord(List<Patient> pList, List<string> statuses) // option 7
        {
            Console.WriteLine();
            Console.WriteLine("Option 7. Add Medical Record Entry");

            ListPatients(pList, statuses);
            Console.Write("Enter patient ID number: ");
            string id = Console.ReadLine();

            Patient patient = FindPatient(pList, id); //Retrieve Patient

            if (patient == null)
            {
                Console.WriteLine("Patient not found");
                Console.ReadKey();
                return;
            }

            if (patient.Status != "Admitted")
            {
                Console.WriteLine("Patient is not admitted!");
                Console.ReadKey();
                return;
            }

            Console.WriteLine();
            Console.Write("Patient temperature: "); //Prompt for patient temperature

            string sTemp = Console.ReadLine();

            if (!double.TryParse(sTemp, out double temp))
            {
                Console.WriteLine("Invalid Temperature");
                Console.ReadKey();
                return;
            }

            Console.Write("Please enter patient observation: ");
            string description = Console.ReadLine();

            DateTime dateTime = DateTime.Now;
            MedicalRecord medicalRecord = new MedicalRecord(description, temp, dateTime);
            patient.Stay.AddMedicalRecord(medicalRecord);
            Console.WriteLine();
            Console.WriteLine("Medical record entry for {0} successful!", patient.Name);
        }

        private static void ViewMedicalRecords(List<Patient> patientList) // option 8 
        {
            Console.WriteLine("");
            Console.WriteLine("Option 8. View medical records");
            List<string> statuses = new List<string> { "Admitted" };

            ListPatients(patientList, statuses);
            Console.Write("Enter patient ID number: ");
            string id = Console.ReadLine();
            Patient patient = FindPatient(patientList, id);
            if (patient == null)
            {
                Console.WriteLine("Patient ID not found");
                return;
            }
            Console.WriteLine();

            RetrievePatientInfo(patient);
            Console.WriteLine();
            Console.WriteLine("====Stay====");
            RetrieveAdmiDis(patient);

            RetrieveRecord(patient);
        }

        private static void TransferPatientBed(List<Patient> pList, List<Bed> bList) // option 9
        {
            Console.WriteLine("Option 9. Transfer Patient to Another Bed");
            List<string> statuses = new List<string> { "Admitted" };

            (Patient patient, Bed bed, DateTime bedStayDate) = ChangeBedStay(pList, bList, statuses);

            int index = patient.Stay.BedStayList.Count - 1;
            patient.Stay.BedStayList[index].EndBedStay = bedStayDate;
            patient.Stay.BedStayList[index].Bed.Available = true;
            
            BedStay bedStay = new BedStay(bedStayDate, bed);
            patient.Stay.AddBedStay(bedStay);
            bed.Available = false;
            Console.WriteLine(patient.Name + " will be transferred to Ward " + bed.WardNo + " Bed " + bed.BedNo + " on " + bedStayDate + ".");
        }

        private static void DischargePayment(List<Patient> patientList, List<Bed> bedList) // option 10
        {
            Console.WriteLine();
            Console.WriteLine("Option 10. Discharge and Payment");
            List<string> statuses = new List<string> { "Admitted" };
            ListPatients(patientList, statuses);
            Console.Write("Enter patient ID number to discharge: ");
            string id = Console.ReadLine();
            Patient patient = FindPatient(patientList, id);
            if (patient == null)
            {
                Console.WriteLine("Patient not found!");
                Console.ReadLine();
                return;
            }

            if(patient.Status != "Admitted")
            {
                Console.WriteLine("{0} is not admitted", patient.Name);
                return;
            }

            Console.Write("Date of discharge (DD/MM/YYYY): ");
            string sdischargeDate = Console.ReadLine();

            if (!DateTime.TryParse(sdischargeDate, out DateTime dischargeDate))
            {
                Console.WriteLine("Invalid Date");
                Console.ReadLine();
                return;
            }

            if (patient.Stay.AdmittedDate > dischargeDate)
            {
                Console.WriteLine("Error! Date entered before admitted date");
                Console.ReadLine();
                return;
            }

            //Patietn records in discharge
            patient.Stay.DischargeDate = dischargeDate.Date;

            int count = patient.Stay.BedStayList.Count();
            int bedNo = patient.Stay.BedStayList[count - 1].Bed.BedNo;
            patient.Stay.BedStayList[count-1].EndBedStay = dischargeDate.Date;
            patient.Stay.BedStayList[count - 1].Bed.Available = true;

            bedList[SearchBed(bedList, patient.Stay.BedStayList[count-1].Bed)].Available = true;

            RetrievePatientInfo(patient);

            Console.WriteLine("====Stay====");
            RetrieveAdmiDis(patient);

            if (!patient.Stay.IsPaid)
            {
                Console.WriteLine("Payment status: Unpaid");
            }

            int i = 0;
            foreach (BedStay b in patient.Stay.BedStayList)
            {

                i += 1;

                Console.WriteLine("\n====Bed # {0}===", i);

                RetrieveBedStay(b);


                if (GetBedType(b.Bed) == "A")
                {
                    ClassABed classA = (ClassABed)b.Bed;
                    Console.WriteLine("Accompanying person: {0}", classA.AccompanyingPerson);
                }
                else if (GetBedType(b.Bed) == "B")
                {
                    ClassBBed classB = (ClassBBed)b.Bed;
                    Console.WriteLine("AirCon: {0}", classB.AirCon);
                }
                else if (GetBedType(b.Bed) == "C")
                {
                    ClassCBed classC = (ClassCBed)b.Bed;
                    Console.WriteLine("Portable Tv: {0}", classC.PortableTv);
                }
                Console.WriteLine();
                Console.WriteLine("Number of days stayed: {0}", (Convert.ToDateTime(b.EndBedStay) - b.StartBedStay).Days + 1);
            }

            DisplayPayment(patient);
        }

        private static void CurrencyAPI() // option 11
        {
            //https://api.exchangeratesapi.io/latest?base=SGD
            
            Currency currency;
            Console.WriteLine("Option 11. Display currencies exchange rate");
            string response = "/latest?base=SGD";
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://api.exchangeratesapi.io");
                    Task<HttpResponseMessage> responseTask = client.GetAsync(response);
                    responseTask.Wait();
                    HttpResponseMessage result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        Task<string> readTask = result.Content.ReadAsStringAsync();
                        readTask.Wait();
                        string data = readTask.Result;
                        currency = JsonConvert.DeserializeObject<Currency>(data);
                        Console.WriteLine("1 SGD can be exchanged for: ");
                        foreach (var property in currency.rates.GetType().GetProperties())
                        {
                            Console.WriteLine(property.Name + ": " + Convert.ToDouble(property.GetValue(currency.rates)).ToString("#,##0.00"));
                        }
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Unable to get currency information. Did you not connnect to the Internet?");
            }
        }

        private static async Task<PM25API> getPM25API() // option 12
        {
            using (var client = new HttpClient())
            {
                PM25API pM25 = null;
                client.BaseAddress = new Uri("https://api.data.gov.sg/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //GET Method
                HttpResponseMessage response = await client.GetAsync("v1/environment/pm25");

                if (response.IsSuccessStatusCode)
                {
                    Task<string> readTask = response.Content.ReadAsStringAsync();
                    readTask.Wait();
                    string data = readTask.Result;
                    pM25 = JsonConvert.DeserializeObject<PM25API>(data);
                }
                else
                {
                    Console.WriteLine("Error, Please try again later!");
                    return null;
                }
                return pM25;
            }
        }

        private static (Patient patient, Bed bed, DateTime bedStayDate) ChangeBedStay(List<Patient> pList, List<Bed> bList, List<string> statuses) // Modifies bed stay
        {
            ListPatients(pList, statuses);

            bool boolean = true; string id = ""; Patient patient = null;
            do
            {
                if (boolean == false)
                {
                    Console.Write("Patient does not exist! Type 'X' to exit.\n");
                }
                Console.Write("Enter patient ID number: ");
                id = Console.ReadLine();
                if (id == "X") Environment.Exit(0);
                patient = FindPatient(pList, id);
                if (patient is null) boolean = false;
                else boolean = true;
            } while (boolean == false);

            ListBeds(bList);
            string ibedNo = ""; int bedNo = 0; Bed bed = null;
            do
            {
                if (boolean == false)
                {
                    Console.Write("Bed is not available! Type 'X' to exit.\n");
                }
                Console.Write("Enter bed to stay: ");
                ibedNo = Console.ReadLine();
                if (ibedNo == "X") Environment.Exit(0);
                else if (!int.TryParse(ibedNo, out bedNo)) boolean = false;
                else
                {
                    bed = bList[bedNo - 1];
                    if (bed.Available == false) boolean = false;
                    else boolean = true;
                }
            } while (boolean == false);

            string ibedStayDate = ""; DateTime bedStayDate = new DateTime();
            do
            {
                if (boolean == false)
                {
                    Console.Write("Date of bed stay is wrong! Type 'X' to exit.\n");
                }
                Console.Write("Enter date of bed stay (DD/MM/YYYY): ");
                ibedStayDate = Console.ReadLine();
                if (ibedStayDate == "X") Environment.Exit(0);
                else if (!DateTime.TryParse(ibedStayDate, out bedStayDate)) boolean = false;
                else boolean = true;
            } while (boolean == false);

            string[] choiceRange = { "Y", "y", "N", "n" };

            if (bed is ClassABed)
            {
                string accompanyingPerson = "";
                do
                {
                    if (boolean == false)
                    {
                        Console.Write("Input error! Type 'X' to exit.\n");
                    }
                    Console.Write("Any accompanying guest? (Additional $100 per day) [Y/N]: ");
                    accompanyingPerson = Console.ReadLine();
                    if (accompanyingPerson == "X") Environment.Exit(0);
                    else if (!choiceRange.Contains(accompanyingPerson)) boolean = false;
                    else boolean = true;
                } while (boolean == false);

                ClassABed a = (ClassABed)bed;
                if (accompanyingPerson == "Y" || accompanyingPerson == "y") a.AccompanyingPerson = true;
                else if (accompanyingPerson == "N" || accompanyingPerson == "n") a.AccompanyingPerson = false;
            }
            else if (bed is ClassBBed)
            {
                string airCon = "";
                do
                {
                    if (boolean == false)
                    {
                        Console.Write("Input error! Type 'X' to exit.\n");
                    }
                    Console.Write("Any air-conditioned variant? (Additional $50 per week) [Y/N]: ");
                    airCon = Console.ReadLine();
                    if (airCon == "X") Environment.Exit(0);
                    else if (!choiceRange.Contains(airCon)) boolean = false;
                    else boolean = true;
                } while (boolean == false);

                ClassBBed b = (ClassBBed)bed;
                if (airCon == "Y" || airCon == "y") b.AirCon = true;
                else if (airCon == "N" || airCon == "n") b.AirCon = false;
            }
            else if (bed is ClassCBed)
            {
                string portableTv = "";
                do
                {
                    if (boolean == false)
                    {
                        Console.Write("Input error! Type 'X' to exit.\n");
                    }
                    Console.Write("Any portable TV? (Additional $30) [Y/N]: ");
                    portableTv = Console.ReadLine();
                    if (portableTv == "X") Environment.Exit(0);
                    else if (!choiceRange.Contains(portableTv)) boolean = false;
                    else boolean = true;
                } while (boolean == false);

                ClassCBed c = (ClassCBed)bed;
                if (portableTv == "Y" || portableTv == "y") c.PortableTv = true;
                else if (portableTv == "N" || portableTv == "n") c.PortableTv = false;
            }
            return (patient, bed, bedStayDate);
        }

        private static Patient FindPatient(List<Patient> pList, string id) //Finds patient object
        {
            Patient patient = null;
            foreach (Patient p in pList)
            {
                if (p.Id == id)
                {
                    patient = p;
                    return patient;
                }
            }
            return null;
        }

        private static void RetrievePatientInfo(Patient patient) //Gets patient information
        {
            Console.WriteLine("Name of patient: " + patient.Name);
            Console.WriteLine("ID number: " + patient.Id);
            Console.WriteLine("Citizenship status: " + patient.CitizenStatus);
            Console.WriteLine("Gender: " + patient.Gender.ToString());
            Console.WriteLine("Status: " + patient.Status);
        }

        private static void RetrieveAdmiDis(Patient patient) //Gets admission date and discharge date
        {
            if (patient.Stay != null)
            {
                Console.WriteLine("Admission date: " + patient.Stay.AdmittedDate.ToShortDateString());
                string discharge;
                if (patient.Stay.DischargeDate == null) discharge = "";
                else discharge = patient.Stay.DischargeDate.Value.ToShortDateString();
                Console.WriteLine("Discharge date: " + discharge);
            }
            else Console.WriteLine("No Records");
        }

        private static void RetrieveBedStay(BedStay b) //Display bed stay information
        {
            string endstay;

            Console.WriteLine("Ward number: " + b.Bed.WardNo.ToString());
            Console.WriteLine("Bed number: " + b.Bed.BedNo.ToString());
            Console.WriteLine("Ward class: " + GetBedType(b.Bed));
            Console.WriteLine("Start of bed stay: " + b.StartBedStay.ToShortDateString());
            if (b.EndBedStay == null) endstay = "";
            else endstay = b.EndBedStay.Value.ToShortDateString();
            Console.WriteLine("End of bed stay: " + endstay);
 
        }

        private static void RetrieveRecord(Patient patient) //Display medical record information
        {
            int i = 0;

            if (patient.Stay != null)
            {
                if (patient.Stay.MedicalRecordList  != null)
                {
                    foreach (MedicalRecord medical in patient.Stay.MedicalRecordList)
                    {
                        i += 1;
                        Console.WriteLine("=====Record # {0}=====", i);
                        Console.WriteLine("Date/Time: {0} / {1}", medical.DatetimeEntered.ToShortDateString(), medical.DatetimeEntered.ToShortTimeString());
                        Console.WriteLine("Temperature: {0} deg.cel.", medical.Temperature);
                        Console.WriteLine("Diagnosis: {0}", medical.Diagnosis);
                    }
                }
                else Console.WriteLine("No Records Found");

            }
            else Console.WriteLine("No Stay Records Found");
        }

        private static string GetBedType(Bed bed) //Returns bed type
        {
            if (bed.GetType() == typeof(ClassABed)) return "A";
            else if (bed.GetType() == typeof(ClassBBed)) return "B";
            else return "C";
        }

        private static void DisplayPayment(Patient patient) //Display payment information
        {
            string type = ""; //Type of payment

            if (patient.GetType() == typeof(Child))
            {
                Child child = (Child)patient;
                type = "CDA";
                Console.WriteLine("Total charges pending: ${0}", child.CalculateCharges()) ;

                double cdaDeduct = 0;
                double subtotal = 0;
                if (child.CitizenStatus == "SC") //Then deduct from CDA
                {
                    Console.WriteLine("CDA balance: ${0}", child.CdaBalance);

                    if (child.CalculateCharges() < child.CdaBalance)
                    {
                        cdaDeduct = child.CalculateCharges();
                        subtotal = 0;
                    }
                    else
                    {
                        cdaDeduct = child.CdaBalance;
                        subtotal = child.CalculateCharges() - child.CdaBalance;
                    }

                    Console.WriteLine("To deduct from CDA: ${0}", cdaDeduct);
                    Console.WriteLine("Sub-total: ${0}", subtotal);

                    Console.WriteLine("[Press any key to proceed with payment]");
                    Console.ReadKey();
                    Console.WriteLine("\nCommencing Payment...\n");

                    child.CdaBalance = child.CdaBalance - cdaDeduct; //Deduct from CDA
                    Console.WriteLine("$ {0} has been deducted from {1}", cdaDeduct, type);
                    Console.WriteLine("New {0} balance: ${1}", type, child.CdaBalance);

                    if (subtotal != 0) Console.WriteLine("Sub-total: ${0} has been paid by cash", child.CalculateCharges() - cdaDeduct);
                }
                else
                {
                    Console.WriteLine("[Press any key to proceed with payment]");
                    Console.ReadKey();
                    Console.WriteLine("\nCommencing Payment...\n");
                    Console.WriteLine("Sub-total: ${0} has been paid by cash", child.CalculateCharges());
                }//Else skip the deduction portion

            }
            else if (patient.GetType() == typeof(Adult))
            {
                Adult adult = (Adult)patient;
                type = "MediaSave";

                Console.WriteLine("Total charges pending: ${0}", adult.CalculateCharges());

                double amtPaid = adult.CalculateCharges();
                double mediDeduct = 0;
                double subtotal;

                List<string> citizenship = new List<string> { "SC", "PR" };
                if (citizenship.Contains(adult.CitizenStatus)) //Check if its PR or SC cause got subsidy from medisave
                {
                    Console.WriteLine("MediaSave balance: ${0}", adult.MedisaveBalance);

                    if (amtPaid > adult.MedisaveBalance)
                    {
                        mediDeduct = adult.MedisaveBalance;
                        subtotal = amtPaid - mediDeduct;
                    }
                    else
                    {
                        mediDeduct = adult.CalculateCharges();
                        subtotal = 0;
                    }

                    Console.WriteLine("To deduct from Mediasave: ${0}", mediDeduct);
                    Console.WriteLine("Sub-total: ${0}", subtotal);

                    Console.WriteLine("[Press any key to proceed with payment]");
                    Console.ReadKey();
                    Console.WriteLine("\nCommencing Payment...\n");
                    adult.MedisaveBalance = adult.MedisaveBalance - mediDeduct; //Deduct from CDA

                    Console.WriteLine("$ {0} has been deducted from {1}", mediDeduct, type);
                    Console.WriteLine("New {0} balance: ${1}", type, adult.MedisaveBalance);

                    if (subtotal == 0) Console.WriteLine("Sub-total: ${0} has been paid by cash", amtPaid);

                }
                else
                {
                    Console.WriteLine("[Press any key to proceed with payment]");
                    Console.ReadKey();
                    Console.WriteLine("\nCommencing Payment...\n");
                    Console.WriteLine("Sub-total: ${0} has been paid by cash", amtPaid);
                }


                
            }
            else if (patient.GetType() == typeof(Senior))
            {
                Senior senior = (Senior)patient;
                Console.WriteLine("[Press any key to proceed with payment]");
                Console.ReadKey();
                Console.WriteLine("\nCommencing Payment...\n");
                Console.WriteLine("Sub-total: ${0} has been paid by cash", senior.CalculateCharges());
            }

            patient.Stay.IsPaid = true;
            patient.Status = "Discharged";
            Console.WriteLine("Payment sucessful");
        }

        private static string SearchBedType(Bed bed) //Search Bed Type
        {
            if (bed.GetType() == typeof(ClassABed))
            {
                return "A";
            }
            else if (bed.GetType() == typeof(ClassBBed))
            {
                return "B";
            }
            if (bed.GetType() == typeof(ClassCBed))
            {
                return "C";
            }
            return null;
        }

        private static int SearchBed(List<Bed> listBed, Bed bed) //Search Bed Type
        {
            for (int i = 0; i < listBed.Count; i++)
            {
                if (listBed[i].WardNo == bed.WardNo)
                {
                    if (listBed[i].BedNo == bed.BedNo)
                    {
                        return i;
                    }
                }
            }
            return 0;
        }

        private static void displayPM25api(PM25API pM25)
        {
            Console.WriteLine("===============================================");
            Console.WriteLine("=                    PM25                     =");
            Console.WriteLine("=           Retrieve from data.gov            =");
            Console.WriteLine("===============================================");

            Console.WriteLine("Locations available: Central, North, South, East and West");
            Console.Write("Enter location to view pM25 Readings (Type 'All' to view all readings) :");
            String Location = Console.ReadLine();

            Console.WriteLine("\n{0, -10}{1, -10}", "Location", "PM25 Reading");

            if (Location.ToUpper() == "ALL")
            {

                foreach (var item in pM25.items)
                {
                    Console.WriteLine("{0, -10}{1, -10}", "Central ", item.readings.pm25_one_hourly.central);
                    Console.WriteLine("{0, -10}{1, -10}", "East ", item.readings.pm25_one_hourly.east);
                    Console.WriteLine("{0, -10}{1, -10}", "West ", item.readings.pm25_one_hourly.west);
                    Console.WriteLine("{0, -10}{1, -10}", "South ", item.readings.pm25_one_hourly.south);
                    Console.WriteLine("{0, -10}{1, -10}", "North ", item.readings.pm25_one_hourly.north);
                }
            }
            else if (Location.ToUpper() == "CENTRAL")
            {
                Console.WriteLine("{0, -10}{1, -10}", "Central ", pM25.items[0].readings.pm25_one_hourly.central);
            }
            else if (Location.ToUpper() == "EAST")
            {
                Console.WriteLine("{0, -10}{1, -10}", "East ", pM25.items[0].readings.pm25_one_hourly.east);
            }
            else if (Location.ToUpper() == "WEST")
            {
                Console.WriteLine("{0, -10}{1, -10}", "West ", pM25.items[0].readings.pm25_one_hourly.west);
            }
            else if (Location.ToUpper() == "SOUTH")
            {
                Console.WriteLine("{0, -10}{1, -10}", "South ", pM25.items[0].readings.pm25_one_hourly.south);
            }
            else if (Location.ToUpper() == "NORTH")
            {
                Console.WriteLine("{0, -10}{1, -10}", "North ", pM25.items[0].readings.pm25_one_hourly.north);
            }
            else
            {
                Console.WriteLine("Invalid Input");
            }
        }

        private static void initBed(List<Bed> bedsList) //Initialises bed list
        {
            string[] bed = File.ReadAllLines("beds.csv");

            for (int i = 1; i < bed.Length; i++)
            {
                string[] eBed = bed[i].Split(',');
                Bed bed1 = null;
                bool available;

                if (eBed[3] == "Yes") available = true;
                else available = false;

                if (eBed[0] == "A")
                {
                    bed1 = new ClassABed(Convert.ToInt32(eBed[1]), Convert.ToInt32(eBed[2]), Convert.ToInt32(eBed[4]), available);
                }
                else if (eBed[0] == "B")
                {
                    bed1 = new ClassBBed(Convert.ToInt32(eBed[1]), Convert.ToInt32(eBed[2]), Convert.ToInt32(eBed[4]), available);
                }
                else if (eBed[0] == "C")
                {
                    bed1 = new ClassCBed(Convert.ToInt32(eBed[1]), Convert.ToInt32(eBed[2]), Convert.ToInt32(eBed[4]), available);
                }
                bedsList.Add(bed1);
            }
        }

        private static void initPatient(List<Patient> pList)
        {
            string[] patients = File.ReadAllLines("patients.csv");
            for (int i = 1; i < patients.Length; i++)
            {
                string[] patient = patients[i].Split(',');
                string name = patient[0];
                string id = patient[1];
                int age = Convert.ToInt32(patient[2]);
                char gender = Convert.ToChar(patient[3]);
                string citizenStatus = patient[4];
                double subsidiser = 0;

                if (patient.Length == 6)
                {
                    subsidiser = Convert.ToDouble(patient[5]);
                }
                if (age <= 12)
                {
                    Child c = new Child(id, name, age, gender, citizenStatus, "Registered", null, subsidiser);
                    pList.Add(c);
                }
                else if (age < 65)
                {
                    Adult a = new Adult(id, name, age, gender, citizenStatus, "Registered", null, subsidiser);
                    pList.Add(a);
                }
                else
                {
                    Senior s = new Senior(id, name, age, gender, citizenStatus, "Registered", null);
                    pList.Add(s);
                }
            }
        }
    }
}