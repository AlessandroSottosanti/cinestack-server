namespace Cinestack.API.DTOs;

public class PatchFilmDto
{
    public string? Title { get; set; }

    public string? Description { get; set; }

    public byte? LanguageId { get; set; }

    public string? Rating { get; set; }
}