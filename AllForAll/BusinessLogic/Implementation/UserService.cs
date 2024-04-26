using AllForAll.Models;
using AutoMapper;
using BusinessLogic.Dto.User;
using BusinessLogic.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BusinessLogic.Dto.UserRole;
using BusinessLogic.Helpers;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Newtonsoft.Json;
using System.Security.Cryptography;

namespace BusinessLogic.Implementation
{
    public class UserService : IUserService
    {
        private readonly AllForAllDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly JWTSettings _options;

        public UserService(AllForAllDbContext dbContext, IMapper mapper, IOptions<JWTSettings> optAccess)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _options = optAccess.Value;

        }

        private string ComputeObjectHash<T>(T obj)
        {
            var json = JsonConvert.SerializeObject(obj);

            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(json);
                var hashBytes = sha256.ComputeHash(bytes);

                return Convert.ToBase64String(hashBytes);
            }
        }

        public async Task<int> CreateUserAsync(UserRequestDto user, CancellationToken cancellation = default)
        {
            var mappedUser = _mapper.Map<User>(user);
            var passwordHash = ComputeObjectHash<string>(mappedUser.Password);
            mappedUser.Password = passwordHash;
            var createdUser = await _dbContext.Users.AddAsync(mappedUser, cancellation);
            await _dbContext.SaveChangesAsync(cancellation);
            return createdUser.Entity.UserId;
        }

        public async Task DeleteUserAsync(int id, CancellationToken cancellation = default)
        {
            var userToDelete = await _dbContext.Users.FindAsync(id, cancellation);
            if (userToDelete != null)
            {
                _dbContext.Users.Remove(userToDelete);
                await _dbContext.SaveChangesAsync(cancellation);
            }
        }

        public async Task<ICollection<User>> GetAllUsersAsync(CancellationToken cancellation = default)
        {
            return await _dbContext.Users.ToListAsync(cancellation);
        }

        public async Task<User> GetUserByIdAsync(int id, CancellationToken cancellation = default)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.UserId == id, cancellation);
        }

        public async Task<bool> IsUserExistAsync(int id, CancellationToken cancellation = default)
        {
            return await _dbContext.Users.AnyAsync(u => u.UserId == id, cancellation);
        }

        public async Task UpdateUserAsync(int id, UserRequestDto user, CancellationToken cancellation = default)
        {
            var userToUpdate = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserId == id, cancellation);
            if (userToUpdate != null)
            {
                _mapper.Map(user, userToUpdate);
                _dbContext.Update(userToUpdate);
                await _dbContext.SaveChangesAsync(cancellation);
            }
        }
        public async Task UpdateUserAsync(User user, CancellationToken cancellation = default)
        {
            _dbContext.Update(user);
            await _dbContext.SaveChangesAsync(cancellation);
        }
        public async Task<string> CreateTokenAsync(User user)
        {
            string IdString = user.UserId.ToString();
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, IdString));
            claims.Add(new Claim(ClaimTypes.Email, user.Email));
            claims.Add(new Claim(ClaimTypes.Name, user.Username));
            claims.Add(new Claim(ClaimTypes.Role, user.UserRole.Name));
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Secret));
            var jwt = new JwtSecurityToken(
                issuer: _options.Issuer,
                audience: _options.Audience,
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(100)),
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
            );
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.WriteToken(jwt);
            return token;
        }

        public async Task<string> LoginAsync(UserLoginRequestDto model)
        {
            string user = null;
            var passwordHash = ComputeObjectHash<string>(model.Password);
            var existinguser = await _dbContext.Users
                .Where(a => a.Password == passwordHash && a.Email == model.Email)
                .Include(a => a.UserRole)
                .FirstOrDefaultAsync();

            if (existinguser != null)
            {
                user = await CreateTokenAsync(existinguser);
                return user;
            }
            else
            {
                return user;
            }
        }
        public async Task<User> CheckToken(string token)
        {
            string secretKey = _options.Secret;
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey)),
                ValidateIssuer = false,
                ValidateAudience = false
            };
            try
            {
                var claimsPrincipal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);
                var UserId = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                var Email = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
                var Username = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
                var Role = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                int Idinteger = int.Parse(UserId);
                UserRole UserRole = new UserRole();
                UserRole.Name = Role;
                User user = new User()
                {
                    UserId = Idinteger,
                    Email = Email,
                    Username = Username,
                    UserRole = UserRole
                };

                return (user);
            }
            catch (SecurityTokenException)
            {
                return null;
            }
        }
    }
}
