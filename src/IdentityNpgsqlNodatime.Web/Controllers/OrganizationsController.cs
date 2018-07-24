using System.Threading.Tasks;
using IdentityNpgsqlNodatime.Infrastructure.Data.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IdentityNpgsqlNodatime.Web.Controllers
{
    [Route("organizations")]
    public class OrganizationsController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public OrganizationsController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var organizations = await _dbContext.Organizations.ToListAsync();

            return Json(organizations);
        }
    }
}
