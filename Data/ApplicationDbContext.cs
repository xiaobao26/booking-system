using booking_system.Models;
using Microsoft.EntityFrameworkCore;

namespace booking_system.Data;

public class ApplicationDbContext: DbContext
{
   public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
   {
      
   }
   
   public virtual DbSet<User> Users { get; set; }
   public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

   protected override void OnModelCreating(ModelBuilder modelBuiler)
   {
      base.OnModelCreating(modelBuiler);

      ConfigureUserEntity(modelBuiler);
      ConfigureRefreshToken(modelBuiler);
   }

   private void ConfigureUserEntity(ModelBuilder modelBuilder)
   {
      modelBuilder.Entity<User>(e =>
      {
         e.ToTable("User");
         e.HasKey(u => u.Id);
         e.Property(u => u.Email).IsRequired().HasMaxLength(255);
         e.Property(u => u.Password).IsRequired().HasMaxLength(128);
         e.Property(u => u.EmailVerified).HasDefaultValue(false);
         
         e.HasIndex(u => u.Email).IsUnique();
      });
   }

   private void ConfigureRefreshToken(ModelBuilder modelBuilder)
   {
      modelBuilder.Entity<RefreshToken>(e =>
      {
         e.ToTable("RefreshToken");
         e.HasKey(r => r.Id);
         e.Property(r => r.Token).HasMaxLength(200);

         e.HasIndex(r => r.Token).IsUnique();
         e.HasOne<User>(r => r.User)
            .WithMany()
            .HasForeignKey(r => r.UserId);
      });
   }
}