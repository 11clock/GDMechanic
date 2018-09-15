using System;
using System.Diagnostics;
using System.Reflection;
using Godot;
using Sigil;

namespace GDMechanic.Wiring.CachedNodeInfos
{
	/// <summary>
	/// Wrapper for a cached node-related FieldInfo or PropertyInfo.
	/// </summary>
	public class CachedNodeStateInfo : CachedNodeMemberInfo
	{
		/// <summary>
		/// The original FieldInfo or PropertyInfo that has been cached.
		/// </summary>
		public MemberInfo State { get; }
		
		public Type StateType { get; }

		private readonly Func<Node, object> _getValue;
		private readonly Action<Node, object> _setValue;

		public CachedNodeStateInfo(MemberInfo state, CachedNodeType declaringType) : base(state, declaringType)
		{
			State = state;
			switch (state)
			{
				case FieldInfo field:
					StateType = field.FieldType;
					break;
				case PropertyInfo property:
					StateType = property.PropertyType;
					break;
			}

			_getValue = CreateGetter();
			_setValue = CreateSetter();
		}

		public object GetValue(Node node)
		{
			if (_getValue != null)
			{
				return _getValue(node);
			}

			switch (State)
			{
				case FieldInfo field:
					return field.GetValue(node);
				case PropertyInfo property:
					return property.GetValue(node);
			}

			return null;
		}
	
		public void SetValue(Node node, object value)
		{
			if (_setValue != null)
			{
				_setValue(node, value);
			}
			else
			{
				switch (State)
				{
					case FieldInfo field:
						field.SetValue(node, value);
						break;
					case PropertyInfo property:
						property.SetValue(node, value);
						break;
				}
			}
		}

		private Func<Node, object> CreateGetter()
		{
			Emit<Func<Node, object>> getterMethod;
			Debug.Assert(State.ReflectedType != null, "state.ReflectedType != null");
			string methodName = State.ReflectedType.FullName+".get_" + Name;

			switch (State)
			{
				case FieldInfo field:
					if (field.FieldType.GetTypeInfo().IsValueType)
					{
						// Code emission doesn't work on primitives.
						return null;
					}
					else
					{
						getterMethod = Emit<Func<Node, object>>
							.NewDynamicMethod(methodName)
							.LoadArgument(0)
							.CastClass(DeclaringType.Type)
							.LoadField(field)
							.Return();
					}
				
					break;
				case PropertyInfo property:
					if (property.PropertyType.GetTypeInfo().IsValueType
						|| property.GetGetMethod(true) == null)
					{
						// Code emission doesn't work on primitives.
						return null;
					}
					else
					{
						getterMethod = Emit<Func<Node, object>>
							.NewDynamicMethod(methodName)
							.LoadArgument(0)
							.CastClass(DeclaringType.Type)
							.Call(property.GetGetMethod(true))
							.Return();
					}
					break;
				default: throw new ArgumentException("MemberInfo must be either a FieldInfo or a PropertyInfo.");
			}

			return getterMethod.CreateDelegate();
		}

		private Action<Node, object> CreateSetter()
		{
			Emit<Action<Node, object>> setterMethod;
			Debug.Assert(State.ReflectedType != null, "state.ReflectedType != null");
			string methodName = State.ReflectedType.FullName+".set_" + Name;
		
			switch (State)
			{
				case FieldInfo field:
					if (field.FieldType.GetTypeInfo().IsValueType)
					{
						// Code emission doesn't work on primitives.
						return null;
					}
					else
					{
						setterMethod = Emit<Action<Node, object>>
							.NewDynamicMethod(methodName)
							.LoadArgument(0)
							.CastClass(DeclaringType.Type)
							.LoadArgument(1)
							.CastClass(field.FieldType)
							.StoreField(field)
							.Return();
					}

					break;
			
				case PropertyInfo property:
					if (property.PropertyType.GetTypeInfo().IsValueType
					    || property.GetSetMethod(true) == null)
					{
						// Code emission doesn't work on primitives.
						return null;
					}
					else
					{
						setterMethod = Emit<Action<Node, object>>
							.NewDynamicMethod(methodName)
							.LoadArgument(0)
							.CastClass(DeclaringType.Type)
							.LoadArgument(1)
							.CastClass(property.PropertyType)
							.Call(property.GetSetMethod(true))
							.Return();
					}

					break;
			
				default: throw new ArgumentException("MemberInfo must be either a FieldInfo or a PropertyInfo.");
			}
		
			return setterMethod.CreateDelegate();
		}
	}
}