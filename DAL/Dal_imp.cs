using System;
using System.Collections.Generic;
using BE;
using DS;
namespace DAL
{
    public class DalImp : IDal
    {
        public void AddTest(Test T) //add check for id if exist
        {
            //adds a test to the list of tests in dataSourse
           // testList.push_back(T);
            throw new NotImplementedException();
        }

        public void AddTester(Tester T)  //add check for id if exist
        {
            throw new NotImplementedException();
        }

        public void AddTrainee(Trainee T) //add check for id if exist
        {
            throw new NotImplementedException();
        }

        public void DeleteTester(Tester T)
        {
            throw new NotImplementedException();
        }

        public void DeleteTester(Trainee T)
        {
            throw new NotImplementedException();
        }

        public List<Tester> GetListOfTesters()
        {
            throw new NotImplementedException();
        }

        public List<Test> GetListOfTests()
        {
            throw new NotImplementedException();
        }

        public List<Trainee> GetListOfTrainees()
        {
            throw new NotImplementedException();
        }

        public void UpdateTest(Test T)
        {
            throw new NotImplementedException();
        }

        public void UpdateTester(Tester T)
        {
            throw new NotImplementedException();
        }

        public void UpdateTrainee(Trainee T)
        {
            throw new NotImplementedException();
        }
    }
}
