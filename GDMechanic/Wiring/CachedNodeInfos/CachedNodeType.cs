using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GDMechanic.Wiring.CachedNodeInfos
{
	/// <summary>
	/// Wrapper for a cached node-related Type and corresponding cached members.
	/// </summary>
	public class CachedNodeType : CachedNodeMemberInfo
	{
		/// <summary>
		/// The original Type that has been cached.
		/// </summary>
		public Type Type { get; }
	
		private readonly Dictionary<string, CachedNodeStateInfo> _states  =
			new Dictionary<string, CachedNodeStateInfo>();

		private readonly Dictionary<string, CachedNodeMethodInfo> _methods =
			new Dictionary<string, CachedNodeMethodInfo>();

		public CachedNodeType(Type type) : base(type, null)
		{
			Type = type;
		
			FieldInfo[] fields = type.GetFields(Core.Filter);
			foreach (FieldInfo field in fields)
			{
				_states[field.Name] = new CachedNodeStateInfo(field, this);
			}

			PropertyInfo[] properties = type.GetProperties(Core.Filter);
			foreach (PropertyInfo property in properties)
			{
				_states[property.Name] = new CachedNodeStateInfo(property, this);
			}

			MethodInfo[] methods = type.GetMethods(Core.Filter);
			foreach (MethodInfo method in methods)
			{
				_methods[method.Name] = new CachedNodeMethodInfo(method, this);
			}
		}

		public CachedNodeStateInfo GetState(string name)
		{
			return _states[name];
		}
	
		public CachedNodeStateInfo[] GetStates()
		{
			return _states.Values.ToArray();
		}

		public CachedNodeMethodInfo GetMethod(string name)
		{
			return _methods[name];
		}

		public CachedNodeMethodInfo[] GetMethods()
		{
			return _methods.Values.ToArray();
		}
	}
}