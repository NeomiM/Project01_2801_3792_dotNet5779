using System;
using System.Collections.Generic;
using System.Text;

using BE;

namespace DAL
{
    interface Idal
    {
        //fuctions for tester
        void addTester(Tester T);
        void deleteTester(Tester T);
        void updateTester(Tester T);
        //fuctions for trainee
        void addTrainee(Trainee T);
        void deleteTester(Trainee T);
        void updateTrainee(Trainee T);
        //fuctions for test
        void addTest(Test T);
        void updateTest(Test T);

        List<Tester> getListOfTesters();
        List<Trainee> getListOfTrainees();
        List<Test> getListOfTests();


    }
}
