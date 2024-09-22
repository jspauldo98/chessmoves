using spauldo_techture;
using server.Context;
using server.Models;

namespace server.Repository;

public interface IPuzzleKnightMovesRepo : IRepoEntityFramework<PuzzleKnightMovesEntity> 
{  }
public interface IPuzzleKnightMovesAuditRepo : IRepoEntityFramework<PuzzleKnightMovesEntityAudit> 
{  }

public class PuzzleKnightMovesRepo(PuzzlesDbContext dbContext) 
: RepoEntityFramework<PuzzleKnightMovesEntity>(dbContext), IPuzzleKnightMovesRepo 
{  }
public class PuzzleKnightMovesAuditRepo(PuzzlesDbContext dbContext) 
: RepoEntityFramework<PuzzleKnightMovesEntityAudit>(dbContext), IPuzzleKnightMovesAuditRepo 
{  }