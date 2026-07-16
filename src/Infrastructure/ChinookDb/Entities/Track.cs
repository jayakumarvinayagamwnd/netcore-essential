namespace ChinookDb.Entities;

public class Track
{
    public int TrackId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int AlbumId { get; set; }
    public int MediaTypeId { get; set; }
    public int GenreId { get; set; }
    public string? Composer { get; set; }
    public int Milliseconds { get; set; }
    public int? Bytes { get; set; }
    public decimal UnitPrice { get; set; }

    public Album Album { get; set; } = null!;
    public MediaType MediaType { get; set; } = null!;
    public Genre Genre { get; set; } = null!;
    public ICollection<InvoiceLine> InvoiceLines { get; set; } = new List<InvoiceLine>();
    public ICollection<PlaylistTrack> PlaylistTracks { get; set; } = new List<PlaylistTrack>();
}