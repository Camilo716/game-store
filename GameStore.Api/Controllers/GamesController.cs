using AutoMapper;
using GameStore.Api.Dtos.GameDtos;
using GameStore.Api.Dtos.GenreDtos;
using GameStore.Api.Dtos.PlatformDtos;
using GameStore.Api.Dtos.PublisherDtos;
using GameStore.Core.Interfaces;
using GameStore.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GamesController(
    IMapper mapper,
    IGameService gameService,
    IGameFileService gameFileService,
    IGenreService genreService,
    IPlatformService platformService,
    IPublisherService publisherService)
    : ControllerBase
{
    private IMapper Mapper => mapper;

    private IGameService GameService => gameService;

    private IGameFileService GameFileService => gameFileService;

    private IGenreService GenreService => genreService;

    private IPlatformService PlatformService => platformService;

    private IPublisherService PublisherService => publisherService;

    [HttpGet]
    public async Task<ActionResult<GameResponseDto>> Get()
    {
        var games = await GameService.GetAllAsync();
        var response = games.Select(g => new GameResponseDto(g));
        return Ok(response);
    }

    [HttpGet]
    [Route("find/{id}")]
    public async Task<ActionResult<GameResponseDto>> GetById([FromRoute] Guid id)
    {
        var game = await GameService.GetByIdAsync(id);
        return Ok(new GameResponseDto(game));
    }

    [HttpGet]
    [Route("{key}")]
    public async Task<ActionResult<GameResponseDto>> GetByKey([FromRoute] string key)
    {
        var game = await GameService.GetByKeyAsync(key);
        return Ok(new GameResponseDto(game));
    }

    [HttpGet]
    [Route("{key}/genres")]
    public async Task<ActionResult<IEnumerable<GenreResponseDto>>> GetGenresByGameKey([FromRoute] string key)
    {
        var genres = await GenreService.GetByGameKeyAsync(key);
        var response = genres.Select(g => new GenreResponseDto(g));
        return Ok(response);
    }

    [HttpGet]
    [Route("{key}/platforms")]
    public async Task<ActionResult<IEnumerable<PlatformResponseDto>>> GetPlatformsByGameKey([FromRoute] string key)
    {
        var platforms = await PlatformService.GetByGameKeyAsync(key);
        var response = platforms.Select(p => new PlatformResponseDto(p));
        return Ok(response);
    }

    [HttpGet]
    [Route("{key}/publisher")]
    public async Task<ActionResult<Publisher>> GetPublisherByGameKey([FromRoute] string key)
    {
        var publisher = await PublisherService.GetByGameKeyAsync(key);
        var response = new PublisherResponseDto(publisher);
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] GamePostRequest gamePostDto)
    {
        if (!gamePostDto.IsValid())
        {
            return BadRequest();
        }

        var game = Mapper.Map<Game>(gamePostDto);

        await GameService.CreateAsync(game);

        return CreatedAtAction(
            actionName: nameof(GetById),
            routeValues: new { Id = game.Id },
            value: game);
    }

    [HttpPut]
    public async Task<ActionResult> Put([FromBody] GamePutRequest gamePutRequest)
    {
        if (!gamePutRequest.IsValid())
        {
            return BadRequest();
        }

        var game = Mapper.Map<Game>(gamePutRequest);

        await GameService.UpdateAsync(game);

        return Ok();
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult> Delete([FromRoute] Guid id)
    {
        await GameService.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet]
    [Route("{key}/file")]
    public async Task<ActionResult> DownloadGame([FromRoute] string key)
    {
        var gameFile = await GameFileService.GetByKeyAsync(key);

        return File(gameFile.Content, gameFile.ContentType, gameFile.FileName);
    }
}