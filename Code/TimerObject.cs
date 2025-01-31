using System;
using System.Threading.Tasks;

namespace TimerSystem;
public class TimerObject
{
	public string Id { get; set; }
	private float Delay { get; set; }
	public int Repeat { get; private set; }
	private Action Func { get; set; }
	public bool IsPaused  { get; set; }
	public float TimeRemaining { get; private set; }
	private Action<string> OnComplete { get; set; }

	public TimerObject( string id, float delay, int rep, Action func, Action<string> onComplete)
	{
		Id = id;
		Delay = delay;
		Repeat = rep;
		Func = func;
		IsPaused = false;
		TimeRemaining = delay;
		OnComplete = onComplete;
	}

	public async Task Run()
	{
		while ( Repeat != 0 && !IsPaused )
		{
			await Task.Delay((int)(TimeRemaining * 1000));
			if ( IsPaused )
			{
				continue;
			}

			Func?.Invoke();
			if (Repeat > 0) Repeat--;
			TimeRemaining = Delay;
		}
		
		OnComplete?.Invoke(Id);
	}
}
