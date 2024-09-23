using Microsoft.EntityFrameworkCore;
using server.Models;

namespace service.Models;

public static class RelationshipContainer
{
    public static Func<IQueryable<PuzzleKnightMovesEntity>, IQueryable<PuzzleKnightMovesEntity>> PuzzleKnightMoves { get; } = pkm => pkm
        .Include(m => m.Matrix)
        .Include(j => j.Job);
}