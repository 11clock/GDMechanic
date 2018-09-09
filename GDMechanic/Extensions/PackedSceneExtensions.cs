using Godot;

namespace GDMechanic.Extensions
{
	public static class PackedSceneExtensions
	{

		public static T Instance<T>(this PackedScene packedScene) where T: Node
		{
			T instance = (T) packedScene.Instance();
			return instance;
		}
	
		public static T InstanceToParent<T>(this PackedScene packedScene, Node parent) where T: Node
		{
			T instance = packedScene.Instance<T>();
			parent.AddChild(instance);
			return instance;
		}

		public static T InstanceToCurrentScene<T>(this PackedScene packedScene, SceneTree tree) where T : Node
		{
			return packedScene.InstanceToParent<T>(tree.CurrentScene);
		}
	
		public static T InstanceToRoot<T>(this PackedScene packedScene, SceneTree tree) where T : Node
		{
			return packedScene.InstanceToParent<T>(tree.Root);
		}
	}
}