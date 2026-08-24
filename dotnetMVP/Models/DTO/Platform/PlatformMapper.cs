using dotnetMVP.Models.Entities;

namespace dotnetMVP.Models.DTO.PlatformDto
{
    public class PlatformMapper
    {
        public Platform MapToEntity(CreatePllatformDto dto)
        {
            return new()
            {
                Id = Guid.NewGuid(),
                Description = dto.Description,
                Name = dto.Name
            };
        }
        public ShowPlatformDto MapFromEntity(Platform entity)
        {
            return new ShowPlatformDto()
            {
                id = entity.Id,
                Description = entity.Description,
                Name = entity.Name
            };
        }

        public ShowPlatformDto MapToShow(Platform entity)
        {
            return new ShowPlatformDto()
            {
                id = entity.Id,
                Description = entity.Description,
                Name = entity.Name
            };
        }
        public Platform UpdateEntity(Platform entity, ShowPlatformDto dto)
        {
            entity.Description = dto.Description;
            entity.Name = dto.Name;
            return entity;
        }
    }
}
