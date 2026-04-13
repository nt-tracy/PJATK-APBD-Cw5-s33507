namespace APBD_C05.DTOs;

public class RoomDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    
    public string BuildingCode { get; set; } = string.Empty;
    public int Floor { get; set; }
    public double Capacity { get; set; }
    public bool HasProjector { get; set; }
    public bool IsActive { get; set; }
}