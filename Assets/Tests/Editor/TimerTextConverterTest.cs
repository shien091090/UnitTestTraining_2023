using NUnit.Framework;

public class TimerTextConverterTest
{
    private TimerTextConverter timerTextConverter;

    [SetUp]
    public void Setup()
    {
        timerTextConverter = new TimerTextConverter();
    }

    [Test]
    //輸入小於0, 輸出"00:00"
    public void output_00_00_when_smaller_then_0()
    {
        Assert.AreEqual("00:00", timerTextConverter.ConvertSecondsToMinutes(-1));
    }

    [Test]
    [TestCase(0, "00:00")]
    [TestCase(59, "00:59")]
    [TestCase(30, "00:30")]
    //小於59秒時, 分鐘顯示00, 其餘秒數照常顯示
    public void output_00_minutes_and_whole_seconds_when_smaller_then_59(int inputSeconds, string expectedResult)
    {
        string result = timerTextConverter.ConvertSecondsToMinutes(inputSeconds);
        Assert.AreEqual(expectedResult, result);
    }

    [Test]
    [TestCase(65, "01:05")]
    [TestCase(250, "04:10")]
    //超過60秒時, 多於秒數轉換為分鐘
    public void output_minutes_and_remain_seconds_when_bigger_then_60(int inputSeconds, string expectedResult)
    {
        string result = timerTextConverter.ConvertSecondsToMinutes(inputSeconds);
        Assert.AreEqual(expectedResult, result);
    }

    [Test]
    //輸入超過3599, 輸出"59:59+"
    public void output_59_59_plus_when_bigger_then_3599()
    {
        string result = timerTextConverter.ConvertSecondsToMinutes(36000);
        Assert.AreEqual("59:59+", result);
    }
}