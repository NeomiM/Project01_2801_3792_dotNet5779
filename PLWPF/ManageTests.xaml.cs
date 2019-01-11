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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class ManageTests : Window
    {
        private BL.IBL bl;
        private List<Trainee> nTraineeListForPL;
        private Test nTestForPL; 
        public ManageTests()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            bl = IBL_imp.Instance;
            
                try
                {
                    nTestForPL = new Test();
                    TestAddGrid.DataContext = nTestForPL;
                    nTraineeListForPL = bl.readyTrainees();
                    emptyAddTab();
                    if (nTraineeListForPL.Count == 0)
                        throw new Exception("ERROR. There are no trainees ready for a test.");
                    this.traineeIdComboBox.ItemsSource = nTraineeListForPL.Select(x => x.TraineeId);

                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }


            


        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource testViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("testViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // testViewSource.Source = [generic data source]
        }

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Update.IsSelected)
            {
                try
                {
                    nTestForPL = new Test();
                    TestAddGrid.DataContext = nTestForPL;
                    nTraineeListForPL = bl.readyTrainees();
                    emptyAddTab();
                    if (nTraineeListForPL.Count == 0)
                        throw new Exception("ERROR. There are no trainees ready for a test.");
                    this.traineeIdComboBox.ItemsSource = nTraineeListForPL.Select(x => x.TraineeId);

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
        }

        private void TraineeIdComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //nTestForPL.TraineeId = traineeIdComboBox.Text;
            //nTestForPL.CarType = bl.GetListOfTrainees().Where(x => x.TraineeId == nTestForPL.TraineeId)
            //    .Select(x => x.Traineecar).FirstOrDefault();
            //carTypeTextBlock. = nTestForPL.CarType;
        }
    }
}
