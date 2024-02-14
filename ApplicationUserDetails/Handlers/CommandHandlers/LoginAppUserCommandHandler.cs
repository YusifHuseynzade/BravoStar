using ApplicationUserDetails.Commands.Request;
using ApplicationUserDetails.Commands.Response;
using Common.Interfaces;
using Domain.Entities;
using Domain.IRepositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Application.ApplicationUserDetails.Commands
{
    public class LoginAppUserCommandHandler : IRequestHandler<LoginAppUserCommandRequest, LoginAppUserCommandResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IAppUserRepository _repository;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public LoginAppUserCommandHandler(IApplicationDbContext context, IConfiguration configuration, IAppUserRepository repository, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _configuration = configuration;
            _repository = repository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<LoginAppUserCommandResponse> Handle(LoginAppUserCommandRequest request, CancellationToken cancellationToken)
        {
            //Hash the password provided in the login request
            var hashedPassword = request.Password;


            //Retrieve the user from the database based on the provided username
            var appUser = await _context.AppUsers.Include(m => m.AppUserRoles).ThenInclude(m => m.Role).FirstOrDefaultAsync(u => u.Badge == request.Badge);

            //Check credentials and if the user is Active
            if (appUser == null)
            {
                return new LoginAppUserCommandResponse { IsSuccess = false, Message = "Account with this username doesn't exist" };
            }

            if (appUser.Password == hashedPassword)
            {

                var jwtToken = GenerateJwtToken(appUser);
                string refreshToken = await SetRefreshToken(appUser, cancellationToken);
                return new LoginAppUserCommandResponse
                {
                    IsSuccess = true,
                    Message = "OTP confirmation successful, redirecting",
                    JwtToken = jwtToken,
                    RefreshToken = refreshToken,
                    UserId = appUser.Id
                };
            }
            return new LoginAppUserCommandResponse { IsSuccess = false };
        }

        private string GenerateJwtToken(AppUser appUser)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                    _configuration.GetSection("AppSettings:Token").Value));

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, appUser.Id.ToString()),
                new Claim(ClaimTypes.Name, appUser.FullName),

            };
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var jwtToken = new JwtSecurityToken
            (
                    claims: claims,
                    expires: DateTime.Now.AddYears(1),
                    signingCredentials: creds
            );
            var jwt = tokenHandler.WriteToken(jwtToken);
            return jwt;
        }

        private async Task<string> SetRefreshToken(AppUser appUser, CancellationToken cancellationToken)
        {
            string refreshToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
            DateTime refreshTokenExpireTime = DateTime.Now.AddDays(365).ToUniversalTime();
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = refreshTokenExpireTime
            };
            _httpContextAccessor?.HttpContext?.Response
                .Cookies.Append("refreshToken", refreshToken, cookieOptions);

            appUser.RefreshToken = refreshToken;
            //appUser.RefreshTokenCreated = DateTime.Now.ToUniversalTime();
            //appUser.RefreshTokenExpires = refreshTokenExpireTime;

            await _context.SaveChangesAsync(cancellationToken);

            return refreshToken;
        }
    }
}
