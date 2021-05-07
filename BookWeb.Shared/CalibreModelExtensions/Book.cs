using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;

namespace BookWeb
{
    public partial record Book
    {
        [NotMapped]
        public string Comment { get; set; }
        [NotMapped]
        public List<Identifier> Identifiers { get; set; }
        [NotMapped]
        public List<BookFormat> Formats { get; set; }
        [NotMapped]
        public long? Size { get; set; }
        [NotMapped]
        public string Filename { get; set; }
        [NotMapped]
        public string Language { get; set; }
        [NotMapped]
        public List<Tag> Tags { get; set; }
        [NotMapped]
        public long SeriesId { get; set; }
        [NotMapped]
        public string Series { get; set; }
        [NotMapped]
        public long AuthorId { get; set; }
        [NotMapped]
        public string Author { get; set; }
        [NotMapped]
        public long PublisherId { get; set; }
        [NotMapped]
        public string Publisher { get; set; }
        [NotMapped]
        public long Rating { get; set; }

    
    }

}
