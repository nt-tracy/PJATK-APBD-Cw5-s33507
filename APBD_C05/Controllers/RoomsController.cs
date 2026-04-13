using APBD_C05.DTOs;
using APBD_C05.Models;
using Microsoft.AspNetCore.Mvc;

namespace APBD_C05.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomsController : ControllerBase
{
    private static readonly List<Room> _rooms =
    [
        new()
        {
            Id = 100,
            Name = "C100",
            BuildingCode = "C",
            Floor = 1,
            Capacity = 28.5,
            HasProjector = true,
            IsActive = true
        }, new()
        {
            Id = 101,
            Name = "C101",
            BuildingCode = "C",
            Floor = 1,
            Capacity = 32,
            HasProjector = false,
            IsActive = true
        }, new()
        {
            Id = 102,
            Name = "A300",
            BuildingCode = "A",
            Floor = 3,
            Capacity = 20.5,
            HasProjector = false,
            IsActive = false
        }, new()
        {
            Id = 103,
            Name = "A203",
            BuildingCode = "A",
            Floor = 2,
            Capacity = 30,
            HasProjector = true,
            IsActive = true
        }, new()
        {
            Id = 104,
            Name = "B404",
            BuildingCode = "B",
            Floor = 4,
            Capacity = 17,
            HasProjector = false,
            IsActive = true
        }
    ];
    
    // [HttpGet] 
    // public IActionResult GetAll()
    // {
    //     return Ok(_rooms.Select(e => new RoomDto()
    //     {
    //         Id = e.Id,
    //         Name = e.Name,
    //         BuildingCode = e.BuildingCode,
    //         Floor = e.Floor,
    //         Capacity = e.Capacity,
    //         HasProjector = e.HasProjector,
    //         IsActive = e.IsActive
    //     }));
    // }
    
    public static List<Room> GetRoomsList() => _rooms;
    
    [HttpGet]
    public IActionResult GetRooms(
        [FromQuery] double? minCapacity, 
        [FromQuery] bool? hasProjector, 
        [FromQuery] bool? activeOnly)
    {
        if (minCapacity == null && hasProjector == null && activeOnly == null)
        {
            return Ok(_rooms.Select(e => new RoomDto()
            {
                Id = e.Id,
                Name = e.Name,
                BuildingCode = e.BuildingCode,
                Floor = e.Floor,
                Capacity = e.Capacity,
                HasProjector = e.HasProjector,
                IsActive = e.IsActive
            }));        }
        
        
        var filtered = _rooms.AsEnumerable();

        if (minCapacity.HasValue)
            filtered = filtered.Where(r => r.Capacity >= minCapacity.Value);

        if (hasProjector.HasValue)
            filtered = filtered.Where(r => r.HasProjector == hasProjector.Value);

        if (activeOnly.HasValue && activeOnly.Value)
            filtered = filtered.Where(r => r.IsActive);

        return Ok(filtered.Select(e => new RoomDto()
        {
            Id = e.Id,
            Name = e.Name,
            BuildingCode = e.BuildingCode,
            Floor = e.Floor,
            Capacity = e.Capacity,
            HasProjector = e.HasProjector,
            IsActive = e.IsActive
        }));
    }

    private static RoomDto MapToDto(Room e) => new()
    {
        Id = e.Id,
        Name = e.Name,
        BuildingCode = e.BuildingCode,
        Floor = e.Floor,
        Capacity = e.Capacity,
        HasProjector = e.HasProjector,
        IsActive = e.IsActive
    };
    
    
    [HttpGet]
    [Route("{id:int}")]
    public IActionResult GetById(int id)
    {
        var room = _rooms.FirstOrDefault(e => e.Id == id);
        if (room is null)
            return NotFound($"Pokój o id {id} nie istnieje");

        return Ok(_rooms.Select(e => new RoomDto()
        {
            Id = e.Id,
            Name = e.Name,
            BuildingCode = e.BuildingCode,
            Floor = e.Floor,
            Capacity = e.Capacity,
            HasProjector = e.HasProjector,
            IsActive = e.IsActive
        }).Where(e => e.Id == id));
    }

    [HttpGet("building/{buildingCode}")]
    public IActionResult GetRoomsFromBuildingCode(string buildingCode)
    {
        var code = _rooms.FirstOrDefault(e => e.BuildingCode == buildingCode);

        if (code is null)
            return NotFound($"Budynek z kodem {buildingCode} nie istnieje");
        return Ok(_rooms.Select(e => new RoomDto()
        {
            Id = e.Id,
            Name = e.Name,
            BuildingCode = e.BuildingCode,
            Floor = e.Floor,
            Capacity = e.Capacity,
            HasProjector = e.HasProjector,
            IsActive = e.IsActive
        }).Where(e => e.BuildingCode == buildingCode));
    }

    [HttpPost]
    public IActionResult AddRoom([FromBody]  RoomDto roomDto)
    {
        var room = new Room()
        {
            Id = roomDto.Id,
            Name = roomDto.Name,
            BuildingCode = roomDto.BuildingCode,
            Floor = roomDto.Floor,
            Capacity = roomDto.Capacity,
            HasProjector = roomDto.HasProjector,
            IsActive = roomDto.IsActive
        };
        
        _rooms.Add(room);

        return CreatedAtAction(nameof(GetById), new { id = room.Id }, room);

    }

    [HttpPut]
    [Route("{id:int}")]
    public IActionResult Update(int id, [FromBody] RoomDto roomDto)
    {
        var room = _rooms.FirstOrDefault(e => e.Id == id);

        if (room is null)
        {
            return NotFound($"Pokój o id {id} nie istnieje!");
        }
        
        room.Name = roomDto.Name;
        room.BuildingCode = roomDto.BuildingCode;
        room.Floor = roomDto.Floor;
        room.Capacity = roomDto.Capacity;
        room.HasProjector = roomDto.HasProjector;
        room.IsActive = roomDto.IsActive;
        
        return NoContent();
    }

    
    [HttpDelete]
    [Route("{id:int}")]
    public IActionResult Delete(int id)
    {
        var room = _rooms.FirstOrDefault(e => e.Id == id);

        if (room is null)
        {
            return NotFound($"Pokój o id {id} nie istnieje!");
        }

        _rooms.Remove(room);
        return NoContent();
    }




}