using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace BookWeb
{
    [Table("books_series_link")]
    [Index(nameof(Book), IsUnique = true)]
    [Index(nameof(Series), Name = "books_series_link_aidx")]
    [Index(nameof(Book), Name = "books_series_link_bidx")]
    public partial record BooksSeriesLink
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("book")]
        public long Book { get; set; }
        [Column("series")]
        public long Series { get; set; }
    }
}
