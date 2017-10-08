namespace Ef.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BookInfo")]
    public partial class BookInfo
    {
        [Key]
        public int Pkid { get; set; }

        public Guid UserId { get; set; }

        [StringLength(500)]
        public string BookName { get; set; }

        [StringLength(100)]
        public string Author { get; set; }

        public string BookDescription { get; set; }

        public decimal? Prise { get; set; }

        [StringLength(500)]
        public string Press { get; set; }

        public int? Type { get; set; }

        public bool? IsEnable { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public DateTime? CreateTime { get; set; }

        public DateTime? UpdateTime { get; set; }

        public int? RegionId { get; set; }

        [StringLength(50)]
        public string Url { get; set; }

        public int? Stock { get; set; }
    }
}
