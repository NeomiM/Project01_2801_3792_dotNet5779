using System;
using System.Collections.Generic;
using System.Text;

namespace BE
{
    public class Test
    {
        private string _testId;
        private string _testerId; //might need to have a user for each tester
        private string _traineeId;
        private DateTime _testDate;
        private DateTime _dateAndHourofTest;

        internal Address StartingPoint;
        //things to check in test
        private bool _keptDistance;
        private bool _parking;
        private bool _reverseParking;
        private bool _checkMirrors;
        private bool _usedSignal;
        private bool _keptRightofPresidence; //zchoot kdima
        private bool _stoppedAtRed;
        private bool _stoppedAtcrossWalk;
        private bool _rightTurn;
        private bool _imediateStop;

        private bool _testPassed;
        private string _remarksOnTest; //hearot

        Test()
        {
            Configuration.FirstTestId++;
            //TestId =Configuration.FirstTestId.ToString();
            TestId +=""+ Configuration.FirstTestId.ToString();
        }

        private string TestId { get => _testId; set => _testId = value; }
        private string TesterId { get => _testerId; set => _testerId = value; }
        private string TraineeId { get => _traineeId; set => _traineeId = value; }
        private DateTime TestDate { get => _testDate; set => _testDate = value; }
        private DateTime DateAndHourofTest { get => _dateAndHourofTest; set => _dateAndHourofTest = value; }
        private bool KeptDistance { get => _keptDistance; set => _keptDistance = value; }
        private bool Parking { get => _parking; set => _parking = value; }
        private bool ReverseParking { get => _reverseParking; set => _reverseParking = value; }
        private bool CheckMirrors { get => _checkMirrors; set => _checkMirrors = value; }
        private bool UsedSignal { get => _usedSignal; set => _usedSignal = value; }
        private bool KeptRightofPresidence { get => _keptRightofPresidence; set => _keptRightofPresidence = value; }
        private bool StoppedAtRed { get => _stoppedAtRed; set => _stoppedAtRed = value; }
        private bool StoppedAtcrossWalk { get => _stoppedAtcrossWalk; set => _stoppedAtcrossWalk = value; }
        private bool RightTurn { get => _rightTurn; set => _rightTurn = value; }
        private bool ImediateStop { get => _imediateStop; set => _imediateStop = value; }
        internal Address StartingPoint1 { get => StartingPoint; set => StartingPoint = value; }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
