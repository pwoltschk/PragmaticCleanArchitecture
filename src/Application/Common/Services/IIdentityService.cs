﻿namespace Application.Common.Services;

public interface IIdentityService
{
    Task<IList<Role>> GetRolesAsync(CancellationToken cancellationToken);

    Task DeleteUserAsync(string userId);


    Task CreateRoleAsync(Role role);

    Task UpdateRoleAsync(Role role);

    Task DeleteRoleAsync(string id);

    Task<IList<User>> GetUsersAsync(CancellationToken cancellationToken);

    Task<User> GetUserAsync(string id);

    Task UpdateUserAsync(User user);
}