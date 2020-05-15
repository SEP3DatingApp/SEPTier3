using System.Collections.Generic;
using Sep3Tier3WithAuth.Entities;

namespace Sep3Tier3WithAuth.Services
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
        IEnumerable<User> GetAll();
        User GetById(int id);
        User Create(User user, string password);
        void Update(Fisher user, string password = null);
        void Delete(int id);
    }
}
