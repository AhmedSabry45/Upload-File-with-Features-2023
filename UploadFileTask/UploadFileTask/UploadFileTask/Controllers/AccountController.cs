using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Runtime.InteropServices;
using UploadFile.Entities.Models;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;

namespace UploadFile.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private UserManager<IdentityUser> _userManager;

        public AccountController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        [Route("Login")]
        public async Task<ApplicationUserModel> Login(string Username, string Password)
        {
           
            try
            {
                var user = await _userManager.FindByNameAsync(Username);

                if (user != null && await _userManager.CheckPasswordAsync(user,Password))
                {



                    var key = Encoding.UTF8.GetBytes("1234567890123456");
                    var role = await _userManager.GetRolesAsync(user);
                    IdentityOptions _options = new IdentityOptions();



                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                         {
                         new Claim("UserID" , user.Id.ToString()),
                         new Claim(_options.ClaimsIdentity.RoleClaimType, role.FirstOrDefault())
                         }),
                        Expires = DateTime.UtcNow.AddSeconds(30),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                    var token = tokenHandler.WriteToken(securityToken);


                    ApplicationUserModel userModel = new ApplicationUserModel
                    {
                        UserName = user.UserName,
                        Token = token,
                    };
                    return userModel;
                }
                else
                    return null;

            }
            catch (Exception ex)
            {

                throw;
            }
               }


              [HttpPost]
        [Route("Register")]

        public async Task<bool> Register( AddUserModel model)

        {

            if (ModelState.IsValid)

            {
                IdentityUser appUser = new IdentityUser

                {
                    UserName = model.UserName,
                    Email = model.Email,
                };

                try

                {
                    var result = await _userManager.CreateAsync(appUser, model.Password);
                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(appUser, "BasicUser");
                        return true;
                    }
                    else
                        return false;
                }

                catch (Exception ex)

                {
                    return false;
                }

            }
           else
                return false;
        }

    }
}

