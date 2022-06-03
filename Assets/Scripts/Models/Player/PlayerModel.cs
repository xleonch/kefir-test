using UnityEngine;

namespace Models.Player
{
	[CreateAssetMenu]
	public class PlayerModel : ScriptableObject
	{
		[Header("Movement settings")]
		public float MaxMoveSpeed;
		public float RotationSpeed;
		public float AccelerationRate;
		public float DecelerationRate;

		public Vector2 CurrentPosition { get; set; }
		public Quaternion CurrentRotation { get; set; }
		public float MoveSpeed { get; set; } // OnMoveSpeedChanged, private set, public get
		public float IsMoving { get; set; }
		public float IsRotating { get; set; }

		public PlayerModel(float maxMoveSpeed,
			float rotationSpeed,
			float accelerationRate,
			float decelerationRate)
		{
			MaxMoveSpeed = maxMoveSpeed;

			AccelerationRate = accelerationRate;
			DecelerationRate = decelerationRate;

			RotationSpeed = rotationSpeed;
		}
	}
}