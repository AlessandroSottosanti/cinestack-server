using System.ComponentModel.DataAnnotations;

namespace Cinestack.API.DTOs;

public class UpdateFilmDto
{
    [Required]
    [MaxLength(255)]
    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    [Required]
    public byte LanguageId { get; set; }

    public string? Rating { get; set; }
}