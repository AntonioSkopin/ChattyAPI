using System.Threading.Tasks;
using ChattyAPI.Authorization.DTO;
using ChattyAPI.Authorization.Models;

namespace ChattyAPI.Authorization.Contracts
{
    public interface IAuthManager
    {
        public Task<LoginResultDTO> Login(LoginInfo loginInfo);
    }
}