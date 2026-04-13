using APBD_C05.DTOs;
using APBD_C05.Models;
using Microsoft.AspNetCore.Mvc;

namespace APBD_C05.Controllers;

[ApiController]
[Route("[controller]")]
public class RoomsController : ControllerBase
{
    public static List<Room> rooms =
    [
        new Room()
        {
            Id = 100,
            Name = "C100",
            BuildingCode = "C",
            Floor = 1,
            Capacity = 28.5,
            HasProjector = true,
            IsActive = true
        }
    ];

    [HttpGet] 
    public IActionResult GetAll()
    {
        return Ok(rooms.Select(e => new RoomDto()
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

    [HttpGet]
    [Route("{id:int}")]
    public IActionResult GetById(int id)
    {
        var room = rooms.FirstOrDefault(e => e.Id == id);
        if (room is null)
            return NotFound($"Pokój o id {id} nie istnieje");

        return Ok(rooms.Select(e => new RoomDto()
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

    // [HttpGet]
    // [Route("buildingCode/{buildingCode:string}")]
    // public IActionResult GetRoomsFromBuildingCode(string buildingCode)
    // {
    //     // var code =
    //     
    //     return Ok(rooms.Select(e => new RoomDto()
    //     {
    //         Id = e.Id,
    //         Name = e.Name,
    //         BuildingCode = e.BuildingCode,
    //         Floor = e.Floor,
    //         Capacity = e.Capacity,
    //         HasProjector = e.HasProjector,
    //         IsActive = e.IsActive
    //     }));
    //     
    // }


}