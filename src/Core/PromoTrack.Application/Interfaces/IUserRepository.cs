using PromoTrack.Domain;

namespace PromoTrack.Application.Interfaces;

/// <summary>
/// Defines the contract for data access operations related to User entities.
/// </summary>
public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllUsersAsync();

    Task<User?> GetUserByIdAsync(int id);

    Task<User> AddUserAsync(User user);

    Task UpdateUserAsync(User user);

    Task DeleteUserAsync(int id);

    Task<User?> GetUserByEmailAsync(string email);
}
