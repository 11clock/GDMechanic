using Godot;

namespace GDMechanic.Extensions
{
	public static class Vector2Extensions
	{
		public static Vector3 ToVector3(this Vector2 vector2)
		{
			return new Vector3(vector2.x, vector2.y, 0f);
		}
		
		public static Vector3[] ToVector3(this Vector2[] vector2s)
		{
			Vector3[] vector3s = new Vector3[vector2s.Length];
			for (int i = 0; i < vector2s.Length; i++)
			{
				vector3s[i] = vector2s[i].ToVector3();
			}

			return vector3s;
		}
	}
}