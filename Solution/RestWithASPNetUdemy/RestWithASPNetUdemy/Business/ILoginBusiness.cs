using RestWithASPNetUdemy.Data.VO;

namespace RestWithASPNetUdemy.Business
{
    public interface ILoginBusiness
    {

        TokenVO ValidateCredentials(UserVO user);

        TokenVO ValidateCredentials(TokenVO token);


        bool RevokeToken(string userName);

    }

}
