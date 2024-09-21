using HashidsNet;
using spauldo_techture;
using server.Models;
using System.Text.Json;

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
            JobId = entity.Id,
            Description = entity.Description,
            Status = entity.Status,
            Type = entity.Type,
            Puzzle = entity.Puzzle,
            CompleteDate = entity.CompleteDate,
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
            Description = model.Description,
            Status = model.Status,
            Type = model.Type,
            Puzzle = model.Puzzle,
            CompleteDate = model.CompleteDate,
        };
    }

    protected async override Task<JobModel> MapDtoToModel(JobDto dto, bool withValidation = false)
    {
        int jobId = !string.IsNullOrEmpty(dto.Id) ? DecodeId(dto.Id) : 0;

        return new JobModel
        {
            JobId = jobId,
            Description = dto.Description,
            Status = dto.Status,
            Type = dto.Type,
            Puzzle = dto.Puzzle,
            CompleteDate = dto.CompleteDate,
        };
    }

    protected async override Task<JobEntity> MapModelToEntity(JobModel model, bool withValidation = false)
    {
        model.Initialize(_contextAccessor);

        return new JobEntity
        {
            JobId = model.Id,
            Description = model.Description,
            Status = model.Status,
            Type = model.Type,
            Puzzle = model.Puzzle,
            CompleteDate = model.CompleteDate,
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
            JobId = entity.Id,
            Description = entity.Description,
            Status = entity.Status,
            Type = entity.Type,
            Puzzle = entity.Puzzle,
            CompleteDate = entity.CompleteDate,
            CreateBy = entity.CreateBy,
            CreateDate = entity.CreateDate,
            ModifyBy = entity.ModifyBy,
            ModifyDate = entity.ModifyDate,
        };
    }
    #pragma warning restore CS1998 // Async method lacks 'await' operators
}