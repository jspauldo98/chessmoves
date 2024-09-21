using spauldo_techture;
using server.Context;
using server.Models;

namespace server.Repository;

// spauldo_techture handles all crud operation. Can create new methods for custom repo layer logic
public interface IMatrixRepo : IRepoEntityFramework<MatrixEntity> 
{  }
public interface IMatrixAuditRepo : IRepoEntityFramework<MatrixEntityAudit> 
{  }

public class MatrixRepo(PuzzlesDbContext dbContext) 
: RepoEntityFramework<MatrixEntity>(dbContext), IMatrixRepo 
{  }
public class MatrixAuditRepo(PuzzlesDbContext dbContext) 
: RepoEntityFramework<MatrixEntityAudit>(dbContext), IMatrixAuditRepo 
{  }