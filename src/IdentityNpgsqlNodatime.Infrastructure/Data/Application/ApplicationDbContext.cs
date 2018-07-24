using System.Threading;
using System.Threading.Tasks;
using Humanizer;
using IdentityNpgsqlNodatime.Core.Entities;
using IdentityNpgsqlNodatime.Core.Extensions;
using IdentityNpgsqlNodatime.Core.Shared;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NodaTime;

namespace IdentityNpgsqlNodatime.Infrastructure.Data.Application
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        private readonly IClock _clock;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IClock clock)
            : base(options)
        {
            _clock = clock;
        }

        #region Entities

        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Membership> Memberships { get; set; }

        #endregion

        #region Overrides

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Use the new identity columns added in Postgresql 10.0
            builder.ForNpgsqlUseIdentityColumns();

            // Configure
            ConfigureOrganization(builder.Entity<Organization>());
            ConfigureMembership(builder.Entity<Membership>());

            // IMPORTANT
            // Apply Postgresql naming conventions
            foreach (var entity in builder.Model.GetEntityTypes())
            {
                // Replace table names
                entity.Relational().TableName = entity.Relational().TableName.Pluralize().ToSnakeCase();

                // Replace column names            
                foreach (var property in entity.GetProperties())
                {
                    property.Relational().ColumnName = property.Name.ToSnakeCase();
                }

                foreach (var key in entity.GetKeys())
                {
                    key.Relational().Name = key.Relational().Name.ToSnakeCase();
                }

                foreach (var key in entity.GetForeignKeys())
                {
                    key.Relational().Name = key.Relational().Name.ToSnakeCase();
                }

                foreach (var index in entity.GetIndexes())
                {
                    index.Relational().Name = index.Relational().Name.ToSnakeCase();
                }
            }
        }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntityWithTimestamps>())
            {
                var entity = entry.Entity;
                if (entry.State == EntityState.Added)
                {
                    entity.CreatedAt = _clock.GetCurrentInstant();
                    entity.UpdatedAt = _clock.GetCurrentInstant();
                }
                else if (entry.State == EntityState.Modified)
                {
                    entity.UpdatedAt = _clock.GetCurrentInstant();
                }
            }

            int result = base.SaveChanges();

            return result;
        }

        public override async Task<int> SaveChangesAsync(
            CancellationToken cancellationToken = default(CancellationToken))
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntityWithTimestamps>())
            {
                var entity = entry.Entity;
                if (entry.State == EntityState.Added)
                {
                    entity.CreatedAt = _clock.GetCurrentInstant();
                    entity.UpdatedAt = _clock.GetCurrentInstant();
                }
                else if (entry.State == EntityState.Modified)
                {
                    entity.UpdatedAt = _clock.GetCurrentInstant();
                }
            }

            int result = await base.SaveChangesAsync(cancellationToken);

            return result;
        }

        #endregion

        #region Configuration

        private void ConfigureOrganization(EntityTypeBuilder<Organization> builder)
        {
            builder.HasIndex(o => o.Name).IsUnique();
            builder.Property(o => o.Name).IsRequired();
        }

        private void ConfigureMembership(EntityTypeBuilder<Membership> builder)
        {
        }

        #endregion
    }
}
