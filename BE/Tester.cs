﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
//using ClassLibrary1.enums
//using BE;

namespace BE
{
    public class Tester //can it be public?S
    {
        string _testerId;
        string _sirname;
        string _firstName;
        DateTime _dateOfBirth;
        Gender _testerGender;
        string _phoneNumber;
        private string _email;
        Address _testerAdress;
        int _yearsOfExperience;
        int _maxTestsInaWeek;

        CarType _testercar;
        double _maxDistanceForTest; //in kilometers
       
        public string TesterId
        {
            get { return _testerId; }
            set { _testerId = value; }
        }
        public string Sirname{ get => _sirname; set => _sirname = value; }
        public string FirstName { get => _firstName; set => _firstName = value; }
        public DateTime DateOfBirth { get => _dateOfBirth; set => _dateOfBirth = value; }
        public Gender TesterGender { get => _testerGender; set => _testerGender = value; } //add enumsomehow
        public string PhoneNumber { get => _phoneNumber; set => _phoneNumber = value; }
        public int YearsOfExperience { get => _yearsOfExperience; set => _yearsOfExperience = value; }
        public int MaxTestsInaWeek { get => _maxTestsInaWeek; set => _maxTestsInaWeek = value; }
        internal Address TesterAdress { get => _testerAdress; set => _testerAdress = value; }
        public double MaxDistanceForTest { get => _maxDistanceForTest; set => _maxDistanceForTest = value; }

        public CarType Testercar { get => _testercar; set => _testercar = value; }
        public bool[,] Schedule { get; set; } = new bool[5, 6];
        public string Email { get => _email; set => _email = value; }

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

        //not sure if need mishtanim

    }
}
