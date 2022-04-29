using RestWithASPNetUdemy.Data.VO;
using RestWithASPNetUdemy.Model;
using RestWithASPNetUdemy.Model.Context;
using System.Security.Cryptography;
using System.Text;

namespace RestWithASPNetUdemy.Repository
{
    public class UserRepository : IUserRepository
    {

        private SQLServerContext _context;

        public UserRepository(SQLServerContext sqlServerContext)
        {
            _context = sqlServerContext;
        }

        public User ValidateCredentials(UserVO user)
        {
            var pass = ComputHash(user.Password, new SHA256CryptoServiceProvider());
            return _context.Users.FirstOrDefault(u => u.UserName == user.UserName && u.Passwoed == pass);
        }

        public User ValidateCredentials(string userName)
        {
            return _context.Users.FirstOrDefault(u => u.UserName == userName);
        }

        public bool RevokeToken(string userName)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserName == userName);
            if (user == null)
                return false;
            user.RefreshToken = null;
            _context.SaveChanges();
            return true;
        }

        public User RefreshUserInfo(User user)
        {

            if (_context.Users.Any(u => u.Id.Equals(user.Id)))
                return null;

            var user_previous = _context.Users.SingleOrDefault(i => i.Id.Equals(user.Id));

            if (user_previous != null)
            {
                try
                {
                    _context.Entry(user_previous).CurrentValues.SetValues(user);
                    _context.SaveChanges();
                    return user_previous;
                }
                catch (Exception ex)
                {
                    throw new Exception("Update Error", ex);
                }
            }
            else
            {
                return user_previous;
            }

        }

        private string ComputHash(string input, SHA256CryptoServiceProvider algorithm)
        {
            byte[] inpuBytes = Encoding.UTF8.GetBytes(input);
            byte[] hashedBytes = algorithm.ComputeHash(inpuBytes);
            return BitConverter.ToString(hashedBytes);  
        }

    }

}
