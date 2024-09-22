using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using spauldo_techture;

namespace server.Models;
public class MatrixDto : Dto
{
    [Required]
    public string Name { get; set; }
    [Required]
    public int Rows { get; set; }
    [Required]
    public int Columns { get; set; }
    [Required]
    public string SerializedMatrix { get; set; }
}

public class MatrixModel : Model
{
    public int MatrixId { get; set; }
    public string Name { get; set; }
    public int Rows { get; set; }
    public int Columns { get; set; }
    public char[][] Matrix { get; set; }

    public override int Id => MatrixId;

    public override void Initialize(IHttpContextAccessor context)
    {
        // TODO - bug in spauldo techture when using hangfire
        CreateDate = DateTime.Now;
        CreateBy =  "Demo User";
        ModifyDate = DateTime.Now;
        ModifyBy = "Demo USer";
    }
}

public class MatrixEntity : Entity
{
    [Key]
    [Required]
    [Column("Id")]
    public int MatrixId { get; set; }
    public string Name { get; set; }
    [Required]
    public int Rows { get; set; }
    [Required]
    public int Columns { get; set; }
    [Required]
    public string SerializedMatrix { get; set; }

     public override int Id => MatrixId;
}

public class MatrixEntityAudit : EntityAudit
{
    [Required]
    public int MatrixId { get; set; }
    public string Name { get; set; }
    [Required]
    public int Rows { get; set; }
    [Required]
    public int Columns { get; set; }
    [Required]
    public string SerializedMatrix { get; set; }
}