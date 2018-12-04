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
        Address _testerAdress;
        int _yearsOfExperience;
        int _maxTestsInaWeek;

        CarType _testercar;

        //int[,] Schedual[5][7]; //5 days a week, 6 hours +1 to show the day
        double _maxDistanceForTest; //in kilometers
       
        public string Id
        {
            get { return _id; }
            //not all options checked
            #region checkid
            set
            {
                string tempId = value;
                //check if it's all numbers- 8/9 numbers
                //check for letters,and ammount of numbers in the id
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
        public Gender TesterGender { get => _testerGender; set => _testerGender = value; } //add enumsomehow
        public string PhoneNumber1 { get => _phoneNumber; set => _phoneNumber = value; }
        public int YearsOfExperience1 { get => _yearsOfExperience; set => _yearsOfExperience = value; }
        public int MaxTestsInaWeek1 { get => _maxTestsInaWeek; set => _maxTestsInaWeek = value; }
        internal Address TesterAdress1 { get => _testerAdress; set => _testerAdress = value; }
        public double MaxDistanceForTest1 { get => _maxDistanceForTest; set => _maxDistanceForTest = value; }

        public CarType Testercar { get => _testercar; set => _testercar = value; }

        public override string ToString()
        {
            return base.ToString();
        }

        //not sure if need mishtanim

    }
}
