using System;
using System.Collections.Generic;
using System.Text;

namespace BE
{
    public class Trainee
    {
        private string _id;
        private string _sirname;
        private string _firstName;
        private DateTime _dateOfBirth;
        private Gender _traineeGender;
        private string _phoneNumber;
        private Address _traineeAddress; 
        private CarType _traineecar;
        private GearType _traineeGear;
        private string _drivingSchool;
        private string _drivingTeacher;
        private int _lessonsPassed;
        //add more

        public string Id
        {
            get { return _id; }
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
                        _id = tempId;

                        //else- throw an exception
                    }
                }
            }
            #endregion
        }
        public string Sirname { get => _sirname; set => _sirname = value; }
        public string FirstName { get => _firstName; set => _firstName = value; }
        public DateTime DateOfBirth1 { get => _dateOfBirth; set => _dateOfBirth = value; }
        public Gender TraineeGender { get => _traineeGender; set => _traineeGender = value; }
        public string PhoneNumber1 { get => _phoneNumber; set => _phoneNumber = value; }
        internal Address TraineeAddress { get => _traineeAddress; set => _traineeAddress = value; }
        public CarType Traineecar { get => _traineecar; set => _traineecar = value; }
        public GearType TraineeGear { get => _traineeGear; set => _traineeGear = value; }
        public string DrivingSchool1 { get => _drivingSchool; set => _drivingSchool = value; }
        public string DrivingTeacher1 { get => _drivingTeacher; set => _drivingTeacher = value; }
        public int LessonsPassed1 { get => _lessonsPassed; set => _lessonsPassed = value; }
       

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
