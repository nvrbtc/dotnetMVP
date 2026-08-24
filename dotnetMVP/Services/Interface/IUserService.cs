using dotnetMVP.Models.DTO.User;
using dotnetMVP.Models.Entities;
using dotnetMVP.Types;

namespace dotnetMVP.Services.Interface
{
    public interface IUserService
    {
        Task<ServiceResult<ShowCreatedUserDto>> CreateUserAsync(CreateUserDto dto);
        Task<IEnumerable<AppUser>> GetAllUsers();
    }
}
