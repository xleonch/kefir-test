using System;
using Controllers.Projectile;
using Models.Projectile;
using UnityEngine;

namespace Views.Projectile
{
	public class ProjectileView : MonoBehaviour
	{
		// ToDo: К модели не должны обращаться через вью, сделать private
		public ProjectileModel Model;
		public ProjectileController Controller;

		private GameViewUtils _gameViewUtils;

		private void Start()
		{
			_gameViewUtils = new GameViewUtils();
		}

		private void Update()
		{
			var position = transform.position;

			transform.position = Controller.Move(position, transform.up);

			_gameViewUtils.ScreenWarp(gameObject);
		}
	}
}