using api_docmanager.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api_docmanager.Controllers
{
    [ApiController]
    [Route("api/roles")]
    public class UserRolesController: ControllerBase
    {
        private readonly DocManagerContext _context;
        
        public UserRolesController(DocManagerContext context)
        {
            this._context = context;                   
        }
        
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var roles = await this._context.UserRoles.ToListAsync();

            return Ok(new
            {
                code = 200,
                message = "User roles retrieved successfully.",
                data = roles
            });
        }
    }
}
