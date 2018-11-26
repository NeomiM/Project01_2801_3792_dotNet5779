using System;
using System.Collections.Generic;
using System.Text;

namespace BE
{
    public class Trainee
    {
        string id;
        string sirname;
        string firstName;
        DateTime DateOfBirth;
        // enums.gender; //do we nned a poperty?
        string traineeGender;
        string PhoneNumber;
        Adrees traineeAdress;
        
         CarType traineecar;
        //enum GearType traineeGear;
        string DrivingSchool;
        string DrivingTeacher;
        int LessonsPassed;

        public string Id { get => id; set => id = value; }
        public string Sirname { get => sirname; set => sirname = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public DateTime DateOfBirth1 { get => DateOfBirth; set => DateOfBirth = value; }
        public string TraineeGender { get => traineeGender; set => traineeGender = value; }
        public string PhoneNumber1 { get => PhoneNumber; set => PhoneNumber = value; }
        internal Adrees TraineeAdress { get => traineeAdress; set => traineeAdress = value; }
        public string DrivingSchool1 { get => DrivingSchool; set => DrivingSchool = value; }
        public string DrivingTeacher1 { get => DrivingTeacher; set => DrivingTeacher = value; }
        public int LessonsPassed1 { get => LessonsPassed; set => LessonsPassed = value; }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
