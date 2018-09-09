using System;
using Godot;

namespace GDMechanic.Wiring.Attributes
{
	
	[AttributeUsage( AttributeTargets.Field | AttributeTargets.Method, AllowMultiple = true)]
	public class TimerReceiverAttribute : MechanicAttribute
	{
		public Timer.TimerProcessMode ProcessMode { get; }
		public float WaitTime { get; }
		public bool OneShot { get; }
		public bool Autostart { get; }

		public string TimeoutMethod { get; }
		
		/// <summary>
		/// Specifies a method to be connected to a <c>timer</c> in the node's <c>TimerSystem</c>.
		/// </summary>
		/// <remarks>
		/// Can be embedded with either the <c>TimerSystem</c> field itself with the specified timeout method, or directly with the timeout method itself.
		/// </remarks>
		/// <param name="processMode"></param>
		/// <param name="waitTime"></param>
		/// <param name="oneShot"></param>
		/// <param name="autostart"></param>
		public TimerReceiverAttribute(Timer.TimerProcessMode processMode = Timer.TimerProcessMode.Idle, float waitTime = 1f, bool oneShot = false, bool autostart = false)
		{
			ProcessMode = processMode;
			WaitTime = waitTime;
			OneShot = oneShot;
			Autostart = autostart;
		}
		
		/// <inheritdoc />
		/// <param name="timeoutMethod"></param>
		/// <param name="processMode"></param>
		/// <param name="waitTime"></param>
		/// <param name="oneShot"></param>
		/// <param name="autostart"></param>
		public TimerReceiverAttribute(string timeoutMethod,
			Timer.TimerProcessMode processMode = Timer.TimerProcessMode.Idle, float waitTime = 1f, bool oneShot = false, bool autostart = false)
			: this(processMode, waitTime, oneShot, autostart)
		{
			TimeoutMethod = timeoutMethod;
		}
	}
}
