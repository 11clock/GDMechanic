using System;
using GDMechanic.Extensions;
using GDMechanic.Wiring.CachedNodeInfos;
using Godot;

namespace GDMechanic.Wiring.Attributes
{
	
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
	public class TimerAttribute : MechanicAttribute, IStateWirer
	{
		private readonly string _timeoutMethod;

		private readonly Timer.TimerProcessMode _processMode;
		private readonly float _waitTime;
		private readonly bool _oneShot;
		private readonly bool _autostart;
	
		/// <summary>
		/// Adds a child <c>Timer</c> node with the specified criteria, and assigns a reference to it to the field.
		/// </summary>
		/// <param name="timeoutMethod"></param>
		/// <param name="processMode"></param>
		/// <param name="waitTime"></param>
		/// <param name="oneShot"></param>
		/// <param name="autostart"></param>
		public TimerAttribute(string timeoutMethod,
			Timer.TimerProcessMode processMode = Timer.TimerProcessMode.Idle, float waitTime = 1f, bool oneShot = false, bool autostart = false)
		{
			_timeoutMethod = timeoutMethod;
		
			_processMode = processMode;
			_waitTime = waitTime;
			_oneShot = oneShot;
			_autostart = autostart;
		}
	
		public void Wire(Node node, CachedNodeStateInfo state)
		{
			Timer timer = node.CreateConnectedTimer(_timeoutMethod, _processMode, _waitTime, _oneShot, _autostart);
			timer.Name = Utils.NormalizeMemberName(state.Name);
			state.SetValue(node, timer);
		}
	}
}