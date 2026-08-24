using dotnetMVP.Models.Entities;

namespace dotnetMVP.Models.DTO.ChallengeDto
{
    public class ChallengeMapper
    {
        public Challenge MapToEntity(CreateChallengeDto dto)
        {
            return new Challenge
            {
                Id = Guid.NewGuid(),
                Name = dto.Title,
                Description = dto.Description,
                Title = dto.Title,
                PlatformId = dto.PlatformId
            };
        }

        public Challenge MapToEntity(ShowChallengeInfoDto dto, Challenge existingEntity)
        { 
            existingEntity.Name = dto.ChallengeName;
            existingEntity.Description = dto.Description;
            existingEntity.Title = dto.Title;

            return existingEntity;
        }
        public ShowChallengeInfoDto MapFromEntity(Challenge entity)
        {
            return new()
            {
                ChallengeId = entity.Id,
                ChallengeName = entity.Name,
                Description = entity.Description,
                PlatformId = entity.PlatformId,
                PlatformName = "SomePlatform",
                Title = entity.Title
            };
        }
    }
}

