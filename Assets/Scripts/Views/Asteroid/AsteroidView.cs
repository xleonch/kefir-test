using Controllers.Asteroid;
using UnityEngine;
using Views;

public class AsteroidView : MonoBehaviour
{
	// ToDo: К модели не должны обращаться через вью, сделать private
	public AsteroidModel Model;
	public AsteroidController Controller;

	private GameViewUtils _gameViewUtils;
	private Vector2 _direction;

	private void Start()
	{
		_gameViewUtils = new GameViewUtils();
		transform.position = _gameViewUtils.GetRandomPointOnEdgeOfScreen();
		_direction = _gameViewUtils.GetRandomPointOnScreen();
	}

	private void Update()
	{
		transform.position = Controller.Move(transform.position, _direction);
		_gameViewUtils.ScreenWarp(gameObject);
	}

	private void OnTriggerEnter2D()
	{
		gameObject.SetActive(false);
		// ToDo: Add to the object pool two copies of asteroid
	}
}