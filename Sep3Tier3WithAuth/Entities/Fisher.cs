using System.ComponentModel;

namespace Sep3Tier3WithAuth.Entities
{
    public class Fisher : User
    {
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string SexPref { get; set; }
        public string PicRef { get; set; }
        public int Age { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
