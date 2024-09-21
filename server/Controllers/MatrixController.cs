using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using server.Logic;
using server.Models;
using spauldo_techture;

namespace server.Controllers;

[ApiController]
[Route("api/Application")]
// Would add authorization here to hit token validator. Haven't added authentication or authorization to this project 
// EG:
//[Authorize(AuthenticationSchemes = AuthenticationScheme.TOKEN)]
public class MatrixController(
    IMatrixLogic matrixLogic
    ) : ControllerBase
{
    private readonly IMatrixLogic _matrixLogic = matrixLogic;

    [HttpGet]
    [Route("Get")]
    [AllowAnonymous]
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
            if (!await _matrixLogic.ExistsByName(name)) return StatusCode(404);
            return Ok(await _matrixLogic.GetByName(name));
        }
        catch (Exception)
        {
            // TODO - need to add exception handling
            return StatusCode(500); 
        }
    }

    [HttpPost]
    [Route("Update")]
    public async Task<IActionResult> Update([FromBody] MatrixDto dto)
    {
        try
        {
            if (string.IsNullOrEmpty(dto.Id)) return BadRequest("Requires matrix Id.");
            // TODO - probably should validate other Dto fields
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