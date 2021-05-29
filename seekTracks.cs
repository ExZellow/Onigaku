using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onigaku
{
    public class seekTracks
    {
        public void seekTrack()
        {
            MLS_DBEntities1 DB = new MLS_DBEntities1();
            var ctx = MLS_DBEntities1.GetContext();

            performer artist = new performer();
            track curr_track = new track();
            tracks_info tr_name = new tracks_info();

            var search =
                from a in ctx.performers
                join t in ctx.tracks on a.performer_id equals t.performer_id
                join i in ctx.tracks_info on t.track_id equals i.track_id
                //where i.track_name == searchBox.Text
                select t.track_id;

        }
    }
}
