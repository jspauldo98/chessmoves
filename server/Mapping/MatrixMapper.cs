using HashidsNet;
using spauldo_techture;
using server.Models;
using System.Text.Json;

namespace server.Mapping;

public class MatrixMapper(IHashids hashids, IHttpContextAccessor contextAccessor) 
: MapperYonHashWrapper<MatrixEntity, MatrixModel, MatrixDto, MatrixEntityAudit>(hashids)
{
    private readonly IHttpContextAccessor _contextAccessor = contextAccessor;
    // For complex relational mapping, other mappers would be injected here

    // Silencing 'async method lacks await operators' warning because spauldo_techture does not have an implementation yet for non async operations when not needing async work.
    // Usually with complex multi-relational models async operations are needed during mapping
    #pragma warning disable CS1998 // Async method lacks 'await' operators
    protected async override Task<MatrixModel> MapEntityToModel(MatrixEntity entity, bool withValidation = false)
    {
        // complex multi-relational models have extra work that would be done here.
        char[][] deserializedMatrix = JsonSerializer.Deserialize<char[][]>(entity.SerializedMatrix);

        return new MatrixModel
        {
            MatrixId = entity.Id,
            Name = entity.Name,
            Rows = entity.Rows,
            Columns = entity.Columns,
            Matrix = deserializedMatrix,
            CreateBy = entity.CreateBy,
            CreateDate = entity.CreateDate,
            ModifyBy = entity.ModifyBy,
            ModifyDate = entity.ModifyDate,
        };
    }

    protected async override Task<MatrixDto> MapModelToDto(MatrixModel model, bool withValidation = false)
    {
        string id = EncodeId(model.Id);
        string serializedMatrix = JsonSerializer.Serialize(model.Matrix);

        return new MatrixDto
        {
            Id = id,
            Name = model.Name,
            Rows = model.Rows,
            Columns = model.Columns,
            SerializedMatrix = serializedMatrix
        };
    }

    protected async override Task<MatrixModel> MapDtoToModel(MatrixDto dto, bool withValidation = false)
    {
        int matrixId = !string.IsNullOrEmpty(dto.Id) ? DecodeId(dto.Id) : 0;
        char[][] deserializedMatrix = JsonSerializer.Deserialize<char[][]>(dto.SerializedMatrix);

        return new MatrixModel
        {
            MatrixId = matrixId,
            Name = dto.Name,
            Rows = dto.Rows,
            Columns = dto.Columns,
            Matrix = deserializedMatrix
        };
    }

    protected async override Task<MatrixEntity> MapModelToEntity(MatrixModel model, bool withValidation = false)
    {
        // model.Initialize(_contextAccessor);
        // TODO - bug in spauldo techture when using hangfire
        model.CreateDate = DateTime.Now;
        model.CreateBy =  "Demo User";
        model.ModifyDate = DateTime.Now;
        model.ModifyBy = "Demo USer";
        string serializedMatrix = JsonSerializer.Serialize(model.Matrix);

        return new MatrixEntity
        {
            MatrixId = model.Id,
            Name = model.Name,
            Rows = model.Rows,
            Columns = model.Columns,
            SerializedMatrix = serializedMatrix,
            CreateBy = model.CreateBy,
            CreateDate = model.CreateDate,
            ModifyBy = model.ModifyBy,
            ModifyDate = model.ModifyDate,
        };
    }

    protected async override Task<MatrixEntityAudit> MapEntityToAudit(MatrixEntity entity, bool withValidation = false)
    {
        return new MatrixEntityAudit
        {
            MatrixId = entity.Id,
            Name = entity.Name,
            Rows = entity.Rows,
            Columns = entity.Columns,
            SerializedMatrix = entity.SerializedMatrix,
            CreateBy = entity.CreateBy,
            CreateDate = entity.CreateDate,
            ModifyBy = entity.ModifyBy,
            ModifyDate = entity.ModifyDate,
        };
    }
    #pragma warning restore CS1998 // Async method lacks 'await' operators
}