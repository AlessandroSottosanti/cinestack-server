using Microsoft.AspNetCore.Mvc;
using Cinestack.API.Services;
using Cinestack.API.DTOs;

namespace Cinestack.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FilmsController : ControllerBase
{
    private readonly FilmService _filmService;

    public FilmsController(FilmService filmService)
    {
        _filmService = filmService;
    }

    [HttpGet]
    public async Task<ActionResult<List<FilmDto>>> GetFilms()
    {
        var films = await _filmService.GetFilmsAsync();

        return Ok(films);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<FilmDto>> GetFilmById(ushort id)
    {
        var film = await _filmService.GetFilmByIdAsync(id);

        if (film == null)
        {
            return NotFound();
        }

        return Ok(film);
    }
    
    [HttpPost]
    public async Task<ActionResult<FilmDto>> CreateFilm(CreateFilmDto dto)
    {
        var createdFilm = await _filmService.CreateFilmAsync(dto);

        return CreatedAtAction(
            nameof(GetFilmById),
            new { id = createdFilm.FilmId },
            createdFilm
        );
    }
    
    [HttpPut("{id}")]
    public async Task<ActionResult<FilmDto>> UpdateFilm(
        ushort id,
        UpdateFilmDto dto)
    {
        var updatedFilm = await _filmService.UpdateFilmAsync(id, dto);

        if (updatedFilm == null)
        {
            return NotFound();
        }

        return Ok(updatedFilm);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFilm(ushort id)
    {
        var deleted = await _filmService.DeleteFilmAsync(id);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
    
    [HttpPatch("{id}")]
    public async Task<ActionResult<FilmDto>> PatchFilm(
        ushort id,
        PatchFilmDto dto)
    {
        var updatedFilm = await _filmService.PatchFilmAsync(id, dto);

        if (updatedFilm == null)
        {
            return NotFound();
        }

        return Ok(updatedFilm);
    }
}