using System;
using System.Collections.Generic;
using System.Linq;

namespace MagicalHexagonSolver.Models
{
    public class Board
    {
        public byte[] BoardList { get; set; }   // Array that holds the values for each item in the hexagon.
        public byte[] Sums;                     // Represents the 15 different sums that belongs to each Board (horizontal and cross sums)
        public double Height;                   // Height of the Board. Higher is better.
        public bool IsFinalBoard;               // Represents whether this is the final board or not.
        private readonly Board _parentBoard;              // This is used to keep board's path. So that we can track back to the first board status.

        public Board(byte[] boardList, Board parent)
        {
            BoardList = boardList;
            _parentBoard = parent;

            CalculateSums();
            CalculateIsFinalBoard();
            CalculateHeight();
        }

        // Calculates the 15 different sums (horizontal and cross sums).
        private void CalculateSums()
        {
            byte[] sums = new byte[15];

            sums[0] = (byte) (BoardList[0] + BoardList[1] + BoardList[2]);
            sums[1] = (byte) (BoardList[3] + BoardList[4] + BoardList[5] + BoardList[6]);
            sums[2] = (byte) (BoardList[7] + BoardList[8] + BoardList[9] + BoardList[10] + BoardList[11]);
            sums[3] = (byte) (BoardList[12] + BoardList[13] + BoardList[14] + BoardList[15]);
            sums[4] = (byte) (BoardList[16] + BoardList[17] + BoardList[18]);

            sums[5] = (byte) (BoardList[7] + BoardList[3] + BoardList[0]);
            sums[6] = (byte) (BoardList[12] + BoardList[8] + BoardList[4] + BoardList[1]);
            sums[7] = (byte) (BoardList[16] + BoardList[13] + BoardList[9] + BoardList[5] + BoardList[2]);
            sums[8] = (byte) (BoardList[17] + BoardList[14] + BoardList[10] + BoardList[6]);
            sums[9] = (byte) (BoardList[18] + BoardList[15] + BoardList[11]);

            sums[10] = (byte) (BoardList[2] + BoardList[6] + BoardList[11]);
            sums[11] = (byte) (BoardList[1] + BoardList[5] + BoardList[10] + BoardList[15]);
            sums[12] = (byte) (BoardList[0] + BoardList[4] + BoardList[9] + BoardList[14] + BoardList[18]);
            sums[13] = (byte) (BoardList[3] + BoardList[8] + BoardList[13] + BoardList[17]);
            sums[14] = (byte) (BoardList[7] + BoardList[12] + BoardList[16]);

            Sums = sums;
        }
        
        // Returns whether the board is final board.
        private void CalculateIsFinalBoard()
        {
            IsFinalBoard = !(Sums.Any(o => o != Sums[0]));
        }

        // Calculates the Height value of the Board.
        // Height can be between 0 and +1.
        private void CalculateHeight()
        {
            int sumsOk = Sums.Count(i => i == 38);

            var retVal = ((double)sumsOk / Sums.Length + GetFullness()) / 2;

            Height = retVal;
        }

        private double GetMeanError()
        {
            double sum = Sums.Aggregate<byte, double>(0, (current, b) => current + Math.Abs(b - 38));

            return sum / Sums.Length;
        }
        
        private double GetFullness()
        {
            return (38 - GetMeanError()) / 38;
        }

        // Returns the board's path from the beginning. This is required to display step-by-step progress in the UI.
        public List<Board> GetPath()
        {
            List<Board> retVal = new List<Board>();
            Board temp = this;

            while (temp != null)
            {
                retVal.Add(temp);
                temp = temp._parentBoard;
            }

            retVal.Reverse();

            return retVal;
        }

    }
}
