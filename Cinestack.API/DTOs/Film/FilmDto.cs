namespace Cinestack.API.DTOs;

public class FilmDto
{
    public ushort FilmId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public string Rating { get; set; } = null!;
}