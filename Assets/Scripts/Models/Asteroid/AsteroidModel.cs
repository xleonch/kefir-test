using UnityEngine;

[CreateAssetMenu]
public class AsteroidModel : ScriptableObject
{
	[Header("Asteroid Settings")]
	public float MinStartMoveSpeed;
	public float MaxStartMoveSpeed;

	public AsteroidModel(float minStartMoveSpeed,
		float maxStartMoveSpeed)
	{
		MinStartMoveSpeed = minStartMoveSpeed;
		MaxStartMoveSpeed = maxStartMoveSpeed;
	}
}