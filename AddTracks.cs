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
    /*public class db_attrib
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int track_id { get; set; }
    }*/
    public class AddTracks
    {
        public void AddTrack()
        {
            MLS_DB DB = new MLS_DB();
            var ctx = MLS_DB.GetContext();

            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.InitialDirectory = @"C:\";
            //fileDialog.Filter = "Audio files | *.mp3; *.aif; *.m3u; *.m4a; *.mid; *.mpa; *.wav; *.wma";

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                performer artist = new performer();
                track curr_track = new track();
                tracks_info tr_name = new tracks_info();
                

                //foreach (string musicFile in musicFiles)
                string server_path = @"C:\DexHydre\webserver\tracks";
                string[] musicFiles = Directory.GetFiles(server_path, "*.mp3");
                int new_number = musicFiles.Count() + 1;

                FileStream musicFile = File.OpenRead(fileDialog.FileName);

                


                byte[] b = new byte[1024];
                musicFile.Read(b, 0, b.Length);

                using (var mp3 = new Mp3(musicFile))
                {
                    Id3Tag tag = mp3.GetTag(Id3TagFamily.Version2X);
                    //try
                    //{
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


                    /*var a = new db_attrib
                    {
                        track_id = new_track_id
                    };*/





                    //System.Windows.MessageBox.Show(tag.Artists);
                    if (duplicate_track.Any()) {
                            System.Windows.MessageBox.Show("Duplicate track");
                    }
                    else
                    //if (!ctx.performers.Where(p => p.performer_name == tag.Artists).Any())
                    {
                        /*if (ctx.performers.Join(ctx.tracks, p => p.performer_id, t => t.performer_id, (p, t) => new
                        {
                            performer = p,
                            track = t
                        }).Join(ctx.tracks_info, tr => tr.track.track_id, i => i.track_id, (t, i) => new
                        {
                            tracks_info = i,
                            track = t
                        }).Where())*/
                        //if (!ctx.tracks_info.Where(t => t.track_name == tag.Title).Any())

                        //System.Windows.MessageBox.Show(tag.Title);
                        //curr_track.performer.performer_name = tag.Artists;

                        /*object obj = a as object;
                        var ret = new List<validation_t>();
                        var valid_ctx = new ValidationContext(obj);
                        if (!Validator.TryValidateObject(obj, valid_ctx, ret, true))
                        {*/
                            curr_track.track_id = new_track_id;
                        //}
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

                            //curr_track.tracks_info = tr_name;
                            //tr_name.track = curr_track;
                            //curr_track.performer = artist;

                        if (!musicFiles.Contains(curr_track.track_id.ToString()))
                        {
                            DB.tracks.Add(curr_track);
                            DB.tracks_info.Add(tr_name);
                            DB.SaveChanges();
                            
                            var my_field = musicFile.GetType()
                            .GetField(musicFile.ToString(), System.Reflection.BindingFlags.Instance
                            | System.Reflection.BindingFlags.NonPublic);

                            my_field.SetValue(musicFile, new_number);

                            File.Copy(musicFile.ToString(), server_path);
                        }



                            

                        }
                    //}
                    //catch
                    //{
                    //    System.Windows.MessageBox.Show("This track already exists!");
                    //}
                }
            }
        }
    }
}
