using UnityEngine;

namespace Views
{
	public class GameViewUtils
	{
		private bool _isWrappingX;
		private bool _isWrappingY;

		private readonly Camera _mainCamera;

		public GameViewUtils()
		{
			_mainCamera = Camera.main;
		}

		public void ScreenWarp(GameObject instance)
		{
			var spriteRenderer = instance.GetComponent<SpriteRenderer>();

			if (spriteRenderer.isVisible)
			{
				_isWrappingX = false;
				_isWrappingY = false;
				return;
			}

			if (_isWrappingX && _isWrappingY)
			{
				return;
			}

			var newPosition = instance.transform.position;

			if (_mainCamera != null)
			{
				var viewportPosition = _mainCamera.WorldToViewportPoint(newPosition);

				if (!_isWrappingX && viewportPosition.x is > 1 or < 0)
				{
					newPosition.x = -newPosition.x;
					_isWrappingX = true;
				}

				if (!_isWrappingY && viewportPosition.y is > 1 or < 0)
				{
					newPosition.y = -newPosition.y;
					_isWrappingY = true;
				}
			}

			instance.transform.position = newPosition;
		}

		public Vector3 GetRandomPointOnScreen()
		{
			return _mainCamera.ViewportToWorldPoint(new Vector2(Random.value, Random.value));
		}

		public Vector2 GetRandomPointOnEdgeOfScreen()
		{
			var randomEdge = Random.Range(0, 2);

			if (randomEdge == 0)
			{
				// ToDo: Реализовать спавн астероидов правильный
				var pos =_mainCamera.ViewportToWorldPoint(new Vector2(Random.Range(0, 2) * 2 - 1, Random.value));
				return pos;
			}

			//var pointOnEdgeY = new Vector2(Random.Range(0f, 1f), Random.Range(0, 2) * 2 - 1);
			var pos1=_mainCamera.ViewportToWorldPoint(new Vector2(Random.value, Random.Range(0, 2) * 2 - 1));
			return pos1;
		}

		public Vector2 GetSpawnPointNear(float spawnDistance, Vector2 position)
		{
			var spawnDirection = Random.insideUnitCircle.normalized;
			var spawnPoint = spawnDirection * spawnDistance;

			spawnPoint += position;

			return spawnPoint;
		}
	}
}