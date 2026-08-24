using dotnetMVP.Models.DTO.ChallengeDto;
using dotnetMVP.Types;

namespace dotnetMVP.Services.Interface
{
    public interface IChallengeService
    {
        Task<IEnumerable<ShowChallengeInfoDto>> GetAllAsync();
        Task<ServiceResult<ShowChallengeInfoDto>> GetByIdAsync(Guid id);
        Task<IEnumerable<ShowChallengeInfoDto>> FilterByPlatformIdAsync(Guid platformId);
        Task<ShowChallengeInfoDto> CreateAsync(CreateChallengeDto dto);
        Task<ServiceResult<ShowChallengeInfoDto>> UpdateAsync(ShowChallengeInfoDto dto);
    }
}
