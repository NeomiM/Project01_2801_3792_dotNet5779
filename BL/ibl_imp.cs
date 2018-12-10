using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;

namespace BL
{
    public class ibl_imp: IBL
    {

        DAL.Idal dal=new DAL.DalImp();
        DateTime now = DateTime.Today;
        //fuctions for tester

        #region Functions for Tester


        public void AddTester(Tester T)
        {
            bool[] checkAll =
                {CheckId(T.TesterId),CheckAge(T.DateOfBirth,"Tester")};

            bool clear = checkAll.All(x => x);
            if (clear)
                dal.AddTester(T);


        }



        public void DeleteTester(Tester T)
        {
            //DeleteTester(T);
        }

        public void UpdateTester(Tester T)
        {
            bool[] checkAll =
                {CheckId(T.TesterId),CheckAge(T.DateOfBirth,"Tester")};

            bool clear = checkAll.All(x => x);
            if (clear)
                dal.UpdateTester(T);
        }

        #endregion

        #region Functions for Trainee

        public void AddTrainee(Trainee T)
        {
            bool[] checkAll = { CheckId(T.TraineeId), CheckAge(T.DateOfBirth, "Trainee") };

            bool clear = checkAll.All(x => x);
            if (clear)
                dal.AddTrainee(T);
        }

        public void DeleteTrainee(Trainee T)
        {
            //DeleteTrainee(T);
        }

        public void UpdateTrainee(Trainee T)
        {
            bool[] checkAll =
                {CheckId(T.TraineeId),CheckAge(T.DateOfBirth,"Trainee")};

            bool clear = checkAll.All(x => x);
            if (clear)
                dal.UpdateTrainee(T);
        }

        #endregion


        public void AddTest(Test T)
        {
            bool[] checkAll = 
                {
                    NoConflictingTests(T),
                    HadMinAmountOfLessons(T),
                    HourInRange(T.DateAndHourOfTest.Hour),
                    NotPassedTest(T),
                    AvailableTester(T)
                };
            bool clear = checkAll.All(x => x);
            if (clear)
                dal.AddTest(T);
                try
            {
                //func check ids

          
                //gets all of the testers that have a test that day
                /*
                var testersOnDate = from tester in testers
                    from test in testlist
                    where test.TesterId == tester.TesterId && test.TestDate == T.TestDate
                     select new{tester, one =(int)test.TestDate.DayOfWeek , two=(int)T.DateAndHourOfTest.Hour };      
                 
                //erases the hours that each tester has a test
                foreach (var t in testersOnDate)
                {
                    t.tester.Schedule[t.one,t.two] = false;
                }

                //if tester is available in hour but he finshed max hours of tests a week
                */

                //V 1 check if test hour is in general hours (9-15)
                //2 get list of testers that are not testing in that hour
                //3 find the first one that hasnt finished his max test a week and that the distance isnt too far 
                //4 if there arent any testers available-> find all available hours and ask the trainee
               //print a list of available hours and ask the adder to add the test again.




             

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        public void UpdateTest(Test T)
        {
            //UpdateTest(T);
            try
            { 
                //checks all of the bool properties to see if any are empty
                bool emptyfield = T.GetType().GetProperties()
                    .Where(pi => pi.PropertyType == typeof(bool))
                    .Select(pi => (bool)pi.GetValue(T))
                    .Any(value => value==null);
                if(emptyfield || T.RemarksOnTest == null)
                    throw new Exception("ERROR. Not all of fields for end of test filled");
                dal.UpdateTest(T);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

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

        #endregion

        #region checks for test

        public bool NoConflictingTests(Test T)
        {
            try
            {
                List<Test> testlist = GetListOfTests();
                //gets all of the datetimes of the tests with the same student
                var testTime = from item in testlist
                               where item.TraineeId == T.TraineeId
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
                if (hour < 9 || hour > 15)
                    throw new Exception("ERROR. Test hour out of range. Range is from 9:00 to 15:00");
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public bool NotPassedTest(Test T)
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

        public bool AvailableTester(Test T)
        {
            return true;
        }

        #endregion

        public List<Tester> TestersInArea(Address a)
        {
            throw new NotImplementedException();
        }


    }
}
