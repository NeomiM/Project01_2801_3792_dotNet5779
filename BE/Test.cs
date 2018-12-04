﻿using System;
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
        private DateTime _dateAndHourofTest; //need to check if it is an exact hour?

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
            if(Configuration.FirstTestId <99999999)
            TestId +=""+ Configuration.FirstTestId.ToString("D" + 8);
            else
            {
                //we finished the numbers
                //we could move on to letters like in hex
            }
        }

        private string TestId
        {
            get { return _testId; }
            //not all options checked
            #region checkid
            set
            {
                string tempId = value;
                //check if it's all numbers- 8/9 numbers
                if (tempId.Length == 8)
                    tempId = "0" + tempId;//adding '0' to id begining
                if (tempId.Length == 9)
                {
                    int sum = 0;
                    int calulate = 0;
                    for (int i = 0; i < 9; i++)
                    {
                        if (i % 2 == 0)//Multiplying the double places by 1
                        {
                            calulate = 1 * (int)Char.GetNumericValue(tempId[i]);
                        }
                        else //if(i % 2 != 0) Multiplying the double places by 2
                        {
                            calulate = 2 * (int)Char.GetNumericValue(tempId[i]);
                        }
                        if (calulate >= 10)
                        {
                            calulate = 1 + (calulate % 10);//tens digit (can only be 1) + Unity digit
                        }
                        sum += calulate;
                    }
                    if (sum % 10 == 0)
                    {
                        _testId = tempId;

                        //else- throw an exception
                    }
                }
            }
            #endregion
        }

        private string TesterId
        {
            get { return _testerId; }
            //not all options checked
            #region checkid
            set
            {
                string tempId = value;
                //check if it's all numbers- 8/9 numbers
                if (tempId.Length == 8)
                    tempId = "0" + tempId;//adding '0' to id begining
                if (tempId.Length == 9)
                {
                    int sum = 0;
                    int calulate = 0;
                    for (int i = 0; i < 9; i++)
                    {
                        if (i % 2 == 0)//Multiplying the double places by 1
                        {
                            calulate = 1 * (int)Char.GetNumericValue(tempId[i]);
                        }
                        else //if(i % 2 != 0) Multiplying the double places by 2
                        {
                            calulate = 2 * (int)Char.GetNumericValue(tempId[i]);
                        }
                        if (calulate >= 10)
                        {
                            calulate = 1 + (calulate % 10);//tens digit (can only be 1) + Unity digit
                        }
                        sum += calulate;
                    }
                    if (sum % 10 == 0)
                    {
                        _testerId = tempId;

                        //else- throw an exception
                    }
                }
            }
            #endregion
        }
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
