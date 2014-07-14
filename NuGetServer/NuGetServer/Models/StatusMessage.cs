using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NuGetServer.Models
{
    public class StatusMessage
    {
        public bool Success
        {
            get;
            set;
        }
        public string Message
        {
            get;
            set;
        }
    }
}