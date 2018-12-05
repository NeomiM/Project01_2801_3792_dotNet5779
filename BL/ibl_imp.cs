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
        //fuctions for tester
        void IBL.AddTester(Tester T)
        {
          /*  try
            {

                DAL.DalImp dal;
                //   if (T.DateOfBirth1 < 1989)
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

        

        void IBL.DeleteTester(Tester T)
        {
           DeleteTester(T);
        }

        void IBL.UpdateTester(Tester T)
        {
            UpdateTester(T);
        }

        void IBL.AddTrainee(Trainee T)
        {
            AddTrainee(T);
        }

        void IBL.DeleteTrainee(Trainee T)
        {
            DeleteTrainee(T);
        }

        void IBL.UpdateTrainee(Trainee T)
        {
            UpdateTrainee(T);
        }

        void IBL.AddTest(Test T)
        {
            AddTest(T);
        }

        void IBL.UpdateTest(Test T)
        {
            UpdateTest(T);
        }

        List<Tester> IBL.GetListOfTesters()
        {
            return GetListOfTesters();
        }

        List<Trainee> IBL.GetListOfTrainees()
        {
            return GetListOfTrainees();
        }

        List<Test> IBL.GetListOfTests()
        {
            return GetListOfTests();
        }
    }
}
