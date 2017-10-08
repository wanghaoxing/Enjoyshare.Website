using System;

namespace Remote.Model
{
    public class BookModel
    {
        public int Pkid { get; set; }

        public int RegionId { get; set; }
        public Guid UserId { get; set; }

        public string BookName { get; set; }

        public string Author { get; set; }

        public string BookDescription { get; set; }

        public decimal? Prise { get; set; }

        public string Press { get; set; }

        public int? Type { get; set; }

        public bool? IsEnable { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public DateTime? CreateTime { get; set; }

        public DateTime? UpdateTime { get; set; }
        public string Url { get; set; }
        public string ImageUrl { get; set; }

        public int Stock { get; set; }
    }
}
