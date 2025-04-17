using Domain.Entities.Identity;
using Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using Services.Abstractions;
using Shared.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AuthenticationService(UserManager<User> userManager) : IAuthenticationService
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
                "Token"
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
                "Token"
                );
        }
    }
}
