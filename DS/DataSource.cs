using System;
using System.Collections.Generic;
using BE;

namespace DS
{

    public static class DataSource
    {
        public static List<Tester> _testerList=new List<Tester>();
        public static List<Trainee> _traineeList=new List<Trainee>();
        public static List<Test> _testList=new List<Test>();

        static  DataSource()
        {
            Test test=new Test();
            test.CheckMirrors = true;
            DateTime time= DateTime.Today;
            test.DateAndHourOfTest =time;
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

            Test test2 = new Test();
            test2.CheckMirrors = true;
            DateTime time2 = DateTime.Today;
            test2.DateAndHourOfTest = time;
            test2.ImediateStop = true;
            test2.KeptDistance = true;
            test2.KeptRightofPresidence = true;
            test2.Parking = true;
            test2.ReverseParking = true;
            test2.RightTurn = true;
            test2.StoppedAtRed = true;
            test2.StoppedAtcrossWalk = true;
            test2.TestDate = time;
            test2.TesterId = "319185997";
            test2.TraineeId = "319185997";

            _testList.Add(test);
            _testList.Add(test2);

            //make netunim for stuff   
        }
    }
}
