using FinalProjectApi.Dto.AccountDto;
using FinalProjectApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRolesController : ControllerBase
    {

        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public UserRolesController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        [HttpPost("AssignRole")]
        public async Task<IActionResult> AssignRole([FromBody] AssignRoleDto dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.UserId) || string.IsNullOrWhiteSpace(dto.RoleName))
                return BadRequest(new { message = "UserId and RoleName are required" });

            var user = await userManager.FindByIdAsync(dto.UserId);
            if (user == null)
                return NotFound(new { message = "User not found" });

            // «· √ﬂœ „‰ ÊÃÊœ «·œÊ—
            if (!await roleManager.RoleExistsAsync(dto.RoleName))
                await roleManager.CreateAsync(new IdentityRole(dto.RoleName));

            // «· √ﬂœ √‰ «·„” Œœ„ ·Ì” ·œÌÂ «·œÊ— „”»ﬁ«
            if (await userManager.IsInRoleAsync(user, dto.RoleName))
                return BadRequest(new { message = "User already has this role" });

            await userManager.AddToRoleAsync(user, dto.RoleName);

            return Ok(new { message = $"Role '{dto.RoleName}' assigned to user '{user.UserName}' successfully" });
        }
    }
}
