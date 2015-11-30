using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace YapayZekaOdevi2
{
    public class HillClimbing
    {
        //private List<String> processedBoards = new List<String>(); // List that keeps each of the processed Board's BoardCode. 
        
        public List<Row> FindLocalMaximum(BackgroundWorker worker, byte k)
        {
            bool found = false;
            Board[] currentBoards = GetRandomBoard(k);
            Board[] finalBoards = null;

            while (!found && !worker.CancellationPending)
            {
                for(byte i = 0; i < k; i++)
                {
                    if (currentBoards[i].IsFinalBoard())
                    {
                        found = true;
                        finalBoards = currentBoards;
                    }
                    else
                    {
                        currentBoards[i] = GetHighestNeighbor(currentBoards[i]);
                    }

                    Console.Out.Write(currentBoards[i].Height + " ");
                    
                }

                Console.Out.WriteLine();
                
            }

            if(found)
                return FormatFinalBoards(finalBoards, k);
            else
                return FormatFinalBoards(currentBoards, k);
        }

        private List<Row> FormatFinalBoards(Board[] currentBoards, byte k)
        {
            List<Board>[] finalBoardPaths = new List<Board>[k];
            List<Row> finalBoards = new List<Row>();

            for (byte i = 0; i < k; i++)
            {
                finalBoardPaths[i] = currentBoards[i].GetPath();
            }
            
            for (int i = 0; i < finalBoardPaths[0].Count; i++)
            {
                Row row = new Row();
                row.StepNo = i + 1;

                for (byte j = 0; j < k; j++)
                {
                    row.RowElements.Add(finalBoardPaths[j].ElementAt(i));
                }

                finalBoards.Add(row);
            }

            return finalBoards;
        }

        private Board GetHighestNeighbor(Board board)
        {
            List<Board> neighbors = GetNeighbors(board);

            double maxHeight = neighbors.Max(a => a.Height);
            Board highestNeighbor = neighbors.First(a => a.Height == maxHeight);

            return highestNeighbor;
        }


        private Board[] GetRandomBoard(byte howMany)
        {
            Board[] retVal = new Board[howMany];

            for (byte i = 0; i < howMany; i++)
            {
                byte[] temp = new byte[19] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19 };
                
                retVal[i] = new Board(temp.OrderBy(a => Guid.NewGuid()).ToArray(), null);
            }
            
            return retVal;
        }

        public List<Board> GetNeighbors(Board board)
        {
            List<Board> retVal = new List<Board>();
            byte i, j;

            for (i = 0; i < 19; i++)
            {
                for(j=(byte)(i+1); j < 19; j++)
                {
                    retVal.Add(Swap(board, i, j));
                }
            }

            return retVal;
        }

        private Board Swap(Board board, byte index1, byte index2)
        {
            byte[] temp1 = new byte[board.BoardList.Count()];
            Array.Copy(board.BoardList, temp1, board.BoardList.Count());

            Board board2 = new Board(temp1, board);

            byte temp = board2.BoardList[index1];
            board2.BoardList[index1] = board2.BoardList[index2];
            board2.BoardList[index2] = temp;

            return board2;
        }

    }
}
