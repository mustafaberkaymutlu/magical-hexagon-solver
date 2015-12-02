﻿using System.Collections.Generic;

namespace YapayZekaOdevi2
{
    public class Row
    {
        public List<Board> RowElements { get; set; } = new List<Board>();

        public int StepNo { get; set; }

        public double Average
        {
            get
            {
                double retVal = 0;
                foreach (Board b in RowElements)
                {
                    retVal += b.Height;
                }

                retVal /= RowElements.Count;

                return retVal;
            }
        }

        public double Best
        {
            get
            {
                double retVal = double.MinValue;
                foreach (Board b in RowElements)
                {
                    if (b.Height > retVal)
                    {
                        retVal = b.Height;
                    }
                }

                return retVal;
            }
        }

        public string AverageString
        {
            get
            {
                return string.Format("{0:0.000}", Average);
            }
        }

        public string BestString {
            get
            {
                return string.Format("{0:0.000}", Best);
            }
        }
        
    }
}
