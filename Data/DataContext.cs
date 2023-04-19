using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace hitachiv1.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options){
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<Employee> Employees => Set<Employee>();
        public DbSet<Department> Departments => Set<Department>();
        public DbSet<Asset> Assets => Set<Asset>();
        public DbSet<Area> Areas => Set<Area>();
        public DbSet<AssetClass> AssetClasses => Set<AssetClass>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Movement> Movements => Set<Movement>();
        //public DbSet<Department> Departments => Set<Department>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Host=127.0.0.1:5432;Database=hitachiv1;Username=hitachi_1;Password=pejuang45");

        public override int SaveChanges()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (
                    e.State == EntityState.Added || e.State == EntityState.Modified )
                );

            foreach (var entityEntry in entries)
            {
                ((BaseEntity)entityEntry.Entity).UpdatedDate = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntity)entityEntry.Entity).CreatedDate = DateTime.Now;
                }
            }
            
            return base.SaveChanges();
        }
    }
}