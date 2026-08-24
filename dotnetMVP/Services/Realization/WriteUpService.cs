using dotnetMVP.Models;
using dotnetMVP.Models.DTO.User;
using dotnetMVP.Models.DTO.Writeup;
using dotnetMVP.Services.Interface;
using dotnetMVP.Types;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace dotnetMVP.Services.Realization
{
    public class WriteUpService : IWriteUpService
    {
        private readonly ApplicationDBcontext _context;
        private readonly WriteupMapper _mapper;
        private readonly ILogger<WriteUpService> _logger;
        public WriteUpService(  ApplicationDBcontext context,
                                WriteupMapper mapper,
                                ILogger<WriteUpService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<ShowWriteUpDto>> FilterByChallengeNameAsync(string name)
        {
            var result = await _context.WriteUps.AsNoTracking().Where(x => x.Challenge.Name.Contains(name))
                                                                    .Select(x => new ShowWriteUpDto
                                                                    {
                                                                        Id = x.Id,
                                                                        Challenge = x.Challenge.Name,
                                                                        Title = x.Title,
                                                                        Text = x.Text
                                                                    }).ToListAsync();
            return result;
        }

        public Task<IEnumerable<ShowWriteUpDto>> FilterByPlatformIdAsync(Guid platformId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ShowWriteUpDto>> GetAllAsync()
        {
            var result =  await _context.WriteUps.AsNoTracking().Select(x => new ShowWriteUpDto
                                                                    {
                                                                        Id =  x.Id,
                                                                        Challenge = x.Challenge.Name,
                                                                        Title = x.Title,
                                                                        Text = x.Text
                                                                    }).ToListAsync();
            return result;
        }

        public async Task<ServiceResult<ShowWriteUpDto>> GetByIdAsync(Guid id)
        {
            var result = await _context.WriteUps.AsNoTracking().Where(x => x.Id == id)
                                                                .Select(x => new ShowWriteUpDto()
                                                                {
                                                                    Challenge = x.Challenge.Name,
                                                                    Title = x.Title,
                                                                    Id = x.Id,
                                                                    Text = x.Text
                                                                }).FirstOrDefaultAsync();

            return result == null ? ServiceResult<ShowWriteUpDto>.Fail("Writeup not found.", OperationResult.ObjectNotFound)
                                  : ServiceResult<ShowWriteUpDto>.Ok(result);
        }


        //rely on JWT to identify user, can be exploitable
        public async Task<ServiceResult<IEnumerable<ShowWriteUpDto>>> GetAllByUserIdAsync(Guid userId)
        {
            //TODO when users introduced
            var userExist = await _context.AppUser.AsNoTracking().AnyAsync(x=>x.Id == userId);
            if (!userExist) return ServiceResult<IEnumerable<ShowWriteUpDto>>.Fail("User not found.",OperationResult.ObjectNotFound);

            var result = await _context.WriteUps.AsNoTracking()
                                                .Where(x=>x.AppUserId == userId 
                                                            && !x.IsHidden 
                                                            && !x.IsHiddenByAdmin)
                                                .Select(x => new ShowWriteUpDto()
                                                {
                                                            Challenge = x.Challenge.Name,
                                                            Id = x.Id,
                                                            Text = x.Text,
                                                            Title = x.Title
                                                }).ToListAsync();

            return ServiceResult<IEnumerable<ShowWriteUpDto>>.Ok(result);
        }

        public async Task<ServiceResult<ShowWriteUpDto>> CreateAsync(CreateWriteUpDto dto,Guid userId) //dto missing AppUserId
        {
            var challengeExists = await _context.Challenges.AnyAsync(x => x.Id == dto.ChallengeId);
            var userExists = await _context.Users.AnyAsync(x => x.Id ==  userId);

            //check if challenge exists
            if (!userExists || !challengeExists) return ServiceResult<ShowWriteUpDto>.Fail("User or Challenge does not exist.", 
                                                                                        OperationResult.ObjectNotFound);

            var entity = _mapper.MapToEntity(dto,userId);
            
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();

            return ServiceResult<ShowWriteUpDto>.Ok(_mapper.MapToDto(entity));
        }

        //ToDo: check if user can edit
        public async Task<ServiceResult<ShowWriteUpDto>> EditAsync(ShowWriteUpDto dto,Guid userId)
        {
            //TODO: check if user can do this action
            var writeUp = await _context.WriteUps.Where(x => x.Id == dto.Id 
                                                    && x.AppUserId == userId).FirstOrDefaultAsync();

            if (writeUp == null) return ServiceResult<ShowWriteUpDto>.Fail("Its not your WriteUp."
                                                                           ,OperationResult.AccessDenied); //consider to add ownership check && if writeup exists to give diff., responses

            _mapper.MapToEntity(dto, writeUp);

            await _context.SaveChangesAsync();
            return ServiceResult<ShowWriteUpDto>.Ok(_mapper.MapToDto(writeUp));
        }

        public async Task<ServiceResult<Guid>> DeleteAsync(Guid writeupId, Guid userId)
        {
            var result = await _context.WriteUps.FindAsync(writeupId);
            if (result == null || result.AppUserId != userId)
            {
                _logger.LogWarning("Unathorized delete. UserId trying to delete writeup = {userId}, owner of writeup = {ownerId}, writeupid = {writeupId}", userId, result?.Id,writeupId);
                return ServiceResult<Guid>.Fail("Unathorized delete.", 
                                                OperationResult.AccessDenied);
            } 

            _context.WriteUps.Remove(result);

            await _context.SaveChangesAsync();
            return ServiceResult<Guid>.Ok(writeupId);
        }
    }
}
