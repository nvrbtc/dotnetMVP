using dotnetMVP.Models;
using dotnetMVP.Models.DTO.ChallengeDto;
using dotnetMVP.Services.Interface;
using dotnetMVP.Types;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace dotnetMVP.Controllers
{
    [Route("api/challenges")]
    [ApiController]
    public class ChallengeController : ControllerBase
    {
        private readonly IChallengeService _challengeService;

        public ChallengeController(IChallengeService serv)
        {
            _challengeService = serv;
        }

        /*TODO list:
         * Pagination when front introduced (?)
         * Authorized access to whole endpoint, challenges should be modified\created\deleted only by moderators\admins ( Roles not introduced yet )
         * Full CRUD methods 
         */

        [HttpGet("all")]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _challengeService.GetAllAsync());

        }
        [HttpGet("id/{id:guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var result = await _challengeService.GetByIdAsync(id);
            return this.ResultToHttpCode(result);
        }
        [HttpGet("platform/{id:guid}")]
        public async Task<IActionResult> FilterByPlatformIdAsync(Guid id)
        {
            return Ok(await _challengeService.FilterByPlatformIdAsync(id));
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAsync([FromBody] CreateChallengeDto dto)
        {
            return Ok(await _challengeService.CreateAsync(dto));
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateAsync([FromBody] ShowChallengeInfoDto dto)
        {
            var result = await _challengeService.UpdateAsync(dto);

            return this.ResultToHttpCode(result);
        }
    }
}
