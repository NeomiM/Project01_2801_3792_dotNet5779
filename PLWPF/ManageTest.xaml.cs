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
    /// Interaction logic for ManageTest.xaml
    /// </summary>
    public partial class ManageTest : Window
    {
        private BL.IBL bl;
        private BE.Test TestForPL;
        private List<Test> TestListForPL;
        public ManageTest()
        {
            InitializeComponent();
            this.DataContext = TestForPL;
           
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            bl = IBL_imp.Instance;
            TestForPL = new Test();
            u_testerIdTextBox.IsReadOnly = true;
            u_traineeIdTextBox.IsReadOnly = true;
            //update ta
            try
            {
                TestListForPL = bl.GetListOfTests();
                foreach (var test in TestListForPL)
                {
                    var id = test.TestId;
                    u_testIdComboBox.Items.Add(id.ToString());
                  }
                //UpdateDockPanel.IsEnabled = false;
                //closeAlmostAll();
                u_testIdComboBox.IsEnabled = true;
                //this.u_testIdComboBox.DataContext = TestListForPL.Select;
                //u_testIdComboBox.ItemsSource = bl.GetListOfTests().Select(x => x.TestId).ToString();
                if (TestListForPL.Count == 0)
                    throw new Exception("There are no Testers to update");
                //UpdateDockPanel.IsEnabled = true;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #region function

        public void closeAlmostAll()
        {
            //TestDetailsGrid.IsEnabled = false;
            u_testerIdTextBox.IsEnabled = false;
            u_traineeIdTextBox.IsEnabled = false;
            u_commentsGroupBox.IsEnabled = false;
            testPassedRadioButton.IsEnabled = false;
            BoolItemsGrid.IsEnabled = false;
            saveButton.IsEnabled = false;
        }

        public void openAll()
        {
            //TestDetailsGrid.IsEnabled = true;
            u_testerIdTextBox.IsEnabled = true;
            u_traineeIdTextBox.IsEnabled = true;
            u_commentsGroupBox.IsEnabled = true;
            testPassedRadioButton.IsEnabled = true;
            BoolItemsGrid.IsEnabled = true;
            saveButton.IsEnabled = true;
        }

        public bool ThereIsEmptyFiles()
        {
            bool allEmpty = true;
            if (checkMirrorsCheckBox.IsChecked == false || checkMirrorsCheckBox.IsChecked == null) allEmpty = false;
            if (imediateStopCheckBox.IsChecked == false || imediateStopCheckBox.IsChecked == null) allEmpty = false;
            if (keptDistanceCheckBox.IsChecked == false || keptDistanceCheckBox.IsChecked == null) allEmpty = false;
            if (keptRightofPresidenceCheckBox.IsChecked == false || keptRightofPresidenceCheckBox.IsChecked == null) allEmpty = false;
            if (parkingCheckBox.IsChecked == false || parkingCheckBox.IsChecked == null) allEmpty = false;
            if (reverseParkingCheckBox.IsChecked == false || reverseParkingCheckBox.IsChecked == null) allEmpty = false;
            if (rightTurnCheckBox.IsChecked == false || rightTurnCheckBox.IsChecked == null) allEmpty = false;
            if (stoppedAtcrossWalkCheckBox.IsChecked == false || stoppedAtcrossWalkCheckBox.IsChecked == null) allEmpty = false;
            if (stoppedAtRedCheckBox.IsChecked == false || stoppedAtRedCheckBox.IsChecked == null) allEmpty = false;
            if (usedSignalCheckBox.IsChecked == false || usedSignalCheckBox.IsChecked == null) allEmpty = false;
            return allEmpty;
        }

        #endregion

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource testViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("testViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // testViewSource.Source = [generic data source]
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TestIdComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(saveButton.Content == "Check")
            {
                openAll();
                //UpdateDockPanel.IsEnabled = true;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if(!ThereIsEmptyFiles())
                temp.Visibility = Visibility.Visible;
        }
    }
}
