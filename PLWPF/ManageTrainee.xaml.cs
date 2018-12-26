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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource traineeViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("traineeViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // traineeViewSource.Source = [generic data source]
        }

        private void TraineeIdTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

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

        private void TraineeIdTextBox_OnLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            try
            {
                bl.CheckId(TraineeForPL.TraineeId);
            }
            catch (Exception ex)
            {
                IdErrors.Text = ex.Message;
                IdErrors.Foreground=Brushes.Red;
                traineeIdTextBox.BorderBrush=Brushes.Red;
            }

        }

        private void TraineeIdTextBox_OnGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            IdErrors.Text="";
            IdErrors.Foreground = Brushes.Black;
            traineeIdTextBox.BorderBrush = Brushes.Black;
        }
    }
}
