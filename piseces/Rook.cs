using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chessC_
{
    internal class Rook : pisece
    {
        public Rook(bool team, int x, int y, Board board) : base(team, "R", x, y, board) { }

        public override List<XY> allSteps()
        {
            List<XY> steps = new List<XY>();

            // Move horizontally to the right
            for (int i = xy.X + 1; i < 8; i++)
            {
                if (!isValidMove(i, xy.Y))
                {
                    if (board.board[xy.Y][i] != null && board.board[xy.Y][i].team != team)
                    {
                        steps.Add(new XY(i, xy.Y));
                    }
                    break;
                }
                steps.Add(new XY(i, xy.Y));
            }

            // Move horizontally to the left
            for (int i = xy.X - 1; i >= 0; i--)
            {
                if (!isValidMove(i, xy.Y))
                {
                    if (board.board[xy.Y][i] != null && board.board[xy.Y][i].team != team)
                    {
                        steps.Add(new XY(i, xy.Y));
                    }
                    break;
                }
                steps.Add(new XY(i, xy.Y));
            }

            // Move vertically downwards
            for (int i = xy.Y + 1; i < 8; i++)
            {
                if (!isValidMove(xy.X, i))
                {
                    if (board.board[i][xy.X] != null && board.board[i][xy.X].team != team)
                    {
                        steps.Add(new XY(xy.X, i));
                    }
                    break;
                }
                steps.Add(new XY(xy.X, i));
            }

            // Move vertically upwards
            for (int i = xy.Y - 1; i >= 0; i--)
            {
                if (!isValidMove(xy.X, i))
                {
                    if (board.board[i][xy.X] != null && board.board[i][xy.X].team != team)
                    {
                        steps.Add(new XY(xy.X, i));
                    }
                    break;
                }
                steps.Add(new XY(xy.X, i));
            }

            return steps;
        }

        private bool isValidMove(int x, int y)
        {
            // Ensure move is within board bounds and target square is either empty or contains opponent piece
            return x >= 0 && x < 8 && y >= 0 && y < 8 && (board.board[y][x] == null || board.board[y][x].team != team);
        }
    }
}
