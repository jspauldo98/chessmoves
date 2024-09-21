using spauldo_techture;
using server.Context;
using server.Models;

namespace server.Repository;

public interface IJobRepo : IRepoEntityFramework<JobEntity> 
{  }
public interface IJobAuditRepo : IRepoEntityFramework<JobEntityAudit> 
{  }

public class JobRepo(PuzzlesDbContext dbContext) 
: RepoEntityFramework<JobEntity>(dbContext), IJobRepo 
{  }
public class JobAuditRepo(PuzzlesDbContext dbContext) 
: RepoEntityFramework<JobEntityAudit>(dbContext), IJobAuditRepo 
{  }