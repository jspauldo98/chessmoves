using Microsoft.AspNetCore.Mvc;

using server.Logic;
using server.Models;
using spauldo_techture;

namespace server.Controllers;

[ApiController]
[Route("api/puzzle-knight-moves")]
public class PuzzleKnightMovesController(
    IPuzzleKnightMovesLogic knightMovesLogic
    ) : ControllerBase
{
    private readonly IPuzzleKnightMovesLogic _knightMovesLogic = knightMovesLogic;

    [HttpGet]
    [Route("GetList")]
    public async Task<IActionResult> GetList()
    {
        try
        {
            List<PuzzleKnightMovesDto> knightMovesDto = await _knightMovesLogic.GetBuilder().ToListAsync();
            return Ok(knightMovesDto);
        }
        catch (Exception) 
        {
            // TODO - need to add exception handling
            return StatusCode(500); 
        }
    }
}