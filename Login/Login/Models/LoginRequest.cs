using System;
using System.Collections.Generic;
using System.Text;

namespace Login.Models
{
    public class LoginRequest
    {
        public string Emailorphone { get; set; }
        public string Password { get; set; }
    }
}
