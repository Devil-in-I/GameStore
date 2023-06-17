using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models
{
    /// <summary>
    /// Model for use in Registration action of UsersController
    /// </summary
    public class UserRegistrationModel
    {
        public User User { get; set; }
        public string Password { get; set; }
    }

    /// <summary>
    /// Model for use in Login action of UsersController
    /// </summary
    public class UserLoginModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    /// <summary>
    /// Model for use in ChangePassword action of UsersController
    /// </summary>
    public class ChangePasswordModel
    {
        public string Email { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
