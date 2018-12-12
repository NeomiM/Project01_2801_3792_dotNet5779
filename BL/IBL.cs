using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
namespace BL
{
    interface IBL
    {
        //fuctions for tester
        void AddTester(Tester T);
        void DeleteTester(Tester T);
        void UpdateTester(Tester T);
        //fuctions for trainee
        void AddTrainee(Trainee T);
        void DeleteTrainee(Trainee T);
        void UpdateTrainee(Trainee T);
        //fuctions for test
        void AddTest(Test T);
        void UpdateTest(Test T);

        List<Tester> GetListOfTesters();
        List<Trainee> GetListOfTrainees();
        List<Test> GetListOfTests();
        
        //checks for trainer and trainee
        bool CheckId(string id);
        bool CheckAge(DateTime birthday, string person);
        //checks for test
        bool NoConflictingTests(Test T);
        bool HadMinAmountOfLessons(Test T);
        bool HourInRange(int hour);
        bool NotPassedPrevTest(Test T);
        string AvailableTesterFound(Test T);
        bool DayInRange(int d);
        bool HourInRage(int h);

        bool HasntPassedMaxTests(Tester T,DateTime DateOfTest);
        //additional functions
        List<Tester> TestersInArea(BE.Address a);
        List<Tester> AvailableTesters(DateTime dateAndHour);
        List<Test> AllTestsThat(Func<Test,bool> predicate);
        

    }
}
