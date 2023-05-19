namespace SudokuSolver;

public class Program
{
    private static readonly Cell[,] board = new Cell[9, 9];
    /*{
        { 5, 3, 0, 0, 7, 0, 0, 0, 0 },
        { 6, 0, 0, 1, 9, 5, 0, 0, 0 },
        { 0, 9, 8, 0, 0, 0, 0, 6, 0 },
        { 8, 0, 0, 0, 6, 0, 0, 0, 3 },
        { 4, 0, 0, 8, 0, 3, 0, 0, 1 },
        { 7, 0, 0, 0, 2, 0, 0, 0, 6 },
        { 0, 6, 0, 0, 0, 0, 2, 8, 0 },
        { 0, 0, 0, 4, 1, 9, 0, 0, 5 },
        { 0, 0, 0, 0, 8, 0, 0, 7, 9 }
    };*/
    public static void Main(string[] args)
    {
        PopulateCells();
        PrintBoard(board);
        Console.WriteLine();
        CheckWhileLoneFound();
        PrintBoard(board);
        Console.WriteLine(CheckCorrect());
    }

    private static void PopulateCells()
    {
        int[,] boardNums = new int[9, 9]
        {
            { 5, 3, 0, 0, 7, 0, 0, 0, 0 },
            { 6, 0, 0, 1, 9, 5, 0, 0, 0 },
            { 0, 9, 8, 0, 0, 0, 0, 6, 0 },
            { 8, 0, 0, 0, 6, 0, 0, 0, 3 },
            { 4, 0, 0, 8, 0, 3, 0, 0, 1 },
            { 7, 0, 0, 0, 2, 0, 0, 0, 6 },
            { 0, 6, 0, 0, 0, 0, 2, 8, 0 },
            { 0, 0, 0, 4, 1, 9, 0, 0, 5 },
            { 0, 0, 0, 0, 8, 0, 0, 7, 9 }
        };
        // Populate the cells with information.
        // Have to initialize first for the position setter to work.
        for (int row = 0;  row < board.GetLength(0); row++)
        {
            for (int col = 0;  col < board.GetLength(1); col++)
            {
                board[row, col] = new(boardNums[row, col], (row, col));
            }
        }
        foreach (var cell in board)
        {
            cell.FindCellCompetition(board);
            cell.FindPossibleNumbers();
        }
    }

    private static void CheckWhileLoneFound()
    {
        bool loneFound = true;
        // Keep looping until no more "lone" possible numbers exist.
        while (loneFound)
        {
            loneFound = false;
            // Check each member of the board.
            foreach (var item in board)
            {
                // Move onto the next item if the possible numbers count is not 1.
                if (item.PossibleNumbers.Count != 1)
                {
                    continue;
                }

                item.Number = item.PossibleNumbers[0];
                item.PossibleNumbers.RemoveAt(0);
                foreach (var cell in item.CellComeptition)
                {
                    cell.PossibleNumbers.Remove(item.Number);
                }
                loneFound = true;
            }
        }
        
    }

    private static void PrintBoard(Cell[,] board)
    {
        for (int row = 0; row < 9; row++)
        {
            for (int col = 0; col < 9; col++)
            {
                Console.Write($"{board[row, col].Number}    ");
            }
            Console.WriteLine();
            Console.WriteLine();
        }
    }

    private static void PrintPossibleCells(Cell[,] board, Cell cell)
    {
        int counter = 0;
        foreach (var item in board)
        {
            if (item == cell)
            {
                Console.Write($"C    ");
            }
            else if (cell.CellComeptition.Contains(item))
            {
                Console.Write($"{item.Number}    ");
            }
            else
            {
                Console.Write($"N/A  ");
            }
            counter++;
            if (counter % 9 == 0)
            {
                Console.WriteLine("\n");
            }
        }
    }

    private static void PrintPossibleNumbers(Cell cell)
    {
        foreach (var item in cell.PossibleNumbers)
        {
            Console.WriteLine(item);
        }
    }

    private static bool CheckCorrect()
    {
        int[,] correctBoard = 
        { 
            { 5, 3, 4, 6, 7, 8, 9, 1, 2 }, 
            { 6, 7, 2, 1, 9, 5, 3, 4, 8 }, 
            { 1, 9, 8, 3, 4, 2, 5, 6, 7 }, 
            { 8, 5, 9, 7, 6, 1, 4, 2, 3 }, 
            { 4, 2, 6, 8, 5, 3, 7, 9, 1 }, 
            { 7, 1, 3, 9, 2, 4, 8, 5, 6 }, 
            { 9, 6, 1, 5, 3, 7, 2, 8, 4 }, 
            { 2, 8, 7, 4, 1, 9, 6, 3, 5 }, 
            { 3, 4, 5, 2, 8, 6, 1, 7, 9 } 
        };

        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (correctBoard[i,j] != board[i,j].Number)
                {
                    return false;
                }
            }
        }
        return true;
    }
}