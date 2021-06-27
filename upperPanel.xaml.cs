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

namespace Onigaku
{
    public partial class upperPanel : UserControl
    {
        private MainWindow m_main_window;
        public upperPanel()
        {
            InitializeComponent();
            this.m_main_window = Application.Current.MainWindow as MainWindow;
        }
        private void minimizeWindow(object sender, RoutedEventArgs e)
        {
            m_main_window.WindowState = WindowState.Minimized;
        }

        private void closeWindow(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void changeWindowScale(object sender, RoutedEventArgs e)
        {
            if (m_main_window.WindowState == System.Windows.WindowState.Normal)
            {
                m_main_window.WindowState = System.Windows.WindowState.Maximized;
                openScaleButton.Content = char.ConvertFromUtf32(0xE923);
            }
            else
            {
                m_main_window.WindowState = System.Windows.WindowState.Normal;
                openScaleButton.Content = char.ConvertFromUtf32(0xE922);
            }
        }
        private void controlPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            m_main_window.DragMove();
        }

    }
}
