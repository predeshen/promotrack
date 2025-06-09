using PromoTrack.Domain;

namespace PromoTrack.Application.Interfaces;

/// <summary>
/// Defines the contract for data access operations related to User entities.
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Retrieves all users from the data store.
    /// </summary>
    /// <returns>A collection of all users.</returns>
    Task<IEnumerable<User>> GetAllUsersAsync();

    /// <summary>
    /// Retrieves a single user by their unique identifier.
    /// </summary>
    /// <param name="id">The ID of the user to retrieve.</param>
    /// <returns>The user if found; otherwise, null.</returns>
    Task<User?> GetUserByIdAsync(int id);

    // We will add more method definitions here later, like:
    // Task<User> AddUserAsync(User user);
    // Task UpdateUserAsync(User user);
    // Task DeleteUserAsync(int id);

    /// <summary>
    /// Adds a new user to the data store.
    /// </summary>
    /// <param name="user">The user entity to add.</param>
    /// <returns>The newly created user entity.</returns>
    Task<User> AddUserAsync(User user);

    /// <summary>
    /// Updates an existing user in the data store.
    /// </summary>
    /// <param name="user">The user entity with updated information.</param>
    Task UpdateUserAsync(User user);

    /// <summary>
    /// Deletes a user from the data store.
    /// </summary>
    /// <param name="id">The ID of the user to delete.</param>
    Task DeleteUserAsync(int id);
}
