using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BD_temp
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Company> Companies { get; set; }
        public DbSet<User> Users { get; set; }
        //public DbSet<UserCompany> UsersCompany { get; set; }
        public ApplicationContext()
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserCompany>()
                .HasKey(t => new { t.CompanyId, t.UserId });

            modelBuilder.Entity<UserCompany>()
                .HasOne(sc => sc.User)
                .WithMany(s => s.UserCompany)
                .HasForeignKey(sc => sc.UserId);

            modelBuilder.Entity<UserCompany>()
                .HasOne(sc => sc.Company)
                .WithMany(c => c.UserCompany)
                .HasForeignKey(sc => sc.CompanyId);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=empty2db;Trusted_Connection=True;");
        }
    }
}