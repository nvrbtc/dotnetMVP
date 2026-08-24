using dotnetMVP.Models;
using dotnetMVP.Models.DTO.User;
using dotnetMVP.Models.Entities;
using dotnetMVP.Services.Interface;
using dotnetMVP.Types;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Context;
using System.Diagnostics;

namespace dotnetMVP.Services.Realization
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly UserMapper _mapper;
        private readonly ILogger<UserService> _logger;
        public UserService( UserManager<AppUser> signInManager, 
                            ApplicationDBcontext context,
                            UserMapper mapper,
                            ILogger<UserService> logger,
                            IDiagnosticContext diagnotstic)
        {
            _userManager = signInManager;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ServiceResult<ShowCreatedUserDto>> CreateUserAsync(CreateUserDto dto)
        {
            var user = _mapper.MapToEntity(dto);
            var result = await _userManager.CreateAsync(user,dto.Password);
            if (result.Succeeded) return ServiceResult<ShowCreatedUserDto>.Ok(_mapper.MapToDto(user));

            using (LogContext.PushProperty("User creation", "Failed"))
            {
                foreach (var error in result.Errors)
                    _logger.LogInformation("Error while creating user [{error}]", error);
            }
            return ServiceResult<ShowCreatedUserDto>.Fail(  "Failed to create user", 
                                                            OperationResult.ObjectNotFound);  // not found temp
        }

        //Testing
        public async Task<IEnumerable<AppUser>> GetAllUsers()
        {
            return await _userManager.Users.ToListAsync();
        }
    }
}
