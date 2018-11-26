using System;
using System.Collections.Generic;
using System.Text;

namespace BE
{
    public class Test
    {
        string testID;
        string testerID; //might need to have a user for each tester
        string traineeID;
        DateTime testDate;
        DateTime dateAndHourofTest;
        Adrees StartingPoint;
        //things to check in test
        bool keptDistance;
        bool parking;
        bool reverseParking;
        bool checkMirrors;
        bool usedSignal;
        bool keptRightofPresidence; //zchoot kdima
        bool stoppedAtRed;
        bool stoppedAtcrossWalk;
        bool rightTurn;
        bool imediateStop;

        bool testPassed;
        string RemarksOnTest; //hearot

        public string TestNumber { get => testID; set => testID = value; }
        public string TesterID { get => testerID; set => testerID = value; }
        public string TraineeID { get => traineeID; set => traineeID = value; }
        public DateTime TestDate { get => testDate; set => testDate = value; }
        public DateTime DateAndHourofTest { get => dateAndHourofTest; set => dateAndHourofTest = value; }
        public bool KeptDistance { get => keptDistance; set => keptDistance = value; }
        public bool Parking { get => parking; set => parking = value; }
        public bool ReverseParking { get => reverseParking; set => reverseParking = value; }
        public bool CheckMirrors { get => checkMirrors; set => checkMirrors = value; }
        public bool UsedSignal { get => usedSignal; set => usedSignal = value; }
        public bool KeptRightofPresidence { get => keptRightofPresidence; set => keptRightofPresidence = value; }
        public bool StoppedAtRed { get => stoppedAtRed; set => stoppedAtRed = value; }
        public bool StoppedAtcrossWalk { get => stoppedAtcrossWalk; set => stoppedAtcrossWalk = value; }
        public bool RightTurn { get => rightTurn; set => rightTurn = value; }
        public bool ImediateStop { get => imediateStop; set => imediateStop = value; }
        internal Adrees StartingPoint1 { get => StartingPoint; set => StartingPoint = value; }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
