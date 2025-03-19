using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MovieCatalogAPI.Data.Identity;
using MovieCatalogAPI.DTO.Models.Account;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MovieCatalogAPI.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    [ApiController]
    [EnableCors("CorePolicy")]
    public class AccountController : ControllerBase
    {
        //https://docs.microsoft.com/en-us/aspnet/core/security/authentication/accconfirm?tabs=aspnetcore2x%2Csql-server

        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        //private readonly IEmailSenderRepository emailSender;
        private readonly ILogger logger;
        private readonly IConfiguration config;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            /*IEmailSenderRepository emailSender,*/
            ILogger<AccountController> logger,
            IConfiguration config)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            //this.emailSender = emailSender;
            this.logger = logger;
            this.config = config;
        }


        [AllowAnonymous]
        //[ValidateModel]
        [HttpPost]
        public async Task<IActionResult> GenerateToken([FromBody] LoginModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    var result = await signInManager.CheckPasswordSignInAsync(user, model.Password, false);

                    if (result.Succeeded)
                    {

                        var claims = new[]
                        {
                          new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                          new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        };

                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Tokens:Key"]));
                        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                        var token =
                           new JwtSecurityToken(
                           config["Tokens:Issuer"],
                           config["Tokens:Audience"],
                           claims,
                           expires: DateTime.Now.AddMinutes(30),
                           signingCredentials: creds);


                        return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token), validTo = token.ValidTo, user = user });
                    }
                    else
                    {
                        return new NotFoundObjectResult("Please check you user name or password.");
                    }
                }
                else
                {
                    return new NotFoundObjectResult("The user does not exist in the system.");
                }
            }

            return new BadRequestObjectResult("The account is not authorized to login from this station.");
        }
    }
}
