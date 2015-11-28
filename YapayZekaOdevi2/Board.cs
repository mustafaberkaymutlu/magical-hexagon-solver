using System;
using System.Collections.Generic;
using System.Linq;

namespace YapayZekaOdevi2
{
    public class Board
    {
        public byte[] BoardList { get; set; }
        public byte[] Sums;
        public double Height;
        private Board ParentBoard; // This is used to keep board's path. So that we can track back to the first board status.

        public Board(byte[] boardList, Board parent)
        {
            this.BoardList = boardList;
            this.ParentBoard = parent;
            CalculateSums();
            CalculateHeight();
        }

        public String BoardCode // Unique board code (e.g "1*2*3*4*5*6*7*8*9*10*11*12*13*14*15*16*17*18*19"). This is used to hold processed boards in a seperate List<String>.
        {
            get
            {
                return String.Join("*", BoardList);
            }
        }

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


            //sums[0] = Sum(new List<byte> { 0, 1, 2 });
            //sums[1] = Sum(new List<byte> { 3, 4, 5, 6 });
            //sums[2] = Sum(new List<byte> { 7, 8, 9, 10, 11});
            //sums[3] = Sum(new List<byte> { 12, 13, 14, 15 });
            //sums[4] = Sum(new List<byte> { 16, 17, 18 });

            //sums[5] = Sum(new List<byte> { 7, 3, 0 });
            //sums[6] = Sum(new List<byte> { 12, 8, 4, 1 });
            //sums[7] = Sum(new List<byte> { 16, 13, 9, 5, 2 });
            //sums[8] = Sum(new List<byte> { 17, 14, 10, 6 });
            //sums[9] = Sum(new List<byte> { 18, 15, 11 });

            //sums[10] = Sum(new List<byte> { 2, 6, 11 });
            //sums[11] = Sum(new List<byte> { 1, 5, 10, 15 });
            //sums[12] = Sum(new List<byte> { 0, 4, 9, 14, 18 });
            //sums[13] = Sum(new List<byte> { 3, 8, 13, 17 });
            //sums[14] = Sum(new List<byte> { 7, 12, 16 });

            this.Sums = sums;
        }

        //private byte Sum(List<byte> order)
        //{
        //    byte temp = 0;

        //    foreach(byte b in order)
        //    {
        //        temp += BoardList[b];
        //    }

        //    return temp;
        //}


        // Returns whether the board is final board.
        public bool IsFinalBoard()
        {
            if (Sums.Any(o => o != Sums[0])) return false;
            else return true;
        }

        private void CalculateHeight()
        {
            double retVal = 1;

            foreach (byte b in BoardList)
            {
                retVal *= ((double)b) / 38;
            }

            //foreach(byte b in Sums)
            //{
            //    if(b == 38)
            //    {
            //        retVal++;
            //    }
            //}

            Height = retVal;
        }

        // Returns the board's path from the beginning. This is required to display step-by-step progress in the UI.
        public List<Board> GetPath()
        {
            List<Board> retVal = new List<Board>();
            Board temp = this;

            while (temp != null)
            {
                retVal.Add(temp);
                temp = temp.ParentBoard;
            }

            retVal.Reverse();

            return retVal;
        }

    }
}
