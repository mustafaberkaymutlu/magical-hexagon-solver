
namespace YapayZekaOdevi2
{
    public static class Config
    {
        // Points if the testing mode is on or off.
        public static readonly bool TEST_MODE = false;

        // The count of iteration when test mode is on.
        public static readonly int TEST_MODE_ITERATION_COUNT = 100;

        // If you chose Config.PROCESSED_BOARDS_UPPER_LIMIT higher than the count of 
        // all neighbors a board can have, than GetHighestNeighbor(..) may fail.
        public static readonly ushort PROCESSED_BOARDS_UPPER_LIMIT = 10;

        // If MAXIMUM_ITERATION_COUNT is reached and still couldn't found any solution,
        // than operation stops with result of solutionIsFound=false;
        public static readonly ushort MAXIMUM_ITERATION_COUNT = 1000;
    }
}
