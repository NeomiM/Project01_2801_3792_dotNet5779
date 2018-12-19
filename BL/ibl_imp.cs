using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using BE;
using DAL;

//by Neomi Mayer 328772801 and Beila Wellner 205823792

namespace BL
{
    public class FactoryBL
    {
        static IBL bl = null;

        public static IBL GetBL()
        {
            if (bl == null) bl = new ibl_imp();
            return bl;
        }
    }

    public class ibl_imp: IBL
    {
        private DAL.Idal dal = FactoryDAL.GetDal();
        DateTime now = DateTime.Today;
        //fuctions for tester
        internal ibl_imp()
        {
        }

        #region Functions for Tester


        public void AddTester(Tester T)
        {
            bool[] checkAll =
                { CheckId(T.TesterId),
                   CheckAge(T.DateOfBirth,"Tester"),
                   TesterNotInSystem(T.TesterId),
                    CheckEmail(T.Email)};

            bool clear = checkAll.All(x => x);
            if (clear)
                dal.AddTester(T);


        }

        public void DeleteTester(Tester T)
        {
             bool[] checkAll =
                {CheckId(T.TesterId),
                TesterInSystem(T.TesterId)
                };

            bool clear = checkAll.All(x => x);
            if (clear)          
            dal.DeleteTester(T);
        }

        public void UpdateTester(Tester T)
        {
            bool[] checkAll =
                {CheckId(T.TesterId),
                CheckAge(T.DateOfBirth,"Tester"),
                TesterInSystem(T.TesterId),
                CheckEmail(T.Email)};

            bool clear = checkAll.All(x => x);
            if (clear)
                dal.UpdateTester(T);
        }

        #endregion

        #region Functions for Trainee

        public void AddTrainee(Trainee T)
        {
            bool[] checkAll = {
                    CheckId(T.TraineeId),
                    CheckAge(T.DateOfBirth, "Trainee"),
                    TraineeNotInSystem(T.TraineeId),
                    CheckEmail(T.Email) };

            bool clear = checkAll.All(x => x);
            if (clear)
                dal.AddTrainee(T);
        }

        public void DeleteTrainee(Trainee T)
        {
                   bool[] checkAll = {
                    CheckId(T.TraineeId),
                    TraineeInSystem(T.TraineeId)
                    };

            bool clear = checkAll.All(x => x);
            if (clear)
            dal.DeleteTrainee(T);
        }

        public void UpdateTrainee(Trainee T)
        {
            bool[] checkAll =
                {CheckId(T.TraineeId),CheckAge(T.DateOfBirth,"Trainee"),
                    TraineeInSystem(T.TraineeId)                
                ,CheckEmail(T.Email)};

            bool clear = checkAll.All(x => x);
            if (clear)
                dal.UpdateTrainee(T);
        }

        #endregion

        #region Functions for Tests

        public void AddTest(Test T)
        {
            bool[] checkAll =
            {
                TraineeInSystem(T.TraineeId),
                TesterInSystem(T.TesterId),
                HadMinAmountOfLessons(T),
                HourInRange(T.DateAndHourOfTest.Hour),
                DayInRange((int)T.TestDate.DayOfWeek),
                NoConflictingTests(T),
                NotPassedPrevTest(T),
                AvailableTesterFound(T)!=null,

            };
            bool clear = checkAll.All(x => x);
            if (clear)
            {
                T.TesterId = AvailableTesterFound(T);
                dal.AddTest(T);
                Console.WriteLine("Test added successfully");
            }

        }

