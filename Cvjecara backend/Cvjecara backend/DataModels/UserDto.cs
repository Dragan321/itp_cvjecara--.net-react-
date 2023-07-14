namespace Cvjecara_backend.DataModels
{
    public class UserDto
    {
        public string Email { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Role { get; set; } = null!;

        public string? Title { get; set; }

        public string password { get; set; } = null!;

        public DateOnly? DateOfBirth { get; set; }
    }
}
