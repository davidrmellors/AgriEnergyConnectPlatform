using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace Data.Extensions
{
    /// <summary>
    /// Provides extension methods for the IIdentity interface.
    /// </summary>
    public static class IdentityExtensions
    {
        /// <summary>
        /// Gets the first name of the user associated with the specified identity.
        /// </summary>
        /// <param name="identity">The identity of the user.</param>
        /// <returns>The first name of the user.</returns>
        public static string GetUserFirstName(this IIdentity identity)
        {
            var userId = identity.GetUserId();
            using (var context = new AgriConnectContext())
            {
                var user = context.Users.SingleOrDefault(u => u.Id == userId);
                return user?.Name; // Assuming Name stores the first name
            }
        }

        /// <summary>
        /// Gets the email of the user associated with the specified identity.
        /// </summary>
        /// <param name="identity">The identity of the user.</param>
        /// <returns>The email of the user.</returns>
        public static string GetUserEmail(this IIdentity identity)
        {
            var userId = identity.GetUserId();
            using (var context = new AgriConnectContext())
            {
                var user = context.Users.SingleOrDefault(u => u.Id == userId);
                return user?.Email;
            }
        }
    }
}
//-----------------------------------------------------END-OF-FILE-----------------------------------------------------//