using SQLite;

namespace Victuz.Models
{
    [Table("Users")]
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        [MaxLength(50)]
        public string? Name { get; set; }
        [MaxLength(15)]
        public string? PhoneNumber { get; set; }
        [MaxLength(50), Unique]
        public string EmailAddress { get; set; } = string.Empty;

        [MaxLength(50)]
        public string Password { get; set; } = string.Empty;

        // Login Methode
        public bool Login(string email, string password)
        {
            return EmailAddress == email && Password == password;
        }

        // Profiel bewerken
        public void EditProfile(string name, string phoneNumber)
        {
            Name = name;
            PhoneNumber = phoneNumber;
        }
    }
}