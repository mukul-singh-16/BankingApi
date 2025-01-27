//The purpose of this file is to define the BankingAppContext, 
//which is the Entity Framework Core DbContext for your application. 
//It acts as the bridge between your database and your application, allowing you 
//to interact with the database using C# objects rather than writing raw SQL queries.

using BankingApp.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankingApp.Infrastructure.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        

        
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<TransactionEntity> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            base.OnModelCreating(modelBuilder);

            
            modelBuilder.Entity<UserEntity>(entity =>
            {
                entity.HasKey(e => e.Id); 
                entity.Property(e => e.Username).IsRequired().HasMaxLength(100); 
                entity.Property(e => e.Balance).IsRequired().HasColumnType("decimal(18,2)"); 
            });

            
            modelBuilder.Entity<TransactionEntity>(entity =>
            {
                entity.HasKey(e => e.Id); 
                entity.Property(e => e.Amount).IsRequired().HasColumnType("decimal(18,2)"); 
                entity.Property(e => e.Date).IsRequired(); 
                entity.Property(e => e.Type).IsRequired().HasMaxLength(50); 

                
                entity.HasOne<UserEntity>()
                      .WithMany()
                      .HasForeignKey(e => e.FromUserId)
                      .OnDelete(DeleteBehavior.Restrict); 

                entity.HasOne<UserEntity>()
                      .WithMany()
                      .HasForeignKey(e => e.ToUserId)
                      .OnDelete(DeleteBehavior.Restrict); 
            });
        }
    }
}
