using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace BE
{
    public class Trainee
    {
        private string _traineeId;
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

        public string TraineeId
        {
            get { return _traineeId; }
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
                        _traineeId = tempId;

                        //else- throw an exception
                    }
                }
            }
            #endregion
        }
        public string Sirname { get => _sirname; set => _sirname = value; }
        public string FirstName { get => _firstName; set => _firstName = value; }
        public DateTime DateOfBirth { get => _dateOfBirth; set => _dateOfBirth = value; }
        public Gender TraineeGender { get => _traineeGender; set => _traineeGender = value; }
        public string PhoneNumber { get => _phoneNumber; set => _phoneNumber = value; }
        internal Address TraineeAddress { get => _traineeAddress; set => _traineeAddress = value; }
        public CarType Traineecar { get => _traineecar; set => _traineecar = value; }
        public GearType TraineeGear { get => _traineeGear; set => _traineeGear = value; }
        public string DrivingSchool { get => _drivingSchool; set => _drivingSchool = value; }
        public string DrivingTeacher { get => _drivingTeacher; set => _drivingTeacher = value; }
        public int LessonsPassed { get => _lessonsPassed; set => _lessonsPassed = value; }


        public override string ToString()
        {
            PropertyInfo[] _PropertyInfos = this.GetType().GetProperties(); ;

            var sb = new StringBuilder();

            foreach (var info in _PropertyInfos)
            {
                var value = info.GetValue(this, null) ?? "(null)";
                //puts spaces between the property words
                StringBuilder builder = new StringBuilder();
                foreach (char c in info.Name)
                {
                    if (Char.IsUpper(c) && builder.Length > 0) builder.Append(' ');
                    builder.Append(c);
                }

                sb.AppendLine(builder.ToString() + ": " + value.ToString());
            }

            return sb.ToString();
        }
    }
}
