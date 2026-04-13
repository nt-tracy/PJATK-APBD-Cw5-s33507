using APBD_C05.Enums;

namespace APBD_C05.DTOs;

public class ReservationDto
{
    public int Id { get; set; }
    public int RoomId { get; set; }
    public string OrganizerName { get; set; } = string.Empty;
    public string Topic { get; set; } = string.Empty;
    public DateOnly Date;
    public TimeOnly StartTime;
    public TimeOnly EndTime;
    public ReservationStatus Status { get; set; }
}