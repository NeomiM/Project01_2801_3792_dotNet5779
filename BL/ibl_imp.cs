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
        public void AddTester(Tester T)
        {
            try
            {
                //DateTime now = DateTime.Today;
                int age = now.Year - T.DateOfBirth.Year;
                if (age < Configuration.MinAgeOFTester)
                    throw new Exception("ERROR. Trainer is too young");
                dal.AddTester(T);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
           
        }

        

        public void DeleteTester(Tester T)
        {
           //DeleteTester(T);
        }

        public void UpdateTester(Tester T)
        {
            //    UpdateTester(T);
            try
            {
                //DateTime now = DateTime.Today;
                int age = now.Year - T.DateOfBirth.Year;
                if (age < Configuration.MinAgeOFTester)
                    throw new Exception("ERROR. Age is too young");
                dal.UpdateTester(T);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void AddTrainee(Trainee T)
        {
            //  AddTrainee(T);
            try
            {
                //DateTime now = DateTime.Today;
                int age = now.Year - T.DateOfBirth.Year;
                if (age < Configuration.MinAgeOFTrainee)
                    throw new Exception("ERROR. Trainee is too young");
                dal.AddTrainee(T);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void DeleteTrainee(Trainee T)
        {
            //DeleteTrainee(T);
        }

        public void UpdateTrainee(Trainee T)
        {
            //UpdateTrainee(T);
            try
            {
                //DateTime now = DateTime.Today;
                int age = now.Year - T.DateOfBirth.Year;
                if (age < Configuration.MinAgeOFTrainee)
                    throw new Exception("ERROR. Age is too young");
                dal.UpdateTrainee(T);
            }
            catch (Exception e)
            {
               Console.WriteLine(e.Message);
            }
        }

        public void AddTest(Test T)
        {
            //AddTest(T);
            try
            {
                //func check ids

                List<Test> testlist = GetListOfTests();
                List<Trainee> trainees = GetListOfTrainees();
                List<Tester> testers = GetListOfTesters();
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
                    if (testTime.Any(x => x==T.DateAndHourOfTest))
                        throw new Exception("ERROR. it is not allowed to have two tests at the same time");
                }

                if(trainees.Find(x=>x.TraineeId==T.TraineeId).LessonsPassed<Configuration.MinAmmountOfLessons)
                  throw new Exception("ERROR. The trainee has not passed the minimun ammount of lessons.");
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

                //1 check if test hour is in general hours (9-15)
                //2 get list of testers that are not testing in that hour
                //3 find the first one that hasnt finished his max test a week and that the distance isnt too far 
                //4 if there arent any testers available-> find all available hours and ask the trainee
               //print a list of available hours and ask the adder to add the test again.




                //if the trainee already git his liscence for a certain car
                bool passedTheTest = testlist.Where(x => x.TraineeId == T.TraineeId)
                    .Any(x => x.CarType == T.CarType && x.TestPassed == T.TestPassed);
                if(passedTheTest)
                    throw new Exception("ERROR. Can't add a test that already has been passed");



             dal.AddTest(T);

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
    }
}
