using System;
using System.Collections.Generic;
using System.Text;

namespace Login.Models
{
    public class MainResponse
    {
        public string Message { get; set; }
        //public ValidationErrors Errors { get; set; }
        public Dictionary<string, string[]> Errors { get; set; }
    }

    public class ValidationErrors
    {

    }
}
