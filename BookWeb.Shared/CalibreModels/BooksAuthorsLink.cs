using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace BookWeb
{
    [Table("books_authors_link")]
    [Index(nameof(Book), nameof(Author), IsUnique = true)]
    [Index(nameof(Author), Name = "books_authors_link_aidx")]
    [Index(nameof(Book), Name = "books_authors_link_bidx")]
    public partial record BooksAuthorsLink
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("book")]
        public long Book { get; set; }
        [Column("author")]
        public long Author { get; set; }
    }
}
