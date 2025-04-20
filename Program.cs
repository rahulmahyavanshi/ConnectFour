using System;

namespace ConnectFour
{
    // Enum to define the state of each cell on the board (used for clarity and organization)
    // This is inspired by C# Enum documentation from Microsoft Docs
    public enum CellState
    {
        Empty,
        Player1,
        Player2
    }

    // Main Game class to control gameplay
    public class Game
    {
        private const int Rows = 6; // Number of rows in the Connect Four grid
        private const int Columns = 7; // Number of columns in the Connect Four grid
        private CellState[,] board = new CellState[Rows, Columns]; // 2D array to represent the board
        private string player1Name;
        private string player2Name;
        private CellState currentPlayer;

        // Constructor: Initializes the game with player names and sets Player1 to start
        // This class-based structure was inspired by object-oriented programming (OOP) principles learned from various sources
        public Game(string player1, string player2)
        {
            player1Name = player1;
            player2Name = player2;
            currentPlayer = CellState.Player1; // Player1 starts the game
            InitializeBoard(); // Call to setup the initial empty board
        }

        // Fill the board with empty cells
        // This method is simple and just initializes every position on the board to `Empty`
        private void InitializeBoard()
        {
            for (int i = 0; i < Rows; i++)
                for (int j = 0; j < Columns; j++)
                    board[i, j] = CellState.Empty;
        }

        // Display the current game board on the console (visual representation of the game)
        // The idea of clearing the console to refresh the game view comes from Stack Overflow discussions on game UIs
        private void PrintBoard()
        {
            Console.Clear(); // Clear the console for the updated board state
            Console.WriteLine("Connect Four Game - {0} (X) vs {1} (O)", player1Name, player2Name);
            Console.WriteLine();

            // Loop through the board and print the current state
            // This 2D array iteration is based on learning from various C# 2D array tutorials
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    switch (board[i, j]) // Check the cell state
                    {
                        case CellState.Empty:
                            Console.Write(". "); // Empty cell
                            break;
                        case CellState.Player1:
                            Console.Write("X "); // Player1's move
                            break;
                        case CellState.Player2:
                            Console.Write("O "); // Player2's move
                            break;
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("1 2 3 4 5 6 7"); // Column numbers for user input reference
            Console.WriteLine();
        }

        // Main loop to run the game
        // The game loop logic was inspired by turn-based game structures seen in various tutorials (like Brackeys)
        public void Start()
        {
            bool gameWon = false;
            int totalMoves = 0;
            const int maxMoves = Rows * Columns; // Max number of moves before a draw

            // Game loop: Continues until a winner is found or the board is full
            while (!gameWon && totalMoves < maxMoves)
            {
                PrintBoard(); // Display the current state of the board

                // Output whose turn it is
                Console.WriteLine("{0}'s Turn ({1})", currentPlayer == CellState.Player1 ? player1Name : player2Name, currentPlayer == CellState.Player1 ? "X" : "O");
                Console.Write("Enter column number (1-7): ");

                int column;
                if (int.TryParse(Console.ReadLine(), out column) && column >= 1 && column <= Columns)
                {
                    // Convert the column input to 0-indexed value and try to make the move
                    if (MakeMove(column - 1)) // Convert to 0-indexed
                    {
                        totalMoves++; // Increment total moves
                        gameWon = CheckWinner(); // Check if a winner is found after the move
                        if (!gameWon)
                            SwitchPlayer(); // Switch to the next player
                    }
                    else
                    {
                        Console.WriteLine("Column {0} is full. Choose another column.", column); // Error if the column is full
                        Console.ReadKey();
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number between 1 and 7."); // Input validation learned from Stack Overflow
                    Console.ReadKey();
                }
            }

            PrintBoard(); // Display the final state of the board

            // Determine the winner or if it's a draw
            if (gameWon)
                Console.WriteLine("Congratulations {0}, you win!", currentPlayer == CellState.Player1 ? player1Name : player2Name);
            else
                Console.WriteLine("Game Over! It's a Draw!");
        }

        // Place the player's disc into the selected column if possible
        // The logic here is inspired by the concept of "gravity" in Connect Four, which I found in various tutorials
        private bool MakeMove(int column)
        {
            for (int i = Rows - 1; i >= 0; i--)
            {
                if (board[i, column] == CellState.Empty)
                {
                    board[i, column] = currentPlayer; // Place the player's disc
                    return true;
                }
            }
            return false; // Column is full
        }

        // Switch the current player after each valid move
        // This is a basic method inspired by general turn-based game logic
        private void SwitchPlayer()
        {
            currentPlayer = currentPlayer == CellState.Player1 ? CellState.Player2 : CellState.Player1;
        }

        // Check all possible winning conditions (horizontal, vertical, diagonal)
        // This logic is derived from the Connect Four win-checking algorithms found in online resources
        private bool CheckWinner()
        {
            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Columns; col++)
                {
                    if (board[row, col] == CellState.Empty)
                        continue; // Skip empty cells

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

    // Program entry point
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Connect Four!");

            // Get player names from the user
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
//References & Sources Used
//https://stackoverflow.com/questions/805449/how-do-i-convert-string-to-int
//https://stackoverflow.com/questions/3660190/how-can-i-loop-through-a-two-dimensional-array
//https://stackoverflow.com/questions/14978523/clear-the-console
//https://codereview.stackexchange.com/questions/191821/connect-four-game-logic
