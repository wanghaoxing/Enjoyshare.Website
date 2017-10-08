namespace Ef.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserAccount")]
    public partial class UserAccount
    {
        [Key]
        [Column(Order = 0)]
        public int PKID { get; set; }

        [Key]
        [Column(Order = 1)]
        public Guid UserId { get; set; }

        [StringLength(20)]
        public string UserTag { get; set; }

        [StringLength(20)]
        public string MobileNumber { get; set; }

        [StringLength(50)]
        public string ThirdPartId { get; set; }

        [StringLength(20)]
        public string Category { get; set; }

        [Key]
        [Column(Order = 2)]
        public bool IsMobileVerified { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(100)]
        public string Password { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(20)]
        public string Status { get; set; }

        [Key]
        [Column(Order = 4)]
        public DateTime CreatedTime { get; set; }

        [Key]
        [Column(Order = 5)]
        public DateTime UpdatedTime { get; set; }

        [Key]
        [Column(Order = 6, TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] RowVersion { get; set; }

        [Key]
        [Column(Order = 7)]
        public bool IsActive { get; set; }
    }
}
