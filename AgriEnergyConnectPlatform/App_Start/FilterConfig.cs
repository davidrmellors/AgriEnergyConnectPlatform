using System.Web;
using System.Web.Mvc;

namespace AgriEnergyConnectPlatform
{
    /// <summary>
    /// Configures the global filters for the application.
    /// </summary>
    public class FilterConfig
    {
        /// <summary>
        /// Registers the global filters for the application.
        /// </summary>
        /// <param name="filters">The collection of global filters.</param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            // Adds a filter that handles errors by displaying an error view.
            filters.Add(new HandleErrorAttribute());
        }
    }
}
//-----------------------------------------------------END-OF-FILE-----------------------------------------------------//