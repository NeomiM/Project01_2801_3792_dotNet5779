﻿using System;
using System.Collections.Generic;
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

    }
}