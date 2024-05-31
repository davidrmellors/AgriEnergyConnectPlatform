using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgriEnergyConnectPlatform.ViewModels
{
    /// <summary>
    /// ViewModel for the Register view.
    /// </summary>
    public class RegisterViewModel
    {
//-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

//-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string Email { get; set; }

//-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public string Password { get; set; }

//-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets or sets the confirm password.
        /// </summary>
        /// <value>
        /// The confirm password.
        /// </value>
        public string ConfirmPassword { get; set; }
    }
}
//-----------------------------------------------------END-OF-FILE-----------------------------------------------------//