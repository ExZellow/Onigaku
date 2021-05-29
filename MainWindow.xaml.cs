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
using Microsoft.Win32;
using Microsoft.SqlServer;
using System.Data.Entity;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Onigaku
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.upPanel.Content = new upperPanel();
            Application.Current.MainWindow = this;
            Loaded += (object sender, RoutedEventArgs a) => { openUIPage(new mainPage()); };
        }


        public void openUIPage(Page page)
        {
            this.navigationFrame.NavigationService.Navigate(page);
        }
    }
}
