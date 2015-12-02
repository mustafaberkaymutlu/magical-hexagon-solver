using System.Collections.Generic;

namespace YapayZekaOdevi2
{
    public class Result
    {
        public bool solverIsCancelled;
        public bool solutionIsFound;
        public List<Row> rows;
        public uint foundIterationNumber;
        public ushort foundKNumber;

        public Result(List<Row> rows, bool solverIsCancelled, bool solutionNotFound, uint foundIterationNumber, ushort foundKNumber)
        {
            this.rows = rows;
            this.solverIsCancelled = solverIsCancelled;
            this.solutionIsFound = solutionNotFound;
            this.foundIterationNumber = foundIterationNumber;
            this.foundKNumber = foundKNumber;
        }

    }
}
