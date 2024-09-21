using HashidsNet;
using server.Models;
using spauldo_techture;

namespace server.Logic;

public interface IJobLogic : ILogicCrudYon<JobDto, JobModel, JobEntity, JobEntityAudit>
{
    Task OnJobComplete();
}

public class JobLogic(AuditorFactory auditorFactory, MapperFactory mapperFactory, RepoFactory repoFactory, IHashids hashids)
: LogicCrudYon<JobEntity, JobModel, JobDto, JobEntityAudit>(repoFactory, mapperFactory, auditorFactory, hashids)
, IJobLogic
{
    public override Task<string> SaveAsync(JobDto dto)
    {
        // TODO get puzzle from puzzle type and job type
        // TODO Hangfire init
        return base.SaveAsync(dto);
    }

    public override Task DeleteAsync(string id)
    {
        // Remove from hangfire
        return base.DeleteAsync(id);
    }

    public async Task OnJobComplete()
    {

    }
}