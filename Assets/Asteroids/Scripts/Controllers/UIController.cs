public sealed class UIController
{
    private UIView _view;

    public UIController(AudioController audioController, GameModeManager gameMode)
    {
        _view = UnityEngine.Object.FindObjectOfType<UIView>();

        if (gameMode == GameModeManager.Multiplayer)
        {
            _view?.SecondPlayer.gameObject.SetActive(true);
        }
        else
        {
            _view?.SecondPlayer.gameObject.SetActive(false);
        }
    }

    public PlayerHUDView GetPlayerHUD(PlayerManager player)
    {
        if (player == PlayerManager.First)
        {
            return _view?.FirstPlayer;
        }
        else
        {
            return _view?.SecondPlayer;
        }
    }
}
