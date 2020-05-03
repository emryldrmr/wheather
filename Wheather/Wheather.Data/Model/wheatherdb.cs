namespace Wheather.Data.Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class wheatherdb : DbContext
    {
        public wheatherdb()
            : base("name=wheatherdb")
        {
        }

        public virtual DbSet<Kullanici> Kullanici { get; set; }
        public virtual DbSet<Uygulama> Uygulama { get; set; }
        public virtual DbSet<Yetki> Yetki { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Kullanici>()
                .Property(e => e.kullanici_adi)
                .IsUnicode(false);

            modelBuilder.Entity<Kullanici>()
                .Property(e => e.sifre)
                .IsUnicode(false);

            modelBuilder.Entity<Kullanici>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<Kullanici>()
                .Property(e => e.adsoyad)
                .IsUnicode(false);

            modelBuilder.Entity<Kullanici>()
                .Property(e => e.tarih)
                .IsUnicode(false);

            modelBuilder.Entity<Kullanici>()
                .Property(e => e.fotograf)
                .IsUnicode(false);

            modelBuilder.Entity<Kullanici>()
                .HasMany(e => e.Uygulama)
                .WithOptional(e => e.Kullanici)
                .HasForeignKey(e => e.kullanici_id);

            modelBuilder.Entity<Uygulama>()
                .Property(e => e.adi)
                .IsUnicode(false);

            modelBuilder.Entity<Uygulama>()
                .Property(e => e.tarih)
                .IsUnicode(false);

            modelBuilder.Entity<Uygulama>()
                .Property(e => e.url)
                .IsUnicode(false);

            modelBuilder.Entity<Uygulama>()
                .Property(e => e.guncelleme_tarih)
                .IsUnicode(false);

            modelBuilder.Entity<Yetki>()
                .Property(e => e.yetki_adi)
                .IsUnicode(false);

            modelBuilder.Entity<Yetki>()
                .Property(e => e.tarih)
                .IsUnicode(false);

            modelBuilder.Entity<Yetki>()
                .HasMany(e => e.Kullanici)
                .WithRequired(e => e.Yetki)
                .HasForeignKey(e => e.yetki_id)
                .WillCascadeOnDelete(false);
        }
    }
}
