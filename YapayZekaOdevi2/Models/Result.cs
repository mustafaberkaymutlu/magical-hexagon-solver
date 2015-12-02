using System.Collections.Generic;

namespace YapayZekaOdevi2
{
    public class Result
    {
        public bool isCancelled;
        public List<Row> rows;
        public uint foundIterationNumber;
        public ushort foundKNumber;

        public Result(List<Row> rows, bool isCancelled, uint foundIterationNumber, ushort foundKNumber)
        {
            this.rows = rows;
            this.isCancelled = isCancelled;
            this.foundIterationNumber = foundIterationNumber;
            this.foundKNumber = foundKNumber;
        }

    }
}
