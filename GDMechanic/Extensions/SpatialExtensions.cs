using Godot;

namespace GDMechanic.Extensions
{
	public static class SpatialExtensions
	{
		public static void SetTranslationX(this Spatial node, float x)
		{
			node.Translation = new Vector3(x, node.Translation.y, node.Translation.z);
		}
	
		public static void SetTranslationY(this Spatial node, float y)
		{
			node.Translation = new Vector3(node.Translation.x, y, node.Translation.z);
		}
		
		public static void SetTranslationZ(this Spatial node, float z)
		{
			node.Translation = new Vector3(node.Translation.x, node.Translation.y, z);
		}
	
		public static void TranslateX(this Spatial node, float xDisplacement)
		{
			node.Translation += new Vector3(xDisplacement, 0, 0);
		}
	
		public static void TranslateY(this Spatial node, float yDisplacement)
		{
			node.Translation += new Vector3(0, yDisplacement, 0);
		}
		
		public static void TranslateZ(this Spatial node, float zDisplacement)
		{
			node.Translation += new Vector3(0, 0, zDisplacement);
		}

		
		
		public static void SetRotationDegreesX(this Spatial node, float x)
		{
			node.RotationDegrees = new Vector3(x, node.RotationDegrees.y, node.RotationDegrees.z);
		}
	
		public static void SetRotationDegreesY(this Spatial node, float y)
		{
			node.RotationDegrees = new Vector3(node.RotationDegrees.x, y, node.RotationDegrees.z);
		}
		
		public static void SetRotationDegreesZ(this Spatial node, float z)
		{
			node.RotationDegrees = new Vector3(node.RotationDegrees.x, node.RotationDegrees.y, z);
		}
	
		
		
		public static void SetScaleX(this Spatial node, float x)
		{
			node.Scale = new Vector3(x, node.Scale.y, node.Scale.z);
		}
	
		public static void SetScaleY(this Spatial node, float y)
		{
			node.Scale = new Vector3(node.Scale.x, y, node.Scale.z);
		}
		
		public static void SetScaleZ(this Spatial node, float z)
		{
			node.Scale = new Vector3(node.Scale.x, node.Scale.y, z);
		}
	
		public static void ScaleX(this Spatial node, float xDisplacement)
		{
			node.Scale += new Vector3(xDisplacement, 0, 0);
		}

		public static void ScaleY(this Spatial node, float yDisplacement)
		{
			node.Scale += new Vector3(0, yDisplacement, 0);
		}
		
		public static void ScaleZ(this Spatial node, float zDisplacement)
		{
			node.Scale += new Vector3(0, 0, zDisplacement);
		}
	}
}