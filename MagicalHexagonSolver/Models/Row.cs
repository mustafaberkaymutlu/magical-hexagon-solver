using System.Collections.Generic;
using System.Linq;

namespace MagicalHexagonSolver.Models
{
    public class Row
    {
        public List<Board> RowElements { get; }

        public int StepNo { get; }

        public double Average;

        public double Best;

        public string AverageString => $"{Average:0.000}";

        public string BestString => $"{Best:0.000}";

        public Row(List<Board> rowElements, int stepNo)
        {
            RowElements = rowElements;
            StepNo = stepNo;

            CalculateAverage();
            CalculateBest();
        }

        private void CalculateAverage()
        {
            double average = RowElements.Sum(b => b.Height);

            average /= RowElements.Count;

            Average = average;
        }

        private void CalculateBest()
        {
            Best = RowElements.Select(b => b.Height).Concat(new[] { double.MinValue }).Max();
        }
        
    }
}
