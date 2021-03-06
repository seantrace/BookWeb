using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace BookWeb
{
    [Table("series")]
    [Index(nameof(Name), IsUnique = true)]
    [Index(nameof(Name), Name = "series_idx")]
    public partial record Series : ICalibreRecord 
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Required]
        [Column("name")]
        public string Name { get; set; }
        [Column("sort")]
        public string Sort { get; set; }
    }
}
