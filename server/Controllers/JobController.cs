using Microsoft.AspNetCore.Mvc;

using server.Logic;
using server.Models;
using spauldo_techture;

namespace server.Controllers;

[ApiController]
[Route("api/job")]
// Would add authorization here to hit token validator. Haven't added authentication or authorization to this project 
// EG:
//[Authorize(AuthenticationSchemes = AuthenticationScheme.TOKEN)]
public class JobController(
    IJobLogic jobLogic
    ) : ControllerBase
{
    private readonly IJobLogic _jobLogic = jobLogic;

    [HttpGet]
    [Route("Get")]
    public async Task<IActionResult> Get([FromQuery] string jobId)
    {
        try
        {
            if (!await _jobLogic.ExistsAsync(jobId)) return StatusCode(404);
            return Ok(await _jobLogic.GetAsync(jobId));
        }
        catch (Exception) 
        {
            // TODO - need to add exception handling
            return StatusCode(500); 
        }
    }

    [HttpPut]
    [Route("Put")]
    public async Task<IActionResult> Put([FromBody] JobDto job, [FromQuery] string matrixId)
    {
        try
        {
            return Ok(await _jobLogic.ExecuteJob(job, matrixId));
        }
        catch (Exception) 
        {
            // TODO - need to add exception handling
            return StatusCode(500); 
        }
    }

    [HttpDelete]
    [Route("Delete")]
    public async Task<IActionResult> Delete([FromQuery] string jobId)
    {
        try
        {
            if (string.IsNullOrEmpty(jobId)) return BadRequest("Requires Job Id.");
            await _jobLogic.DeleteAsync(jobId);
            return Ok();
        }
        catch (Exception)
        {
            // TODO - need to add exception handling
            return StatusCode(500); 
        }
    }
}