using System.Threading.Tasks;
using ZTDomain.Model;

namespace ZtApplication
{
    public interface IUserAppService
    {
        public  Task<User> CheckUser(string userName, string password);

        Task<bool> Add(User user);
        Task<bool> RefreshToken(string id, string RefreshToken);
       Task<bool> Save(User user);
    }
}