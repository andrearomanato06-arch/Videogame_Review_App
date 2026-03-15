using Microsoft.EntityFrameworkCore;
using GameRev.Models;
using GameRev.Models.Auth;
using GameRev.Models.Utils;
using GameRev.Models.Entities;

namespace GameRev.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Author> Authors { get; set; }
    public DbSet<Platform> Platforms { get; set; }
    public DbSet<Videogame> Videogames { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserSession> UserSessions { get; set; }
    public DbSet<VideogamePlatform> VideogamePlatforms { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(a =>
        {
            a.HasKey(a => a.Id);

            a.HasIndex(a => a.Name).IsUnique();

            a.Property(a => a.Id).ValueGeneratedOnAdd();
            a.Property(a => a.Name).HasColumnName("name").HasMaxLength(128).IsRequired();
        });

        modelBuilder.Entity<Platform>(p =>
        {
            p.HasKey(p => p.Id);

            p.HasIndex(p => p.Name).IsUnique();

            p.Property(p => p.Id).ValueGeneratedOnAdd();
            p.Property(p => p.Name).HasColumnName("name").HasMaxLength(128).IsRequired();
        });

        modelBuilder.Entity<Videogame>(v =>
        {
            v.HasKey(v => v.Id);
            
            v.Property(v => v.Id).ValueGeneratedOnAdd();
            v.Property(v => v.Title).HasColumnName("title").HasMaxLength(128).IsRequired();
            v.Property(v => v.Description).HasColumnName("description").HasMaxLength(int.MaxValue).IsRequired();
            v.Property(v => v.CoverImage).HasColumnName("cover_image").IsRequired();
            v.Property(v => v.ReleaseDate).HasColumnName("publication_date").IsRequired();
            v.Property(v => v.AuthorId).HasColumnName("author_id");
            v.Property(v => v.Released).HasColumnName("released").HasDefaultValue(true);

            v.HasOne(v => v.Author)
            .WithMany(a => a.Videogames)
            .HasForeignKey(v => v.AuthorId)
            .OnDelete(DeleteBehavior.SetNull);
        });

        modelBuilder.Entity<Review>(r =>
        {
            r.HasKey(r => r.Id);
           
            r.Property(r => r.Id).ValueGeneratedOnAdd();
            r.Property(r => r.Rating).HasColumnName("score");
            r.Property(r => r.Description).HasColumnName("description").HasMaxLength(int.MaxValue).IsRequired();
            r.Property(r => r.ReviewDate).HasColumnName("review_date").IsRequired();
            r.Property(r => r.VideogameId).HasColumnName("videogame_id");
            r.Property(r => r.UserId).HasColumnName("user_id");

            r.HasOne(r => r.Videogame)
            .WithMany(v => v.Reviews)
            .HasForeignKey(r => r.VideogameId)
            .OnDelete(DeleteBehavior.Cascade);

            r.HasOne(r => r.User)
            .WithMany(u => u.Reviews)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.SetNull);
        });

        modelBuilder.Entity<User>(u =>
        {
            u.HasKey(u => u.Id);
            
            u.HasIndex(u => u.Username).IsUnique();
            u.HasIndex(u => u.Email).IsUnique();
            
            u.Property(u => u.Id).ValueGeneratedOnAdd();
            u.Property(u => u.Username).HasColumnName("username").HasMaxLength(50).IsRequired();
            u.Property(u => u.Email).HasColumnName("email").HasMaxLength(64).IsRequired();
            u.Property(u => u.Password).HasColumnName("password").HasMaxLength(64).IsRequired();
            u.Property(u => u.RegistrationDate).HasColumnName("registration_date").IsRequired();
            u.Property(u => u.LastAccess).HasColumnName("last_access_date").IsRequired();
            u.Property(u => u.Role).HasColumnName("role").HasDefaultValue(UserRole.BASIC).IsRequired();
        });

        modelBuilder.Entity<VideogamePlatform>(vp =>
        {
            vp.HasKey(vp => new { vp.VideogameId, vp.PlatformId });
            
            vp.Property(vp => vp.VideogameId).HasColumnName("videogame_id");
            vp.Property(vp => vp.PlatformId).HasColumnName("platform_id");

            vp.HasOne(vp => vp.Videogame)
            .WithMany(v => v.VideogamePlatforms)
            .HasForeignKey(vp => vp.VideogameId)
            .OnDelete(DeleteBehavior.Cascade);
            
            vp.HasOne(vp => vp.Platform)
            .WithMany(p => p.VideogamePlatforms)
            .HasForeignKey(vp => vp.PlatformId)
            .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<UserSession>(j =>
        {
           j.HasKey(j => j.Jtid);
           
           j.Property(j => j.Jtid).ValueGeneratedOnAdd().HasColumnName("jwt_token_id");

           j.HasOne(j => j.User)
           .WithOne(u => u.UserSession)
           .HasForeignKey<UserSession>(j => j.UserId)
           .OnDelete(DeleteBehavior.Cascade);
        });
    }
}