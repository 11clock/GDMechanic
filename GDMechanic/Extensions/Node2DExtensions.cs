using Godot;

namespace GDMechanic.Extensions
{
	public static class Node2DExtensions
	{
		public static void SetPositionX(this Node2D node, float x)
		{
			node.Position = new Vector2(x, node.Position.y);
		}
	
		public static void SetPositionY(this Node2D node, float y)
		{
			node.Position = new Vector2(node.Position.x, y);
		}
	
		public static void TranslateX(this Node2D node, float xDisplacement)
		{
		
			node.Position += new Vector2(xDisplacement, 0);
		}
	
		public static void TranslateY(this Node2D node, float yDisplacement)
		{
			node.Position += new Vector2(0, yDisplacement);
		}
	
		public static void SetScaleX(this Node2D node, float x)
		{
			node.Scale = new Vector2(x, node.Scale.y);
		}
	
		public static void SetScaleY(this Node2D node, float y)
		{
			node.Scale = new Vector2(node.Scale.x, y);
		}
	
		public static void ScaleX(this Node2D node, float xDisplacement)
		{
			node.Scale += new Vector2(xDisplacement, 0);
		}

		public static void ScaleY(this Node2D node, float yDisplacement)
		{
			node.Scale += new Vector2(0, yDisplacement);
		}
	}
}