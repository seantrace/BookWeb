using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace BookWeb
{
    [Table("books")]
    [Index(nameof(AuthorSort), Name = "authors_idx")]
    [Index(nameof(Sort), Name = "books_idx")]
    public partial record Book
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Required]
        [Column("title")]
        public string Title { get; set; }
        [Column("sort")]
        public string Sort { get; set; }
        [Column("timestamp", TypeName = "TIMESTAMP")]
        public DateTime Timestamp { get; set; }
        [Column("pubdate", TypeName = "TIMESTAMP")]
        public DateTime Pubdate { get; set; }
        [Column("series_index")]
        public double SeriesIndex { get; set; }
        [Column("author_sort")]
        public string AuthorSort { get; set; }
        [Required]
        [Column("path")]
        public string Path { get; set; }
        [Column("flags")]
        public long Flags { get; set; }
        [Column("uuid")]
        public string Uuid { get; set; }
        [Column("has_cover", TypeName = "BOOL")]
        public bool HasCover { get; set; }
        [Required]
        [Column("last_modified", TypeName = "TIMESTAMP")]
        public DateTime LastModified { get; set; }
    }
}
