using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace BookWeb
{
    [Table("books_tags_link")]
    [Index(nameof(Book), nameof(Tag), IsUnique = true)]
    [Index(nameof(Tag), Name = "books_tags_link_aidx")]
    [Index(nameof(Book), Name = "books_tags_link_bidx")]
    public partial record BooksTagsLink
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("book")]
        public long Book { get; set; }
        [Column("tag")]
        public long Tag { get; set; }
    }
}
