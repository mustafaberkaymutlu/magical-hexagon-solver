using System.Collections.Generic;

namespace MagicalHexagonSolver.Models
{
    public class Result
    {
        public bool SolverIsCancelled;
        public bool SolutionIsFound;
        public List<Row> Rows;
        public uint FoundIterationNumber;
        public ushort FoundKNumber;

        public Result(List<Row> rows, bool solverIsCancelled, bool solutionNotFound, uint foundIterationNumber, ushort foundKNumber)
        {
            Rows = rows;
            SolverIsCancelled = solverIsCancelled;
            SolutionIsFound = solutionNotFound;
            FoundIterationNumber = foundIterationNumber;
            FoundKNumber = foundKNumber;
        }

    }
}
