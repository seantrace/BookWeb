using System;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace BookWeb
{
    public partial class CalibreDBContext : DbContext
    {
        private readonly HttpContext _httpContext;

        public CalibreDBContext()
        {
        }

        public CalibreDBContext(DbContextOptions<CalibreDBContext> options, IHttpContextAccessor httpContextAccessor = null)
            : base(options)
        {
            _httpContext = httpContextAccessor?.HttpContext;
        }

        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<BooksAuthorsLink> BooksAuthorsLinks { get; set; }
        public virtual DbSet<BooksLanguagesLink> BooksLanguagesLinks { get; set; }
        public virtual DbSet<BooksPublishersLink> BooksPublishersLinks { get; set; }
        public virtual DbSet<BooksSeriesLink> BooksSeriesLinks { get; set; }
        public virtual DbSet<BooksTagsLink> BooksTagsLinks { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Datum> Data { get; set; }
        public virtual DbSet<Identifier> Identifiers { get; set; }
        public virtual DbSet<Language> Languages { get; set; }
        public virtual DbSet<Publisher> Publishers { get; set; }
        public virtual DbSet<Series> Series { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<BooksRatingsLink> BooksRatingsLinks { get; set; }
        public virtual DbSet<Rating> Ratings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var httpRequest = _httpContext.Request;
            var libraryQuerystringParameter = httpRequest.Query["library"].ToString();
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlite("Data Source=F:\\EBooks\\Libraries\\Fiction\\metadata.db;Mode=ReadOnly");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Link).HasDefaultValueSql("\"\"");
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.Property(e => e.Flags).HasDefaultValueSql("1");

                entity.Property(e => e.HasCover).HasDefaultValueSql("0");

                entity.Property(e => e.LastModified).HasDefaultValueSql("\"2000-01-01 00:00:00+00:00\"");

                entity.Property(e => e.Path).HasDefaultValueSql("\"\"");

                entity.Property(e => e.Pubdate).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.SeriesIndex).HasDefaultValueSql("1.0");

                entity.Property(e => e.Timestamp).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Title).HasDefaultValueSql("'Unknown'");
            });

            modelBuilder.Entity<BooksAuthorsLink>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<BooksLanguagesLink>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<BooksPublishersLink>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<BooksSeriesLink>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<BooksTagsLink>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<Datum>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<Identifier>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Type).HasDefaultValueSql("\"isbn\"");
            });

            modelBuilder.Entity<Language>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<Publisher>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<Series>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
