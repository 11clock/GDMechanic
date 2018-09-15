using System;
using System.Collections.Generic;
using Godot;

namespace GDMechanic.Extensions
{
	public static class SceneTreeExtensions
	{
	
		public static T GetNodeOfType<T>(this SceneTree sceneTree) where T : Node {
			return sceneTree.Root.GetChildOfTypeDeep<T>();
		}
		
		public static Node GetNodeOfType(this SceneTree sceneTree, Type type) {
			return sceneTree.GetRoot().GetChildOfTypeDeep(type);
		}
	
		public static IEnumerable<object> GetNodesOfType<T>(this SceneTree sceneTree) where T : Node {
			return sceneTree.Root.GetChildrenOfTypeDeep<T>();
		}
	}
}