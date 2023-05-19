using System;

namespace SudokuSolver;
internal class Cell
{
    internal Cell(int number, (int row, int column) position)
    {
        Number = number;
        Position = position;
        PossibleNumbers = new() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
    }

    internal int Number { get; set; } = 0;
    internal (int row, int column) Position { get; set; }

    internal List<Cell> CellComeptition { get; set; } = new();
    internal List<int> PossibleNumbers { get; set; } = new();

    internal void FindCellCompetition(Cell[,] board)
    {
        for (int i = 0; i < 9; i++)
        {
            if (i != Position.column)
            {
                CellComeptition.Add(board[Position.row, i]);
            }
            if (i != Position.row)
            {
                CellComeptition.Add(board[i, Position.column]);
            }
        }

        int startRow = Position.row - Position.row % 3;
        int startCol = Position.column - Position.column % 3;
        for (int row = startRow; row < startRow + 3; row++)
        {
            for (int column = startCol; column < startCol + 3; column++)
            {
                if (row != Position.row && column != Position.column)
                {
                    CellComeptition.Add(board[row, column]);
                }
            }
        }
    }

    internal void FindPossibleNumbers()
    {
        foreach (Cell cell in CellComeptition)
        {
            PossibleNumbers.Remove(cell.Number);
        }
    }
}
