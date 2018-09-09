using System;
using GDMechanic.Wiring.CachedNodeInfos;
using Godot;

namespace GDMechanic.Wiring.Attributes
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public class GroupAttribute : MechanicAttribute, IClassWirer
	{
		private readonly string _group;
		
		/// <summary>
		/// Adds this node to the specified group.
		/// </summary>
		/// <param name="group"></param>
		public GroupAttribute(string group)
		{
			_group = group;
		}

		public void Wire(Node node, CachedNodeType @class)
		{
			node.AddToGroup(_group);
		}
	}
}