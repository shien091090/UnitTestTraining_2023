public class TimerTextConverter
{
    public string ConvertSecondsToMinutes(int seconds)
    {
        if (seconds <= 0)
            return "00:00";
        else if (seconds > 3599)
            return "59:59+";

        int minutes = seconds / 60;
        int second = seconds % 60;
        return $"{minutes:00}:{second:00}";
    }
}