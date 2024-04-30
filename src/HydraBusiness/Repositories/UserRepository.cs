using HydraDataAccess.Models;

namespace HydraBusiness;

public class UserRepository : IUserRepository
{   
    private readonly HydraContext _dbContext;

    public UserRepository(HydraContext dbContext)
    {
        _dbContext = dbContext;
    }

    public User? GetUserByUsername(string username)
    {
        var users = _dbContext.Users;
        return users.Find(username);
    }
}
