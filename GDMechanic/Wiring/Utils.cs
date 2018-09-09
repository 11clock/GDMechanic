namespace GDMechanic.Wiring
{
	public static class Utils
	{
		/// <summary>
		/// Converts <c>name</c> to PascalCase.
		/// </summary>
		/// <param name="name"></param>
		/// <returns>The normalized name.</returns>
		public static string NormalizeMemberName(string name)
		{
			string normalized = name.StartsWith("_") ? name.Substring(1) : name;

			return normalized.Length < 2 ? normalized : normalized.Substring(0, 1).ToUpper() + normalized.Substring(1);
		}
	}
}