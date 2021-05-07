using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace BookWeb
{
    [Table("ratings")]
   // [Index(nameof(RatingValue), IsUnique = true)]
    public partial class Rating
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("rating")]
        public long? RatingValue { get; set; }
    }
}
