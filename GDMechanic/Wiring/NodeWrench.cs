using System.Collections.Generic;
using System.Linq;
using GDMechanic.Wiring.Attributes;
using GDMechanic.Wiring.CachedNodeInfos;
using Godot;

namespace GDMechanic.Wiring
{
	internal class NodeWrench
	{
	
		private readonly CachedNodeType _cachedNodeType;
	
		private readonly IClassWirer[] _classWirers;
	
		private readonly Dictionary<CachedNodeStateInfo, IStateWirer[]> _stateWirers = new Dictionary<CachedNodeStateInfo, IStateWirer[]>();
		private readonly Dictionary<CachedNodeMethodInfo, IMethodWirer[]> _methodWirers = new Dictionary<CachedNodeMethodInfo, IMethodWirer[]>();
	
		public NodeWrench(CachedNodeType cachedNodeType)
		{
			_cachedNodeType = cachedNodeType;

			_classWirers = cachedNodeType.Attributes.Where(a => a is IClassWirer).Cast<IClassWirer>().ToArray();

			foreach (CachedNodeStateInfo state in cachedNodeType.GetStates())
			{
				_stateWirers[state] = state.Attributes.Where(a => a is IStateWirer).Cast<IStateWirer>().ToArray();
			}
		
			foreach (CachedNodeMethodInfo method in cachedNodeType.GetMethods())
			{
				_methodWirers[method] = method.Attributes.Where(a => a is IMethodWirer).Cast<IMethodWirer>().ToArray();
			}
		}
	
		public void Wire(Node node)
		{	
			WireClass(node);
			WireStates(node);
			WireMethods(node);
		}

		private void WireClass(Node node)
		{
			foreach (IClassWirer wirer in _classWirers)
			{
				wirer.Wire(node, _cachedNodeType);
			}
		}
	
		private void WireStates(Node node)
		{
			foreach (KeyValuePair<CachedNodeStateInfo,IStateWirer[]> wirers in _stateWirers)
			{
				foreach (IStateWirer wirer in wirers.Value)
				{
					wirer.Wire(node, wirers.Key);
				}
			}
		}

		private void WireMethods(Node node)
		{
			foreach (KeyValuePair<CachedNodeMethodInfo,IMethodWirer[]> wirers in _methodWirers)
			{
				foreach (IMethodWirer wirer in wirers.Value)
				{
					wirer.Wire(node, wirers.Key);
				}
			}
		}
	}
}