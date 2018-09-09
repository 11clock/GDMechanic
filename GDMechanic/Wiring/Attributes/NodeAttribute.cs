using System;
using GDMechanic.Wiring.CachedNodeInfos;
using Godot;

namespace GDMechanic.Wiring.Attributes
{
	
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
	public class NodeAttribute : MechanicAttribute, IStateWirer
	{
	
		private readonly string _path;
		
		/// <summary>
		/// Returns a reference to a node based on the provided node path.
		/// </summary>
		/// <param name="path"></param>
		public NodeAttribute(string path) {
			_path = path;
		}
	
		public void Wire(Node node, CachedNodeStateInfo state)
		{
			state.SetValue(node, node.GetNode(_path));
		}
	}
}