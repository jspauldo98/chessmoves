using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using spauldo_techture;

namespace server.Models;

public enum JobTypeEnum
{
    SOLVE,
    // ... Other types of puzzle jobs
}

public enum JobStatusEnum
{
    DEFAULT,
    QUEUED,
    IN_PROGRESS,
    FINISHED
}

public class JobDto : Dto
{
    [Required]
    public string Description { get; set; }
    [Required]
    public JobStatusEnum Status { get; set; }
    [Required]
    public PuzzleTypeEnum Puzzle { get; set; }
    [Required]
    public JobTypeEnum Type { get; set; }
    public DateTime CompleteDate { get; set; }
}

public class JobModel : Model
{
    public int JobId { get; set; }
    public string Description { get; set; }
    public JobStatusEnum Status { get; set; }
    public JobTypeEnum Type { get; set; }
    public PuzzleTypeEnum Puzzle { get; set; }
    public DateTime CompleteDate { get; set; }
    
    public override int Id => JobId;
    public override void Initialize(IHttpContextAccessor context)
    {
        base.Initialize(context);
    }
}

public class JobEntity : Entity
{
    [Key]
    [Required]
    [Column("Id")]
    public int JobId { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public JobStatusEnum Status { get; set; }
    [Required]
    public PuzzleTypeEnum Puzzle { get; set; }
    [Required]
    public JobTypeEnum Type { get; set; }
    public DateTime CompleteDate { get; set; }

    public override int Id => JobId;
}

public class JobEntityAudit : EntityAudit
{
    [Key]
    [Required]
    public int JobId { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public JobStatusEnum Status { get; set; }
    [Required]
    public PuzzleTypeEnum Puzzle { get; set; }
    [Required]
    public JobTypeEnum Type { get; set; }
    public DateTime CompleteDate { get; set; }
}