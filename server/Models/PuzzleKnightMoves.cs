using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using spauldo_techture;

namespace server.Models;

public class PuzzleKnightMovesDto : Dto
{
    public long UniquePathsCount { get; set; }
    public string MatrixId { get; set; }
    public string JobId { get; set; }
    public MatrixDto Matrix { get; set; }
    public JobDto Job { get; set; }
}

public class PuzzleKnightMovesModel : Model
{
    public int PuzzleKnightMovesId { get; set; }
    public long UniquePathsCount { get; set; }
    public int MatrixId { get; set; }
    public int JobId { get; set; }
    public MatrixModel Matrix { get; set; }
    public JobModel Job { get; set; }

    public override int Id => PuzzleKnightMovesId;
    public override void Initialize(IHttpContextAccessor context)
    {
        // TODO - bug in spauldo techture when using hangfire
        CreateDate = DateTime.Now;
        CreateBy =  "Demo User";
        ModifyDate = DateTime.Now;
        ModifyBy = "Demo USer";
    }
}

public class PuzzleKnightMovesEntity : Entity
{
    [Key]
    [Required]
    [Column("Id")]
    public int PuzzleKnightMovesId { get; set; }
    public long UniquePathsCount { get; set; }
    public int MatrixId { get; set; }
    public int JobId { get; set; }

    public override int Id => PuzzleKnightMovesId;
    // Relational Attributes
    public virtual MatrixEntity Matrix { get; set;}
    public virtual JobEntity Job { get; set; }
}

public class PuzzleKnightMovesEntityAudit : EntityAudit
{
    [Required]
    public int PuzzleKnightMovesId { get; set; }
    public long UniquePathsCount { get; set; }
    public int MatrixId { get; set; }
    public int JobId { get; set; }
}