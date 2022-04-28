using RestWithASPNetUdemy.Model;
using RestWithASPNetUdemy.Data.VO;

namespace RestWithASPNetUdemy.Repository
{
    public interface IUserRepository
    {

        User ValidateCredentials(UserVO user);

    }
}
