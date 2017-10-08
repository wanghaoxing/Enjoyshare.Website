namespace Ef.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Region")]
    public partial class Region
    {
        [Key]
        [Column(Order = 0)]
        public int PKID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RegionId { get; set; }

        [StringLength(100)]
        public string RegionName { get; set; }

        public int? ParentId { get; set; }

        public int? Level { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Status { get; set; }

        [Key]
        [Column(Order = 3)]
        public DateTime CreatedTime { get; set; }

        [Key]
        [Column(Order = 4)]
        public DateTime UpdatedTime { get; set; }
    }
}
