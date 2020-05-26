namespace Sep3Tier3WithAuth.Models
{
    public class UpdateModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string SexPref { get; set; }
        public string PicRef { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
