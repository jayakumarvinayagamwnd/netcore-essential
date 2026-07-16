namespace ChinookDb.Entities;

public class Playlist
{
    public int PlaylistId { get; set; }
    public string Name { get; set; } = string.Empty;

    public ICollection<PlaylistTrack> PlaylistTracks { get; set; } = new List<PlaylistTrack>();
}