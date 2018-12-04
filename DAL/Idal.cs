using System;
using System.Collections.Generic;
using System.Text;

using BE;

namespace DAL
{
    interface IDal
    {
        //fuctions for tester
        void AddTester(Tester T);
        void DeleteTester(Tester T);
        void UpdateTester(Tester T);
        //fuctions for trainee
        void AddTrainee(Trainee T);
        void DeleteTester(Trainee T);
        void UpdateTrainee(Trainee T);
        //fuctions for test
        void AddTest(Test T);
        void UpdateTest(Test T);

        List<Tester> GetListOfTesters();
        List<Trainee> GetListOfTrainees();
        List<Test> GetListOfTests();


    }
}
