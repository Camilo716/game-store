using AutoMapper;
using GameStore.Api.Dtos.CommentDtos;
using GameStore.Api.Dtos.GameDtos;
using GameStore.Api.Dtos.GenreDtos;
using GameStore.Api.Dtos.PlatformDtos;
using GameStore.Api.Dtos.PublisherDtos;
using GameStore.Core.Auth;
using GameStore.Core.Comment;
using GameStore.Core.Game;
using GameStore.Core.Genre;
using GameStore.Core.Platform;
using GameStore.Core.Publisher;
using Microsoft.AspNetCore.Authorization;
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
    IPublisherService publisherService,
    ICommentService commentService)
    : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<GameResponseDto>> Get()
    {
        var games = await gameService.GetAllAsync();
        var response = games.Select(g => new GameResponseDto(g));
        return Ok(response);
    }

    [HttpGet]
    [Route("find/{id}")]
    public async Task<ActionResult<GameResponseDto>> GetById([FromRoute] Guid id)
    {
        var game = await gameService.GetByIdAsync(id);
        return Ok(new GameResponseDto(game));
    }

    [HttpGet]
    [Route("{key}")]
    public async Task<ActionResult<GameResponseDto>> GetByKey([FromRoute] string key)
    {
        var game = await gameService.GetByKeyAsync(key);
        return Ok(new GameResponseDto(game));
    }

    [HttpGet]
    [Route("{key}/genres")]
    [Authorize(Policy = nameof(Permissions.ViewGenres))]
    public async Task<ActionResult<IEnumerable<GenreResponseDto>>> GetGenresByGameKey([FromRoute] string key)
    {
        var genres = await genreService.GetByGameKeyAsync(key);
        var response = genres.Select(g => new GenreResponseDto(g));
        return Ok(response);
    }

    [HttpGet]
    [Route("{key}/platforms")]
    [Authorize(Policy = nameof(Permissions.ViewPlatforms))]
    public async Task<ActionResult<IEnumerable<PlatformResponseDto>>> GetPlatformsByGameKey([FromRoute] string key)
    {
        var platforms = await platformService.GetByGameKeyAsync(key);
        var response = platforms.Select(p => new PlatformResponseDto(p));
        return Ok(response);
    }

    [HttpGet]
    [Route("{key}/comments")]
    public async Task<ActionResult<IEnumerable<Comment>>> GetCommentsByGameKey([FromRoute] string key)
    {
        var comments = await commentService.GetByGameKeyAsync(key);
        return Ok(comments);
    }

    [HttpPost]
    [Route("{key}/comments")]
    [Authorize(Policy = nameof(Policy.NotBanned))]
    public async Task<ActionResult<IEnumerable<Comment>>> AddGameComment(
        [FromRoute] string key,
        [FromBody] CommentRequest commentRequest)
    {
        Comment comment = new()
        {
            UserName = commentRequest.Comment.UserName,
            Body = commentRequest.Comment.Body,
            Type = commentRequest.Comment.Type,
            ParentCommentId = commentRequest.ParentId,
        };

        await commentService.CreateAsync(comment, key);

        return Ok();
    }

    [HttpDelete]
    [Route("comments/{id}")]
    [Authorize(Policy = nameof(Permissions.DeleteComment))]
    public async Task<ActionResult<IEnumerable<Comment>>> DeleteComment([FromRoute] Guid id)
    {
        await commentService.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet]
    [Route("{key}/publisher")]
    [Authorize(Policy = nameof(Permissions.ViewPublishers))]
    public async Task<ActionResult<Publisher>> GetPublisherByGameKey([FromRoute] string key)
    {
        var publisher = await publisherService.GetByGameKeyAsync(key);
        var response = new PublisherResponseDto(publisher);
        return Ok(response);
    }

    [HttpPost]
    [Authorize(Policy = nameof(Permissions.AddGame))]
    public async Task<ActionResult> Post([FromBody] GamePostRequest gamePostDto)
    {
        if (!gamePostDto.IsValid())
        {
            return BadRequest();
        }

        var game = mapper.Map<Game>(gamePostDto);

        await gameService.CreateAsync(game);

        return CreatedAtAction(
            actionName: nameof(GetById),
            routeValues: new { Id = game.Id },
            value: game);
    }

    [HttpPut]
    [Authorize(Policy = nameof(Permissions.UpdateGame))]
    public async Task<ActionResult> Put([FromBody] GamePutRequest gamePutRequest)
    {
        if (!gamePutRequest.IsValid())
        {
            return BadRequest();
        }

        var game = mapper.Map<Game>(gamePutRequest);

        await gameService.UpdateAsync(game);

        return Ok();
    }

    [HttpDelete]
    [Route("{id}")]
    [Authorize(Policy = nameof(Permissions.DeleteGame))]
    public async Task<ActionResult> Delete([FromRoute] Guid id)
    {
        await gameService.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet]
    [Route("{key}/file")]
    public async Task<ActionResult> DownloadGame([FromRoute] string key)
    {
        var gameFile = await gameFileService.GetByKeyAsync(key);

        return File(gameFile.Content, gameFile.ContentType, gameFile.FileName);
    }
}