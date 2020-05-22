using System.Collections.Generic;
using Sep3Tier3WithAuth.Entities;

namespace Sep3Tier3WithAuth.Services
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
        IEnumerable<Fisher> GetAll();
        User GetById(int id);
        User Create(User fisher, string password);
        Administrator CreateAdmin(Administrator admin, string password);
        void Update(Fisher user, string password = null);
        void Delete(int id);
    }
}
