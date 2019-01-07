using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

//by Neomi Mayer 328772801 and Beila Wellner 205823792
namespace BE
{
    public class Tester //can it be public?S
    {
        
       #region private variables
        private string _testerId;
        private string _sirname;
        private string _firstName;
        private DateTime _dateOfBirth;
        private Gender _testerGender;
        private string _phoneNumber;

        internal Address _testerAdress;
        public bool[,] _schedual = new bool[6,5];
        private int _yearsOfExperience;
        private int _maxTestsInaWeek;

        private CarType _testercar;
        double _maxDistanceForTest; //in kilometers
        //things I added
        private string _email;

        #endregion

        #region gets and sets

        public string TesterId
        {
            get { return _testerId; }
            set { _testerId = value; }
        }
        public string Sirname { get => _sirname; set => _sirname = value; }
        public string FirstName { get => _firstName; set => _firstName = value; }
        public DateTime DateOfBirth { get => _dateOfBirth; set => _dateOfBirth = value; }
        public Gender TesterGender { get => _testerGender; set => _testerGender = value; } //add enumsomehow
        public string PhoneNumber { get => _phoneNumber; set => _phoneNumber = value; }
        public int YearsOfExperience { get => _yearsOfExperience; set => _yearsOfExperience = value; }
        public int MaxTestsInaWeek { get => _maxTestsInaWeek; set => _maxTestsInaWeek = value; }
        internal Address TesterAdress { get => _testerAdress; set => _testerAdress = value; }
        public double MaxDistanceForTest { get => _maxDistanceForTest; set => _maxDistanceForTest = value; }

        public CarType Testercar { get => _testercar; set => _testercar = value; }
        //public bool[,] Schedule { get; set; } = new bool[5, 6];
        public string Email { get => _email; set => _email = value; }
        //public bool[,] Schedual { get => _schedual; set => _schedual = value; }
        public void setSchedual(bool[] day1, bool[] day2, bool[] day3, bool[] day4, bool[] day5)
        {
            for (int i = 0; i < 6; i++)
                if (day1[i])
                    _schedual[0, i] = true;
            for (int i = 0; i < 6; i++)
                if (day2[i])
                    _schedual[1, i] = true;
            for (int i = 0; i < 6; i++)
                if (day3[i])
                    _schedual[2, i] = true;
            for (int i = 0; i < 6; i++)
                if (day4[i])
                    _schedual[3, i] = true;
            for (int i = 0; i < 6; i++)
                if (day5[i])
                    _schedual[4, i] = true;
        }
        public bool[,] getSchedual()
        {
            return _schedual;
        }

        #endregion

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
