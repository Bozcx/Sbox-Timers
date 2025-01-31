using System;
using System.Collections.Generic;

namespace TimerSystem;

public static class Timers
{
	private static readonly Dictionary<string, TimerObject> _Timers = new ();

	/**
	 * Creates a timer with the given ID.
	 * <param name="id">Id of the timer</param>
	 * <param name="delay">Delay in seconds</param>
	 * <param name="rep">Number of repetitions</param>
	 * <param name="func">Function to run</param>
	 */
	 public static void Create( string id, float delay, int rep, Action func)
	{
		if ( _Timers.ContainsKey( id ) )
		{
			Log.Warning($"Timer with ID {id} already exists!");
			return;
		}

		var timer = new TimerObject( id, delay, rep, func, Remove);
		_Timers.Add( id, timer );
		_ = timer.Run();
	}
	/**
	 * Creates a simple timer that runs once after a delay.
	 * <param name="delay">Delay in seconds</param>
	 * <param name="func">Function to run</param>
	 */
	public static void Simple( float delay, Action func)
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
		if ( _Timers.TryGetValue( id, out var timer ) )
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
		if ( !_Timers.TryGetValue( id, out var timer ) )
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
		if ( !_Timers.TryGetValue( id, out var timer ) )
		{ 
			Log.Info($"Could not find timer with ID {id}");
		}
		
		Log.Info($"Removing timer with ID {id}");
		_Timers.Remove( id );
	}
	
	
	/**
	 * Clears all timers.
	 */
	public static void Clear()
	{
		foreach ( var timer in _Timers )
		{
			timer.Value.IsPaused = true;
		}
		_Timers.Clear();
	}
	
	/**
	 * Checks if a timer with the given ID exists.
	 * <param name="id">Id of the timer to check</param>
	 */
	public static bool Exist( string id )
	{
		return _Timers.ContainsKey( id );
	}
	
	/**
	 * Returns the time left for a timer with the given ID.
	 * <param name="id">Id of the timer to check</param>
	 */
	public static void TimeLeft( string id )
	{
		if ( !_Timers.TryGetValue( id, out var timer ) )
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
		if ( !_Timers.TryGetValue( id, out var timer ) )
		{
			Log.Warning($"Timer with ID {id} does not exist!");
			return;
		}

		Log.Info($"Repetitions left for timer {id}: {timer.Repeat}");
	}
}
