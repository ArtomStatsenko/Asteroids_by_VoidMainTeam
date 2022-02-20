using UnityEngine;

public sealed class PlayerShipMovement : MovementController
{
    private string _horizontal;
    private string _vertical;

    public PlayerShipMovement(PlayerManager player)
    {
        if (player == PlayerManager.First)
        {
            _vertical = AxisManager.VERTICAL_WS;
            _horizontal = AxisManager.HORIZONTAL_AD;
        }
        else
        {
            _vertical = AxisManager.VERTICAL_ARROW;
            _horizontal = AxisManager.HORIZONTAL_ARROW;
        }
    }

    public override Vector3 GetMovementDirection()
    {
        float vertical = Input.GetAxis(_vertical);
        float horizontal = Input.GetAxis(_horizontal);
        return new Vector3(horizontal, 0, vertical);
    }
}
