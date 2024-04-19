﻿using AllForAll.Models;
using BusinessLogic.Dto.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IUserService
    {
        Task<ICollection<User>> GetAllUsersAsync(CancellationToken cancellation = default);
        Task<User> GetUserByIdAsync(int id, CancellationToken cancellation = default);

        Task<bool> IsUserExistAsync(int id, CancellationToken cancellation = default);

        Task<int> CreateUserAsync(UserRequestDto user, CancellationToken cancellation = default);

        Task UpdateUserAsync(int id, UserRequestDto user, CancellationToken cancellation = default);

        Task DeleteUserAsync(int id, CancellationToken cancellation = default);

    }
}
