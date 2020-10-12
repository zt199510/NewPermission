using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardPlatform.Models
{
    public class Request
    {
        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }
    }
}
