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

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for TestersWindow.xaml
    /// </summary>
    public partial class TestersWindow : Window
    {
        public TestersWindow()
        {
            InitializeComponent();
        }

        private void AddTesterButton_Click(object sender, RoutedEventArgs e)
        {
            AddTesterWindow AddWindow = new AddTesterWindow();
            this.Close();
            AddWindow.Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource testerViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("testerViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // testerViewSource.Source = [generic data source]
        }
    }
}
