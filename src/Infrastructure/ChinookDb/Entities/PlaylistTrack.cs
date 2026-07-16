namespace ChinookDb.Entities;

public class PlaylistTrack
{
    public int PlaylistId { get; set; }
    public int TrackId { get; set; }

    public Playlist Playlist { get; set; } = null!;
    public Track Track { get; set; } = null!;
}