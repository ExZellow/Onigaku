//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Onigaku
{
    using System;
    using System.Collections.Generic;
    
    public partial class track
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public track()
        {
            this.albums = new HashSet<album>();
            this.playlists = new HashSet<playlist>();
        }
    
        public int track_id { get; set; }
        public Nullable<int> performer_id { get; set; }
        public Nullable<int> track_duration { get; set; }
        public string genre { get; set; }
        public Nullable<int> bitrate { get; set; }
    
        public virtual performer performer { get; set; }
        public virtual tracks_info tracks_info { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<album> albums { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<playlist> playlists { get; set; }
    }
}