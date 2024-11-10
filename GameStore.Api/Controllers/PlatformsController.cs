using GameStore.Api.Dtos.GameDtos;
using GameStore.Api.Dtos.PlatformDtos;
using GameStore.Core.Interfaces;
using GameStore.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Api.Controllers;

[Controller]
[Route("/api/[Controller]")]
public class PlatformsController(
    IPlatformService platformService,
    IGameService gameService)
    : ControllerBase
{
    private IPlatformService PlatformService => platformService;

    private IGameService GameService => gameService;

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<PlatformResponseDto>> GetById([FromRoute] Guid id)
    {
        var platform = await PlatformService.GetByIdAsync(id);

        return platform is null ? NotFound() : Ok(new PlatformResponseDto(platform));
    }

    [HttpGet]
    [Route("{id}/games")]
    public async Task<ActionResult<IEnumerable<GameResponseDto>>> GetGamesByPlatformId([FromRoute] Guid id)
    {
        var games = await GameService.GetByPlatformIdAsync(id);
        var response = games.Select(g => new GameResponseDto(g));
        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PlatformResponseDto>>> GetAll()
    {
        var platforms = await PlatformService.GetAllAsync();
        var response = platforms.Select(p => new PlatformResponseDto(p));
        return Ok(response);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult> Delete([FromRoute] Guid id)
    {
        await PlatformService.DeleteAsync(id);

        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] PlatformPostRequest platformCreationDto)
    {
        if (!platformCreationDto.IsValid())
        {
            return BadRequest();
        }

        var platform = new Platform()
        {
            Type = platformCreationDto.Platform.Type,
        };

        await PlatformService.CreateAsync(platform);

        var createdPlatform = await PlatformService.GetByIdAsync(platform.Id);
        return CreatedAtAction(nameof(GetById), new { id = createdPlatform.Id }, createdPlatform);
    }

    [HttpPut]
    public async Task<ActionResult> Put([FromBody] PlatformPutRequest platformUpdateDto)
    {
        if (!platformUpdateDto.IsValid())
        {
            return BadRequest();
        }

        var platform = new Platform()
        {
            Id = platformUpdateDto.Platform.Id,
            Type = platformUpdateDto.Platform.Type,
        };

        await PlatformService.UpdateAsync(platform);

        return Ok();
    }
}