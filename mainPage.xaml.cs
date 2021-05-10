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
using System.Net.Http;
using System.IO;

using path_t = System.IO.Path;

namespace Onigaku
{
    /// <summary>
    /// Логика взаимодействия для mainPage.xaml
    /// </summary>
    public partial class mainPage : Page
    {
        private MainWindow m_main_window;
        private MediaPlayer player = new MediaPlayer();

        public mainPage()
        {
            InitializeComponent();
            this.m_main_window = Application.Current.MainWindow as MainWindow;

            List<Button> buttons = new List<Button>() { SQLForm, playerOpenButton };
            foreach (Button btn in buttons)// enumerate_elements<Button>(this))
            {
                btn.Style = StyleClass.buttonStyle;
            }
            foreach (var curr_track in MLS_DBEntities1.GetContext().tracks)
            {
                music_panel.Children.Add(new Button
                {
                    FontFamily = new FontFamily("Segoe MDL2 Assets"),
                    Content = char.ConvertFromUtf32(0xE768),
                    Height = 40
                }
                );
            }
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
        private void SQLFormOpen(object sender, RoutedEventArgs e)
        {
            SQLData sqlWindow = new SQLData();
            sqlWindow.Show();
        }
        private void openSignUp(object sender, RoutedEventArgs e)
        {
            m_main_window.openUIPage(new signupPage());
        }

        /// <summary>
        /// if track.time_played < 30 && user_clicked_next() -> delete
        /// play -> apple seed -> next -> previous -> next 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var client = new System.Net.WebClient();
            client.DownloadFile("http://localhost:1337/12", path_t.GetTempPath().ToString() + "12.mp3");
            player.Open(new Uri(path_t.GetTempPath().ToString() + "12.mp3", UriKind.Relative));
            player.Play();
        }

    }
}
