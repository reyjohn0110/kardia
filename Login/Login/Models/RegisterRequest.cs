using System;
using System.Collections.Generic;
using System.Text;

namespace Login.Models
{
    public class RegisterRequest
    {
        public string First_name { get; set; }
        public string Middle_name { get; set; }
        public string Last_name { get; set; }
        public string Password { get; set; }
        public string Verification_id { get; set; }
        public string Gender { get; set; }
        public string Suffix { get; set; }
    }
}
