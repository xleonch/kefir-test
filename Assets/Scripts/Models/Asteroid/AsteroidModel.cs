using UnityEngine;

[CreateAssetMenu]
public class AsteroidModel : ScriptableObject
{
	[Header("Asteroid Settings")]
	public float MinStartMoveSpeed;
	public float MaxStartMoveSpeed;
	public float SpawnDuration;
	public float SpawnDistance;
	public float SpawnDelay;

	public AsteroidModel(float minStartMoveSpeed,
		float maxStartMoveSpeed,
		float spawnDuration,
		float spawnDistance,
		float spawnDelay)
	{
		MinStartMoveSpeed = minStartMoveSpeed;
		MaxStartMoveSpeed = maxStartMoveSpeed;

		SpawnDuration = spawnDuration;
		SpawnDelay = spawnDelay;
		SpawnDistance = spawnDistance;
	}
}