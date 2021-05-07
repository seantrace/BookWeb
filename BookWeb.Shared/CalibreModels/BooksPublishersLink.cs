using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace BookWeb
{
    [Table("books_publishers_link")]
    [Index(nameof(Book), IsUnique = true)]
    [Index(nameof(Publisher), Name = "books_publishers_link_aidx")]
    [Index(nameof(Book), Name = "books_publishers_link_bidx")]
    public partial record BooksPublishersLink
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("book")]
        public long Book { get; set; }
        [Column("publisher")]
        public long Publisher { get; set; }
    }
}
