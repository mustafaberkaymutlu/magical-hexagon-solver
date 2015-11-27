using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace YapayZekaOdevi2
{
    public class HillClimbing
    {
        //private List<String> processedBoards = new List<String>(); // List that keeps each of the processed Board's BoardCode. 
        
        public List<List<Board>> FindLocalMaximum(BackgroundWorker worker, byte k)
        {
            bool found = false;
            //Board finalBoard = null;
            Board[] currentBoards = GetRandomBoard(k);
            List<Board>[] neighbors = new List<Board>[k];
            double[] nextHeights = new double[k];
            Board[] nextBoards = new Board[k];

            while (!found && !worker.CancellationPending)
            {
                for(byte i = 0; i < k; i++)
                {
                    neighbors[i] = GetNeighbors(currentBoards[i]);
                    nextHeights[i] = double.MinValue;
                    nextBoards[i] = null;

                    foreach (Board b in neighbors[i])
                    {
                        if (b.Height > nextHeights[i])
                        {
                            nextBoards[i] = b;
                            nextHeights[i] = b.Height;
                        }
                    }

                    if (nextHeights[i] <= currentBoards[i].Height)
                    {
                        if (currentBoards[i].IsFinalBoard())
                        {
                            found = true;
                            //finalBoard = currentBoards[i];
                        }
                    }

                    
                    currentBoards[i] = nextBoards[i];
                }
                
            }

            

            List<Board>[] finalBoardPaths = new List<Board>[k];
            for (byte i=0;i< k; i++)
            {
                finalBoardPaths[i] = currentBoards[i].GetPath();
            }

            List<List<Board>> finalBoards = new List<List<Board>>();

            for(int i = 0; i < finalBoardPaths[0].Count; i++)
            {
                List<Board> row = new List<Board>();

                for(byte j=0;j< k; j++)
                {
                    row.Add(finalBoardPaths[j].ElementAt(i));
                }

                finalBoards.Add(row);
            }

            return finalBoards;
        }


        private Board[] GetRandomBoard(byte howMany)
        {
            Board[] retVal = new Board[howMany];

            for (byte i = 0; i < howMany; i++)
            {
                byte[] temp = new byte[19] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19 };

                retVal[i] = new Board(RandomPermutation(temp).ToArray(), null);
            }
            
            return retVal;
        }

        // Shuffles any suquence.
        public static IEnumerable<T> RandomPermutation<T>(IEnumerable<T> sequence)
        {
            Random random = new Random();
            T[] retArray = sequence.ToArray();


            for (int i = 0; i < retArray.Length - 1; i += 1)
            {
                int swapIndex = random.Next(i, retArray.Length);
                if (swapIndex != i)
                {
                    T temp = retArray[i];
                    retArray[i] = retArray[swapIndex];
                    retArray[swapIndex] = temp;
                }
            }

            return retArray;
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
            Board board2 = new Board(board.BoardList, board);
            byte temp = board2.BoardList[index1];
            board2.BoardList[index1] = board2.BoardList[index2];
            board2.BoardList[index2] = temp;

            return board2;
        }

    }
}
