using ApartmentsBilling.Entity.Entities;
using ApartmentsBilling.Entity.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApartmentsBilling.DataAccesLayer.Contexts
{
    public class ApartmentDbContext : DbContext
    {
        public ApartmentDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Apartment> Apartments { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<BillType> BillTypes { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Flat> Flats { get; set; }
        public DbSet<UserPassword> UserPasswords { get; set; }
        public DbSet<Message> Messages { get; set; }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries();
            foreach (var entry in entries)
            {
                if (entry.Entity is not BaseEntity entity) continue;
                var now = DateTime.Now;
                switch (entry.State)
                {
                    case EntityState.Added:
                        entity.Status = true;
                        entity.CreatedDate = now;
                        break;

                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entity.Status = false;
                        entity.UpdateDate = now;
                        break;


                    case EntityState.Modified:
                        entry.State = EntityState.Modified;
                        entity.Status = true;
                        entity.UpdateDate = now;
                        break;

                }
            }
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}
