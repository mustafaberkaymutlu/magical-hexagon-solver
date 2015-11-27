﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace YapayZekaOdevi2
{
    public class Board
    {
        public byte[] BoardList { get; set; }
        public byte[] Sums;
        public double Height;

        public Board(byte[] boardList)
        {
            this.BoardList = boardList;
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

            sums[0] = Sum(new List<byte> { 0, 1, 2 });
            sums[1] = Sum(new List<byte> { 3, 4, 5, 6 });
            sums[2] = Sum(new List<byte> { 7, 8, 9, 10, 11});
            sums[3] = Sum(new List<byte> { 12, 13, 14, 15 });
            sums[4] = Sum(new List<byte> { 16, 17, 18 });

            sums[5] = Sum(new List<byte> { 7, 3, 0 });
            sums[6] = Sum(new List<byte> { 12, 8, 4, 1 });
            sums[7] = Sum(new List<byte> { 16, 13, 9, 5, 2 });
            sums[8] = Sum(new List<byte> { 17, 14, 10, 6 });
            sums[9] = Sum(new List<byte> { 18, 15, 11 });

            sums[10] = Sum(new List<byte> { 2, 6, 11 });
            sums[11] = Sum(new List<byte> { 1, 5, 10, 15 });
            sums[12] = Sum(new List<byte> { 0, 4, 9, 14, 18 });
            sums[13] = Sum(new List<byte> { 3, 8, 13, 17 });
            sums[14] = Sum(new List<byte> { 7, 12, 16 });

            this.Sums = sums;
        }

        private byte Sum(List<byte> order)
        {
            byte temp = 0;

            foreach(byte b in order)
            {
                temp += BoardList[b];
            }

            return temp;
        }


        // Returns whether the board is final board.
        public bool IsFinalBoard()
        {
            if (Sums.Any(o => o != Sums[0])) return true;
            else return false;

        }

        private void CalculateHeight()
        {
            double retVal = 1;
            foreach (byte b in BoardList)
            {
                retVal *= ((double) b) / 38;
            }

            //foreach (byte b in Sums)
            //{
            //    retVal += b;
            //}

            Height = retVal;
        }


    }
}
