using System.Security.Claims;
using System.Text.Json;
using HashidsNet;
using LinqToDB.DataProvider;
using server.Models;
using spauldo_techture;

namespace server.Logic;

public interface IPuzzleKnightMovesLogic 
: ILogicCrudYon<PuzzleKnightMovesDto, PuzzleKnightMovesModel, PuzzleKnightMovesEntity, PuzzleKnightMovesEntityAudit>
{
    Task<long> Solve(string matrixId, string puzzleId);
}

public class PuzzleKnightMovesLogic(AuditorFactory auditorFactory, MapperFactory mapperFactory, RepoFactory repoFactory, IHashids hashids, IMatrixLogic matrixLogic)
: LogicCrudYon<PuzzleKnightMovesEntity, PuzzleKnightMovesModel, PuzzleKnightMovesDto, PuzzleKnightMovesEntityAudit>(repoFactory, mapperFactory, auditorFactory, hashids)
, IPuzzleKnightMovesLogic
{
    private readonly IMatrixLogic _matrixLogic = matrixLogic;
    private readonly MapperFactory _mapperFactory = mapperFactory;
    private static readonly int TARGET_DEPTH = 10;
    private static readonly int MAX_VOWELS = 2;
    private static readonly HashSet<char> VOWELS = ['A', 'E', 'I', 'O', 'U', 'Y'];
    private static readonly HashSet<char> INVALID_CELLS = [' '];
    private static readonly (int, int)[] KNIGHT_MOVES =
    [
        (1, 2), (1, -2),
        (2, 1), (2, -1),
        (-1, 2), (-1, -2),
        (-2, 1), (-2, -1)
    ];
    private class Cell(int row, int col, int depth, int vowelCount)
    {
        public int Row { get; } = row;
        public int Col { get; } = col;
        public int Depth { get; } = depth;
        public int VowelCount { get; } = vowelCount;
    }

    public async Task<long> Solve(string matrixId, string puzzleId)
    {
        MatrixDto matrixDto = await _matrixLogic.GetAsync(matrixId);
        PuzzleKnightMovesDto puzzleDto = await GetAsync(puzzleId);
        char[][] matrix = JsonSerializer.Deserialize<char[][]>(matrixDto.SerializedMatrix);

        long count = 0;
        for (int i = 0; i < matrix.Length; i++)
        {
            for (int j = 0; j < matrix[i].Length; j++)
            {
                if (INVALID_CELLS.Contains(matrix[i][j])) continue;

                int vowelCount = VOWELS.Contains(matrix[i][j]) ? 1 : 0;
                var startCell = new Cell(i, j, 1, vowelCount);
                count += RecurseForDepth(matrix, startCell);
            }
        }

        puzzleDto.UniquePathsCount = count;
        await SaveAsync(puzzleDto);
        return count;
    }

    private static long RecurseForDepth(char[][] matrix, Cell cell)
        {
            if (cell == null) return 0;
            if (cell.Depth == TARGET_DEPTH) return 1;

            long count = 0;
            foreach (var move in KNIGHT_MOVES)
            {
                if (TryGetNextCell(matrix, cell, move, out Cell nextCell))
                {
                    count += RecurseForDepth(matrix, nextCell);
                }
            }

            return count;
        }

        private static bool TryGetNextCell(char[][] matrix, Cell currentCell, (int rowMove, int colMove) move, out Cell nextCell)
        {
            nextCell = null;
            int newRow = currentCell.Row + move.rowMove;
            int newCol = currentCell.Col + move.colMove;

            // Check for invalid moves
            if (!IsValidMove(matrix, newRow, newCol)) return false;

            char cell = matrix[newRow][newCol];
            int newVowelCount = currentCell.VowelCount + (VOWELS.Contains(cell) ? 1 : 0); 

            // Check for illegal vowels
            if (newVowelCount > MAX_VOWELS) return false;

            nextCell = new Cell(newRow, newCol, currentCell.Depth + 1, newVowelCount);
            return true;
        }

        private static bool IsValidMove(char[][] matrix, int row, int col)
        {
            return row >= 0 && row < matrix.Length &&
                   col >= 0 && col < matrix[row].Length &&
                   !INVALID_CELLS.Contains(matrix[row][col]);
        }
}