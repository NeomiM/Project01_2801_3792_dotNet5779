using System;
using System.Collections.Generic;
using System.Text;
//using ClassLibrary1.enums
//using BE;

namespace BE
{
    public class Tester //can it be public?S
    {
        string _id;
        string _sirname;
        string _firstName;
        DateTime _dateOfBirth;
        Gender _testerGender;
        string _phoneNumber;
        Address _testerAddress;
        int _yearsOfExperience;
        int _maxTestsInaWeek;
        CarType _testercar;
        private bool[,] Schedual= new bool[5, 6];

        double _maxDistanceForTest; //in kilometers
        //get function to check id
        public string Id
        { get => _id; set => _id = value; }
        public string Sirname
        { get => _sirname; set => _sirname = value; }
        public string FirstName { get => _firstName; set => _firstName = value; }
        public DateTime DateOfBirth1 { get => _dateOfBirth; set => _dateOfBirth = value; }
        public Gender TesterGender { get => _testerGender; set => _testerGender = value; } 
        public string PhoneNumber1 { get => _phoneNumber; set => _phoneNumber = value; }
        public int YearsOfExperience1 { get => _yearsOfExperience; set => _yearsOfExperience = value; }
        public int MaxTestsInaWeek1 { get => _maxTestsInaWeek; set => _maxTestsInaWeek = value; }
        public CarType Testercar1 { get => _testercar; set => _testercar = value; }
        public bool[,] Schedual1 { get => Schedual; set => Schedual = value; }
        internal Address TesterAdress1 { get => _testerAddress; set => _testerAddress = value; }
        public double MaxDistanceForTest1 { get => _maxDistanceForTest; set => _maxDistanceForTest = value; }

        public override string ToString()
        {
            return base.ToString();
        }

        //not sure if need mishtanim

    }
}
