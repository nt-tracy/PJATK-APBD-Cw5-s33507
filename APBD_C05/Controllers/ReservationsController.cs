using APBD_C05.DTOs;
using APBD_C05.Enums;
using APBD_C05.Models;
using Microsoft.AspNetCore.Mvc;

namespace APBD_C05.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class ReservationsController : ControllerBase
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

    [HttpGet]
    public IActionResult GetReservations(
        [FromQuery] DateOnly? date, 
        [FromQuery] ReservationStatus? status, 
        [FromQuery] int? roomId)
    {
        var query = _reservations.AsEnumerable();

        if (date.HasValue)
            query = query.Where(r => r.Date == date.Value);
        
        if (status.HasValue)
            query = query.Where(r => r.Status == status.Value);
        
        if (roomId.HasValue)
            query = query.Where(r => r.RoomId == roomId.Value);

        return Ok(query);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        var res = _reservations.FirstOrDefault(r => r.Id == id);
        return res == null ? NotFound() : Ok(res);
    }

    [HttpPost]
    public IActionResult Create([FromBody] ReservationDto dto)
    {
        if (dto.EndTime <= dto.StartTime)
            return BadRequest("EndTime must be after StartTime.");

        var room = RoomsController.GetRoomsList().FirstOrDefault(r => r.Id == dto.RoomId);
        
        if (room == null)
            return NotFound($"Room with ID {dto.RoomId} does not exist.");
        
        if (!room.IsActive)
            return BadRequest("Cannot reserve an inactive room.");

        bool hasCollision = _reservations.Any(r => 
            r.RoomId == dto.RoomId && 
            r.Date == dto.Date &&
            r.Status != ReservationStatus.Cancelled && 
            dto.StartTime < r.EndTime && r.StartTime < dto.EndTime);

        if (hasCollision)
            return Conflict("The room is already reserved at this time.");

        var newRes = new Reservation
        {
            Id = dto.Id,
            RoomId = dto.RoomId,
            OrganizerName = dto.OrganizerName,
            Topic = dto.Topic,
            Date = dto.Date,
            StartTime = dto.StartTime,
            EndTime = dto.EndTime,
            Status = dto.Status
        };

        _reservations.Add(newRes);
        return CreatedAtAction(nameof(GetById), new { id = newRes.Id }, newRes);
    }

    [HttpPut("{id:int}")]
    public IActionResult Update(int id, [FromBody] ReservationDto dto)
    {
        var existing = _reservations.FirstOrDefault(r => r.Id == id);
        if (existing == null) return NotFound();

        if (dto.EndTime <= dto.StartTime)
            return BadRequest("EndTime must be after StartTime.");

        bool hasCollision = _reservations.Any(r => 
            r.Id != id &&
            r.RoomId == dto.RoomId && 
            r.Date == dto.Date &&
            r.Status != ReservationStatus.Cancelled && 
            dto.StartTime < r.EndTime && r.StartTime < dto.EndTime);

        if (hasCollision)
            return Conflict("The room is already reserved at this time.");

        existing.RoomId = dto.RoomId;
        existing.OrganizerName = dto.OrganizerName;
        existing.Topic = dto.Topic;
        existing.Date = dto.Date;
        existing.StartTime = dto.StartTime;
        existing.EndTime = dto.EndTime;
        existing.Status = dto.Status;

        return Ok(existing);
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var res = _reservations.FirstOrDefault(r => r.Id == id);
        if (res == null) return NotFound();

        _reservations.Remove(res);
        return NoContent();
    }
}