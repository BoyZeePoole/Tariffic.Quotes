namespace Tariffic.Quotes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Quote
    {
        [Key]
        [Column(Order = 0)]
        public int Id { get; set; }

        [Column(Order = 1)]
        [StringLength(150)]
        public string Author { get; set; }

        [Column("Quote", Order = 2)]
        [StringLength(500)]
        public string Quote1 { get; set; }

        [Column(Order = 3)]
        [StringLength(50)]
        public string Mood { get; set; }


        [Column(Order = 4)]
        [StringLength(500)]
        public string Source { get; set; }
    }
}
