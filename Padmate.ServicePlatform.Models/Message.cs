using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Padmate.ServicePlatform.Models
{
    public class Message
    {
        public bool Success { get; set; }

        public string Content { get; set; }

        public int ReturnId { get; set; }
    }
}