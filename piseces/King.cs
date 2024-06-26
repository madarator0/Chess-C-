﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chessC_
{
    internal class King : pisece
    {
        public King(bool team, int x, int y, Board board) : base(team, "K", x, y, board) { }

        public override List<XY> allSteps()
        {
            List<XY> steps = new List<XY>();

            // King moves
            int[] dx = { 1, 1, 1, 0, 0, -1, -1, -1 };
            int[] dy = { 1, 0, -1, 1, -1, 1, 0, -1 };

            for (int i = 0; i < 8; i++)
            {
                int nx = xy.X + dx[i];
                int ny = xy.Y + dy[i];
                if (!Program.isOut(nx, ny))
                {
                    if ((board.board[ny][nx] == null || board.board[ny][nx].team != team))
                    {
                        steps.Add(new XY(nx, ny));
                    }
                }
            }

            return steps;
        }
    }

}
