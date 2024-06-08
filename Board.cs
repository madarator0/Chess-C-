using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chessC_
{
    internal class Board
    {
        private Player player1;
        private Player player2;
        private Player attacking;
        private Player protecting;

        public pisece[][] board { get; private set; }

        public Board()
        {
            player1 = new Player(this, pisece.W);
            player2 = new Player(this, pisece.B);
            board = new pisece[8][];
            for (int i = 0; i < 8; i++)
            {
                board[i] = new pisece[8];
            }
            attacking = player1;
            protecting = player2;
            initializePiseces();
        }

        private void initializePiseces()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    board[i][j] = null;
                }
            }
            foreach (pisece p in player1.piseces)
            {
                board[p.xy.Y][p.xy.X] = p;
            }
            foreach (pisece p in player2.piseces)
            {
                board[p.xy.Y][p.xy.X] = p;
            }
        }

        private void printAllP(List<XY> allSteps)
        {
            Console.Write("+");
            for (int i = 0; i < allSteps.Count; i++)
            {
                Console.Write("---+");
            }
            Console.WriteLine();
            Console.Write("|");
            for (int i = 0; i < allSteps.Count; i++)
            {
                if ((board[allSteps[i].Y][allSteps[i].X]?.symbol ?? " ") != "K")
                    Console.Write($" {(char)('a' + allSteps[i].X)} |");
            }
            Console.WriteLine();
            Console.Write("+");
            for (int i = 0; i < allSteps.Count; i++)
            {
                Console.Write("---+");
            }
            Console.WriteLine();
            Console.Write("|");
            for (int i = 0; i < allSteps.Count; i++)
            {
                if ((board[allSteps[i].Y][allSteps[i].X]?.symbol ?? " ") != "K")
                    Console.Write($" {allSteps[i].Y + 1} |");
            }
            Console.WriteLine();
            Console.Write("+");
            for (int i = 0; i < allSteps.Count; i++)
            {
                Console.Write("---+");
            } 
            Console.WriteLine();
        }

        private bool canMove(List<XY> allSteps, XY xy)
        {
            return allSteps.Contains(xy);
        }

        private bool isCheck(Player player)
        {
            var king = player.piseces.Find(p => p is King);
            foreach (var p in attacking.piseces)
            {
                if (p.allSteps().Contains(king.xy))
                {
                    return true;
                }
            }
            return false;
        }

        private bool isCheckmate(Player player)
        {
            foreach (var p in player.piseces)
            {
                if (p.validMoves().Count > 0)
                {
                    return false;
                }
            }
            return true;
        }

        public bool isValidMove(pisece piece, XY move)
        {
            var originalPosition = piece.xy;
            var targetPiece = board[move.Y][move.X];

            board[move.Y][move.X] = piece;
            board[originalPosition.Y][originalPosition.X] = null;
            piece.move(move.X, move.Y);

            bool valid = !isCheck(protecting);

            board[move.Y][move.X] = targetPiece;
            board[originalPosition.Y][originalPosition.X] = piece;
            piece.move(originalPosition.X, originalPosition.Y);

            return valid;
        }


        private void round()
        {
            string team = (attacking.team == true) ? "Белых" : "Чёрных";
            Console.WriteLine($"Ход {team}");
            pisece Pisece = attacking.choose();
            List<XY> allSteps = Pisece.allSteps();
            printAllP(allSteps);
            XY xy;
            do
            {
                xy =  Program.getValidCoordinates();
            } while (!canMove(allSteps, xy));

            if (board[xy.Y][xy.X] == null)
            {
                Pisece.move(xy.X, xy.Y);
            }
            else
            {
                protecting.piseces.Remove(board[xy.Y][xy.X]);
                Pisece.move(xy.X, xy.Y);
            }
            initializePiseces();
            Player tmp = attacking;
            attacking = protecting;
            protecting = tmp;
        }

        private void print()
        {
            Console.Clear();
            Console.Write("  ");
            for (char a = 'a'; a <= 'h'; a++)
            {
                Console.Write("  " + a + " ");
            }
            Console.WriteLine();

            Console.Write("  +");
            for (int i = 0; i < 8; i++)
            {
                Console.Write("---+");
            }
            Console.WriteLine();

            for (int i = 0; i < 8; i++)
            {
                Console.Write(i + 1 + " |");
                for (int j = 0; j < 8; j++)
                {
                    var piece = board[i][j];
                    if (piece != null)
                    {
                        Console.BackgroundColor = (i + j) % 2 == 0 ? ConsoleColor.DarkGreen : ConsoleColor.DarkCyan;
                        Console.ForegroundColor = piece.team ? ConsoleColor.White : ConsoleColor.Black;
                        Console.Write($" {piece.symbol} ");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.BackgroundColor = (i + j) % 2 == 0 ? ConsoleColor.DarkGreen : ConsoleColor.DarkCyan;
                        Console.Write("   ");
                        Console.ResetColor();
                    }
                    Console.Write("|");
                }
                Console.WriteLine(i + 1);
                Console.Write("  +");
                for (int j = 0; j < 8; j++)
                {
                    Console.Write("---+");
                }
                Console.WriteLine();
            }

            Console.Write("  ");
            for (char a = 'a'; a <= 'h'; a++)
            {
                Console.Write("  " + a + " ");
            }
            Console.WriteLine();
        }

        public void Play()
        {
            print();
            while (true)
            {
                round();
                print();
            }
        }
    }
}
