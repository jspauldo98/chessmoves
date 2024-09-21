using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using server.Models;

namespace server.Context;

public class PuzzlesDbContext(DbContextOptions<PuzzlesDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        MatrixInit(modelBuilder);
        MatrixAuditInit(modelBuilder);
        SeedMatrix(modelBuilder);
        // ... More model generation
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<MatrixEntity> Applications { get; set; }
    public DbSet<MatrixEntityAudit> ApplicationAudits { get; set; }

    public static void MatrixInit(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MatrixEntity>()
            .ToTable("dbo_matrix")
            .HasKey(e => e.MatrixId);
    }

    public static void MatrixAuditInit(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MatrixEntityAudit>()
            .ToTable("audit_matrix")
            .HasKey(e => e.AuditId);
    }

    public static void SeedMatrix(ModelBuilder modelBuilder)
    {
        char[][] charArray =
        [
            ['A', 'B', 'C', ' ', 'E'],
            [' ', 'G', 'H', 'I', 'J'],
            ['K', 'L', ' ', 'N', 'O'],
            ['P', 'Q', 'R', 'S', 'T'],
            ['U', 'V', ' ', ' ', 'Y']
        ];
        var serializedMatrix = JsonSerializer.Serialize(charArray);

        modelBuilder.Entity<MatrixEntity>().HasData(
            new MatrixEntity { MatrixId = 1, Name = "Default", Rows = 5, Columns = 5, SerializedMatrix = serializedMatrix, CreateBy = "EF-SEED", CreateDate = DateTime.Now, ModifyBy = "EF-SEED", ModifyDate = DateTime.Now }
        );
    }
}
