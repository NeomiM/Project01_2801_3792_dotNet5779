using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;

namespace BL
{
    public class ibl_imp: IBL
    {

        DAL.Idal dal=new DAL.DalImp();

        //fuctions for tester
        public void AddTester(Tester T)
        {
          /*  try
            {

                DAL.DalImp dal;
                //   if (T.DateOfBirth < 1989)
                // throw("too young")      


                //dal.AddTester(T);
            }
            catch (string messege)
            {
                Console
                write(message);
            }
            */
        }

        

        public void DeleteTester(Tester T)
        {
           //DeleteTester(T);
        }

        public void UpdateTester(Tester T)
        {
        //    UpdateTester(T);
        }

        public void AddTrainee(Trainee T)
        {
          //  AddTrainee(T);
        }

        public void DeleteTrainee(Trainee T)
        {
            //DeleteTrainee(T);
        }

        public void UpdateTrainee(Trainee T)
        {
            //UpdateTrainee(T);
        }

        public void AddTest(Test T)
        {
            //AddTest(T);
        }

        public void UpdateTest(Test T)
        {
            //UpdateTest(T);
        }

        public List<Tester> GetListOfTesters()
        {
            return dal.GetListOfTesters();
        }

        public List<Trainee> GetListOfTrainees()
        {
            return dal.GetListOfTrainees();
        }

        public List<Test> GetListOfTests()
        {
            return dal.GetListOfTests();
        }
    }
}
