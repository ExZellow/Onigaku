using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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
using AutoCompleteTextBox.Editors;

namespace Onigaku
{
    public partial class mainPage : Page
    {
        public static Frame FrameMainWindow { get; set; }
        private MainWindow m_main_window;
        private MediaPlayer player = new MediaPlayer();
        int played_track_counter = 0;
        public bool IsPlaying = false;

        MLS_DB ctx = MLS_DB.GetContext();

        public mainPage()
        {
            InitializeComponent();


            this.searchBox.Provider = new SuggestionProvider(x =>
            {
                List<string> track_n = new List<string>();
                foreach (var current_track in ctx.tracks_info.ToList())
                {
                    track_n.Add(current_track.track_name);
                }
                ObservableCollection<string> obs_collection = new ObservableCollection<string>(track_n);
                return obs_collection.Where(t => t.Trim().ToLower().Contains(x.Trim().ToLower()));
            });

            displayPanel();

            this.m_main_window = Application.Current.MainWindow as MainWindow;

            List<Button> buttons = new List<Button>() { SQLForm, signUp, play, logIn };
            foreach (Button btn in buttons)
            {
                btn.Style = StyleClass.buttonStyle;
            }
        }
        public StackPanel displayPanel()
        {

            searchBox.Filter = "";

            StackPanel pl_item = null;

            foreach (var curr_track in ctx.tracks.ToList())
            {
                FontFamily ui_symbols_font = new FontFamily("Segoe MDL2 Assets");
                pl_item = new StackPanel
                {
                    Orientation = Orientation.Horizontal,
                    Height = 40,
                    Background = Brushes.Black
                };
                var play_btn = new Button
                {
                    Height = 40,
                    Width = 40,
                    FontFamily = ui_symbols_font,
                    Content = char.ConvertFromUtf32(0xE768),
                    Foreground = Brushes.White,
                    Background = Brushes.Black
                };
                var t_add_btn = new Button
                {
                    Height = 40,
                    Width = 40,
                    FontFamily = ui_symbols_font,
                    Content = char.ConvertFromUtf32(0xE948),
                    Foreground = Brushes.White,
                    Background = Brushes.Black
                };

                var tr_duration = new TextBlock 
                {
                    Height = 40,
                    FontSize = 18,
                    Foreground = Brushes.White,
                    Margin = new Thickness(11, 6, 11, 5),
                    Text = TimeSpan.FromSeconds((double)curr_track.track_duration).ToString(),
                    TextAlignment = TextAlignment.Right
                };

                var tr_name = new TextBlock
                {
                    Name = "track_block",
                    Height = 40,
                    FontSize = 18,
                    Foreground = Brushes.White,
                    Margin = new Thickness(11, 6, 11, 5),
                    Text = curr_track.tracks_info.track_name + " - " + curr_track.performer.performer_name
                };
                pl_item.Children.Add(play_btn);
                pl_item.Children.Add(t_add_btn);
                pl_item.Children.Add(tr_name);
                pl_item.Children.Add(tr_duration);
                music_panel.Children.Add(pl_item);
                play_btn.Click += play_track;
                play_btn.Tag = curr_track.track_id;
            }
            return pl_item;
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
        private void openSignIn(object sender, RoutedEventArgs e)
        {
            m_main_window.openUIPage(new signinPage());
        }
        public void play_track(object sender, RoutedEventArgs e)
        {
            var c = sender as Control;
            var client = new System.Net.WebClient();

            if (c.Tag == this.Tag)
            {
                if (IsPlaying)
                {
                    player.Pause();
                    IsPlaying = false;
                } else
                {
                    player.Play();
                    IsPlaying = true;
                }
            }
            else
            {
                if (played_track_counter != 0 && player.Position.TotalSeconds < 30)
                {
                    try
                    {
                        System.IO.File.Delete("C:\\Users\\DexHydre\\AppData\\Local\\Temp\\" + this.Tag + ".mp3");
                    }
                    catch
                    {
                        MessageBox.Show("There is no such file!");
                    }
                }
                try
                {
                    client.DownloadFile("http://localhost:1337/" + Convert.ToInt32(c.Tag), path_t.GetTempPath().ToString() + Convert.ToInt32(c.Tag) + ".mp3");
                    player.Open(new Uri(path_t.GetTempPath().ToString() + Convert.ToInt32(c.Tag) + ".mp3", UriKind.Relative));
                    player.Play();
                    played_track_counter++;
                    IsPlaying = true;
                    this.Tag = c.Tag;
                }
                catch
                {
                    MessageBox.Show("Запустите сервер!");
                }
            }
        }


        private void play_Click(object sender, RoutedEventArgs e)
        {
            AddTracks tracks_add = new AddTracks();
            tracks_add.AddTrack();
            NavigationService.Refresh();
        }

        public void left_panel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch(left_panel.SelectedIndex)
            {
                case 1:
                    var c = sender as Control;

                    if (c.IsFocused)
                    {
                        c.Visibility = Visibility.Hidden;
                        searchBox.Visibility = Visibility.Visible;
                        searchBox.Focus();
                    }
                    else
                    {
                        searchBox.Visibility = Visibility.Hidden;
                        c.Visibility = Visibility.Visible;
                    }

                    searchBox.Visibility = Visibility.Visible;
                    searchBox.Focus();
                    searchBox.Background = Brushes.Black;
                    searchBox.Foreground = Brushes.White;
                    break;

                case 2:
                    this.m_main_window.openUIPage(new Playlists());
                    break;
            }
            left_panel.SelectedIndex = -1;
        }

        private void searchBox_LostFocus(object sender, RoutedEventArgs e)
        {
            searchBox.Visibility = Visibility.Collapsed;
        }
    }
}
