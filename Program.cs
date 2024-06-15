using System;

namespace GemHunters
{
    class Player
    {
        public string Name { get; set; }
        public int Score { get; set; }
        public int Lives { get; set; }
        public int Gems { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int turn { get; set; }
        public Player(string name, int x, int y)
        {
            Name = name;
            Score = 0;
            Lives = 3;
            Gems = 0;
            X = x;
            Y = y;
            turn = 0;
        }
    }

    class Gem
    {
        public int X { get; set; }
        public int Y { get; set; }
        public string Name { get; set; }

        public Gem(int x, int y)
        {
            X = x;
            Y = y;
            Name = " G";
        }
    }

    class Obstacle
    {
        public int X { get; set; }
        public int Y { get; set; }
        public string Name { get; set; }
        public Obstacle(int x, int y)
        {
            X = x;
            Y = y;
            Name = " 0";
        }
    }

    class GemHuntersGame
    {
        public readonly int boardSize;
        public readonly object[,] board;
        readonly int num_of_gems = 5;
        readonly int num_of_obstacles = 4;
        public Player P1 = new Player("P1", 0, 0);
        public Player P2 = new Player("P2", 5, 5);

        public GemHuntersGame(int size)
        {
            boardSize = size;
            board = new object[boardSize, boardSize];
            board[P1.X, P1.Y] = P1;
            board[P2.X, P2.Y] = P2;
            Random random = new Random();
            for (int i = 0; i < num_of_obstacles; i++)
            {
                do
                {
                    int x = random.Next(0, boardSize - 1);
                    int y = random.Next(0, boardSize - 1);
                    if (board[x, y] == null)
                    {
                        board[x, y] = new Obstacle(x, y);
                        break;
                    }
                } while (true);
            }
            for (int i = 0; i < num_of_gems; i++)
            {
                do
                {
                    int x = random.Next(0, boardSize - 1);
                    int y = random.Next(0, boardSize - 1);
                    if (board[x, y] == null)
                    {
                        board[x, y] = new Gem(x, y);
                        break;
                    }
                } while (true);
            }
        }

        public void PrintBoard()
        {
            for (int row = 0; row < boardSize; row++)
            {
                for (int col = 0; col < boardSize; col++)
                {
                    if (board[row, col] == null)
                    {
                        Console.Write(" --");
                    }
                    else if (board[row, col] is Player)
                    {
                        Console.Write(" "+ ((Player)board[row, col]).Name);
                    }
                    else if (board[row, col] is Gem)
                    {
                        Console.Write(" "+((Gem)board[row, col]).Name);
                    }
                    else if (board[row, col] is Obstacle)
                    {
                        Console.Write(" "+((Obstacle)board[row, col]).Name);
                    }
                    else {
                        Console.Write(board[row, col]);
                    }
                }
                Console.WriteLine();
            }
        }

        public void DisplayBoard()
        {
            for (int row = 0; row < boardSize; row++)
            {
                for (int col = 0; col < boardSize; col++)
                {
                    Console.Write("row: " + row + " col: " + col);
                    Console.Write(" ("+ row.ToString(), col.ToString()+ ") ");
                }

                Console.WriteLine();
            }
        }

        internal void AnnounceWinner()
        {
            Console.WriteLine("Player 1 Gems " + P1.Gems);
            Console.WriteLine("Player 2 Gems " + P2.Gems);
            if (P1.Gems > P2.Gems)
            {
                Console.WriteLine("Player 1 wins!");
            }
            else if (P1.Gems < P2.Gems)
            {
                Console.WriteLine("Player 2 wins!");
            }
            else
            {
                Console.WriteLine("It's a tie!");
            }
        }

        internal bool IsGameOver()
        {
            if (P1.turn >= 15 && P2.turn >= 15)
            {
                Console.WriteLine("Game Over!");
                AnnounceWinner();
                return true;
            }
            return false;
        }

        internal void PlayerTurn(Player player, string turn)
        {
            int previous_x = player.X;
            int previous_y = player.Y;
            if (turn == "up")
            {
                if (player.X == 0)
                {
                    Console.WriteLine("Invalid move, try again");
                    GetTurn();
                }
                else
                {
                    board[player.X, player.Y] = " --";
                    player.X -= 1;

                }
            }
            else if (turn == "down")
            {
                if (player.X == 5)
                {
                    Console.WriteLine("Invalid move, try again");
                    GetTurn();
                }
                else
                {
                    board[player.X, player.Y] = " --";
                    player.X += 1;
                }
            }
            else if (turn == "left")
            {
                if (player.Y == 0)
                {
                    Console.WriteLine("Invalid move, try again");
                    GetTurn();
                }
                else
                {
                    board[player.X, player.Y] = " --";
                    player.Y -= 1;
                }
            }
            else if (turn == "right")
            {
                if (player.Y == 5)
                {
                    Console.WriteLine("Invalid move, try again");
                    GetTurn();
                }
                else
                {
                    board[player.X, player.Y] = " --";
                    player.Y += 1;
                }
            }
            player.turn += 1;
            if (board[player.X, player.Y] is Gem)
            {
                player.Score += 1;
                player.Gems += 1;
                board[player.X, player.Y] = player;
            }
            else if (board[player.X, player.Y] is Obstacle)
            {
                Console.WriteLine("You hit an obstacle!");
                player.X  = previous_x;
                player.Y = previous_y;
                board[player.X, player.Y] = player;

            }
            else
            {
                board[player.X, player.Y] = player;
            }
        }

        internal string GetTurn()
        {
            Console.WriteLine("Enter your move: ");
            string move = Console.ReadLine() ?? string.Empty;
            if (string.IsNullOrEmpty(move))
            {
                Console.WriteLine("Invalid move, try again");
                return GetTurn();
            }
            move = move.ToLower();
            if (move == "u")
            {
                Console.WriteLine("going up");
                return "up";
            }
            else if (move == "d")
            {
                return "down";
            }
            else if (move == "l")
            {
                return "left";
            }
            else if (move == "r")
            {
                return "right";
            }
            else
            {
                Console.WriteLine("Invalid move, try again");
                return GetTurn();
            }
        }

        internal Player SwitchTurn(Player player)
        {
            if (player == P1)
            {
                Console.WriteLine("Player 1's turn");
                PlayerTurn(P1, GetTurn());
                PrintBoard();
                return P2;
            }
            else
            {
                Console.WriteLine("Player 2's turn");
                PlayerTurn(P2, GetTurn());
                PrintBoard();
                return P1;
            }
        }

        internal void StartGame()
        {
            Player player = P1;
            while (!IsGameOver())
            {
                if (player == P1)
                {
                    player = SwitchTurn(P1);
                }
                else
                {
                    player = SwitchTurn(P2);
                }
            }
        }
    }

    public static class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the world of Gem Hunters!");

            int boardSize = 6;
            GemHuntersGame game = new GemHuntersGame(boardSize);

            Console.WriteLine("Here is the board:");
            game.DisplayBoard();
            Console.WriteLine("Here is the board with players and gems:");
            Console.WriteLine("* represents a gem, 0 represents an obstacle, P1 and P2 represent players.");
            game.PrintBoard();
            Console.WriteLine("Press any key to continue...");
            Console.Read();

            Console.Clear();
            game.StartGame();
        }
    }
}
