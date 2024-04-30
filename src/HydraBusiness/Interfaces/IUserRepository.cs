using HydraDataAccess.Models;

namespace HydraBusiness;

public interface IUserRepository
{
    public User? GetUserByUsername(string username);
}
