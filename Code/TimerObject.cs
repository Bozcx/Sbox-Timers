using System;
using System.Threading.Tasks;

namespace TimerSystem;
public class TimerObject
{
	public string Id { get; set; }
	private float Delay { get; set; }
	public int Repeat { get; set; }

	private Action Func { get; set; }
	public bool IsPaused  { get; set; }
	public float TimeRemaining { get; set; }

	public TimerObject( string id, float delay, int rep, Action func )
	{
		Id = id;
		Delay = delay;
		Repeat = rep;
		Func = func;
		IsPaused = false;
		TimeRemaining = delay;
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
	}
}
