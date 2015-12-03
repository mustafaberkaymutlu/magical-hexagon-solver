
namespace MagicalHexagonSolver
{
    public static class Config
    {
        // Points if the testing mode is on or off.
        public static readonly bool TEST_MODE = false;

        // The count of iteration when test mode is on.
        public static readonly int TEST_MODE_ITERATION_COUNT = 100;

        // This is the upper limit for the Linked List that holds processed boards.
        // GetHighestNeighbor(..) is removing neighbors that are also in this list.
        public static readonly ushort PROCESSED_BOARDS_UPPER_LIMIT = 10;

        // If MAXIMUM_ITERATION_COUNT is reached and still couldn't found any solution,
        // than operation stops with result of SolutionIsFound=false;
        public static readonly ushort MAXIMUM_ITERATION_COUNT = 1000;
    }
}
