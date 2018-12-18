using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;


namespace RepManagement.Models
{
    public class RepManagementContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Dictionary> Dictionaries { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Mould> Moulds { get; set; }
        public DbSet<SystemImage> SystemImages { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Production> Productions { get; set; }
        public DbSet<ProductionImage> ProductionImages { get; set; }



        public RepManagementContext(DbContextOptions options) : base(options)
        {

        }

        private void addShadowProp<TEntry>(ModelBuilder modelBuilder) where TEntry : class
        {
            modelBuilder.Entity<TEntry>().Property<DateTime>(ConstData.ShadowPropName_CreatedDate);
            modelBuilder.Entity<TEntry>().Property<Guid>(ConstData.ShadowPropName_CreatedBy);
            modelBuilder.Entity<TEntry>().Property<DateTime>(ConstData.ShadowPropName_UpdatedDate);
            modelBuilder.Entity<TEntry>().Property<Guid>(ConstData.ShadowPropName_UpdatedBy);

            modelBuilder.Entity<TEntry>().Property<bool>(ConstData.ShadowPropName_IsDeleted).HasDefaultValue(false);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           //optionsBuilder.UseMySql("server=localhost;database=RepManagement;user=root;password=");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            addShadowProp<User>(modelBuilder);
            addShadowProp<Role>(modelBuilder);
            addShadowProp<UserRole>(modelBuilder);
            addShadowProp<Dictionary>(modelBuilder);
            addShadowProp<Material>(modelBuilder);
            addShadowProp<Vendor>(modelBuilder);
            addShadowProp<Mould>(modelBuilder);

            addShadowProp<Customer>(modelBuilder);
            addShadowProp<Production>(modelBuilder);

        }
    }
}
