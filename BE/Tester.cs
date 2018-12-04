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
        private bool[,] _schedual= new bool[5, 6];

        double _maxDistanceForTest; //in kilometers
                                    //add more
        //get function to check id
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
        public string Sirname
        { get => _sirname; set => _sirname = value; }
        public string FirstName { get => _firstName; set => _firstName = value; }
        public DateTime DateOfBirth1 { get => _dateOfBirth; set => _dateOfBirth = value; }
        public Gender TesterGender { get => _testerGender; set => _testerGender = value; } 
        public string PhoneNumber { get => _phoneNumber; set => _phoneNumber = value; }
        public int YearsOfExperience { get => _yearsOfExperience; set => _yearsOfExperience = value; }
        public int MaxTestsInaWeek { get => _maxTestsInaWeek; set => _maxTestsInaWeek = value; }
        public CarType Testercar { get => _testercar; set => _testercar = value; }
        public bool[,] Schedual { get => Schedual; set => Schedual = value; }
        internal Address TesterAdress { get => _testerAddress; set => _testerAddress = value; }
        public double MaxDistanceForTest { get => _maxDistanceForTest; set => _maxDistanceForTest = value; }

        public override string ToString()
        {
            return base.ToString();
        }

        

    }
}
