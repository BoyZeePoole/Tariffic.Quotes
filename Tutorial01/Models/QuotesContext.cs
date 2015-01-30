namespace Tariffic.Quotes
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class QuotesContext : DbContext
    {
        public QuotesContext()
            : base("name=TarifficModel")
        {
        }

        public virtual DbSet<Quote> Quotes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Quote>()
                .Property(e => e.Author)
                .IsUnicode(false);

            modelBuilder.Entity<Quote>()
                .Property(e => e.Mood)
                .IsUnicode(false);

            modelBuilder.Entity<Quote>()
                .Property(e => e.Source)
                .IsUnicode(false);
        }
    }
}
