using System;

namespace SignalRAPI.Dtos.UserDtos
{
    public class UserReadDto
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; } 
        public DateTime? UpdatedAt { get; set; }
    }
}
