using System.ComponentModel;
using Sep3Tier3WithAuth.Entities;

namespace Sep3Tier3WithAuth.Entities
{
    public class Fisher : User
    {
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public char Gender { get; set; }
        public char SexPref { get; set; }
        public string PicRef { get; set; }
        public int Age { get; set; }
        public string Description { get; set; }
        [DefaultValue(true)]
        public bool IsActive { get; set; }
    }
}
