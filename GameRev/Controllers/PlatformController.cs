using FluentValidation;
using GameRev.DTOs.Requests;
using GameRev.DTOs.Requests.Update;
using GameRev.Models.Entities;
using GameRev.Services.Interfaces;
using GameRev.Validators;
using Microsoft.AspNetCore.Mvc;

namespace GameRev.Controllers;

[ApiController]
[Route("api/platform")]
public class PlatformController : ControllerBase
{

    private readonly IPlatformService platformService;
    private readonly IValidator<PlatformRequest> platformValidator;

    public PlatformController(IPlatformService platformService, IValidator<PlatformRequest> platformValidator)
    {
        this.platformService = platformService;
        this.platformValidator = platformValidator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll (CancellationToken ct)
    {
        var platforms = await platformService.GetAllAsync(ct);
        platforms = platforms.OrderBy(p => p.Name).ToList();
        if(platforms.Count == 0)
        {
            return Ok("No platforms found");
        }
        return Ok(platforms);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById ([FromRoute] long id, CancellationToken ct)
    {
        var platform = await platformService.GetByIdAsync(id,ct);
        if(platform is null)
        {
            return NotFound();
        }
        return Ok(platform);
    }

    [HttpPost]
    public async Task<IActionResult> AddNew ([FromBody] PlatformRequest request, CancellationToken ct)
    {
        var validationResult = await platformValidator.ValidateAsync(request, ct);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }
        var platform = await platformService.AddAsync(request,ct);
        if(platform is null)
        {
            return BadRequest();
        }
        return Ok(platform);
    }

    [HttpPut]
    public async Task<IActionResult> Update ([FromBody] UpdatePlatformRequest request, CancellationToken ct)
    {
        var success = await platformService.UpdatePlatformAsync(request,ct);
        if (!success)
        {
            return BadRequest();
        }
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete ([FromQuery] long id, CancellationToken ct)
    {
        var success = await platformService.DeleteAsync(id,ct);
        if (!success)
        {
            return NotFound();
        }
        return NoContent();
    }
}