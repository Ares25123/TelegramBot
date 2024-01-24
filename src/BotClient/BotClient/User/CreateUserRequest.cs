namespace APIForBD.Contracts.User
{
    public class CreateUserRequest
    {
        public string Surname { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string UserPassword { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
