namespace Wheather.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Uygulama")]
    public partial class Uygulama
    {
        public int id { get; set; }

        [StringLength(100)]
        public string adi { get; set; }

        [StringLength(50)]
        public string tarih { get; set; }

        public bool? aktif { get; set; }

        public int? kullanici_id { get; set; }

        [StringLength(255)]
        public string url { get; set; }

        [StringLength(50)]
        public string guncelleme_tarih { get; set; }

        public virtual Kullanici Kullanici { get; set; }
    }
}
