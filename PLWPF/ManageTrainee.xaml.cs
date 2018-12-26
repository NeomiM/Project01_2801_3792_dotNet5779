using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Security.Cryptography.X509Certificates;
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
    /// Interaction logic for ManageTrainee.xaml
    /// </summary>
    public partial class ManageTrainee : Window
    {
        private BL.IBL bl;
        private BE.Trainee TraineeForPL;
        public ManageTrainee()
        {
            InitializeComponent();
            TraineeGrid.Visibility = Visibility.Hidden;
            bl = IBL_imp.Instance;
            TraineeForPL=new Trainee();
            this.TraineeGrid.DataContext = TraineeForPL;
            Save.IsEnabled = false;
        }

        private void BackToMainMenue_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #region manage buttons
        private void AddTrainee_Click(object sender, RoutedEventArgs e)
        {
            TraineeGrid.Visibility = Visibility.Visible;
            Save.Content = "Add";
        }

        private void UpdateTrainee_Click(object sender, RoutedEventArgs e)
        {
            try
            {
           
            List<Trainee> Trainees = IBL_imp.Instance.GetListOfTrainees();
                if (Trainees.Count==0)
                    throw new Exception("There are no trainees to update.");
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
                List<Trainee> Trainees = IBL_imp.Instance.GetListOfTrainees();
                if (Trainees.Count == 0)
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
                bl.AddTrainee(TraineeForPL);
                TraineeForPL=new Trainee();
                this.TraineeGrid.DataContext = TraineeForPL;
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
                NameErrors.Text = ex.Message;
                NameErrors.Foreground = Brushes.Red;
                firstNameTextBox.BorderBrush = Brushes.Red;
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
                SirNameErrors.Text = ex.Message;
                SirNameErrors.Foreground = Brushes.Red;
                sirnameTextBox.BorderBrush = Brushes.Red;
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
                PhoneNumberErrors.Text = ex.Message;
                PhoneNumberErrors.Foreground = Brushes.Red;
                phoneNumberTextBox.BorderBrush = Brushes.Red;
            }
        }

        private void PhoneNumberTextBox_OnGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            PhoneNumberErrors.Text = "";
            PhoneNumberErrors.Foreground = Brushes.Black;
            phoneNumberTextBox.BorderBrush = Brushes.Black;
        }
        #endregion

        #region email

        private void EmailTextBox_OnLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            try
            {
                if(TraineeForPL.Email==null|| TraineeForPL.Email=="")
                    throw new Exception("ERROR. Empty Field");
                bl.CheckEmail(TraineeForPL.Email);
            }
            catch (Exception ex)
            {
                EmailErrors.Text = ex.Message;
                EmailErrors.Foreground = Brushes.Red;
                emailTextBox.BorderBrush = Brushes.Red;
            }

        }

        private void EmailTextBox_OnGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            EmailErrors.Text = "";
            EmailErrors.Foreground = Brushes.Black;
            emailTextBox.BorderBrush = Brushes.Black;
        }
        #endregion
    }
}
