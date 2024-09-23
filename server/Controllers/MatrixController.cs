using Microsoft.AspNetCore.Mvc;

using server.Logic;
using server.Models;

namespace server.Controllers;

[ApiController]
[Route("api/matrix")]
public class MatrixController(
    IMatrixLogic matrixLogic
    ) : ControllerBase
{
    private readonly IMatrixLogic _matrixLogic = matrixLogic;

    [HttpGet]
    [Route("Get")]
    public async Task<IActionResult> Get([FromQuery] string matrixId)
    {
        try
        {
            if (!await _matrixLogic.ExistsAsync(matrixId)) return StatusCode(404);
            return Ok(await _matrixLogic.GetAsync(matrixId));
        }
        catch (Exception) 
        {
            // TODO - need to add exception handling
            return StatusCode(500); 
        }
    }

    [HttpGet]
    [Route("GetByName")]
    public async Task<IActionResult> GetByName([FromQuery] string name)
    {
        try
        {
            return Ok(await _matrixLogic.GetByName(name));
        }
        catch (Exception)
        {
            // TODO - need to add exception handling
            return StatusCode(500); 
        }
    }

    [HttpGet]
    [Route("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            return Ok(await _matrixLogic.GetAll());
        }
        catch (Exception)
        {
            // TODO - need to add exception handling
            return StatusCode(500); 
        }
    }

    [HttpPut]
    [Route("Put")]
    public async Task<IActionResult> Put([FromBody] MatrixDto dto)
    {
        try
        {
            return Ok(await _matrixLogic.SaveAsync(dto));
        }
        catch (Exception) 
        {
            // TODO - need to add exception handling
            return StatusCode(500); 
        }
    }

    [HttpDelete]
    [Route("Delete")]
    public async Task<IActionResult> Delete([FromQuery] string matrixId)
    {
        try
        {
            if (string.IsNullOrEmpty(matrixId)) return BadRequest("Requires matrix Id.");
            await _matrixLogic.DeleteAsync(matrixId);
            return Ok();
        }
        catch (Exception)
        {
            // TODO - need to add exception handling
            return StatusCode(500); 
        }
    }
}