using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace elogbookapi.Models.API
{
    public class APIError
    {
        //E--for error, I--for info
        public string MessageType { get; set; }
        public string ErrorMessage { get; set; }
        public string DetailedMessage { get; set; }
    }
}