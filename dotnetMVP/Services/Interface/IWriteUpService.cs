using dotnetMVP.Models;
using dotnetMVP.Models.DTO.Writeup;
using dotnetMVP.Types;

namespace dotnetMVP.Services.Interface
{
    public interface IWriteUpService
    {
        Task<IEnumerable<ShowWriteUpDto>> GetAllAsync();
        Task<IEnumerable<ShowWriteUpDto>> FilterByChallengeNameAsync(string name);
        Task<IEnumerable<ShowWriteUpDto>> FilterByPlatformIdAsync(Guid platformId);
        Task<ServiceResult<IEnumerable<ShowWriteUpDto>>> GetAllByUserIdAsync(Guid userId);
        Task<ServiceResult<ShowWriteUpDto>> GetByIdAsync(Guid writeupId);
        Task<ServiceResult<ShowWriteUpDto>> CreateAsync(CreateWriteUpDto dto,Guid id);
        Task<ServiceResult<ShowWriteUpDto>> EditAsync(ShowWriteUpDto dto,Guid userId);
        Task<ServiceResult<Guid>> DeleteAsync(Guid writeupId,Guid userId);

    }
}
