using System.Threading.Tasks;
using ZTDomain.Model;

namespace ZtApplication
{
    public interface IUserAppService
    {
        public  Task<User> CheckUser(string userName, string password);

        Task<bool> Add(User user);
        Task<object> RefreshToken(string id, string RefreshToken, string refreshToken);
       Task<bool> Save(User user);
    }
}