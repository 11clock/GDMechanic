using System;
using System.Linq;
using GDMechanic.Extensions;
using GDMechanic.Wiring.CachedNodeInfos;
using Godot;

namespace GDMechanic.Wiring.Attributes
{
	
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
	public class TimerSystemAttribute : MechanicAttribute, IStateWirer
	{
		
		/// <summary>
		/// Adds a child <c>TimerSystem</c> node, and assigns a reference to it to the field.
		/// Also generates timers based on embedded <c>TimerReceiverAttribute</c>s in the node.
		/// </summary>
		/// <remarks>
		/// Does not work with <c>TimerAttribute</c>. Use <c>TimerReceiverAttribute</c> instead for <c>TimerSystem</c> compatibility.
		/// </remarks>
		/// <remarks>
		/// <c>TimerAttribute</c> is meant for individual timer references not related to a <c>TimerSystem</c>.
		/// </remarks>
		public TimerSystemAttribute()
		{
			
		}
		
		public void Wire(Node node, CachedNodeStateInfo state)
		{
			TimerSystem timerSystem = node.AddAndReturnChild(new TimerSystem());
			timerSystem.Name = Utils.NormalizeMemberName(state.Name);
			timerSystem.Parent = node;
		
			// Field-Defined TimerReceivers
			foreach (TimerReceiverAttribute attribute in state.GetAttributes().OfType<TimerReceiverAttribute>())
			{
				timerSystem.AddTimer(attribute.TimeoutMethod, attribute.ProcessMode, attribute.WaitTime, attribute.OneShot,
					attribute.Autostart);
			}
		
			// Method-Defined TimerReceivers
			CachedNodeMethodInfo[] compatibleMethods = state.DeclaringType.GetMethods().Where(m => m.GetAttribute<TimerReceiverAttribute>() != null).ToArray();
			foreach (CachedNodeMethodInfo method in compatibleMethods)
			{
				TimerReceiverAttribute attribute = method.GetAttribute<TimerReceiverAttribute>();
				timerSystem.AddTimer(method.Name, attribute.ProcessMode, attribute.WaitTime, attribute.OneShot, attribute.Autostart);
			}
		
			state.SetValue(node, timerSystem);
		}
	}
}