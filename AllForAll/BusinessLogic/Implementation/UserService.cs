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

namespace BusinessLogic.Implementation
{
    public class UserService : IUserService
    {
        private readonly AllForAllDbContext _dbContext;
        private readonly IMapper _mapper;

        public UserService(AllForAllDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<int> CreateUserAsync(UserRequestDto user, CancellationToken cancellation = default)
        {
            var mappedUser = _mapper.Map<User>(user);
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
    }
}
