using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace BookWeb
{
    [Table("identifiers")]
    [Index(nameof(Book), nameof(Type), IsUnique = true)]
    public partial record Identifier
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("book")]
        public long Book { get; set; }
        [Required]
        [Column("type")]
        public string Type { get; set; }
        [Required]
        [Column("val")]
        public string Val { get; set; }
    }
}
