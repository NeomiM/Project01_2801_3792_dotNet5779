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
            //else loadData(configRoot, configPath);

            if (!File.Exists(testerPath))
            {
                testerRoot = new XElement("testers");
                testerRoot.Save(testerPath);
            }
            else loadData(ref testerRoot, testerPath);

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

        #region tester
        //to xml
        public void SaveTesterList(List<Tester> testersList)
        {
            testerRoot = new XElement("testers",
                                    from t in testersList
                                    select new XElement("tester",
                                        new XElement("id", t.TesterId),
                                        new XElement("firstName", t.FirstName),
                                        new XElement("sirName", t.Sirname),
                                        new XElement("dateOfBirth", t.DateOfBirth),
                                        new XElement("gender", t.TesterGender),
                                        new XElement("phoneNumber", t.PhoneNumber),
                                        new XElement("email", t.Email),
                                        new XElement("car", t.Testercar),
                                        new XElement("maxDistanceForTest", t.MaxDistanceForTest),
                                        new XElement("yearsOfExperience", t.YearsOfExperience),
                                        new XElement("maxTestInAWeek", t.MaxTestsInaWeek),

                                        new XElement("address", new XElement("street"), new XElement("buildingNumber"), new XElement("city")),

                                        //new XElement("schedule", get_schedule(t._schedual))
                                        new XElement("schedule", new XElement("sunday", get_schedule(t._schedual, 0)),
                                            new XElement("monday", get_schedule(t._schedual, 1)),
                                            new XElement("tuesday", get_schedule(t._schedual, 2)),
                                            new XElement("wednesday", get_schedule(t._schedual, 3)),
                                            new XElement("thursday", get_schedule(t._schedual, 4)))
                                        )
                                     );
            testerRoot.Save(testPath);
        }

        //public string get_schedule(bool[,] scheduleMatrix, int day)
        //{
        //    string result = "";
        //    for (int i = 0; i < Configuration.NumOfHoursPerDay; i++)
        //    {
        //        if (scheduleMatrix[i, day]) result += "true";
        //        else result += "false";
        //        if (i != Configuration.NumOfHoursPerDay - 1) result += ",";
        //    }
        //    return result;
        //}

        public string get_schedule(bool[,] scheduleMatrix, int day)
        {
            bool started = false;
            bool finished = false;
            bool empty = true;
            string start = "";
            string end = "";
            string result = "";
            for (int i = 0; i < Configuration.NumOfHoursPerDay; i++)
            {
                switch (i)
                {
                    case 0:
                        if (scheduleMatrix[i, day])
                        {
                            start = "09:00-";
                            end = "10:00";
                            started = true;
                        }
                        break;

                    case 1:
                        if (scheduleMatrix[i, day])
                        {
                            if (started)
                            {
                                end = "11:00";
                            }
                            else
                            {
                                start = "10:00-";
                                end = "11:00";
                                started = true;
                            }
                        }
                        else
                        {
                            if (result == "" && start != "")
                            {
                                result = start + end;
                                start = "";
                                end = "";
                                started = false;
                                //empty = false;
                            }
                            else
                            {
                                if (start != "")
                                {
                                    result += ",";
                                    result += start;
                                    result += end;
                                    start = "";
                                    end = "";
                                    started = false;
                                    //empty = false;
                                }
                            }
                        }
                        break;

                    case 2:
                        if (scheduleMatrix[i, day])
                        {
                            if (started)
                            {
                                end = "12:00";
                            }
                            else
                            {
                                start = "11:00-";
                                end = "12:00";
                                started = true;
                            }
                        }
                        else
                        {
                            if (result == "" && start != "")
                            {
                                result = start + end;
                                start = "";
                                end = "";
                                started = false;
                                //empty = false;
                            }
                            else
                            {
                                if (start != "")
                                {
                                    result += ",";
                                    result += start;
                                    result += end;
                                    start = "";
                                    end = "";
                                    started = false;
                                    //empty = false;
                                }
                            }
                        }
                        break;

                    case 3:
                        if (scheduleMatrix[i, day])
                        {
                            if (started)
                            {
                                end = "13:00";
                            }
                            else
                            {
                                start = "12:00-";
                                end = "13:00";
                                started = true;
                            }
                        }
                        else
                        {
                            if (result == "" && start != "")
                            {
                                result = start + end;
                                start = "";
                                end = "";
                                started = false;
                                //empty = false;
                            }
                            else
                            {
                                if (start != "")
                                {
                                    result += ",";
                                    result += start;
                                    result += end;
                                    start = "";
                                    end = "";
                                    started = false;
                                    //empty = false;
                                }
                            }
                        }
                        break;

                    case 4:
                        if (scheduleMatrix[i, day])
                        {
                            if (started)
                            {
                                end = "14:00";
                            }
                            else
                            {
                                start = "13:00-";
                                end = "14:00";
                                started = true;
                            }
                        }
                        else
                        {
                            if (result == "" && start != "")
                            {
                                result = start + end;
                                start = "";
                                end = "";
                                started = false;
                                //empty = false;
                            }
                            else
                            {
                                if (start != "")
                                {
                                    result += ",";
                                    result += start;
                                    result += end;
                                    start = "";
                                    end = "";
                                    started = false;
                                    //empty = false;
                                }
                            }
                        }
                        break;

                    case 5:
                        if (scheduleMatrix[i, day])
                        {
                            if (started)
                            {
                                end = "15:00";
                            }
                            else
                            {
                                start = "14:00-";
                                end = "15:00";
                                started = true;
                            }
                        }
                        else
                        {
                            if (result == "" && start != "")
                            {
                                result = start + end;
                                start = "";
                                end = "";
                                started = false;
                                //empty = false;
                            }
                            else
                            {
                                if (start != "")
                                {
                                    result += ",";
                                    result += start;
                                    result += end;
                                    start = "";
                                    end = "";
                                    started = false;
                                    //empty = false;
                                }
                            }
                        }
                        if (start != "")
                        {
                            if (result == "")
                                result = start + end;
                            else
                            {
                                result += ",";
                                result += start;
                                result += end;
                            }
                        }
                        break;

                }
            }
            return result;
        }


        //from xml
        public List<Tester> GetListOfTesters()
        {
            var testers = new List<Tester>();
            try
            {
                testers = (from t in testerRoot.Elements()
                           select new Tester()
                           {
                               TesterId = t.Element("id").Value,
                               FirstName = t.Element("firstName").Value,
                               Sirname = t.Element("sirName").Value,
                               DateOfBirth = DateTime.Parse(t.Element("dateOfBirth").Value),
                               //TesterGender = Gender.Parse(t.Element("gender").Value),
                               PhoneNumber = t.Element("phoneNumber").Value,
                               Email = t.Element("email").Value,
                               //Testercar = CarType.Parse(t.Element("car").Value),
                               MaxDistanceForTest = int.Parse(t.Element("maxDistanceForTest").Value),
                               YearsOfExperience = int.Parse(t.Element("yearsOfExperience").Value),
                               MaxTestsInaWeek = int.Parse(t.Element("maxTestInAWeek").Value),
                               //TesterAdress = new Address(t.Element("address").Element("street").Value,
                               //     t.Element("address").Element("BuildingNumber").Value, t.Element("address").Element("city").Value),
                               _schedual = set_schedule(t.Element("schedule").Element("sunday").Value, t.Element("schedule").Element("monday").Value, t.Element("schedule").Element("tuesday").Value,
                                    t.Element("schedule").Element("wednesday").Value, t.Element("schedule").Element("thursday").Value)
                           }).ToList();
            }
            catch (Exception ex)
            {
                testers = null;
            }
            return testers;
        }

        public Tester GetTester(string id)
        {
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
                              //Testercar = (Types.CarType)int.Parse(t.Element("car").Value),
                              MaxDistanceForTest = int.Parse(t.Element("maxDistanceForTest").Value),
                              YearsOfExperience = int.Parse(t.Element("yearsOfExperience").Value),
                              MaxTestsInaWeek = int.Parse(t.Element("maxTestInAWeek").Value),
                              TesterAdress = new Address(t.Element("address").Element("strret").Value,
                                    t.Element("address").Element("buildingNumber").Value,
                                    t.Element("address").Element("city").Value),
                              _schedual = set_schedule(t.Element("schedule").Element("sunday").Value, t.Element("schedule").Element("monday").Value,
                                    t.Element("schedule").Element("tuesday").Value, t.Element("schedule").Element("wednesday").Value, t.Element("schedule").Element("thursday").Value)
                          }).FirstOrDefault();
            }
            catch
            {
                tester = null;
            }
            return tester;
        }

        //public bool[,] set_schedule(string sunday, string monday, string tuesday, string wednesday, string thursday)
        //{
        //    string[] _sunday = sunday.Split(',');
        //    string[] _monday = monday.Split(',');
        //    string[] _tuesday = tuesday.Split(',');
        //    string[] _wednesday = wednesday.Split(',');
        //    string[] _thursday = thursday.Split(',');
        //    bool[,] scheduleMatrix = new bool[Configuration.NumOfHoursPerDay, Configuration.NumOfWorkingDays];
        //    for (int i = 0; i < Configuration.NumOfWorkingDays; i++)
        //    {
        //        for (int j = 0; j < Configuration.NumOfHoursPerDay; j++)
        //        {
        //            switch (i)
        //            {
        //                case 0:
        //                    if (_sunday[j] == "true")
        //                        scheduleMatrix[j, 0] = true;
        //                    else scheduleMatrix[j, 0] = false;
        //                    break;

        //                case 1:
        //                    if (_monday[j] == "true")
        //                        scheduleMatrix[j, 1] = true;
        //                    else scheduleMatrix[j, 1] = false;
        //                    break;

        //                case 2:
        //                    if (_tuesday[j] == "true")
        //                        scheduleMatrix[j, 2] = true;
        //                    else scheduleMatrix[j, 2] = false;
        //                    break;

        //                case 3:
        //                    if (_wednesday[j] == "true")
        //                        scheduleMatrix[j, 3] = true;
        //                    else scheduleMatrix[j, 3] = false;
        //                    break;

        //                case 4:
        //                    if (_thursday[j] == "true")
        //                        scheduleMatrix[j, 4] = true;
        //                    else scheduleMatrix[j, 4] = false;
        //                    break;
        //            }
        //        }
        //    }
        //    return scheduleMatrix;
        //}



        public void set_schedule_help(bool[,] scheduleMatrix, string dayString, int num)
        {
            string hour1 = "";
            string hour2 = "";
            int h1, h2;
            int i = 0;
            while(dayString != "")
            {
                hour1 = dayString.Substring(0, 2);
                dayString = dayString.Remove(0,6);
                hour2 = dayString.Substring(0,2);
                dayString = dayString.Remove(0,5);
                if (dayString.Length > 0) dayString = dayString.Remove(0,1);
                h1 = int.Parse(hour1);
                h2 = int.Parse(hour2);
                switch(h1)
                {
                    case 9:
                        scheduleMatrix[0, num] = true;
                        i = 1;
                        while(i <= h2 - 10)
                        {
                            scheduleMatrix[i, num] = true;
                            i++;
                        }
                        break;

                    case 10:
                        scheduleMatrix[1, num] = true;
                        i = 2;
                        while (i <= h2 - 10)
                        {
                            scheduleMatrix[i, num] = true;
                            i++;
                        }
                        break;

                    case 11:
                        scheduleMatrix[2, num] = true;
                        i = 3;
                        while (i <= h2 - 10)
                        {
                            scheduleMatrix[i, num] = true;
                            i++;
                        }
                        break;

                    case 12:
                        scheduleMatrix[3, num] = true;
                        i = 4;
                        while (i <= h2 - 10)
                        {
                            scheduleMatrix[i, num] = true;
                            i++;
                        }
                        break;

                    case 13:
                        scheduleMatrix[4, num] = true;
                        i = 5;
                        while (i <= h2 - 10)
                        {
                            scheduleMatrix[i, num] = true;
                            i++;
                        }
                        break;

                    case 14:
                        scheduleMatrix[5, num] = true;
                        break;
                }
            }
        }


        public bool[,] set_schedule(string sunday, string monday, string tuesday, string wednesday, string thursday)
        {
            bool[,] scheduleMatrix = new bool[Configuration.NumOfHoursPerDay, Configuration.NumOfWorkingDays];

            set_schedule_help(scheduleMatrix, sunday, 0);
            set_schedule_help(scheduleMatrix, monday, 1);
            set_schedule_help(scheduleMatrix, tuesday, 2);
            set_schedule_help(scheduleMatrix, wednesday, 3);
            set_schedule_help(scheduleMatrix, thursday, 4);

            return scheduleMatrix;

            //for (int i = 0; i < Configuration.NumOfWorkingDays; i++)
            //{
            //    switch (i)
            //    {
            //        case 0:
            //            void set_schedule_help(scheduleMatrix, sunday, 0);
            //            break;

            //        //case 1:
            //        //    if (_monday[j] == "true")
            //        //        scheduleMatrix[j, 1] = true;
            //        //    else scheduleMatrix[j, 1] = false;
            //        //    break;

            //        //case 2:
            //        //    if (_tuesday[j] == "true")
            //        //        scheduleMatrix[j, 2] = true;
            //        //    else scheduleMatrix[j, 2] = false;
            //        //    break;

            //        //case 3:
            //        //    if (_wednesday[j] == "true")
            //        //        scheduleMatrix[j, 3] = true;
            //        //    else scheduleMatrix[j, 3] = false;
            //        //    break;

            //        //case 4:
            //        //    if (_thursday[j] == "true")
            //        //        scheduleMatrix[j, 4] = true;
            //        //    else scheduleMatrix[j, 4] = false;
            //        //    break;
            //    }
            //}



            //string[] _sunday = sunday.Split(',');
            //string[] _monday = monday.Split(',');
            //string[] _tuesday = tuesday.Split(',');
            //string[] _wednesday = wednesday.Split(',');
            //string[] _thursday = thursday.Split(',');
            //bool[,] scheduleMatrix = new bool[Configuration.NumOfHoursPerDay, Configuration.NumOfWorkingDays];
            //for (int i = 0; i < Configuration.NumOfWorkingDays; i++)
            //{
            //    for (int j = 0; j < Configuration.NumOfHoursPerDay; j++)
            //    {
            //        switch (i)
            //        {
            //            case 0:
            //                if (_sunday[j] == "true")
            //                    scheduleMatrix[j, 0] = true;
            //                else scheduleMatrix[j, 0] = false;
            //                break;

            //            case 1:
            //                if (_monday[j] == "true")
            //                    scheduleMatrix[j, 1] = true;
            //                else scheduleMatrix[j, 1] = false;
            //                break;

            //            case 2:
            //                if (_tuesday[j] == "true")
            //                    scheduleMatrix[j, 2] = true;
            //                else scheduleMatrix[j, 2] = false;
            //                break;

            //            case 3:
            //                if (_wednesday[j] == "true")
            //                    scheduleMatrix[j, 3] = true;
            //                else scheduleMatrix[j, 3] = false;
            //                break;

            //            case 4:
            //                if (_thursday[j] == "true")
            //                    scheduleMatrix[j, 4] = true;
            //                else scheduleMatrix[j, 4] = false;
            //                break;
            //        }
            //    }
            //}
            //return scheduleMatrix;
        }



        //functions for tester
        public void AddTester(Tester tester)
        {
            loadData(ref testerRoot, testerPath);
            var id = new XElement("id", tester.TesterId);
            var firstName = new XElement("firstName", tester.FirstName);
            var sirName = new XElement("sirName", tester.Sirname);
            var dateOfBirth = new XElement("dateOfBirth", tester.DateOfBirth);
            var gender = new XElement("gender", tester.TesterGender);
            var phoneNumber = new XElement("phoneNumber", tester.PhoneNumber);
            var email = new XElement("email", tester.Email);
            var car = new XElement("car", tester.Testercar);
            var maxDistanceForTest = new XElement("maxDistanceForTest", tester.MaxDistanceForTest);
            var yearsOfExperience = new XElement("yearsOfExperience", tester.YearsOfExperience);
            var maxTestInAWeek = new XElement("maxTestInAWeek", tester.MaxTestsInaWeek);

            var address = new XElement("address", new XElement("street", tester.TesterAdress.Street),
                new XElement("buildingNumber", tester.TesterAdress.BuildingNumber),
                new XElement("city", tester.TesterAdress.City));

            var schedule = new XElement("schedule", new XElement("sunday", get_schedule(tester._schedual, 0)),
                new XElement("monday", get_schedule(tester._schedual, 1)),
                new XElement("tuesday", get_schedule(tester._schedual, 2)),
                new XElement("wednesday", get_schedule(tester._schedual, 3)),
                new XElement("thursday", get_schedule(tester._schedual, 4)));

            XElement finalTester = new XElement("tester", id, firstName, sirName, dateOfBirth, gender, phoneNumber, email, car, maxDistanceForTest,
                yearsOfExperience, maxTestInAWeek, address, schedule);

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
            testerElement.Element("firstName").Value = tester.FirstName;
            testerElement.Element("sirName").Value = tester.Sirname;
            testerElement.Element("dateOfBirth").Value = tester.DateOfBirth.ToString();
            testerElement.Element("gender").Value = tester.TesterGender.ToString();
            testerElement.Element("phoneNumber").Value = tester.PhoneNumber;
            testerElement.Element("email").Value = tester.Email;
            testerElement.Element("car").Value = tester.Testercar.ToString();
            testerElement.Element("maxDistanceForTest").Value = tester.MaxDistanceForTest.ToString();
            testerElement.Element("yearsOfExperience").Value = tester.YearsOfExperience.ToString();
            testerElement.Element("maxTestInAWeek").Value = tester.MaxTestsInaWeek.ToString();

            testerElement.Element("address").Element("street").Value = tester.TesterAdress.Street;
            testerElement.Element("address").Element("buildingNumber").Value = tester.TesterAdress.BuildingNumber;
            testerElement.Element("address").Element("city").Value = tester.TesterAdress.City;

            testerElement.Element("schedule").Element("sunday").Value = get_schedule(tester._schedual, 0);
            testerElement.Element("schedule").Element("monday").Value = get_schedule(tester._schedual, 1);
            testerElement.Element("schedule").Element("tuesday").Value = get_schedule(tester._schedual, 2);
            testerElement.Element("schedule").Element("wednesday").Value = get_schedule(tester._schedual, 3);
            testerElement.Element("schedule").Element("thursday").Value = get_schedule(tester._schedual, 4);

            testerRoot.Save(testerPath);
        }
        #endregion

        //functions for trainee
        public void AddTrainee(Trainee T) { }
        public void DeleteTrainee(Trainee T) { }
        public void UpdateTrainee(Trainee T) { }
        //functions for test
        public void AddTest(Test T) { }
        public void UpdateTest(Test T) { }

        //public List<Tester> GetListOfTesters() { return null; }
        public List<Trainee> GetListOfTrainees() { return null; }
        public List<Test> GetListOfTests() { return null; }





































        //class Dal_Imp_Xml : Idal
        //{

        //    public Dal_Imp_Xml()
        //    {
        //        //XElement help = XElement.Load("..//..//..//xmlFiles//config.xml");
        //        //Configuration.NumberOfTest = int.Parse(help.Element("NumberOfTest").Value);

        //        if (!File.Exists(pathTest))
        //        {
        //            List<Test> TestList = new List<Test>();//empty list for start
        //            SaveToXML<List<Test>>(TestList, pathTest);
        //        }

        //        if (!File.Exists(pathTester))
        //        {
        //            List<Tester> TesterList = new List<Tester>();//empty list for start
        //            SaveToXML<List<Tester>>(TesterList, pathTester);
        //        }

        //        if (!File.Exists(pathConfig))
        //        {

        //        }
        //        if (!File.Exists(pathTrainee))//we have to add new file
        //            CreateFileTrainee();
        //        else//ensure all data is according to current information
        //            LoadDataTrainee();
        //    }

        //    private void LoadDataTrainee()//load from file to program
        //    {
        //        try
        //        {
        //            TraineeRoot = XElement.Load(pathTrainee);
        //        }
        //        catch
        //        {
        //            throw new Exception("File upload problem");
        //        }
        //    }

        //    private void CreateFileTrainee()//for new file
        //    {
        //        TraineeRoot = new XElement("Trainee");
        //        TraineeRoot.Save(pathTrainee);//add new main element
        //    }

        //    public static void SaveToXML<T>(T source, string path)//save objects like elements from program to  file
        //    {
        //        FileStream file = new FileStream(path, FileMode.Create);
        //        XmlSerializer xmlSerializer = new XmlSerializer(source.GetType());
        //        xmlSerializer.Serialize(file, source);
        //        file.Close();
        //    }

        //    public static T LoadFromXML<T>(string path)//save elements like objects from file to program 
        //    {
        //        FileStream file = new FileStream(path, FileMode.Open);
        //        XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
        //        T result = (T)xmlSerializer.Deserialize(file);
        //        file.Close();
        //        return result;
        //    }




            //#region Tester
            //public void addTesterDAL(Tester tes)
            //{
            //    Tester tester = getTesterDAL(tes.Id);
            //    if (tester == null)
            //    {
            //        List<Tester> testerList = LoadFromXML<List<Tester>>(pathTester);
            //        testerList.Add(tes);
            //        SaveToXML<List<Tester>>(testerList, pathTester);
            //    }
            //    else
            //        throw new Exception("הטסטר הזה כבר קיים במערכת\n");
            //}

            //public void deleteTesterDAL(int id)
            //{
            //    List<Tester> testersListHelp = getTestersDAL().ToList();
            //    if (testersListHelp.Count() == 0)
            //        throw new Exception("לא קיים טסטר כזה במערכת\n");
            //    Tester tester = getTesterDAL(id);
            //    if (tester != null)
            //    {
            //        List<Tester> testersList = LoadFromXML<List<Tester>>(pathTester);
            //        testersList.RemoveAll(t => t.Id == id);
            //        SaveToXML<List<Tester>>(testersList, pathTester);
            //        throw new Exception("נמחקת בהצלחה מהמערכת");
            //    }
            //    else
            //        throw new Exception("לא נמצא טסטר כזה במערכת\n");
            //}

            //public void setTesterDetailsDAL(Tester tes)
            //{
            //    if (File.Exists(pathTester))
            //    {
            //        List<Tester> testersList = LoadFromXML<List<Tester>>(pathTester);
            //        int index = testersList.FindIndex(s => s.Id == tes.Id);
            //        if (index != -1)
            //        {
            //            testersList[index] = tes;
            //            SaveToXML<List<Tester>>(testersList, pathTester);
            //            throw new Exception("פרטיך עודכנו בהצלחה במערכת");

            //        }
            //    }
            //    else
            //        throw new Exception("לא נמצא טסטר כזה במערכת\n");
            //}

            //public Tester getTesterDAL(int id)
            //{
            //    if (File.Exists(pathTester))
            //    {

            //        List<Tester> testersList = LoadFromXML<List<Tester>>(pathTester);
            //        if (testersList.Count() == 0)
            //        {
            //            return null;
            //        }
            //        return testersList.FirstOrDefault(s => s.Id == id);
            //    }
            //    return null;
            //}

            //public IEnumerable<Tester> getTestersDAL(Func<Tester, bool> predicat = null)
            //{
            //    if (predicat == null)
            //        return LoadFromXML<List<Tester>>(pathTester).AsEnumerable().ToList();

            //    return LoadFromXML<List<Tester>>(pathTester).Where(predicat).ToList();
            //}

            //#endregion

            //#region Test

            //public void addTestDAL(Test test)
            //{
            //    if (File.Exists(pathTrainee) && File.Exists(pathTest))
            //    {
            //        List<Trainee> traineehelp = LoadFromXML<List<Trainee>>(pathTrainee);
            //        int index = traineehelp.FindIndex(s => s.Id == test.TraineeId);
            //        if (index != -1)
            //            traineehelp[index].NumOfTests++;//the amount of tests this trainee is taking
            //        else
            //            throw new Exception("לא נמצא נבחן כזה במערכת\n");
            //        Configuration.NumberOfTest++;//id of all tests
            //        test.TestNumber = Configuration.NumberOfTest;
            //        List<Test> testhelp = LoadFromXML<List<Test>>(pathTest);
            //        testhelp.Add(test);
            //        SaveToXML<List<Trainee>>(traineehelp, pathTrainee);
            //        SaveToXML<List<Test>>(testhelp, pathTest);
            //    }
            //}

            //public void setTestDAL(Test test)
            //{
            //    if (File.Exists(pathTest))
            //    {
            //        List<Test> testhelp = LoadFromXML<List<Test>>(pathTest);
            //        int index = testhelp.FindIndex(s => s.TestNumber == test.TestNumber);
            //        if (index != -1)
            //        {
            //            testhelp[index] = test;
            //            SaveToXML<List<Test>>(testhelp, pathTest);
            //        }
            //    }
            //    else
            //        throw new Exception("לא נמצא מבחן כזה במערכת\n");
            //}

            //public Test getTestDAL(int num)
            //{
            //    if (File.Exists(pathTest))
            //    {
            //        List<Test> testhelp = LoadFromXML<List<Test>>(pathTest);
            //        if (testhelp.Count == 0)
            //            return null;
            //        return new Test(testhelp.FirstOrDefault(s => s.TestNumber == num));
            //    }
            //    return null;
            //}

            //public IEnumerable<Test> getTestsDAL(Func<Test, bool> predicat = null)
            //{
            //    if (predicat == null)
            //        return LoadFromXML<List<Test>>(pathTest).AsEnumerable().ToList();

            //    return LoadFromXML<List<Test>>(pathTest).Where(predicat).ToList();
            //}

            //#endregion

            //#region Trainee
            //public void addTraineeDAL(Trainee trainee)
            //{
            //    LoadDataTrainee();
            //    Trainee train = getTraineeDAL(trainee.Id);
            //    if (train != null)
            //        throw new Exception("כבר קיים תלמיד עם אותה ת.ז");
            //    XElement Id = new XElement("id", trainee.Id);
            //    XElement FirstName = new XElement("firstName", trainee.FirstName);
            //    XElement LastName = new XElement("lastName", trainee.LastName);
            //    XElement Email = new XElement("email", trainee.Email);
            //    XElement TraineeGender = new XElement("traineeGender", trainee.TraineeGender);
            //    XElement PhoneNumber = new XElement("phoneNumber", trainee.PhoneNumber);
            //    //XElement street = new XElement("studentAddress", trainee.StudentAddress.street);
            //    //XElement buildingNumber = new XElement("studentAddress", trainee.StudentAddress.buildingNumber);
            //    //XElement town = new XElement("studentAddress", trainee.StudentAddress.town);
            //    //XElement StudentAddress = new XElement("studentAddress", street,buildingNumber,town /street,buildingNumber,town/);
            //    XElement Address = new XElement("Address", new XElement("street", trainee.StudentAddress.street), new XElement("building", trainee.StudentAddress.buildingNumber), new XElement("city", trainee.StudentAddress.town));
            //    XElement BirthDate = new XElement("birthDate", trainee.BirthDate);
            //    XElement Vehicle = new XElement("vehicle", trainee.Vehicle);
            //    XElement Gear = new XElement("gear", trainee.Gear);
            //    XElement DrivingSchoolName = new XElement("drivingSchoolName", trainee.DrivingSchoolName);
            //    XElement TeacherName = new XElement("teacherName", trainee.TeacherName);
            //    XElement NumberOfClasses = new XElement("numberOfClasses", trainee.NumberOfClasses);
            //    XElement Age = new XElement("age", trainee.Age);
            //    XElement Isrealicitzian = new XElement("isrealicitzian", trainee.Isrealicitzian);

            //    XElement complete = new XElement("trainee", Id, FirstName, LastName, Email, TraineeGender, PhoneNumber, Address, BirthDate, Vehicle, Gear, DrivingSchoolName, TeacherName, NumberOfClasses, Age, Isrealicitzian);

            //    TraineeRoot.Add(complete);//add new trainee to all trainee in file
            //    TraineeRoot.Save(pathTrainee);
            //}

            //public void deleteTraineeDAL(int id)
            //{
            //    LoadDataTrainee();
            //    XElement deleteTraineeElement;
            //    deleteTraineeElement = (from trElement in TraineeRoot.Elements()
            //                            where int.Parse(trElement.Element("id").Value) == id
            //                            select trElement).FirstOrDefault();
            //    if (deleteTraineeElement == null)
            //        throw new Exception("לא קיים תלמיד כזה במערכת");
            //    else
            //    {
            //        deleteTraineeElement.Remove();
            //        TraineeRoot.Save(pathTrainee);
            //    }

            //}

            //public void setTraineeDAL(Trainee trainee)
            //{
            //    LoadDataTrainee();
            //    //find element of this trainee
            //    XElement setTraineeElement;
            //    setTraineeElement = (from trElement in TraineeRoot.Elements()
            //                         where int.Parse(trElement.Element("id").Value) == trainee.Id
            //                         select trElement).FirstOrDefault();
            //    if (setTraineeElement == null)
            //        throw new Exception("לא קיים תלמיד עם אותה ת.ז");
            //    setTraineeElement.Element("id").Value = trainee.Id.ToString();
            //    setTraineeElement.Element("firstName").Value = trainee.FirstName;
            //    setTraineeElement.Element("lastName").Value = trainee.LastName;
            //    setTraineeElement.Element("email").Value = trainee.Email;
            //    setTraineeElement.Element("traineeGender").Value = trainee.TraineeGender.ToString();
            //    setTraineeElement.Element("phoneNumber").Value = trainee.PhoneNumber.ToString();
            //    setTraineeElement.Element("Address").Element("street").Value = trainee.StudentAddress.street;
            //    setTraineeElement.Element("Address").Element("buildingNumber").Value = trainee.StudentAddress.buildingNumber.ToString();
            //    setTraineeElement.Element("Address").Element("town").Value = trainee.StudentAddress.town;
            //    setTraineeElement.Element("birthDate").Value = trainee.BirthDate.ToString();
            //    setTraineeElement.Element("vehicle").Value = trainee.Vehicle.ToString();
            //    setTraineeElement.Element("gear").Value = trainee.Gear.ToString();
            //    setTraineeElement.Element("drivingSchoolName").Value = trainee.DrivingSchoolName;
            //    setTraineeElement.Element("teacherName").Value = trainee.TeacherName;
            //    setTraineeElement.Element("numberOfClasses").Value = trainee.NumberOfClasses.ToString();
            //    setTraineeElement.Element("age").Value = trainee.Age.ToString();
            //    setTraineeElement.Element("isrealicitzian").Value = trainee.Isrealicitzian.ToString();
            //    //save new to file            
            //    TraineeRoot.Save(pathTrainee);

            //}
            //public Trainee getTraineeDAL(int id)
            //{
            //    LoadDataTrainee();
            //    //find element of this trainee
            //    Trainee trainee;
            //    trainee = (from trElement in TraineeRoot.Elements()
            //               where int.Parse(trElement.Element("id").Value) == id
            //               select new Trainee()
            //               {
            //                   Id = int.Parse(trElement.Element("id").Value),
            //                   FirstName = trElement.Element("firstName").Value,
            //                   LastName = trElement.Element("lastName").Value,
            //                   Email = trElement.Element("email").Value,
            //                   TraineeGender = (Gender)Enum.Parse(typeof(Gender), trElement.Element("traineeGender").Value),
            //                   PhoneNumber = int.Parse(trElement.Element("phoneNumber").Value),
            //                   StudentAddress = ToAddress(trElement.Element("Address")),
            //                   BirthDate = DateTime.Parse(trElement.Element("birthDate").Value),
            //                   Vehicle = (TypeOfVehicle)Enum.Parse(typeof(TypeOfVehicle), trElement.Element("vehicle").Value),
            //                   Gear = (TypeOfGearControl)Enum.Parse(typeof(TypeOfGearControl), trElement.Element("gear").Value),
            //                   DrivingSchoolName = trElement.Element("drivingSchoolName").Value,
            //                   TeacherName = trElement.Element("teacherName").Value,
            //                   NumberOfClasses = int.Parse(trElement.Element("numberOfClasses").Value),
            //                   Age = int.Parse(trElement.Element("age").Value),
            //                   Isrealicitzian = bool.Parse(trElement.Element("isrealicitzian").Value),
            //               }).FirstOrDefault();
            //    return trainee;
            //}


            //public static Address ToAddress(XElement a)
            //{
            //    Address help = new Address();
            //    help.street = a.Element("street").Value;
            //    help.buildingNumber = Int32.Parse(a.Element("building").Value);
            //    help.town = a.Element("city").Value;
            //    return help;
            //}

            //public IEnumerable<Trainee> getTraineesDAL(Func<Trainee, bool> predicate = null)
            //{
            //    LoadDataTrainee();
            //    //string StudentAddress = null;
            //    IEnumerable<Trainee> allTrainee = (from trElement in TraineeRoot.Elements()
            //                                       select new Trainee()//convert from element in file to object of trainee
            //                                       {
            //                                           Id = int.Parse(trElement.Element("id").Value),
            //                                           FirstName = trElement.Element("firstName").Value,
            //                                           LastName = trElement.Element("lastName").Value,
            //                                           Email = trElement.Element("email").Value,
            //                                           TraineeGender = (Gender)Enum.Parse(typeof(Gender), trElement.Element("traineeGender").Value),
            //                                           PhoneNumber = int.Parse(trElement.Element("phoneNumber").Value),
            //                                           StudentAddress = ToAddress(trElement.Element("Address")),
            //                                           BirthDate = DateTime.Parse(trElement.Element("birthDate").Value),
            //                                           Vehicle = (TypeOfVehicle)Enum.Parse(typeof(TypeOfVehicle), trElement.Element("vehicle").Value),
            //                                           Gear = (TypeOfGearControl)Enum.Parse(typeof(TypeOfGearControl), trElement.Element("gear").Value),
            //                                           DrivingSchoolName = trElement.Element("drivingSchoolName").Value,
            //                                           TeacherName = trElement.Element("teacherName").Value,
            //                                           NumberOfClasses = int.Parse(trElement.Element("numberOfClasses").Value),
            //                                           Age = int.Parse(trElement.Element("age").Value),
            //                                           Isrealicitzian = bool.Parse(trElement.Element("isrealicitzian").Value)
            //                                       });

            //    if (predicate == null)//no condition
            //    {
            //        return allTrainee;
            //    }
            //    else
            //    {
            //        return allTrainee.Where(predicate);
            //    }
            //}
            //#endregion

        }



























}
