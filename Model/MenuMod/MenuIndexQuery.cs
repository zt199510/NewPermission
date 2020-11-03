using CardPlatform.Models;
using System;
using System.Runtime.InteropServices.ComTypes;

namespace CardPlatform.Controllers
{
    public class MenuIndexQuery
    {
        public string QName { get; set; }
        public string QId { get; set; } 
        public Guid? QParentId { get; set; } = Guid.Empty;
        public int QMenuType { get; set; }
    }
}