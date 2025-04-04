using Dapper;
using eCommerce.Core.Dto;
using eCommerce.Core.Entities;
using eCommerce.Core.RepositoryContracts;
using eCommerce.Infrastructure.DbContext;

namespace eCommerce.Infrastructure.Repositories;

public class UsersRepository : IUsersRepository
{
    private readonly DapperDbContext _dbContext;

    public UsersRepository(DapperDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ApplicationUser?> AddUser(ApplicationUser user)
    {
        // Generate a new unique user id for the user
        user.UserId = Guid.NewGuid();

        // SQL Query per dapper
        string query = "INSERT INTO public.\"Users\"(\"UserID\", \"Email\", \"Name\", \"Gender\", \"Password\") VALUES(@UserID, @Email, @Name, @Gender, @Password)";
        var rowCount = await _dbContext.DbConnection.ExecuteAsync(query, user);
        
        return rowCount > 0 ? user : null;
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
