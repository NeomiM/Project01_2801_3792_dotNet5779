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
            if(!DataSource._testList.Contains(T)) //if the test doesnt exist
                if(DataSource._traineeList.Exists(x=> x.Id==T.TraineeId)) //and both ids are in the system
                    if (DataSource._testerList.Exists(x => x.Id == T.TesterId))
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
            //removes the old thing in list
            DataSource._testList.Remove(DataSource._testList.Find(x => x.TestId == T.TestId));
            DataSource._testList.Add(T);
        }

        public void UpdateTester(Tester T)
        {
            //removes the old thing in list
            if (DataSource._testerList.Exists((x => x.Id == T.Id)))
            {
                DataSource._testerList.Remove(DataSource._testerList.Find(x => x.Id == T.Id));
                DataSource._testerList.Add(T);
            }
        }

        public void UpdateTrainee(Trainee T)
        {
            //removes the old thing in list
            if (DataSource._traineeList.Exists((x => x.Id == T.Id)))
            {
                DataSource._traineeList.Remove(DataSource._traineeList.Find(x => x.Id == T.Id));
                DataSource._traineeList.Add(T);
            }
        }
    }
}
