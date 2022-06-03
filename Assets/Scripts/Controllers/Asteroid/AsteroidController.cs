using UnityEngine;

namespace Controllers.Asteroid
{
    public class AsteroidController
    {
        private readonly AsteroidModel _model;
        private readonly float _moveSpeed;

        public AsteroidController(AsteroidModel model)
        {
            _model = model;
            _moveSpeed = Random.Range(_model.MinStartMoveSpeed, _model.MaxStartMoveSpeed);
        }

        public Vector3 Move(Vector2 currPosition, Vector2 direction)
        {
            var normalizedDirection = direction.normalized;
            var newPosition = currPosition + normalizedDirection * (_moveSpeed * Time.deltaTime);

            return newPosition;
        }
    }
}