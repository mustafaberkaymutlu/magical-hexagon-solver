using System.Collections.Generic;

namespace YapayZekaOdevi2
{
    public class Row
    {
        public List<Board> RowElements { get; }

        public int StepNo { get; }

        public double Average;

        public double Best;

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

        public Row(List<Board> RowElements, int StepNo)
        {
            this.RowElements = RowElements;
            this.StepNo = StepNo;

            CalculateAverage();
            CalculateBest();
        }

        private void CalculateAverage()
        {
            double average = 0;
            foreach (Board b in RowElements)
            {
                average += b.Height;
            }

            average /= RowElements.Count;

            this.Average = average;
        }

        private void CalculateBest()
        {
            double best = double.MinValue;
            foreach (Board b in RowElements)
            {
                if (b.Height > best)
                {
                    best = b.Height;
                }
            }

            this.Best = best;
        }
        
    }
}
