using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.IO;
using BE;

namespace DAL
{
    public class FactoryDAL
    {
        public static Idal getDAL(string typeDAL)
        {
            return Dal_XML_imp.Instance;
        }
    }
    public class Dal_XML_imp : Idal
    {
        #region Singleton
        private static readonly Dal_XML_imp instance = new Dal_XML_imp();
        public static Dal_XML_imp Instance
        {
            get { return instance; }
        }

        //private Dal_XML_imp() { }
        static Dal_XML_imp() { }

        #endregion

        XElement configRoot;
        string configPath = @"ConfigXml.xml";
        XElement testerRoot;
        string testerPath = @"TesterXml.xml";
        XElement traineeRoot;
        string traineePath = @"TraineeXml.xml";
        XElement testRoot;
        string testPath = @"TestXml.xml";

        public Dal_XML_imp()
        {
            #region create/load files
            if (!File.Exists(configPath))
            {
                //
            }
            else loadData(ref configRoot, configPath);

            if (!File.Exists(testerPath))
            {
                testerRoot = new XElement("testers");
                testerRoot.Save(testerPath);
            }
            else loadData(ref testerRoot, testerPath);
            loadTesterData();

            if (!File.Exists(traineePath))
            {
                traineeRoot = new XElement("trainees");
                traineeRoot.Save(traineePath);
            }
            else loadData(ref traineeRoot, traineePath);

            if (!File.Exists(testPath))
            {
                testRoot = new XElement("tests");
                testRoot.Save(testPath);
            }
            else loadData(ref testRoot, testPath);
            #endregion
        }
        #region additional function
        private void loadTesterData()
        {
            try { testerRoot = XElement.Load(testerPath); }
            catch { throw new Exception("File upload problem"); }
        }

        private void loadData(ref XElement element, string path)
        {
            try
            {
                element = XElement.Load(path);
            }
            catch
            {
                throw new Exception("File upload problem");
            }
        }

        #endregion
        #region tester
        //to xml
        public void SaveTesterList(List<Tester> testersList)
        {
            testerRoot = new XElement("testers",
                                    from t in testersList
                                    select new XElement("tester",
                                        new XElement("id", t.TesterId),
                                        new XElement("name",
                                            new XElement("firstName", t.FirstName),
                                            new XElement("sirName", t.Sirname)
                                            ),
                                        new XElement("DateOfBirth", t.DateOfBirth),
                                        new XElement("gender", t.TesterGender),
                                        new XElement("PhoneNumber", t.PhoneNumber),
                                        new XElement("email", t.Email),
                                        new XElement("car", t.Testercar),
                                        new XElement("maxDistanceForTest", t.MaxDistanceForTest),
                                        new XElement("yearsOfExperience", t.YearsOfExperience),
                                        new XElement("maxTestInAWeek", t.MaxTestsInaWeek),
                                        new XElement("address", t.TesterAdress),
                                        new XElement("Schedule", get_schedule(t._schedual))
                                        )
                                     );
            testerRoot.Save(testPath);
        }

        public string get_schedule(bool[,] scheduleMatrix)
        {
            if (scheduleMatrix == null) return null;
            string result = "";
            int numOfDays = scheduleMatrix.GetLength(1);
            int numOfHours = scheduleMatrix.GetLength(0);
            result += "" + numOfDays + "," + numOfHours;
            for (int i = 0; i < numOfDays; i++)
            {
                //{
                //    switch(i)
                //    {
                //        case 0:
                //            result += "" + " Sunday:";
                //            break;
                //        case 1:
                //            result += "" + " Monday:";
                //            break;
                //        case 2:
                //            result += "" + " Tuesday:";
                //            break;
                //        case 3:
                //            result += "" + " Wednesday:";
                //            break;
                //        case 4:
                //            result += "" + " Thursday:";
                //            break;
                //    }
                for (int j = 0; j < numOfHours; j++)
                {
                    if (scheduleMatrix[j, i]) result += "," + "true";
                    else result += "," + "false";
                }
            }
            return result;
        }

