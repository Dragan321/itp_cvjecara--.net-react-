namespace Cvjecara_backend.DataModels
{
    public class UserDtoResponse
    {
        public int id { get; set; } 
        public string Email { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Role { get; set; } = null!;

        public string? Title { get; set; }

        public string token { get; set; } = null!;

        public DateOnly? DateOfBirth { get; set; }
    }
}
