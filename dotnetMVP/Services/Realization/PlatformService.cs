using dotnetMVP.Models;
using dotnetMVP.Models.DTO.PlatformDto;
using dotnetMVP.Services.Interface;
using dotnetMVP.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dotnetMVP.Services.Realization
{
    public class PlatformService : IPlatformService
    {
        private readonly ApplicationDBcontext _dbcontext;
        private readonly PlatformMapper _mapper;
        private readonly ILogger<PlatformService> _logger;

        public PlatformService(ApplicationDBcontext db,
                                PlatformMapper platformMapper,
                                ILogger<PlatformService> logger)
        {
            _dbcontext = db;
            _mapper = platformMapper;
            _logger = logger;
        }
        public async Task<ShowPlatformDto> CreateAsync(CreatePllatformDto dto)
        {
            var entity = _mapper.MapToEntity(dto);

            await _dbcontext.Platforms.AddAsync(entity); // could be non async method, not really initiating connection with db here
            await _dbcontext.SaveChangesAsync();

            _logger.LogInformation("Created platform with id = {platformId}", entity.Id);

            return _mapper.MapFromEntity(entity);
        }

        public async Task<ServiceResult<Guid>> DeleteByIdAsync(Guid id)
        {
            var result = await _dbcontext.Platforms.FindAsync(id);

            if (result == null) return ServiceResult<Guid>.Fail("Platform not found.",
                                                                OperationResult.ObjectNotFound);

            _dbcontext.Platforms.Remove(result);
            await _dbcontext.SaveChangesAsync();

            _logger.LogInformation("Deleted platform with id = {platformId}", id);

            return ServiceResult<Guid>.Ok(id);
        }

        public async Task<IEnumerable<ShowPlatformDto>> GetAllAsync()
        {
            var result = await _dbcontext.Platforms.AsNoTracking().Select(x => new ShowPlatformDto
            {
                id = x.Id,
                Description = x.Description,
                Name = x.Name
            }).ToListAsync();

            return result;
        }

        public async Task<ServiceResult<ShowPlatformDto>> GetByIdAsync(Guid id)
        {
            var result = await _dbcontext.Platforms.AsNoTracking().Where(x => x.Id == id).FirstOrDefaultAsync();

            return (result == default) ? ServiceResult<ShowPlatformDto>.Fail("Platform not found.", OperationResult.ObjectNotFound) : // Should be something better and meaningfull 
                ServiceResult<ShowPlatformDto>.Ok(_mapper.MapFromEntity(result));

        }

        public async Task<ServiceResult<ShowPlatformDto>> UpdateAsync(ShowPlatformDto dto)
        {
            var result = await _dbcontext.Platforms.FindAsync(dto.id);

            if (result == null) return ServiceResult<ShowPlatformDto>.Fail("Platform not found",
                                                                            OperationResult.ObjectNotFound); 

            _mapper.UpdateEntity(result, dto);
            _dbcontext.Update(result);

            await _dbcontext.SaveChangesAsync();

            //_logger.LogInformation("User = {userId} updated platform = {platformId}", userId, dto.id);

            return ServiceResult<ShowPlatformDto>.Ok(dto);
        }
    }
}
