using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace BookWeb
{
    [Table("books_ratings_link")]
    [Index(nameof(Book), nameof(Rating), IsUnique = true)]
    [Index(nameof(Rating), Name = "books_ratings_link_aidx")]
    [Index(nameof(Book), Name = "books_ratings_link_bidx")]
    public partial class BooksRatingsLink
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("book")]
        public long Book { get; set; }
        [Column("rating")]
        public long Rating { get; set; }
    }
}
