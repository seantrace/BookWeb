using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace BookWeb
{
    [Table("books_languages_link")]
    [Index(nameof(Book), nameof(LangCode), IsUnique = true)]
    [Index(nameof(LangCode), Name = "books_languages_link_aidx")]
    [Index(nameof(Book), Name = "books_languages_link_bidx")]
    public partial record BooksLanguagesLink
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("book")]
        public long Book { get; set; }
        [Column("lang_code")]
        public long LangCode { get; set; }
        [Column("item_order")]
        public long ItemOrder { get; set; }
    }
}
