using IdentityNpgsqlNodatime.Core.Shared;

namespace IdentityNpgsqlNodatime.Core.Entities
{
    public class Organization : BaseEntityWithTimestamps
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
