using NodaTime;

namespace IdentityNpgsqlNodatime.Core.Shared
{
    public abstract class BaseEntityWithTimestamps : BaseEntity
    {
        public Instant CreatedAt { get; set; }
        public Instant UpdatedAt { get; set; }
    }
}
