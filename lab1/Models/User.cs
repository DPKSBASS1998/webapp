namespace lab1.Models
{
    public class User
    {
        public int Id { get; set; }          // Первинний ключ
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
