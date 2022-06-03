using UnityEngine;
using Controllers.Player;
using Models.Player;
using UnityEngine.InputSystem;

namespace Views.Player
{
	public class PlayerView : MonoBehaviour
	{
		// ToDo: К модели не должны обращаться через вью, сделать private
		public PlayerModel Model;
		public PlayerController Controller;

		private GameViewUtils _gameViewUtils;

		private void Awake()
		{
			_gameViewUtils = new GameViewUtils();
		}

		private void Update()
		{
			var position = transform.position;
			var isMoving = Model.IsMoving;
			var isRotating = Model.IsRotating;
			// SetPosAndRot
			if (isRotating != 0)
			{
				var rotation = Controller.Rotate(isRotating);
				transform.rotation *= rotation;
			}
			Model.CurrentRotation = transform.rotation;

			position = Controller.HandleAccelerate(position, isMoving, transform.up);
			transform.position = position;
			Model.CurrentPosition = position;

			//Debug.Log(Model.CurrentRotation.eulerAngles.z);

			_gameViewUtils.ScreenWarp(gameObject);
		}

		public void OnAccelerate(InputAction.CallbackContext context) => Model.IsMoving = context.ReadValue<float>();
		public void OnRotate(InputAction.CallbackContext context) => Model.IsRotating = context.ReadValue<float>();
	}
}