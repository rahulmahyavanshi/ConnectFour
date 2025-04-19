using System;

namespace ConnectFour
{
    // Enum to define the state of each cell on the board
    public enum CellState
    {
        Empty,
        Player1,
        Player2
    }

    // Main Game class to control gameplay
    public class Game
    {
        private const int Rows = 6;
        private const int Columns = 7;
        private CellState[,] board = new CellState[Rows, Columns];
        private string player1Name;
        private string player2Name;
        private CellState currentPlayer;

        public Game(string player1, string player2)
        {
            player1Name = player1;
            player2Name = player2;
            currentPlayer = CellState.Player1; // Player1 starts the game
            InitializeBoard();
        }

        // Fill the board with empty cells
        private void InitializeBoard()
        {
            for (int i = 0; i < Rows; i++)
                for (int j = 0; j < Columns; j++)
                    board[i, j] = CellState.Empty;
        }

        // Display the current game board on console
        private void PrintBoard()
        {
            Console.Clear();
            Console.WriteLine("Connect Four Game - {0} (X) vs {1} (O)", player1Name, player2Name);
            Console.WriteLine();

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    switch (board[i, j])
                    {
                        case CellState.Empty:
                            Console.Write(". ");
                            break;
                        case CellState.Player1:
                            Console.Write("X ");
                            break;
                        case CellState.Player2:
                            Console.Write("O ");
                            break;
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("1 2 3 4 5 6 7"); // Column numbers
            Console.WriteLine();
        }

        // Main loop to run the game
        public void Start()
        {
            bool gameWon = false;
            int totalMoves = 0;
            const int maxMoves = Rows * Columns;

            while (!gameWon && totalMoves < maxMoves)
            {
                PrintBoard();
                Console.WriteLine("{0}'s Turn ({1})", currentPlayer == CellState.Player1 ? player1Name : player2Name, currentPlayer == CellState.Player1 ? "X" : "O");
                Console.Write("Enter column number (1-7): ");

                int column;
                if (int.TryParse(Console.ReadLine(), out column) && column >= 1 && column <= Columns)
                {
                    if (MakeMove(column - 1)) // Convert to 0-indexed
                    {
                        totalMoves++;
                        gameWon = CheckWinner();
                        if (!gameWon)
                            SwitchPlayer();
                    }
                    else
                    {
                        Console.WriteLine("Column {0} is full. Choose another column.", column);
                        Console.ReadKey();
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number between 1 and 7.");
                    Console.ReadKey();
                }
            }

            PrintBoard();

            if (gameWon)
                Console.WriteLine("Congratulations {0}, you win!", currentPlayer == CellState.Player1 ? player1Name : player2Name);
            else
                Console.WriteLine("Game Over! It's a Draw!");
        }

        // Place the player's disc into the selected column if possible
        private bool MakeMove(int column)
        {
            for (int i = Rows - 1; i >= 0; i--)
            {
                if (board[i, column] == CellState.Empty)
                {
                    board[i, column] = currentPlayer;
                    return true;
                }
            }
            return false; // Column is full
        }

        // Switch current player after each valid move
        private void SwitchPlayer()
        {
            currentPlayer = currentPlayer == CellState.Player1 ? CellState.Player2 : CellState.Player1;
        }

        // Check all possible winning conditions
        private bool CheckWinner()
        {
            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Columns; col++)
                {
                    if (board[row, col] == CellState.Empty)
                        continue;

                    // Check horizontally
                    if (col + 3 < Columns && board[row, col] == board[row, col + 1] &&
                        board[row, col] == board[row, col + 2] &&
                        board[row, col] == board[row, col + 3])
                        return true;

                    // Check vertically
                    if (row + 3 < Rows && board[row, col] == board[row + 1, col] &&
                        board[row, col] == board[row + 2, col] &&
                        board[row, col] == board[row + 3, col])
                        return true;

                    // Check diagonal down-right
                    if (row + 3 < Rows && col + 3 < Columns &&
                        board[row, col] == board[row + 1, col + 1] &&
                        board[row, col] == board[row + 2, col + 2] &&
                        board[row, col] == board[row + 3, col + 3])
                        return true;

                    // Check diagonal down-left
                    if (row + 3 < Rows && col - 3 >= 0 &&
                        board[row, col] == board[row + 1, col - 1] &&
                        board[row, col] == board[row + 2, col - 2] &&
                        board[row, col] == board[row + 3, col - 3])
                        return true;
                }
            }

            return false; // No winner found
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Connect Four!");

            // Get player names
            Console.Write("Enter Player 1 name (X): ");
            string player1 = Console.ReadLine();
            Console.Write("Enter Player 2 name (O): ");
            string player2 = Console.ReadLine();

            // Create and start the game
            Game game = new Game(player1, player2);
            game.Start();

            Console.WriteLine("\nThanks for playing!");
            Console.ReadLine();
        }
    }
}