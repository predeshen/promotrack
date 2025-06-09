using Microsoft.AspNetCore.Mvc;
using PromoTrack.Application.Dtos;
using PromoTrack.Application.Interfaces;
using PromoTrack.Domain;

namespace PromoTrack.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UsersController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    /// <summary>
    /// Gets a list of all users in the system.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
        var users = await _userRepository.GetAllUsersAsync();
        return Ok(users);
    }

    /// <summary>
    /// Gets a specific user by their unique identifier.
    /// </summary>
    /// <param name="id">The ID of the user to retrieve.</param>
    /// <returns>The user if found; otherwise, a 404 Not Found response.</returns>
    [HttpGet("{id}")] // The route will be "/api/users/{id}" e.g., /api/users/1
    public async Task<ActionResult<User>> GetUserById(int id)
    {
        var user = await _userRepository.GetUserByIdAsync(id);

        if (user == null)
        {
            // Returns a 404 Not Found status code if no user with the given ID exists.
            return NotFound();
        }

        // Returns a 200 OK status code along with the user object.
        return Ok(user);
    }

    /// <summary>
    /// Creates a new user.
    /// </summary>
    /// <param name="createUserDto">The data for the new user.</param>
    /// <returns>The newly created user.</returns>
    [HttpPost]
    public async Task<ActionResult<User>> CreateUser(CreateUserDto createUserDto)
    {
        // Here we would perform validation on the DTO...

        // Map the DTO to our domain User entity
        var user = new User
        {
            FirstName = createUserDto.FirstName,
            LastName = createUserDto.LastName,
            Email = createUserDto.Email,
            Role = createUserDto.Role,
            IsActive = true,
            CreatedDate = DateTime.UtcNow
        };

        // --- IMPORTANT: Password Hashing ---
        // We will use a proper password hashing library here.
        // For now, we'll use a simple placeholder. We'll replace this with real hashing.
        // NEVER store plain text passwords.
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(createUserDto.Password);


        var newUser = await _userRepository.AddUserAsync(user);

        // Return a 201 Created response, which is the standard for a successful POST.
        // We also include a "Location" header pointing to the new resource's URL.
        return CreatedAtAction(nameof(GetUserById), new { id = newUser.UserId }, newUser);
    }

    /// <summary>
    /// Updates an existing user.
    /// </summary>
    /// <param name="id">The ID of the user to update.</param>
    /// <param name="updateUserDto">The updated user data.</param>
    /// <returns>A 204 No Content response if successful.</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, UpdateUserDto updateUserDto)
    {
        var userToUpdate = await _userRepository.GetUserByIdAsync(id);

        if (userToUpdate == null)
        {
            return NotFound();
        }

        // Map properties from the DTO to the entity we fetched from the database
        userToUpdate.FirstName = updateUserDto.FirstName;
        userToUpdate.LastName = updateUserDto.LastName;
        userToUpdate.Email = updateUserDto.Email;
        userToUpdate.Role = updateUserDto.Role;
        userToUpdate.IsActive = updateUserDto.IsActive;

        await _userRepository.UpdateUserAsync(userToUpdate);

        // A 204 No Content response is the standard for a successful PUT request.
        return NoContent();
    }

    /// <summary>
    /// Deletes a user by their ID.
    /// </summary>
    /// <param name="id">The ID of the user to delete.</param>
    /// <returns>A 204 No Content response if successful.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var user = await _userRepository.GetUserByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        await _userRepository.DeleteUserAsync(id);

        // A 204 No Content response is the standard for a successful DELETE.
        return NoContent();
    }
}