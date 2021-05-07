using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace BookWeb
{
    [Table("data")]
    [Index(nameof(Book), nameof(Format), IsUnique = true)]
    [Index(nameof(Book), Name = "data_idx")]
    [Index(nameof(Format), Name = "formats_idx")]
    public partial record Datum
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("book")]
        public long Book { get; set; }
        [Required]
        [Column("format")]
        public string Format { get; set; }
        [Column("uncompressed_size")]
        public long UncompressedSize { get; set; }
        [Required]
        [Column("name")]
        public string Name { get; set; }
    }
}
