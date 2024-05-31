using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AgriEnergyConnectPlatform.ViewModels
{
    /// <summary>
    /// ViewModel for the Login view.
    /// </summary>
    public class LoginViewModel
    {
//-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        [Required]
        [EmailAddress]
        public string Email { get; set; }

//-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
//-----------------------------------------------------END-OF-FILE-----------------------------------------------------//