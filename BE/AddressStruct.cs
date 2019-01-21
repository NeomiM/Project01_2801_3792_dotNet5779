using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
//by Neomi Mayer 328772801 and Beila Wellner 205823792

namespace BE
{
    public struct Address
    {
        private string _street;
        private string _buildingNumber;
        private string _city;

        public string Street { get => _street; set => _street = value; }
        public string BuildingNumber { get => _buildingNumber; set => _buildingNumber = value; }
        public string City { get => _city; set => _city = value; }

        //public Address()
        //{
        //    _street = null;
        //    _buildingNumber = null;
        //    _city = null;
        //}

        public Address(string st,string bn, string c)
        {
            _street = st;
            _buildingNumber = bn;
            _city = c;
        }

        public override string ToString()
        {
            //PropertyInfo[] _PropertyInfos = this.GetType().GetProperties(); ;

            //var sb = new StringBuilder();

            //foreach (var info in _PropertyInfos)
            //{
            //    var value = info.GetValue(this, null) ?? "(null)";
            //    //puts spaces between the property words
            //    StringBuilder builder = new StringBuilder();
            //    foreach (char c in info.Name)
            //    {
            //        if (Char.IsUpper(c) && builder.Length > 0) builder.Append(' ');
            //        builder.Append(c);
            //    }

            //    sb.AppendLine(builder.ToString() + ": " + value.ToString());
            //}

           // return sb.ToString();
            if (Street != null && City != null && BuildingNumber != null)
                return Street + " " + BuildingNumber + " st. " + City;

            else return null;

        }
        public static bool operator ==(Address a,Address b)
        {
            return a.Street == null || a.City == null || a.BuildingNumber == null;
        }
        public static bool operator !=(Address a, Address b)
        {
            return a.Street != null && a.City != null && a.BuildingNumber != null;
        }


    }


}


