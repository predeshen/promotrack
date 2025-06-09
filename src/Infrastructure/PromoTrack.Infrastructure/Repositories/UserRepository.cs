using Microsoft.EntityFrameworkCore;
using PromoTrack.Application.Interfaces;
using PromoTrack.Domain;
using PromoTrack.Infrastructure.Data;

namespace PromoTrack.Infrastructure.Repositories;

/// <summary>
/// Implements the IUserRepository interface using Entity Framework Core.
/// </summary>
public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Retrieves all users from the database.
    /// </summary>
    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return await _context.Users.ToListAsync();
    }

    /// <summary>
    /// Retrieves a single user by their unique identifier from the database.
    /// </summary>
    /// <param name="id">The ID of the user to retrieve.</param>
    /// <returns>The user if found; otherwise, null.</returns>
    public async Task<User?> GetUserByIdAsync(int id)
    {
        // FindAsync is highly efficient for querying an entity by its primary key.
        return await _context.Users.FindAsync(id);
    }

    /// <summary>
    /// Adds a new user to the database.
    /// </summary>
    public async Task<User> AddUserAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync(); // Persist the changes to the database
        return user;
    }

    /// <summary>
    /// Updates an existing user in the database.
    /// </summary>
    public async Task UpdateUserAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Deletes a user from the database by their ID.
    /// </summary>
    public async Task DeleteUserAsync(int id)
    {
        var userToDelete = await _context.Users.FindAsync(id);
        if (userToDelete != null)
        {
            _context.Users.Remove(userToDelete);
            await _context.SaveChangesAsync();
        }
    }
}