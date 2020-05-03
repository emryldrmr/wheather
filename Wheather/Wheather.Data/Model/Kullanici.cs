namespace Wheather.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Kullanici")]
    public partial class Kullanici
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Kullanici()
        {
            Uygulama = new HashSet<Uygulama>();
        }

        public int id { get; set; }

        [StringLength(50)]
        public string kullanici_adi { get; set; }

        [StringLength(50)]
        public string sifre { get; set; }

        [StringLength(50)]
        public string email { get; set; }

        [StringLength(100)]
        public string adsoyad { get; set; }

        public int yetki_id { get; set; }

        [StringLength(50)]
        public string tarih { get; set; }

        public bool? aktif { get; set; }

        [StringLength(100)]
        public string fotograf { get; set; }

        public virtual Yetki Yetki { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Uygulama> Uygulama { get; set; }
    }
}
