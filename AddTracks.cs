using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Data.Sql;
using Id3;
using System.IO;
using System.ComponentModel.DataAnnotations.Schema;

using validation_t = System.ComponentModel.DataAnnotations.ValidationResult;
using System.ComponentModel.DataAnnotations;

namespace Onigaku
{
    public class AddTracks
    {
        public void AddTrack()
        {
            MLS_DB DB = new MLS_DB();
            var ctx = MLS_DB.GetContext();

            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.InitialDirectory = @"C:\";

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                performer artist = new performer();
                track curr_track = new track();
                tracks_info tr_name = new tracks_info();
                

                string server_path = @"C:\DexHydre\webserver\tracks";
                string[] musicFiles = Directory.GetFiles(server_path, "*.mp3");
                int new_number = musicFiles.Count() + 1;

                var musicFile = File.OpenRead(fileDialog.FileName);
                
                byte[] b = new byte[1024];
                
                musicFile.Read(b, 0, b.Length);

                using (var mp3 = new Mp3(musicFile))
                {
                    Id3Tag tag = mp3.GetTag(Id3TagFamily.Version2X);
                    if (tag != null) {
                        var duplicate_track =

                        from tr in ctx.tracks_info
                        join t in ctx.tracks on tr.track_id equals t.track_id
                        join p in ctx.performers on t.performer_id equals p.performer_id
                        where p.performer_name == tag.Artists && tr.track_name == tag.Title
                        select tr.track_id;


                        var existing_performer =

                        from p in ctx.performers
                        where p.performer_name == tag.Artists
                        select p.performer_id;


                        var new_track_id =

                        (from t in ctx.tracks
                        orderby t.track_id descending
                        select t.track_id).Count() + 1;

                        if (duplicate_track.Any())
                        {
                            System.Windows.MessageBox.Show("Duplicate track");
                        }
                        else
                        {
                            curr_track.track_id = new_track_id;
                            if (existing_performer.Any())
                            {
                                curr_track.performer_id = existing_performer.Single();
                            }
                            else
                            {
                                artist.performer_name = tag.Artists;
                                DB.performers.Add(artist);
                                DB.SaveChanges();
                                curr_track.performer_id = existing_performer.Single();
                            }

                            curr_track.track_duration = Convert.ToInt32(mp3.Audio.Duration.TotalSeconds);
                            curr_track.bitrate = mp3.Audio.Bitrate;
                            if (tag.Genre == null)
                            {
                                curr_track.genre = null;
                            }
                            else
                            {
                                curr_track.genre = tag.Genre;
                            }

                            tr_name.track_id = curr_track.track_id;
                            tr_name.track_name = tag.Title;

                            if (!musicFiles.Contains(curr_track.track_id.ToString()))
                            {
                                DB.tracks.Add(curr_track);
                                DB.tracks_info.Add(tr_name);

                                int dot_position = musicFile.Name.IndexOf(".");
                                string new_name = server_path + @"\" + new_number.ToString() + musicFile.Name.Substring(dot_position);
                                if (File.Exists(new_name))
                                {
                                    System.Windows.MessageBox.Show("Такой файл уже существует!");
                                }
                                else
                                {
                                    File.Copy(musicFile.Name, new_name);
                                }
                                DB.SaveChanges();
                            }
                        }
                    } else
                    {
                        System.Windows.MessageBox.Show("Invalid track data");
                    }
                }
            }
        }
    }
}
