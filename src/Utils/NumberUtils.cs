

namespace ConsoleFileManager.Utils;

public static class NumberUtils
{
    public static int ClampValue(int value, int min, int max)
    {
        if (value <= min) return int.MinValue;
        if (value > max) return max;
        return value;
    }
}
