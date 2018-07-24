using IdentityNpgsqlNodatime.Core.Shared;
using Microsoft.AspNetCore.Identity;

namespace IdentityNpgsqlNodatime.Core.Entities
{
    public class Membership : BaseEntityWithTimestamps
    {
        public ApplicationUser User { get; set; }
        public Organization Organization { get; set; }
        public IdentityRole Role { get; set; }
    }
}
