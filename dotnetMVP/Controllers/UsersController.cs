using dotnetMVP.Models.DTO.User;
using dotnetMVP.Models.Entities;
using dotnetMVP.Services.Interface;
using dotnetMVP.Types;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog.Context;
using System.Diagnostics;

[Route("api/user")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _usrService;
    private readonly ILogger<UsersController> _logger;

    public UsersController(IUserService usrServ, ILogger<UsersController> logger)
    {
        _usrService = usrServ;
        _logger = logger;
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserDto dto)
    {

        var result = await _usrService.CreateUserAsync(dto);

        return this.ResultToHttpCode(result);

    }

    //[Authorize(Policy = "FullAccess")] means that user should have claim role with value "admin" or "moder" 
    //Test
    [Authorize(Policy = "FullAccess")]
    [HttpGet("all")]
    public ActionResult GetAllUsersAsync()
    {
        return Ok();
    }



    //Test
    [Authorize(Roles = "Admin")]
    [HttpGet("testrole")]
    public ActionResult TestAdmin()
    {
        return Ok();
    }


    [Authorize]
    [HttpPut("update")]
    public ActionResult UpdateInfoAsync()
    {
        return Ok();
    }

    [HttpGet("all_test")]
    public async Task<IActionResult> ShowAllUsersTest()
    {
        return Ok(await _usrService.GetAllUsers());
        
    }

}
