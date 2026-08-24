using dotnetMVP.Models.DTO.PlatformDto;
using dotnetMVP.Types;
namespace dotnetMVP.Services.Interface
{
    public interface IPlatformService
    {
        Task<ShowPlatformDto> CreateAsync(CreatePllatformDto dto);
        Task<IEnumerable<ShowPlatformDto>> GetAllAsync();
        Task<ServiceResult<ShowPlatformDto>> GetByIdAsync(Guid id);
        Task<ServiceResult<ShowPlatformDto>> UpdateAsync(ShowPlatformDto dto);
        Task<ServiceResult<Guid>> DeleteByIdAsync(Guid id);
    }
}
