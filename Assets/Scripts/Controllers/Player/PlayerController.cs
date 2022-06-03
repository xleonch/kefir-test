using UnityEngine;
using Models.Player;

namespace Controllers.Player
{
	public class PlayerController
	{
		private readonly PlayerModel _model;
		//private ObjectPool<GameObject> _pool;

		public PlayerController(PlayerModel model) => _model = model;

		public Vector2 HandleAccelerate(Vector2 currPosition, float isMoving, Vector2 upDirection)
		{
			var moveSpeed = _model.MoveSpeed;
			var maxMoveSpeed = _model.MaxMoveSpeed;
			var accelerationRate = _model.AccelerationRate;
			var decelerationRate = _model.DecelerationRate;
			var newPosition = currPosition;

			/*Debug.Log(playerMoveSpeed);*/

			if (isMoving > 0 && moveSpeed < maxMoveSpeed)
			{
				_model.MoveSpeed += accelerationRate * Time.deltaTime;
				newPosition = Move(currPosition, upDirection);
			}
			else if (moveSpeed > decelerationRate * Time.deltaTime)
			{
				_model.MoveSpeed -= decelerationRate * Time.deltaTime;
				newPosition = Move(currPosition, upDirection);
			}

			return newPosition;
		}

		// ToDo: IMovable
		private Vector2 Move(Vector2 currPosition, Vector2 upDirection)
		{
			var moveSpeed = _model.MoveSpeed;
			var newPosition = currPosition + upDirection * (moveSpeed * Time.deltaTime);

			return newPosition;
		}

		// ToDo: IRotable (xleonch: может быть)
		public Quaternion Rotate(float turnDirection)
		{
			var rotationSpeed = _model.RotationSpeed;
			var rotationAngle = turnDirection * (rotationSpeed * Time.deltaTime);

			return Quaternion.Euler(new Vector3(0, 0, rotationAngle));
		}
	}
}