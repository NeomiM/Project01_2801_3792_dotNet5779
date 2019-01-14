using System;
using System.Collections.Generic;
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

        public AddTestUserControl()
        {
            InitializeComponent();
            bl = IBL_imp.Instance;

            try
            {
                AddDateErrors.Foreground = Brushes.Red;
                HoursErrors.Foreground = Brushes.Red;
                AddTestForPL = new Test();
                TestAddGrid.DataContext = AddTestForPL;
                AddTraineeListForPL = bl.readyTrainees();
                emptyAddTab();
                if (AddTraineeListForPL.Count == 0)
                    throw new Exception("ERROR. There are no trainees ready for a test.");
                this.AddTraineeIdComboBox.ItemsSource = AddTraineeListForPL.Select(x => x.TraineeId);
                AddTestCalender.DisplayDateStart = DateTime.Today;
                AddTestCalender.IsEnabled = false;
                hours.IsEnabled = false;
                testIdTextBlock.Text = Configuration.FirstTestId.ToString("D" + 8);
                blackoutFridaysAndSaterdays(DateTime.Today,DateTime.Today.AddDays(60));
                
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        public void emptyAddTab()
        {
            dateAndHourOfTestTextBlock.Text = "";
            carTypeTextBlock.Text = "";
            testAddress.Text = "";
            hours.SelectedItem = null;
            emptyErrors();
        }
        public void emptyErrors()
        {
            AddDateErrors.Text = "";
            AddDateErrors.Visibility = Visibility.Collapsed;
            HoursErrors.Visibility = Visibility.Collapsed;
            HoursErrors.Text = "";

        }

        private void TraineeIdComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Blackoutdays();
                AddTestCalender.IsEnabled = true;
                AddTestForPL.TraineeId = AddTraineeIdComboBox.SelectedItem.ToString();
                AddTestForPL.CarType = bl.GetListOfTrainees().Where(x => x.TraineeId == AddTestForPL.TraineeId)
                    .Select(x => x.Traineecar).FirstOrDefault();
                if (AddTestForPL.CarType == null)
                    throw new Exception("ERROR. Add a car type to the trainee first");
                carTypeTextBlock.Text = AddTestForPL.CarType.ToString();

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                AddTraineeIdComboBox.SelectedIndex = -1;
            }

        }

        #region calender
        private void AddTestCalender_OnDisplayDateChanged(object sender, CalendarDateChangedEventArgs e)
        {
            //blacks out fridays and saterdays when month is changed
            blackoutFridaysAndSaterdays((DateTime)AddTestCalender.DisplayDate, ((DateTime)AddTestCalender.DisplayDate).AddDays(60));
            Blackoutdays();
            
        }

        private void AddTestCalender_OnSelectedDatesChanged(object sender, SelectionChangedEventArgs selectionChangedEventArgs)
        {
            try
            {
                testerIdTextBlock.Text= bl.AvailableTesterFound(AddTestForPL);
                AddTestForPL.TesterId = testerIdTextBlock.Text;

                AddDateErrors.Visibility = Visibility.Collapsed;
                AddDateErrors.Text = "";
                TimeSpan ts = new TimeSpan(9, 0, 0);
                if ((DateTime) AddTestCalender.SelectedDate == DateTime.Today)
                {
                    ts = new TimeSpan(DateTime.Now.Hour+1, 0, 0);
                }
                
                AddTestForPL.TestDate = (DateTime)AddTestCalender.SelectedDate;
                AddTestForPL.DateAndHourOfTest = (DateTime)AddTestCalender.SelectedDate + ts;
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

        public void Blackoutdays()
        {
            try
            {


                for (int i = 1;
                    i <= DateTime.DaysInMonth(AddTestCalender.DisplayDate.Year, AddTestCalender.DisplayDate.Month);
                    i++)
                {

                    if (bl.AvailableTesterFound(AddTestForPL) == null)
                        AddTestCalender.BlackoutDates.Add(new CalendarDateRange(
                            new DateTime(AddTestCalender.DisplayDate.Year, AddTestCalender.DisplayDate.Month, i)));

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
                if (AddTestForPL.DateAndHourOfTest.Date == DateTime.Today && DateTime.Now.Hour >= hours.SelectedIndex + 9)
                    throw new Exception("EROOR. Can't add an hour that has already passed.");
                TimeSpan ts;
                switch (hours.SelectedIndex)
                {
                    case 0://nine 
                        ts = new TimeSpan(9, 0, 0);
                        AddTestForPL.DateAndHourOfTest = AddTestForPL.DateAndHourOfTest.Date + ts;
                        dateAndHourOfTestTextBlock.Text = AddTestForPL.DateAndHourOfTest.ToString();
                        break;
                    case 1://ten
                        ts = new TimeSpan(10, 0, 0);
                        AddTestForPL.DateAndHourOfTest = AddTestForPL.DateAndHourOfTest.Date + ts;
                        dateAndHourOfTestTextBlock.Text = AddTestForPL.DateAndHourOfTest.ToString();
                        break;
                    case 2://eleven
                        ts = new TimeSpan(11, 0, 0);
                        AddTestForPL.DateAndHourOfTest = AddTestForPL.DateAndHourOfTest.Date + ts;
                        dateAndHourOfTestTextBlock.Text = AddTestForPL.DateAndHourOfTest.ToString();
                        break;
                    case 3://twelve
                        ts = new TimeSpan(12, 0, 0);
                        AddTestForPL.DateAndHourOfTest = AddTestForPL.DateAndHourOfTest.Date + ts;
                        dateAndHourOfTestTextBlock.Text = AddTestForPL.DateAndHourOfTest.ToString();
                        break;
                    case 4://one
                        ts = new TimeSpan(13, 0, 0);
                        AddTestForPL.DateAndHourOfTest = AddTestForPL.DateAndHourOfTest.Date + ts;
                        dateAndHourOfTestTextBlock.Text = AddTestForPL.DateAndHourOfTest.ToString();
                        break;
                    case 5://two
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
                HoursErrors.Visibility = Visibility.Visible;
                HoursErrors.Text = exception.Message;
                HoursErrors.Foreground = Brushes.Red;
            }


        }







    }
}
