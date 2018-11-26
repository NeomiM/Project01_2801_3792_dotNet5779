using System;
using System.Collections.Generic;
using System.Text;
//using ClassLibrary1.enums
//using BE;

namespace BE
{
    public class Tester //can it be public?S
    {
        string id;
        string sirname;
        string firstName;
        DateTime DateOfBirth;
        // enums.gender; //do we nned a poperty?
        string testerGender;
        string PhoneNumber;
        Adrees TesterAdress;
        int YearsOfExperience;
        int MaxTestsInaWeek;
        enum CarType Testercar;
        int[,] Schedual[5][7]; //5 days a week, 6 hours +1 to show the day
        double MaxDistanceForTest; //in kilometers
        //get function to check id
        public string Id
        { get => id; set => id = value; }
        public string Sirname
        { get => sirname; set => sirname = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public DateTime DateOfBirth1 { get => DateOfBirth; set => DateOfBirth = value; }
        public string TesterGender { get => testerGender; set => testerGender = value; } //add enumsomehow
        public string PhoneNumber1 { get => PhoneNumber; set => PhoneNumber = value; }
        public int YearsOfExperience1 { get => YearsOfExperience; set => YearsOfExperience = value; }
        public int MaxTestsInaWeek1 { get => MaxTestsInaWeek; set => MaxTestsInaWeek = value; }
        internal Adrees TesterAdress1 { get => TesterAdress; set => TesterAdress = value; }
        public double MaxDistanceForTest1 { get => MaxDistanceForTest; set => MaxDistanceForTest = value; }

        public override string ToString()
        {
            return base.ToString();
        }

        //not sure if need mishtanim

    }
}
