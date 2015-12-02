
namespace YapayZekaOdevi2
{
    public static class Config
    {
        public static readonly int TEST_MEASUREMENT_COUNT = 100;
        public static readonly bool TEST_MEASUREMENT_ACTIVE = false;

        // If you chose Config.SETTING_DO_NOT_TURN_BACK_UNTIL_ITERATION_COUNT higher than the count of 
        // all neighbors a board can have, than GetHighestNeighbor(..) may fail.
        public static readonly ushort SETTING_DO_NOT_TURN_BACK_UNTIL_ITERATION_COUNT = 10;

        public static readonly ushort SETTING_QUIT_WHEN_ITERATION_COUNT = 1000;
    }
}
