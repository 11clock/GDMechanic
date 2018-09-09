using System.Reflection;

namespace GDMechanic.Wiring.CachedNodeInfos
{
	/// <summary>
	/// Wrapper for a cached node-related MethodInfo.
	/// </summary>
	public class CachedNodeMethodInfo : CachedNodeMemberInfo
	{
		/// <summary>
		/// The original MethodInfo that has been cached.
		/// </summary>
		public MethodInfo Method { get; }

		public CachedNodeMethodInfo(MethodInfo method, CachedNodeType declaringType) : base(method, declaringType)
		{
			Method = method;
		}
	}
}