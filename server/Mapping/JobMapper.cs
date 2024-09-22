using HashidsNet;
using spauldo_techture;
using server.Models;

namespace server.Mapping;

public class JobMapper(IHashids hashids, IHttpContextAccessor contextAccessor) 
: MapperYonHashWrapper<JobEntity, JobModel, JobDto, JobEntityAudit>(hashids)
{
    private readonly IHttpContextAccessor _contextAccessor = contextAccessor;

    #pragma warning disable CS1998 // Async method lacks 'await' operators
    protected async override Task<JobModel> MapEntityToModel(JobEntity entity, bool withValidation = false)
    {
        return new JobModel
        {
            JobId = entity.JobId,
            HangfireJobId = entity.HangfireJobId,
            Description = entity.Description,
            Status = entity.Status,
            Puzzle = entity.Puzzle,
            CompleteDate = entity.CompleteDate,
            ErrorMessage = entity.ErrorMessage,
            CreateBy = entity.CreateBy,
            CreateDate = entity.CreateDate,
            ModifyBy = entity.ModifyBy,
            ModifyDate = entity.ModifyDate,
        };
    }

    protected async override Task<JobDto> MapModelToDto(JobModel model, bool withValidation = false)
    {
        string id = EncodeId(model.Id);

        return new JobDto
        {
            Id = id,
            HangfireJobId = model.HangfireJobId,
            Description = model.Description,
            Status = model.Status,
            Puzzle = model.Puzzle,
            CompleteDate = model.CompleteDate,
            ErrorMessage = model.ErrorMessage,
        };
    }

    protected async override Task<JobModel> MapDtoToModel(JobDto dto, bool withValidation = false)
    {
        int jobId = !string.IsNullOrEmpty(dto.Id) ? DecodeId(dto.Id) : 0;

        return new JobModel
        {
            JobId = jobId,
            HangfireJobId = dto.HangfireJobId,
            Description = dto.Description,
            Status = dto.Status,
            Puzzle = dto.Puzzle,
            CompleteDate = dto.CompleteDate,
            ErrorMessage = dto.ErrorMessage,
        };
    }

    protected async override Task<JobEntity> MapModelToEntity(JobModel model, bool withValidation = false)
    {
        // model.Initialize(_contextAccessor);
        // TODO - bug in spauldo techture when using hangfire
        model.CreateDate = DateTime.Now;
        model.CreateBy =  "Demo User";
        model.ModifyDate = DateTime.Now;
        model.ModifyBy = "Demo USer";

        return new JobEntity
        {
            JobId = model.JobId,
            HangfireJobId = model.HangfireJobId,
            Description = model.Description,
            Status = model.Status,
            Puzzle = model.Puzzle,
            CompleteDate = model.CompleteDate,
            ErrorMessage = model.ErrorMessage,
            CreateBy = model.CreateBy,
            CreateDate = model.CreateDate,
            ModifyBy = model.ModifyBy,
            ModifyDate = model.ModifyDate,
        };
    }

    protected async override Task<JobEntityAudit> MapEntityToAudit(JobEntity entity, bool withValidation = false)
    {
        return new JobEntityAudit
        {
            JobId = entity.JobId,
            HangfireJobId = entity.HangfireJobId,
            Description = entity.Description,
            Status = entity.Status,
            Puzzle = entity.Puzzle,
            CompleteDate = entity.CompleteDate,
            ErrorMessage = entity.ErrorMessage,
            CreateBy = entity.CreateBy,
            CreateDate = entity.CreateDate,
            ModifyBy = entity.ModifyBy,
            ModifyDate = entity.ModifyDate,
        };
    }
    #pragma warning restore CS1998 // Async method lacks 'await' operators
}