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

namespace Onigaku
{
    public partial class SQLData : Window
    {
        MLS_DB DB;
        public SQLData()
        {
            InitializeComponent();
        }

        private void DisplayData(object sender, RoutedEventArgs e)
        {
            DB = new MLS_DB();
            DG.ItemsSource = MLS_DB.GetContext().users.ToList();
        }

        private void Delete(object sender, RoutedEventArgs e)
        {

        }
    }
}
