using System;
using System.Collections.Generic;
using System.Text;

namespace Login.Models
{
    public class VerificationRequest
    {
        public string Code { get; set; }
        public int Type { get; set; }
        public string Verification_id { get; set; }
    }
}
