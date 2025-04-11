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

    const string GetUserQuery = "SELECT * FROM public.\"Users\" WHERE \"Email\" = @Email AND \"Password\" = @Password;";
    const string AddUserQuery = "INSERT INTO public.\"Users\"(\"UserID\", \"Email\", \"Name\", \"Gender\", \"Password\") VALUES(@UserID, @Email, @Name, @Gender, @Password)";

    public async Task<ApplicationUser?> AddUser(ApplicationUser user)
    {
        // Generate a new unique user id for the user
        user.UserId = Guid.NewGuid();
        var rowCount = await _dbContext.DbConnection.ExecuteAsync(AddUserQuery, user);
        return rowCount > 0 ? user : null;
    }

    public async Task<ApplicationUser?> GetUser(string? email, string? password)
    {
        var parameters = new { Email = email, Password = password };
        return await _dbContext.DbConnection.QueryFirstOrDefaultAsync<ApplicationUser>(GetUserQuery, parameters);
    }
}
