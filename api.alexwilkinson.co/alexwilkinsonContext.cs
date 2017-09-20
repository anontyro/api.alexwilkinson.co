using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace api.alexwilkinson.co
{
    public partial class alexwilkinsonContext : DbContext
    {
        public virtual DbSet<Author> Author { get; set; }
        public virtual DbSet<AuthorAccess> AuthorAccess { get; set; }
        public virtual DbSet<BlogPost> BlogPost { get; set; }
        public virtual DbSet<Portfolio> Portfolio { get; set; }


        public alexwilkinsonContext(DbContextOptions<alexwilkinsonContext> options)
               : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>(entity =>
            {
                entity.ToTable("author");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever();

                entity.Property(e => e.AuthorAccess)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.AuthorActive).HasColumnType("int(11)");

                entity.Property(e => e.AuthorEmail).IsRequired();

                entity.Property(e => e.AuthorPassword).IsRequired();
            });

            modelBuilder.Entity<AuthorAccess>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever();

                entity.Property(e => e.AccessEnabled).HasColumnType("bit(1)");

                entity.Property(e => e.AuthorAbleToCreate).HasColumnType("bit(1)");

                entity.Property(e => e.AuthorAbleToDelete).HasColumnType("bit(1)");

                entity.Property(e => e.AuthorAbleToUpdate).HasColumnType("bit(1)");
            });

            modelBuilder.Entity<BlogPost>(entity =>
            {
                entity.ToTable("blog_post");

                entity.HasIndex(e => e.AuthorId)
                    .HasName("blog_post_4f331e2f");

                entity.HasIndex(e => e.Slug)
                    .HasName("sqlite_autoindex_blog_post_1")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever();

                entity.Property(e => e.AuthorId)
                    .HasColumnName("author_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Body)
                    .IsRequired()
                    .HasColumnName("body");

                entity.Property(e => e.CoverImg)
                    .IsRequired()
                    .HasColumnName("cover_img")
                    .HasMaxLength(100);

                entity.Property(e => e.Date)
                    .IsRequired()
                    .HasColumnName("date");

                entity.Property(e => e.Draft)
                    .IsRequired()
                    .HasColumnName("draft");

                entity.Property(e => e.LastModified).HasColumnName("last_modified");

                entity.Property(e => e.Publish)
                    .IsRequired()
                    .HasColumnName("publish");

                entity.Property(e => e.Slug)
                    .IsRequired()
                    .HasColumnName("slug")
                    .HasMaxLength(50);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasMaxLength(140);
            });

            modelBuilder.Entity<Portfolio>(entity =>
            {
                entity.ToTable("portfolio");

                entity.Property(e => e.Portfolioid)
                    .HasColumnName("portfolioid")
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever();

                entity.Property(e => e.Portfolioauthorid)
                    .HasColumnName("portfolioauthorid")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Portfoliobody)
                    .HasColumnName("portfoliobody")
                    .HasMaxLength(45);

                entity.Property(e => e.Portfoliodate)
                    .HasColumnName("portfoliodate")
                    .HasColumnType("datetime");

                entity.Property(e => e.Portfoliodraft)
                    .HasColumnName("portfoliodraft")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.Portfoliofeaturedimage)
                    .HasColumnName("portfoliofeaturedimage")
                    .HasMaxLength(45);

                entity.Property(e => e.Portfoliolastmodified)
                    .HasColumnName("portfoliolastmodified")
                    .HasColumnType("datetime");

                entity.Property(e => e.Portfoliopublished)
                    .HasColumnName("portfoliopublished")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Portfolioslug)
                    .HasColumnName("portfolioslug")
                    .HasMaxLength(45);

                entity.Property(e => e.Portfoliotitle)
                    .HasColumnName("portfoliotitle")
                    .HasMaxLength(45);
            });
        }
    }
}
