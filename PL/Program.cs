using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
namespace PL
{
    class Program
    {
        static void Main(string[] args)
        {
            Test test = new Test();
            test.CheckMirrors = true;
            DateTime time = DateTime.Today;
            test.DateAndHourOfTest = time;
            test.ImediateStop = true;
            test.KeptDistance = true;
            test.KeptRightofPresidence = true;
            test.Parking = true;
            test.ReverseParking = true;
            test.RightTurn = true;
            test.StoppedAtRed = true;
            test.StoppedAtcrossWalk = true;
            test.TestDate = time;
            test.TesterId = "328772801";
            test.TraineeId = "328772801";
            System.Console.WriteLine(test.ToString());
            System.Threading.Thread.Sleep(20000);
        }
    }
}
