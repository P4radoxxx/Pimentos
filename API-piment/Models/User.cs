namespace API_piment.Models
{
    public record class User
    {
        public int Id { get;  }
        public string Name { get;} = string.Empty;
        public string Email { get; } = string.Empty;
        public string Password { get; } = string.Empty;
        public string Role { get;  } = string.Empty;

        public User(string name, string email, string password, string role)
        {
            this.Name = name;
            this.Email = email;
            this.Password = password;
            this.Role = role;
        }
    }

    public record class UserRegisterDTO 
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public record class UserLogDTO 
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public record class UserTokenDTO 
    {
        public int Id { get; }
        public string Role { get; set; } = string.Empty;
    }

    public record class UserDisplayDTO
    {
        public int Id { get; }
        public string Name { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }



    public record class UserAdminInfo
    {
        public int Id { get; }
        public string Name { get; } = string.Empty;
        public string Email { get; } = string.Empty;
        public string Password { get; } = string.Empty;
        public string Role { get; } = string.Empty;
    }
}
