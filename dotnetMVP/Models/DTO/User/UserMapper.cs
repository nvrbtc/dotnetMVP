using dotnetMVP.Models.Entities;

namespace dotnetMVP.Models.DTO.User
{
    public class UserMapper
    {
        public AppUser MapToEntity(CreateUserDto dto)
        {
            return new AppUser
            {
                Id = Guid.NewGuid(),
                UserName = dto.Username,
                //PasswordHash = dto.Password, // Simple obfuscation for demonstration purposes
                
            };
        }

        public ShowCreatedUserDto MapToDto(AppUser entity)
        {
            return new ShowCreatedUserDto
            {
                Id = entity.Id,
                Username = entity.UserName ?? "FAILED",
                Password = entity.PasswordHash ?? "FAILED"
            };
        }
        
    }
}
