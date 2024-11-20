using GameStore.Api.Dtos.GameDtos;
using GameStore.Api.Dtos.PublisherDtos;
using GameStore.Core.Enums;
using GameStore.Core.Interfaces;
using GameStore.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Api.Controllers;

[Controller]
[Route("/api/[Controller]")]
public class PublishersController(
    IPublisherService publisherService,
    IGameService gameService)
    : ControllerBase
{
    private IPublisherService PublisherService => publisherService;

    private IGameService GameService => gameService;

    [HttpGet]
    [Route("{id}")]
    [Authorize(Policy = nameof(Permissions.ViewPublishers))]
    public async Task<ActionResult<PublisherResponseDto>> GetById([FromRoute] Guid id)
    {
        var publisher = await PublisherService.GetByIdAsync(id);

        return publisher is null ? NotFound() : Ok(new PublisherResponseDto(publisher));
    }

    [HttpGet]
    [Route("{id}/games")]
    public async Task<ActionResult<IEnumerable<GameResponseDto>>> GetGamesByPublisherId([FromRoute] Guid id)
    {
        var games = await GameService.GetByPublisherIdAsync(id);
        var response = games.Select(g => new GameResponseDto(g));
        return Ok(response);
    }

    [HttpGet]
    [Authorize(Policy = nameof(Permissions.ViewPublishers))]
    public async Task<ActionResult<IEnumerable<PublisherResponseDto>>> GetAll()
    {
        var publishers = await PublisherService.GetAllAsync();
        var response = publishers.Select(p => new PublisherResponseDto(p));
        return Ok(response);
    }

    [HttpDelete]
    [Route("{id}")]
    [Authorize(Policy = nameof(Permissions.DeletePublisher))]
    public async Task<ActionResult> Delete([FromRoute] Guid id)
    {
        await PublisherService.DeleteAsync(id);

        return NoContent();
    }

    [HttpPost]
    [Authorize(Policy = nameof(Permissions.AddPublisher))]
    public async Task<ActionResult> Post([FromBody] PublisherPostRequest publisherCreationDto)
    {
        if (!publisherCreationDto.IsValid())
        {
            return BadRequest();
        }

        var publisher = new Publisher()
        {
            CompanyName = publisherCreationDto.Publisher.CompanyName,
            HomePage = publisherCreationDto.Publisher.HomePage,
            Description = publisherCreationDto.Publisher.Description,
        };

        await PublisherService.CreateAsync(publisher);

        var createdPublisher = await PublisherService.GetByIdAsync(publisher.Id);
        return CreatedAtAction(nameof(GetById), new { id = createdPublisher.Id }, createdPublisher);
    }

    [HttpPut]
    [Authorize(Policy = nameof(Permissions.UpdatePublisher))]
    public async Task<ActionResult> Put([FromBody] PublisherPutRequest publisherUpdateDto)
    {
        if (!publisherUpdateDto.IsValid())
        {
            return BadRequest();
        }

        var publisher = new Publisher()
        {
            Id = publisherUpdateDto.Publisher.Id,
            CompanyName = publisherUpdateDto.Publisher.CompanyName,
            HomePage = publisherUpdateDto.Publisher.HomePage,
            Description = publisherUpdateDto.Publisher.Description,
        };

        await PublisherService.UpdateAsync(publisher);

        return Ok();
    }
}