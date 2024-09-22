using HashidsNet;
using spauldo_techture;
using server.Models;
using server.Mapping;

namespace server.Mapping;

public class PuzzleKnightMovesMapper(IHashids hashids, IHttpContextAccessor contextAccessor, MatrixMapper matrixMapper, JobMapper jobMapper) 
: MapperYonHashWrapper<PuzzleKnightMovesEntity, PuzzleKnightMovesModel, PuzzleKnightMovesDto, PuzzleKnightMovesEntityAudit>(hashids)
{
    private readonly IHttpContextAccessor _contextAccessor = contextAccessor;
    private readonly MatrixMapper _matrixMapper = matrixMapper;
    private readonly JobMapper _jobMapper = jobMapper;

    protected async override Task<PuzzleKnightMovesModel> MapEntityToModel(PuzzleKnightMovesEntity entity, bool withValidation = false)
    {
        MatrixModel matrixModel = null;
        JobModel jobModel = null;

        if (entity.Matrix != null)
            matrixModel = await _matrixMapper.Map<MatrixModel>(entity.Matrix);
        if (entity.Job != null)
            jobModel = await _jobMapper.Map<JobModel>(entity.Job);

        return new PuzzleKnightMovesModel
        {
            PuzzleKnightMovesId = entity.Id,
            UniquePathsCount = entity.UniquePathsCount,
            MatrixId = entity.MatrixId,
            JobId = entity.JobId,
            Matrix = matrixModel,
            Job = jobModel,
            CreateBy = entity.CreateBy,
            CreateDate = entity.CreateDate,
            ModifyBy = entity.ModifyBy,
            ModifyDate = entity.ModifyDate,
        };
    }

    protected async override Task<PuzzleKnightMovesDto> MapModelToDto(PuzzleKnightMovesModel model, bool withValidation = false)
    {
        string id = EncodeId(model.Id);
        string matrixId = EncodeId(model.MatrixId);
        string jobId = EncodeId(model.JobId);

        MatrixDto matrixDto = null;
        JobDto jobDto = null;
        if (model.Matrix != null)
            matrixDto = await _matrixMapper.Map<MatrixDto>(model.Matrix);
        if (model.Job != null)
            jobDto = await _jobMapper.Map<JobDto>(model.Job);

        return new PuzzleKnightMovesDto
        {
            Id = id,
            UniquePathsCount = model.UniquePathsCount,
            MatrixId = matrixId,
            JobId = jobId,
            Matrix = matrixDto,
            Job = jobDto
        };
    }

    protected async override Task<PuzzleKnightMovesModel> MapDtoToModel(PuzzleKnightMovesDto dto, bool withValidation = false)
    {
        int puzzleKnightMovesId = !string.IsNullOrEmpty(dto.Id) ? DecodeId(dto.Id) : 0;
        int matrixId = !string.IsNullOrEmpty(dto.MatrixId) ? DecodeId(dto.MatrixId) : 0;
        int jobId = !string.IsNullOrEmpty(dto.JobId) ? DecodeId(dto.JobId) : 0;

        MatrixModel matrixModel = null;
        JobModel jobModel = null;

        if (dto.Matrix != null)
            matrixModel = await _matrixMapper.Map<MatrixModel>(dto.Matrix);
        if (dto.Job != null)
            jobModel = await _jobMapper.Map<JobModel>(dto.Job);

        return new PuzzleKnightMovesModel
        {
            PuzzleKnightMovesId = puzzleKnightMovesId,
            UniquePathsCount = dto.UniquePathsCount,
            MatrixId = matrixId,
            JobId = jobId,
            Matrix = matrixModel,
            Job = jobModel
        };
    }

    #pragma warning disable CS1998 // Async method lacks 'await' operators
    protected async override Task<PuzzleKnightMovesEntity> MapModelToEntity(PuzzleKnightMovesModel model, bool withValidation = false)
    {
        // model.Initialize(_contextAccessor);
        // TODO - bug in spauldo techture when using hangfire
        model.CreateDate = DateTime.Now;
        model.CreateBy =  "Demo User";
        model.ModifyDate = DateTime.Now;
        model.ModifyBy = "Demo USer";

        return new PuzzleKnightMovesEntity
        {
            PuzzleKnightMovesId = model.Id,
            UniquePathsCount = model.UniquePathsCount,
            MatrixId = model.MatrixId,
            JobId = model.JobId,
            CreateBy = model.CreateBy,
            CreateDate = model.CreateDate,
            ModifyBy = model.ModifyBy,
            ModifyDate = model.ModifyDate,
        };
    }

    protected async override Task<PuzzleKnightMovesEntityAudit> MapEntityToAudit(PuzzleKnightMovesEntity entity, bool withValidation = false)
    {
        return new PuzzleKnightMovesEntityAudit
        {
            PuzzleKnightMovesId = entity.Id,
            UniquePathsCount = entity.UniquePathsCount,
            MatrixId = entity.MatrixId,
            JobId = entity.JobId,
            CreateBy = entity.CreateBy,
            CreateDate = entity.CreateDate,
            ModifyBy = entity.ModifyBy,
            ModifyDate = entity.ModifyDate,
        };
    }
    #pragma warning restore CS1998 // Async method lacks 'await' operators
}