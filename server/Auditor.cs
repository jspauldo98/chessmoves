using server.Models;
using spauldo_techture;

namespace server;

// Model specific classes for auditing. Spauldo techture writes Operations (insert, update, delete) plus timestamps for any models defined here.
// Can override or add new methods for custom auditing. 
public class MatrixAuditor(MapperFactory mapperFactory, RepoFactory repoFactory)
: Auditor<MatrixEntity, MatrixModel, MatrixDto, MatrixEntityAudit>(mapperFactory, repoFactory) 
{  }
public class JobAuditor(MapperFactory mapperFactory, RepoFactory repoFactory)
: Auditor<JobEntity, JobModel, JobDto, JobEntityAudit>(mapperFactory, repoFactory) 
{  }
public class PuzzleKnightMovesAuditor(MapperFactory mapperFactory, RepoFactory repoFactory)
: Auditor<PuzzleKnightMovesEntity, PuzzleKnightMovesModel, PuzzleKnightMovesDto, PuzzleKnightMovesEntityAudit>(mapperFactory, repoFactory) 
{  }