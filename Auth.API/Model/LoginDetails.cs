using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.API.Model
{
    public class LoginDetails
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string LoginIndex { get; set; }
    }
}
