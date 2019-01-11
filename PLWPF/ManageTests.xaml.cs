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
        private List<Trainee> TraineeListForPL;
        public ManageTests()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            bl = IBL_imp.Instance;
            TraineeListForPL = bl.readyTrainees();
            this.TraineeComboBox.ItemsSource = TraineeListForPL.Select(x=>x.TraineeId);

        }
    }
}
