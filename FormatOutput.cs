namespace Scoreboard
{
    public static class FormatOutput
    {
        public static string FormatGameTime(TimeSpan time)
        {
            if (time.TotalMinutes >= 1)
            {
                if (time.TotalMinutes <= 9)
                {
                    return $"{time:m\\:ss}";
                }
                return $"{time:mm\\:ss}";
            }
            else
            {
                return $"{time:ss\\.f}";
            }
        }
        public static string FormatPenaltyTime(TimeSpan time)
        {
            if (time.TotalMinutes >= 1)
            {
                return $"{time:mm\\:ss}";
            }
            else
            {
                return $"{time:ss\\.f}";
            }
        }


    }
}