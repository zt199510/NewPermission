using System.Threading.Tasks;
using ZTDomain.Model;

namespace ZtApplication
{
    public interface IUserAppService
    {
        public  Task<User> CheckUser(string userName, string password);



    }
}