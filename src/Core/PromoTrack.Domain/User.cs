using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace PromoTrack.Domain
{
    public class User
    {
        /// <summary>
        /// The unique identifier for the user.
        /// Primary Key in the database.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// The user's first name.
        /// </summary>
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// The user's last name.
        /// </summary>
        public string LastName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

        /// <summary>
        /// The securely hashed password for the user.
        /// This should never store a plain-text password.
        /// </summary>
        public string PasswordHash { get; set; } = string.Empty;

        /// <summary>
        /// The role assigned to the user, which determines their permissions.
        /// e.g., "Promoter", "Admin", "Manager"
        /// </summary>
        public string Role { get; set; } = string.Empty;

        /// <summary>
        /// A flag to indicate if the user account is active and can be used.
        /// </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// The user's contact phone number.
        /// </summary>
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// The date and time when the user account was created.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// The date and time of the user's last login.
        /// </summary>
        public DateTime? LastLoginDate { get; set; }
    }
}
