using System.Collections.Generic;
using Sep3Tier3WithAuth.Entities;

namespace Sep3Tier3WithAuth.Services
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
        IEnumerable<Fisher> GetAllFishersAccordingToTheirPref(int userId,string gender, int sexPref);
        IEnumerable<Fisher> GetAll();
        Fisher GetById(int id);
        void LikePerson(int userId, LikeReject lr);
        void RejectPerson(int userId,LikeReject lr);
        User Create(User fisher, string password);
        Administrator CreateAdmin(Administrator admin, string password);
        void Update(Fisher user, string password = null);
        void Delete(int id);
        IEnumerable<LikePersonList> GetHistory(int userid);
    }
}