        //from xml
        public List<Tester> GetListOfTesters()
        {
            //loadData(ref testerRoot, testerPath);
            List<Tester> testers;
            try
            {
                testers = (from t in testerRoot.Elements()
                           select new Tester()
                           {
                               TesterId = t.Element("id").Value,
                               FirstName = t.Element("name").Element("firstName").Value,
                               Sirname = t.Element("name").Element("sirName").Value,
                               DateOfBirth = DateTime.Parse(t.Element("DateOfBirth").Value),
                               //TesterGender = Gender.Parse(t.Element("gender").Value),
                               PhoneNumber = t.Element("PhoneNumber").Value,
                               Email = t.Element("email").Value,
                               //Testercar = CarType.Parse(t.Element("car").Value),
                               MaxDistanceForTest = int.Parse(t.Element("maxDistanceForTest").Value),
                               YearsOfExperience = int.Parse(t.Element("yearsOfExperience").Value),
                               MaxTestsInaWeek = int.Parse(t.Element("maxTestInAWeek").Value),
                               //TesterAdress = Address.Parse(t.Element("address").Value),
                               _schedual = set_schedule(t.Element("schedule").Value)
                           }).ToList();
            }
            catch
            {
                testers = null;
            }
            return testers;
        }

        public Tester GetTester(string id)
        {
            //loadData(testerRoot, testerPath);
            Tester tester;
            try
            {
                tester = (from t in testerRoot.Elements()
                          where t.Element("id").Value == id
                          select new Tester()
                          {
                              TesterId = t.Element("id").Value,
                              FirstName = t.Element("name").Element("firstName").Value,
                              Sirname = t.Element("name").Element("sirName").Value,
                              DateOfBirth = DateTime.Parse(t.Element("DateOfBirth").Value),
                              //TesterGender = Gender.Parse(t.Element("gender").Value),
                              PhoneNumber = t.Element("PhoneNumber").Value,
                              Email = t.Element("email").Value,
                              //Testercar = CarType.Parse(t.Element("car").Value),
                              MaxDistanceForTest = int.Parse(t.Element("maxDistanceForTest").Value),
                              YearsOfExperience = int.Parse(t.Element("yearsOfExperience").Value),
                              MaxTestsInaWeek = int.Parse(t.Element("maxTestInAWeek").Value),
                              //TesterAdress = Address.Parse(t.Element("address").Value),
                              _schedual = set_schedule(t.Element("schedule").Value)
                          }).FirstOrDefault();
            }
            catch
            {
                tester = null;
            }
            return tester;
        }

        public bool[,] set_schedule(string value)
        {
            if (value != null && value.Length > 0)
            {
                string[] values = value.Split(',');
                int numOfDays = int.Parse(values[0]);
                int numOfHours = int.Parse(values[1]);
                bool[,] scheduleMatrix = new bool[numOfHours, numOfDays];
                int index = 2;
                for (int i = 0; i < numOfHours; i++)
                {
                    for (int j = 0; j < numOfDays; j++)
                    {
                        if (values[index] == "true")
                            scheduleMatrix[i, j] = true;
                        else if (values[index] == "false")
                            scheduleMatrix[i, j] = false;
                        index++;
                    }
                }
                return scheduleMatrix;
            }
            return null;
        }

        //functions for tester
        public void AddTester(Tester tester)
        {
            //try { testerRoot = XElement.Load(testerPath); }
            //catch { throw new Exception("File upload problem"); }
            loadData(ref testerRoot, testerPath);
            //loadTesterData();
            var id = new XElement("id", tester.TesterId);
            var firstName = new XElement("firstName", tester.FirstName);
            var sirName = new XElement("sirName", tester.Sirname);
            //var name = new XElement("name", firstName, sirName);
            var DateOfBirth = new XElement("DateOfBirth", tester.DateOfBirth);
            var gender = new XElement("gender", tester.TesterGender);
            var PhoneNumber = new XElement("PhoneNumber", tester.PhoneNumber);
            var email = new XElement("email", tester.Email);
            var car = new XElement("car", tester.Testercar);
            var maxDistanceForTest = new XElement("maxDistanceForTest", tester.MaxDistanceForTest);
            var yearsOfExperience = new XElement("yearsOfExperience", tester.YearsOfExperience);
            var maxTestInAWeek = new XElement("maxTestInAWeek", tester.MaxTestsInaWeek);
            var street = new XElement("street", tester.TesterAdress.Street);
            var buildingNumber = new XElement("buildingNumber", tester.TesterAdress.BuildingNumber);
            var city = new XElement("city", tester.TesterAdress.City);
            var address = new XElement("address", street, buildingNumber, city);

            var Schedule = new XElement("Schedule", get_schedule(tester._schedual));

            XElement finalTester = new XElement("tester", id, firstName, sirName, DateOfBirth, gender, PhoneNumber, email, car, maxDistanceForTest,
                yearsOfExperience, maxTestInAWeek, address, Schedule);

            testerRoot.Add(finalTester);
            testerRoot.Save(testerPath);
        }

