using UnityEngine;

public sealed class UIView : MonoBehaviour
{
    [SerializeField] private PlayerHUDView _firstPlayer;
    [SerializeField] private PlayerHUDView _secondPlayer;

    public PlayerHUDView FirstPlayer => _firstPlayer;
    public PlayerHUDView SecondPlayer => _secondPlayer;
}
