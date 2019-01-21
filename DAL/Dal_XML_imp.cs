//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.IO;
//using System.Xml.Linq;
//using BE;

//namespace DAL
//{

//    public class FactoryDAL
//    {
//        public static Idal getDAL(string typeDAL)
//        {
//            return Dal_imp.Instance;
//        }
//    }
//    class Dal_XML_imp: Idal
//    {

//        #region Singleton
//        private static readonly Dal_XML_imp instance = new Dal_XML_imp();
//        public static Dal_XML_imp Instance
//        {
//            get { return instance; }
//        }



//        private Dal_XML_imp(){}
//        static Dal_XML_imp() { }

//        #endregion


//        #region xmls and lists
//        private readonly XElement _traineesXml;
//        private readonly XElement _config;
//        private readonly XElement _testersXml;

//        private List<Trainee> _trainees = new List<Trainee>();
//        private readonly List<Test> _tests = new List<Test>();
//        private readonly List<Tester> _testers = new List<Tester>();



//        #endregion
//    }
//}
