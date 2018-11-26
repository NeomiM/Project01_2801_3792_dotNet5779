using System;
using System.Collections.Generic;
using BE;
using DS;
namespace DAL
{
    public class Dal_imp : Idal
    {
        public void addTest(Test T) //add check for id if exist
        {
            //adds a test to the list of tests in dataSourse
            //pushback/front
            //testList.push_back(T);
            throw new NotImplementedException();
        }

        public void addTester(Tester T)  //add check for id if exist
        {
            throw new NotImplementedException();
        }

        public void addTrainee(Trainee T) //add check for id if exist
        {
            throw new NotImplementedException();
        }

        public void deleteTester(Tester T)
        {
            throw new NotImplementedException();
        }

        public void deleteTester(Trainee T)
        {
            throw new NotImplementedException();
        }

        public List<Tester> getListOfTesters()
        {
            throw new NotImplementedException();
        }

        public List<Test> getListOfTests()
        {
            throw new NotImplementedException();
        }

        public List<Trainee> getListOfTrainees()
        {
            throw new NotImplementedException();
        }

        public void updateTest(Test T)
        {
            throw new NotImplementedException();
        }

        public void updateTester(Tester T)
        {
            throw new NotImplementedException();
        }

        public void updateTrainee(Trainee T)
        {
            throw new NotImplementedException();
        }
    }
}
