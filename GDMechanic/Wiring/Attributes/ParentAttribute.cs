using System;
using GDMechanic.Wiring.CachedNodeInfos;
using Godot;

namespace GDMechanic.Wiring.Attributes
{
	
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
	public class ParentAttribute : MechanicAttribute, IStateWirer
	{
		
		/// <summary>
		/// Returns a reference to the node's parent.
		/// </summary>
		public ParentAttribute()
		{
			
		}
		
		public void Wire(Node node, CachedNodeStateInfo state)
		{
			state.SetValue(node, node.GetParent());
		}
	}
}