using Microsoft.AspNetCore.Identity;
using NodaTime;

namespace IdentityNpgsqlNodatime.Core.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public Instant CreatedAt { get; set; }
        public Instant UpdatedAt { get; set; }
    }
}
