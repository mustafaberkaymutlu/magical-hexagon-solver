using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YapayZekaOdevi2
{
    public class Row
    {
        public List<Board> RowElements { get; set; } = new List<Board>();

        public int StepNo { get; set; }

        public double Average {
            get
            {
                double retVal = 0;
                foreach (Board b in RowElements)
                {
                    retVal += b.Height;
                }

                return retVal/RowElements.Count;
            }
        }

        public double Best {
            get
            {
                double retVal = double.MinValue;
                foreach(Board b in RowElements)
                {
                    if(b.Height > retVal)
                    {
                        retVal = b.Height;
                    }
                }

                return retVal;
            }
        }
        
    }
}
