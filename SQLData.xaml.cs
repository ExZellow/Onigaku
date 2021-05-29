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
    /// <summary>
    /// Логика взаимодействия для SQLData.xaml
    /// </summary>
    public partial class SQLData : Window
    {
        MLS_DB DB;
        public SQLData()
        {
            InitializeComponent();
            //DB = new MLS_DBEntities1();
            //DG.ItemsSource = MLS_DBEntities1.context.users.ToList();


        }

        private void Insert_Data(object sender, RoutedEventArgs e)
        {
            user user = new user();
            user.email = Email.Text;
            user.username = User.Text;
            PasswordBox pw = new PasswordBox();
            pw.Password = PW.Text;
            user.password = pw.Password;
            //user.access_level = 
            
            //DG.ItemsSource = MLS_DBEntities1.GetContext().users.Add(user);

        }
        private void DisplayData(object sender, RoutedEventArgs e)
        {
            DB = new MLS_DB();
            DG.ItemsSource = MLS_DB.GetContext().users.ToList();
        }

        private void LoadData(object sender, RoutedEventArgs e)
        {
            DB = new MLS_DB();
            DG.ItemsSource = DB.users.ToList();
        }

        private void Delete(object sender, RoutedEventArgs e)
        {

        }

        private void UpdateData(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
