using dotnetMVP.Models;
using dotnetMVP.Models.DTO.ChallengeDto;
using dotnetMVP.Services.Interface;
using dotnetMVP.Types;
using Microsoft.EntityFrameworkCore;

namespace dotnetMVP.Services.Realization
{
    public class ChallengeService : IChallengeService
    {
        private readonly ApplicationDBcontext _db;
        private readonly ChallengeMapper _mapper;
        private readonly ILogger<ChallengeService> _logger;
        public ChallengeService(ApplicationDBcontext dbcontext, 
                                ChallengeMapper mapper,
                                ILogger<ChallengeService> logger)
        {
            this._db = dbcontext;
            this._mapper = mapper;
            this._logger = logger;
        }


        //later track who created Challenge
        public async Task<ShowChallengeInfoDto> CreateAsync(CreateChallengeDto dto)
        {
            var challenge = _mapper.MapToEntity(dto);

            await _db.Challenges.AddAsync(challenge);
            await _db.SaveChangesAsync();

            _logger.LogInformation("Created challenge = {challengeId}", challenge.Id);

            return _mapper.MapFromEntity(challenge);
        }

        public async Task<IEnumerable<ShowChallengeInfoDto>> GetAllAsync()
        {
            var result = await _db.Challenges.AsNoTracking()
                                                    .Select(x => new ShowChallengeInfoDto()
                                                    {
                                                        Description = x.Description,
                                                        ChallengeId = x.Id,
                                                        ChallengeName = x.Name,
                                                        PlatformId = x.Platform.Id,
                                                        PlatformName = x.Platform.Name,
                                                        Title = x.Title
                                                    }).ToListAsync();

            return result;
        }

        public async Task<ServiceResult<ShowChallengeInfoDto>> GetByIdAsync(Guid id)
        {
            var result = await _db.Challenges.AsNoTracking()
                                                    .Where(x => x.Id == id )
                                                    .Select(x => new ShowChallengeInfoDto
                                                    {
                                                        ChallengeId = x.Id,
                                                        ChallengeName = x.Name,
                                                        Description = x.Description,
                                                        Title = x.Title,
                                                        PlatformId = x.Platform.Id,
                                                        PlatformName = x.Platform.Name
                                                    }).FirstOrDefaultAsync();

            return (result == null) ? ServiceResult<ShowChallengeInfoDto>.Fail("Challenge not found.", OperationResult.ObjectNotFound) :
                                    ServiceResult<ShowChallengeInfoDto>.Ok(result);
        }

        public async Task<IEnumerable<ShowChallengeInfoDto>> FilterByPlatformIdAsync(Guid platformId)
        {
            var result = await _db.Challenges.AsNoTracking()
                                                    .Where(x => x.Platform.Id == platformId)
                                                    .Select(x => new ShowChallengeInfoDto
                                                    {
                                                        ChallengeId = x.Id,
                                                        ChallengeName = x.Name,
                                                        Description = x.Description,
                                                        Title = x.Title,
                                                        PlatformId = x.Platform.Id,
                                                        PlatformName = x.Platform.Name
                                                    }).ToListAsync();

            return result;
        }

        public async Task<ServiceResult<ShowChallengeInfoDto>> UpdateAsync(ShowChallengeInfoDto dto)
        {
            var entity = await _db.Challenges.FindAsync(dto.ChallengeId);

            if (entity == null) return ServiceResult<ShowChallengeInfoDto>.Fail("ChallengeNotFound",
                                                                                OperationResult.ObjectNotFound);
            
            var updated = _mapper.MapToEntity(dto, entity);
            await _db.SaveChangesAsync();

            _logger.LogInformation("Challenge = {challengeId} updated by [later]", updated.Id);

            return ServiceResult<ShowChallengeInfoDto>.Ok(_mapper.MapFromEntity(updated));
        }
        
    }
}   
