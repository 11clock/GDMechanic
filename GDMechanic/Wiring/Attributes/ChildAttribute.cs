﻿using System;
using System.Linq;
using GDMechanic.Wiring.CachedNodeInfos;
using Godot;

namespace GDMechanic.Wiring.Attributes
{
	
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
	public class ChildAttribute : MechanicAttribute, IStateWirer
	{
		private readonly MatchTypes _matchType;
		
		/// <summary>
		/// Returns a reference to a child based on match type.
		/// </summary>
		/// <remarks>
		/// <c>MatchTypes.Name</c> (default): Fetches the child based on the field/property name converted to PascalCase.
		/// </remarks>
		/// <remarks>
		/// <c>MatchTypes.Type</c>: Fetches the first child that matches the field/property's type.
		/// </remarks>
		/// <param name="matchType"></param>
		public ChildAttribute(MatchTypes matchType = MatchTypes.Name) {
			_matchType = matchType;
		}

		public void Wire(Node node, CachedNodeStateInfo state)
		{

			Node nodeToReference;
		
			if (_matchType == MatchTypes.Type)
			{
				nodeToReference = (Node) node.GetChildren().First(c => state.StateType.IsInstanceOfType(c));
			}
			else
			{
				NodePath nodePath = Utils.NormalizeMemberName(state.Name);
				nodeToReference = node.GetNode(nodePath);
			}
			state.SetValue(node, nodeToReference);
		}
	}
}