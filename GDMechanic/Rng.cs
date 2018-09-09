using System;

namespace GDMechanic
{
	/// <summary>
	///  Reimplementation of GDScript's "rand" functions.
	/// </summary>
	/// <remarks>
	///	Uses System.Random under the hood, so it will not have the same output as GDScript's "rand" functions.
	/// </remarks>
	public static class Rng {
	
		private static Random _rand;

		static Rng() {
			_rand = new Random();
		}
	
		/*---Reimplementing rand methods from GDScript---*/
		
		/// <summary>
		/// Random range, any floating point value between <c>from</c> and <c>to</c>.
		/// </summary>
		/// <param name="from"></param>
		/// <param name="to"></param>
		public static float RandRange(float from, float to)
		{
			return _rand.NextFloat() * (to - from) + from;
		}
		
		/// <summary>
		/// Returns a random floating point value between 0 and 1.
		/// </summary>
		public static float Randf()
		{
			return _rand.NextFloat();
		}
		
		/// <summary>
		/// Returns a random 32 bit integer.
		/// </summary>
		public static int Randi()
		{
			return _rand.Next();
		}
	
		/// <summary>
		/// Randomizes the seed (or the internal state) of the random number generator.
		/// </summary>
		public static void Randomize()
		{
			_rand = new Random();
		}
		
		/// <summary>
		/// Sets seed for the random number generator.
		/// </summary>
		/// <param name="seed"></param>
		public static void Seed(int seed)
		{
			_rand = new Random(seed);
		}
	
		/*---Extra methods for convenience---*/
		
		/// <summary>
		/// Random range, any integer value between <c>from</c> and <c>to</c> exclusive.
		/// </summary>
		/// <param name="from"></param>
		/// <param name="to"></param>
		public static float RandRange(int from, int to)
		{
			return _rand.Next(from, to);
		}
		
		/// <summary>
		/// Random range, any integer value between 0 and <c>to</c> exclusive.
		/// </summary>
		/// <param name="to"></param>
		public static int RandRange(int to)
		{
			return _rand.Next(to);
		}
		
		/// <summary>
		/// Random range, any floating point value between 0 and <c>to</c>.
		/// </summary>
		/// <param name="to"></param>
		public static float RandRange(float to)
		{
			return RandRange(0f, to);
		}
		
		/// <summary>
		/// Randomly returns true or false, based on <c>ratio</c>.
		/// </summary>
		/// <param name="ratio"></param>
		public static bool Chance(float ratio)
		{
			return Randf() < ratio;
		}

		private static float NextFloat(this Random rand)
		{
			return (float) rand.NextDouble();
		}
    
	}
}