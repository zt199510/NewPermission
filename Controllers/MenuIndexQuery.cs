using CardPlatform.Models;
using System.Runtime.InteropServices.ComTypes;

namespace CardPlatform.Controllers
{
    public class MenuIndexQuery
    {
        public string QName { get; set; }
        public string QId { get; set; }
        public string QParentId { get; set; }
        public int QMenuType { get; set; }
    }
}