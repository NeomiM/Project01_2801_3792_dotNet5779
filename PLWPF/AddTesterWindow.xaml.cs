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
    /// Interaction logic for AddTesterWindow.xaml
    /// </summary>
    public partial class AddTesterWindow : Window
    {
        BE.Tester tester;
        BL.IBL bl;
        public AddTesterWindow()
        {
            InitializeComponent();

            tester = new BE.Tester();
            this.DataContext = tester;
            bl = BL.IBL_imp.Instance;

            this.testerGenderComboBox.ItemsSource = Enum.GetValues(typeof(BE.Gender));
            this.testercarComboBox.ItemsSource = Enum.GetValues(typeof(BE.CarType));
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            //try
            {
                bl.AddTester(tester);
                tester = new BE.Tester();
                this.DataContext = tester;
            }
            //catch (Exception e)
            //{
            //    MessageBox.Show(e.Message);
            //}
        }
    }
}
