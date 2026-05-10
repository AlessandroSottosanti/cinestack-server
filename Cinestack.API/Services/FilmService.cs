using Microsoft.EntityFrameworkCore;
using Cinestack.API.Data;
using Cinestack.API.DTOs;
using Cinestack.API.Models;

namespace Cinestack.API.Services;

public class FilmService
{
    private readonly SakilaContext _context;

    public FilmService(SakilaContext context)
    {
        _context = context;
    }

    public async Task<List<FilmDto>> GetFilmsAsync()
    {
        return await _context.Films
            .Select(f => new FilmDto
            {
                FilmId = f.FilmId,
                Title = f.Title,
                Description = f.Description,
                Rating = f.Rating
            })
            .ToListAsync();
    }
    
    public async Task<FilmDto?> GetFilmByIdAsync(ushort id)
    {
        return await _context.Films
            .Where(f => f.FilmId == id)
            .Select(f => new FilmDto
            {
                FilmId = f.FilmId,
                Title = f.Title,
                Description = f.Description,
                Rating = f.Rating
            })
            .FirstOrDefaultAsync();
    }

    public async Task<FilmDto> CreateFilmAsync(CreateFilmDto dto)
    {
        var film = new Film
        {
            Title = dto.Title,
            Description = dto.Description,
            LanguageId = dto.LanguageId,
            Rating = dto.Rating,
            LastUpdate = DateTime.UtcNow
        };

        _context.Films.Add(film);

        await _context.SaveChangesAsync();

        return new FilmDto
        {
            FilmId = film.FilmId,
            Title = film.Title,
            Description = film.Description,
            Rating = film.Rating
        };
    }
    
    public async Task<FilmDto?> UpdateFilmAsync(ushort id, UpdateFilmDto dto)
    {
        var film = await _context.Films
            .FirstOrDefaultAsync(f => f.FilmId == id);

        if (film == null)
        {
            return null;
        }

        film.Title = dto.Title;
        film.Description = dto.Description;
        film.LanguageId = dto.LanguageId;
        film.Rating = dto.Rating;
        film.LastUpdate = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return new FilmDto
        {
            FilmId = film.FilmId,
            Title = film.Title,
            Description = film.Description,
            Rating = film.Rating
        };
    }
    
    
    public async Task<bool> DeleteFilmAsync(ushort id)
    {
        var film = await _context.Films
            .FirstOrDefaultAsync(f => f.FilmId == id);

        if (film == null)
        {
            return false;
        }

        _context.Films.Remove(film);

        await _context.SaveChangesAsync();

        return true;
    }
    
    public async Task<FilmDto?> PatchFilmAsync(
        ushort id,
        PatchFilmDto dto)
    {
        var film = await _context.Films
            .FirstOrDefaultAsync(f => f.FilmId == id);

        if (film == null)
        {
            return null;
        }

        if (dto.Title != null)
        {
            film.Title = dto.Title;
        }

        if (dto.Description != null)
        {
            film.Description = dto.Description;
        }

        if (dto.LanguageId.HasValue)
        {
            film.LanguageId = dto.LanguageId.Value;
        }

        if (dto.Rating != null)
        {
            film.Rating = dto.Rating;
        }

        film.LastUpdate = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return new FilmDto
        {
            FilmId = film.FilmId,
            Title = film.Title,
            Description = film.Description,
            Rating = film.Rating
        };
    }
}