using System.Collections;
using Controllers.Player;
using Controllers.Projectile;
using Controllers.Asteroid;
using Models.Player;
using Models.Projectile;
using Views.Player;
using Views.Projectile;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Pool;

namespace Core
{
	public class GameManager : MonoBehaviour
	{
		// Prefab
		[SerializeField] private GameObject _playerPrefab;
		[SerializeField] private GameObject _projectilePrefab;
		[SerializeField] private GameObject _asteroidPrefab;

		// Model
		[SerializeField] private PlayerModel _playerModelAsset;
		[SerializeField] private ProjectileModel _projectileModelAsset;
		[SerializeField] private AsteroidModel _asteroidModelAsset;

		private InputActions _inputActions;
		private ObjectPool<GameObject> _projectilePool;
		private ObjectPool<GameObject> _asteroidsPool;

		private void Awake()
		{
			// Player
			var player = Instantiate(_playerPrefab);
			var playerModel = _playerModelAsset;
			var playerView = player.AddComponent<PlayerView>();
			var playerController = new PlayerController(playerModel);

			playerView.Model = playerModel;
			playerView.Controller = playerController;

			// Input. ToDo: Возможно, должно находиться там, где игрок
			_inputActions = new InputActions();
			_inputActions.Player.Acceleration.performed += playerView.OnAccelerate;
			_inputActions.Player.Rotation.performed += playerView.OnRotate;
			_inputActions.Player.Shooting.performed += SpawnProjectile;

			_inputActions.Player.Acceleration.canceled += _ => playerView.Model.IsMoving = 0;
			_inputActions.Player.Rotation.canceled += _ => playerView.Model.IsRotating = 0;

			// ObjectPool
			_projectilePool = new ObjectPool<GameObject>(() => Instantiate(_projectilePrefab),
				actionOnGet: (obj) => obj.SetActive(true),
				actionOnRelease: (obj) => obj.SetActive(false),
				Destroy,
				collectionCheck: false,
				defaultCapacity: 10,
				maxSize: 10); // custom

			_asteroidsPool = new ObjectPool<GameObject>(createFunc: () => Instantiate(_asteroidPrefab),
				actionOnGet: (obj) => obj.SetActive(true),
				actionOnRelease: (obj) => obj.SetActive(false),
				actionOnDestroy: Destroy,
				collectionCheck: false,
				defaultCapacity: 10,
				maxSize: 10); // custom


			SpawnAsteroids();
		}

		// ToDo: Вынести в отдельный класс Spawner, реализовать ISpawnable?
		private void SpawnProjectile(InputAction.CallbackContext context)
		{
			// Use ObjectPool
			var projectile = _projectilePool.Get();

			var duration = _projectileModelAsset.ProjectileDuration;
			var position = _playerModelAsset.CurrentPosition;
			var rotation = _playerModelAsset.CurrentRotation;

			// AddToView
			projectile.transform.SetPositionAndRotation(position, rotation);

			if (!projectile.TryGetComponent(out ProjectileView _))
			{
				var model = _projectileModelAsset;
				var view = projectile.AddComponent<ProjectileView>();
				var controller = new ProjectileController(model);

				view.Model = model;
				view.Controller = controller;
			}

			var projectileCoroutine = ReleaseFromPoolAfter(_projectilePool, duration, projectile);
			StartCoroutine(projectileCoroutine);
		}

		// ToDo: Вынести в отдельный класс Spawner, реализовать ISpawnable?
		private void SpawnAsteroids()
		{
			for (var i = 0; i < 10; i++)
			{
				var asteroid = _asteroidsPool.Get();

				if (!asteroid.TryGetComponent(out AsteroidView _))
				{
					var model = _asteroidModelAsset;
					var view = asteroid.AddComponent<AsteroidView>();
					var controller = new AsteroidController(model);

					view.Model = model;
					view.Controller = controller;
				}

				// Время до поражения
				/*var asteroidCoroutine = ReleaseFromPoolAfter(_asteroidsPool, duration, asteroid);*/
				/*StartCoroutine(asteroidCoroutine);*/
			}
		}

		private IEnumerator ReleaseFromPoolAfter(IObjectPool<GameObject> pool, float seconds, GameObject instance)
		{
			yield return new WaitForSeconds(seconds);
			pool.Release(instance);
		}

		private void OnApplicationQuit()
		{
			_projectilePool.Dispose();
			_asteroidsPool.Dispose();
		}

		private void OnEnable() => _inputActions.Player.Enable();
		private void OnDisable() => _inputActions.Player.Disable();
	}
}