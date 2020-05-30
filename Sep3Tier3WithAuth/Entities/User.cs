using System.ComponentModel.DataAnnotations;

namespace Sep3Tier3WithAuth.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Discriminator { get; private set; }
    }
}
