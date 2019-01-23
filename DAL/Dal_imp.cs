using System;
using System.Collections.Generic;
using System.Linq;

using BE;
using DS;
//by Neomi Mayer 328772801 and Beila Wellner 205823792
namespace DAL
{
    public class FactoryDAL
    {
        public static Idal getDAL(string typeDAL)
        {
            return Dal_imp.Instance;
        }
    }

    public class Dal_imp : Idal
    {

        #region Singleton
        private static readonly Dal_imp instance = new Dal_imp();
        public static Dal_imp Instance
        {
            get { return instance; }
        }



        private Dal_imp()
        {

        }
        static Dal_imp() { }

        #endregion

        public void AddTest(Test T)
        {
            try
            {
                if (Configuration.FirstTestId < 99999999)
                    T.TestId = "" + Configuration.FirstTestId.ToString("D" + 8);
                else
                {
                    throw new Exception("test id storage full");
                }
                Configuration.FirstTestId++;
                //add if ids are in the system
                if (!DataSource._traineeList.Any(x => x.TraineeId == T.TraineeId))
                {
                 throw new Exception("ERROR. The trainee isn't in the system.");
                }
                if (!DataSource._testerList.Any(x => x.TesterId == T.TesterId))
                 {
                 throw new Exception("ERROR. The tester isn't in the system");
                }                        
                DataSource._testList.Add(T);

            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
        }

        public void AddTester(Tester T)
        {
            try
            {
                if (DataSource._testerList.Exists((x => x.TesterId == T.TesterId)))
                    throw new Exception("ERROR. The tester already exists in the system.");
                DataSource._testerList.Add(T);
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
        }

        public void AddTrainee(Trainee T) //add check for id if exist
        {
            try
            {
                if (DataSource._traineeList.Exists((x => x.TraineeId == T.TraineeId)))
                    throw new Exception("ERROR. The trainee already exists in the system.");
                DataSource._traineeList.Add(T);
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
        }

        public void DeleteTester(Tester T)//and check if exist
        {
            try
            {
                if (!DataSource._testerList.Exists((x => x.TesterId == T.TesterId)))
                    throw new Exception("ERROR. The tester isn't in the system");
                DataSource._testerList.Remove(T);
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
        }

        public void DeleteTrainee(Trainee T)
        {
            try
            {
                if (!DataSource._traineeList.Exists((x => x.TraineeId == T.TraineeId)))
                    throw new Exception("ERROR. The trainee isn't in the system");
                DataSource._traineeList.Remove(T);
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
        }



        public void UpdateTest(Test T)
        {
            try
            {
                if (!DataSource._testList.Exists((x => x.TestId == T.TestId)))
                    throw new Exception("ERROR. The test isn't in the system");
                //removes the old thing in list
                DataSource._testList.Remove(DataSource._testList.Find(x => x.TestId == T.TestId));
                DataSource._testList.Add(T);
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
        }

        public void UpdateTester(Tester T)
        {
            try
            {
                if (!DataSource._testerList.Exists((x => x.TesterId == T.TesterId)))
                    throw new Exception("ERROR. The tester isn't in the system");
                //removes the old thing in list
                DataSource._testerList.Remove(DataSource._testerList.Find(x => x.TesterId == T.TesterId));
                DataSource._testerList.Add(T);
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
        }

        public void UpdateTrainee(Trainee T)
        {
            try
            {
                if (!DataSource._traineeList.Exists((x => x.TraineeId == T.TraineeId)))
                    throw new Exception("ERROR. The trainee isn't in the system");
                //removes the old thing in list
                DataSource._traineeList.Remove(DataSource._traineeList.Find(x => x.TraineeId == T.TraineeId));
                DataSource._traineeList.Add(T);
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
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
    }
}