        public void DeleteTester(Tester tester)
        {
            string id = tester.TesterId;
            XElement testerElement;
            try
            {
                testerElement = (from t in testerRoot.Elements()
                                 where t.Element("id").Value == id
                                 select t).FirstOrDefault();
                testerElement.Remove();
                testerRoot.Save(testerPath);
            }
            catch
            {
                throw new Exception("problem deleting tester");
            }
        }

        public void UpdateTester(Tester tester)
        {
            XElement testerElement = (from t in testerRoot.Elements()
                                      where t.Element("id").Value == tester.TesterId
                                      select t).FirstOrDefault();
            testerElement.Element("name").Element("firstName").Value = tester.FirstName;
            testerElement.Element("name").Element("sirName").Value = tester.Sirname;
            testerElement.Element("DateOfBirth").Value = tester.DateOfBirth.ToString();
            testerElement.Element("gender").Value = tester.TesterGender.ToString();
            testerElement.Element("PhoneNumber").Value = tester.PhoneNumber;
            testerElement.Element("email").Value = tester.Email;
            testerElement.Element("car").Value = tester.Testercar.ToString();
            testerElement.Element("maxDistanceForTest").Value = tester.MaxDistanceForTest.ToString();
            testerElement.Element("yearsOfExperience").Value = tester.YearsOfExperience.ToString();
            testerElement.Element("maxTestInAWeek").Value = tester.MaxTestsInaWeek.ToString();
            testerElement.Element("address").Value = tester.TesterAdress.ToString();
            testerElement.Element("schedule").Value = get_schedule(tester._schedual);

            testerRoot.Save(testerPath);
        }
        #endregion































        #region trainee
        public void AddTrainee(Trainee trainee)
        {
            var id = new XElement("id", trainee.TraineeId);
            var firstName = new XElement("firstName", trainee.FirstName);
            var sirName = new XElement("sirName", trainee.Sirname);
            //var name = new XElement("name", firstName, sirName);
            var DateOfBirth = new XElement("DateOfBirth", trainee.DateOfBirth);
            var gender = new XElement("gender", trainee.TraineeGender);
            var PhoneNumber = new XElement("PhoneNumber", trainee.PhoneNumber);
            var email = new XElement("email", trainee.Email);
            var DrivingSchool = new XElement("DrivingSchool", trainee.DrivingSchool);
            var DrivingTeacher = new XElement("DrivingTeacher", trainee.DrivingTeacher);
            var car = new XElement("car", trainee.Traineecar);
            var Gear = new XElement("Gear", trainee.TraineeGear);
            var street = new XElement("street", trainee.TraineeAddress.Street);
            var buildingNumber = new XElement("buildingNumber", trainee.TraineeAddress.BuildingNumber);
            var city = new XElement("city", trainee.TraineeAddress.City);
            var address = new XElement("address", street, buildingNumber, city);
            var LessonsPassed = new XElement("LessonsPassed", trainee.LessonsPassed);

            XElement final = new XElement("trainee", id, firstName, sirName, DateOfBirth, gender, PhoneNumber, email, DrivingSchool, DrivingTeacher, car, Gear, address, LessonsPassed);
            testerRoot.Add(final);
            testerRoot.Save(testerPath);
        }

