using System;
using System.Collections.Generic;
using BE;

namespace DS
{
    public class DataSource
    {
        static List<Tester> _testerList;
        static List<Trainee> _traineeList;
        static List<Test> _testList;

        public DataSource()
        {
            Test test=new Test();
            test.CheckMirrors = true;
            DateTime time= DateTime.Today;
            test.DateAndHourofTest =time;
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
            


            //make netunim for stuff   
        }
    }
}
