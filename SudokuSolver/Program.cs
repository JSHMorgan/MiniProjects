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
        PrintPossibleCells(board, board[0, 0]);
        PrintPossibleNumbers(board[0, 0]);
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
}