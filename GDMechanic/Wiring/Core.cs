using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using GDMechanic.Wiring.CachedNodeInfos;
using Godot;

namespace GDMechanic.Wiring
{
	/// <summary>
	/// The foundation of GDMechanic's wiring system.
	/// </summary>
	public static class Core {
	
		private static readonly Dictionary<Type, NodeWrench> NodeWrenches = new Dictionary<Type, NodeWrench>();

		public static readonly Dictionary<Type, CachedNodeType> CachedNodeTypes = new Dictionary<Type, CachedNodeType>();
	
		public static BindingFlags Filter => BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic |
		                                     BindingFlags.Instance;
	
		static Core()
		{
			Stopwatch stopwatch = Stopwatch.StartNew();
			Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies()
				.Where(a => !(a.GetName().Name.Contains("GodotSharp")
				              || a.GetName().Name.Contains("System")
				              || a.GetName().Name.Contains("mscorlib")))
				.ToArray();
			GD.Print("Caching types not in System and GodotSharp...");
			foreach (Assembly assembly in assemblies)
			{
				GD.Print($"Caching {assembly.GetName().Name}...");
				Type[] publicTypes = assembly.GetExportedTypes();
				foreach (Type type in publicTypes)
				{
					if (typeof(Node).IsAssignableFrom(type))
					{
						GenerateTypeSupport(type);
					}
				}
			}
			stopwatch.Stop();
			GD.Print($"Assemblies Searched: {assemblies.Length}");
			GD.Print($"Cached Nodes: {NodeWrenches.Count} in {stopwatch.ElapsedMilliseconds} ms / {stopwatch.ElapsedTicks} ticks");
		}
		
		/// <summary>
		/// Wires all GDMechanic attributes embedded in this node's class definition.
		/// To be called in _Ready() for any nodes that use GDMechanic's wiring.
		/// </summary>
		/// <param name="node"></param>
		public static void Wire(this Node node)
		{
			Type nodeType = node.GetType();

			if (NodeWrenches.TryGetValue(nodeType, out NodeWrench nodeWrench))
			{
				nodeWrench.Wire(node);
			}
			else
			{
				GenerateTypeSupport(nodeType);
				GD.Print($"Lazily cached {nodeType.FullName}");
			}
		
		}

		private static void GenerateTypeSupport(Type type)
		{
			CachedNodeType newCachedType = new CachedNodeType(type);
			CachedNodeTypes[type] = newCachedType;
			NodeWrenches[type] = new NodeWrench(newCachedType);
		}
	}
}