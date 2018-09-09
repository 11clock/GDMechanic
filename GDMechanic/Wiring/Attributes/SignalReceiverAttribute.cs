using System;
using GDMechanic.Wiring.CachedNodeInfos;
using Godot;

namespace GDMechanic.Wiring.Attributes
{
	
	[AttributeUsage(AttributeTargets.Method)]
	public class SignalReceiverAttribute : MechanicAttribute, IMethodWirer
	{
		private readonly string _source;
		private readonly string _signal;
		private readonly SourceTypes _sourceType;
		
		/// <summary>
		/// Connects a specified signal from a specified source.
		/// </summary>
		/// <remarks>
		/// <c>SourceTypes.Path</c> (default): Reads <c>source</c> as a node path.
		/// </remarks>
		/// <remarks>
		/// <c>SourceTypes.Reference</c>: Reads <c>source</c> as the name of a field under the target node which contains a reference to the source node.
		/// </remarks>
		/// <param name="source"></param>
		/// <param name="signal"></param>
		/// <param name="sourceType"></param>
		public SignalReceiverAttribute(string source, string signal,
			SourceTypes sourceType = SourceTypes.Path)
		{
			_source = (source.Equals("self") || source.Equals("this")) && sourceType == SourceTypes.Path
				? "."
				: source;
			_signal = signal;
			_sourceType = sourceType;
		}

		public void Wire(Node node, CachedNodeMethodInfo method)
		{
			Node source;
			switch (_sourceType)
			{
				case SourceTypes.Path:
					source = node.GetNode(_source);
					break;
				case SourceTypes.Reference:
					source = method.DeclaringType.GetState(_source).GetValue(node) as Node;
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			source?.Connect(_signal, node, method.Name);
		}
	}
}