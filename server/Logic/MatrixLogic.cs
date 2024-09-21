using HashidsNet;
using spauldo_techture;
using server.Models;

namespace server.Logic;

public interface IMatrixLogic : ILogicCrudYon<MatrixDto, MatrixModel, MatrixEntity, MatrixEntityAudit>
{
    // Any custom aside from defaults provided by ILogicCrudYon. EG. GetByName
    Task<MatrixDto> GetByName(string name);
    Task<bool> ExistsByName(string name);
}

public class MatrixLogic(AuditorFactory auditorFactory, MapperFactory mapperFactory, RepoFactory repoFactory, IHashids hashids) 
: LogicCrudYon<MatrixEntity, MatrixModel, MatrixDto, MatrixEntityAudit>(repoFactory, mapperFactory, auditorFactory, hashids)
, IMatrixLogic
{
    // Can override any default crud operations here ... 

    public async Task<MatrixDto> GetByName(string name)
    {
        return await GetBuilder().Where(m => m.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefaultAsync();
    }

    public async Task<bool> ExistsByName(string name)
    {
        return await ExistsBuilder().Where(m => m.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase)).ExistsAsync();
    }
}