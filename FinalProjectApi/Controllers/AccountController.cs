using FinalProjectApi.Dto.AccountDto;
using FinalProjectApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FinalProjectApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration configuration;

        public AccountController(UserManager<ApplicationUser> userManager, IConfiguration Configuration)
        {
            this.userManager=userManager;
            this.configuration=Configuration;
        }


        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser appUser = new ApplicationUser();
                appUser.UserName = registerDto.UserName;
                appUser.Email = registerDto.Email;

                IdentityResult result = await userManager.CreateAsync(appUser, registerDto.Password);
                if (result.Succeeded)
                {
                    return Ok("created");
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("password", item.Description);
                }


            }
            return BadRequest(ModelState);
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // 1️⃣ البحث عن المستخدم
            var user = await userManager.FindByNameAsync(loginDto.userName);
            if (user == null)
                return Unauthorized(new { message = "اسم المستخدم أو كلمة المرور غير صحيحة." });

            // 2️⃣ التحقق من كلمة المرور
            var isPasswordValid = await userManager.CheckPasswordAsync(user, loginDto.password);
            if (!isPasswordValid)
                return Unauthorized(new { message = "اسم المستخدم أو كلمة المرور غير صحيحة." });

            // 3️⃣ إنشاء Claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // إضافة الأدوار
            var roles = await userManager.GetRolesAsync(user);
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            // 4️⃣ إعداد مفتاح التشفير والتوقيع
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecritKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // 5️⃣ إنشاء التوكن
            var token = new JwtSecurityToken(
                issuer: configuration["JWT:autherId"],
                audience: configuration["JWT:AudiansId"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            // 6️⃣ إرسال التوكن للعميل
            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            });
        }



    }
}
