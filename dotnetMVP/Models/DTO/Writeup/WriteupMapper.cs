using dotnetMVP.Models.Entities;

namespace dotnetMVP.Models.DTO.Writeup
{
    public class WriteupMapper
    {
        public ShowWriteUpDto MapToDto(WriteUp entity)
        {
            return new ShowWriteUpDto()
            {
                Challenge = entity.Challenge.Name,
                Title = entity.Title,
                Text = entity.Text,
                Id = entity.Id,
            };
        }
        public WriteUp MapToEntity(CreateWriteUpDto dto)
        {
            return new WriteUp()
            {
                Id = Guid.NewGuid(),
                //AppUserId = dto.AppUserId,
                ChallengeId = dto.ChallengeId,
                Title = dto.Title,
                Text = dto.Text,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsHidden = true,
                IsHiddenByAdmin = false
            };
        }

        public WriteUp MapToEntity(CreateWriteUpDto dto, Guid ownerId)
        {
            return new WriteUp()
            {
                Id = Guid.NewGuid(),
                ChallengeId = dto.ChallengeId,
                AppUserId = ownerId,
                Title = dto.Title,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsHidden = false,
                IsHiddenByAdmin = false,
                Text = dto.Text
            };
        }
        public void MapToEntity(ShowWriteUpDto dto, WriteUp oldVersion)
        { 
            //apply updates
            oldVersion.Title = dto.Title;
            oldVersion.Text = dto.Text;
            oldVersion.UpdatedAt = DateTime.UtcNow;
        }
    }
}
