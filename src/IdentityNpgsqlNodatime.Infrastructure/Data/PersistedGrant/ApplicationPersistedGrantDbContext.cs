using System;
using System.Threading.Tasks;
using Humanizer;
using IdentityNpgsqlNodatime.Core.Extensions;
using IdentityServer4.EntityFramework.Extensions;
using IdentityServer4.EntityFramework.Interfaces;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;

namespace IdentityNpgsqlNodatime.Infrastructure.Data.PersistedGrant
{
    public class ApplicationPersistedGrantDbContext : DbContext, IPersistedGrantDbContext
    {
        private readonly OperationalStoreOptions _storeOptions;

        public ApplicationPersistedGrantDbContext(DbContextOptions<ApplicationPersistedGrantDbContext> options,
            OperationalStoreOptions storeOptions)
            : base(options)
        {
            _storeOptions = storeOptions ?? throw new ArgumentNullException(nameof(storeOptions));
        }

        public DbSet<IdentityServer4.EntityFramework.Entities.PersistedGrant> PersistedGrants { get; set; }

        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigurePersistedGrantContext(_storeOptions);

            // Use the new identity columns added in Postgresql 10.0
            builder.ForNpgsqlUseIdentityColumns();

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
    }
}
