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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IBL_imp mainBL;
        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            mainBL = IBL_imp.Instance;

        }

        private void ManageTesters_Click(object sender, RoutedEventArgs e)
        {
            new TestersWindow().ShowDialog();

        }

        private void ManageTrainees_Click(object sender, RoutedEventArgs e)
        {
            new ManageTrainee().ShowDialog();
        }

        private void ManageTests_Click(object sender, RoutedEventArgs e)
        {

        }
        private void ValidableTesters_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TraineesByTesters_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TestersByCarType_Click(object sender, RoutedEventArgs e)
        {
            new TestersByCarType().ShowDialog();
        }

        private void ValidTrainees_Click(object sender, RoutedEventArgs e)
        {
            new validTrainees().Show();
        }

        private void TestsByDate_Click(object sender, RoutedEventArgs e)
        {
            new TestsByDate().Show();
        }

        private void TraineesByTesters_OnClick(object sender, RoutedEventArgs e)
        {
            new traineesByTeachers().Show();
        }
    }
}
