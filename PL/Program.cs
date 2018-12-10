using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BE;
using BL;
namespace PL
{
    class Program
    {
        static void Main(string[] args)
        {
            BL.ibl_imp ibl=new ibl_imp();
        List<Tester> _testerList=ibl.GetListOfTesters();
        List<Trainee> _traineeList=ibl.GetListOfTrainees();
        List<Test> _testList=ibl.GetListOfTests();
            foreach (Test t in _testList)
            {
                System.Console.WriteLine(t);
            }
            foreach (Tester t in _testerList)
            {
                System.Console.WriteLine(t);
            }
            foreach (Trainee t in _traineeList)
            {
                System.Console.WriteLine(t);
            }
            System.Console.ReadKey();
            //make a trainee that it not ok
            //call ibl.addtrainee 
            Trainee trainee;
            //trainee.id=328772
            ibl.AddTrainee(trainee);
            //see what it prints out if it is ok

        }
    }
}
