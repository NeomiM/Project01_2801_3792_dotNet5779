using System;
using System.Collections.Generic;
using System.Linq;
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
        public ManageTrainee()
        {
            InitializeComponent();
        }

        private void BackToMainMenue_Click(object sender, RoutedEventArgs e)
        {
            MainWindow win=new MainWindow();
            this.Close();
            win.Show();
        }
        private void AddTrainee_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UpdateTrainee_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BL.IBL isThisBLOk = FactoryBL.GetBL();
                List<Trainee> Trainees = isThisBLOk.GetListOfTrainees();
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
                BL.IBL isThisBLOk = FactoryBL.GetBL();
                List<Trainee> Trainees = isThisBLOk.GetListOfTrainees();
                if (Trainees.Count == 0)
                    throw new Exception("There are no trainees to update.");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }


        
    }
}
