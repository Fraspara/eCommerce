using eCommerce.Core.Dto;
using eCommerce.Core.Entities;
using eCommerce.Core.RepositoryContracts;

namespace eCommerce.Infrastructure.Repositories;

public class UsersRepository : IUsersRepository
{
    public async Task<ApplicationUser?> AddUser(ApplicationUser user)
    {
        // Generate a new unique user id for the user
        user.UserId = Guid.NewGuid();
        return user;
    }

    public async Task<ApplicationUser?> GetUser(string? email, string? password)
    {
        return new()
        {
            UserId = Guid.NewGuid(),
            Email = email,
            Password = password,
            Name = "Dummy person name",
            Gender = GenderOptions.Male.ToString()
        };
    }
}
