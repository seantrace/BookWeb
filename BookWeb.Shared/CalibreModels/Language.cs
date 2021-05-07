using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace BookWeb
{
    [Table("languages")]
    [Index(nameof(LangCode), IsUnique = true)]
    [Index(nameof(LangCode), Name = "languages_idx")]
    public partial record Language
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Required]
        [Column("lang_code")]
        public string LangCode { get; set; }
    }
}
