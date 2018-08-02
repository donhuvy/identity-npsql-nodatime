using System.Threading.Tasks;
using IdentityNpgsqlNodatime.Core.Entities;
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
        
        [HttpGet("id")]
        public async Task<IActionResult> Show(long id)
        {
            var organization = await _dbContext.Organizations.FindAsync(id);

            return Json(organization);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Organization model)
        {
            var organization = await _dbContext.Organizations.AddAsync(model);
            await _dbContext.SaveChangesAsync();

            return Created($"/organizations/{organization.Entity.Id}", model);
        }
    }
}
