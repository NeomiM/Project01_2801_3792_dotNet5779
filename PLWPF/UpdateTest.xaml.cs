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
using BE;
using BL;


namespace PLWPF
{
    /// <summary>
    /// Interaction logic for UpdateTest.xaml
    /// </summary>
    public partial class UpdateTest : UserControl
    {
        private BL.IBL bl;
        private BE.Test TestForPL;
        private List<Test> TestListForPL;
        public UpdateTest()
        {
            InitializeComponent();
            

            //update test
            try
            {
                u_testerIdTextBox.IsReadOnly = true;
                u_traineeIdTextBox.IsReadOnly = true;

                bl = IBL_imp.Instance;
                TestForPL = new Test();
                TestListForPL = bl.GetListOfTests();
                //this.UpdateTab.DataContext = TestForPL;

                this.u_testIdComboBox.ItemsSource = bl.GetListOfTests();
                //u_testIdComboBox.ItemsSource = bl.GetListOfTests().Select(x => x.TestId);
                if (TestListForPL.Count == 0)
                    throw new Exception("There are no Testers to update");
                closeAlmostAll();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #region function
        public void closeAlmostAll()
        {
            u_testerIdTextBox.IsEnabled = false;
            u_traineeIdTextBox.IsEnabled = false;
            u_commentsGroupBox.IsEnabled = false;
            testPassedCheckBox.IsEnabled = false;
            BoolItemsGrid.IsEnabled = false;
            saveButton.IsEnabled = false;
        }

        public void openAll()
        {
            u_testerIdTextBox.IsEnabled = true;
            u_traineeIdTextBox.IsEnabled = true;
            u_commentsGroupBox.IsEnabled = true;
            testPassedCheckBox.IsEnabled = true;
            BoolItemsGrid.IsEnabled = true;
            saveButton.IsEnabled = true;
        }

        public bool ThereNoEmptyFiles()
        {
            bool allfill = true;
            
            if (checkMirrorsCheckBox.IsChecked == false || checkMirrorsCheckBox.IsThreeState == false) allfill = false;
            if (imediateStopCheckBox.IsChecked == false || imediateStopCheckBox.IsThreeState == false) allfill = false;
            if (keptDistanceCheckBox.IsChecked == false || keptDistanceCheckBox.IsThreeState == false) allfill = false;
            if (keptRightofPresidenceCheckBox.IsChecked == false || keptRightofPresidenceCheckBox.IsThreeState == false) allfill = false;
            if (parkingCheckBox.IsChecked == false || parkingCheckBox.IsThreeState == false) allfill = false;
            if (reverseParkingCheckBox.IsChecked == false || reverseParkingCheckBox.IsThreeState == false) allfill = false;
            if (rightTurnCheckBox.IsChecked == false || rightTurnCheckBox.IsThreeState == false) allfill = false;
            if (stoppedAtcrossWalkCheckBox.IsChecked == false || stoppedAtcrossWalkCheckBox.IsThreeState == false) allfill = false;
            if (stoppedAtRedCheckBox.IsChecked == false || stoppedAtRedCheckBox.IsThreeState == false) allfill = false;
            if (usedSignalCheckBox.IsChecked == false || usedSignalCheckBox.IsThreeState == false) allfill = false;
            return allfill;
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

        private void TestIdComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (Save.Content == "Check")
            //openAll();
            string id = (string)u_testIdComboBox.SelectedItem.ToString();
            TestForPL = bl.GetListOfTests().FirstOrDefault(a => a.TestId == id);

            this.TestDetailsGrid.DataContext = TestForPL;
            openAll();
            //this.u_testerIdTextBox.Text = TestForPL.TesterId;
            //this.u_traineeIdTextBox.Text = TestForPL.TraineeId;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            //if(saveButton.Content == "Check")
            //{
                if (!ThereNoEmptyFiles())
                    warningTextBlock.Visibility = Visibility.Visible;
                if (testPassedCheckBox.IsChecked == false || testPassedCheckBox.IsThreeState == false)
                    warningTextBlock1.Visibility = Visibility.Visible;
                if (warningTextBlock.Visibility == Visibility.Hidden && warningTextBlock1.Visibility == Visibility.Hidden)
            {
                //saveButton.Content = "Save";
                Button.IsEnabled = true;
            }
            //}
            //if(saveButton.Content == "Save")
            //{
            //    bl.UpdateTest(TestForPL);
            //    MessageBox.Show("Test saved successfully", "", MessageBoxButton.OK, MessageBoxImage.Information);
            //}
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (keptDistanceCheckBox.IsChecked == true)
                TestForPL.KeptDistance = true;
            else TestForPL.KeptDistance = false;

            if (parkingCheckBox.IsChecked == true)
                TestForPL.Parking = true;
            else TestForPL.Parking = false;

            if (reverseParkingCheckBox.IsChecked == true)
                TestForPL.ReverseParking = true;
            else TestForPL.ReverseParking = false;

            if (checkMirrorsCheckBox.IsChecked == true)
                TestForPL.CheckMirrors = true;
            else TestForPL.CheckMirrors = false;

            if (usedSignalCheckBox.IsChecked == true)
                TestForPL.UsedSignal = true;
            else TestForPL.UsedSignal = false;

            if (keptRightofPresidenceCheckBox.IsChecked == true)
                TestForPL.KeptRightofPresidence = true;
            else TestForPL.KeptRightofPresidence = false;

            if (stoppedAtRedCheckBox.IsChecked == true)
                TestForPL.StoppedAtRed = true;
            else TestForPL.StoppedAtRed = false;

            if (stoppedAtcrossWalkCheckBox.IsChecked == true)
                TestForPL.StoppedAtcrossWalk = true;
            else TestForPL.StoppedAtcrossWalk = false;

            if (rightTurnCheckBox.IsChecked == true)
                TestForPL.RightTurn = true;
            else TestForPL.RightTurn = false;

            if (imediateStopCheckBox.IsChecked == true)
                TestForPL.ImediateStop = true;
            else TestForPL.ImediateStop = false;

            if (testPassedCheckBox.IsChecked == true)
                TestForPL.TestPassed = true;
            else TestForPL.TestPassed = false;

            TestForPL.RemarksOnTest = commentsTextBox.Text;

            bl.UpdateTest(TestForPL);

            MessageBox.Show("Test saved successfully", "", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
