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
    public partial class Playlists : Page
    {
        public Playlists()
        {
            InitializeComponent();
            displayPanel();
        }
        public StackPanel displayPanel()
        {
            MLS_DB ctx = MLS_DB.GetContext();

            StackPanel pl_item = null;

            foreach (var curr_track in ctx.tracks_playlist.ToList())
            {
                FontFamily ui_symbols_font = new FontFamily("Segoe MDL2 Assets");
                pl_item = new StackPanel
                {
                    Orientation = Orientation.Horizontal,
                    Height = 60,
                    Background = Brushes.Black
                };
                var play_btn = new Button
                {
                    Height = 60,
                    Width = 60,
                    FontFamily = ui_symbols_font,
                    Content = char.ConvertFromUtf32(0xE768),
                    Foreground = Brushes.White,
                    Background = Brushes.Black
                };

                var pl_name = new TextBlock
                {
                    Name = "pl_block",
                    Height = 60,
                    FontSize = 18,
                    Foreground = Brushes.White,
                    Margin = new Thickness(11, 6, 11, 5),
                    Text = curr_track.track_name + '(' + curr_track.playlist_name + ',' + curr_track.user_id + ')'
                };
                pl_item.Children.Add(play_btn);
                pl_item.Children.Add(pl_name);
                music_panel.Children.Add(pl_item);
                play_btn.Tag = curr_track.track_id;
            }
            return pl_item;
        }
        private void pl_MoveBack(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
