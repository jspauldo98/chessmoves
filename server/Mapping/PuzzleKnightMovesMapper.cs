using HashidsNet;
using spauldo_techture;
using server.Models;
using server.Mapping;

namespace server.Mapping;

public class PuzzleKnightMovesMapper(IHashids hashids, IHttpContextAccessor contextAccessor, MatrixMapper matrixMapper) 
: MapperYonHashWrapper<PuzzleKnightMovesEntity, PuzzleKnightMovesModel, PuzzleKnightMovesDto, PuzzleKnightMovesEntityAudit>(hashids)
{
    private readonly IHttpContextAccessor _contextAccessor = contextAccessor;
    private readonly MatrixMapper _matrixMapper = matrixMapper;

    protected async override Task<PuzzleKnightMovesModel> MapEntityToModel(PuzzleKnightMovesEntity entity, bool withValidation = false)
    {
        MatrixModel matrixModel = null;

        if (entity.Matrix != null)
            matrixModel = await _matrixMapper.Map<MatrixModel>(entity.Matrix);

        return new PuzzleKnightMovesModel
        {
            PuzzleKnightMovesId = entity.Id,
            UniquePathsCount = entity.UniquePathsCount,
            MatrixId = entity.MatrixId,
            Matrix = matrixModel,
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

        MatrixDto matrixDto = null;
        if (model.Matrix != null)
            matrixDto = await _matrixMapper.Map<MatrixDto>(model.Matrix);

        return new PuzzleKnightMovesDto
        {
            Id = id,
            UniquePathsCount = model.UniquePathsCount,
            MatrixId = matrixId,
            Matrix = matrixDto,
        };
    }

    protected async override Task<PuzzleKnightMovesModel> MapDtoToModel(PuzzleKnightMovesDto dto, bool withValidation = false)
    {
        int puzzleKnightMovesId = !string.IsNullOrEmpty(dto.Id) ? DecodeId(dto.Id) : 0;
        int matrixId = !string.IsNullOrEmpty(dto.MatrixId) ? DecodeId(dto.MatrixId) : 0;

        MatrixModel matrixModel = null;

        if (dto.Matrix != null)
            matrixModel = await _matrixMapper.Map<MatrixModel>(dto.Matrix);

        return new PuzzleKnightMovesModel
        {
            PuzzleKnightMovesId = puzzleKnightMovesId,
            UniquePathsCount = dto.UniquePathsCount,
            MatrixId = matrixId,
            Matrix = matrixModel,
        };
    }

    #pragma warning disable CS1998 // Async method lacks 'await' operators
    protected async override Task<PuzzleKnightMovesEntity> MapModelToEntity(PuzzleKnightMovesModel model, bool withValidation = false)
    {
        model.Initialize(_contextAccessor);

        return new PuzzleKnightMovesEntity
        {
            PuzzleKnightMovesId = model.Id,
            UniquePathsCount = model.UniquePathsCount,
            MatrixId = model.MatrixId,
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
            CreateBy = entity.CreateBy,
            CreateDate = entity.CreateDate,
            ModifyBy = entity.ModifyBy,
            ModifyDate = entity.ModifyDate,
        };
    }
    #pragma warning restore CS1998 // Async method lacks 'await' operators
}