        public void UpdateTrainee(Trainee trainee)
        {
            XElement traineeElement = (from t in traineeRoot.Elements()
                                       where t.Element("id").Value == trainee.TraineeId
                                       select t).FirstOrDefault();
            traineeElement.Element("name").Element("firstName").Value = trainee.FirstName;
            traineeElement.Element("name").Element("sirName").Value = trainee.Sirname;
            traineeElement.Element("DateOfBirth").Value = trainee.DateOfBirth.ToString();
            traineeElement.Element("gender").Value = trainee.TraineeGender.ToString();
            traineeElement.Element("PhoneNumber").Value = trainee.PhoneNumber;
            traineeElement.Element("email").Value = trainee.Email;
            traineeElement.Element("DrivingSchool").Value = trainee.DrivingSchool;
            traineeElement.Element("DrivingTeacher").Value = trainee.DrivingTeacher;
            traineeElement.Element("car").Value = trainee.Traineecar.ToString();
            traineeElement.Element("Gear").Value = trainee.TraineeGear.ToString();
            traineeElement.Element("Address").Value = trainee.TraineeAddress.ToString();
            traineeElement.Element("LessonsPassed").Value = trainee.LessonsPassed.ToString();

            traineeRoot.Save(traineePath);
        }

        public void DeleteTrainee(Trainee trainee)
        {
            string id = trainee.TraineeId;
            XElement traineeElement;
            try
            {
                traineeElement = (from t in traineeRoot.Elements()
                                  where t.Element("id").Value == id
                                  select t).FirstOrDefault();
                traineeElement.Remove();
                testerRoot.Save(testerPath);
            }
            catch
            {
                throw new Exception("problem deleting trainee");
            }
        }

        public List<Trainee> GetListOfTrainees()
        {
            FileStream file = new FileStream(traineePath, FileMode.Open);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Trainee>));
            List<Trainee> list = (List<Trainee>)xmlSerializer.Deserialize(file);
            file.Close();
            if (list == null) return new List<Trainee>();
            return list;
        }
        #endregion

        #region test
        public void AddTest(Test test)
        {
            var TestId = new XElement("TestId", test.TestId);
            var TesterId = new XElement("TesterId", test.TesterId);
            var TraineeId = new XElement("TraineeId", test.TraineeId);
            var TestDate = new XElement("TestDate", test.TestDate);
            var DateAndHourOfTest = new XElement("DateAndHourOfTest", test.DateAndHourOfTest);
            var CarType = new XElement("CarType", test.CarType);
            var StartingPoint = new XElement("StartingPoint", test.StartingPoint);
            var KeptDistance = new XElement("KeptDistance", test.KeptDistance);
            var Parking = new XElement("Parking", test.Parking);
            var ReverseParking = new XElement("ReverseParking", test.ReverseParking);
            var CheckMirrors = new XElement("CheckMirrors", test.CheckMirrors);
            var UsedSignal = new XElement("UsedSignal", test.UsedSignal);
            var KeptRightofPresidence = new XElement("KeptRightofPresidence", test.KeptRightofPresidence);
            var StoppedAtRed = new XElement("StoppedAtRed", test.StoppedAtRed);
            var StoppedAtcrossWalk = new XElement("StoppedAtcrossWalk", test.StoppedAtcrossWalk);
            var RightTurn = new XElement("RightTurn", test.RightTurn);
            var ImediateStop = new XElement("ImediateStop", test.ImediateStop);
            var TestPassed = new XElement("TestPassed", test.TestPassed);
            var RemarksOnTest = new XElement("StoppedAtcrossWalk", test.RemarksOnTest);

            XElement final = new XElement("trainee", TestId, TesterId, TraineeId, TestDate, DateAndHourOfTest, CarType, StartingPoint, KeptDistance, Parking, ReverseParking,
                CheckMirrors, UsedSignal, KeptRightofPresidence, StoppedAtRed, StoppedAtcrossWalk, RightTurn, ImediateStop, TestPassed, RemarksOnTest);
            testRoot.Add(final);
            testRoot.Save(testPath);
        }
        #endregion



















        //functions for trainee
        //public void AddTrainee(Trainee T) { }
        //public void DeleteTrainee(Trainee T) { }
        //public void UpdateTrainee(Trainee T) { }
        //functions for test
        //public void AddTest(Test T) { }
        public void UpdateTest(Test T) { }

        //public List<Tester> GetListOfTesters() { return null; }
        //public List<Trainee> GetListOfTrainees() { return null; }
        public List<Test> GetListOfTests() { return null; }

    }
}
