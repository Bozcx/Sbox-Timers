using System;
using System.Collections.Generic;

namespace TimerSystem;

// TODO: Clear timers once they are done ( Probably make it a bool )
public static class Timer
{
	private static readonly Dictionary<string, TimerObject> Timers = new ();

	/**
	 * Creates a timer with the given ID.
	 * <param name="id">Id of the timer</param>
	 * <param name="delay">Delay in seconds</param>
	 * <param name="rep">Number of repetitions</param>
	 * <param name="func">Function to run</param>
	 */
	 public static void Create( string id, float delay, int rep, Action func )
	{
		if ( Timers.ContainsKey( id ) )
		{
			Log.Warning($"Timer with ID {id} already exists!");
			return;
		}

		var timer = new TimerObject( id, delay, rep, func );
		Timers.Add( id, timer );
		_ = timer.Run();
	}
	/**
	 * Creates a simple timer that runs once after a delay.
	 * <param name="delay">Delay in seconds</param>
	 * <param name="func">Function to run</param>
	 */
	public static void Simple( float delay, Action func )
	{
		var id = Guid.NewGuid().ToString();
		Create( id, delay, 1, func );
	}

	/**
	 * Pauses a timer with the given ID.
	 * <param name="id">Id of the timer to Pause</param>
	 */
	public static void Pause( string id )
	{
		if ( Timers.TryGetValue( id, out var timer ) )
		{
			timer.IsPaused = true;
		}
	}
	
	/**
	 * Resumes a timer with the given ID.
	 * If the timer is not paused, it will do nothing.
	 * <param name="id">Id of the timer to Resume</param>
	 */
	public static void Resume( string id )
	{
		if ( !Timers.TryGetValue( id, out var timer ) )
		{
			return;
		}

		timer.IsPaused = false;
		_ = timer.Run();
	}
	
	/**
	 * Removes a timer with the given ID.
	 * <param name="id">Id of the timer to Remove</param>
	 */
	public static void Remove( string id )
	{
		if ( !Timers.TryGetValue( id, out var timer ) )
		{
			return;
		}

		timer.IsPaused = true;
		Timers.Remove( id );
	}

	/**
	 * Clears all timers.
	 */
	public static void Clear()
	{
		foreach ( var timer in Timers )
		{
			timer.Value.IsPaused = true;
		}
		Timers.Clear();
	}
	
	/**
	 * Checks if a timer with the given ID exists.
	 * <param name="id">Id of the timer to check</param>
	 */
	public static bool Exist( string id )
	{
		return Timers.ContainsKey( id );
	}
	
	/**
	 * Returns the time left for a timer with the given ID.
	 * <param name="id">Id of the timer to check</param>
	 */
	public static void TimeLeft( string id )
	{
		if ( !Timers.TryGetValue( id, out var timer ) )
		{
			Log.Warning($"Timer with ID {id} does not exist!");
			return;
		}

		Log.Info($"Time left for timer {id}: {timer.TimeRemaining}");
	}
	
	/**
	 * Returns the repetitions left for a timer with the given ID.
	 * <param name="id">Id of the timer to check</param>
	 */
	public static void RepsLeft( string id )
	{
		if ( !Timers.TryGetValue( id, out var timer ) )
		{
			Log.Warning($"Timer with ID {id} does not exist!");
			return;
		}

		Log.Info($"Repetitions left for timer {id}: {timer.Repeat}");
	}
}
