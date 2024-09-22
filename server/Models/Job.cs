using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using spauldo_techture;

namespace server.Models;

public enum JobStatusEnum
{
    DEFAULT = 0,
    QUEUED = 1,
    IN_PROGRESS = 2,
    FINISHED = 3
}

public class JobDto : Dto
{
    public string HangfireJobId { get; set; }
    [Required]
    public string Description { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public JobStatusEnum Status { get; set; }
    [Required]
    [JsonConverter(typeof(JsonStringEnumConverter))]
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
        // TODO - bug in spauldo techture when using hangfire
        CreateDate = DateTime.Now;
        CreateBy =  "Demo User";
        ModifyDate = DateTime.Now;
        ModifyBy = "Demo USer";
    }
}

public class JobEntity : Entity
{
    [Key]
    [Required]
    [Column("Id")]
    public int JobId { get; set; }
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