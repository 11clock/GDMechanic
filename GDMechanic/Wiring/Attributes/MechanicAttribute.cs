using System;
using GDMechanic.Wiring.CachedNodeInfos;
using Godot;

namespace GDMechanic.Wiring.Attributes
{
	/// <summary>
	/// Base class for GDMechanic's core attributes. Not required to be extended for wiring.
	/// </summary>
	/// <remarks>
	/// Only exists for core organization purses.
	/// </remarks>
	public abstract class MechanicAttribute : Attribute
	{
	
	}
	
	/// <summary>
	/// Registers an attribute to be wired based on field/property.
	/// </summary>
	public interface IStateWirer
	{
		void Wire(Node node, CachedNodeStateInfo state);
	}
	
	/// <summary>
	/// Registers an attribute to be wired based on method.
	/// </summary>
	public interface IMethodWirer
	{
		void Wire(Node node, CachedNodeMethodInfo method);
	}
	
	/// <summary>
	/// Registers an attribute to be wired based on class/type.
	/// </summary>
	public interface IClassWirer
	{
		void Wire(Node node, CachedNodeType @class);
	}
}