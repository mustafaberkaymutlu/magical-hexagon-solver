using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace YapayZekaOdevi2
{
    public class HillClimbing
    {

        public Result FindLocalMaximum(BackgroundWorker worker, ushort k)
        {
            bool found = false;
            Board[] currentBoards = GetRandomBoard(k);
            Board[] finalBoards = null;
            List<String> processedBoards = new List<string>(); // List that keeps all processed Boards from previous iteration. 
            ushort foundKNumber = 0;

            while (!found && !worker.CancellationPending)
            {
                for(ushort i = 0; i < k; i++)
                {
                    if (currentBoards[i].IsFinalBoard)
                    {
                        found = true;
                        foundKNumber = (ushort) (i + 1);
                        finalBoards = currentBoards;
                    }
                    else
                    {
                        processedBoards.Add(currentBoards[i].BoardCode);
                        currentBoards[i] = GetHighestNeighbor(currentBoards[i], processedBoards);
                    }
                }
            }
            
            if (found)
            {
                List<Row> rows = FormatFinalBoards(finalBoards, k);
                return new Result(rows, !found, ((uint)rows.Count), foundKNumber);
            }
            else {
                List<Row> rows = FormatFinalBoards(currentBoards, k);
                return new Result(rows, !found, ((uint)rows.Count), foundKNumber);
            }
        }

        private List<Row> FormatFinalBoards(Board[] currentBoards, ushort k)
        {
            List<Board>[] finalBoardPaths = new List<Board>[k];
            List<Row> finalBoards = new List<Row>();

            for (ushort i = 0; i < k; i++)
            {
                finalBoardPaths[i] = currentBoards[i].GetPath();
            }

            foreach(List<Board> ff in finalBoardPaths)
            {
                if (!ff[ff.Count - 1].IsFinalBoard)
                {
                    ff.RemoveAt(ff.Count - 1);
                }
            }
            
            for (int i = 0; i < finalBoardPaths[0].Count; i++)
            {
                List<Board> temp = new List<Board>();

                for (ushort j = 0; j < k; j++)
                {
                    temp.Add(finalBoardPaths[j].ElementAt(i));
                }

                Row row = new Row(temp, i + 1);

                finalBoards.Add(row);
            }

            return finalBoards;
        }

        private Board GetHighestNeighbor(Board currentBoard, List<String> processedBoards)
        {
            bool found = false;
            double maxHeight;
            Board highestNeighbor;
            List<Board> neighbors = GetNeighbors(currentBoard);

            do
            {
                maxHeight = neighbors.Max(a => a.Height);
                highestNeighbor = neighbors.First(a => a.Height == maxHeight);

                if(processedBoards.Any(a => a.Equals(highestNeighbor.BoardCode)))
                    neighbors.Remove(highestNeighbor);
                else
                    found = true;

            } while (!found && neighbors.Count > 0);

            if (found)
                return highestNeighbor;
            else
                return null;
        }
        
        private Board[] GetRandomBoard(ushort howMany)
        {
            Board[] retVal = new Board[howMany];

            for (ushort i = 0; i < howMany; i++)
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
                    retVal.Add(GenerateBoard(board, i, j));
                }
            }

            return retVal;
        }

        private Board GenerateBoard(Board board, byte index1, byte index2)
        {
            byte[] temp1 = new byte[board.BoardList.Count()];
            Array.Copy(board.BoardList, temp1, board.BoardList.Count());
            
            byte temp = temp1[index1];
            temp1[index1] = temp1[index2];
            temp1[index2] = temp;

            return new Board(temp1, board);
        }

    }
}
