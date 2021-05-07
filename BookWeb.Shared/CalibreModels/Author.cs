using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace BookWeb
{
    [Table("authors")]
    [Index(nameof(Name), IsUnique = true)]
    public partial record Author
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Required]
        [Column("name")]
        public string Name { get; set; }
        [Column("sort")]
        public string Sort { get; set; }
        [Required]
        [Column("link")]
        public string Link { get; set; }
    }
}
