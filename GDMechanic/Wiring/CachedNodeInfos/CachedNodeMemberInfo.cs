using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GDMechanic.Wiring.CachedNodeInfos
{
	
	/// <summary>
	/// Provides a base for cached node-related MemberInfos.
	/// </summary>
	public abstract class CachedNodeMemberInfo
	{
		public string Name { get; }
		public IEnumerable<Attribute> Attributes { get; }
		public CachedNodeType DeclaringType { get; }

		protected CachedNodeMemberInfo(MemberInfo member, CachedNodeType declaringType)
		{
			Name = member.Name;
			Attributes = member.GetCustomAttributes();
			DeclaringType = declaringType;
		}

		public T GetAttribute<T>() where T: Attribute
		{
			T[] compatibleAttributes = Attributes.OfType<T>().ToArray();

			return compatibleAttributes.Length > 0 ? compatibleAttributes[0] : null;
		}

		public Attribute[] GetAttributes()
		{
			return Attributes.ToArray();
		}
	}
}