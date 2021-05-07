using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace BookWeb
{
    [Table("comments")]
    [Index(nameof(Book), IsUnique = true)]
    [Index(nameof(Book), Name = "comments_idx")]
    public partial record Comment
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("book")]
        public long Book { get; set; }
        [Required]
        [Column("text")]
        public string Text { get; set; }
    }
}
