using UnityEngine;

namespace Models.Projectile
{
	[CreateAssetMenu]
	public class ProjectileModel : ScriptableObject
	{
		[Header("Projectile settings")]
		public float ProjectileSpeed;
		public float ProjectileDuration;

		public ProjectileModel(
			float projectileSpeed,
			float projectileDuration)
		{
			ProjectileSpeed = projectileSpeed;
			ProjectileDuration = projectileDuration;
		}
	}
}