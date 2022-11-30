using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Timeboxed.Api.Models.Requests;
using Timeboxed.Api.Models.Responses;
using Timeboxed.Api.Services.Interfaces;
using Timeboxed.Core.AccessControl.Interfaces;
using Timeboxed.Core.Cryptography;
using Timeboxed.Core.Exceptions;
using Timeboxed.Data;
using Timeboxed.Domain.Models;

namespace Timeboxed.Api.Services
{
    public class UserService : IUserService
    {
        private HashingManager hashing = new();

        private TimeboxedContext context;
        private IConfiguration configuration;
        private IUserContextProvider userContextProvider;

        public UserService(TimeboxedContext context, IConfiguration configuration, IUserContextProvider userContextProvider)
        {
            this.context = context;
            this.configuration = configuration;
            this.userContextProvider = userContextProvider;
        }

        public async Task<bool> UserExistsAsync(UserRequest user, CancellationToken cancellationToken) =>
            await this.context.Users.Where(u =>
                u.Username.Equals(user.Username) ||
                u.Email.Equals(user.Email) ||
                (u.Username.Equals(user.UsernameOrEmail) || u.Email.Equals(user.UsernameOrEmail)))
            .SingleOrDefaultAsync(cancellationToken) != null;

        public async Task<bool> UserExistsAsync(Guid userId, CancellationToken cancellationToken) =>
            await this.context.Users.Where(u => u.Id.Equals(userId)).SingleOrDefaultAsync(cancellationToken) != null;

        public async Task<UserResponse> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken)
        {
            var user = await this.context.Users
                .Where(u => u.Id == userId)
                .Include(u => u.Preferences)
                .SingleOrDefaultAsync(cancellationToken)
            ?? throw new EntityNotFoundException($"User {userId} not found");

            return new UserResponse
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                IsAdmin = user.IsAdmin,
                Token = GenerateToken(user),
                Preferences = new UserPreferencesResponse
                {
                    DefaultGroup = user.Preferences.DefaultGroup,
                },
            };
        }

        public async Task<User> GetUserByUsernameOrEmailAsync(string usernameOrEmail, CancellationToken cancellationToken) =>
            await this.context.Users.Where(u => u.Username == usernameOrEmail || u.Email == usernameOrEmail).SingleOrDefaultAsync(cancellationToken);

        public async Task<UserResponse> CreateUserAsync(UserRequest userRequest, CancellationToken cancellationToken)
        {
            var user = new User
            {
                Username = userRequest.Username,
                Email = userRequest.Email,
                Password = hashing.HashToString(userRequest.Password),
            };

            await this.context.Users.AddAsync(user, cancellationToken);
            await this.context.SaveChangesAsync(cancellationToken);

            return await this.GetUserByIdAsync(user.Id, cancellationToken);
        }

        public async Task<UserResponse?> AuthenticateUserAsync(UserRequest userRequest, CancellationToken cancellationToken)
        {
            var user = await this.GetUserByUsernameOrEmailAsync(userRequest.UsernameOrEmail, cancellationToken);

            return hashing.Verify(userRequest.Password, user.Password)
                ? await this.GetUserByIdAsync(user.Id, cancellationToken)
                : null;
        }

        public async Task<UserPreferencesResponse> UpdateUserPreferencesAsync(UserPreferencesResponse requestBody, CancellationToken cancellationToken)
        {
            var user = await this.context.Users
                .Where(u => u.Id.Equals(this.userContextProvider.UserId))
                .Include(u => u.Preferences)
                .SingleOrDefaultAsync(cancellationToken);

            user.Preferences = new UserPreferences
            {
                DefaultGroup = requestBody.DefaultGroup,
            };

            await this.context.SaveChangesAsync(cancellationToken);

            return new UserPreferencesResponse { Id = user.Preferences.Id, DefaultGroup = user.Preferences.DefaultGroup };
        }

        private string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.configuration["JwtPrivateKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var secToken = new JwtSecurityToken(
                issuer: "timeboxed",
                audience: "timeboxed",
                signingCredentials: credentials,
                claims: new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email)
                },
                expires: DateTime.UtcNow.AddDays(14));

            return new JwtSecurityTokenHandler().WriteToken(secToken);
        }
    }
}
