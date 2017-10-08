namespace Ef.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Enjoyshare : DbContext
    {
        public Enjoyshare()
            : base("name=Enjoyshare")
        {
        }

        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<BookInfo> BookInfo { get; set; }
        public virtual DbSet<Region> Region { get; set; }
        public virtual DbSet<UserAccount> UserAccount { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>()
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<BookInfo>()
                .Property(e => e.Prise)
                .HasPrecision(18, 0);

            modelBuilder.Entity<UserAccount>()
                .Property(e => e.RowVersion)
                .IsFixedLength();
        }
    }
}
