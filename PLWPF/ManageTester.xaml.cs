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
using System.Windows.Shapes;
using BE;
using BL;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for TestersWindow.xaml
    /// </summary>
    public partial class TestersWindow : Window
    {
        private BL.IBL bl;
        private BE.Tester TesterForPL;
        public TestersWindow()
        {
            InitializeComponent();
            TesterGrid.Visibility = Visibility.Hidden;
            bl = IBL_imp.Instance;
            TesterForPL = new Tester();
            this.TesterGrid.DataContext = TesterForPL;
            Save.IsEnabled = false;
            dateOfBirthDatePicker.DisplayDateEnd = DateTime.Now.AddYears((int)BE.Configuration.MinAgeOFTester);
            dateOfBirthDatePicker.DisplayDateStart = DateTime.Now.AddYears((int)BE.Configuration.MaxAgeOFTester);
        }

        #region manage buttons
        private void BackToMainMenue_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            //MainWindow.Show();
        }
        private void AddTester_Click(object sender, RoutedEventArgs e)
        {
            TesterGrid.Visibility = Visibility.Visible;
            Save.Content = "Add";
        }

        private void UpdateTester_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<Tester> Testers = IBL_imp.Instance.GetListOfTesters();
                if (Testers.Count == 0)
                    throw new Exception("There are no testers to update.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteTester_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<Tester> Testers = IBL_imp.Instance.GetListOfTesters();
                if (Testers.Count == 0)
                    throw new Exception("There are no testerss to update.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if ((string)Save.Content == "Add")
            {

            }
            if ((string)Save.Content == "Update")
            {

            }
            if ((string)Save.Content == "Delete")
            {

            }
        }
        #endregion




        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource testerViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("testerViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // testerViewSource.Source = [generic data source]
        }





        private void testerGenderComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            testerGenderComboBox.ItemsSource = Enum.GetValues(typeof(BE.Gender)).Cast<BE.Gender>();
        }

        private void testercarComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            testercarComboBox.ItemsSource = Enum.GetValues(typeof(BE.CarType)).Cast<BE.CarType>();
        }







        //private void AddTesterButton_Click(object sender, RoutedEventArgs e)
        //{
        //    AddTesterWindow AddWindow = new AddTesterWindow();
        //    this.Close();
        //    AddWindow.Show();
        //}

    }
}
