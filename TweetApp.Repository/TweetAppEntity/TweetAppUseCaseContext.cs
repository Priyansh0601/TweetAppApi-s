using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using MongoDB.Driver;

#nullable disable

namespace TweetApp.Repository.TweetAppEntity
{
    public partial class TweetAppUseCaseContext : DbContext
    {
        public TweetAppUseCaseContext()
        {
        }

        public TweetAppUseCaseContext(DbContextOptions<TweetAppUseCaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Tweet> Tweets { get; set; }
        public virtual DbSet<TweetComment> TweetComments { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server=.;database=TweetAppUseCase;trusted_connection=yes");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Tweet>(entity =>
            {
                entity.Property(e => e.TweetId).HasColumnName("id");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UserTweets)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TweetComment>(entity =>
            {
                entity.Property(e => e.Comments)
                    .HasMaxLength(144)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .HasMaxLength(75)
                    .IsUnicode(false)
                    .HasColumnName("userId");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.EmailId)
                    .HasName("PK__Users__87355E5204319B2D");

                entity.Property(e => e.EmailId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("emailID");

                entity.Property(e => e.Dob)
                    .HasColumnType("date")
                    .HasColumnName("DOB");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("firstname");

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("gender");

                entity.Property(e => e.ImgName)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("imgname");

                entity.Property(e => e.LastName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("lastname");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("password");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

       // var dbContext = new MongoClient(config.GetConnectionString("TweetAppCon"));
        
        }
}
