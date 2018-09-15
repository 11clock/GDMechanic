using Godot;

namespace GDMechanic.Extensions
{
	public static class AnimatedSpriteExtensions
	{
		public static void EnsurePlaying(this AnimatedSprite animatedSprite, string anim)
		{
			if (animatedSprite.Animation != anim)
			{
				animatedSprite.Play(anim);
			}
		}
		
		public static void EnsurePlaying(this AnimatedSprite animatedSprite, string anim, float speedScale)
		{
			if (animatedSprite.Animation != anim)
			{
				animatedSprite.Play(anim);
			}

			animatedSprite.SpeedScale = speedScale;
		}
	}
}