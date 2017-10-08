namespace Ef.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Address")]
    public partial class Address
    {
        [Key]
        public int PKID { get; set; }

        public Guid UserId { get; set; }

        [StringLength(50)]
        public string Receiver { get; set; }

        [StringLength(20)]
        public string MobileNumber { get; set; }

        [StringLength(100)]
        public string Region { get; set; }

        [StringLength(500)]
        public string AddressDetail { get; set; }

        public int Status { get; set; }

        public DateTime CreatedTime { get; set; }

        public DateTime UpdatedTime { get; set; }

        [Column(TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
