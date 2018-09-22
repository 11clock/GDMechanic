using Godot;

namespace GDMechanic.Extensions
{
	public static class Vector3Extensions
	{
		public static Vector2 ToVector2(this Vector3 vector3)
		{
			return new Vector2(vector3.x, vector3.y);
		}

		public static Vector2[] ToVector2(this Vector3[] vector3s)
		{
			Vector2[] vector2s = new Vector2[vector3s.Length];
			for (int i = 0; i < vector3s.Length; i++)
			{
				vector2s[i] = vector3s[i].ToVector2();
			}

			return vector2s;
		}
	}
}