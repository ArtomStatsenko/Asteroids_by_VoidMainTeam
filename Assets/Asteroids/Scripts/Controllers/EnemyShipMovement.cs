using UnityEngine;

public sealed class EnemyShipMovement : MovementController
{
    private float _nextChangeDirectionTime;
    private Vector3 _movement;

    public override Vector3 GetMovementDirection()
    {
        if (Time.time > _nextChangeDirectionTime)
        {
            _nextChangeDirectionTime += Random.Range(1f, 5f);
            _movement = new Vector3(Random.Range(-1f, 1f), 0f, 0f);
        }

        return _movement;
    }
}
