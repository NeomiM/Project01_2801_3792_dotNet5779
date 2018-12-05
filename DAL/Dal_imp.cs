using System;
using System.Collections.Generic;
using System.Linq;
using BE;
using DS;
namespace DAL
{
    public class DalImp : Idal
    {
        public void AddTest(Test T) //add check for id if exist
        {
            if (Configuration.FirstTestId < 99999999)
            T.TestId += "" + Configuration.FirstTestId.ToString("D" + 8);
            else
            {
                //we finished the numbers
                //we could move on to letters like in hex
            }
            Configuration.FirstTestId++;

            //add if ids are in the system
            if (DataSource._traineeList.Exists(x=> x.TraineeId==T.TraineeId))
          if (DataSource._testerList.Exists(x => x.TesterId == T.TesterId))
             DataSource._testList.Add(T);
           //if(DataSource._testList.Any(x=>x.i)) 
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
            return new List<Tester>(DataSource._testerList);
        }

        public List<Test> GetListOfTests()
        {
            return new List<Test>(DataSource._testList);
        }

        public List<Trainee> GetListOfTrainees()
        {
            return new List<Trainee>(DataSource._traineeList);
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
            if (DataSource._testerList.Exists((x => x.TesterId == T.TesterId)))
            {
                DataSource._testerList.Remove(DataSource._testerList.Find(x => x.TesterId == T.TesterId));
                DataSource._testerList.Add(T);
            }
        }

        public void UpdateTrainee(Trainee T)
        {
            //removes the old thing in list
            if (DataSource._traineeList.Exists((x => x.TraineeId == T.TraineeId)))
            {
                DataSource._traineeList.Remove(DataSource._traineeList.Find(x => x.TraineeId == T.TraineeId));
                DataSource._traineeList.Add(T);
            }
        }
    }
}
