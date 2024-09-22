using Hangfire;
using HashidsNet;
using server.Mapping;
using server.Models;
using spauldo_techture;

namespace server.Logic;

public interface IJobLogic : ILogicCrudYon<JobDto, JobModel, JobEntity, JobEntityAudit>
{
    Task<string> ExecuteJob(JobDto dto, string matrixId);
    Task OnJobComplete(JobDto dto);
}

public class JobLogic(
    AuditorFactory auditorFactory, 
    MapperFactory mapperFactory, 
    RepoFactory repoFactory, 
    IHashids hashids, 
    IPuzzleKnightMovesLogic knightMovesLogic)
: LogicCrudYon<JobEntity, JobModel, JobDto, JobEntityAudit>(repoFactory, mapperFactory, auditorFactory, hashids)
, IJobLogic
{
    private readonly IPuzzleKnightMovesLogic _knightMovesLogic = knightMovesLogic;
    public async Task<string> ExecuteJob(JobDto dto, string matrixId)
    {
        dto.Status = JobStatusEnum.QUEUED;
        var jobId = await base.SaveAsync(dto);

        // TODO use factory pattern for puzzle types
        switch (dto.Puzzle)
        {
            case PuzzleTypeEnum.KNIGHT_MOVES:
                string hangfireJobId = BackgroundJob.Enqueue(() => _knightMovesLogic.Solve(matrixId));
                dto.Status = JobStatusEnum.IN_PROGRESS;
                dto.HangfireJobId = hangfireJobId;
                await base.SaveAsync(dto);
                BackgroundJob.ContinueJobWith(hangfireJobId, () => OnJobComplete(dto));
                break;
            default:
                throw new NotSupportedException($"Puzzle Type: {dto.Puzzle} is not supported");
        }
        return jobId;      
    }

    public async Task OnJobComplete(JobDto dto)
    {
        dto.Status = JobStatusEnum.FINISHED;
        dto.CompleteDate = DateTime.Now;
        await base.SaveAsync(dto);
    }
}