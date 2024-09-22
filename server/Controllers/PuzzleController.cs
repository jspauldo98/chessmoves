using Microsoft.AspNetCore.Mvc;

using server.Logic;
using server.Models;

namespace server.Controllers;

[ApiController]
[Route("api/puzzle")]
public class PuzzleController(
    IPuzzleKnightMovesLogic knightMovesLogic
    ) : ControllerBase
{
    private readonly IPuzzleKnightMovesLogic _knightMovesLogic = knightMovesLogic;

    [HttpGet]
    [Route("GetKnightMovesResults")]
    public async Task<IActionResult> GetKnightMovesResults()
    {
        try
        {
            var knightMovesDto = await _knightMovesLogic.GetBuilder().WithAllRelated().ToListAsync();
            return Ok(knightMovesDto);
        }
        catch (Exception) 
        {
            // TODO - need to add exception handling
            return StatusCode(500); 
        }
    }

    [HttpDelete]
    [Route("DeleteKnightMovesResult")]
    public async Task<IActionResult> DeleteKnightMovesResult([FromQuery] string id)
    {
        try
        {
            await _knightMovesLogic.DeleteAsync(id);
            return Ok();
        }
        catch (Exception) 
        {
            // TODO - need to add exception handling
            return StatusCode(500); 
        }
    }
}