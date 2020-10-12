using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardPlatform.Models
{
    public class UserRefreshToken
    {
        public string Id { get; private set; } = Guid.NewGuid().ToString();
        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public string UserId { get; set; }
        public bool Active => DateTime.Now <= Expires;
    }
}
