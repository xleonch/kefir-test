using Models.Projectile;
using UnityEngine;

namespace Controllers.Projectile
{
	public class ProjectileController
	{
		private readonly ProjectileModel _model;

		public ProjectileController(ProjectileModel model) => _model = model;

		public Vector2 Move(Vector2 currPosition, Vector2 upDirection)
		{
			var moveSpeed = _model.ProjectileSpeed;
			var newPosition = currPosition + upDirection * (moveSpeed * Time.deltaTime);

			return newPosition;
		}
	}
}