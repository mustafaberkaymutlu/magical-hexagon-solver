using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YapayZekaOdevi2
{
    public class Result
    {
        public bool isCancelled;
        public List<Row> rows;

        public Result(List<Row> rows, bool isCancelled)
        {
            this.rows = rows;
            this.isCancelled = isCancelled;
        }

    }
}