        public void UpdateTest(Test T)
        {
            //UpdateTest(T);
            try
            {
                //checks all of the bool properties to see if any are empty
                bool emptyfield = T.GetType().GetProperties()
                    .Where(pi => pi.PropertyType == typeof(string))
                    .Select(pi => (string)pi.GetValue(T))
                    .Any(value => value == null);
                if (emptyfield || T.RemarksOnTest == null)
                    throw new Exception("ERROR. Not all of fields for end of test filled");
                var mostFailed = T.GetType().GetProperties()
                    .Where(pi => pi.PropertyType == typeof(bool))
                    .Select(pi => (bool)pi.GetValue(T)==false);
                if(mostFailed.Count(x=>x==false)>5 && T.TestPassed==true) 
                    throw new Exception("ERROR. Cannot pass a student if failed more then five checks." +
                                        " The test will not be updated.");
                dal.UpdateTest(T);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        #endregion


        #region Get Lists

        public List<Tester> GetListOfTesters()
        {
            return dal.GetListOfTesters();
        }

        public List<Trainee> GetListOfTrainees()
        {
            return dal.GetListOfTrainees();
        }

        public List<Test> GetListOfTests()
        {
            return dal.GetListOfTests();
        }

        #endregion


        #region Checks for people

        public bool CheckId(string id)
        {
            try
            {
                int idcheck;
                if (!int.TryParse(id, out idcheck))
                    throw new Exception("ERROR. Id must only contain numbers.");
                if (id.Length < 8)
                    throw new Exception("ERROR. Not enough numbers in id.");
                string tempId = id;
                //check if it's all numbers- 8/9 numbers
                if (tempId.Length == 8)
                    tempId = "0" + tempId;//adding '0' to id begining
                if (tempId.Length == 9)
                {
                    int sum = 0;
                    int calulate = 0;
                    for (int i = 0; i < 9; i++)
                    {
                        if (i % 2 == 0)//Multiplying the double places by 1
                        {
                            calulate = 1 * (int)Char.GetNumericValue(tempId[i]);
                        }
                        else //if(i % 2 != 0) Multiplying the double places by 2
                        {
                            calulate = 2 * (int)Char.GetNumericValue(tempId[i]);
                        }
                        if (calulate >= 10)
                        {
                            calulate = 1 + (calulate % 10);//tens digit (can only be 1) + Unity digit
                        }
                        sum += calulate;
                    }
                    if (sum % 10 == 0)
                    {
                        return true;
                    }
                    else throw new Exception("ERROR. Id is invalid.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }

            return false;
        }

        public bool CheckAge(DateTime birthday, string person)
        {
            try
            {
                //DateTime now = DateTime.Today;
                int age = now.Year - birthday.Year;
                switch (person)
                {
                    case "Tester":
                        if (age < Configuration.MinAgeOFTester)
                            throw new Exception("ERROR. Age is too young");
                        break;
                    case "Trainee":
                        if (age < Configuration.MinAgeOFTrainee)
                            throw new Exception("ERROR. Age is too young");
                        break;
                    default:
                        break;
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        public bool TraineeInSystem(string TraineeId)
        {

            try
            {
                List<Trainee> traineeList = dal.GetListOfTrainees();
                if (!traineeList.Any(x=>x.TraineeId==TraineeId))
                {
                 throw new Exception("ERROR. The trainee isn't in the system.");
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }

        }       
        public bool TesterInSystem(string TesterId)
            {
                try
                {
                    List<Tester> testerList = dal.GetListOfTesters();
                    if (testerList.All(x=>x.TesterId!=TesterId))
                    {
                    throw new Exception("ERROR. The tester isn't in the system.");
                    }
                    return true;
                }
                catch (Exception e)
                {
                 Console.WriteLine(e.Message);
                 return false;
                }
            }
        public bool TraineeNotInSystem(string TraineeId)
        {
            try
            {
                List<Trainee> traineeList = dal.GetListOfTrainees();
                if (traineeList.Any(x=>x.TraineeId==TraineeId))
                {
                 throw new Exception("ERROR. The trainee is alredy in the system.");
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        public bool TesterNotInSystem(string TesterId)
            {
                try
                {
                    List<Tester> testerList = dal.GetListOfTesters();
                    if (testerList.Any(x=>x.TesterId==TesterId))
                    {
                    throw new Exception("ERROR. The tester alredy is in the system.");
                    }
                    return true;
                }
                catch (Exception e)
                {
                 Console.WriteLine(e.Message);
                 return false;
                }
           }
        public bool CheckEmail(string email)
        {
            try
            {
                var eAddress = new System.Net.Mail.MailAddress(email);
                if(eAddress.Address != email)
                    throw new Exception("ERROR. Invalid email address");
                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        #endregion

        #region checks for test
        public bool NoConflictingTests(Test T)
        {
            try
            {
                List<Test> testlist = GetListOfTests();
                //gets all of the datetimes of the tests with the same student
                //var testTime = from item in testlist
                  //             where item.TraineeId == T.TraineeId &&item.CarType==T.CarType
                    //           select item.DateAndHourOfTest;
                var testTime=from item in AllTestsThat(x=>x.TraineeId == T.TraineeId &&x.CarType==T.CarType)
                select item.DateAndHourOfTest;
                if (testTime.Any())
                {
                    //if there is a test that is less then a week 
                    if (testTime.Any(x => (now - x).TotalDays < 7))
                        throw new Exception("ERROR. test dates are less than a week apart");
                    //if there are any tests with the same date and hour
                    if (testTime.Any(x => x == T.DateAndHourOfTest))
                        throw new Exception("ERROR. it is not allowed to have two tests at the same time");
                }

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public bool HadMinAmountOfLessons(Test T)
        {
            try
            {
                List<Trainee> trainees = GetListOfTrainees();
                if (trainees.Find(x => x.TraineeId == T.TraineeId).LessonsPassed < Configuration.MinAmmountOfLessons)
                    throw new Exception("ERROR. The trainee has not passed the minimum amount of lessons.");
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public bool HourInRange(int hour)
        {
            try
            {
                if (hour < Configuration.StartOfWorkDay || hour > Configuration.EndOfWorkDay)
                    throw new Exception("ERROR. Test hour out of range. Range is from 9:00 to 15:00");
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public bool NotPassedPrevTest(Test T)
        {
            try
            {
                List<Test> testlist = dal.GetListOfTests();
                bool passedTheTest = testlist.Where(x => x.TraineeId == T.TraineeId)
                    .Any(x => x.CarType == T.CarType && x.TestPassed == T.TestPassed);
                if (passedTheTest)
                    throw new Exception("ERROR. Can't add a test that already has been passed");
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public string AvailableTesterFound(Test T)
        {
            //all testers available in that hour from work schedual and other tests
            List<int> availableHours = new List<int>();
            try
            {
                //makes a list of all avialble hours for a test
                List<Tester> filteredTesters = new List<Tester>();                
                DateTime checkhour = T.TestDate;
                checkhour = checkhour.AddHours(Configuration.StartOfWorkDay);
                for (int i = Configuration.StartOfWorkDay; i <= Configuration.EndOfWorkDay; i++)
                {
                    checkhour = checkhour.AddHours(1);
                    filteredTesters = AvailableTesters(checkhour);
                    if (filteredTesters.Any())
                    {
                        availableHours.Add(i);
                    }
                    else throw new Exception("ERROR. There are now available testers that day.");
                }
                string hours = string.Join(",", availableHours);
                filteredTesters =AvailableTesters(T.DateAndHourOfTest);
                //check for any testers in that hour
                if (!filteredTesters.Any())
                {
                    throw new Exception("ERROR. No testers available in that hour");
                }
                //filters all of the cartypes
                var carMatch = from tester in filteredTesters
                    where tester.Testercar == T.CarType
                    select tester;
                if(!carMatch.Any())
                    throw new Exception("ERROR. There are no testers with that car type available that date.");
                filteredTesters = (List<Tester>) carMatch;
                string testerFound = "";
                foreach (Tester t in filteredTesters)
                {
                    if (HasntPassedMaxTests(t,T.TestDate))
                    {
                        testerFound = t.TesterId;
                        return testerFound;
                    }
                }
                throw new Exception("ERROR. Potential testers have passed their max amount of tests in a week");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                if (e.Message == "ERROR. No testers available in that hour")
                {
                    if (availableHours.Any())
                    {
                        Console.WriteLine("Avialable hours are: ");
                        string hours=string.Join(",",availableHours);
                        Console.WriteLine(hours);
                    }

                }

                return null;
            }


            
        }

        public bool DayInRange(int t)
        {
            try
            {
                if (t >Configuration.EndOfWorkWeek)
                    throw new Exception("ERROR. Test day of the week is out of range.");
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }

        }

        public bool HasntPassedMaxTests(Tester T,DateTime DateOfTest)
        {
            List < Test > tests= dal.GetListOfTests();
            //gets the date for the beginning of the week
            int diff = (7 + (DateOfTest.DayOfWeek - DayOfWeek.Sunday)) % 7;
            DateTime weekDay = DateOfTest.AddDays(-1 * diff).Date;
            int countTests=0;
            //goes through the week and adds the tests the tester has that week
            for (int i = 0; i < 5; i++)
            {
                var testsInDay = from test in tests
                    where test.TestDate == weekDay && T.TesterId == test.TestId
                    select test;
                countTests += testsInDay.Count();
                weekDay=weekDay.AddDays(1);
            }

            if (countTests < T.MaxTestsInaWeek)
                return true;
            return false;
        }

        #endregion

        #region additional functions

        public List<Tester> TestersInArea(Address a)
        {
            List<Tester> testerlist = dal.GetListOfTesters();
            //makes a random number for distance
            Random r = new Random();
            int x = r.Next(100, 1000);
            foreach (Tester t in testerlist)
            {
                // if(!distance(a,t)==x)
                //testerlist.remove(T);
            }

            return testerlist;
        }
     
        public List<Tester> AvailableTesters(DateTime dateAndHour)
        {
            List<Tester> testerlist = dal.GetListOfTesters();
            List<Test> testlist = dal.GetListOfTests();
            List<Tester> filteredTesters = new List<Tester>();
            int dayOfWeek = (int)dateAndHour.DayOfWeek;
            int hour = dateAndHour.Hour;
            if (dayOfWeek < 5 && hour >= Configuration.StartOfWorkDay && hour <= Configuration.EndOfWorkDay)
                foreach (Tester t in testerlist)
                {

                    var row = Enumerable.Range(0, t.Schedule.GetLength(1))
                        .Select(x => t.Schedule[dayOfWeek, x])
                        .ToArray();
                    bool noOtherTest =
                        testlist.Where(x => x.TestDate == dateAndHour.Date && x.TesterId == t.TesterId)
                        .All(delegate(Test x) { return x.DateAndHourOfTest.Hour != hour; });
                    //.All(x => x.DateAndHourOfTest.Hour != hour);
                    if (row[hour - Configuration.StartOfWorkDay] != false && noOtherTest)
                        filteredTesters.Add(t);

                }
            return filteredTesters;
        }
        
        public List<Test> AllTestsThat(Func<Test, bool> predicate)
        {
            List<Test> testlsList = dal.GetListOfTests();
            var all = from test in testlsList
                where predicate(test)
                select new {test};
            return (List<Test>) all;

        }
        
        public int NumberOfTests(Trainee T)
        {
            List<Test> testList = dal.GetListOfTests();
            var tests = from test in testList
                where test.TraineeId == T.TraineeId
                select test;
            return tests.Count();
        }
        
        public bool CanGetLicence(Trainee T)
        {
            List<Test> testList = dal.GetListOfTests();
            var tests = from test in testList
                where test.TestPassed
                select test;
            if (tests.Any())
                return true;
            return false;

        }
        
        public List<Test> TestsByDate()
        {
            List<Test> testList = dal.GetListOfTests();
            var tests = testList.OrderBy(x => x.TestDate);
            return (List<Test>) tests;
        }

        #endregion

        #region Grouping

        public IEnumerable<IGrouping<CarType, Tester>> TestersByCarType(bool orderList = false)
        {
            List<Tester> testerList = dal.GetListOfTesters();
            if (orderList)
            {
                var testersInOrder = from tester in testerList
                    orderby tester.Testercar
                    group tester by tester.Testercar;
                return testersInOrder;


            }
            else
            {
                var testers = from tester in testerList
                    group tester by tester.Testercar;
                return testers;
            }
        }

        public IEnumerable<IGrouping<string, Trainee>> TraineesByDrivingSchool(bool orderList = false)
        {
            List<Trainee> traineeList = dal.GetListOfTrainees();
            if (orderList)
            {
                var traineesInOrder = from trainee in traineeList
                    orderby trainee.DrivingSchool
                    group trainee by trainee.DrivingSchool;
                return traineesInOrder;


            }
            else
            {
                var trainees = from trainee in traineeList
                    group trainee by trainee.DrivingSchool;
                return trainees;

            }
        }

        public IEnumerable<IGrouping<string, Trainee>> TraineesByTeachers(bool orderList = false)
        {

            List<Test> testList = dal.GetListOfTests();
            List<Trainee> traineeList = dal.GetListOfTrainees();
            List<Tester> testerList = dal.GetListOfTesters();
            if (orderList)
            {
                 var traineesInOrder = from trainee in traineeList
                                       orderby trainee.DrivingTeacher
                                       group trainee by trainee.DrivingTeacher;
                return traineesInOrder;


            }
            else
            {
               var trainees = from trainee in traineeList
                   group trainee by trainee.DrivingTeacher;
                return trainees;

            }

        }

        public IEnumerable<IGrouping<int, Trainee>> TraineesByNumTestsDone(bool orderList = false)
        {
           
            List<Trainee> traineeList = dal.GetListOfTrainees();
            if (orderList)
            {
                var traineesInOrder = from trainee in traineeList
                    let numTests=NumberOfTests(trainee)
                    orderby numTests
                    group trainee by numTests;
                return traineesInOrder;


            }
            else
            {
                var trainees = from trainee in traineeList
                    group trainee by NumberOfTests(trainee);
                return trainees;

            }
        }

        #endregion

    }
}
