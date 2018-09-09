using Godot;

namespace GDMechanic.Extensions
{
	public static class TimerExtensions
	{
		public static void Start(this Timer timer, float waitTime)
		{
			float defaultTime = timer.WaitTime;
			timer.SetWaitTime(waitTime);
			timer.Start();
			timer.SetWaitTime(defaultTime);
		}
	}
}