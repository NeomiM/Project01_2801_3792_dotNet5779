using System;
using System.Collections.Generic;
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

        public AddTestUserControl()
        {
            InitializeComponent();
            bl = IBL_imp.Instance;

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
                MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        public void emptyAddTab()
        {
            dateAndHourOfTestTextBlock.Text = "";
            testerAddress.Text = "";
            carTypeTextBlock.Text = "";
            testAddress.Text = "";
            hours.SelectedItem = null;
            traineeAddress.Text = "";
            emptyErrors();
            TesterComboBox.SelectedIndex = -1;
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

        private void TraineeIdComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                hours.Visibility = Visibility.Hidden;
                if (AddTraineeIdComboBox.SelectedIndex != -1)
                {
                    emptyAddTab();


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

                    string address = bl.GetListOfTrainees().Where(x => x.TraineeId == AddTestForPL.TraineeId)
                        .Select(x => (x.TraineeAddress.ToString())).FirstOrDefault();
                    if (address != null)
                    {
                        traineeAddress.Text = bl.GetListOfTrainees().Where(x => x.TraineeId == AddTestForPL.TraineeId)
                            .Select(x => (x.TraineeAddress.ToString())).FirstOrDefault();
                    }
                    else traineeAddress.Text = "Address not found";

                if (AddTestForPL.CarType == null)
                    throw new Exception("ERROR. Add a car type to the trainee first");
                carTypeTextBlock.Text = AddTestForPL.CarType.ToString();
                if(bl.GetListOfTesters().All(x=>x.Testercar!=AddTestForPL.CarType))
                    throw new Exception("ERROR. there are no testers with that car type.");
                AddTestCalender.IsEnabled = true;
               // Blackoutdays(DateTime.Today.Day, DateTime.Today.Month);
              // blackoutFridaysAndSaterdays(DateTime.Today, DateTime.Today.AddDays(60));

                Blackoutdays(1, AddTestCalender.DisplayDate.Month);
                AddTestCalender.DisplayDate = new DateTime(AddTestCalender.DisplayDate.Year,AddTestCalender.DisplayDate.Month,1);
               blackoutFridaysAndSaterdays(AddTestCalender.DisplayDate, AddTestCalender.DisplayDate.AddDays(60));
             
                int next = DateTime.Today.Month + 1;
               // if (DateTime.Today.Month == 12)
                //    next = 1;
                //Blackoutdays(DateTime.Today.Day, next);
                

                }

            }
            catch (Exception exception)
            {
                TesterErrors.Text = exception.Message;
                TesterErrors.Visibility = Visibility.Visible;
                AddTestCalender.IsEnabled = false;
                // MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                //AddTraineeIdComboBox.SelectedIndex = -1;
            }

        }

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
                TesterComboBox.IsEnabled = true;
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


        private void TesterComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                hours.Items.Clear();
                AddTestForPL.TesterId = TesterComboBox.SelectedItem.ToString();
                string address = bl.GetListOfTesters().Where(x => x.TesterId == AddTestForPL.TesterId)
                    .Select(x => (x.TesterAdress.ToString())).FirstOrDefault();
                if (address != null)
                {
                    testerAddress.Text = bl.GetListOfTrainees().Where(x => x.TraineeId == AddTestForPL.TraineeId)
                        .Select(x => (x.TraineeAddress.ToString())).FirstOrDefault();
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
    }
}
