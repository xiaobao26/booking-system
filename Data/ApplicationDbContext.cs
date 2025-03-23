using booking_system.Models;
using Microsoft.EntityFrameworkCore;

namespace booking_system.Data;

public class ApplicationDbContext: DbContext
{
   public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
   {
      
   }
   
   public virtual DbSet<User> Users { get; set; }

   protected override void OnModelCreating(ModelBuilder modelBuiler)
   {
      base.OnModelCreating(modelBuiler);

      ConfigureUserEntity(modelBuiler);
   }

   private void ConfigureUserEntity(ModelBuilder modelBuilder)
   {
      modelBuilder.Entity<User>(e =>
      {
         e.ToTable("User");
         e.HasKey(u => u.Id);
         e.Property(u => u.Email).IsRequired().HasMaxLength(255);
         e.Property(u => u.Password).IsRequired().HasMaxLength(255);

         e.HasIndex(u => u.Email).IsUnique();
      });
   }
}