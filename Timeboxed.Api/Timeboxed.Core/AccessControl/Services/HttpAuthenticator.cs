using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Timeboxed.Core.AccessControl.Interfaces;
using Timeboxed.Core.Extensions;
using Timeboxed.Data;
using Timeboxed.Domain.Models;

namespace Timeboxed.Core.AccessControl.Services
{
    internal class HttpAuthenticator : IAuthenticator
    {
        private readonly ILogger<HttpAuthenticator> logger;
        private readonly TimeboxedContext context;
        private readonly JwtSecurityTokenHandler handler;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IConfiguration configuration;
        private Guid? userId;
        private User? user;

        public HttpAuthenticator(ILogger<HttpAuthenticator> logger, TimeboxedContext context, JwtSecurityTokenHandler handler, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            this.logger = logger;
            this.context = context;
            this.handler = handler;
            this.httpContextAccessor = httpContextAccessor;
            this.configuration = configuration;
        }

        public Guid UserId => this.userId ?? throw new NotSupportedException("User ID cannot be accessed before authentication");

        public User User => this.user ?? throw new NotSupportedException("User cannot be accessed before authentication");

        public async Task<bool> AuthenticateAsync()
        {
            if (!this.httpContextAccessor.HttpContext.Request.TryGetAuthHeader(out var token))
            {
                return false;
            };

            this.handler.ValidateToken(
                token,
                new TokenValidationParameters()
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.configuration["JwtPrivateKey"]))
                },
                out var validatedToken);

            if (!Guid.TryParse((validatedToken as JwtSecurityToken).Subject, out var userId))
            {
                return false;
            }

            // Will throw an exception if the user doesn't exist causing a 401
            var currentUser = await this.context.Users.Where(u => u.Id == userId).Include(u => u.Preferences).FirstAsync();

            if (currentUser == null)
            {
                return false;
            }

            this.userId = userId;
            this.user = currentUser;

            return true;
        }
    }
}
