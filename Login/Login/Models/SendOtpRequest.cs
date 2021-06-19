using System;
using System.Collections.Generic;
using System.Text;

namespace Login.Models
{
    public class SendOtpRequest
    {
        public string Emailorphone { get; set; }
        public int Type { get; set; }
    }
}
