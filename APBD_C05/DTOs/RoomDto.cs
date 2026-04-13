using System.ComponentModel.DataAnnotations;
namespace APBD_C05.DTOs;

public class RoomDto
{
    [Required]
    public int Id { get; set; }

    [Required(ErrorMessage = "Room name is required")]
    public string Name { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Building code is required")]
    public string BuildingCode { get; set; } = string.Empty;

    [Required]
    public int Floor { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "Capacity must be greater than zero")]
    public double Capacity { get; set; }

    public bool HasProjector { get; set; }

    public bool IsActive { get; set; }
}