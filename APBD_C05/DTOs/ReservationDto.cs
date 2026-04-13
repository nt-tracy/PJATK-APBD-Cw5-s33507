using System.ComponentModel.DataAnnotations;
using APBD_C05.Enums;

namespace APBD_C05.DTOs;

public class ReservationDto
{
    [Required]
    public int Id { get; set; }

    [Required]
    public int RoomId { get; set; }

    [Required(ErrorMessage = "Organizer name is required")]
    [MinLength(3, ErrorMessage = "Organizer name must be at least 3 characters long")]
    public string OrganizerName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Topic is required")]
    public string Topic { get; set; } = string.Empty;

    [Required]
    public DateOnly Date { get; set; } 

    [Required]
    public TimeOnly StartTime { get; set; }

    [Required]
    public TimeOnly EndTime { get; set; }

    [Required]
    public ReservationStatus Status { get; set; }
}