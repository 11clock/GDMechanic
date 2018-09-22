using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

namespace GDMechanic.Extensions
{
	public static class NodeExtensions
	{
		public static T AddAndReturnChild<T>(this Node node, T child) where T : Node {
			node.AddChild(child);
			return child;
		}
		
		public static T RemoveAndReturnChild<T>(this Node node, T child) where T : Node {
			node.RemoveChild(child);
			return child;
		}
	
		public static Timer ConnectTimer(this Node node, NodePath path, string method) {
			Timer timer = (Timer)node.GetNode(path);
			timer.Connect("timeout", node, method);
			return timer;
		}

		public static Timer ConnectTimer(this Node node, Timer timer, string method) {
			timer.Connect("timeout", node, method);
			return timer;
		}

		public static Timer CreateConnectedTimer(this Node node, string method,
			Timer.TimerProcessMode processMode = Timer.TimerProcessMode.Idle, float waitTime = 1f, bool oneShot = false, bool autostart = false) {
			Timer timer = node.CreateConnectedTimer(node, method, processMode, waitTime, oneShot, autostart);
			return timer;
		}

		public static Timer CreateConnectedTimer(this Node node, Node target, string method,
			Timer.TimerProcessMode processMode = Timer.TimerProcessMode.Idle, float waitTime = 1f, bool oneShot = false, bool autostart = false) {
			Timer timer = node.AddAndReturnChild(new Timer());
			timer.ProcessMode = processMode;
			timer.WaitTime = waitTime;
			timer.OneShot = oneShot;
			timer.Autostart = autostart;
			target.ConnectTimer(timer, method);
			timer.Name = method;
			return timer;
		}
	
		public static IEnumerable<object> GetChildrenDeep(this Node node) {
			List<object> list = new List<object>();
			GetChildrenDeepHelper(node, list);
			return list.ToArray();
		}

		private static void GetChildrenDeepHelper(Node node, ICollection<object> list) {
			list.Add(node);
			foreach (Node child in node.GetChildren()) {
				if (child.GetChildCount() > 0) {
					GetChildrenDeepHelper(child, list);
				}
			}
		}

		public static IEnumerable<object> GetChildrenOfType<T>(this Node node) where T : Node {
			return node.GetChildren().OfType<T>();
		}

		public static IEnumerable<object> GetChildrenOfTypeDeep<T>(this Node node) where T : Node {
			return node.GetChildrenDeep().OfType<T>();
		}

		public static T GetChildOfType<T>(this Node node) where T : Node {
			return node.GetChildren().OfType<T>().FirstOrDefault();
		}

		public static T GetParentOfType<T>(this Node node) where T : Node {
			while (true) {
				Node parent = node.GetParent();
				switch (parent) {
					case null:
						return null;
					case T _:
						return parent as T;
				}
				node = parent;
			}
		}

		public static T GetChildOfTypeDeep<T>(this Node node) where T : Node {
			return node.GetChildrenDeep().OfType<T>().FirstOrDefault();
		}
		
		public static Node GetChildOfTypeDeep(this Node node, Type type) {
			return node.GetChildrenDeep().Cast<Node>().FirstOrDefault(type.IsInstanceOfType);
		}
	}
}