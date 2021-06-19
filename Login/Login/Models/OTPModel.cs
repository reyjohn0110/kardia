using System.Collections.Generic;

namespace Login.Models
{
    //OTPModel & Sms classes are using for sending the payload of that HTTP post request.
    class OTPModel
    {
        public string sender { get; set; }

        public string route { get; set; }

        public string  country { get; set; }

        public List<Sms> sms { get; set; } = new List<Sms> { };

    }

    public class Sms
    {

        public string message { get; set; }

        public List<string> to { get; set; }
    }

    public class OTPResponse
    {
        // OTPResponse class is using for getting the response form that HTTP call.
        public string message { get; set; }

        public string type { get; set; }
    }
}
