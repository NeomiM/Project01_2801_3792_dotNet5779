using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        private List<Tester> TesterListForPL;
        //lists for schedule
        List<int> SundayHours = new List<int>();
        List<int> MondayHours = new List<int>();
        List<int> TuesdayHours = new List<int>();
        List<int> WednesdayHours = new List<int>();
        List<int> ThursdayHours = new List<int>();
        bool[,] hoursFromSchedualArr = new bool[5, 6];
        string winCondition;
        public TestersWindow()
        {
            InitializeComponent();
            TesterGrid.Visibility = Visibility.Hidden;
            bl = IBL_imp.Instance;
            TesterForPL = new Tester();
            this.TesterGrid.DataContext = TesterForPL;
            Save.IsEnabled = false;
            //manage calendar
            dateOfBirthDatePicker.DisplayDateEnd = DateTime.Now.AddYears(-1 * (int)BE.Configuration.MinAgeOFTester);
            dateOfBirthDatePicker.DisplayDateStart = DateTime.Now.AddYears(-1 * (int)BE.Configuration.MaxAgeOFTester);
            //enums
            this.testerGenderComboBox.ItemsSource = Enum.GetValues(typeof(BE.Gender));
            this.testercarComboBox.ItemsSource = Enum.GetValues(typeof(BE.CarType));
            //for numbers only
            this.testerIdTextBox.PreviewTextInput += TextBox_PreviewTextInputNumbers;
            this.phoneNumberTextBox.PreviewTextInput += TextBox_PreviewTextInputNumbers;
            this.maxDistanceForTestTextBox.PreviewTextInput += TextBox_PreviewTextInputNumbers;
            this.yearsOfExperienceTextBox.PreviewTextInput += TextBox_PreviewTextInputNumbers;
            this.maxTestsInaWeekTextBox.PreviewTextInput += TextBox_PreviewTextInputNumbers;
            //for letters only
            this.sirnameTextBox.PreviewTextInput += TextBox_PreviewTextInputLetters;
            this.firstNameTextBox.PreviewTextInput += TextBox_PreviewTextInputLetters;
            

        }

        #region manage buttons
        private void BackToMainMenue_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            //MainWindow.Show();
        }
        private void AddTester_Click(object sender, RoutedEventArgs e)
        {
            winCondition = "add";
            TesterComboBox.ItemsSource = bl.GetListOfTesters().Select(x => x.TesterId);
            TesterForPL = new Tester();
            openAll();
            TesterGrid.DataContext = TesterForPL;
            IdErrors.Text = "";
            testerIdTextBox.Visibility = Visibility.Visible;
            TesterGrid.Visibility = Visibility.Visible;
            TesterComboBox.Visibility = Visibility.Hidden;
            TesterGrid.IsEnabled = true;
            Save.Content = "Check";
        }

        private void UpdateTester_Click(object sender, RoutedEventArgs e)
        {
            winCondition = "update";
            try
            {
                TesterForPL = new Tester();
                TesterComboBox.SelectedItem = null;
                closeAlmostAll();
                TesterGrid.DataContext = TesterForPL;
                IdErrors.Text = "First Select ID";
                IdErrors.Foreground = Brushes.DarkBlue;
                TesterListForPL = bl.GetListOfTesters();
                TesterComboBox.ItemsSource = bl.GetListOfTesters().Select(x => x.TesterId);
                if (TesterListForPL.Count == 0)
                    throw new Exception("There are no Testers to update.");
                TesterGrid.Visibility = Visibility.Visible;
                TesterGrid.IsEnabled = true;
                
                Save.Content = "Check";
                TesterComboBox.Visibility = Visibility.Visible;
                testerIdTextBox.Visibility = Visibility.Hidden;

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteTester_Click(object sender, RoutedEventArgs e)
        {
            winCondition = "delete";
            try
            {
                Save.Content = "Delete";
                TesterForPL = new Tester();
                TesterGrid.Visibility = Visibility.Visible;
                TesterComboBox.Visibility = Visibility.Visible;
                TesterComboBox.SelectedItem = null;
                TesterGrid.DataContext = TesterForPL;
                closeAlmostAll();
                IdErrors.Text = "First Select ID";
                TesterListForPL = bl.GetListOfTesters();
                TesterComboBox.ItemsSource = bl.GetListOfTesters().Select(x => x.TesterId);
                if (TesterListForPL.Count == 0)
                    throw new Exception("There are no testers to update.");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (Save.Content == "Add")
            {
                try
                {
                    //TesterForPL.TesterAddress = new Address(Street.Text, BuidingNumber.Text, City.Text);
                    TesterForPL.setSchedual(SundayHours, MondayHours, TuesdayHours, WednesdayHours, ThursdayHours);
                    bl.AddTester(TesterForPL);
                    TesterGrid.Visibility = Visibility.Hidden;
                    MessageBox.Show("Tester saved successfully", "", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    TesterGrid.Visibility = Visibility.Visible;
                }

            }
            if (Save.Content == "Update")
            {
                //TesterForPL.TesterAddress = new Address(Street.Text, BuidingNumber.Text, City.Text);

                bl.UpdateTester(TesterForPL);
                TesterGrid.Visibility = Visibility.Hidden;
                MessageBox.Show("Tester saved successfully", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            if (Save.Content == "Delete")
            {
                MessageBoxResult dialogResult = MessageBox.Show("Are you sure you want to delete?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                if (dialogResult == MessageBoxResult.Yes)
                {
                    bl.DeleteTester(TesterForPL);
                    TesterGrid.Visibility = Visibility.Hidden;
                    MessageBox.Show("Tester successfully deleted.", "", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    TesterForPL = new Tester();
                    TesterGrid.DataContext = TesterForPL;
                }
                else if (dialogResult == MessageBoxResult.No)
                {
                    TesterForPL = new Tester();
                    TesterGrid.DataContext = TesterForPL;
                    TesterGrid.Visibility = Visibility.Hidden;
                    MessageBox.Show("Tester not deleted.", "", MessageBoxButton.OK, MessageBoxImage.Asterisk);

                }
            }

            if (Save.Content == "Check")
            {

                if (noErrors() && TesterComboBox.Visibility == Visibility.Hidden)
                {
                    if (bl.TesterInSystem(TesterForPL.TesterId))
                    {
                        MessageBoxResult dialogResult = MessageBox.Show("Tester alredy exists in the system! Do you want to update?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                        if (dialogResult == MessageBoxResult.Yes)
                        {
                            TesterComboBox.Visibility = Visibility.Visible;
                            TesterComboBox.SelectedValue = (object)TesterForPL.TesterId;
                            TesterForPL = bl.GetListOfTesters()
                                .FirstOrDefault(x => x.TesterId == testerIdTextBox.Text);
                        }
                        else if (dialogResult == MessageBoxResult.No)
                        {
                            TesterForPL = new Tester();
                            TesterGrid.DataContext = TesterForPL;
                        }

                    }
                    else Save.Content = "Add";
                }
                else if (noErrors())
                {
                    Save.Content = "Update";
                }
                else
                {
                    MessageBox.Show("Can't add Tester. Fill ID " +
                                    "and fix errors.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        #endregion

        #region additions
        public void closeAlmostAll()
        {
            testerIdTextBox.IsEnabled = false;
            firstNameTextBox.IsEnabled = false;
            sirnameTextBox.IsEnabled = false;
            dateOfBirthDatePicker.IsEnabled = false;
            testerGenderComboBox.IsEnabled = false;
            phoneNumberTextBox.IsEnabled = false;
            emailTextBox.IsEnabled = false;
            testercarComboBox.IsEnabled = false;
            maxDistanceForTestTextBox.IsEnabled = false;
            yearsOfExperienceTextBox.IsEnabled = false;
            maxTestsInaWeekTextBox.IsEnabled = false;
            schedualExpander.IsEnabled = false;
        }

        public void openAll()
        {
            testerIdTextBox.IsEnabled = true;
            firstNameTextBox.IsEnabled = true;
            sirnameTextBox.IsEnabled = true;
            dateOfBirthDatePicker.IsEnabled = true;
            testerGenderComboBox.IsEnabled = true;
            phoneNumberTextBox.IsEnabled = true;
            emailTextBox.IsEnabled = true;
            testercarComboBox.IsEnabled = true;
            maxDistanceForTestTextBox.IsEnabled = true;
            yearsOfExperienceTextBox.IsEnabled = true;
            maxTestsInaWeekTextBox.IsEnabled = true;
            schedualExpander.IsEnabled = true;
        }

        public bool noErrors()
        {

            try
            {
                if (TesterForPL.TesterId == null)
                    throw new Exception();
                if (IdErrors.Text != "")
                    throw new Exception();
                if (NameErrors.Text.Contains("ERROR"))
                    throw new Exception();
                if (SirNameErrors.Text.Contains("ERROR"))
                    throw new Exception();
                if (PhoneNumberErrors.Text.Contains("ERROR"))
                    throw new Exception();
                if (EmailErrors.Text.Contains("ERROR"))
                    throw new Exception();
                if (yearsOfExperienceErrors.Text.Contains("ERROR"))
                    throw new Exception();
                return true;
            }
            catch (Exception exception)
            {
                return false;
            }
        }

        //for numbers only
        private void TextBox_PreviewTextInputNumbers(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        //for numbers only
        private void TextBox_PreviewTextInputLetters(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^A-z]+").IsMatch(e.Text);
        }
        #endregion

        //check errors
        #region check id
        private void TesterIdTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            throw new NotImplementedException();
        }
        private void TesterComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IdErrors.Text = "";
            if (Save.Content == "Check")
            {
                openAll();
            }

            string id = (string)TesterComboBox.SelectedItem;
            TesterForPL = bl.GetListOfTesters().FirstOrDefault(a => a.TesterId == id);
            {//schedual
                hoursFromSchedualArr = TesterForPL.getSchedual();
                for(int i = 0; i < 5; i++)
                {
                    if (hoursFromSchedualArr[0,i])
                    {
                        schedualListBox.SelectedIndex = i;
                    }
                }
                
            }
            this.TesterGrid.DataContext = TesterForPL;
        }

        private void testerIdTextBox_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            try
            {
                bl.CheckId(TesterForPL.TesterId);
            }
            catch
            {
                IdErrors.Text = "";
                IdErrors.Foreground = Brushes.Red;
                testerIdTextBox.BorderBrush = Brushes.Red;
            }
        }

        private void testerIdTextBox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            IdErrors.Text = "";
            IdErrors.Foreground = Brushes.Black;
            testerIdTextBox.BorderBrush = Brushes.Black;
        }
        #endregion

        #region check names
        private void firstNameTextBox_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            try
            {
                bl.IsText(TesterForPL.FirstName);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("ERROR"))
                {
                    NameErrors.Foreground = Brushes.Red;
                    firstNameTextBox.BorderBrush = Brushes.Red;

                }
                else
                {
                    NameErrors.Foreground = Brushes.Orange;
                    firstNameTextBox.BorderBrush = Brushes.Orange;
                }
                NameErrors.Text = ex.Message;

            }
        }

        private void firstNameTextBox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            NameErrors.Text = ";";
            NameErrors.Foreground = Brushes.Black;
            firstNameTextBox.BorderBrush = Brushes.Black;
        }

        private void sirnameTextBox_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            try
            {
                bl.IsText(TesterForPL.Sirname);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("ERROR"))
                {
                    SirNameErrors.Foreground = Brushes.Red;
                    sirnameTextBox.BorderBrush = Brushes.Red;

                }
                else
                {
                    SirNameErrors.Foreground = Brushes.Orange;
                    sirnameTextBox.BorderBrush = Brushes.Orange;
                }

                SirNameErrors.Text = ex.Message;

            }
        }
        private void sirnameTextBox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            SirNameErrors.Text = "";
            SirNameErrors.Foreground = Brushes.Black;
            sirnameTextBox.BorderBrush = Brushes.Black;
        }
        #endregion

        #region check phone number
        private void phoneNumberTextBox_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            try
            {
                bl.IsNumber(TesterForPL.PhoneNumber);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("ERROR"))
                {
                    PhoneNumberErrors.Foreground = Brushes.Red;
                    phoneNumberTextBox.BorderBrush = Brushes.Red;
                }
                else
                {
                    PhoneNumberErrors.Foreground = Brushes.Orange;
                    phoneNumberTextBox.BorderBrush = Brushes.Orange;
                }
                PhoneNumberErrors.Text = ex.Message;
            }
        }
        #endregion

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource testerViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("testerViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // testerViewSource.Source = [generic data source]
        }

        #region Schedual
        #endregion
        private void NextDayButton_Click(object sender, RoutedEventArgs e)
        {
            if(winCondition == "add")
            {
                switch (dayLabel.Content)//change the Label
                {

                    case "Sunday":
                        dayLabel.Content = "Monday";
                        break;

                    case "Monday":
                        dayLabel.Content = "Tuesday";
                        break;

                    case "Tuesday":
                        dayLabel.Content = "Wednesday";
                        break;

                    case "Wednesday":
                        dayLabel.Content = "Thursday";
                        break;

                    case "Thursday":
                        dayLabel.Content = "Sunday";
                        break;
                }
            }
            else
            {
                switch (dayLabel.Content)//change the Label
                {

                    case "Sunday":
                        dayLabel.Content = "Monday";
                        for (int i = 0; i < 5; i++)
                        {
                            if (hoursFromSchedualArr[1, i])
                            {
                                schedualListBox.SelectedIndex = i;
                            }
                        }

                        break;

                    case "Monday":
                        dayLabel.Content = "Tuesday";
                        for (int i = 0; i < 5; i++)
                        {
                            if (hoursFromSchedualArr[2, i])
                            {
                                schedualListBox.SelectedIndex = i;
                            }
                        }
                        break;

                    case "Tuesday":
                        dayLabel.Content = "Wednesday";
                        for (int i = 0; i < 5; i++)
                        {
                            if (hoursFromSchedualArr[3, i])
                            {
                                schedualListBox.SelectedIndex = i;
                            }
                        }
                        break;

                    case "Wednesday":
                        dayLabel.Content = "Thursday";
                        for (int i = 0; i < 5; i++)
                        {
                            if (hoursFromSchedualArr[4, i])
                            {
                                schedualListBox.SelectedIndex = i;
                            }
                        }
                        break;

                    case "Thursday":
                        dayLabel.Content = "Sunday";
                        for (int i = 0; i < 5; i++)
                        {
                            if (hoursFromSchedualArr[5, i])
                            {
                                schedualListBox.SelectedIndex = i;
                            }
                        }
                        break;
                }
            }
        }

        private void SchedualListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (dayLabel.Content)//change the hours in list
            {

                case "Sunday":
                    SundayHours.Clear();
                    foreach (object SelectedItem in schedualListBox.SelectedItems)
                    {
                        SundayHours.Add(schedualListBox.Items.IndexOf(SelectedItem));
                    }
                    break;

                case "Monday":
                    MondayHours.Clear();
                    foreach (object SelectedItem in schedualListBox.SelectedItems)
                    {
                        MondayHours.Add(schedualListBox.Items.IndexOf(SelectedItem));
                    }
                    break;

                case "Tuesday":
                    TuesdayHours.Clear();
                    foreach (object SelectedItem in schedualListBox.SelectedItems)
                    {
                        TuesdayHours.Add(schedualListBox.Items.IndexOf(SelectedItem));
                    }
                    break;

                case "Wednesday":
                    WednesdayHours.Clear();

                    foreach (object SelectedItem in schedualListBox.SelectedItems)
                    {
                        WednesdayHours.Add(schedualListBox.Items.IndexOf(SelectedItem));
                    }
                    break;

                case "Thursday":
                    ThursdayHours.Clear();
                    foreach (object SelectedItem in schedualListBox.SelectedItems)
                    {
                        ThursdayHours.Add(schedualListBox.Items.IndexOf(SelectedItem));
                    }
                    break;
            }
        }
    }
}
