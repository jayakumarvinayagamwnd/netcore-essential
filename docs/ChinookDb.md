# ChinookDb Library

## Overview

A shared **ChinookDb** class library project located at `src/Infrastructure/ChinookDb/` that provides the **Entity Framework Core** data access layer for the Chinook music store database. This library is designed to be shared across all projects in the solution (`AspNetCoreFeatures`, `EfCoreFeatures`, `WebApiCoreFeatures`, etc.).

## Database: Chinook

The Chinook database is a sample music store schema containing Artists, Albums, Tracks, Customers, Invoices, Employees, and related entities.

### Database File

| Item        | Value                          |
|-------------|--------------------------------|
| **File**    | `data/chinookDb.db`            |
| **Type**    | SQLite                         |

The SQLite database file is stored at the solution root under the `data/` folder and is consumed by all projects via the shared `ChinookDbContext`.

## Project Setup

| Property          | Value                                            |
|-------------------|--------------------------------------------------|
| **Project Path**  | `src/Infrastructure/ChinookDb/ChinookDb.csproj`  |
| **Target**        | `net10.0`                                        |
| **Framework**     | .NET 10                                          |
| **EF Core**       | `10.0.0-preview.3.25171.6`                      |
| **Nullable**      | Enabled                                          |
| **ImplicitUsings**| Enabled                                          |

### NuGet Dependencies

- `Microsoft.EntityFrameworkCore` — core EF Framework
- `Microsoft.EntityFrameworkCore.Relational` — relational database support (for `ToTable()`, `HasColumnType()`, etc.)

## Entity Relationship Diagram (Logical)

```
Artist ── Album ── Track ── InvoiceLine ── Invoice ── Customer ── Employee
                    │  │         │
                    │  └── MediaType
                    └───── Genre

Playlist ── PlaylistTrack ── Track
```

## Entity List (11 entities)

| Entity          | Table Name       | Key                  | Key Relationships                                                                                                                                     |
|-----------------|------------------|----------------------|-------------------------------------------------------------------------------------------------------------------------------------------------------|
| **Artist**      | `Artist`         | `ArtistId`           | Albums                                                                                                                                                |
| **Album**       | `Album`          | `AlbumId`            | → Artist, Tracks                                                                                                                                      |
| **Track**       | `Track`          | `TrackId`            | → Album, → Genre, → MediaType, InvoiceLines, PlaylistTracks                                                                                           |
| **Genre**       | `Genre`          | `GenreId`            | Tracks                                                                                                                                                |
| **MediaType**   | `MediaType`      | `MediaTypeId`        | Tracks                                                                                                                                                |
| **Playlist**    | `Playlist`       | `PlaylistId`         | PlaylistTracks                                                                                                                                        |
| **PlaylistTrack**| `PlaylistTrack` | `(PlaylistId, TrackId)` composite | → Playlist, → Track                                                                                                                        |
| **Customer**    | `Customer`       | `CustomerId`         | → Employee (SupportRep), Invoices                                                                                                                     |
| **Employee**    | `Employee`       | `EmployeeId`         | → Employee (Manager self-ref), Customers                                                                                                              |
| **Invoice**     | `Invoice`        | `InvoiceId`          | → Customer, InvoiceLines                                                                                                                              |
| **InvoiceLine** | `InvoiceLine`    | `InvoiceLineId`      | → Invoice, → Track                                                                                                                                    |

## DbContext

**`ChinookDbContext`** (`ChinookDbContext.cs`)

- Inherits from `DbContext`
- Constructor takes `DbContextOptions<ChinookDbContext>`
- Exposes all entities as `DbSet<T>` properties
- Configures mappings via **Fluent API** in `OnModelCreating()`:

### Fluent API Configuration Highlights

- **Table mapping**: All entities mapped to matching table names via `.ToTable("TableName")`
- **Primary keys**: Defined with `.HasKey()`, composite key for `PlaylistTrack`
- **String length constraints**: `.HasMaxLength(n)` applied to all string properties
- **Required fields**: `.IsRequired()` for non-nullable properties
- **Decimal precision**: `.HasColumnType("decimal(10,2)")` for `UnitPrice` and `Total`
- **Foreign keys**: `.HasForeignKey()` with `.HasOne()` / `.WithMany()`
- **Delete behavior**: `DeleteBehavior.SetNull` for optional relationships (SupportRep, Manager)

### Example: Registering in `Program.cs`

```csharp
builder.Services.AddDbContext<ChinookDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Chinook")));
```

## Usage

Add a project reference from any consuming project:

```xml
<ProjectReference Include="..\Infrastructure\ChinookDb\ChinookDb.csproj" />
```

Then inject `ChinookDbContext` via dependency injection:

```csharp
public class SomeService
{
    private readonly ChinookDbContext _context;

    public SomeService(ChinookDbContext context)
    {
        _context = context;
    }

    public async Task<List<Artist>> GetArtistsAsync()
    {
        return await _context.Artists.ToListAsync();
    }
}