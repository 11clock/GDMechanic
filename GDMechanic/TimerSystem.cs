using System.Collections.Generic;
using GDMechanic.Extensions;
using Godot;

namespace GDMechanic
{
	/// <summary>
	/// Node that organizes a collection of timers for easy access through delegates.
	/// </summary>
	/// <remarks>
	/// Designed to be used with <c>TimerSystemAttribute</c> and <c>TimerReceiverAttribute</c>,
	/// although it is fully functional on its own.
	/// </remarks>
	public class TimerSystem : Node {

		public delegate void TimeoutDelegate();

		public Node Parent { get; set; }
		
		/// <summary>
		/// Dictionary of timers that matches "timeout" method names with corresponding timers.
		/// </summary>
		public Dictionary<string, Timer> Timers { get; private set; }

		public override void _Ready() {
			Timers = new Dictionary<string, Timer>();
		}
		
		
		/// <summary>
		/// Generates a new child timer and adds it to the dictionary.
		/// </summary>
		/// <param name="timeoutMethod"></param>
		/// <param name="processMode"></param>
		/// <param name="waitTime"></param>
		/// <param name="oneShot"></param>
		/// <param name="autostart"></param>
		/// <returns>A reference to the generated Timer.</returns>
		public Timer AddTimer(TimeoutDelegate timeoutMethod,
			Timer.TimerProcessMode processMode = Timer.TimerProcessMode.Idle,
			float waitTime = 1f, bool oneShot = false, bool autostart = false)
		{
			string timeoutName = timeoutMethod.Method.Name;
			return AddTimer(timeoutName, processMode, waitTime, oneShot, autostart);
		}
		
		/// <summary>
		/// Generates a new child timer and adds it to the dictionary.
		/// </summary>
		/// <param name="timeoutName"></param>
		/// <param name="processMode"></param>
		/// <param name="waitTime"></param>
		/// <param name="oneShot"></param>
		/// <param name="autostart"></param>
		/// <returns>A reference to the generated Timer.</returns>
		public Timer AddTimer(string timeoutName,
			Timer.TimerProcessMode processMode = Timer.TimerProcessMode.Idle,
			float waitTime = 1f, bool oneShot = false, bool autostart = false)
		{
			Timer timer = this.CreateConnectedTimer(Parent, timeoutName, processMode, waitTime, oneShot, autostart);
			Timers.Add(timeoutName, timer);
			return timer;
		}
		
		/// <summary>
		/// Starts a timer. This also resets the remaining time to <c>waitTime</c>.
		/// </summary>
		/// <param name="timeoutMethod"></param>
		public void Start(TimeoutDelegate timeoutMethod) {
			string timeoutName = timeoutMethod.Method.Name;
			Timers[timeoutName].Start();
		}
		
		/// <summary>
		/// Starts a timer with a custom starting time.
		/// </summary>
		/// <param name="timeoutMethod"></param>
		/// <param name="time"></param>
		public void Start(TimeoutDelegate timeoutMethod, float time) {
			string timeoutName = timeoutMethod.Method.Name;
			TimerExtensions.Start(Timers[timeoutName], time);
		}
		
		/// <summary>
		/// Stops (cancels) a timer.
		/// </summary>
		/// <param name="timeoutMethod"></param>
		public void Stop(TimeoutDelegate timeoutMethod) {
			string timeoutName = timeoutMethod.Method.Name;
			Timers[timeoutName].Stop();
		}

		public bool GetAutostart(TimeoutDelegate timeoutMethod)
		{
			return Timers[timeoutMethod.Method.Name].Autostart;
		}
	
		public bool GetOneShot(TimeoutDelegate timeoutMethod)
		{
			return Timers[timeoutMethod.Method.Name].OneShot;
		}
	
		public bool GetPaused(TimeoutDelegate timeoutMethod)
		{
			return Timers[timeoutMethod.Method.Name].Paused;
		}
	
		public float GetTimeLeft(TimeoutDelegate timeoutMethod)
		{
			return Timers[timeoutMethod.Method.Name].TimeLeft;
		}

		public Timer.TimerProcessMode GetTimerProcessMode(TimeoutDelegate timeoutMethod)
		{
			return Timers[timeoutMethod.Method.Name].ProcessMode;
		}
	
		public float GetWaitTime(TimeoutDelegate timeoutMethod)
		{
			return Timers[timeoutMethod.Method.Name].WaitTime;
		}
	
		public void SetAutostart(TimeoutDelegate timeoutMethod, bool autostart)
		{
			Timers[timeoutMethod.Method.Name].Autostart = autostart;
		}
	
		public void SetOneShot(TimeoutDelegate timeoutMethod, bool oneShot)
		{
			Timers[timeoutMethod.Method.Name].OneShot = oneShot;
		}
	
		public void SetPaused(TimeoutDelegate timeoutMethod, bool paused)
		{
			Timers[timeoutMethod.Method.Name].Paused = paused;
		}

		public void SetTimerProcessMode(TimeoutDelegate timeoutMethod, Timer.TimerProcessMode processMode)
		{
			Timers[timeoutMethod.Method.Name].ProcessMode = processMode;
		}
	
		public void SetWaitTime(TimeoutDelegate timeoutMethod, float waitTime)
		{
			Timers[timeoutMethod.Method.Name].WaitTime = waitTime;
		}
	}
}