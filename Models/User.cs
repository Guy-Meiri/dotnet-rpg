namespace dotnet_rpg.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public byte[] passwordHash { get; set; }

        public byte[] PasswordSalt { get; set; }
    }
}