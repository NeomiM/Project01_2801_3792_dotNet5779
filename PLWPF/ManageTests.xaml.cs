using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BE;
using BL;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class ManageTests : Window
    {
        private BL.IBL bl;
        private List<Trainee> AddTraineeListForPL;
        private Test AddTestForPL;

        public ManageTests()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            bl = IBL_imp.Instance;

            try
            {
                AddTestForPL = new Test();
                TestAddGrid.DataContext = AddTestForPL;
                AddTraineeListForPL = bl.readyTrainees();
                emptyAddTab();
                if (AddTraineeListForPL.Count == 0)
                    throw new Exception("ERROR. There are no trainees ready for a test.");
                this.AddTraineeIdComboBox.ItemsSource = AddTraineeListForPL.Select(x => x.TraineeId);
                AddTestCalender.DisplayDateStart = DateTime.Today;
                hours.IsEnabled = false;

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }





        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource testViewSource =
                ((System.Windows.Data.CollectionViewSource) (this.FindResource("testViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // testViewSource.Source = [generic data source]
        }

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Update.IsSelected)
            {
                try
                {
                    AddTestForPL = new Test();
                    TestAddGrid.DataContext = AddTestForPL;
                    AddTraineeListForPL = bl.readyTrainees();
                    emptyAddTab();
                    AddTestCalender.SelectedDate = null;
                    hours.IsEnabled = false;
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }


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

        private void TraineeIdComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {

                AddTestForPL.TraineeId = AddTraineeIdComboBox.Text;
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


        private void AddTestCalender_OnSelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                AddDateErrors.Visibility = Visibility.Collapsed;
                AddDateErrors.Text = "";
                TimeSpan ts = new TimeSpan(9, 0, 0);
                AddTestForPL.TestDate = (DateTime)AddTestCalender.SelectedDate;
                AddTestForPL.DateAndHourOfTest =(DateTime)AddTestCalender.SelectedDate+ts;
                dateAndHourOfTestTextBlock.Text = AddTestForPL.DateAndHourOfTest.ToString();
                hours.SelectedIndex = 0;
                HoursErrors.Visibility = Visibility.Collapsed;
                HoursErrors.Text = "";
                hours.IsEnabled = true;
            }
            catch (Exception exception)
            {
                AddDateErrors.Visibility = Visibility.Visible;
                AddDateErrors.Text = exception.Message;
                AddDateErrors.Foreground=Brushes.Red;
            }
        }

        public void emptyErrors()
        {
            AddDateErrors.Text = "";
            AddDateErrors.Visibility = Visibility.Collapsed;
            HoursErrors.Visibility = Visibility.Collapsed;
            HoursErrors.Text = "";

        }

        private void Hours_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if(AddTestForPL.DateAndHourOfTest.Date==DateTime.Today && DateTime.Now.Hour>= hours.SelectedIndex+9)
                    throw  new Exception("EROOR. Can't add an hour that has already passed.");
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
