using APBD_C05.Enums;
using APBD_C05.Models;
using Microsoft.AspNetCore.Mvc;

namespace APBD_C05.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class ReservationsController
{
    private static readonly List<Reservation> _reservations =
    [
        new()
        {
            Id = 1,
            RoomId = 101,
            OrganizerName = "John Doe",
            Topic = "Quarterly Business Review",
            Date = new DateOnly(2024, 6, 10),
            StartTime = new TimeOnly(9, 0),
            EndTime = new TimeOnly(10, 30),
            Status = ReservationStatus.Confirmed
        },
        new()
        {
            Id = 2,
            RoomId = 205,
            OrganizerName = "Jane Smith",
            Topic = "Project Kick-off: Apollo",
            Date = new DateOnly(2024, 6, 10),
            StartTime = new TimeOnly(11, 0),
            EndTime = new TimeOnly(12, 0),
            Status = ReservationStatus.Planned
        },
        new()
        {
            Id = 3,
            RoomId = 101,
            OrganizerName = "Michael Brown",
            Topic = "Technical Interview - Backend Developer",
            Date = new DateOnly(2024, 6, 11),
            StartTime = new TimeOnly(14, 0),
            EndTime = new TimeOnly(15, 0),
            Status = ReservationStatus.Cancelled
        },
        new()
        {
            Id = 4,
            RoomId = 303,
            OrganizerName = "Emily White",
            Topic = "Weekly Team Sync",
            Date = new DateOnly(2024, 6, 12),
            StartTime = new TimeOnly(10, 0),
            EndTime = new TimeOnly(11, 0),
            Status = ReservationStatus.Confirmed
        }
    ];
}