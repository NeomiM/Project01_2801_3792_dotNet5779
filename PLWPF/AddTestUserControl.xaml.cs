using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BL;
using BE;
using Calendar = System.Globalization.Calendar;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for AddTestUserControl.xaml
    /// </summary>
    public partial class AddTestUserControl : UserControl
    {
        private BL.IBL bl;
        private List<Trainee> AddTraineeListForPL; 
        private Test AddTestForPL;
        private Dictionary<string, List<int>> AvilabletestersForPL;
        private Address? traineeAdd;
       private BackgroundWorker sortTestersByAddress;
     //   private BackgroundWorker DistanceOfTest;
        private List<Tester> TesterByDistance;
        private string testDistance;
        public AddTestUserControl()
        {
            InitializeComponent();
            bl = IBL_imp.Instance;

            List<Tester> SortedTestersByCarType= new List<Tester>();

            try
            {
                

                hours.Visibility = Visibility.Hidden;
                
                TesterComboBox.IsEnabled = false;
                AddDateErrors.Foreground = Brushes.Red;
                HoursErrors.Foreground = Brushes.Red;
                TesterErrors.Foreground = Brushes.Red;
                AddTestForPL = new Test();
                TestAddGrid.DataContext = AddTestForPL;
                dateAndHourOfTestTextBlock.DataContext = AddTestForPL.DateAndHourOfTest.ToString();
                AddTraineeListForPL = bl.readyTrainees();
                emptyAddTab();
                if (AddTraineeListForPL.Count == 0)
                    throw new Exception("ERROR. There are no trainees ready for a test.");
                this.AddTraineeIdComboBox.ItemsSource = AddTraineeListForPL.Select(x => x.TraineeId);
               AddTestCalender.DisplayDateStart = DateTime.Today;
                AddTestCalender.IsEnabled = false;
                hours.IsEnabled = false;
                testIdTextBlock.Text = Configuration.FirstTestId.ToString("D" + 8);
               // blackoutFridaysAndSaterdays(DateTime.Today, DateTime.Today.AddDays(60));
                if(DateTime.Now.Hour>BE.Configuration.EndOfWorkDay-1)
                    AddTestCalender.BlackoutDates.Add(new CalendarDateRange(DateTime.Today));
                


            }
            catch (Exception exception)
            {
                emptyAddTab();
                TestAddGrid.IsEnabled = false;
                calenderAndHours.IsEnabled = false;
                TesterErrors.Text = exception.Message;
                TesterErrors.Visibility = Visibility.Visible;
                TesterErrors.Foreground = Brushes.Red;
                //  MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }


        #region empty
        public void emptyAddTab()
        {
            blErrors.Visibility = Visibility.Collapsed;
            blErrors.Text = "";
            AddTestCalender.IsEnabled = false;
            Save.IsEnabled = false;
            dateAndHourOfTestTextBlock.Text = "";
            TestAddressErrors.Visibility = Visibility.Collapsed;
            TestAddressErrors.Text = "";
            testerAddress.Text = "";
            carTypeTextBlock.Text = "";
            street.Text = "";
            stNumber.Text = "";
            city.Text = "";
            hours.SelectedItem = null;
            traineeAddress.Text = "";
            findTesters.IsEnabled = false;
            emptyErrors();
            TesterComboBox.SelectedIndex = -1;
        //    SortByDistance.Visibility = Visibility.Hidden;
            //    AddTestCalender=new System.Windows.Controls.Calendar();
        }

        public void emptyErrors()
        {
            AddDateErrors.Text = "";
            AddDateErrors.Visibility = Visibility.Collapsed;
            HoursErrors.Visibility = Visibility.Collapsed;
            HoursErrors.Text = "";
            TesterErrors.Text = "";
            TesterErrors.Visibility = Visibility.Collapsed;

        }
        #endregion

        #region calender

        private void AddTestCalender_OnDisplayDateChanged(object sender, CalendarDateChangedEventArgs e)
        {
            //blacks out fridays and saterdays when month is changed
            blackoutFridaysAndSaterdays((DateTime) AddTestCalender.DisplayDate,
                ((DateTime) AddTestCalender.DisplayDate).AddDays(60));
            Blackoutdays(1, AddTestCalender.DisplayDate.Month);

        }

        private void AddTestCalender_OnSelectedDatesChanged(object sender,
            SelectionChangedEventArgs selectionChangedEventArgs)
        {
            try
            {
                TesterComboBox.Visibility = Visibility.Hidden;
                TestAddressBlock.IsEnabled = true;
                findTesters.IsEnabled = true;
                if(TesterByDistance!=null)
                    //SortByDistance.Visibility = Visibility.Visible;
                testerAddress.Text = "";
                hours.Items.Clear();
                hours.Visibility = Visibility.Hidden;
                AddDateErrors.Text = "";
                TesterComboBox.SelectedIndex=-1;
                //if (TesterComboBox.Items != null)
                //    TesterComboBox.SelectedIndex = -1;
               // if (bl.AvailableTesterFound(AddTestForPL) == null)
                 //   throw new Exception("ERROR. No testers avialble on that date");
                AddTestForPL.TestDate = (DateTime) AddTestCalender.SelectedDate;
                AvilabletestersForPL = bl.AvailableTesterFound(AddTestForPL);
                TesterComboBox.ItemsSource = AvilabletestersForPL.Keys.ToList();
                TesterComboBox.SelectedItem = null;
               // TesterComboBox.IsEnabled = true;
                //SortByDistance.Visibility = Visibility.Visible;
                AddDateErrors.Visibility = Visibility.Collapsed;
                AddDateErrors.Text = "";
                TimeSpan ts = new TimeSpan(9, 0, 0);
                if ((DateTime) AddTestCalender.SelectedDate == DateTime.Today)
                {
                    ts = new TimeSpan(DateTime.Now.Hour + 1, 0, 0);
                }

                AddTestForPL.TestDate = (DateTime) AddTestCalender.SelectedDate;
                AddTestForPL.DateAndHourOfTest = (DateTime) AddTestCalender.SelectedDate + ts;
                dateAndHourOfTestTextBlock.Text = AddTestForPL.DateAndHourOfTest.ToString();
                hours.SelectedIndex = 0;
                HoursErrors.Visibility = Visibility.Collapsed;
                HoursErrors.Text = "";
                hours.IsEnabled = true;
                AddDateErrors.Visibility = Visibility.Collapsed;
                AddDateErrors.Text = "";
            }
            catch (Exception exception)
            {
                AddDateErrors.Visibility = Visibility.Visible;
                AddDateErrors.Text = exception.Message;
                AddDateErrors.Foreground = Brushes.Red;
            }
        }

        public void blackoutFridaysAndSaterdays(DateTime startdate, DateTime enddate)
        {
            try
            {


                // step forward to the first friday
                while (startdate.DayOfWeek != DayOfWeek.Friday)
                    startdate = startdate.AddDays(1);

                while (startdate < enddate)
                {

                    AddTestCalender.BlackoutDates.Add(new CalendarDateRange(startdate));
                    AddTestCalender.BlackoutDates.Add(new CalendarDateRange(startdate.AddDays(1)));
                    //yield return startdate;
                    startdate = startdate.AddDays(7);
                }

                AddDateErrors.Visibility = Visibility.Collapsed;
                AddDateErrors.Text = "";
            }
            catch (Exception exception)
            {
                AddDateErrors.Visibility = Visibility.Visible;
                AddDateErrors.Text = exception.Message;

            }
        }

        public void Blackoutdays(int start, int startMonth)
        {
            try
            {
                if (DateTime.Now.Hour >= Configuration.EndOfWorkDay)
                {
                    AddTestCalender.BlackoutDates.Add(new CalendarDateRange(
                        DateTime.Today));
                }

                for (;
                    start <= DateTime.DaysInMonth(AddTestCalender.DisplayDate.Year, startMonth);
                    start++)
                {
                    AddTestForPL.TestDate = new DateTime(AddTestCalender.DisplayDate.Year,
                        AddTestCalender.DisplayDate.Month, start);
                    if (bl.AvailableTesterFound(AddTestForPL) == null)
                        AddTestCalender.BlackoutDates.Add(new CalendarDateRange(
                            new DateTime(AddTestCalender.DisplayDate.Year, startMonth, start)));

                }
            }
            catch (Exception exception)
            {
                AddDateErrors.Text = exception.Message;
                AddDateErrors.Visibility = Visibility.Visible;
            }
        }

        #endregion

        #region combobox
        private void TraineeIdComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                TesterByDistance = new List<Tester>();
                hours.Visibility = Visibility.Hidden;
                if (AddTraineeIdComboBox.SelectedIndex != -1)
                {
                    emptyAddTab();
                    if(sortTestersByAddress!=null)
                        if(sortTestersByAddress.IsBusy)
                        sortTestersByAddress.CancelAsync();

                    // AddTestCalender.DisplayDateStart = DateTime.Today;
                    AddTestCalender.BlackoutDates.Clear();
                    //var blackoutdates = from date in AddTestCalender.BlackoutDates select date;
                    //foreach (var blackoutdate in blackoutdates)
                    //{
                    //    AddTestCalender.BlackoutDates.Remove(blackoutdate);
                    //}
                    // AddTestCalender.DisplayDate = DateTime.Now;
                    AddTestForPL.TraineeId = AddTraineeIdComboBox.SelectedItem.ToString();
                    AddTestForPL.CarType = bl.GetListOfTrainees().Where(x => x.TraineeId == AddTestForPL.TraineeId)
                        .Select(x => x.Traineecar).FirstOrDefault();

                if (AddTestForPL.CarType == null)
                    throw new Exception("ERROR. Add a car type to the trainee first");
                carTypeTextBlock.Text = AddTestForPL.CarType.ToString();
                if(bl.GetListOfTesters().All(x=>x.Testercar!=AddTestForPL.CarType))
                    throw new Exception("ERROR. there are no testers with that car type.");

                Blackoutdays(1, AddTestCalender.DisplayDate.Month);
                AddTestCalender.DisplayDate = new DateTime(AddTestCalender.DisplayDate.Year,AddTestCalender.DisplayDate.Month,1);
               blackoutFridaysAndSaterdays(AddTestCalender.DisplayDate, AddTestCalender.DisplayDate.AddDays(60));

                    traineeAdd = bl.GetListOfTrainees().Where(x => x.TraineeId == AddTestForPL.TraineeId)
                    .Select(x => (x.TraineeAddress)).FirstOrDefault();
                    string address = traineeAdd.ToString();
                    if (traineeAdd != null)
                    {
                        traineeAddress.Text = address;
                        city.Text = traineeAdd.Value.City;
                        street.Text = traineeAdd.Value.Street;
                        stNumber.Text = traineeAdd.Value.BuildingNumber;
                        //sortTestersByAddress=new BackgroundWorker();
                        //sortTestersByAddress.DoWork += sortTestersByAddress_DoWork;
                        //sortTestersByAddress.RunWorkerCompleted += sortTestersByAddress_Complete;
                        //sortTestersByAddress.RunWorkerAsync();
                    }
                    else traineeAddress.Text = "Address not found";


                AddTestCalender.IsEnabled = true;
                    TestAddressBlock.IsEnabled = false;
                    findTesters.IsEnabled = false;
                    // Blackoutdays(DateTime.Today.Day, DateTime.Today.Month);
                    // blackoutFridaysAndSaterdays(DateTime.Today, DateTime.Today.AddDays(60));

                    //   int next = DateTime.Today.Month + 1;
                    // if (DateTime.Today.Month == 12)
                    //    next = 1;
                    //Blackoutdays(DateTime.Today.Day, next);


                }

            }
            catch (Exception exception)
            {
                TesterErrors.Text = exception.Message;
                TesterErrors.Visibility = Visibility.Visible;
                TesterErrors.Foreground = Brushes.Red;
                AddTestCalender.IsEnabled = false;
                // MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                //AddTraineeIdComboBox.SelectedIndex = -1;
            }

        }

        private void TesterComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                TestAddressErrors.Visibility = Visibility.Collapsed;
                TestAddressErrors.Text = "";
                hours.Items.Clear();
                hours.Visibility = Visibility.Hidden;
                AddTestForPL.TesterId = TesterComboBox.SelectedItem.ToString();
                string address = bl.GetListOfTesters().Where(x => x.TesterId == AddTestForPL.TesterId)
                    .Select(x => (x.TesterAdress.ToString())).FirstOrDefault();
                if (address != null)
                {
                    testerAddress.Text = address;
                }
                else testerAddress.Text = "Address not found";
                List<int> hoursOfTester = AvilabletestersForPL[AddTestForPL.TesterId];
                TimeSpan ts = new TimeSpan(hoursOfTester.First(), 0, 0);
                AddTestForPL.DateAndHourOfTest = AddTestForPL.DateAndHourOfTest.Date;
                AddTestForPL.DateAndHourOfTest= AddTestForPL.DateAndHourOfTest + ts;
                dateAndHourOfTestTextBlock.Text = AddTestForPL.DateAndHourOfTest.ToString();


                if (hoursOfTester != null)
                {
                    hours.Visibility = Visibility.Visible;
                    foreach (var time in hoursOfTester)
                    {
                        if (AddTestForPL.TestDate.Date != DateTime.Today)
                        {
                            string timeframe = "" + time + ":00-" + (time + 1) + ":00";
                            hours.Items.Add(timeframe);
                            
                        }
                         else if (DateTime.Now.Hour < time)
                        {
                            string timeframe = "" + time + ":00-" + (time + 1) + ":00";
                            hours.Items.Add(timeframe);

                        }
                    }


                }
            }
            catch (Exception exception)
            {

            }

        }
        #endregion
        


        private void Hours_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                //if (AddTestForPL.DateAndHourOfTest.Date == DateTime.Today &&
                  //  DateTime.Now.Hour >= hours.SelectedIndex + 9)
                    //throw new Exception("EROOR. Can't add an hour that has already passed.");
                TimeSpan ts;
                if(hours.Items.Count!=null)
                switch (hours.SelectedItem.ToString())
                {
                    case "9:00-10:00": //nine 
                        ts = new TimeSpan(9, 0, 0);
                        AddTestForPL.DateAndHourOfTest = AddTestForPL.DateAndHourOfTest.Date + ts;
                        dateAndHourOfTestTextBlock.Text = AddTestForPL.DateAndHourOfTest.ToString();
                        break;
                    case "10:00-11:00": //ten
                        ts = new TimeSpan(10, 0, 0);
                        AddTestForPL.DateAndHourOfTest = AddTestForPL.DateAndHourOfTest.Date + ts;
                        dateAndHourOfTestTextBlock.Text = AddTestForPL.DateAndHourOfTest.ToString();
                        break;
                    case "11:00-12:00": //eleven
                        ts = new TimeSpan(11, 0, 0);
                        AddTestForPL.DateAndHourOfTest = AddTestForPL.DateAndHourOfTest.Date + ts;
                        dateAndHourOfTestTextBlock.Text = AddTestForPL.DateAndHourOfTest.ToString();
                        break;
                    case "12:00-13:00": //twelve
                        ts = new TimeSpan(12, 0, 0);
                        AddTestForPL.DateAndHourOfTest = AddTestForPL.DateAndHourOfTest.Date + ts;
                        dateAndHourOfTestTextBlock.Text = AddTestForPL.DateAndHourOfTest.ToString();
                        break;
                    case "13:00-14:00": //one
                        ts = new TimeSpan(13, 0, 0);
                        AddTestForPL.DateAndHourOfTest = AddTestForPL.DateAndHourOfTest.Date + ts;
                        dateAndHourOfTestTextBlock.Text = AddTestForPL.DateAndHourOfTest.ToString();
                        break;
                    case "14:00-15:00": //two
                        ts = new TimeSpan(14, 0, 0);
                        AddTestForPL.DateAndHourOfTest = AddTestForPL.DateAndHourOfTest.Date + ts;
                        dateAndHourOfTestTextBlock.Text = AddTestForPL.DateAndHourOfTest.ToString();
                        break;

                }
                Save.IsEnabled=true;
                HoursErrors.Visibility = Visibility.Collapsed;
                HoursErrors.Text = "";

            }
            catch (Exception exception)
            {
                if (exception.Message != "Object reference not set to an instance of an object.")
                {

                HoursErrors.Visibility = Visibility.Visible;
                HoursErrors.Text = exception.Message;
                HoursErrors.Foreground = Brushes.Red;
                }
            }


        }

        #region test address

        private void sortTestersByAddress_DoWork(object sender, DoWorkEventArgs e)
        {
            //List<Tester> TestersWithcar = bl.GetListOfTesters().Where(x => x.Testercar == AddTestForPL.CarType && x.TesterAdress != null).ToList();
            //if (TestersWithcar.Count >= 2)
            //    e.Result = bl.TestersInArea(TestersWithcar, traineeAdd);
            //else e.Result = null;
            List<Tester> TestersWithcar = bl.GetListOfTesters().Where(x => x.Testercar == AddTestForPL.CarType && x.TesterAdress != null && AvilabletestersForPL.ContainsKey(x.TesterId)).ToList();
            if (TestersWithcar.Count > 0)
                e.Result = bl.TestersInArea(TestersWithcar, AddTestForPL.StartingPoint);
            

        }

        private void sortTestersByAddress_Complete(object sender, RunWorkerCompletedEventArgs e)
        {
            TesterByDistance = new List<Tester>();
            TesterByDistance = (List<Tester>)e.Result;
            List<string> fileredDistance = new List<string>();
            //List<string> unknownDistance=new List<string>();
            foreach (var tester in TesterByDistance)
            {
                if (AvilabletestersForPL.ContainsKey(tester.TesterId))
                    fileredDistance.Add(tester.TesterId);
            }
            //List<string> faultyAddress = new List<string>();
            //foreach (var key in AvilabletestersForPL.Keys)
            //{
            //    if (!TesterByDistance.Exists(x => x.TesterId == key))
            //    {
            //        fileredDistance.Add(key);
            //        if (bl.GetListOfTesters().Where(x => x.TesterId == key).Select(x => x.TesterAdress)
            //                .FirstOrDefault() != null)
            //        {
            //            faultyAddress.Add(key);
            //        }
            //    }

            //}
            //if (faultyAddress.Count > 0)
            //{
            //    TesterErrors.Text = "Warning: faulty addresses with testers: ";
            //    TesterErrors.Text += String.Join(",", faultyAddress);
            //    TesterErrors.Foreground = Brushes.Orange;
            //    TesterErrors.Visibility = Visibility.Visible;
            //}
            if (fileredDistance.Count != 0)
            {
            TesterComboBox.ItemsSource = fileredDistance;
            TesterComboBox.IsEnabled = true;
            TesterComboBox.Visibility = Visibility.Visible;
                TesterErrors.Text = "";
                TesterErrors.Visibility = Visibility.Collapsed;
            }
            else
            {
                TesterErrors.Text = "ERROR. No testers found.";
                TesterErrors.Foreground = Brushes.Red;
                TesterErrors.Visibility = Visibility.Visible;


            }
            findTesters.Content = "Find Testers";
        }

        private void ClosesedToTest_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!bl.NoConflictingTests(AddTestForPL))
                {
                 
                 throw new Exception("ERROR. can't add dates that are less than a week apart.");
                }

                TesterComboBox.Visibility = Visibility.Hidden;
                TestAddressErrors.Visibility = Visibility.Collapsed;
                checkAddress();
                     //   if (testAddress.Text==""||testAddress.Text==null)
                        //    throw new Exception("ERROR.Test address empty. cannot calculate distance. ");

                sortTestersByAddress = new BackgroundWorker();
                sortTestersByAddress.DoWork += sortTestersByAddress_DoWork;
                sortTestersByAddress.RunWorkerCompleted += sortTestersByAddress_Complete;
                sortTestersByAddress.RunWorkerAsync();
                findTesters.Content = "Waiting";
                TestAddressErrors.Text = "";
                TestAddressErrors.Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("not include numbers"))
                {
                    TestAddressErrors.Text = "ERROR. street/city must not include numbers.";
                }
                else if (ex.Message.Contains("only include numbers"))
                {
                    TestAddressErrors.Text = "ERROR. street number must only include numbers.";
                }
                else
                {
                TestAddressErrors.Text = ex.Message;
                }
                TestAddressErrors.Visibility = Visibility.Visible;
                TestAddressErrors.Foreground = Brushes.Red;


            }

        }
        
        void checkAddress()
        {
            //try
           // {
                bl.IsText(city.Text);
                bl.IsText(street.Text);
                bl.IsNumber(stNumber.Text);
                AddTestForPL.StartingPoint=new Address(street.Text,stNumber.Text,city.Text);
            //}
            //catch (Exception ex)
            //{
            //    TestAddressErrors.Text = ex.Message;
            //    TestAddressErrors.Foreground = Brushes.Red;
            //    TestAddressErrors.Visibility = Visibility.Visible;
            //}
        }

        #endregion

        private void checkErrors()
        {
            if(AddDateErrors.Text!="" && AddDateErrors.Text != null)
                throw new Exception();
            if (TesterErrors.Text != "" && TesterErrors.Text != null)
                throw new Exception();
            if (HoursErrors.Text != "" && HoursErrors.Text != null)
                throw new Exception();
            if (TestAddressErrors.Text != "" && TestAddressErrors.Text != null)
                throw new Exception();
            //if (blErrors.Text != "" && blErrors.Text != null)
            //    throw new Exception();


        }

        private void checkFields()
        {
            if(AddTestForPL.DateAndHourOfTest==null)
                throw new Exception();
            if (AddTestForPL.TesterId == null)
                throw new Exception();
            if (AddTestForPL.TestId == null)
                throw new Exception();
            if (AddTestForPL.TraineeId == null)
                throw new Exception();
            if (AddTestForPL.TestDate == null)
                throw new Exception();
            if (AddTestForPL.StartingPoint == null)
                throw new Exception();
        }
            
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {  
                    
                    checkErrors();
                    checkFields();
                    
                    bl.AddTest(AddTestForPL);
                    AddTestForPL = new Test();
                    TestAddGrid.DataContext = AddTestForPL;
                        testIdTextBlock.Text = (Configuration.FirstTestId).ToString("D" + 8);
                        AddTestCalender.DisplayDate = new DateTime(AddTestCalender.DisplayDate.Year, AddTestCalender.DisplayDate.Month, 1);
                TesterComboBox.IsEnabled = false;
                        MessageBox.Show("Test successfully added.", "", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                emptyAddTab();

            }
            catch (Exception ex)
            {
                //blErrors.Visibility = Visibility.Visible;
                //blErrors.Text = ex.Message;
                //blErrors.Foreground = Brushes.Red;
                MessageBox.Show("Can't add test. Check Errors and empty Fields.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                
            }
        }

    }
}
