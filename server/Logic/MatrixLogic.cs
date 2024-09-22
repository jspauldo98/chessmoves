using HashidsNet;
using spauldo_techture;
using server.Models;

namespace server.Logic;

public interface IMatrixLogic : ILogicCrudYon<MatrixDto, MatrixModel, MatrixEntity, MatrixEntityAudit>
{
    // Any custom aside from defaults provided by ILogicCrudYon. EG. GetByName
    Task<MatrixDto> GetByName(string name);
}

public class MatrixLogic(AuditorFactory auditorFactory, MapperFactory mapperFactory, RepoFactory repoFactory, IHashids hashids) 
: LogicCrudYon<MatrixEntity, MatrixModel, MatrixDto, MatrixEntityAudit>(repoFactory, mapperFactory, auditorFactory, hashids)
, IMatrixLogic
{
    // Can override any default crud operations here ... 

    public async Task<MatrixDto> GetByName(string name)
    {
        // Entity framework does not like parameter with 'StringComparison.CurrentCultureIgnoreCase'
        // Spauldo techture has a bug with FirstOrDefault
        return await GetBuilder().Where(m => m.Name.ToLower() == name.ToLower()).LastOrDefaultAsync();
    }
}