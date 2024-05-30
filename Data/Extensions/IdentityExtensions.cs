using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace Data.Extensions
{
    public static class IdentityExtensions
    {
        public static string GetUserFirstName(this IIdentity identity)
        {
            var userId = identity.GetUserId();
            using (var context = new AgriConnectContext())
            {
                var user = context.Users.SingleOrDefault(u => u.Id == userId);
                return user?.Name; // Assuming Name stores the first name
            }
        }

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
