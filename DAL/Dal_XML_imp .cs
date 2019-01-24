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
            else loadData(configRoot, configPath);

            if (!File.Exists(testerPath))
            {
                testerRoot = new XElement("testers");
                testerRoot.Save(testerPath);
            }
            else loadData(testerRoot, testerPath);

            if (!File.Exists(traineePath))
            {
                traineeRoot = new XElement("trainees");
                traineeRoot.Save(traineePath);
            }
            else loadData(traineeRoot, traineePath);

            if (!File.Exists(testPath))
            {
                testRoot = new XElement("tests");
                testRoot.Save(testPath);
            }
            else loadData(testerRoot, testPath);
            #endregion
        }

        private void loadData(XElement element, string path)
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
        public List<Tester> GetTestersList()
        {
            loadData(testerRoot, testerPath);
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
            loadData(testerRoot, testerPath);
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
            XElement id = new XElement("id", tester.TesterId);
            XElement firstName = new XElement("firstName", tester.FirstName);
            XElement sirName = new XElement("sirName", tester.Sirname);
            XElement name = new XElement("name", firstName, sirName);
            XElement DateOfBirth = new XElement("DateOfBirth", tester.DateOfBirth);
            XElement gender = new XElement("gender", tester.TesterGender);
            XElement PhoneNumber = new XElement("PhoneNumber", tester.PhoneNumber);
            XElement email = new XElement("email", tester.Email);
            XElement car = new XElement("car", tester.Testercar);
            XElement maxDistanceForTest = new XElement("maxDistanceForTest", tester.MaxDistanceForTest);
            XElement yearsOfExperience = new XElement("yearsOfExperience", tester.YearsOfExperience);
            XElement maxTestInAWeek = new XElement("maxTestInAWeek", tester.MaxTestsInaWeek);
            XElement address = new XElement("address", tester.TesterAdress);
            XElement Schedule = new XElement("Schedule", get_schedule(tester._schedual));

            testerRoot.Add(new XElement("student", id, name, DateOfBirth, gender, PhoneNumber, email, car, maxDistanceForTest, yearsOfExperience,
                maxTestInAWeek, address, Schedule));
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
















        //functions for trainee
        public void AddTrainee(Trainee T)
        {
            if (XML.GetAllTraineesFromXml(_traineesXml).Any(t => t. == T.Id))

                throw new Exception("The trainee already exist in the system");



            _traineesXml.Add(XML.ConvertTraineeToXml(newTrainee));

            _traineesXml.Save(Configuration.TraineesXmlFilePath);
        }
        public void DeleteTrainee(Trainee T) { }
        public void UpdateTrainee(Trainee T) { }
        //functions for test
        public void AddTest(Test T) { }
        public void UpdateTest(Test T) { }

        public List<Tester> GetListOfTesters() { return null; }
        public List<Trainee> GetListOfTrainees() { return null; }
        public List<Test> GetListOfTests() { return null; }

    }
}
