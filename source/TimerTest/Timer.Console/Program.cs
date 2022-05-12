var autoEvent = new AutoResetEvent(false);
var checker = new Checker(10);

Console.WriteLine($"{DateTime.Now:h:mm:ss.ff} Creating timer. \n");

Timer? timer = new(checker.Check,
										autoEvent,
										TimeSpan.FromSeconds(1),
										TimeSpan.FromSeconds(0.25));


// When autoEvent signals the second time, dispose of the timer.
//autoEvent.WaitOne();
timer.Dispose();
Console.WriteLine("\nDestroying timer.");

class Checker
{
	private int invokeCount;
	private int maxCount;

	public Checker(int count)
	{
		invokeCount = 0;
		maxCount = count;
	}

	public void Check(Object stateInfo)
	{
		AutoResetEvent autoEvent = (AutoResetEvent)stateInfo;
		Console.WriteLine($"{DateTime.Now:h:mm:ss.ff} Checking status {++invokeCount}");

		if (invokeCount == maxCount)
		{
			invokeCount = 0;
			//autoEvent.Set();
		}
	}
}