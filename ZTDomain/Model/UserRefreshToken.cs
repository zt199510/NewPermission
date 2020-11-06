using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZTDomain.Models
{
    public class UserRefreshToken: Entity
    {

        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public string UserId { get; set; }

         public bool Active => DateTime.Now <= Expires;
    }
}
