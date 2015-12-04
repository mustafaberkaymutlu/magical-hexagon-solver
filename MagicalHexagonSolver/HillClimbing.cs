using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using MagicalHexagonSolver.Models;

namespace MagicalHexagonSolver
{
    public class HillClimbing
    {
        // Parallel Hill Climbing algorithm for finding the maximum of number placement problem. 
        public Result FindMaximum(BackgroundWorker worker, ushort k)
        {
            bool solutionIsFound = false;
            bool quit = false;
            Board[] currentBoards = GetRandomBoard(k);
            Board[] foundBoards = null;
            LinkedList<Board>[] processedBoards = new LinkedList<Board>[k];
            ushort foundKNumber = 0;
            ushort iterationCount = 0;

            for(int i = 0; i < k; i++)
            {
                processedBoards[i] = new LinkedList<Board>();
            }

            while (!quit && !worker.CancellationPending)
            {
                for(ushort i = 0; i < k; i++) // k different paths (or robots). Each of them are processing seperately
                {
                    if (currentBoards[i].IsFinalBoard) // if this is the final board then stop
                    {
                        quit = true;
                        solutionIsFound = true;
                        foundKNumber = (ushort) (i + 1);
                        foundBoards = currentBoards;
                    }
                    else // if this not the board we are looking for, 
                    {    //  then select the highest neighbor and continue iteration
                        processedBoards[i].AddLast(currentBoards[i]);
                        currentBoards[i] = GetHighestNeighbor(currentBoards[i], processedBoards[i]);
                    }

                    // If the Linked List's size is higher than it should be, then remove the oldest (first) item
                    if (processedBoards[i].Count > Config.PROCESSED_BOARDS_UPPER_LIMIT)
                    {
                        processedBoards[i].RemoveFirst();
                    }
                }

                iterationCount++;

                // If the iteration count is higher then maximum count, then stop with result SolutionIsFound = false
                if (iterationCount > Config.MAXIMUM_ITERATION_COUNT)
                {
                    quit = true;
                    solutionIsFound = false;
                }
            }
            
            if (solutionIsFound)
            {
                List<Row> rows = FormatFinalBoards(foundBoards, k);
                return new Result(rows, worker.CancellationPending, true, ((uint)rows.Count), foundKNumber);
            }
            else {
                List<Row> rows = FormatFinalBoards(currentBoards, k);
                return new Result(rows, worker.CancellationPending, false, ((uint)rows.Count), foundKNumber);
            }
        }

        // Formats the boards as Row objects to be displayed in the UI.
        private List<Row> FormatFinalBoards(Board[] currentBoards, ushort k)
        {
            List<Board>[] finalBoardPaths = new List<Board>[k];
            List<Row> finalBoards = new List<Row>();

            // Get all paths of the final boards.
            for (ushort i = 0; i < k; i++)
            {
                finalBoardPaths[i] = currentBoards[i].GetPath();
            }

            // If this is not the solution board then remove the last elements of from that list.
            // We are removing the last elements because we want to stop the iteration when the final board is found.
            foreach (List<Board> ff in finalBoardPaths.Where(ff => !ff[ff.Count - 1].IsFinalBoard))
            {
                ff.RemoveAt(ff.Count - 1);
            }
            
            // Create Row objects and set the items.
            // Row objects represents the each row of the GUI.
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

        // Creates all possible neighbors of a board, then removes the boards previously processed
        // and lastly selects and returns the higher neighbor. 
        private Board GetHighestNeighbor(Board currentBoard, LinkedList<Board> processedBoards)
        {
            bool found = false;
            double maxHeight;
            Board highestNeighbor;
            List<Board> neighbors = GetNeighbors(currentBoard);
            
            do // Do this until we finally found a neigbor board.
            {
                maxHeight = neighbors.Max(a => a.Height); // Find the maximum value of Height in neighbors list.
                highestNeighbor = neighbors.First(a => a.Height == maxHeight); // Get the first occurence with the maximum Height.

                // If we already processed this heighestNeighbor, then remove it from the neighbors list and find 
                // a new highestNeighbor!
                if (processedBoards.Any(j => j.BoardList.SequenceEqual(highestNeighbor.BoardList)))
                    neighbors.Remove(highestNeighbor);
                else
                    found = true;

            } while (!found && neighbors.Count > 0);
            
            return highestNeighbor;
        }
        
        // Creates random boards.
        private Board[] GetRandomBoard(ushort howMany)
        {
            Board[] retVal = new Board[howMany];

            for (ushort i = 0; i < howMany; i++)
            {
                byte[] temp = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19 };

                // Shuffle the array randomly and generate a Board object from shuffled array.
                retVal[i] = new Board(temp.OrderBy(a => Guid.NewGuid()).ToArray(), null); 
            }
            
            return retVal;
        }

        // Generates all neighbors of a board.
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

        // Generates a board from a parent board. Replaces the items in the parent board.
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
