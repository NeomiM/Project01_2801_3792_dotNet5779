using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Security.Cryptography.X509Certificates;
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
    /// Interaction logic for ManageTrainee.xaml
    /// </summary>
    public partial class ManageTrainee : Window
    {
        private BL.IBL bl;
        private BE.Trainee TraineeForPL;
        private List<Trainee> TraineeListForPL;
        public ManageTrainee()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            TraineeGrid.Visibility = Visibility.Hidden;
            bl = IBL_imp.Instance;
            TraineeForPL = new Trainee();
            this.TraineeGrid.DataContext = TraineeForPL;
            this.TraineeComboBox.DataContext = TraineeListForPL;
            this.traineeGenderComboBox.ItemsSource = Enum.GetValues(typeof(BE.Gender));
            this.traineeGearComboBox.ItemsSource = Enum.GetValues(typeof(BE.GearType));
            this.traineecarComboBox.ItemsSource = Enum.GetValues(typeof(BE.CarType));
            dateOfBirthDatePicker.DisplayDateEnd = DateTime.Now.AddYears(-1*(int)BE.Configuration.MinAgeOFTrainee);
            dateOfBirthDatePicker.DisplayDateStart = DateTime.Now.AddYears(-1 * (int)BE.Configuration.MaxAgeOFTrainee);
            TraineeComboBox.Visibility = Visibility.Hidden;

        }

        private void BackToMainMenue_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #region manage buttons
        private void AddTrainee_Click(object sender, RoutedEventArgs e)
        {

            TraineeGrid.Visibility = Visibility.Visible;
            TraineeGrid.IsEnabled = true;
            Save.Content = "Check";
        }

        private void UpdateTrainee_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TraineeListForPL = bl.GetListOfTrainees();
                if (TraineeListForPL.Count==0)
                    throw new Exception("There are no trainees to update.");
                TraineeGrid.Visibility = Visibility.Visible;
                TraineeGrid.IsEnabled = false;
               
                Save.Content = "Check";

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteTrainee_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TraineeListForPL = bl.GetListOfTrainees();
                if (TraineeListForPL.Count == 0)
                    throw new Exception("There are no trainees to update.");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        #endregion
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource traineeViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("traineeViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // traineeViewSource.Source = [generic data source]
        }


        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (Save.Content == "Add")
            {
                try
                {
                    bl.AddTrainee(TraineeForPL);
                    TraineeForPL = new Trainee();
                    TraineeGrid.Visibility = Visibility.Hidden;
                    this.TraineeGrid.DataContext = TraineeForPL;
                    MessageBox.Show("Trainee saved successfully", "", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    TraineeGrid.Visibility = Visibility.Visible;
                }

            }

            if (Save.Content == "Check")
            {

                if(noErrors())
                {
                    Save.Content = "Add";
                }
                else
                {
                    MessageBox.Show("Can't add Trainee. Fill ID " +
                                    "and fix errors.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

        }

        #region id checks

        private void TraineeIdTextBox_OnLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            try
            {
                bl.CheckId(TraineeForPL.TraineeId);
                //make a check if in system- make a messege box option for update/delete then "select" the combox option.
               
            }
            catch (Exception ex)
            {
                
                IdErrors.Text = ex.Message;
                IdErrors.Foreground = Brushes.Red;
                traineeIdTextBox.BorderBrush = Brushes.Red;
            }

        }

        private void TraineeIdTextBox_OnGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            IdErrors.Text = "";
            IdErrors.Foreground = Brushes.Black;
            traineeIdTextBox.BorderBrush = Brushes.Black;
        }

        #endregion
        #region Name checks
        private void FirstNameTextBox_OnLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            try
            {
                bl.IsText(TraineeForPL.FirstName);
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

        private void FirstNameTextBox_OnGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            NameErrors.Text = "";
            NameErrors.Foreground = Brushes.Black;
            firstNameTextBox.BorderBrush = Brushes.Black;
        }

        private void SirnameTextBox_OnLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            try
            {
                bl.IsText(TraineeForPL.Sirname);
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

        private void SirnameTextBox_OnGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            SirNameErrors.Text = "";
            SirNameErrors.Foreground = Brushes.Black;
            sirnameTextBox.BorderBrush = Brushes.Black;
        }
        #endregion
        #region phone Number

        private void PhoneNumberTextBox_OnLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            try
            {
                bl.IsNumber(TraineeForPL.PhoneNumber);
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

        private void PhoneNumberTextBox_OnGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            PhoneNumberErrors.Text = "";
            PhoneNumberErrors.Foreground = Brushes.Black;
            phoneNumberTextBox.BorderBrush = Brushes.Black;
        }
        #endregion

        #region driving teacher and driving school

        private void DrivingSchoolTextBox_OnLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            try
            {
                bl.IsText(TraineeForPL.DrivingSchool);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("ERROR"))
                {
                    DrivingSchoolErrors.Foreground = Brushes.Red;
                    drivingSchoolTextBox.BorderBrush = Brushes.Red;

                }
                else
                {
                    DrivingSchoolErrors.Foreground = Brushes.Orange;
                    drivingSchoolTextBox.BorderBrush = Brushes.Orange;
                }
                DrivingSchoolErrors.Text = ex.Message;

            }
        }
        private void DrivingTeacherTextBox_OnLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            try
            {
                
                //DrivingTeacherErrors.Text = "";
                //drivingTeacherTextBox.BorderBrush = Brushes.Black;
                bl.CheckId(TraineeForPL.DrivingTeacher);
                
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("ERROR. Field is empty."))
                {
                    DrivingTeacherErrors.Foreground = Brushes.Orange;
                    drivingTeacherTextBox.BorderBrush = Brushes.Orange;
                    DrivingTeacherErrors.Text = "Warning. Field is empty.";
                }

                else if(ex.Message.Contains("ERROR"))
                {
                    DrivingTeacherErrors.Foreground = Brushes.Red;
                    drivingTeacherTextBox.BorderBrush = Brushes.Red;
                    DrivingTeacherErrors.Text = ex.Message;

                }

            }
        }

        #endregion

        #region email

        private void EmailTextBox_OnLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            try
            {
                if(TraineeForPL.Email==null|| TraineeForPL.Email=="")
                    throw new Exception("Waring. Empty Field");
                if(!bl.CheckEmail(TraineeForPL.Email))
                    throw new Exception("ERROR. Invalid Email");

            }
            catch (Exception ex)
            {
                if(ex.Message.Contains("ERROR"))
                {
                    EmailErrors.Foreground = Brushes.Red;
                    emailTextBox.BorderBrush = Brushes.Red;
                }
                else
                {
                    EmailErrors.Foreground = Brushes.Orange;
                    emailTextBox.BorderBrush = Brushes.Orange;
                }

                EmailErrors.Text = ex.Message;

            }

        }

        private void EmailTextBox_OnGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            EmailErrors.Text = "";
            EmailErrors.Foreground = Brushes.Black;
            emailTextBox.BorderBrush = Brushes.Black;
        }
        #endregion

        #region lessson passed buttons

        private void Plus_On_Click(object sender, RoutedEventArgs e)
        {
            TraineeForPL.LessonsPassed++;
            lessonsPassedTextBox.Text = "" + TraineeForPL.LessonsPassed;

        }

        private void Minus_Click(object sender, RoutedEventArgs e)
        {
            if(TraineeForPL.LessonsPassed>=1)
            TraineeForPL.LessonsPassed--;
            lessonsPassedTextBox.Text = "" + TraineeForPL.LessonsPassed;
        }
        #endregion

        #region comboboxes
        private void TraineeGenderComboBox_OnMouseLeave(object sender, MouseEventArgs e)
        {
            if(traineeGenderComboBox.SelectedItem == null)
            {
                GenderErrors.Text = "Warning. Field is empty.";
                GenderErrors.Foreground = Brushes.Orange;
                traineeGenderComboBox.BorderBrush = Brushes.Orange;

            }

            else
            {

                GenderErrors.Text = "";
                GenderErrors.Foreground = Brushes.Black;
                traineeGenderComboBox.BorderBrush = Brushes.Black;
            }

        }
        

        private void TraineecarComboBox_OnMouseLeave(object sender, MouseEventArgs e)
        {
            if (traineecarComboBox.SelectedItem == null)
            {
                CarTypeErrors.Text = "Warning. Field is empty.";
                CarTypeErrors.Foreground = Brushes.Orange;
                traineecarComboBox.BorderBrush = Brushes.Orange;

            }

            else
            {

                CarTypeErrors.Text = "";
                CarTypeErrors.Foreground = Brushes.Black;
                traineecarComboBox.BorderBrush = Brushes.Black;
            }
        }

        private void TraineeGearComboBox_OnMouseLeave(object sender, MouseEventArgs e)
        {
            if (traineeGearComboBox.SelectedItem == null)
            {
                GearTypeErrors.Text = "Warning. Field is empty.";
                GearTypeErrors.Foreground = Brushes.Orange;
                traineeGearComboBox.BorderBrush = Brushes.Orange;

            }

            else
            {

                GearTypeErrors.Text = "";
                GearTypeErrors.Foreground = Brushes.Black;
                traineeGearComboBox.BorderBrush = Brushes.Black;
            }
        }
        #endregion

        public bool noErrors()
        {

            try
            {
                if(TraineeForPL.TraineeId == null)
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
                if (DrivingSchoolErrors.Text.Contains("ERROR"))
                    throw new Exception();
                if (DrivingTeacherErrors.Text.Contains("ERROR"))
                    throw new Exception();
                return true;
            }
            catch (Exception exception)
            {
                return false;
            }

            
        }

    }
}
