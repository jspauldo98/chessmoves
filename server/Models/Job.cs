using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using spauldo_techture;

namespace server.Models;

public enum JobStatusEnum
{
    DEFAULT,
    QUEUED,
    IN_PROGRESS,
    FINISHED
}

public class JobDto : Dto
{
    public string HangfireJobId { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public JobStatusEnum Status { get; set; }
    [Required]
    public PuzzleTypeEnum Puzzle { get; set; }
    public DateTime CompleteDate { get; set; }
    public string ErrorMessage { get; set; }
}

public class JobModel : Model
{
    public string HangfireJobId { get; set; }
    public int JobId { get; set; }
    public string Description { get; set; }
    public JobStatusEnum Status { get; set; }
    public PuzzleTypeEnum Puzzle { get; set; }
    public DateTime CompleteDate { get; set; }
    public string ErrorMessage { get; set; }
    
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
    public string HangfireJobId { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public JobStatusEnum Status { get; set; }
    [Required]
    public PuzzleTypeEnum Puzzle { get; set; }
    public DateTime CompleteDate { get; set; }
    public string ErrorMessage { get; set; }

    public override int Id => JobId;
}

public class JobEntityAudit : EntityAudit
{
    [Key]
    [Required]
    public int JobId { get; set; }
    [Required]
    public string HangfireJobId { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public JobStatusEnum Status { get; set; }
    [Required]
    public PuzzleTypeEnum Puzzle { get; set; }
    public DateTime CompleteDate { get; set; }
    public string ErrorMessage { get; set; }
}