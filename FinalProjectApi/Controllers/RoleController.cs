using FinalProjectApi.Dto.AccountDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {

            private readonly RoleManager<IdentityRole> roleManager;

            public RoleController(RoleManager<IdentityRole> roleManager)
            {
                this.roleManager = roleManager;
            }

            // 1️⃣ جلب كل الرولز
            [HttpGet("GetAllRoles")]
            public IActionResult GetAllRoles()
            {
                var roles = roleManager.Roles.ToList().Select(item => new RoleDto
                {
                    Id = item.Id,
                    Name = item.Name
                }).ToList();
          
                return Ok(roles);
            }

        // 2️⃣ إنشاء Role جديد
        [HttpPost("CreateRole")]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleDto createRole)
        {
            if (createRole == null || string.IsNullOrWhiteSpace(createRole.Name))
                return BadRequest(new { message = "Role name is required" });

            if (await roleManager.RoleExistsAsync(createRole.Name))
                return BadRequest(new { message = "Role already exists" });

            var result = await roleManager.CreateAsync(new IdentityRole(createRole.Name));

            if (result.Succeeded)
                return Ok(new { message = "Role created successfully", role = createRole });

            // إعادة الأخطاء بطريقة مفهومة
            var errors = result.Errors.Select(e => e.Description);
            return BadRequest(new { errors });
        }


        // 3️⃣ تعديل اسم Role موجود
        [HttpPut("UpdateRole/{roleId}")]
        public async Task<IActionResult> UpdateRole(string roleId, [FromBody] CreateRoleDto createRole)
        {
            if (createRole == null || string.IsNullOrWhiteSpace(createRole.Name))
                return BadRequest(new { message = "New role name is required" });

            var role = await roleManager.FindByIdAsync(roleId);
            if (role == null)
                return NotFound(new { message = "Role not found" });

            role.Name = createRole.Name;
            var result = await roleManager.UpdateAsync(role);

            if (result.Succeeded)
                return Ok(new { message = "Role updated successfully", role = createRole });

            // إعادة الأخطاء بطريقة واضحة
            var errors = result.Errors.Select(e => e.Description);
            return BadRequest(new { errors });
        }



        // 4️⃣ حذف Role (اختياري)
        [HttpDelete("DeleteRole/{roleId}")]
        public async Task<IActionResult> DeleteRole(string roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId);
            if (role == null)
                return NotFound(new { message = "Role not found" });

            var result = await roleManager.DeleteAsync(role);

            if (result.Succeeded)
                return Ok(new { message = "Role deleted successfully" });

            // إعادة الأخطاء بطريقة واضحة
            var errors = result.Errors.Select(e => e.Description);
            return BadRequest(new { errors });
        }

    } 
}


