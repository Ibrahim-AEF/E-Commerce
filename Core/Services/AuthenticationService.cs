using Domain.Entities.Identity;
using Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Services.Abstractions;
using Shared.Security;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AuthenticationService(UserManager<User> userManager,IConfiguration configuration,IOptions<Jwtoptions> options) : IAuthenticationService
    {
        public async Task<UserResultDto> LoginAsync(LoginDto loginDto)
        {
            //Check If There Is User Under This Email
            var User = await userManager.FindByEmailAsync(loginDto.Email);
            if (User == null) throw new UnAuthorizedExceptions("Incorrect Email"); //Email Validation
            //Check if The Password Is Correct Fot This Email
            var Result = await userManager.CheckPasswordAsync(User, loginDto.Password);
            if (!Result) throw new UnAuthorizedExceptions("Incorrect Password");//For Password Validation
            //Create Token And Return Response
            return new UserResultDto(
                User.DisplayName,
                User.Email,
                await CreateTokenAsync(User)
                );
        }

        public async Task<UserResultDto> RegisterAsync(UserRegisterDto registerDto)
        {
            var User = new User()
            {
                Email=registerDto.Email,
                DisplayName=registerDto.DisplayName,
                PhoneNumber=registerDto.PhoneNumber,
                UserName=registerDto.UserName
            };
            var Result = await userManager.CreateAsync(User, registerDto.Password);
            if(!Result.Succeeded)
            {
                var errors = Result.Errors.Select(a => a.Description).ToList();
                throw new RegisterValidationExceptions(errors);
            }
            return new UserResultDto(
                User.DisplayName,
                User.Email,
                await CreateTokenAsync(User)
                );
        }
        public async Task<string> CreateTokenAsync(User user)
        {
            var jwtoptions = options.Value;
            //Private Claims
            var AuthClaims = new List<Claim>
            { new Claim(ClaimTypes.Name,user.UserName!),
            new Claim(ClaimTypes.Email,user.Email!)};
            //Add Roles To Claims If Exist
            var Roles = await userManager.GetRolesAsync(user);
            foreach(var role in Roles)
            {
                AuthClaims.Add(new Claim(ClaimTypes.Role, role));
            }
            //For Secret Key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtoptions.SecretKey));//For Key
            var SigningCredintials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            //Token
            var Token = new JwtSecurityToken(
                audience: jwtoptions.Audience,
                issuer: jwtoptions.Issure,
                expires: DateTime.UtcNow.AddDays(jwtoptions.DurationsInDays),
                claims: AuthClaims,
                signingCredentials: SigningCredintials
                );
            return new JwtSecurityTokenHandler().WriteToken(Token);
        }
    }
}
