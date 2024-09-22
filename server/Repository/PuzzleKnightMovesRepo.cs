using spauldo_techture;
using server.Context;
using server.Models;
using service.Models;

namespace server.Repository;

public interface IPuzzleKnightMovesRepo : IRepoEntityFramework<PuzzleKnightMovesEntity> 
{  }
public interface IPuzzleKnightMovesAuditRepo : IRepoEntityFramework<PuzzleKnightMovesEntityAudit> 
{  }

public class PuzzleKnightMovesRepo(PuzzlesDbContext dbContext) 
: RepoEntityFramework<PuzzleKnightMovesEntity>(dbContext, RelationshipContainer.PuzzleKnightMoves), IPuzzleKnightMovesRepo 
{ 
    private readonly PuzzlesDbContext _dbContext = dbContext;
    // spauldo_techture has a bug with updates (inserts work fine)
    public async override Task Update(PuzzleKnightMovesEntity entity)
    {
        try
        {
            var entityType = _dbContext.Model.FindEntityType(entity.GetType());
            var primaryKey = entityType.FindPrimaryKey() ?? throw new InvalidOperationException("Entity does not have a primary key defined");
            var keyValues = new object[primaryKey.Properties.Count];
            for (int i = 0; i < keyValues.Length; i++)
            {
                keyValues[i] = primaryKey.Properties[i].GetGetter().GetClrValue(entity);
            }

            var existingEntity = await _dbContext.Set<PuzzleKnightMovesEntity>().FindAsync(keyValues);

            if (existingEntity == null)
            {
                // If the entity doesn't exist in the database, add it
                _dbContext.Set<PuzzleKnightMovesEntity>().Add(entity);
            }
            else
            {
                // If the entity exists, update its values
                _dbContext.Entry(existingEntity).CurrentValues.SetValues(entity);
            }

            await _dbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
public class PuzzleKnightMovesAuditRepo(PuzzlesDbContext dbContext) 
: RepoEntityFramework<PuzzleKnightMovesEntityAudit>(dbContext), IPuzzleKnightMovesAuditRepo 
{  }