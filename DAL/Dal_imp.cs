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
            if(!DataSource._testList.Contains(T))
            DataSource._testList.Add(T);
            
        }

        public void AddTester(Tester T)  //add check for id if exist
        {
            if (!DataSource._testerList.Contains(T))
                DataSource._testerList.Add(T);
        }

        public void AddTrainee(Trainee T) //add check for id if exist
        {
            if (!DataSource._traineeList.Contains(T))
                DataSource._traineeList.Add(T);
        }

        public void DeleteTester(Tester T)
        {
            DataSource._testerList.Remove(T);
        }

        public void DeleteTrainee(Trainee T)
        {
            DataSource._traineeList.Remove(T);
        }

        public List<Tester> GetListOfTesters()
        {
            return DataSource._testerList;
        }

        public List<Test> GetListOfTests()
        {
            return DataSource._testList;
        }

        public List<Trainee> GetListOfTrainees()
        {
            return DataSource._traineeList;
        }

        public void UpdateTest(Test T)
        {
            //DataSource._testList.Find(==T.TestId);

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
