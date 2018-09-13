using GDMechanic.Extensions;
using GDMechanic.Wiring.CachedNodeInfos;
using Godot;

namespace GDMechanic.Wiring.Attributes
{
	public class TreeNodeAttribute : MechanicAttribute, IStateWirer
	{
		
		/// <summary>
		/// Fetches the first node in the tree that matches the field/property's type.
		/// </summary>
		public TreeNodeAttribute()
		{
			
		}
		
		public void Wire(Node node, CachedNodeStateInfo state)
		{
			state.SetValue(node, node.GetTree().GetNodeOfType(state.StateType));
		}
	}
}